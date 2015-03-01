using System.Collections;

namespace comp472project
{
    public abstract class PlayerManager
    {

        char color;
        //bool AI; 
        Move move;

        public PlayerManager(char color)
        {
            this.color = color;
        }

        public abstract string getMove();

        public char getColor()
        {
            return color;
        }

    }
}