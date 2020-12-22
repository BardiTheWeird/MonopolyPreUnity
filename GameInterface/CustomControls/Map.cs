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
        int botRightIndex = 0;
        int botLeftIndex = 10;
        int topLeftIndex = 20;
        int topRightIndex = 30;

        double imageSide = 6800;
        Size tile = new Size(832, 577.5);
        Size bigTile = new Size(815, 815);

        Size actualTileSize => new Size(RenderSize.Height * tile.Height / imageSide,
            RenderSize.Height * tile.Width / imageSide);
        Size actualBigTileSize => new Size(RenderSize.Height * bigTile.Height / imageSide,
            RenderSize.Height * bigTile.Width / imageSide);

        double tileWidth => ActualWidth - 2 * actualBigTileSize.Width;
        #endregion

        #region ctor
        public Map()
        {
            mapImageSource = new BitmapImage(new Uri(@"D:\ImmaCodder\Kursach\MonopolyPreUnity\GameInterface\Images\map.png"));
            PointClicked = new Point();
            ClickedTileId = -1;
            MouseLeftButtonUp += Map_MouseLeftButtonUp;
        }
        #endregion

        #region drawing groups
        DrawingGroup rectangleGroup = new DrawingGroup();
        #endregion

        #region click mathematics
        private void Map_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PointClicked = e.GetPosition((UIElement)sender);
            ClickedTileId = GetClickedTileInfo(PointClicked);
            DrawClickedTileRect();
            //Debug.WriteLine($"Well duh, I tried, {PointClicked}");
        }

        void DrawClickedTileRect()
        {
            if (rectangleGroup.Children.Count > 0)
                rectangleGroup.Children.RemoveAt(0);

            var rect = GetTileRectangle(ClickedTileId);
            var geomDrawing = new GeometryDrawing(
                new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                new Pen(Brushes.Black, 4),
                new RectangleGeometry(rect));

            rectangleGroup.Children.Add(geomDrawing);
            InvalidateVisual();
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
                    return topLeftIndex + offset + 1;
                }
                else if (hor == Position.left)
                    return topLeftIndex;
                else
                    return topRightIndex;
            }
            else if (vert == Position.bottom)
            {
                if (hor == Position.middle)
                {
                    // count the tile
                    var offset = 8 - (int)((point.X - actualBigTileSize.Width) / actualTileSize.Width);
                    return botRightIndex + offset + 1;
                }
                else if (hor == Position.left)
                    return botLeftIndex;
                else
                    return botRightIndex;
            }
            else
            {
                if (hor == Position.left)
                {
                    // count the tile
                    var offset = 8 - (int)((point.Y - actualBigTileSize.Height) / actualTileSize.Width);
                    return botLeftIndex + offset + 1;
                }
                else if (hor == Position.right)
                {
                    // count the tile
                    var offset = (int)((point.Y - actualBigTileSize.Height) / actualTileSize.Width);
                    return topRightIndex + offset + 1;
                }
                else
                    return -1;
            }
        }
        #endregion

        #region mapRenderingMath
        Rect GetTileRectangle(int index)
        {
            Point origin;
            Size size;
            bool invert = false;

            if (index <= botLeftIndex) // bottom row
            {
                origin = new Point(0, ActualHeight - actualTileSize.Height);

                if (index != botLeftIndex)
                {
                    if (index == botRightIndex)
                        origin.X = ActualWidth - actualBigTileSize.Width;
                    else
                    {
                        origin.X += actualBigTileSize.Width;
                        origin.X += actualTileSize.Width * (9 - index % 10);
                    }
                }
            }
            else if (index < topLeftIndex) // left column
            {
                invert = true;
                origin = new Point(0, actualBigTileSize.Height);
                origin.Y += actualTileSize.Width * (9 - index % 10);
            }
            else if (index <= topRightIndex) // top row
            {
                origin = new Point(0, 0);

                if (index != topLeftIndex)
                {
                    if (index == topRightIndex)
                        origin.X = ActualWidth - actualBigTileSize.Width;
                    else
                    {
                        origin.X += actualBigTileSize.Width;
                        origin.X += actualTileSize.Width * (index % 10 - 1);
                    }
                }
            }
            else // right column
            {
                invert = true;
                origin = new Point(ActualWidth - actualBigTileSize.Width, actualBigTileSize.Height);
                origin.Y += actualTileSize.Width * (index % 10 - 1);
            }

            if (index % 10 == 0)
                size = actualBigTileSize;
            else if (invert)
                size = new Size(actualTileSize.Height, actualTileSize.Width);
            else
                size = actualTileSize;
            return new Rect(origin, size);
        }
        #endregion

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            //Debug.WriteLine($"RenderWidth:{RenderSize.Width}; RenderHeight:{RenderSize.Height}");
            drawingContext.DrawImage(mapImageSource, new Rect(RenderSize));
            drawingContext.DrawDrawing(rectangleGroup);
        }
    }
}
