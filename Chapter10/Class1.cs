﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Petzold.GetMedieval
{
    public class MedievalButton : Control
    {         
        // Just two private fields.
        FormattedText formtxt;   
        bool isMouseReallyOver;   
        // Static readonly fields. 
        public static readonly DependencyProperty  TextProperty;  
        public static readonly RoutedEvent KnockEvent;
        public static readonly RoutedEvent  PreviewKnockEvent; 
        // Static constructor.    
        static MedievalButton()      
        {       
            // Register dependency property.  
            TextProperty = DependencyProperty.Register("Text",  typeof(string), typeof (MedievalButton),new FrameworkPropertyMetadata(" ",FrameworkPropertyMetadataOptions.AffectsMeasure));  
            // Register routed events.  
            KnockEvent = EventManager.RegisterRoutedEvent ("Knock", RoutingStrategy.Bubble,typeof(RoutedEventHandler),  typeof(MedievalButton)); 
            PreviewKnockEvent = EventManager.RegisterRoutedEvent ("PreviewKnock",RoutingStrategy.Tunnel,typeof(RoutedEventHandler),  typeof(MedievalButton));
        }     // Public interface to dependency property. 
        public string Text   
        {       
            set 
            { 
                SetValue(TextProperty, value == null  ? " " : value);
            }         
            get
            { return (string)GetValue(TextProperty); 
            }     
        }    
        // Public interface to routed events.  
        public event RoutedEventHandler Knock   
        {        
            add { AddHandler(KnockEvent, value); 
            }        
            remove 
            { 
                RemoveHandler(KnockEvent, value); 
            }    
        }     
        public event RoutedEventHandler PreviewKnock     
        {        
            add 
            { 
                AddHandler(PreviewKnockEvent, value);
            }         
            remove 
            { 
                RemoveHandler(PreviewKnockEvent,  value);
            }     
        }     // MeasureOverride called whenever the size of  the button might change. 
        protected override Size MeasureOverride(Size  sizeAvailable)  
        {       
            formtxt = new FormattedText(Text, CultureInfo.CurrentCulture,  FlowDirection,new Typeface(FontFamily, FontStyle , FontWeight, FontStretch),FontSize, Foreground);   
            // Take account of Padding when  calculating the size.    
            Size sizeDesired = new Size(Math.Max(48,  formtxt.Width) + 4,formtxt.Height + 4);    
            sizeDesired.Width += Padding.Left +  Padding.Right;      
            sizeDesired.Height += Padding.Top +  Padding.Bottom;   
            return sizeDesired; 
        }     // OnRender called to redraw the button.  
        protected override void OnRender (DrawingContext dc) 
        {       
            // Determine background color. 
            Brush brushBackground = SystemColors.ControlBrush; // Задний фон кнопки  
            if (isMouseReallyOver && IsMouseCaptured)       
                brushBackground = SystemColors.ControlDarkBrush;     // Задний фон кнопки, если курсор находится над ней
            // Determine pen width.          
            Pen pen = new Pen(Foreground,  IsMouseOver ? 2 : 1);  
            // Draw filled rounded rectangle.     
            dc.DrawRoundedRectangle (brushBackground, pen,new Rect(new  Point(0, 0), RenderSize), 4, 4); // Рисование прямоугольника со скруглёнными углами  
            // Determine foreground color.      
            formtxt.SetForegroundBrush(IsEnabled ? Foreground :  SystemColors.ControlDarkBrush);  
            // Determine start point of text.            
            Point ptText = new Point(2, 2);       // Точка начала текста в кнопке     
            switch (HorizontalContentAlignment) // Обработка различных вариантов расположения начала текста по горизонтали       
            {               
                case HorizontalAlignment.Left:       
                    ptText.X += Padding.Left;   
                    break;              
                case HorizontalAlignment.Right: 
                    ptText.X += RenderSize.Width -  formtxt.Width - Padding.Right;  
                    break;                
                case HorizontalAlignment.Center: 
                case HorizontalAlignment.Stretch:  
                    ptText.X += (RenderSize.Width  - formtxt.Width - Padding.Left - Padding .Right) / 2; 
                    break;            
            }            
            switch (VerticalContentAlignment)  // Обработка различных вариантов расположения начала текста по вертикали      
            {               
                case VerticalAlignment.Top:                  
                    ptText.Y += Padding.Top;   
                    break;               
                case VerticalAlignment.Bottom:   
                    ptText.Y += RenderSize.Height -  formtxt.Height - Padding.Bottom;   
                    break;      
                case VerticalAlignment.Center:   
                case VerticalAlignment.Stretch: 
                    ptText.Y += (RenderSize.Height  - formtxt.Height - Padding.Top - Padding .Bottom) / 2;   
                    break;            
            }             
            // Draw the text.         
            dc.DrawText(formtxt, ptText);    // Запись текста
        }        
        // Mouse events that affect the visual  look of the button.   
        protected override void OnMouseEnter (MouseEventArgs args) // Когда курсор входит в границы кнопки 
        {           
            base.OnMouseEnter(args);     
            InvalidateVisual();       
        }         
        protected override void OnMouseLeave (MouseEventArgs args)  // Когда курсор выходит за границы кнопки   
        {            
            base.OnMouseLeave(args);
            InvalidateVisual();       
        }      
        protected override void OnMouseMove (MouseEventArgs args) // // Когда курсор движется 
        {             
            base.OnMouseMove(args);       
            // Determine if mouse has really moved  inside or out.
            Point pt = args.GetPosition(this);   
            bool isReallyOverNow = (pt.X >= 0 &&  pt.X < ActualWidth &&  pt.Y >= 0 &&  pt.Y < ActualHeight);
            if (isReallyOverNow != isMouseReallyOver)    // Проверка того, располагается ли курсор над кнопкой или нет  
            {         
                isMouseReallyOver = isReallyOverNow;    
                InvalidateVisual();         
            }     
        }        
        // This is the start of how 'Knock' events  are triggered.   
        protected override void  OnMouseLeftButtonDown(MouseButtonEventArgs args) // При нажатии левой кнопки мыши     
        {             
            base.OnMouseLeftButtonDown(args);   
            CaptureMouse();    // Захват курсора
            InvalidateVisual();  
            args.Handled = true;  
        }         // This event actually triggers the  'Knock' event.
        protected override void  OnMouseLeftButtonUp(MouseButtonEventArgs args) // При отпускании левой кнопки мыши
        {           
            base.OnMouseLeftButtonUp(args);       
            if (IsMouseCaptured)         // Проверка захвата
            {            
                if (isMouseReallyOver)       // Проверка, находится ли курсор над кнопкой
                {              
                    OnPreviewKnock();   
                    OnKnock();              
                }              
                args.Handled = true;  
                Mouse.Capture(null);  
            }      
        }        
        // If lose mouse capture (either  internally or externally), redraw.
        protected override void OnLostMouseCapture (MouseEventArgs args) // При потере захвата  
        {         
            base.OnLostMouseCapture(args);      
            InvalidateVisual();       
        }        
        // Кнопки space и enter также тригерят кнопку  
        protected override void OnKeyDown (KeyEventArgs args)      
        {         
            base.OnKeyDown(args);    
            if (args.Key == Key.Space || args.Key  == Key.Enter) args.Handled = true; 
        }         
        protected override void OnKeyUp (KeyEventArgs args)       
        {           
            base.OnKeyUp(args);    
            if (args.Key == Key.Space || args.Key  == Key.Enter)            
            {            
                OnPreviewKnock();   
                OnKnock();      
                args.Handled = true;    
            }     
        }         // OnKnock method raises the 'Knock' event.
        protected virtual void OnKnock()      
        {           
            RoutedEventArgs argsEvent = new  RoutedEventArgs();     
            argsEvent.RoutedEvent = MedievalButton.PreviewKnockEvent; 
            argsEvent.Source = this;         
            RaiseEvent(argsEvent);   
        }         // OnPreviewKnock method raises the  'PreviewKnock' event.  
        protected virtual void OnPreviewKnock()         
        {             RoutedEventArgs argsEvent = new  RoutedEventArgs();  
            argsEvent.RoutedEvent = MedievalButton.KnockEvent;    
            argsEvent.Source = this;        
            RaiseEvent(argsEvent);    
        }    
    } 
} 


