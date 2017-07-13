using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KaranSnake
{
    class SnakePiece : Shape
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public int XSpeed { get; set; }
        public int YSpeed { get; set; }

        public SnakePiece(int x, int y)
        {
            this.Rect = new Rectangle(x, y, 10, 10);

        }

        public void DrawPiece(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), this.Rect);
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.Right:
                    MoveRight();
                    break;
                case Direction.Left:
                    MoveLeft();
                    break;
                case Direction.Up:
                    MoveUp();
                    break;
                case Direction.Down:
                    MoveDown();
                    break;
            }
        }

        public void MoveStop()
        {
            XSpeed = 0;
            YSpeed = 0;

            Point newPoint = new Point(this.Rect.X + XSpeed, this.Rect.Y);
            Rectangle newRect = new Rectangle(newPoint.X, newPoint.Y, this.Rect.Width, this.Rect.Height);
            this.Rect = newRect;
        }

        void MoveRight()
        {
            XSpeed = 10;
            YSpeed = 0;
            Point newPoint = new Point(this.Rect.X + XSpeed, this.Rect.Y);
            Rectangle newRect = new Rectangle(newPoint.X, newPoint.Y, this.Rect.Width, this.Rect.Height);
            this.Rect = newRect;
        }
        void MoveLeft()
        {
            XSpeed = -10;
            YSpeed = 0;
            Point newPoint = new Point(this.Rect.X + XSpeed, this.Rect.Y);
            Rectangle newRect = new Rectangle(newPoint.X, newPoint.Y, this.Rect.Width, this.Rect.Height);
            this.Rect = newRect;
        }

        void MoveUp()
        {
            XSpeed = 0;
            YSpeed = -10;
            Point newPoint = new Point(this.Rect.X, this.Rect.Y + YSpeed);
            Rectangle newRect = new Rectangle(newPoint.X, newPoint.Y, this.Rect.Width, this.Rect.Height);
            this.Rect = newRect;
        }
        void MoveDown()
        {
            XSpeed = 0;
            YSpeed = 10;
            Point newPoint = new Point(this.Rect.X, this.Rect.Y + YSpeed);
            Rectangle newRect = new Rectangle(newPoint.X, newPoint.Y, this.Rect.Width, this.Rect.Height);
            this.Rect = newRect;
        }
    }
}
