using MultiscaleModelingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiscaleModelingApp
{
    public class RSXMC
    {
        public static void Nucleate(int number, int location)
        {
            Random rand = new Random();
            if (location == 0)
            {
                List<Grain> grainsOnEdges = Inclusions.GrainOnEdges(1, MainWindow.XNumOfCells, MainWindow.YNumOfCells);
                grainsOnEdges.RemoveAll(x => x.Recrystalized == true);
                
                for(int i = 0; i < number; i++)
                {
                    Grain g = grainsOnEdges.ElementAt(rand.Next(grainsOnEdges.Count));
                    g.Recrystalized = true;
                    Color color = Color.FromRgb((byte)(rand.Next(206)+50),0,0);
                    g.Seed(color);
                    g.EnergyColor = Color.FromRgb(255, 255, 255);
                    g.H = 0;
                    grainsOnEdges.Remove(g);
                }

            }
            else
            {
                for(int i = 0; i < number; i++)
                {
                    int x,y;
                    do
                    {
                        x = rand.Next(MainWindow.XNumOfCells);
                        y = rand.Next(MainWindow.YNumOfCells);
                    } while (MainWindow.GrainTable[x, y].Recrystalized);
                    MainWindow.GrainTable[x, y].Recrystalized = true;
                    Color color = Color.FromRgb((byte)(rand.Next(206) + 50), 0, 0);
                    MainWindow.GrainTable[x, y].Seed(color);
                    MainWindow.GrainTable[x, y].EnergyColor = Color.FromRgb(255, 255, 255);
                    MainWindow.GrainTable[x, y].H = 0;
                }
            }
        }
        public static List<Grain> NewRSXMCList()
        {
            List<Grain> RSXMCgrains = new List<Grain>();
            foreach(Grain g in MainWindow.GrainTable)
            {
                if (NeighbourRecrystalized(g)&&!g.Recrystalized)
                    RSXMCgrains.Add(g);
            }
            RSXMCgrains = RSXMCgrains.Distinct().ToList();
            RSXMCgrains = MonteCarlo.ShuffleList(RSXMCgrains);
            return RSXMCgrains;

        }
        private static bool NeighbourRecrystalized(Grain g)
        {
            int recrystalized=0;
            for(int i = g.X - 1; i < g.X + 2; i++)
            {
                for (int j = g.Y - 1; j < g.Y + 2; j++)
                {
                    if(j>=0 && i>=0 && i < MainWindow.XNumOfCells && j < MainWindow.YNumOfCells)
                    {
                        if (i == g.X && j == g.Y) continue;
                        if (MainWindow.GrainTable[i, j].Recrystalized) recrystalized++;
                    }


                }
            }
            if (recrystalized < 8 && recrystalized > 0) return true;
            return false;
        }
    }
}
