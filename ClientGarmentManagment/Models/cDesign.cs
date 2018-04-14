using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.Models
{
    public class cDesign
    {
        public string Company_ID { get; set; }
        public string Design_ID { get; set; }
        public string UB { get; set; }
        public string Design_Description { get; set; }
        public string Print_Location_Description { get; set; }
        public string Print_Cost { get; set; }
        public string Print_Hue_Cost { get; set; }
        public string Hang_Point { get; set; }
        public string Hang_Point_Back { get; set; }
        public string Design_Width { get; set; }
        public string Design_Height { get; set; }
        public string Back_Width { get; set; }
        public string Back_Height { get; set; }
        public string Filename_A { get; set; }
        public string Filename_A_Back { get; set; }
        

        public cDesign() { }
    }
}
