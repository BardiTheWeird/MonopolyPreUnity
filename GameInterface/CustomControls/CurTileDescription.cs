using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameInterface.CustomControls
{
    class CurTileDescription : TextBox, INotifyPropertyChanged
    {
        #region properties
        public static readonly DependencyProperty TileDescriptionRenderCommProperty =
            DependencyProperty.Register(
            "TileDescriptionRenderComm",
            typeof(string),
            typeof(CurTileDescription),
            new PropertyMetadata(OnTileDescriptionRenderCommChangedCallBack));

        public string TileDescriptionRenderComm
        {
            get => (string)GetValue(TileDescriptionRenderCommProperty);
            set
            {
                SetValue(TileDescriptionRenderCommProperty, value);
                RaisePropertyChanged(nameof(TileDescriptionRenderComm));
            }
        }

        public static readonly DependencyProperty TileDescriptionTileViewProperty =
            DependencyProperty.Register(
            "TileDescriptionTileView",
            typeof(string),
            typeof(CurTileDescription),
            new PropertyMetadata(OnTileDescriptionTileViewChangedCallBack));

        public string TileDescriptionTileView
        {
            get => (string)GetValue(TileDescriptionTileViewProperty);
            set
            {
                SetValue(TileDescriptionTileViewProperty, value);
                RaisePropertyChanged(nameof(TileDescriptionTileView));
            }
        }
        #endregion

        #region propertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static void OnTileDescriptionTileViewChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CurTileDescription c = sender as CurTileDescription;
            if (c != null)
                c.OnTileDescriptionTileViewChanged();
        }
        protected virtual void OnTileDescriptionTileViewChanged()
        {
            Text = TileDescriptionTileView;
            RaisePropertyChanged(nameof(Text));
        }

        private static void OnTileDescriptionRenderCommChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CurTileDescription c = sender as CurTileDescription;
            if (c != null)
                c.OnTileDescriptionRenderCommChanged();
        }
        protected virtual void OnTileDescriptionRenderCommChanged()
        {
            Text = TileDescriptionRenderComm;
            RaisePropertyChanged(nameof(Text));
        }
        #endregion

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
    }
}
