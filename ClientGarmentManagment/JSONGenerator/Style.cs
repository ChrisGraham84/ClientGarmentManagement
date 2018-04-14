using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.JSONGenerator.Models
{
    public class Style
    {
        public string name { get; set; }
        public string stylenumber { get; set; }
        public List<Color> colors { get; set; }
        public List<Size> sizes { get; set; }
    }

    public class Color
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Size
    {
        public string name { get; set; }
    }
}
