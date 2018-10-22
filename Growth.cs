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


    public static void Moore(int i, int j, Grain[,] grainTable, Grain[,] tempGrainTable, int xNumOfCells, int yNumOfCells)
        {
            if (grainTable[i, j].State == 0)
            {
                List<Grain> neighbours = new List<Grain>();
                for (int k = i - 1; k < i + 2; k++)
                {
                    for (int l = j - 1; l < j + 2; l++)
                    {
                        if(k>=0 && k<xNumOfCells && l>=0 && l<yNumOfCells)
                        {
                            if (grainTable[k, l].State == 1)
                            {
                                neighbours.Add(grainTable[k, l]);
                            }
                        }
                        
                        
                    }
                }
                if (neighbours.Any())
                {
                    Color c= neighbours.GroupBy(y => y.Color).OrderByDescending(y => y.Count()).First().Key;
                    Grain g = neighbours.Find(x => x.Color == c);
                    tempGrainTable[i, j].Rect.Fill = g.Rect.Fill;
                    tempGrainTable[i, j].Rect.Stroke = g.Rect.Stroke;
                    tempGrainTable[i, j].Color = g.Color;
                    tempGrainTable[i, j].State = g.State;
                }
                
            }
            else if(grainTable[i,j].State==1)
            {
                tempGrainTable[i, j].Rect.Fill = grainTable[i, j].Rect.Fill;
                tempGrainTable[i, j].Rect.Stroke = grainTable[i, j].Rect.Stroke;
                tempGrainTable[i, j].Color = grainTable[i, j].Color;
                tempGrainTable[i, j].State = grainTable[i, j].State;
            }
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
