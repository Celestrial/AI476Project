using System.Collections;
using System;

namespace comp472project
{
    public class HPlayer : PlayerManager
    {

        public HPlayer(char color) : base(color) { }

        public override string getMove()
        {
            Console.Out.Write("Please enter "+getColor()+" play position: ");
            return Console.ReadLine().ToUpper();
        }

    }
}