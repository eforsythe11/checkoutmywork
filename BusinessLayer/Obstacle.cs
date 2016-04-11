using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BusinessLayer
{
    public class Obstacle
    {
      
        Rectangle[] innerRectangle;
        private int size = 200;
        private int length = 10;

        /// <summary>
        /// Constructor for obstace. create three 
        /// </summary>
        /// <param name="gameArea"></param>
        public Obstacle(Rectangle gameArea)
        {
            innerRectangle = new Rectangle[5];
            for(int i =0; i < 5; i++)
            {
                innerRectangle[i].Height = size;
                innerRectangle[i].Width = length;
                innerRectangle[i].X = (150 * i) + 500;
                innerRectangle[i].Y = 300;
            }

        }
        /// <summary>
        /// Draw the obstacles
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(Color.White);
            graphics.FillRectangles(brush, innerRectangle);
        }
        /// <summary>
        /// inner reactangle
        /// </summary>
        public Rectangle[] InnerRectangle
        {
            get
            {
                return innerRectangle;
            }
        }


    }
}
