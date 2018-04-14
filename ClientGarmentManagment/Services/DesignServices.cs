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
    public static class DesignServices
    {
        public static List<cDesignStyle> GetDesignStylesByClientID(int ClientID)
        {
            var designs = AccessServices.GetDesignsByClientID(ClientID);
            var styles = AccessServices.GetStylesByClientID(ClientID);

            decimal profit = 7.0M;

            if(ClientID == 512)
            {
                profit = 11.0M;
            }

            List<cDesignStyle> designstyles = new List<cDesignStyle>();
            foreach (var dsgn in designs)
            {

                if (dsgn.UB == "")
                {
                    dsgn.UB = "PER";
                }
                List<string> designpositions = dsgn.Print_Location_Description.Split('_').ToList();
                foreach (var stl in styles)
                {
                    List<string> stylepositions = stl.Print_Positions.Split('|').ToList();
                    if (stylepositions.Intersect(designpositions).Count() > 0)
                    {
                        //calculating style design cost
                        decimal price = 0.0M;
                        var print_cost = decimal.Parse(dsgn.Print_Cost);
                        var print_hue_cost = decimal.Parse(dsgn.Print_Hue_Cost);
                        int mult = 0;
                        if (dsgn.UB == "YES")
                        {
                            if (stl.MinOverride == 1)
                                mult = 0;
                            else
                                mult = 1;
                        }
                        else if (dsgn.UB == "PER")
                        {
                            if (stl.MaxHue == 1)
                                mult = 1;
                            else
                                mult = 0;
                        }

                        price = profit + print_cost + print_hue_cost * mult;
                        designstyles.Add(new cDesignStyle { Design = dsgn, Style = stl, DesignCost = Math.Ceiling(price) });
                    }
                }
            }

            return (designstyles);
        }
        public static List<cDesignStyle> GetDesignStylesFromSpreadsheets(string Path)
        {
            string designpath = Path + "\\QueryDesigns.xls";
            string stylepath = Path + "\\QueryStyles.xls";

            if (File.Exists(designpath) && File.Exists(designpath))
            {
                var designdt = ImportHelper.ReadFromToDataTable(designpath, "Query4");
                var styledt = ImportHelper.ReadFromToDataTable(stylepath, "Query4");

                var styleflats = ImportHelper.GetObjectsFromDataTable<cStyleFlat>(styledt);
                var designs = ImportHelper.GetObjectsFromDataTable<cDesign>(designdt);

                var styles = StyleServices.GetStyles(styleflats);

                List<cDesignStyle> designstyles = new List<cDesignStyle>();
                decimal profit = 11.0M;
                foreach (var dsgn in designs)
                {
                    if (dsgn.Design_ID.Length != 5)
                    {
                        dsgn.Design_ID = dsgn.Design_ID + "A";
                    }
                    foreach (var stl in styles)
                    {
                        stl.Gender = (Globals.GenderDictionary.ContainsKey(stl.Gender) ? Globals.GenderDictionary[stl.Gender] : "Mens");
                        //calculating style design cost
                        decimal price = 0.0M;
                        var print_cost = decimal.Parse(dsgn.Print_Cost);
                        var print_hue_cost = decimal.Parse(dsgn.Print_Hue_Cost);
                        int mult = 0;
                        if (dsgn.UB == "YES")
                        {
                            if (stl.MinOverride == 1)
                                mult = 0;
                            else
                                mult = 1;
                        }
                        else if (dsgn.UB == "PER")
                        {
                            if (stl.MaxHue == 1)
                                mult = 1;
                            else
                                mult = 0;
                        }

                        price = profit + print_cost + print_hue_cost * mult;
                        designstyles.Add(new cDesignStyle { Design = dsgn, Style = stl, DesignCost = Math.Ceiling(price) });
                    }
                }

                return (designstyles);
            }

            return null;
        }
        public static void GenerateShopifyProductFile(List<cDesignStyle> DesignStyles)
        {
            List<cShopifyProduct> shopifyproducts = new List<cShopifyProduct>();
            cShopifyProduct sp;
            foreach (var ds in DesignStyles.Where(x => x.Design.Design_Description != "Blank High-Quality Wholesale Shirts"))
            {

                bool first = true;
                foreach (var s in ds.Style.ColorSize.Select(x => x.size).Distinct().ToList())
                {
                    string handle = (ds.Design.Design_Description + ds.Style.Gender + " " + ds.Style.Website_Description).ToLower().Replace(' ', '-').Replace("?", "").Replace("!", "").Replace("#", "").Replace(",","");
                    if (first)
                    {
                        sp = new cShopifyProduct();
                        sp.Handle = handle;
                        sp.Title = (ds.Design.Design_Description + ds.Style.Gender + " " + ds.Style.Website_Description);
                        sp.Body = ds.Style.Detailed_Description.Replace(",", "");
                        sp.Vendor = ds.Style.Manufacturer;
                        sp.Type = ds.Style.Gender + " " + ds.Style.Website_Description;
                        sp.Tags = ds.Style.Gender.ToLower();
                        sp.Published = "FALSE";
                        sp.Option1_Name = "Size";
                        sp.Option1_Value = s.Size_Description;
                        sp.Variant_SKU = string.Format("{0}_{1}_{2}", ds.Design.Design_ID, ds.Style.Style_Number, s.Size_Description);
                        sp.Variant_Grams = "0";
                        sp.Variant_Inventory_Qty = "1000";
                        sp.Variant_Inventory_Policy = "deny";
                        sp.Variant_Fulfillment_Service = "manual";
                        sp.Variant_Price = (ds.DesignCost + decimal.Parse(ds.Style.ColorSize.Where(x => int.Parse(x.color.Hue) == ds.Style.MaxHue && x.size == s).FirstOrDefault().Catalog_Price)).ToString();
                        sp.Variant_Compare_At_Price = "";
                        sp.Variant_Requires_Shipping = "TRUE";
                        sp.Variant_Taxable = "TRUE";
                        sp.Variant_Barcode = "0";
                        sp.Image_Src = "";
                        sp.Image_Alt_Text = "";
                        sp.Variant_Image = "";
                        shopifyproducts.Add(sp);

                        first = false;
                    }
                    else
                    {
                        sp = new cShopifyProduct();
                        sp.Handle = handle;
                        //sp.Title = ds.Design.Design_Description;
                        //sp.Body = ds.Style.Detailed_Description.Replace(",","");
                        //sp.Vendor = ds.Style.Manufacturer;
                        //sp.Type = ds.Style.Gender + " " + ds.Style.Website_Description;
                        //sp.Tags = ds.Style.Gender.ToLower();
                        //sp.Published = "FALSE";
                        sp.Option1_Name = "Size";
                        sp.Option1_Value = s.Size_Description;
                        sp.Variant_SKU = string.Format("{0}_{1}_{2}", ds.Design.Design_ID, ds.Style.Style_Number, s.Size_Description);
                        sp.Variant_Grams = "0";
                        sp.Variant_Inventory_Qty = "1000";
                        sp.Variant_Inventory_Policy = "deny";
                        sp.Variant_Fulfillment_Service = "manual";
                        sp.Variant_Price = (ds.DesignCost + decimal.Parse(ds.Style.ColorSize.Where(x => int.Parse(x.color.Hue) == ds.Style.MaxHue && x.size == s).FirstOrDefault().Catalog_Price)).ToString();
                        sp.Variant_Compare_At_Price = "";
                        sp.Variant_Requires_Shipping = "TRUE";
                        sp.Variant_Taxable = "TRUE";
                        sp.Variant_Barcode = "0";
                        sp.Image_Src = "";
                        sp.Image_Alt_Text = "";
                        sp.Variant_Image = "";
                        shopifyproducts.Add(sp);
                    }

                }

            }

            List<string> properties = new cShopifyProduct().GetType().GetProperties().Select(x => x.Name).ToList();

            StringBuilder sbHeader = new StringBuilder();
            foreach (var prop in properties)
            {
                sbHeader.AppendFormat("{0},", prop.Replace("_", " ").Replace("Body", "Body (HTML)"));
            }


            using (StreamWriter writer = new StreamWriter(@"OuputPath\ProductUploadt.csv"))
            {

                writer.WriteLine(sbHeader.ToString());
                foreach (var ext in shopifyproducts)
                {
                    StringBuilder sbData = new StringBuilder();
                    foreach (var prop in properties)
                    {
                        var o = ext.GetType().GetProperty(prop).GetValue(ext, null);
                        if (o == null)
                        {
                            o = "";
                        }
                        sbData.AppendFormat("{0},", o.ToString());
                    }
                    writer.WriteLine(sbData.ToString());
                }
            }
        }
        
    }
}
