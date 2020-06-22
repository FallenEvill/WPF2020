using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Petzold.CreateFullColorBitmap
{
    public class CreateFullColorBitmap : Window
    {
        [STAThread] 
        public static void Main() 
        { 
            Application app = new Application();
            app.Run(new CreateFullColorBitmap()); 
        }
        public CreateFullColorBitmap()
        {
            Title = "Create Full-Color Bitmap";  
              
            int[] array = new int[256 * 256];   // Массив битов для bitmap
            for (int x = 0; x < 256; x++)   // Заполнение массива 
                for (int y = 0; y < 256; y++)     
                {               
                    int b = x;      
                    int g = 0;          
                    int r = y;          
                    array[256 * y + x] = b | (g << 8)  | (r << 16); 
                }                  
            BitmapSource bitmap = BitmapSource.Create(256, 256, 96,  96, PixelFormats.Bgr32,null, array,  256 * 4); // Создание bitmap, на основе масива 
            Image img = new Image(); // Новое изображение        
            img.Source = bitmap; // Растровое изображение     
            // Make the Image object the content  of the window.     
            Content = img;  // изображение - содержимое окна  
        }    
    } 
} 
