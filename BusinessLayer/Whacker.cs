using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BusinessLayer
{
    public class Whacker
    {

        Rectangle innerRectangle;
        Rectangle gameArea;

        private readonly int offset;

        private int horizontalHeight = 20;
        private int verticalWidth = 20;



        public enum Wall
        {
            Top,
            Bottom,
            Left,
            Right,
        }

    

        public enum Direction
        {
            Left,
            Right
        }
        /// <summary>
        /// Whacker constructor
        /// </summary>
        /// <param name="gameArea"></param>
        /// <param name="wall"></param>
        /// <param name="comingFrom"></param>
        public Whacker(Rectangle gameArea, Wall wall, Direction comingFrom)
        {
            //where should i put the wall
            switch (wall)
            {
                //bottom
                case Wall.Bottom:
                    {
                        //bottom right if coming the left
                        innerRectangle.Height = horizontalHeight;
                        innerRectangle.Width = gameArea.Width / 2;
                        if (comingFrom == Direction.Left)
                        {                       
                            innerRectangle.X = gameArea.Width / 2;
                            innerRectangle.Y = gameArea.Height - 30;
                        }
                        //bottom left if coming from the right 
                        else
                        { 
                            innerRectangle.X = 0;
                            innerRectangle.Y = gameArea.Height - 30;
                        }
                     
                        break;
                    }
                case Wall.Left:
                    {
                        innerRectangle.Height = gameArea.Height / 2;
                        innerRectangle.Width = verticalWidth;
                        if (comingFrom == Direction.Left)
                        {
                            innerRectangle.X = 30 - verticalWidth;
                            innerRectangle.Y = gameArea.Height / 2;
                        }
                        else
                        {
                            innerRectangle.X = 30 - verticalWidth;
                            innerRectangle.Y = 0;
                        }
                        break;
                    }
                case Wall.Top:
                    {
                        innerRectangle.Height = horizontalHeight;
                        innerRectangle.Width = gameArea.Width / 2;
                        if (comingFrom == Direction.Left)
                        { 
                            innerRectangle.X = 0;
                            innerRectangle.Y = 30 - horizontalHeight;
                        }
                        else
                        {
                            innerRectangle.X = gameArea.Width / 2;
                            innerRectangle.Y = 30 - horizontalHeight; 
                        }

                        break;
                    }
                case Wall.Right:
                    {
                        innerRectangle.Height = gameArea.Height / 2;
                        innerRectangle.Width = verticalWidth;
                        if (comingFrom == Direction.Left)
                        {
                            innerRectangle.X = gameArea.Width - 30;
                            innerRectangle.Y = 0;
                        }
                        else
                        {
                            innerRectangle.X = gameArea.Width - 30;
                            innerRectangle.Y = gameArea.Height / 2;
                        }
                        break;
                    }
            }
          
               

  
        }
        /// <summary>
        /// draw the whacker
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            //draw the whacker with the graphics passed in from the form
            SolidBrush brush = new SolidBrush(Color.White);
            graphics.FillRectangle(brush, innerRectangle);
        }
        /// <summary>
        /// move the whacker
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="gameArea"></param>
        /// <param name="currentWall"></param>
        /// <returns></returns>

        public bool Move(Direction direction, Rectangle gameArea, Wall currentWall)
        {
            bool switchWall = false;
            int horiztonalOffset = gameArea.Width / 2;
            int verticalOffset = gameArea.Height / 2;
            switch (direction)
            {
                case Direction.Left:
                    {

                        switch (currentWall)
                        {
                            case Wall.Bottom:
                                {
                                    innerRectangle.X -= horiztonalOffset;
                                    if (innerRectangle.X < 0)
                                    {
                                        //hit wall
                                        switchWall = true;
                                    }
                                    break;
                                }
                            case Wall.Left:
                                {
                                    innerRectangle.Y -= verticalOffset;
                                    if(innerRectangle.Y < 0)
                                    {
                                        switchWall = true;
                                    }
                                    break;
                                }
                            case Wall.Top:
                                {
                                    innerRectangle.X += horiztonalOffset;
                                    if (innerRectangle.X > (gameArea.Width / 2))
                                    {
                                        switchWall = true;
                                    }
                                    break;
                                }
                            case Wall.Right:
                                {
                                    innerRectangle.Y += verticalOffset;
                                    if (innerRectangle.Y >(gameArea.Height/2))
                                    {
                                        switchWall = true;
                                    }
                                    break;
                                }
                         
                        }
                        break;
                    }
                case Direction.Right:
                    {
                        switch (currentWall)
                        {
                            case Wall.Bottom:
                                {
                                    innerRectangle.X += horiztonalOffset;
                                    if (innerRectangle.X > horiztonalOffset)
                                    {
                                        //hit wall
                                        switchWall = true;
                                    }
                                    break;
                                }
                            case Wall.Left:
                                {
                                    innerRectangle.Y += verticalOffset;
                                    if (innerRectangle.Y > verticalOffset)
                                    {
                                        switchWall = true;
                                    }
                                    break;
                                }
                            case Wall.Top:
                                {
                                    innerRectangle.X -= horiztonalOffset;
                                    if (innerRectangle.X < 0)
                                    {
                                        switchWall = true;
                                    }
                                    break;
                                }
                            case Wall.Right:
                                {
                                    innerRectangle.Y -= verticalOffset;
                                    if (innerRectangle.Y < 0)
                                    {
                                        switchWall = true;
                                    }
                                    break;
                                }

                        }
                        break;
                    }

            }
            return switchWall;
        }
       
        /// <summary>
        /// Inner rectangle
        /// </summary>
        public Rectangle InnerRectangle
        {
            get
            {
                return innerRectangle;
            }
        }
      


    }
}
