using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Petzold.PaintTheButton
{
    public class PaintTheButton : Window
    {
        [STAThread] 
        public static void Main() 
        { 
            Application app = new Application();
            app.Run(new PaintTheButton());
        }
        public PaintTheButton()
        {
            Title = "Paint the Button";  
            // Create the Button as content of the  window.
            Button btn = new Button(); // Новая кнопка    
            btn.HorizontalAlignment =  HorizontalAlignment.Center;  // Выравнивание по центру по горизонтали    
            btn.VerticalAlignment =  VerticalAlignment.Center;     // Выравнивание по центру по вертукали
            Content = btn;         
            // Create the Canvas as content of the  button.      
            Canvas canv = new Canvas(); // новый холст       
            canv.Width = 144; // Ширина, высота холста   
            canv.Height = 144;        
            btn.Content = canv; // Холст это содержимое кнопки          
            // Create Rectangle as child of canvas.   
            Rectangle rect = new Rectangle(); // Новый прямоугольник
            rect.Width = canv.Width;     
            rect.Height = canv.Height;        
            rect.RadiusX = 24; //Скругление углов прямоугольника        
            rect.RadiusY = 24;         
            rect.Fill = Brushes.Blue;        
            canv.Children.Add(rect); // Присоединение прямоугольника к холсту         
            Canvas.SetLeft(rect, 0);          
            Canvas.SetRight(rect, 0);        
            // Create Polygon as child of canvas.       
            Polygon poly = new Polygon(); // Новый многоугольник     
            poly.Fill = Brushes.Yellow;       
            poly.Points = new PointCollection(); // Множество точек многоугольника    
            for (int i = 0; i < 5; i++)  // Всего 5 точек    
            {                
                double angle = i * 4 * Math.PI / 5;          
                Point pt = new Point(48 * Math.Sin (angle), -48 * Math.Cos (angle)); // Расположение точки                
                poly.Points.Add(pt);          
            }             
            canv.Children.Add(poly);  // Присоединение многоугольника к холсту
            Canvas.SetLeft(poly, canv.Width / 2); // Отступ от границ холста
            Canvas.SetTop(poly, canv.Height / 2);     
        }    
    }
} 
