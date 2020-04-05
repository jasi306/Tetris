using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tetris
{
    class Stats
    {
        int top;
        int points;
        public int NumOfLines = 5;
        public int Points
        {
            get => points;
            set
            {
                points = value;
                if(points> top)
                {
                    top = points;
                    if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TetrisJana")))
                    {
                        Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TetrisJana"));
                    }
                    File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TetrisJana/Tetris.Tsave"), points.ToString());
                }
            }
        }
        public Stats(int top)
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TetrisJana")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TetrisJana"));
            }
            


            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TetrisJana/Tetris.Tsave")))
            {
                string lines = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TetrisJana/Tetris.Tsave"));
                this.top = Int32.Parse(lines);
            }
            else
                this.top = top;
            points = 0;
            
        }
        public string GetLine(int i)
        {
            switch (i)
            {
                case 0:
                    return "    TOP:";
                case 1:
                    return "    " + top;
                case 2:
                    return "";
                case 3:
                    return "    SCORE:";
                case 4:
                    return "    " + points;
                default:
                    return "";
            }
        }
    }
}
