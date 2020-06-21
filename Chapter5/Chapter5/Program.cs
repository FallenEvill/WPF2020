
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Petzold.TuneTheRadio 
{     
    public class TuneTheRadio : Window     
    {         
        [STAThread]         
        public static void Main()    
        {            
            Application app = new Application();    
            app.Run(new TuneTheRadio());    
        }     
        public TuneTheRadio()       
        {           
            Title = "Tune the Radio";  
            SizeToContent = SizeToContent.WidthAndHeight;     // Установка ширины и высоты окна по ширине и высоте контента
            GroupBox group = new GroupBox();  // Создание группы, элементами которой являются взаимоисключающие кнопки. Имеет отдельный заголовок и рамку 
            group.Header = "Window Style"; // Название группы     
            group.Margin = new Thickness(96); // Отступ рамки от границ окна        
            group.Padding = new Thickness(5); // Отступ кнопок от рамки        
            Content = group;        
            StackPanel stack = new StackPanel(); // Рассположение элементов по вертикали
            group.Content = stack;     
            stack.Children.Add(CreateRadioButton("No border or  caption",WindowStyle.None));  //Создание кнопок, составляющих переключатель
            stack.Children.Add(CreateRadioButton("Single-border  window",WindowStyle .SingleBorderWindow));      
            stack.Children.Add(CreateRadioButton("3D-border window",WindowStyle .ThreeDBorderWindow));       
            stack.Children.Add(CreateRadioButton("Tool window",WindowStyle .ToolWindow));    
            AddHandler(RadioButton.CheckedEvent,new RoutedEventHandler (RadioOnChecked)); //Обработчик события, нажатия на переключатель
        }        
        RadioButton CreateRadioButton(string  strText, WindowStyle winstyle) //Функция создания переключателя  
        {           
            RadioButton radio = new RadioButton(); // Создание кнопки     
            radio.Content = strText;   // Название кнопки
            radio.Tag = winstyle;   // Содержимое кнопки
            radio.Margin = new Thickness(5);  // Отступ от других кнопок
            radio.IsChecked = (winstyle ==  WindowStyle); // Проверка выбрана ли эта кнопка    
            return radio;   
        }        
        void RadioOnChecked(object sender,  RoutedEventArgs args)  // Событие когда выбирается одна из кнопок переключателя  
        {        
            RadioButton radio = args.Source as  RadioButton;    
            WindowStyle = (WindowStyle)radio.Tag;        // Изменение стиля окна на содержащийся в кнопке переключателя
        }    
    } 
} 


