using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
class ColorScroll : Window
{
    ScrollBar[] scrolls = new ScrollBar[3]; // Массив полос прокрутки
    TextBlock[] txtValue = new TextBlock[3]; // Массив текстовых блоков
    Panel pnlColor;
    [STAThread] 
    public static void Main() 
    { 
        Application app = new Application();
        app.Run(new ColorScroll()); // Запуск приложения
    }
    public ColorScroll()
    {
        Title = "Color Scroll";
        Width = 500;
        Height = 300;         
        // GridMain contains a vertical splitter.         
        Grid gridMain = new Grid(); // Создание новой сетки
        Content = gridMain;  
        // GridMain column definitions.   
        ColumnDefinition coldef = new  ColumnDefinition();  // Определение столбца
        coldef.Width = new GridLength(200,  GridUnitType.Pixel); // Ширина столбца
        gridMain.ColumnDefinitions.Add(coldef);    // Определение первого столбца
        coldef = new ColumnDefinition();
        coldef.Width = GridLength.Auto; // Ширина по содержимому столбца
        gridMain.ColumnDefinitions.Add(coldef);
        coldef = new ColumnDefinition();   
        coldef.Width = new GridLength(200,  GridUnitType.Star);
        gridMain.ColumnDefinitions.Add(coldef);
        // Vertical splitter.      
        GridSplitter split = new GridSplitter(); // Разделитель полос прокрутки и панели с цветом 
        split.HorizontalAlignment =  HorizontalAlignment.Center;   
        split.VerticalAlignment =  VerticalAlignment.Stretch;  // Заполнение всей высоты доступной области  
        split.Width = 6; // Ширина разделителя   
        gridMain.Children.Add(split); //Добавление разделителя в сетку   
        Grid.SetRow(split, 0); // Первый ряд   
        Grid.SetColumn(split, 1); // Второй столбец
        // Panel on right side of splitter to  display color. 
        pnlColor = new StackPanel();  // Новая панель  
        pnlColor.Background = new SolidColorBrush (SystemColors.WindowColor);  // Задний фон панели
        gridMain.Children.Add(pnlColor); // Добавление панели в сетку
        Grid.SetRow(pnlColor, 0);  // Первый ряд
        Grid.SetColumn(pnlColor, 2); // Третий столбец
        // Secondary grid at left of splitter. 
        Grid grid = new Grid();   // Создание еще одной сетки
        gridMain.Children.Add(grid); // Добавление второй сетки в главную
        Grid.SetRow(grid, 0);  
        Grid.SetColumn(grid, 0); 
        // Three rows for label, scroll, and label.  
        RowDefinition rowdef = new RowDefinition(); // Определение строки      
        rowdef.Height = GridLength.Auto;     
        grid.RowDefinitions.Add(rowdef); // Определение первой строки второй сетки   
        rowdef = new RowDefinition();     
        rowdef.Height = new GridLength(100,  GridUnitType.Star);  
        grid.RowDefinitions.Add(rowdef);    
        rowdef = new RowDefinition();        
        rowdef.Height = GridLength.Auto; 
        grid.RowDefinitions.Add(rowdef);
        // Three columns for Red, Green, and Blue.     
        for (int i = 0; i < 3; i++)   // Создание 3 столбцов
        {           
            coldef = new ColumnDefinition();    
            coldef.Width = new GridLength(33,  GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);  
        }        
        for (int i = 0; i < 3; i++) // Создание 3 полос прокрутки
        {          
            Label lbl = new Label(); // Подпись
            lbl.Content = new string[] { "Red",  "Green", "Blue" }[i]; // Содержимое подписи
            lbl.HorizontalAlignment =  HorizontalAlignment.Center;    // Выравнивание по центру
            grid.Children.Add(lbl); // добавление подписи к второй сетке
            Grid.SetRow(lbl, 0); 
            Grid.SetColumn(lbl, i);  // i столбец
            scrolls[i] = new ScrollBar(); // Новая полоса прокрутки
            scrolls[i].Focusable = true; 
            scrolls[i].Orientation = Orientation.Vertical;   //Вертикальная ориентация
            scrolls[i].Minimum = 0;  
            scrolls[i].Maximum = 255; 
            scrolls[i].SmallChange = 1;   
            scrolls[i].LargeChange = 16; 
            scrolls[i].ValueChanged +=  ScrollOnValueChanged;  // Перехват изменения положения бегунка
            grid.Children.Add(scrolls[i]); 
            Grid.SetRow(scrolls[i], 1);    
            Grid.SetColumn(scrolls[i], i);   
            txtValue[i] = new TextBlock();   
            txtValue[i].TextAlignment =  TextAlignment.Center;    
            txtValue[i].HorizontalAlignment =  HorizontalAlignment.Center;   
            txtValue[i].Margin = new Thickness(5); 
            grid.Children.Add(txtValue[i]);      
            Grid.SetRow(txtValue[i], 2);   
            Grid.SetColumn(txtValue[i], i);  
        }         
        // Initialize scroll bars.       
        Color clr = (pnlColor.Background as  SolidColorBrush).Color; // Цвет заднего фона панели   
        scrolls[0].Value = clr.R; // Задание цветов полосам прокрутки     
        scrolls[1].Value = clr.G;   
        scrolls[2].Value = clr.B;      
        // Set initial focus.       
        scrolls[0].Focus(); // Изначальный фокус на первой полосе прокрутки     
    }
    void ScrollOnValueChanged(object sender,  RoutedEventArgs args) // Функция перехвата изменения бегунка полосы прокрутки 
    {        
        ScrollBar scroll = sender as ScrollBar;   
        Panel pnl = scroll.Parent as Panel;    
        TextBlock txt = pnl.Children[1 + pnl.Children .IndexOf(scroll)] as TextBlock; // Выбор текстового блока под полосой прокрутки с изменённым положением бегунка
        txt.Text = String.Format("{0}\n0x{0:X2}",  (int)scroll.Value);  // Изменение текста
        pnlColor.Background = new SolidColorBrush(Color.FromRgb((byte) scrolls[0].Value,(byte) scrolls[1] .Value,(byte) scrolls[2].Value)); // Изменение цвета заднего фона панели на основе изменения положения бегунков
    }
}
