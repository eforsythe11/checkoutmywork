using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PresentationLayer
{
    public class Ball
    {
        private int xVelocity;
        private int yVelocity;
        private bool slowDown;
        private int hitCount;

        Rectangle innerRectangle;



        Random random = new Random();

        /// <summary>
        /// Constructor for the Ball, Rnadom Velocity
        /// </summary>
        /// <param name="gameplayArea"></param>
        public Ball(Rectangle gameplayArea)
        {

            

            int xRandomize = random.Next(0, 10);
            int yRandomize = random.Next(0, 10);
            int randy = 1;
            if(xRandomize > 5)
            {
                randy *= -1;
            }
            xVelocity = 20 * randy;

            if (yRandomize > 5)
            {
                randy *= -1;
            }
            yVelocity = 20 * randy;


            innerRectangle.Height = 30;
            innerRectangle.Width = 30;

            randomPosition(gameplayArea);
            //innerRectangle.X = gameplayArea.Width / 2 - innerRectangle.Width / 2; 
            //innerRectangle.Y = gameplayArea.Height / 2 - innerRectangle.Height / 2;

            


        }
        /// <summary>
        /// put the ball in one of two positions
        /// </summary>
        /// <param name="area"></param>
        public void randomPosition(Rectangle area)
        {
            Random rnd = new Random();
            int rndm = rnd.Next(0, 10);
           
            if(rndm < 5)
            {
                innerRectangle.X = (area.Width / 2) - 100; 
                innerRectangle.Y = (area.Height/2) - 30;
            }
            else
            {
                innerRectangle.X = (area.Width/2) + 200;
                innerRectangle.Y = (area.Height / 2) + 30;
            }
          
        }
        /// <summary>
        /// draw the ball
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            graphics.FillEllipse(brush, innerRectangle);

        }
        /// <summary>
        /// move the ball
        /// </summary>
        public void Move()
        {

            //change x and y position according to the velocity
            if(slowDown == true)
            {
                if(xVelocity < 0)
                {
                    innerRectangle.X += (xVelocity + 10);
                }
                else
                {
                    innerRectangle.X += (xVelocity - 10);
                }
                if(yVelocity < 0)
                {
                    innerRectangle.Y += (yVelocity + 10);
                }
                else
                {
                    innerRectangle.Y += (yVelocity - 10);
                }
            
            }
            else
            {
                innerRectangle.X += (xVelocity);
                innerRectangle.Y += (yVelocity);
            }

  
            
        }

        /// <summary>
        /// xPosiion get
        /// </summary>
        public int XPosition
        {
            get
            {
                return innerRectangle.X;
            }
        }
        /// <summary>
        /// YPosition get
        /// </summary>
        public int YPosition
        {
            get
            {
                return innerRectangle.Y;
            }
        }
        /// <summary>
        /// reset the ball
        /// </summary>
        /// <param name="gameplayArea"></param>
        public void Reset(Rectangle gameplayArea )
        {

            randomPosition(gameplayArea);

            int xRandomize = random.Next(0, 10);
            int yRandomize = random.Next(0, 10);
            int randy = 1;
            if (xRandomize > 5)
            {
                randy *= -1;
            }
            xVelocity = 20 * randy;

            if (yRandomize > 5)
            {
                randy *= -1;
            }
            yVelocity = 20 * randy;

        }
       
        /// <summary>
        /// Width get
        /// </summary>
        public int Width
        {
            get
            {
                return innerRectangle.Width;
            }
        }
        /// <summary>
        /// Height get
        /// </summary>
        public int Height
        {
            get
            {
                return innerRectangle.Height;
            }
        }
        /// <summary>
        /// get set xvelocity
        /// </summary>
        public int XVelocity
        {
            get { return xVelocity; }
            set { xVelocity = value; }
        }
        /// <summary>
        /// get set yvelocity
        /// </summary>
        public int YVelocity
        {
            get { return yVelocity; }
            set { yVelocity = value; }
        }
        /// <summary>
        /// hicount get set
        /// </summary>
        public int HitCount
        {
            get { return hitCount; }
            set { hitCount = value; }
        }
        /// <summary>
        /// slow down getset
        /// </summary>
        public bool SlowDown
        {
            get { return slowDown; }
            set { slowDown = value; }
        }


        /// <summary>
        /// inner rectangle get
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

