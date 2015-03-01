using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp472project
{
    class Move
    {
        int x;
        int y;

        public void setMove (string input)
        {
            input = input.ToUpper();
            char temp = input[0];
            x = (int)temp - 65;
            y = (int)input[1] - 49;
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
