using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameInterface.CustomControls
{
    //[ContentProperty("Content")]
    class Map : FrameworkElement
    {
        public static readonly DependencyProperty MapImagePathProperty =
            DependencyProperty.Register(
            "MapImagePath", typeof(string),
            typeof(Map)
            );
        public string MapImagePath
        {
            get => (string)GetValue(MapImagePathProperty); 
            set => SetValue(MapImagePathProperty, value); 
        }

        private ImageSource mapImageSource;

        public Map()
        {
            mapImageSource = new BitmapImage(new Uri(@"D:\ImmaCodder\Kursach\MonopolyPreUnity\GameInterface\Images\map.png"));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Debug.WriteLine($"RenderWidth:{RenderSize.Width}; RenderHeight:{RenderSize.Height}");
            drawingContext.DrawImage(mapImageSource, new Rect(RenderSize));
        }
    }
}
