using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaranSnake
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap canvas;
        Border border;

        SnakePiece head;
        List<SnakePiece> snake;
        SnakePiece food;

        Random rand;
        bool paused;

        SnakePiece.Direction currentDirection;

        // initialize graphics and draw on the picturebox 
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(canvas);
        }

        // Returns a random Direction (Up, Down, Left, Right) enum
        SnakePiece.Direction GetRandomDirection()
        {
            int randIndex = rand.Next(Enum.GetNames(typeof(SnakePiece.Direction)).Length);
            return (SnakePiece.Direction)randIndex;
        }

        // Print a message on form
        void DisplayOnScreen(string message)
        {
            g.DrawString(message, new Font("Consolas", 12), new SolidBrush(Color.Black), (ClientSize.Width / 2) - 40, 20);
        }

        // initialize values
        void InitGame()
        {
            rand = new Random();
            border = new Border(0, 0, ClientSize.Width, ClientSize.Height, 20);
            //randomized start location
            head = new SnakePiece(rand.Next(20, ClientSize.Width - 20), rand.Next(20, ClientSize.Height - 20));
            //head = new SnakePiece(40, 40);
            snake = new List<SnakePiece>();
            snake.Add(head);
            // food randomly spawns at new location within the screen
            food = new SnakePiece(rand.Next(20, ClientSize.Width - 20), rand.Next(20, ClientSize.Height - 20));
            currentDirection = GetRandomDirection();
            //currentDirection = SnakePiece.Direction.Right;
            paused = false;

        }

        // on form load initialize with values
        private void Form1_Load(object sender, EventArgs e)
        {
            InitGame();
        }

        // move each of the snake blocks
        void MoveSnake()
        {
            // starting at the last one
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                // if its the head, set its direction according to key press
                if (i == 0)
                {
                    snake[i].Move(currentDirection);

                    //Detect collission with body
                    for (int j = 1; j < snake.Count; j++)
                    {
                        if (snake[i].Rect.X == snake[j].Rect.X && snake[i].Rect.Y == snake[j].Rect.Y)
                        {
                            GameOver(true);
                        }
                    }
                }
                else
                {
                    // for every other block, its new position will be the previous blocks position, starting after head
                    Point newLocation = new Point(snake[i - 1].Rect.X, snake[i - 1].Rect.Y);
                    snake[i].Rect = new Rectangle(newLocation, snake[i - 1].Rect.Size);

                }

            }
        }

        void DrawShapes()
        {
            // draw the border
            border.DrawBorder(g);

            // draw the food piece
            food.DrawPiece(g);

            // draw all blocks of the snake
            foreach (var piece in snake)
            {
                piece.DrawPiece(g);
            }
        }

        // GAME LOOP
        // every 250ms
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // erase display
            g.Clear(Color.White);

            // move snake
            MoveSnake();

            // if collision occurs with the food piece, we will add it to snake body
            EatFood();

            // Draw to screen
            DrawShapes();

            // if snake hits the border, we end the game
            GameOver();

            // check if game paused
            GamePaused();

            // draw to picturebox
            pictureBox1.Image = canvas;

        }

        void EatFood()
        {
            // if you eat the food
            if (head.CollidesWith(food))
            {

                // add new piece to snake's last block location
                SnakePiece newPiece = new SnakePiece(snake[snake.Count - 1].Rect.X, snake[snake.Count - 1].Rect.Y);
                snake.Add(newPiece);
                // food spawns at a random location
                food = new SnakePiece(rand.Next(20, ClientSize.Width - 20), rand.Next(20, ClientSize.Height - 20));
            }
        }

        // Stop game if the snake collides with the border
        void GameOver(bool gameover = false)
        {
            if (head.LeftPos() < 10 || head.RightPos() > ClientSize.Width - 10 || head.UpPos() < 10 || head.DownPos() > ClientSize.Height - 10 || gameover == true)
            {

                paused = true;
                head.MoveStop();
                DisplayOnScreen("Game Over!");
            }

        }

        // if game is paused, we stop our gameloop, otherwise start out gameloop again
        void GamePaused()
        {
            if (paused)
            {
                gameTimer.Stop();
            }
            else
            {
                gameTimer.Start();
            }
        }

        // Right/Left/Up/Down move the snake, Space restarts the game
        private void ButtonPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                currentDirection = SnakePiece.Direction.Right;
            }
            if (e.KeyCode == Keys.Left)
            {
                currentDirection = SnakePiece.Direction.Left;
            }
            if (e.KeyCode == Keys.Up)
            {
                currentDirection = SnakePiece.Direction.Up;
            }
            if (e.KeyCode == Keys.Down)
            {
                currentDirection = SnakePiece.Direction.Down;
            }

            // Pause the game
            if (e.KeyCode == Keys.P)
            {
                if (paused)
                {
                    paused = false;
                    gameTimer.Start();
                }
                else
                {
                    paused = true;
                }
            }
            // Restart the game
            if (e.KeyCode == Keys.Space)
            {
                InitGame();
                gameTimer.Start();
            }
        }
    }
}
