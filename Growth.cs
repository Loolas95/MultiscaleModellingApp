using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiscaleModelingApp.Model;

namespace MultiscaleModelingApp
{
    public class Growth
    {


    public static void Moore(int i, int j, Grain[,] grainTable, Grain[,] tempGrainTable, int xNumOfCells, int yNumOfCells)
        {
            if (grainTable[i, j].State == 1)
            {
                for (int k = i - 1; k < i + 2; k++)
                {
                    for (int l = j - 1; l < j + 2; l++)
                    {
                        if(k>=0 && k<xNumOfCells && l>=0 && l<yNumOfCells)
                        {
                            if (tempGrainTable[k, l].State == 0)
                            {
                                tempGrainTable[k, l].Rect.Fill = grainTable[i, j].Rect.Fill;
                                tempGrainTable[k, l].Rect.Stroke = grainTable[i, j].Rect.Stroke;
                                tempGrainTable[k, l].Color = grainTable[i, j].Color;
                                tempGrainTable[k, l].State = grainTable[i, j].State;
                            }
                        }
                    }
                }
            }
            else if(tempGrainTable[i,j].State==0)
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
