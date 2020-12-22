using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    class Map : FrameworkElement, INotifyPropertyChanged
    {
        #region properties
        public static readonly DependencyProperty ClickedTileIdProperty =
            DependencyProperty.Register(
            "ClickedTileId",
            typeof(int),
            typeof(Map),
            new PropertyMetadata(OnPointClickedChangedCallBack));

        public int ClickedTileId
        {
            get => (int)GetValue(ClickedTileIdProperty);
            set
            {
                SetValue(ClickedTileIdProperty, value);
                RaisePropertyChanged(nameof(ClickedTileId));
            }
        }

        private readonly DependencyProperty PointClickedProperty =
            DependencyProperty.Register(
            "PointClicked", 
            typeof(Point),
            typeof(Map),
            new PropertyMetadata(OnPointClickedChangedCallBack)
            );
        public Point PointClicked
        {
            get => (Point)GetValue(PointClickedProperty); 
            set 
            {
                SetValue(PointClickedProperty, value);
                Debug.WriteLine("the property was changed");
                RaisePropertyChanged(nameof(PointClicked));
            }
        }

        private ImageSource mapImageSource;
        #endregion

        #region propertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static void OnPointClickedChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Map c = sender as Map;
            if (c != null)
                c.OnPointClickedChanged();
        }

        protected virtual void OnPointClickedChanged()
        {
            //Debug.WriteLine("OnPointClickedChanged");
            RaisePropertyChanged(nameof(PointClicked));
            RaisePropertyChanged(nameof(ClickedTileId));
        }
        #endregion

        #region field
        double imageSide = 6800;
        Size tile = new Size(832, 575);
        Size bigTile = new Size(832, 832);

        Size actualTileSize => new Size(RenderSize.Height * tile.Height / imageSide,
            RenderSize.Height * tile.Width / imageSide);
        Size actualBigTileSize => new Size(RenderSize.Height * bigTile.Height / imageSide,
            RenderSize.Height * bigTile.Width / imageSide);

        double tileWidth => ActualWidth - 2 * actualBigTileSize.Width;
        #endregion

        public Map()
        {
            mapImageSource = new BitmapImage(new Uri(@"D:\ImmaCodder\Kursach\MonopolyPreUnity\GameInterface\Images\map.png"));
            PointClicked = new Point();
            ClickedTileId = -1;
            MouseLeftButtonUp += Map_MouseLeftButtonUp;
        }

        #region click mathematics
        private void Map_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PointClicked = e.GetPosition((UIElement)sender);
            ClickedTileId = GetClickedTileInfo(PointClicked);
            //Debug.WriteLine($"Well duh, I tried, {PointClicked}");
        }

        enum Position
        {
            top, bottom,
            left, middle, right
        }
        private int GetClickedTileInfo(Point point)
        {
            Position vert;
            Position hor;
            int botRight = 0;
            int botLeft = 10;
            int topLeft = 20;
            int topRight = 30;

            // vertical position
            if (point.Y <= actualTileSize.Height)
                vert = Position.top;
            else if (point.Y <= ActualHeight - actualTileSize.Height)
                vert = Position.middle;
            else
                vert = Position.bottom;

            // horizontal position
            if (point.X <= actualTileSize.Height)
                hor = Position.left;
            else if (point.X <= ActualWidth - actualTileSize.Height)
                hor = Position.middle;
            else
                hor = Position.right;

            if (vert == Position.top)
            {
                if (hor == Position.middle)
                {
                    // count the tile
                    var offset = (int)((point.X - actualBigTileSize.Width) / actualTileSize.Width);
                    return topLeft + offset + 1;
                }
                else if (hor == Position.left)
                    return topLeft;
                else
                    return topRight;
            }
            else if (vert == Position.bottom)
            {
                if (hor == Position.middle)
                {
                    // count the tile
                    var offset = 8 - (int)((point.X - actualBigTileSize.Width) / actualTileSize.Width);
                    return botRight + offset + 1;
                }
                else if (hor == Position.left)
                    return botLeft;
                else
                    return botRight;
            }
            else
            {
                if (hor == Position.left)
                {
                    // count the tile
                    var offset = 8 - (int)((point.Y - actualBigTileSize.Height) / actualTileSize.Width);
                    return botLeft + offset + 1;
                }
                else if (hor == Position.right)
                {
                    // count the tile
                    var offset = (int)((point.Y - actualBigTileSize.Height) / actualTileSize.Width);
                    return topRight + offset + 1;
                }
                else
                    return -1;
            }
        }
        #endregion

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            //Debug.WriteLine($"RenderWidth:{RenderSize.Width}; RenderHeight:{RenderSize.Height}");
            drawingContext.DrawImage(mapImageSource, new Rect(RenderSize));
        }
    }
}
