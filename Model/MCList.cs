using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiscaleModelingApp.Model
{
    public class MCList
    {
        public int Energy { get; set; }
        public List<Color> Colorlist { get; set; }
        public MCList(int energy, List<Color> colors)
        {
            Energy = energy;
            Colorlist = colors;
        }
    }
}
