using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiscaleModelingApp.Model;
using System.Windows.Media;

namespace MultiscaleModelingApp
{
    public class Growth
    {


    public static bool Moore(int i, int j, int xNumOfCells, int yNumOfCells)
        {
            if (MainWindow.GrainTable[i, j].State == 0)
            {
                List<Grain> neighbours = new List<Grain>();
                for (int k = i - 1; k < i + 2; k++)
                {
                    for (int l = j - 1; l < j + 2; l++)
                    {
                        if(k>=0 && k<xNumOfCells && l>=0 && l<yNumOfCells)
                        {
                            if (MainWindow.GrainTable[k, l].State == 1 && !MainWindow.GrainTable[k,l].Inclusion)
                            {
                                neighbours.Add(MainWindow.GrainTable[k, l]);
                            }
                        }
                    }
                }
                if (neighbours.Any())
                {
                    var c = neighbours.GroupBy(y => y.Color).Where(x => x.Count() > 5).FirstOrDefault();
                    //var c = neighbours.GroupBy(y => y.Color).OrderByDescending(x => x.Count()).First();
                    if (c != null)
                    {
                        Grain g = neighbours.Find(x => x.Color == c.Key);
                        SetValues2TempGrain(g, i, j);
                        return true;
                    }                    
                }
                return false;
            }
            else
            {
                SetValues2TempGrain(MainWindow.GrainTable[i, j], i, j);
                return true;
            }
        }
        public static bool NearestMoore(int i, int j, int xNumOfCells, int yNumOfCells)
        {
            if (MainWindow.GrainTable[i, j].State == 0)
            {
                List<Grain> neighbours = new List<Grain>();
                for (int k = i - 1; k < i + 2; k+=2)
                {
                        if (k >= 0 && k < xNumOfCells)
                        {
                            if (MainWindow.GrainTable[k, j].State == 1 && !MainWindow.GrainTable[k, j].Inclusion)
                            {
                                neighbours.Add(MainWindow.GrainTable[k, j]);
                            }
                        }
                }
                for (int k = j - 1; k < j + 2; k +=2)
                {
                    if (k >= 0 && k < yNumOfCells)
                    {
                        if (MainWindow.GrainTable[i, k].State == 1 && !MainWindow.GrainTable[i, k].Inclusion)
                        {
                            neighbours.Add(MainWindow.GrainTable[i, k]);
                        }
                    }
                }
                if (neighbours.Any())
                {
                    var c = neighbours.GroupBy(y => y.Color).Where(x => x.Count() > 3).FirstOrDefault();
                    if (c != null)
                    {
                        Grain g = neighbours.Find(x => x.Color == c.Key);
                        SetValues2TempGrain(g, i, j);
                        return true;
                    }
                }
                return false;
            }
            else
            {
                SetValues2TempGrain(MainWindow.GrainTable[i, j], i, j);
                return true;
            }
        }
        public static bool FutherMoore(int i, int j, int xNumOfCells, int yNumOfCells)
        {
            if (MainWindow.GrainTable[i, j].State == 0)
            {
                List<Grain> neighbours = new List<Grain>();
                for (int k = i - 1; k < i + 2; k+=2)
                {
                    for (int l = j - 1; l < j + 2; l+=2)
                    {
                        if (k >= 0 && k < xNumOfCells && l >= 0 && l < yNumOfCells)
                        {
                            if (MainWindow.GrainTable[k, l].State == 1 && !MainWindow.GrainTable[k, l].Inclusion)
                            {
                                neighbours.Add(MainWindow.GrainTable[k, l]);
                            }
                        }
                    }
                }
                if (neighbours.Any())
                {
                    var c = neighbours.GroupBy(y => y.Color).Where(x => x.Count() > 3).FirstOrDefault();
                    if (c != null)
                    {
                        Grain g = neighbours.Find(x => x.Color == c.Key);
                        SetValues2TempGrain(g, i, j);
                        return true;
                    }
                }
                return false;
            }
            else
            {
                SetValues2TempGrain(MainWindow.GrainTable[i, j], i, j);
                return true;
            }
        }
        public static bool RuleFour(int i, int j,  int xNumOfCells, int yNumOfCells)
        {
            if (MainWindow.GrainTable[i, j].State == 0)
            {
                Random random = new Random();
                if(random.Next(100)>MainWindow.Probability)
                {
                    List<Grain> neighbours = new List<Grain>();
                    for (int k = i - 1; k < i + 2; k++)
                    {
                        for (int l = j - 1; l < j + 2; l++)
                        {
                            if (k >= 0 && k < xNumOfCells && l >= 0 && l < yNumOfCells)
                            {
                                if (MainWindow.GrainTable[k, l].State == 1 && !MainWindow.GrainTable[k, l].Inclusion)
                                {
                                    neighbours.Add(MainWindow.GrainTable[k, l]);
                                }
                            }
                        }
                    }
                    if (neighbours.Any())
                    {
                        var c = neighbours.GroupBy(y => y.Color).OrderByDescending(x => x.Count()).FirstOrDefault();
                        if (c != null)
                        {
                            Grain g = neighbours.Find(x => x.Color == c.Key);
                            SetValues2TempGrain(g, i, j);
                            return true;
                        }
                    }
                    
                }
                return false;
                
            }
            else
            {
                SetValues2TempGrain(MainWindow.GrainTable[i, j], i, j);
                return true;
            }
        }
        private static void SetValues2TempGrain(Grain g,int i,int j)
        {
            MainWindow.TempGrainTable[i, j].Rect.Fill = g.Rect.Fill;
            MainWindow.TempGrainTable[i, j].Rect.Stroke = g.Rect.Stroke;
            MainWindow.TempGrainTable[i, j].Color = g.Color;
            MainWindow.TempGrainTable[i, j].State = g.State;
            MainWindow.TempGrainTable[i,j].Inclusion = g.Inclusion;
        }

        public  static void Replace(Grain[,] grainTable, Grain[,] tempGrainTable, int xNumOfCells, int yNumOfCells)
        {
            for (int i = 0; i < xNumOfCells; i++)
            {
                for (int j = 0; j < yNumOfCells; j++)
                {
                    grainTable[i, j].Rect.Fill = tempGrainTable[i, j].Rect.Fill;
                    grainTable[i, j].Rect.Stroke = tempGrainTable[i, j].Rect.Stroke;
                    grainTable[i, j].Color = tempGrainTable[i, j].Color;
                    grainTable[i, j].State = tempGrainTable[i, j].State;
                }
            }
        }
    }
}
