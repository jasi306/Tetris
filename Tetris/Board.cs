using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    
    class Board
    {
        public char[,] Array;
        public Board(int ySize, int xSize)
        {
            Array = new char[ySize, xSize];
            for (int i = 0; i < ySize; ++i)
            {
                Array[i, xSize - 1] = 'H';
                Array[i, 0] = 'H';
            }
            for (int i = 0; i < xSize; ++i)
            {
                Array[ySize - 1, i] = 'H';
            }
        }

        public void showBrick(Brick b)
        {
            int x = b.Shape.GetLength(0);
            int y = b.Shape.GetLength(1);
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    if (b.Shape[i, j] == 1)
                    {
                        Array[b.PosY - x / 2 + i, b.PosX - y / 2 + j] = b.Color;
                    }
                }
            }
        }


        public void hideBrick(Brick b)
        {
            int x = b.Shape.GetLength(0);
            int y = b.Shape.GetLength(1);
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    if (b.Shape[i, j] == 1)
                    {
                        Array[b.PosY - x / 2 + i, b.PosX - y / 2 + j] = new char();
                    }
                }
            }
        }
        public bool NotColision(int[,] objectArray, int PosX, int PosY)
        {
            int x = objectArray.GetLength(0);
            int y = objectArray.GetLength(1);
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    if (objectArray[i, j] == 1)
                    {
                        if (PosX - y / 2 + j < 0) return false;
                        if (Array[PosY - x / 2 + i, PosX - y / 2 + j] != new char()) return false;
                    }
                }
            }
            return true;
        }
        public void DeepClear()
        {
            int x = Array.GetLength(0) - 1;
            int y = Array.GetLength(1) - 1;
            for (int i = 0; i < x; ++i)
            {
                for (int j = 1; j < y; ++j)
                {
                    Array[i, j] = new char();
                }
            }
        }
        public void fallAfterErase(int i)
        {
            for (; i > 0; --i)
            {
                for (int j = 0; j < Array.GetLength(1); ++j)
                {
                    Array[i, j] = Array[i - 1, j];
                }
            }
        }
    }
}
