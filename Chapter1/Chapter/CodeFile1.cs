//----------------------------------------- // SayHello.cs (c) 2006 by Charles Petzold //----------------------------------------- 
using System; 
using System.Windows; 
namespace Petzold.SayHello 
{     
    class SayHello     // Название класса //
    {         
        [STAThread]         
        public static void Main()         
        {             
            Window win = new Window(); // Создание нового окна //            
            win.Title = "Say Hello";     // Заголовок окна //        
            win.Show(); // Открытие окна //      
            Application app = new Application();     // Создание приложения // 
            app.Run(); // Запуск приложения // 
        }     
    } 
} 