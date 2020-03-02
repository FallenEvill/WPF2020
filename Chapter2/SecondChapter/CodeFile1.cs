//------------------------------------------------- // GradiateTheBrush.cs (c) 2006 by Charles Petzold //------------------------------------------------- 
using System; 
using System.Windows; 
using System.Windows.Input; 
using System.Windows.Media; 
namespace Petzold.GradiateTheBrush 
{     
    public class GradiateTheBrush : Window // Наследование от окошка //    
    {         [STAThread]         
        public static void Main()         
        {             
            Application app = new Application();   // Создание приложния //          
            app.Run(new GradiateTheBrush());    // Запуск приложеня,  //      
        }         
        public GradiateTheBrush() // Создание окна приложения(экземляра функции) //        
        {             
            Title = "Gradiate the Brush"; // Название окна //              
            LinearGradientBrush brush = new LinearGradientBrush(Colors.Green , Colors.Blue, new Point (0, 0), new Point(5, 5)); // Создание "кисти", которая меняет цвет от первого ко второму с линейной скоростью) //            
            Background = brush; // Закрашивание фона с помощью созданной "кисти" //       
        }     
    } 
} 