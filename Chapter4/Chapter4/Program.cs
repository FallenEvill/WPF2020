using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Petzold.EditSomeText 
{     
    class EditSomeText : Window     
    {         
        static string strFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder .LocalApplicationData),"Petzold\\EditSomeText\\EditSomeText.txt"); // Путь к текстовому файлу и его наименивание
        TextBox txtbox;
        [STAThread]         
        public static void Main()         
        {             
            Application app = new Application();
            app.Run(new EditSomeText());
        }         
        public EditSomeText()         
        {             
            Title = "Edit Some Text";             // Create the text box.
            txtbox = new TextBox(); // Создание текстового блока             
            txtbox.AcceptsReturn = true; // Использование клавиши Enter для переноса строки
            txtbox.TextWrapping = TextWrapping.Wrap; // Перенос строки при достижении края окна
            txtbox.VerticalScrollBarVisibility =  ScrollBarVisibility.Auto; // Появление полосы прокрутки по вертикали в случае необходимости 
            txtbox.KeyDown += TextBoxOnKeyDown; // Перехват нажатия кнопки
            Content = txtbox;             
            try  // Пытаемся загрузить текстовый файл           
            {                 
                txtbox.Text = File.ReadAllText (strFileName); // Чтение файла
            }             
            catch             
            {             
            
            }                         
            txtbox.CaretIndex = txtbox.Text.Length; // Постановка курсора в конце уже записанного в файле текста             
            txtbox.Focus();  // Установка фокуса       
        }         
        protected override void OnClosing (CancelEventArgs args) // Перехват закрытия приложения       
        {             
            try // Попытка сохранить файл             
            {                
                Directory.CreateDirectory(Path .GetDirectoryName(strFileName)); // Создание каталогов на пути к файлу, если их не существует
                File.WriteAllText(strFileName,  txtbox.Text); // Запись текста из текстового блока в файл
            }             
            catch (Exception exc) // В случае провала попытки сохранения           
            {                 
                MessageBoxResult result = MessageBox.Show("File could  not be saved: " + //Вывод сообщения об ошибке
                    exc.Message +  "\nClose  program anyway?", Title,MessageBoxButton.YesNo,MessageBoxImage.Exclamation); // Вывод диалога с выбором закрыть программу или нет 
                args.Cancel = (result ==  MessageBoxResult.No); // Применение решения диалога
            }         
        }         
        void TextBoxOnKeyDown(object sender,  KeyEventArgs args) // Функция перехвата нажатия клавиши        
        {             
            if (args.Key == Key.F5)   // Если нажата клавиша F5          
            {                 
                txtbox.SelectedText = DateTime.Now .ToString(); // Ввод текущих даты и времину в тексь
                txtbox.CaretIndex = txtbox .SelectionStart + txtbox.SelectionLength; // Перенос курсора на конец выведенной даты, времени
            }         
        }     
    } 
} 