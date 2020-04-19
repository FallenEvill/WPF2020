//------------------------------------------------- // LoadEmbeddedXaml.cs (c) 2006 by Charles Petzold //------------------------------------------------- 
using System; 
using System.IO; 
using System.Windows; 
using System.Windows.Controls; 
using System.Windows.Markup; 
using System.Xml; 
namespace Petzold.LoadEmbeddedXaml 
{     
    public class LoadEmbeddedXaml : Window     
    {         
        [STAThread]         
        public static void Main()        
        {             
            Application app = new Application();    // Создание приложения //          
            app.Run(new LoadEmbeddedXaml());      // Запуск приложения //   
        }         
        public LoadEmbeddedXaml()         
        {             
            Title = "Load Embedded Xaml";     // Заголовок окна //       
            string strXaml =    //   Создание XAML документа в виде строки //
                "<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'"+ // Адрес кнопки //                 
                "         Foreground='LightSeaGreen' FontSize='24pt'>" +                 // Фон и размер шрифта кнопки //
                "    Click me!" +                 // Текст кнопки //
                "</Button>";            
            StringReader strreader = new  StringReader(strXaml); // Считывание данных строки //
            XmlTextReader xmlreader = new  XmlTextReader(strreader); // Считывание XAML данных //             
            object obj = XamlReader.Load(xmlreader);             // Создание объекта содержащего XAML данные //
            Content = obj;         // Содержимым окна становиться объект созданный строкой ранее //
        }     
    } 
} 
