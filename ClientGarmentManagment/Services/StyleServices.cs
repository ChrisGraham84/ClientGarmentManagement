using ClientGarmentManagment.Helpers;
using ClientGarmentManagment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.Services
{
    public static class StyleServices
    {
        public static List<cStyle> GetStyles(List<cStyleFlat> FlatData)
        {
            var grpStyle = FlatData.GroupBy(x => x.Style_Number);

            List<cStyle> styles = new List<cStyle>();
            //int maxhue = 0;
            //int minoverride = 1;
            foreach (var grp in grpStyle)
            {
                var colors = GetColors(FlatData,grp.Key);
                var sizes = GetSizes(FlatData, grp.Key);
                cStyle style = grp.Select(x => new cStyle
                {
                    Style_Number = x.Style_Number,
                    Detailed_Description = x.Detailed_Description,
                    Gender = x.Gender,
                    Manufacturer = x.Manufacturer,
                    Print_Positions = x.Print_Positions
                ,
                    ColorSize = new List<cColorSize>(),
                    Website_Description = x.Website_Description,
                    MaxHue = int.Parse(x.Max_Hue),
                    MinOverride = int.Parse(x.Min_Override)
                }).First();
                foreach (var color in colors)
                {
                    foreach (var size in sizes)
                    {
                        var q = grp.Where(x => x.Color_Description == color.Color_Description && x.Size == size.Size_Description).Select(x => x.Catalog_Price).FirstOrDefault();
                        if (q != null)
                        {
                           
                            cColorSize colorsize = new cColorSize { Catalog_Price = q, color = color, size = size };
                            style.ColorSize.Add(colorsize);
                        }
                    }
                }
                styles.Add(style);

            }
            return (styles);
        }
        public static List<cColor> GetColors(List<cStyleFlat> FlatData, string StyleNumber)
        {
            var stylecolor = FlatData.Where(x => x.Style_Number == StyleNumber);
            List<cColor> colors = new List<cColor>();
            foreach (var grp in stylecolor.GroupBy(x=>x.Color_Description))
            {
                var clr = grp.FirstOrDefault();
                cColor color = new cColor
                {
                    shirt_style_number = clr.Style_Number,
                    Color_Description = clr.Color_Description,
                    Color_Type = clr.Color_Type,
                    Hue = clr.Hue,
                    Underbase_NO_Override = clr.Underbase_NO_Override,
                    Hex = (Globals.ColorHexDictionary.ContainsKey(clr.Color_Description) ? Globals.ColorHexDictionary[clr.Color_Description] : "#000000")
                };
                colors.Add(color);
            }
            return (colors.OrderBy(x => x.Color_Description).ToList());
        }
        public static List<cSize> GetSizes(List<cStyleFlat> FlatData, string StyleNumber)
        {
            var stylesizes = FlatData.Where(x=>x.Style_Number == StyleNumber);
            List<cSize> sizes = new List<cSize>();

            foreach (var grp in stylesizes.GroupBy(x=>x.Size))
            {
                var sze = grp.FirstOrDefault();
                cSize size = new cSize
                {
                    Shirt_Style_Number = sze.Style_Number,
                    Size_Description = sze.Size,
                    Weight = sze.Weight
                };
                sizes.Add(size);
            }
            return (sizes);
        }
        public static void GenerateShopifyStyleSnippits(List<cStyle> Styles)
        {
            foreach (var st in Styles)
            {
                //start building string
                StringBuilder sb = new StringBuilder();
                //open div Size
                sb.Append("<div>\n");
                sb.Append("\t<p>Size</p>\n");
                sb.Append("\t<select class=\"styled\" name=\"properties[Size]\">\n");
                var sizes = st.ColorSize.Select(x => x.size.Size_Description).Distinct().ToList();
                foreach (var sz in sizes)
                {
                    sb.Append("\t\t<option value=\"" + sz + "\">" + sz + "</option>\n");
                }
                sb.Append("\t</select>\n");

                //close div Size
                sb.Append("</div>\n\n");

                var colors = st.ColorSize.GroupBy(x => x.color);

                //Some some style divs
                sb.Append("<div style=\"clear:both\"/>\n");
                sb.Append("<div style=\"margin:10px\"/>\n\n");

                //open div Color
                sb.Append("<div>\n");
                sb.Append("<p>Color</p>\n");

                sb.Append("<label>\n");
                int count = 0;
                foreach (var color in colors)
                {
                    var clr = color.FirstOrDefault().color;
                    if (count == 0)
                    {
                        sb.AppendFormat("\t<input type=\"radio\" id=\"rdoFirstColor\" name=\"properties[Color]\" value=\"{0}\"/>\n", clr.Color_Description);
                    }
                    else
                    {
                        sb.AppendFormat("\t<input type=\"radio\" name=\"properties[Color]\" value=\"{0}\"/>\n", clr.Color_Description);
                    }
                    sb.Append("<div style='height:15px;width:15px;background-color:" + clr.Hex + ";border-radius:15px'  ><img src=\"{{'spacer.png' | asset_url }}\" alt=\"" + clr.Color_Description + "\"/></div>\n");
                    count++;
                }
                sb.Append("</label>\n");


                //close div Color
                sb.Append("</div>");

                using (StreamWriter sw = new StreamWriter(@"OuputPath\ShopifyOptionTemplate\" + st.Style_Number + "_markup.html"))
                {
                    sw.Write(sb.ToString());
                }

            }
        }
    }
}
