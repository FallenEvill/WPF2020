
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Petzold.ExamineKeystrokes 
{ 
    class ExamineKeystrokes : Window 
    { 
        StackPanel stack;
        ScrollViewer scroll;
        string strHeader = "Event Key Sys-Key Text" + "Ctrl-Text Sys-Text Ime KeyStates " + "IsDown  IsUp    IsToggled IsRepeat ";
            string strFormatKey = "{0,-10}{1,-20}{2 ,-10} " + " {3,-10}{4,-15 }{5,-8}{6,-7}{7,-10}{8,-10}";
        string strFormatText = "{0,-10}" + "{1,-10}{2,-10}{3 ,-10}";
        [STAThread] 
        public static void Main() 
        { 
            Application app = new Application();
            app.Run(new ExamineKeystrokes());
        } 
        public ExamineKeystrokes()
        { 
            Title = "Examine Keystrokes";
            FontFamily = new FontFamily("Courier  New"); // Выбор шрифта
            Grid grid = new Grid(); 
            Content = grid;         
            RowDefinition rowdef = new  RowDefinition();       
            rowdef.Height = GridLength.Auto;        
            grid.RowDefinitions.Add(rowdef);           
            grid.RowDefinitions.Add(new  RowDefinition()); // ряд, заполняющий всё оставшееся пространство 
            // Display header text.      
            TextBlock textHeader = new TextBlock();   
            textHeader.FontWeight = FontWeights.Bold;   // Жирный шрифт
            textHeader.Text = strHeader;         
            grid.Children.Add(textHeader);       
            // Create StackPanel as child of  ScrollViewer for displaying events. 
            scroll = new ScrollViewer(); // Прокрутка содержимого      
            grid.Children.Add(scroll);       
            Grid.SetRow(scroll, 1);       
            stack = new StackPanel();    
            scroll.Content = stack;      // StackPanel с прокруткой
        }       
        protected override void OnKeyDown (KeyEventArgs args) // Перехват нажатия клавишы  
        {        
            base.OnKeyDown(args);      
            DisplayKeyInfo(args);   
        }    
        protected override void OnKeyUp (KeyEventArgs args)    // Перехват отпускания клавиши
        {            
            base.OnKeyUp(args);
            DisplayKeyInfo(args);     
        }    
        protected override void OnTextInput (TextCompositionEventArgs args) // Перехват случая, когда нажата клавиша имеющая другое значение при нажатии shift или caps lock
        {        
            base.OnTextInput(args);     
            string str = String.Format(strFormatText, args .RoutedEvent.Name, args.Text,args .ControlText, args.SystemText); // Запись в строку второго значения клавиши
            DisplayInfo(str);  // Функция отображения строки на экране  
        }      
        void DisplayKeyInfo(KeyEventArgs args)    
        {          
            string str = String.Format(strFormatKey, args .RoutedEvent.Name, args.Key,args.SystemKey, args .ImeProcessedKey, args.KeyStates, args.IsDown, args.IsUp, args .IsToggled, args.IsRepeat); // Запись в строку значения клавиши и аргументов(Нажата/отпущена) и т.д.
            DisplayInfo(str);      
        }      
        void DisplayInfo(string str)    
        {        
            TextBlock text = new TextBlock();   
            text.Text = str;           
            stack.Children.Add(text);     
            scroll.ScrollToBottom(); // Прокрутка по вертикали до конца содержимого    
        }  
    }
} 