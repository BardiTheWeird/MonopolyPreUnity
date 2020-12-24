using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;
using MonopolyPreUnity.UI;
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
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register(
            "Context",
            typeof(Context),
            typeof(CurTileView),
            new PropertyMetadata(OnContextChangedCallBack));

        public Context Context
        {
            get => (Context)GetValue(ContextProperty);
            set
            {
                SetValue(ContextProperty, value);
                RaisePropertyChanged(nameof(Context));
            }
        }

        public static readonly DependencyProperty TileDescriptionProperty =
            DependencyProperty.Register(
            "TileDescription",
            typeof(string),
            typeof(CurTileView));

        public string TileDescription
        {
            get => (string)GetValue(TileDescriptionProperty);
            set
            {
                SetValue(TileDescriptionProperty, value);
                RaisePropertyChanged(nameof(TileDescription));
            }
        }

        public static readonly DependencyProperty TileLockedIdProperty =
            DependencyProperty.Register(
            "TileLockedId",
            typeof(int),
            typeof(CurTileView),
            new PropertyMetadata(OnTileLockChangedCallBack));

        public int TileLockedId
        {
            get => (int)GetValue(TileLockedIdProperty);
            set
            {
                Debug.WriteLine("Time to invalidate visual");
                SetValue(TileLockedIdProperty, value);
                RaisePropertyChanged(nameof(TileLockedId));
                InvalidateVisual();
            }
        }

        public static readonly DependencyProperty CurTileLockedProperty =
            DependencyProperty.Register(
            "CurTileLocked",
            typeof(bool),
            typeof(CurTileView));

        public bool CurTileLocked
        {
            get => (bool)GetValue(CurTileLockedProperty);
            set
            {
                Debug.WriteLine("Time to invalidate visual");
                SetValue(CurTileLockedProperty, value);
                RaisePropertyChanged(nameof(CurTileLocked));
                InvalidateVisual();
            }
        }

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

        #region fields
        DrawingGroup backingStore = new DrawingGroup();
        double aspectRatio => ActualWidth / ActualHeight;
        double minActualSide => Math.Min(ActualWidth, ActualHeight);

        FormatOutput formatOutput;
        #endregion

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

            if (!CurTileLocked)
            {
                SetNewCardImage(ClickedTileId);
                InvalidateVisual();
            }
        }

        private static void OnTileLockChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CurTileView c = sender as CurTileView;
            if (c != null)
                c.OnTileLockChanged();
        }
        protected virtual void OnTileLockChanged()
        {
            SetNewCardImage(TileLockedId - 1);
            InvalidateVisual();
        }

        private static void OnContextChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CurTileView c = sender as CurTileView;
            if (c != null)
                c.OnContextChanged();
        }
        protected virtual void OnContextChanged()
        {
            formatOutput = new FormatOutput(Context);
        }
        #endregion

        #region cardMethods
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

                SetTileDescription(index);
            }
            else if (backingStore.Children.Count > 0)
                backingStore.Children.RemoveAt(0);
        }

        void SetTileDescription(int index)
        {
            var prop = Context.GetTileComponent<Property>(index + 1);
            if (prop == null)
            {
                if (!CurTileLocked)
                {
                    var actionBox = Context.GetTileComponent<ActionBox>(index + 1);
                    if (actionBox != null)
                        TileDescription = "";
                }
                return;
            }

            var auctionInfo = Context.AuctionInfo();
            if (auctionInfo != null)
                return;

            TileDescription = formatOutput.GetPropertyString(index + 1);
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
        #endregion

        #region ctor
        public CurTileView()
        {
            ClickedTileId = -1;
        }
        #endregion

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawDrawing(backingStore);
        }
    }
}
