using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
namespace Petzold.ListColorValues 
{ 
    class ListColorValues : Window 
    {
        [STAThread] 
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorValues()); 
        } 
        public ListColorValues() 
        { 
            Title = "List Color Values";   
            // Create ListBox as content of window.    
            ListBox lstbox = new ListBox(); // Создание нового листа    
            lstbox.Width = 150;      
            lstbox.Height = 150;        
            lstbox.SelectionChanged +=  ListBoxOnSelectionChanged;  // Перехват события, когда меняется выбранный элемент листа
            Content = lstbox;       
            // Fill ListBox with Color values.   
            PropertyInfo[] props = typeof(Colors).GetProperties(); // Получение всех свойств всех цветов
            foreach (PropertyInfo prop in props) // Внесение свойств в лист        
                lstbox.Items.Add(prop.GetValue (null, null));   
        }       
        void ListBoxOnSelectionChanged(object sender,SelectionChangedEventArgs args)  
        {         
            ListBox lstbox = sender as ListBox;        
            if (lstbox.SelectedIndex != -1)         
            {           
                Color clr = (Color)lstbox .SelectedItem; // Получение цвета, выбранного в списке 
                Background = new SolidColorBrush(clr);    // Применене полученного цвета к заднему фону окна
            }      
        }  
    } 
} 