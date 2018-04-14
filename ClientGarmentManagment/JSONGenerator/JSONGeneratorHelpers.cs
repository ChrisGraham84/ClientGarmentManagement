using ClientGarmentManagment.JSONGenerator.Models;
using ClientGarmentManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.JSONGenerator
{
    public static class JSONGeneratorHelpers
    {
        
        public static Wrapper GetObjectsForJSON(List<cStyle> Styles, List<cDesign> Designs)
        {
            return (new Wrapper { styles = CreateStyles(Styles), designs = CreateDesigns(Designs) });
        } 

        public static List<Style> CreateStyles (List<cStyle> Styles)
        {
            List<Style> jstyles = new List<Style>();


            foreach(var style in Styles)
            {
                Style jstyle = new Style();
                jstyle.name = style.Detailed_Description;
                jstyle.stylenumber = style.Style_Number;
                jstyle.colors = new List<Color>();
                jstyle.sizes = new List<Size>();
                //grab colors
                var colors = style.ColorSize.Select(x => x.color).Distinct();
                var sizes = style.ColorSize.Select(x => x.size).Distinct();
                foreach(var color in colors)
                {
                    Color jcolor = new Color();
                    jcolor.name = color.Color_Description;
                    jcolor.value = color.Hex;
                    jstyle.colors.Add(jcolor);
                }
                //grab styles
                foreach(var size in sizes)
                {
                    Size jsize = new Size();
                    jsize.name = size.Size_Description;
                    jstyle.sizes.Add(jsize);
                }
                jstyles.Add(jstyle);
            }

            return (jstyles); 
                     
        }
        public static List<Design> CreateDesigns(List<cDesign> Designs)
        {
            return (Designs.Select(x => new Design { name = x.Design_Description, designid = x.Design_ID + "A", printposition = x.Print_Location_Description }).ToList());
        }
        public static Customer CreateCustomer(cCustomer Customer)
        {
            return (new Customer { name = Customer.Company_Name, companyID = Customer.Company_ID });
        }
    }
}
