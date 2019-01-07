using MultiscaleModelingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiscaleModelingApp
{
    public class Energy
    {
        public static void DistributeEnergy(int EnergyDistribution, int EnergyOnEgdes, int EnergyInside)
        {
            if (EnergyDistribution == 0)
            {
                int energy = (EnergyOnEgdes + EnergyInside) / 2;
                foreach (Grain g in MainWindow.GrainTable)
                {
                    g.EnergyColor = Color.FromRgb(255, (byte)(255 * energy / EnergyOnEgdes), 0);
                    g.H = energy;
                }
            }
            else
            {
                List<Grain> grainsOnEdges = Inclusions.GrainOnEdges(1, MainWindow.XNumOfCells, MainWindow.YNumOfCells);
                foreach(Grain g in MainWindow.GrainTable)
                {
                    if (grainsOnEdges.Contains(g))
                    {
                        g.EnergyColor = Color.FromRgb(255, (byte)(0), 0);
                        g.H = EnergyOnEgdes;
                    }
                    else
                    {
                        g.EnergyColor = Color.FromRgb(255, (byte)(255-255*EnergyInside/EnergyOnEgdes), 0);
                        g.H = EnergyInside;
                    }
                }
            }

        }
    }
}
