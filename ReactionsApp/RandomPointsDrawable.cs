using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp
{
    public class RandomPointsDrawable : IDrawable
    {
        private readonly Color FILL_COLOR = Color.FromRgb(0, 0, 255);
        private readonly Color CLICKED_FILL_COLOR = Color.FromRgb(0, 255, 0);
        private const double RADIUS = 20;

        private Point currentCircleCenter;
        private bool isCurrentCircleSelected;


        public RandomPointsDrawable()
        {

        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (isCurrentCircleSelected)
            {
                canvas.FillColor = CLICKED_FILL_COLOR;
                canvas.FillCircle(currentCircleCenter, RADIUS);
                isCurrentCircleSelected = false;
            }
            else
            {
                double pointX = Random.Shared.NextDouble() * (dirtyRect.Width - RADIUS);
                double pointY = Random.Shared.NextDouble() * (dirtyRect.Height - RADIUS);
                var center = new Point(pointX, pointY);
                currentCircleCenter = center;
                canvas.FillColor = FILL_COLOR;
                canvas.FillCircle(center, RADIUS);                
            }            
        }

        public bool TrySelectCircle(Point point)
        {
            double distance = Math.Sqrt(Math.Pow(point.X - currentCircleCenter.X, 2) + Math.Pow(point.Y - currentCircleCenter.Y, 2));

            if (distance <= RADIUS)
            { 
                isCurrentCircleSelected = true;
                return true;
            }

            return false;
        }
    }
}
