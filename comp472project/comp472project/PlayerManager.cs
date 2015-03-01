using System.Collections;

namespace comp472project
{
    public abstract class PlayerManager
    {

        char color;
        //bool AI; 
        //Move move;

        public abstract string getMove();

        public PlayerManager(char color)
        {
            this.color = color;
        }

        public char getColor()
        {
            return color;
        }

    }
}