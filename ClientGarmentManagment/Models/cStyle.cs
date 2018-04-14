using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.Models
{
    public class cStyle
    {
        public string ID { get; set; }
        public string Style_Number { get; set; }
        public string Gender { get; set; }
        public string Website_Description { get; set; }
        public int MaxHue { get; set; }
        public int MinOverride { get; set; }
        public string Detailed_Description { get; set; }
        public string Print_Positions { get; set; }
        public string Manufacturer { get; set; }
        public string Discontinued { get; set; }
        public List<cColorSize> ColorSize = null;

        public cStyle() { }
    }
}
