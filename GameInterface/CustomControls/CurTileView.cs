using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameInterface.CustomControls
{
    class CurTileView : FrameworkElement, INotifyPropertyChanged
    {
        #region properties
        public static readonly DependencyProperty ClickedTileIdProperty =
            DependencyProperty.Register(
            "ClickedTileId",
            typeof(int),
            typeof(CurTileView),
            new PropertyMetadata(OnPointClickedChangedCallBack));

        public int ClickedTileId 
        {
            get => (int)GetValue(ClickedTileIdProperty);
            set
            {
                Debug.WriteLine("Time to invalidate visual");
                SetValue(ClickedTileIdProperty, value);
                RaisePropertyChanged(nameof(ClickedTileId));
                InvalidateVisual();
            }
        }

        public static readonly DependencyProperty PointClickedProperty =
            DependencyProperty.Register(
            "PointClicked", 
            typeof(Point),
            typeof(CurTileView),
            new PropertyMetadata(OnPointClickedChangedCallBack));
        public Point PointClicked
        {
            get => (Point)GetValue(PointClickedProperty);
            set
            {
                Debug.WriteLine("Time to invalidate visual");
                SetValue(PointClickedProperty, value);
                RaisePropertyChanged(nameof(PointClicked));
                InvalidateVisual();
            }
        }
        #endregion

        DrawingGroup backingStore = new DrawingGroup();
        double aspectRatio => ActualWidth / ActualHeight;
        double minActualSide => Math.Min(ActualWidth, ActualHeight);

        #region propertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static void OnPointClickedChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CurTileView c = sender as CurTileView;
            if (c != null)
                c.OnPointClickedChanged();
        }

        protected virtual void OnPointClickedChanged()
        {
            //Debug.WriteLine("CurTileView OnPointClickedChanged");
            RaisePropertyChanged(nameof(PointClicked));
            RaisePropertyChanged(nameof(ClickedTileId));

            
            SetNewCardImage(ClickedTileId);
            InvalidateVisual();
        }
        #endregion

        void SetNewCardImage(int index)
        {
            if (index != -1)
            {
                var image = new BitmapImage(new Uri(@"..\..\..\Images\tiles\detalizedIcons\" + index.ToString() + @".png", UriKind.Relative));
                var drawing = new ImageDrawing(image, GetImageRectangle(image));

                if (backingStore.Children.Count > 0)
                    backingStore.Children[0] = drawing;
                else
                    backingStore.Children.Add(drawing);
            }
            else if (backingStore.Children.Count > 0)
                backingStore.Children.RemoveAt(0);
        }

        Rect GetImageRectangle(BitmapImage image)
        {
            var imageRatio = image.Width / image.Height;
            Size size;
            if (Math.Abs(imageRatio - 1) < 0.01)
                size = new Size(minActualSide, minActualSide);

            if (imageRatio > aspectRatio)
                size = new Size(ActualWidth, ActualWidth / aspectRatio);
            else
                size = new Size(ActualHeight * imageRatio, ActualHeight);

            return new Rect(new Point(0, 0), size);
        }

        public CurTileView()
        {
            //MouseLeftButtonUp += CurTileView_MouseLeftButtonUp;
            ClickedTileId = -1;
        }

        //private void CurTileView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    ClickedPoint = e.GetPosition((UIElement)sender);
        //    Debug.WriteLine($"Well duh, I tried, {ClickedPoint}");
        //}

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            //var text = new FormattedText(
            //    ClickedTileId.ToString(),
            //    CultureInfo.InvariantCulture,
            //    FlowDirection.LeftToRight,
            //    new Typeface("Verdana"),
            //    12,
            //    Brushes.Black,
            //    VisualTreeHelper.GetDpi(this).PixelsPerDip);

            //drawingContext.DrawText(text, new Point(0, 0));

            drawingContext.DrawDrawing(backingStore);
        }
    }
}
