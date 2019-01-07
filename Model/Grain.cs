using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;
using System.Windows.Shapes;

namespace MultiscaleModelingApp.Model
{
    public class Grain
    {

        public Color Color { get; set; }
        public int State { get; set; } = 0;
        public int H { get; set; } = 0;
        public int X { get; set; }
        public int Y { get; set; }
        public Rectangle Rect{ get; set; }
        public bool Inclusion { get; set; } = false;
        public string id { get; set; }
        public Color EnergyColor { get; set; }
        public bool Recrystalized = false;
        public Grain(int x, int y,int width, int height)
        {
            X = x;
            Y = y;
            Color = Color.FromRgb(255, 255, 255);
            Rect = new Rectangle();
            Rect.Fill = new SolidColorBrush(Color);
            Rect.Stroke = new SolidColorBrush(Color);
            Rect.Width = width;
            Rect.Height = height;
            id = X.ToString() + "." + Y.ToString();
        }

        public void SetInclusion()
        {
            Inclusion = true;
            State = 1;
            Color = Color.FromRgb(0, 0, 0);
            Rect.Fill = new SolidColorBrush(Color);
            Rect.Stroke = new SolidColorBrush(Color);
        }

        public Grain(int x, int y, int width, int height,Color c)
        {
            X = x;
            Y = y;
            Color = c;
            Rect = new Rectangle();
            Rect.Fill = new SolidColorBrush(c);
            Rect.Stroke = new SolidColorBrush(c);
            Rect.Width = width;
            Rect.Height = height;
        }

        public void Seed(Random Rand)
        {

            Color = Color.FromRgb(Convert.ToByte(Rand.Next(255)), Convert.ToByte(Rand.Next(255)), Convert.ToByte(Rand.Next(255)));
            Rect.Fill = new SolidColorBrush(Color);
            Rect.Stroke = new SolidColorBrush(Color);
            State = 1;

        }
        public void Seed(Color color)
        {

            Color = color;
            Rect.Fill = new SolidColorBrush(color);
            Rect.Stroke = new SolidColorBrush(color);
            State = 1;
        }
        public void MakeEmpty()
        {
            Color = Color.FromRgb(255, 255, 255);
            Rect.Fill = new SolidColorBrush(Color);
            Rect.Stroke = new SolidColorBrush(Color);
            State = 0;

        }
        public Grain(Grain g)
        {
            X = g.X;
            Y = g.Y;
            Color = g.Color;
            State = g.State;
            Rect = g.Rect;
            H = g.H;
            Inclusion = g.Inclusion;
            

        }
        public static List<Grain> NumberOfFeeeCells(List<Grain> grainEgdes)
        {
            List<Grain> freeGrains = new List<Grain>();
            foreach(Grain g in MainWindow.GrainTable)
            {
                if (grainEgdes!=null)
                {
                    if (g.State == 0 )
                    {
                        freeGrains.Add(g);
                    }
                }
                else
                {
                    if (g.State == 0)
                    {
                        freeGrains.Add(g);
                    }
                }
                
            }
            freeGrains = MonteCarlo.ShuffleList(freeGrains);
            return freeGrains;

        }

    }
}
