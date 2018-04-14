using ClientGarmentManagment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.Helpers
{
    public static class ExportHelper
    {
        public static void GenerateStyleSnippets(string Path, List<cStyle> Styles)
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

                using (StreamWriter sw = new StreamWriter(Path + "\\" + st.Style_Number + "_markup.html"))
                {
                    sw.Write(sb.ToString());
                }

            }
        }
    }
}
