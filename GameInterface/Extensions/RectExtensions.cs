using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace GameInterface.Extensions
{
    static class RectExtensions
    {
        /// <summary>
        /// shifts rect's origin point, so that point is in the center
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Rect Center(this Rect rect, Point point)
        {
            var newOriginPoint = new Point();
            newOriginPoint.X = point.X - rect.Width / 2;
            newOriginPoint.Y = point.Y - rect.Height / 2;

            return new Rect(newOriginPoint, rect.Size);
        }
    }
}
