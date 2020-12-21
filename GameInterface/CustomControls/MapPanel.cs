using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace GameInterface.CustomControls
{
    class MapPanel : Panel
    {
        public MapPanel()
            : base()
        {

        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(new Point(0, 0), finalSize));
            }
            return finalSize; // Returns the final Arranged size
        }
    }
}
