using MultiscaleModelingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiscaleModelingApp
{
    public static class Regrowth
    {
        public static void Substructure(List<Grain> grains)
        {
            List<Color> ids = new List<Color>();
            foreach(Grain g in grains)
            {
                if(!ids.Contains(g.Color))
                {
                    ids.Add(g.Color);
                }
            }
            foreach(Grain g in MainWindow.GrainTable)
            {
                if (!g.Inclusion && !ids.Contains(g.Color))
                {
                    g.MakeEmpty();
                }
                if(g.State!=0) g.Inclusion = true;
                
            }
        }
        public static void DualPhase(List<Grain> grains)
        {
            List<Grain> ChangedGrains = new List<Grain>();
            foreach (Grain g in grains)
            {
                ChangedGrains.Add(g);
                ChangedGrains.AddRange(ListOfNeighbours(g));
            }
            ChangedGrains = ChangedGrains.Distinct().ToList();
            Random rand = new Random();
            Color c = Color.FromRgb(Convert.ToByte(rand.Next(255)), Convert.ToByte(rand.Next(255)), Convert.ToByte(rand.Next(255)));
            foreach (Grain g in MainWindow.GrainTable)
            {
                if (!g.Inclusion && !ChangedGrains.Contains(g))
                {
                    g.MakeEmpty();
                }
                if (g.State != 0 && g.Inclusion==false)
                {
                    g.Color = c;
                    g.Rect.Fill = new SolidColorBrush(c);
                    g.Rect.Stroke = new SolidColorBrush(c);
                    g.Inclusion = true;
                }

            }
        }
        public static List<Grain> ListOfNeighbours(Grain g)
        {
            List<Grain> listneigbours = new List<Grain>();
            List<Grain> neigbours = new List<Grain>();
            int i = g.X;
            while(i < MainWindow.XNumOfCells&&MainWindow.GrainTable[i, g.Y].Color == g.Color )
            {
                neigbours.Add(MainWindow.GrainTable[i, g.Y]);
                i++;
            }
            i = g.X - 1;
            while (i>=0 && MainWindow.GrainTable[i, g.Y].Color == g.Color )
            {
                neigbours.Add(MainWindow.GrainTable[i, g.Y]);
                i--;
            }
            listneigbours.AddRange(neigbours);
            List<Grain> neigbourstemp = new List<Grain>();
            do
            {
                foreach (Grain grain in neigbours)
                {
                    for(int j = grain.Y - 1; j < grain.Y + 2; j += 2)
                    {
                        if (j >= 0 && j < MainWindow.YNumOfCells && MainWindow.GrainTable[grain.X, j].Color == grain.Color)
                        {
                            neigbourstemp.Add(MainWindow.GrainTable[grain.X, j]);
                        }
                    }
                    for (int j = grain.X - 1; j < grain.X + 2; j += 2)
                    {
                        if (j >= 0 && j < MainWindow.XNumOfCells && MainWindow.GrainTable[j,grain.Y].Color == grain.Color)
                        {
                            neigbourstemp.Add(MainWindow.GrainTable[j,grain.Y]);
                        }
                    }


                }
                neigbourstemp.RemoveAll(x => listneigbours.Contains(x));
                neigbours.Clear();
                neigbours.AddRange(neigbourstemp);
                listneigbours.AddRange(neigbours);
                neigbourstemp.Clear();
            } while (neigbours.Any());
            

            return listneigbours;
        }

    }
}
