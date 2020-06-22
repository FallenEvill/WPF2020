//-----------------------------------------------
// MoveTheToolbar.cs (c) 2006 by Charles Petzold
//-----------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.MoveTheToolbar
{
    public class MoveTheToolbar : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MoveTheToolbar());
        }
        public MoveTheToolbar()
        {
            Title = "Move the Toolbar";

            // Create DockPanel as content of window.
            DockPanel dock = new DockPanel(); // Новая док-панель, содержимое окна
            Content = dock;

            // Create ToolBarTray at top and left of window.
            ToolBarTray trayTop = new ToolBarTray(); // Новая панель инструментов
            dock.Children.Add(trayTop); // Присоединение панели инструментов к док панели, вверх окна
            DockPanel.SetDock(trayTop, Dock.Top);

            ToolBarTray trayLeft = new ToolBarTray(); 
            trayLeft.Orientation = Orientation.Vertical;
            dock.Children.Add(trayLeft); // Присоединение панели инструментов к док панели, влево окна 
            DockPanel.SetDock(trayLeft, Dock.Left);

            // Create TextBox to fill rest of client area.
            TextBox txtbox = new TextBox(); // Создание текстового блока для заполнения оставшегося пространства
            dock.Children.Add(txtbox);

            // Create six Toolbars...
            for (int i = 0; i < 6; i++) // Создание 6 элементов панели инструментов
            {
                ToolBar toolbar = new ToolBar();
                toolbar.Header = "Toolbar " + (i + 1);

                if (i < 3)
                    trayTop.ToolBars.Add(toolbar); // Добавление элемента в верхнюю панель
                else
                    trayLeft.ToolBars.Add(toolbar);

                // ... with six buttons each.
                for (int j = 0; j < 6; j++) // Создание кнопок для элементов панели инструментов
                {
                    Button btn = new Button();
                    btn.FontSize = 16;
                    btn.Content = (char)('A' + j); // Содержимое кнопки
                    toolbar.Items.Add(btn);
                }
            }
        }
    }
}
