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
            List<Grain> inclusions = ListOfInclusions(amount, xNumOfCells, yNumOfCells);
            foreach(Grain maingrain in MainWindow.GrainTable)
            {
                foreach(Grain inclugrain in inclusions)
                {
                    if(Math.Sqrt(Math.Pow((maingrain.X-inclugrain.X),2)+ Math.Pow((maingrain.Y - inclugrain.Y), 2)) <=size/2)
                    {
                        maingrain.SetInclusion();
                    }
                }
            }

        }

        public static void Diagonal(int amount, int size, int xNumOfCells, int yNumOfCells)
        {
            List<Grain> inclusions = ListOfInclusions(amount, xNumOfCells, yNumOfCells);
            foreach (Grain inclugrain in inclusions)
            {
                DiagonalNeighbours(inclugrain, size, xNumOfCells, yNumOfCells);
            }
        }
        private static bool AllTableFilled()
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
        private static List<Grain> ListOfInclusions(int amount, int xNumOfCells, int yNumOfCells)
        {
            List<Grain> inclusions = new List<Grain>();
            Random random = new Random();
            if (AllTableFilled())
            {
                //inclusion
                List<Grain> templist = GrainOnEdges(1,xNumOfCells, yNumOfCells);
                for (int i = 0; i < amount; i++)
                {
                    if(amount<templist.Count)
                    {
                        Grain grain;
                        do
                        {
                            grain = templist.ElementAt(random.Next(templist.Count));
                        } while (inclusions.Contains(grain));
                        inclusions.Add(grain);
                    }
                }
            }
            else
            {
                
                for (int i = 0; i < amount; i++)
                {
                    if (amount < MainWindow.GrainTable.Length)
                    {
                        int x, y;
                        do
                        {
                            x = random.Next(xNumOfCells);
                            y = random.Next(yNumOfCells);
                        } while (inclusions.Contains(MainWindow.GrainTable[x, y]) && MainWindow.GrainTable[x, y].State == 0);

                        inclusions.Add(MainWindow.GrainTable[x, y]);
                    }
                    
                    
                }
            }
            return inclusions;
        }
        public static List<Grain> GrainOnEdges(int width,int xNumOfCells, int yNumOfCells)
        {
            List<Grain> edgesGrains = new List<Grain>();
            foreach (Grain g in MainWindow.GrainTable)
            {
                if (IsOnTheEdge(g,width, xNumOfCells, yNumOfCells))
                {
                    edgesGrains.Add(g);
                }
            }
            return edgesGrains;
        }
        public static List<Grain> GrainOnEdges(List<Grain> grains,int width, int xNumOfCells, int yNumOfCells)
        {
            List<Grain> edgesGrains = new List<Grain>();
            if (grains == null) return null;
            foreach (Grain g in grains)
            {
                if (IsOnTheEdge(g, width, xNumOfCells, yNumOfCells))
                {
                    edgesGrains.Add(g);
                }
            }
            return edgesGrains;
        }
        private static bool IsOnTheEdge(Grain g,int width, int xNumOfCells, int yNumOfCells)
        {
            Color color = g.Color;
            for (int i = g.X - width; i <= g.X + width; i++)
            {
                for (int j = g.Y - width; j <= g.Y + width; j++)
                {
                    if(i>=0 && j>=0 && i<xNumOfCells && j<yNumOfCells)
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
        private static void DiagonalNeighbours(Grain g, int size, int xNumOfCells, int yNumOfCells)
        {
            List<Grain> neighbours = new List<Grain>();
            for (int i = g.X - size / 2; i < g.X + size / 2; i++)
            {
                for (int j = g.Y - size / 2; j < g.Y + size / 2; j++)
                {
                    if(i>=0&&j>=0&&i<xNumOfCells&&j<yNumOfCells)
                    {
                        MainWindow.GrainTable[i, j].SetInclusion();
                    }
                }
            }
        }
    }
}
