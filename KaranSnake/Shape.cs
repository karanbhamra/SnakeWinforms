using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KaranSnake
{
    class Shape
    {
        public Rectangle Rect { get; set; }

        public int LeftPos()
        {
            return Rect.Location.X;
        }

        public int RightPos()
        {
            return Rect.Location.X + Rect.Width;
        }

        public int UpPos()
        {
            return Rect.Location.Y;
        }

        public int DownPos()
        {
            return Rect.Location.Y + Rect.Height;
        }

        public bool CollidesWith(Shape s)
        {
            return (Rect.IntersectsWith(s.Rect));
        }

    }


}
