using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media; // Обеспечивает интеграцию мультимедийных данных
using System.Windows.Media.Imaging; //Обеспечивает интеграцию изображений
namespace Petzold.ShowMyFace 
{ class ShowMyFace : Window // Наследование от класса Окно
    {
        [STAThread] 
        public static void Main() 
        { 
            Application app = new Application(); // Создание нового приложения
            app.Run(new ShowMyFace());
        } // Запуск цикла обработки сообщений приложения в потоке
        public ShowMyFace() 
        { 
            Title = "Show My Face"; // Заголовок окна
            Uri uri = new Uri("http://www.charlespetzold.com/PetzoldTattoo.jpg"); // Определение расположения изображения
            BitmapImage bitmap = new BitmapImage(uri); // Выгрузка изображения в память 
            Image img = new Image(); // Показ изображения в окне
            img.Source = bitmap; // Источник изображения
            Content = img; 
        } 
    }
}