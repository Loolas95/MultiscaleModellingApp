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
        public int X { get; set; }
        public int Y { get; set; }
        public Rectangle Rect{ get; set; }
        public Grain(int state, int width, int height)
        {
            Color = Color.FromRgb(255, 255, 255);
            Rect = new Rectangle();
            Rect.Fill = new SolidColorBrush(Color);
            Rect.Width = width;
            Rect.Height = height;
        }

        public void Seed()
        {
            Random rand = new Random();
            Color = Color.FromRgb(Convert.ToByte(rand.Next(255)), Convert.ToByte(rand.Next(255)), Convert.ToByte(rand.Next(255)));
            State = 1;
        }
    }
}
