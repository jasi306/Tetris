using System;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            TetrisLogic tetris = new TetrisLogic(25,12);
            tetris.mainLoop();
        }
    }
}

