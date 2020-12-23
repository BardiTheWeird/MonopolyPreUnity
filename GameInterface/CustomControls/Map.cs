using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GameInterface.Extensions;
using MonopolyPreUnity.Classes;
using MonopolyPreUnity.Components;
using MonopolyPreUnity.Entity;
using MonopolyPreUnity.Entity.ContextExtensions;

namespace GameInterface.CustomControls
{
    //[ContentProperty("Content")]
    class Map : FrameworkElement, INotifyPropertyChanged
    {
        #region properties
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register(
            "Context",
            typeof(Context),
            typeof(Map),
            new FrameworkPropertyMetadata { AffectsRender = true });

        public Context Context
        {
            get => (Context)GetValue(ContextProperty);
            set
            {
                SetValue(ContextProperty, value);
                RaisePropertyChanged(nameof(Context));
            }
        }

        public static readonly DependencyProperty PlayerMovedProperty =
            DependencyProperty.Register(
            "PlayerMoved",
            typeof(bool),
            typeof(Map),
            new FrameworkPropertyMetadata { AffectsRender = true });

        public bool PlayerMoved
        {
            get => (bool)GetValue(PlayerMovedProperty);
            set
            {
                SetValue(PlayerMovedProperty, value);
                RaisePropertyChanged(nameof(PlayerMoved));
            }
        }

        public static readonly DependencyProperty HouseChangedProperty =
            DependencyProperty.Register(
            "HouseChanged",
            typeof(bool),
            typeof(Map),
            new FrameworkPropertyMetadata { AffectsRender = true });

        public bool HouseChanged
        {
            get => (bool)GetValue(HouseChangedProperty);
            set
            {
                SetValue(HouseChangedProperty, value);
                RaisePropertyChanged(nameof(HouseChanged));
            }
        }

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
        // (curTile, icon); id through index

        int botRightIndex = 0;
        int botLeftIndex = 10;
        int topLeftIndex = 20;
        int topRightIndex = 30;

        double imageSide => tileImages[0].Width * 2 + tileImages[1].Width * 9;
        double timeToActualRatio => ActualWidth / imageSide;
        Size tile => new Size(tileImages[1].Height, tileImages[1].Width);
        Size bigTile => new Size(tileImages[0].Height, tileImages[0].Width);
        Size houseIcon => new Size(houseIcons[0][0].Height, houseIcons[0][0].Width);
        Point houseIconRelativePosition = new Point(600, 875);

        Size actualTileSize => new Size(timeToActualRatio * tile.Height, timeToActualRatio * tile.Width );
        Size actualBigTileSize => new Size(timeToActualRatio * bigTile.Height, timeToActualRatio * bigTile.Width);
        Size actualHouseIconSize => new Size(timeToActualRatio * houseIcon.Width, timeToActualRatio * houseIcon.Height);
        Point actualHouseIconRelativePosition => new Point(houseIconRelativePosition.X * timeToActualRatio, 
            houseIconRelativePosition.Y * timeToActualRatio);
        #endregion

        #region resources
        List<BitmapImage> playerImages = new List<BitmapImage>();
        List<BitmapImage> tileImages = new List<BitmapImage>(40);
        // [set][numberOfHouses]
        List<List<BitmapImage>> houseIcons = new List<List<BitmapImage>>(8);


        List<Rect> tileRects = new List<Rect>(40);

        void LoadResources()
        {
            // player icons
            for (int i = 1; i <= 4; i++)
            {
                var uri = new Uri($@"..\..\..\..\GameInterface\Images\Players\{i}.png", UriKind.Relative);
                playerImages.Add(new BitmapImage(uri));
            }

            // tiles
            for (int i = 0; i < 40; i++)
            {
                var uri = new Uri($@"..\..\..\..\GameInterface\Images\tiles\mapIcons\{i}.png", UriKind.Relative);
                var bitmapImage = new BitmapImage(uri);

                // for columns
                if (botLeftIndex < i && i < topLeftIndex)
                    bitmapImage.Rotation = Rotation.Rotate270;
                else if (topRightIndex < i)
                    bitmapImage.Rotation = Rotation.Rotate90;

                tileImages.Add(bitmapImage);
            }

            // house
            for (int set = 1; set <= 8; set++)
            {
                var setList = new List<BitmapImage>(6);
                for (int house = 0; house <= 5; house++)
                {
                    var uri = new Uri($@"..\..\..\..\GameInterface\Images\HouseIcons\{set}set_{house}.png", UriKind.Relative);
                    var bitmap = new BitmapImage(uri);
                    setList.Add(bitmap);
                }
                houseIcons.Add(setList);
            }
        }
        #endregion

        #region ctor
        public Map()
        {
            mapImageSource = new BitmapImage(new Uri(@"..\..\..\..\GameInterface\Images\newmap.png", UriKind.Relative));
            //mapImageSource = new BitmapImage(new Uri(@"D:\ImmaCodder\Kursach\MonopolyPreUnity\GameInterface\Images\newmap.png"));
            PointClicked = new Point();
            ClickedTileId = -1;
            MouseLeftButtonUp += Map_MouseLeftButtonUp;

            LoadResources();
        }
        #endregion

        #region drawing groups
        DrawingGroup mapGroup = new DrawingGroup();
        DrawingGroup rectangleGroup = new DrawingGroup();
        DrawingGroup playersGroup = new DrawingGroup();
        DrawingGroup housesGroup = new DrawingGroup();
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

            if (ClickedTileId >= 0)
            {
                var rect = tileRects[ClickedTileId];
                //if (ClickedTileId != -1)
                //{
                //    Debug.WriteLine($"Clicked (id = {ClickedTileId}) rect:\n" +
                //        $"  Old method: {rect}\n" +
                //        $"  New method: {tileRects[ClickedTileId]}");
                //    Debug.WriteLine($"ActualWidth: {ActualWidth}, ActualHeigh {ActualHeight};" +
                //        $"CalcAWidht: {imageSide * timeToActualRatio}");
                //}

                var geomDrawing = new GeometryDrawing(
                    new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                    new Pen(Brushes.IndianRed, 4),
                    new RectangleGeometry(rect));

                rectangleGroup.Children.Add(geomDrawing);
            }
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
        void CalculateTileRects()
        {
            //Debug.WriteLine("ran CulculateTileRects");
            var sizeBig = actualBigTileSize;
            var sizeReg = actualTileSize;
            var sizeRegRotated = new Size(sizeReg.Height, sizeReg.Width);

            var origin = new Point(0, 0);
            // top row
            tileRects[topLeftIndex] = new Rect(origin, sizeBig);
            origin.X += sizeBig.Width;
            for (int i = topLeftIndex + 1; i < topRightIndex; i++)
            {
                tileRects[i] = new Rect(origin, sizeReg);
                origin.X += sizeReg.Width;
            }
            tileRects[topRightIndex] = new Rect(origin, sizeBig);
            // right column
            origin.Y += sizeBig.Height;
            for (int i = topRightIndex + 1; i < 40; i++)
            {
                tileRects[i] = new Rect(origin, sizeRegRotated);
                origin.Y += sizeReg.Width;
            }
            // bottom row
            tileRects[botRightIndex] = new Rect(origin, sizeBig);
            origin.X -= sizeReg.Width;
            for (int i = botRightIndex + 1; i < botLeftIndex; i++)
            {
                tileRects[i] = new Rect(origin, sizeReg);
                origin.X -= sizeReg.Width;
            }
            origin.X = 0;
            tileRects[botLeftIndex] = new Rect(origin, sizeBig);
            // left column
            origin.Y -= sizeReg.Width;
            for (int i = botLeftIndex + 1; i < topLeftIndex; i++)
            {
                tileRects[i] = new Rect(origin, sizeRegRotated);
                origin.Y -= sizeReg.Width;
            }
        }
        #endregion

        #region map render methods
        void RenderMap()
        {
            //var straights = new DrawingCollection();
            //var lefties = new DrawingGroup();
            //var righties = new DrawingGroup();

            //// top and bottom
            //for (int i = 0; i <= 10; i++)
            //{
            //    straights.Add(new ImageDrawing(tileImages[i], tileRects[i]));
            //    straights.Add(new ImageDrawing(tileImages[i + 20], tileRects[i + 20]));
            //}
            //for (int i = 11; i < 20; i++)
            //{
            //    lefties.Children.Add(new ImageDrawing(tileImages[i], tileRects[i]));
            //    righties.Children.Add(new ImageDrawing(tileImages[i + 20], tileRects[i + 20]));
            //}

            //lefties.Transform = new RotateTransform(90);
            //righties.Transform = new RotateTransform(270);

            //mapGroup.Children = straights;
            //mapGroup.Children.Add(lefties);
            //mapGroup.Children.Add(righties);

            var drawingCollection = new DrawingCollection();

            // top and bottom
            for (int i = 0; i < 40; i++)
            {
                drawingCollection.Add(new ImageDrawing(tileImages[i], tileRects[i]));
            }

            mapGroup.Children = drawingCollection;
        }
        void RenderPlayers()
        {
            var tileGroups = Context.PlayerObservables
                .Select(x => x.Player)
                .GroupBy(x => x.CurTileId);

            var drawings = new DrawingCollection();

            foreach (var tileGroup in tileGroups)
            {
                // implement dynamic tile layout for multiple players
                foreach (var player in tileGroup)
                {
                    var tileRect = tileRects[player.CurTileId - 1];
                    var imageDrawing = new ImageDrawing(playerImages[player.Id - 1], tileRect);
                    drawings.Add(imageDrawing);
                }
            }

            playersGroup.Children = drawings;
            //InvalidateVisual();
        }
        void RenderHouses()
        {
            var relativePosition = actualHouseIconRelativePosition;
            var actualSize = actualHouseIconSize;
            var tileSize = actualTileSize;
            var drawingCollection = new DrawingCollection();

            // (id, set, housesBuilt)
            var houseTiles = Context.GetComponents<Tile>()
                .Where(tile => tile.ContainsComponent(typeof(PropertyDevelopment), Context))
                .Select(x =>
                {
                    return (x.Id,
                    Context.GetTileComponent<Property>(x.Id).SetId,
                    Context.GetTileComponent<PropertyDevelopment>(x.Id).HousesBuilt);
                });

            foreach (var tile in houseTiles)
            {
                var tileRect = tileRects[tile.Id - 1];
                var rectOrigin = tileRect.TopLeft;
                var houseRect = new Rect(rectOrigin, actualSize);
                Point pointToCenterAround = new Point(rectOrigin.X + relativePosition.X, rectOrigin.Y + relativePosition.Y);

                if (11 <= tile.Id - 1 && tile.Id - 1 <= 19)
                {
                    pointToCenterAround = new Point(rectOrigin.X + actualTileSize.Height * 0.185d,
                        rectOrigin.Y + relativePosition.X);
                }
                else if (31 <= tile.Id - 1)
                {
                    pointToCenterAround = new Point(rectOrigin.X + actualTileSize.Height * 0.825d,
                        rectOrigin.Y + actualTileSize.Width * 0.2d);
                }

                houseRect = houseRect.Center(pointToCenterAround);

                var imgDrawing = new ImageDrawing();
                imgDrawing.ImageSource = houseIcons[tile.SetId - 1][tile.HousesBuilt];
                imgDrawing.Rect = houseRect;
                drawingCollection.Add(imgDrawing);
            }

            housesGroup.Children = drawingCollection;
        }
        #endregion
        protected override Size MeasureOverride(Size availableSize)
        {
            //Debug.WriteLine("Yay, it works for size change!");
            //if (tileRects.Count < 40)
            //{
            //    for (int i = tileRects.Count; i < 40; i++)
            //        tileRects.Add(default);
            //}
            //CalculateTileRects();
            //Debug.WriteLine($"The size of rect on [0]: ({tileRects[0].Size})");

            var minSide = Math.Min(availableSize.Width, availableSize.Height);

            return base.MeasureOverride(new Size(minSide, minSide));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (tileRects.Count < 40)
            {
                for (int i = tileRects.Count; i < 40; i++)
                    tileRects.Add(default);
            }
            CalculateTileRects();
            Debug.WriteLine($"The size of rect on [0]: ({tileRects[0].Size})");

            var minSide = Math.Min(finalSize.Width, finalSize.Height);
            return base.ArrangeOverride(new Size(minSide, minSide));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            //Debug.WriteLine($"RenderWidth:{RenderSize.Width}; RenderHeight:{RenderSize.Height}");
            //if (tileRects[topLeftIndex] == new Rect(0, 0, 0, 0))
            //    CalculateTileRects();

            //drawingContext.DrawImage(mapImageSource, new Rect(RenderSize));

            RenderMap();
            RenderHouses();
            RenderPlayers();

            drawingContext.DrawDrawing(mapGroup);
            drawingContext.DrawDrawing(rectangleGroup);
            drawingContext.DrawDrawing(housesGroup);
            drawingContext.DrawDrawing(playersGroup);

            //var randomTileRect = GetTileRectangle((new Random()).Next(0, 40));
            //drawingContext.DrawRectangle(
            //    new SolidColorBrush(Color.FromRgb(0, 40, 0)),
            //    new Pen(Brushes.Black, 4),
            //    randomTileRect);
        }
    }
}
