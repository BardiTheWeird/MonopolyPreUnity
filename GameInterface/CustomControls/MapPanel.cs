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
            var minSide = Math.Min(availableSize.Width, availableSize.Height);
            return new Size(minSide, minSide);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var minSide = Math.Min(finalSize.Width, finalSize.Height);
            finalSize = new Size(minSide, minSide);
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(new Point(0, 0), finalSize));
            }
            return finalSize; // Returns the final Arranged size
        }
    }
}
