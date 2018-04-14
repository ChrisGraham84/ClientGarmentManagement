using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.Models
{
    public class cColorSize
    {
        public string Catalog_Price { get; set; }
        public cColor color;
        public cSize size;

        public cColorSize() { }
    }
}
