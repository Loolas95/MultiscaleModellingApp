using MultiscaleModelingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiscaleModelingApp
{
    public class Boundaries
    {
        public static void AllBoundaries(int width,int xNumOfCells,int yNumOfCells)
        {
            List<Grain> grainsOnEdges = Inclusions.GrainOnEdges(width, xNumOfCells, yNumOfCells);
            DrawEdges(grainsOnEdges);
        }
        public static void SelectedBoundaries(List<Grain> grains,int width, int xNumOfCells, int yNumOfCells)
        {
            MainWindow.grain2Edge = getAllSelectedGrains(grains);
            List<Grain> grainsOnEdges = Inclusions.GrainOnEdges(MainWindow.grain2Edge, width, xNumOfCells, yNumOfCells);
            DrawEdges(grainsOnEdges);
        }
        public static void DrawEdges(List<Grain> grains)
        {
            foreach(Grain g in grains)
            {
                g.SetInclusion();
            }
        }
        private static List<Grain> getAllSelectedGrains(List<Grain> grains)
        {
            List<Color> colors = new List<Color>();
            foreach(Grain g in grains)
            {
                colors.Add(g.Color);
            }
            List<Grain> selectedGrains = new List<Grain>();
            foreach(Grain g in MainWindow.GrainTable)
            {
                if(colors.Contains(g.Color))
                {
                    selectedGrains.Add(g);
                }
            }
            return selectedGrains;
        }

        public static void ClearSpace(int xNumOfCells, int yNumOfCells)
        {
            foreach(Grain g in MainWindow.GrainTable)
            {
                if(!g.Inclusion==true)
                {
                    g.MakeEmpty();
                }
            }
            Growth.Replace(MainWindow.TempGrainTable, MainWindow.GrainTable, xNumOfCells, yNumOfCells);
            
        }
    }
}
