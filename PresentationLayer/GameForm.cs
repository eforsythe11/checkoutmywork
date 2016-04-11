using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace PresentationLayer
{
    public partial class GameForm : Form
    {
        Whacker whacker;
        Ball ball;
        Obstacle obstacles;
        int currentWall;
        bool pause1;
        bool pause2;
        //0 = bottom
        //1 = left
        //2 = top
        //3 = right

        public GameForm()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Load the game state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            whacker = new Whacker(this.DisplayRectangle, Whacker.Wall.Bottom, Whacker.Direction.Left);
            obstacles = new Obstacle(this.DisplayRectangle);
            ball = new Ball(this.DisplayRectangle);
            ball.HitCount = 0;
            currentWall = 0;

        }
        /// <summary>
        /// paint the canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            whacker.Draw(e.Graphics);
            obstacles.Draw(e.Graphics);
            ball.Draw(e.Graphics);

            Font displayFont = new Font("Arial", 16);
            SolidBrush brush = new SolidBrush(Color.White);
            string displayString = "Whack Count: {0}";

            e.Graphics.DrawString(String.Format(displayString, ball.HitCount), displayFont, brush, new Point(50, 50));
            bool gameOver = PaddleMisses(ball);
            if (gameOver)
            {
                e.Graphics.DrawString(String.Format("GAME OVER!"), displayFont, brush, new Point(this.DisplayRectangle.Width / 2, this.DisplayRectangle.Height / 4));
                e.Graphics.DrawString(String.Format("Hit R to Restart"), displayFont, brush, new Point(this.DisplayRectangle.Width / 2, (this.DisplayRectangle.Height / 4 + 20)));
            }
            if(pause1 == true)
            {
                e.Graphics.DrawString(String.Format("GET READY!"), displayFont, brush, new Point(this.DisplayRectangle.Width / 2, this.DisplayRectangle.Height / 4));
                pause2 = true;
            }
        }
        /// <summary>
        /// do this every timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pause2 == true)
            {

                pause1 = false;
                pause2 = false;
            }

            bool gameOver = PaddleMisses(ball);
            if (!gameOver)
            {
                    ball.Move();
                    HandleCollisions();
                          
            }
            else
            {
                timer1.Stop();
            }
            Invalidate();
        }
        /// <summary>
        /// handle key presses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    {
                        //go counter clockwise
                        if (currentWall == 0)
                        {
                            //going , current wall, potential next wall
                            Move(Whacker.Direction.Right, Whacker.Wall.Bottom, Whacker.Wall.Right);
                        }
                        else if (currentWall == 1)
                        {
                            Move(Whacker.Direction.Right, Whacker.Wall.Left, Whacker.Wall.Bottom);
                        }
                        else if (currentWall == 2)
                        {
                            Move(Whacker.Direction.Right, Whacker.Wall.Top, Whacker.Wall.Left);
                        }
                        else if (currentWall == 3)
                        {
                            Move(Whacker.Direction.Right, Whacker.Wall.Right, Whacker.Wall.Top);
                        }
                        break;

                    }
                case Keys.Left:
                    {
                        //go to clockwise
                        if (currentWall == 0)
                        {
                            //going , current wall, potential next wall
                            Move(Whacker.Direction.Left, Whacker.Wall.Bottom, Whacker.Wall.Left);
                        }
                        else if (currentWall == 1)
                        {
                            Move(Whacker.Direction.Left, Whacker.Wall.Left, Whacker.Wall.Top);
                        }
                        else if (currentWall == 2)
                        {
                            Move(Whacker.Direction.Left, Whacker.Wall.Top, Whacker.Wall.Right);
                        }
                        else if (currentWall == 3)
                        {
                            Move(Whacker.Direction.Left, Whacker.Wall.Right, Whacker.Wall.Bottom);
                        }
                        break;

                    }
                case Keys.Space:
                    {
                        if (!timer1.Enabled)
                        {
                            timer1.Start();
                            ball.SlowDown = true;
                        }
                        break;
                    }
                case Keys.R:
                    {
                
                        ball.Reset(this.DisplayRectangle);
                        ball.HitCount = 0;
                        ball.SlowDown = true;
                        timer1.Start();
                       
                        break;
                    }

            }
        }
        /// <summary>
        /// move the whacker from wall the wall method
        /// </summary>
        /// <param name="goingTo"></param>
        /// <param name="startWall"></param>
        /// <param name="nextWall"></param>
        public void Move(Whacker.Direction goingTo, Whacker.Wall startWall, Whacker.Wall nextWall)
        {

            bool switchWall;

            switchWall = whacker.Move(goingTo, this.DisplayRectangle, startWall);

            if (switchWall)
            {

                whacker = new Whacker(this.DisplayRectangle, nextWall, goingTo);

                //WHAT WALL U ON ???
                if (goingTo == Whacker.Direction.Left) {
                    if (currentWall < 3)
                    {
                        currentWall++;
                    }
                    else
                    {
                        currentWall = 0;
                    }
                }
                else
                {
                    if (currentWall > 0)
                    {
                        currentWall--;
                    }
                    else
                    {
                        currentWall = 3;
                    }
                }
            }
        }
        /// <summary>
        /// detect a paddle miss
        /// </summary>
        /// <param name="ball"></param>
        /// <returns></returns>
        private bool PaddleMisses(Ball ball)
        {
            if (ball.InnerRectangle.X < 0 || ball.InnerRectangle.X > this.DisplayRectangle.Width)
            {
                return true;
            } else if (ball.InnerRectangle.Y < 0 || ball.InnerRectangle.Y > this.DisplayRectangle.Height)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Handle collisions
        /// </summary>
        private void HandleCollisions()
        {

            ////paddle
            if (whacker.InnerRectangle.IntersectsWith(ball.InnerRectangle))
            {

                
                if (ball.XPosition <= (this.DisplayRectangle.Left + 40))
                {
                    //left
                    ball.SlowDown = false;     
                    ball.XVelocity *= -1;
                    ball.HitCount++;
                }
                else if(ball.YPosition <= ((this.DisplayRectangle.Top - 40) - ball.Height)) 
                {
                    //top
                    ball.SlowDown = false;
                    ball.YVelocity *= -1;
                    ball.HitCount++;
                }
                 else if (ball.XPosition >= ((this.DisplayRectangle.Right -40) - ball.Width))
                {
                    //RIGHT
                    ball.SlowDown = false;
                    ball.XVelocity *= -1;
                    ball.HitCount++;
                }
                else if(ball.YPosition <= (this.DisplayRectangle.Bottom + 40))
                {
                    ball.SlowDown = false;
                    ball.YVelocity *= -1;
                    ball.HitCount++;
                }


            }

            for (int x = 0; x < 5; x++) {
                if (obstacles.InnerRectangle[x].IntersectsWith(ball.InnerRectangle))
                {
                    if(ball.XVelocity < 0 && ball.YVelocity < 0)
                    {
                       ball.XVelocity *= -1;
                    }
                    else if(ball.XVelocity > 0 && ball.YVelocity > 0)
                    {
                        ball.XVelocity *= -1;
                    }
                    else if(ball.XVelocity > ball.YVelocity)
                    {
                        ball.XVelocity *= -1;
                    }
                    else
                    {
                        ball.XVelocity *= -1;
                    }


                }
            }



        }
        


        }
    }




