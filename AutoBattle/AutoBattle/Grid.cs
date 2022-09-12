using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLength;
        public int yLength;

        public Grid(int Lines, int Columns)
        {
            xLength = Lines;
            yLength = Columns;
            Console.WriteLine("The battlefield has been created!\n");
            for (int i = 0; i < Lines; i++)
            {
                //   grids.Add(newBox);
                for (int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    //    Console.Write($"{newBox.Index}\n"); 
                    grids.Add(newBox);
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield(int Lines, int Columns)
        {
            Console.ResetColor();
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox currentgrid = GetGridBox(j, i);
                    if (currentgrid.occupied)
                    {
                        //if()
                        //     Console.Write("[X]\t");
                        Program.WriteColor("[{X}]\t", ConsoleColor.Yellow, ConsoleColor.White, false);
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }

                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        public GridBox GetGridBox(int row, int column)
        {
            GridBox gridBox = new GridBox();
            for (int i = 0; i < grids.Count; i++)
            {
                if (grids[i].xIndex == row && grids[i].yIndex == column)
                {
                    gridBox = grids[i];
                    return gridBox;
                }
            }

            return gridBox;
        }
    }
}