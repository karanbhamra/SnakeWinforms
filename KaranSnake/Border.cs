using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KaranSnake
{
    class Border: Shape
    {
        int border;

        public Border(int x, int y, int width, int height, int borderwidth = 5)
        {
            this.Rect = new Rectangle(x, y, width, height);
            border = borderwidth;
        }

        public void DrawBorder(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Black, border), this.Rect);
        }
    }
}
