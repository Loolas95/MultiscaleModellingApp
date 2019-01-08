using MultiscaleModelingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiscaleModelingApp
{
    public class MonteCarlo
    {
        public static List<Color> possibleColors { get; set; }
        public static Random rand = new Random();
        public static void GenerateListOfColors(int Q)
        {
            possibleColors = new List<Color>();
            for(int i = 0; i < Q; i++)
            {
                possibleColors.Add(Color.FromRgb(Convert.ToByte(rand.Next(255)), Convert.ToByte(rand.Next(255)), Convert.ToByte(rand.Next(255))));
            }
        }
        public static void GenerateGrains()
        {
            
            foreach(Grain g in MainWindow.grainMCList)
            {
                Color color = possibleColors.ElementAt(rand.Next(possibleColors.Count));
                g.Seed(color);
            }
        }
        public static void ChangeGrains()
        {

            Grain g = MainWindow.grainMCList.ElementAt(0);
            MCList neighbours= CalculateEnergy(g, g.Color);
            int EBefore = neighbours.Energy;

            if (EBefore > 0)
            {
                List<Color> neighbourColors = neighbours.Colorlist;
                Color newColor = neighbourColors.ElementAt(rand.Next(neighbourColors.Count));
                int EAfter = CalculateEnergy(g, newColor).Energy;
                if (EBefore >= EAfter)
                {
                    MainWindow.GrainTable[g.X, g.Y].Seed(newColor);
                    g.Seed(newColor);
                }
            }
                
            MainWindow.grainMCList.Remove(g);
        }
        public static void ChangeRSXGrains()
        {

            Grain g = MainWindow.RSXMCList.ElementAt(0);
            MCList neighbours = CalculateEnergy(g, g.Color);
            int EBefore = neighbours.Energy+g.H;

            if (EBefore > 0)
            {
                List<Color> neighbourColors = neighbours.Colorlist;
                if (neighbourColors.Count > 0)
                {
                    Color newColor = neighbourColors.ElementAt(rand.Next(neighbourColors.Count));
                    int EAfter = CalculateEnergy(g, newColor).Energy;
                    if (EBefore > EAfter)
                    {
                        MainWindow.GrainTable[g.X, g.Y].Seed(newColor);
                        MainWindow.GrainTable[g.X, g.Y].EnergyColor = Color.FromRgb(255, 255, 255);
                        MainWindow.GrainTable[g.X, g.Y].H = 0;
                        MainWindow.GrainTable[g.X, g.Y].Recrystalized = true;
                        g.Seed(newColor);
                        g.EnergyColor = Color.FromRgb(255, 255, 255);
                        g.H = 0;
                        g.Recrystalized = true;
                    }
                }
                else
                {
                    int a = 0;
                    a = 5;
                }
                
            }

            MainWindow.RSXMCList.Remove(g);
        }
        public static List<Grain> ShuffleList(List<Grain> grains)
        {
            int i = grains.Count;
            while (i > 1)
            {
                i--;
                int k = rand.Next(i + 1);
                Grain tmp = grains[k];
                grains[k] = grains[i];
                grains[i] = tmp;
            }
            return grains;
        }
        public static MCList CalculateEnergy(Grain g,Color c)
        {
            List<Color> energyColors = new List<Color>();
            List<Color> colorlist = new List<Color>();
            int i = g.X;
            int j = g.Y;
            for (int k = i - 1; k < i + 2; k++)
            {
                for (int l = j - 1; l < j + 2; l++)
                {
                    if (k >= 0 && k < MainWindow.XNumOfCells && l >= 0 && l < MainWindow.YNumOfCells)
                    {
                        if (k == i && j == l) continue;
                        if (MainWindow.GrainTable[k, l].Inclusion) continue;
                        if (MainWindow.GrainTable[k, l].Color!=c)
                        {
                            energyColors.Add(MainWindow.GrainTable[k, l].Color);
                            if (!colorlist.Contains(MainWindow.GrainTable[k, l].Color))
                            {
                                colorlist.Add(MainWindow.GrainTable[k, l].Color);
                            }
                        }
                        
                    }
                }
            }
            if (energyColors.Count == 0 || colorlist.Count == 0)
            {
                Console.WriteLine("remove : " );
            }
            return new MCList(energyColors.Count, colorlist);
        }
        public static List<Color> NeighbourColors(Grain g)
        {
            List<Color> colorlist = new List<Color>();
            int i = g.X;
            int j = g.Y;
            for (int k = i - 1; k < i + 2; k++)
            {
                for (int l = j - 1; l < j + 2; l++)
                {
                    if (k >= 0 && k < MainWindow.XNumOfCells && l >= 0 && l < MainWindow.YNumOfCells)
                    {
                        if (MainWindow.GrainTable[k, l].Color != g.Color)
                        {
                            if (!colorlist.Contains(MainWindow.GrainTable[k, l].Color))
                            {
                                colorlist.Add(MainWindow.GrainTable[k, l].Color);
                            }
                        }
                    }
                }
            }
            return colorlist;
        }
        public static List<Grain> GetNewMCList()
        {
            List<Grain> list = new List<Grain>();
            foreach(Grain g in MainWindow.GrainTable)
            {
                if (Inclusions.IsOnTheEdge(g, 1, MainWindow.XNumOfCells, MainWindow.YNumOfCells))
                {
                    if(g.Inclusion==false)
                    {
                        list.Add(new Grain(g));
                    }
                        
                }
                
            }
            list = MonteCarlo.ShuffleList(list);
            return list;
        }
    }
}
