using MultiscaleModelingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiscaleModelingApp
{
    public class Inclusions
    {
        public static void Circular(int amount, int size, int xNumOfCells, int yNumOfCells)
        {


        }

        public static void Diagonal(int amount, int size, int xNumOfCells, int yNumOfCells)
        {

        }
        private bool AllTableFilled()
        {
            bool filled = true;
            foreach (Grain g in MainWindow.GrainTable)
            {
                if (g.State == 0)
                {
                    filled = false;
                    return filled;
                }
            }
            return filled;
        }
        private List<Grain> ListOfInslusions(int amount, int xNumOfCells, int yNumOfCells)
        {
            List<Grain> inclusions = new List<Grain>();
            if (AllTableFilled())
            {
                //inclusion
            }
            else
            {
                Random random = new Random();
                for (int i = 0; i < amount; i++)
                {
                    int x = random.Next(xNumOfCells);
                    int y = random.Next(yNumOfCells);
                    inclusions.Add(MainWindow.GrainTable[x, y]);
                }
            }
            return inclusions;
        }
        private List<Grain> GrainOnEdges(int amount, int xNumOfCells, int yNumOfCells)
        {
            List<Grain> edgesGrains = new List<Grain>();
            foreach (Grain g in MainWindow.GrainTable)
            {
                if (IsOnTheEdge(g, xNumOfCells, yNumOfCells))
                {
                    edgesGrains.Add(g);
                }
            }
            return edgesGrains;
        }
        private bool IsOnTheEdge(Grain g, int xNumOfCells, int yNumOfCells)
        {
            Color color = g.Color;
            for (int i = g.X - 1; i < g.X + 1; i++)
            {
                for (int j = g.Y - 1; j < g.Y + 1; j++)
                {
                    if(i>=0 && j>0 && i<xNumOfCells && j<yNumOfCells)
                    {
                        if (color != MainWindow.GrainTable[i, j].Color)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
