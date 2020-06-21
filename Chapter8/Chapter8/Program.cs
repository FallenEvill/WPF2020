using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
namespace Petzold.SetFontSizeProperty 
{ 
    public class SetFontSizeProperty : Window 
    {
        [STAThread] 
        public static void Main()
        { 
            Application app = new Application(); 
            app.Run(new SetFontSizeProperty()); 
        } 
        public SetFontSizeProperty() 
        { Title = "Set FontSize Property"; 
            SizeToContent = SizeToContent.WidthAndHeight; // Окно автоматически меняет размер в случае изменения размера содержимого
            ResizeMode = ResizeMode.CanMinimize; // Возможность сворачивать окно
            FontSize = 16;
            double[] fntsizes = { 8, 16, 32 }; // массив размеров шрифтов       
            // Create Grid panel.          
            Grid grid = new Grid();  // новая сетка           
            Content = grid;      // сетка становиться содержимым окна
            // Define row and columns.       
            for (int i = 0; i < 2; i++)        // Создание 2 рядов
            {               
                RowDefinition row = new  RowDefinition();    
                row.Height = GridLength.Auto;                
                grid.RowDefinitions.Add(row);           
            }           
            for (int i = 0; i < fntsizes.Length; i++)       // Создание колонок по количесву элементов в массиве шрифтов(3 на данный момент)
            {               
                ColumnDefinition col = new  ColumnDefinition();  
                col.Width = GridLength.Auto;         
                grid.ColumnDefinitions.Add(col);         
            }                    
            for (int i = 0; i < fntsizes.Length; i++)      // Создание кнопок для каждого участка сетки
            {               
                Button btn = new Button();              
                btn.Content = new TextBlock(new Run("Set window FontSize to " + fntsizes[i]));  // текстовый блок - содержимое кнопки              
                btn.Tag = fntsizes[i]; // информация о размере шрифта привязанная к кнопке            
                btn.HorizontalAlignment =  HorizontalAlignment.Center;      // Центрирование по вертикали и горизонтали
                btn.VerticalAlignment =  VerticalAlignment.Center;      
                btn.Click += WindowFontSizeOnClick;            // Перехват клика по кнопке
                grid.Children.Add(btn);              // Добавление кнопки в сетку
                Grid.SetRow(btn, 0);         
                Grid.SetColumn(btn, i);     
                btn = new Button();         
                btn.Content = new TextBlock(new Run("Set button FontSize  to " + fntsizes[i]));  
                btn.Tag = fntsizes[i];        
                btn.HorizontalAlignment =  HorizontalAlignment.Center;      
                btn.VerticalAlignment =  VerticalAlignment.Center;
                btn.Click += ButtonFontSizeOnClick;  // Перехват клика по кнопке           
                grid.Children.Add(btn);            
                Grid.SetRow(btn, 1);         
                Grid.SetColumn(btn, i);       
            }       
        }    
        void WindowFontSizeOnClick(object sender,  RoutedEventArgs args)   
        {            
            Button btn = args.Source as Button; // Определение источника сообщения   
            FontSize = (double)btn.Tag;       // Изменение размера шрифта всего окна на содержащийся в Tag кнопки
        }         
        void ButtonFontSizeOnClick(object sender,  RoutedEventArgs args)     
        {           
            Button btn = args.Source as Button;    
            btn.FontSize = (double)btn.Tag;    // Изменение раззмера шрифта кнопки на содержащийся в Tag
        }     
    } 
} 