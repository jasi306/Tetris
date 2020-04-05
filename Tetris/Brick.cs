using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    class Brick

    {
        char color;
        public char Color
        {
            get => color;
        }
        int posY, posX;
        public int PosY
        {
            get => posY;
            set { posY = value; }
        }
        public int PosX
        {
            get => posX;
            set { posX = value; }
        }
        int[,] shape;
        public int[,] Shape
        {
            get => shape;
            set { shape = value; }
        }


        public Brick()
        {
            randType();
        }

        private void randType()
        {
            Random r = new Random();
            int type = r.Next(0, 7);
            //type = 0;
          
            posX = 5;
            posY = 1;
            switch (type)
            {
                case 0:  //LINE xxxx
                    color = 'l';
                    shape = new int[1, 4];
                    for (int i = 0; i < shape.GetLength(0); ++i)
                    {
                        for (int j = 0; j < shape.GetLength(1); ++j)
                        {
                            shape[i, j] = 1;
                        }
                    }
                    posY = 0;

                    break;
                case 1:  //squer xx
                         //      xx
                    color = 'o';
                    shape = new int[2, 2];
                    for (int i = 0; i < shape.GetLength(0); ++i)
                    {
                        for (int j = 0; j < shape.GetLength(1); ++j)
                        {
                            shape[i, j] = 1;
                        }
                    }
                    
                    break; 
                case 2:   // Z Shape  x
                          //         xx
                          //         x
                    color = 'z';
                    shape = new int[2, 3];
                    for (int i = 0; i < shape.GetLength(0); ++i)
                    {
                        for (int j = 0; j < shape.GetLength(1); ++j)
                        {
                            shape[i, j] = 1;
                        }
                    }
                    shape[0, 0] = 0;
                    shape[1, 2] = 0;
                    break;
                case 3:  // S Shape x
                         //         xx
                         //          x
                    color = 's';
                    shape = new int[2, 3];
                    for (int i = 0; i < shape.GetLength(0); ++i)
                    {
                        for (int j = 0; j < shape.GetLength(1); ++j)
                        {
                            shape[i, j] = 1;
                        }
                    }
                    shape[1, 0] = 0;
                    shape[0, 2] = 0;
                    break;
                case 4:   // L Shape xx
                          //          x
                          //          x
                    color = 'L';
                    shape = new int[2, 3];
                    for (int j = 0; j < shape.GetLength(1); ++j)
                    {
                        shape[0, j] = 1;
                    }
                    shape[1, 0] = 1; 
                    break;
                case 5:   // L Shape Mirror xx
                          //                x
                          //                x
                    color = 'F';
                    shape = new int[2, 3];
                    for (int j = 0; j < shape.GetLength(1); ++j)
                    {
                        shape[1, j] = 1;
                    }
                    shape[0, 0] = 1;
                    break;
                case 6:   // T Shape  x
                          //         xxx
                    color = 'T';
                    shape = new int[3, 2];
                    for (int i = 0; i < shape.GetLength(0); ++i)
                    {
                        shape[i, 1] = 1;
                    }
                    shape[1, 0] = 1;
                    break;
                default:
                    throw new Exception("blad konstruktora");
            }
        }

        public int[,] Rotate(bool clockDirection)
        {
            int[,] newShape = new int[shape.GetLength(1), shape.GetLength(0)];

            if (clockDirection)
                for (int i = 0; i < shape.GetLength(0); ++i)
                {
                    for (int j = 0; j < shape.GetLength(1); ++j)
                    {
                        newShape[j, i] = shape[i, shape.GetLength(1) - j - 1];
                    }
                }
            else
                for (int i = 0; i < shape.GetLength(0); ++i)
                {
                    for (int j = 0; j < shape.GetLength(1); ++j)
                    {
                        newShape[j, i] = shape[shape.GetLength(0) - i - 1, j];
                    }
                }
            return newShape;
        }
    }
}
