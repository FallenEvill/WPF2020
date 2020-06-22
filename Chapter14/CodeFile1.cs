using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
namespace Petzold.PopupContextMenu 
{ 
    public class PopupContextMenu : Window 
    { 
        ContextMenu menu; // Контекстное меню
        MenuItem itemBold, itemItalic; //Элементы меню
        MenuItem[] itemDecor;
        Inline inlClicked;
        [STAThread] 
        public static void Main() 
        { 
            Application app = new Application();
            app.Run(new PopupContextMenu()); 
        } 
        public PopupContextMenu() 
        { 
            Title = "Popup Context Menu";    
            menu = new ContextMenu();   // Создание контекстного меню
            // Add an item for "Bold". 
            itemBold = new MenuItem(); // Создание элемента меню
            itemBold.Header = "Bold";  
            menu.Items.Add(itemBold);    // Добавление элемента в меню
            // Add an item for "Italic". 
            itemItalic = new MenuItem(); 
            itemItalic.Header = "Italic";
            menu.Items.Add(itemItalic);    
            TextDecorationLocation[] locs = (TextDecorationLocation[])Enum.GetValues(typeof(TextDecorationLocation)); // Получение всех доступных визульных украшений текста  
            itemDecor = new MenuItem[locs.Length];  // Создание массива элементов меню       
            for (int i = 0; i < locs.Length; i++)     
            {          
                TextDecoration decor = new  TextDecoration();  // Новое визуальное украшение
                decor.Location = locs[i]; // Расположение украшения на тексте       
                itemDecor[i] = new MenuItem();          
                itemDecor[i].Header = locs[i] .ToString();  
                itemDecor[i].Tag = decor;      
                menu.Items.Add(itemDecor[i]); 
            }           
            // Use one handler for the entire  context menu.     
            menu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler (MenuOnClick)); // Создание обработчика клика по меню
            TextBlock text = new TextBlock();        
            text.FontSize = 32;         
            text.HorizontalAlignment =  HorizontalAlignment.Center;      
            text.VerticalAlignment =  VerticalAlignment.Center;        
            Content = text;               
            string strQuote = "To be, or not to be , that is the question";
            string[] strWords = strQuote.Split(); // Разбиение строки на массив слов     
            // Make each word a Run, and add to  the TextBlock.   
            foreach (string str in strWords)        
            {         
                Run run = new Run(str);    //  Создание объекта типа Run содержащего одно слово из строки 
                run.TextDecorations = new  TextDecorationCollection();         // Создание визуальных украшений для объекта
                text.Inlines.Add(run);        // Добавление объекта в текстовый блок
                text.Inlines.Add(" ");      
            }       
        }   
        protected override void  OnMouseRightButtonUp(MouseButtonEventArgs args)  // Перехват отпускания правой кнопки мыши 
        {           
            base.OnMouseRightButtonUp(args);        
            if ((inlClicked = args.Source as  Inline) != null) // Проверка, того, что отпускание кнопки произошло над одним из слов     
            {               
                itemBold.IsChecked = (inlClicked .FontWeight == FontWeights.Bold); // Отображение галочки, если выполнено условие    
                itemItalic.IsChecked = (inlClicked .FontStyle == FontStyles.Italic);       
                foreach (MenuItem item in itemDecor)                  
                    item.IsChecked = (inlClicked .TextDecorations.Contains(item.Tag as  TextDecoration));                     
                menu.IsOpen = true;  // Показ меню    
                args.Handled = true;   
            }         
        }    
        void MenuOnClick(object sender,  RoutedEventArgs args) // Перехват клика в меню  
        {          
            MenuItem item = args.Source as MenuItem; // Получение пункта меню, на который кликнули         
            item.IsChecked ^= true;        // Инвертирование bool значения (с false на true или обратно)  
            if (item == itemBold)          
                inlClicked.FontWeight =  (item.IsChecked ? FontWeights .Bold : FontWeights.Normal);  // Изменение шрифта слова в зависимости от того ставится или убирается флажок
            else if (item == itemItalic)inlClicked.FontStyle = (item.IsChecked ? FontStyles .Italic : FontStyles.Normal);  
            else         
            {      
                if (item.IsChecked)        
                    inlClicked.TextDecorations.Add (item.Tag as TextDecoration); // Добавление визуального украшения если был установлен флажок       
                else                  
                    inlClicked.TextDecorations .Remove(item.Tag as TextDecoration); // Удаление визуального украшения если флажок убран      
            }          
            (inlClicked.Parent as TextBlock).InvalidateVisual(); // Закрывает меню
        }    
    }
} 