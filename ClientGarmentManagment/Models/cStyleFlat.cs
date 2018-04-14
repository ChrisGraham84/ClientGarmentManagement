using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.Models
{
    public class cStyleFlat
    {
            public string Style_Number { get; set; }
            public string Gender { get; set; }
            public string Manufacturer { get; set; }
            public string Website_Description { get; set; }
            public string Detailed_Description { get; set; }
            public string Print_Positions { get; set; }
            public string Color_Description { get; set; }
            public string Hue { get; set; }
            public string Color_Type { get; set; }
            public string Underbase_NO_Override { get; set; }
            public string Size { get; set; }
            public string Catalog_Price { get; set; }
            public string Weight { get; set; }
            public string Min_Override { get; set; }
            public string Max_Hue { get; set; }

            public cStyleFlat() { }
    }
}
