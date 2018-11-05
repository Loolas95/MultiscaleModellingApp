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
            List<Color> ids = new List<Color>();
            foreach (Grain g in grains)
            {
                if (!ids.Contains(g.Color))
                {
                    ids.Add(g.Color);
                }
            }
            Random rand = new Random();
            Color c = Color.FromRgb(Convert.ToByte(rand.Next(255)), Convert.ToByte(rand.Next(255)), Convert.ToByte(rand.Next(255)));
            foreach (Grain g in MainWindow.GrainTable)
            {
                if (!g.Inclusion && !ids.Contains(g.Color))
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
    }
}
