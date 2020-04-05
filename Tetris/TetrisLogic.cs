using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    
    class TetrisLogic
    {
        Stats stats;
        Brick b;
        Brick[] nextBricks;
        Board board;
        public TetrisLogic(int ySize, int xSize)
        {
            Random rand = new Random();
            board = new Board( ySize,  xSize);
            b = new Brick();
            nextBricks = new Brick[5];

            for (int i = 0; i < nextBricks.Length; ++i)
            {
                nextBricks[i] = new Brick();
            }
            int topScore = 0;
            stats = new Stats(topScore);
        }

        public void mainLoop()
        {
            System.Diagnostics.Stopwatch watch;
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            bool acction = false;
            bool fastUp=false;

            board.showBrick(b);
            gamePrint();
            board.hideBrick(b);
            while (true)
            {
               
                if (Console.KeyAvailable)
                {
                    if (acction)
                    {
                        acction = false;
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.Q:
                                tryRotate(false);
                                break;
                            case ConsoleKey.E:
                                tryRotate(true);
                                break;
                            case ConsoleKey.A:
                                tryMove(true);
                                break;
                            case ConsoleKey.D:
                                tryMove(false);
                                break;
                            case ConsoleKey.S:
                                fastUp = true;
                                break;
                            default:
                                acction = true;
                                break;
                        }
                    }
                    else Console.ReadKey();
                }
                if (watch.ElapsedMilliseconds > 1000 || (fastUp && watch.ElapsedMilliseconds > 100))
                {
                    fastUp = false;
                    watch.Restart();
                    acction = true;
                    if (!tryFall())
                    {
                        
                        board.showBrick(b);
                        findAndEraseFullLines();
                        b = nextBricks[0];

                        if (!board.NotColision(b.Shape, b.PosX, b.PosY))  //endgame
                        {
                            board.showBrick(b);
                            Console.WriteLine("\nGAME OVER");
                            endGameAnimation();
                            board.DeepClear();
                            stats.Points = 0;
                        }
                        for (int i = 0; i < 4; ++i)
                        {
                            nextBricks[i] = nextBricks[i + 1];
                        }
                        nextBricks[4] = new Brick();
                    }
                    board.showBrick(b);
                    gamePrint();
                    board.hideBrick(b);
                    while(Console.KeyAvailable) Console.ReadKey(); //czyszczenie FastUp'a
                }
            }
        }
        private void gamePrint()
        {
            Console.Clear();
            int currentComponentLenght = stats.NumOfLines;
            int currentComponentCounter = 0;
            int currentComponentID = -1;
            int maxComponentID = 5;


            for (int i = 0; i < board.Array.GetLength(0); ++i)
            {
                //wypisywanie
                for (int j = 0; j < board.Array.GetLength(1); ++j)
                {
                    Console.Write($"{board.Array[i, j]}");
                }
                //------------------
                // dodatki
                if (currentComponentID < maxComponentID)
                {
                    if (currentComponentID == -1)
                    {
                        Console.Write(stats.GetLine(currentComponentCounter++));
                    }
                    else
                    {
                        if (currentComponentCounter < 0)
                        {
                            if (currentComponentCounter == -1)
                            {
                                Console.Write($" {currentComponentID + 1}:");
                            }
                        }
                        else
                        {
                            Console.Write("  ");
                            for (int j = 0; j < nextBricks[currentComponentID].Shape.GetLength(1); ++j)
                            {
                                Console.Write((nextBricks[currentComponentID].Shape[currentComponentCounter, j] == 1) ? nextBricks[currentComponentID].Color : new char());
                            }
                        }
                        currentComponentCounter++;
                    }
                    if (currentComponentCounter > currentComponentLenght - 1)
                    {
                        currentComponentID++;
                        if ((currentComponentID < maxComponentID))
                        {
                            currentComponentLenght = nextBricks[currentComponentID].Shape.GetLength(0);
                            currentComponentCounter = -1; //<---------- 
                            //-2 -> odstep jednego entera
                            //-1 -> brak odstepu
                        }
                    }
                }
                Console.Write('\n');
            }
        }
        
        private bool tryFall()
        {
            if (board.NotColision(b.Shape,  b.PosX , b.PosY+1))
            {
                b.PosY++;
                return true;
            }
            return false;
        }
        private bool tryMove(bool left)
        {
            if (board.NotColision(b.Shape, left ? b.PosX-1 : b.PosX+1, b.PosY))
            {
                if (left) b.PosX--; else b.PosX++;
                return true;
            }
            return false;
            
        }
        private bool tryRotate(bool rotateDirection)
        {
            if (board.NotColision(b.Rotate(rotateDirection), b.PosX, b.PosY))
            {
                b.Shape = b.Rotate(rotateDirection);
                return true;
            }
            return false;
        }
        private bool lineIsFull(int i)
        {
            int j = 1;
            while (board.Array[i, j] != 'H')
            {
                if (board.Array[i, j] == new char()) return false;
                j++;
            }
            return true;
        }
        private void findAndEraseFullLines()
        {
            for (int i = 0; i < board.Array.GetLength(0) - 1;)
            {
                if (lineIsFull(i))
                {
                    stats.Points += 100;
                    animation(i);
                    board.fallAfterErase(i);
                }
                else i++;
            }
        }
        
        private void animation(int i)
        {
            char[] array = new char[board.Array.GetLength(1) - 2];
            for (int j = 0; j < array.Length; ++j)
            {
                array[j] = board.Array[i, j + 1];
                board.Array[i, j + 1] = 'X';
            }
            System.Threading.Thread.Sleep(700);
            gamePrint();
            for (int j = 0; j < array.Length; ++j)
            {
                board.Array[i, j + 1] = array[j];
            }
            System.Threading.Thread.Sleep(700);
            gamePrint();
            System.Threading.Thread.Sleep(700);
        }
        private void endGameAnimation()
        {
            System.Threading.Thread.Sleep(3000);
        }
    }
}
