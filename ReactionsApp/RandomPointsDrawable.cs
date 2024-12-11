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
        private const float RADIUS = 20;

        public RandomPointsDrawable()
        {
            
        }

        public float CanvasWidth { get; set; }
        public float CanvasHeight { get; set; }

        public PointF CurrentCircleCenter { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float pointX = (float)Random.Shared.NextDouble() * (dirtyRect.Width - RADIUS);
            float pointY = (float)Random.Shared.NextDouble() * (dirtyRect.Height - RADIUS);
            var center = new PointF(pointX, pointY);
            CurrentCircleCenter = center;
            canvas.FillColor = FILL_COLOR;
            canvas.FillCircle(pointX, pointY, RADIUS);
        }

        public bool IsPointInsideCircle(PointF point)
        {
            float distance = (float)Math.Sqrt(Math.Pow(point.X - CurrentCircleCenter.X, 2) + Math.Pow(point.Y - CurrentCircleCenter.Y, 2));
        
            return distance <= RADIUS;
        }
    }
}
