using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp472project
{
    public class Move
    {
        int x;
        int y;
        char color;

        public void setMove (string input)
        {
            try
            {
                input = input.ToUpper();
                char temp = input[0];
                x = (int)temp - 65;
                if (input.Length == 3)
                    y = Convert.ToInt32(input.Substring(1))-1;
                else
                    y = (int)input[1] - 49;
            }
            catch (Exception e)
            {
                x = -1;
                y = -1;
            }
        }
        public void setMove(int x, int y, char color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }
    }


}
