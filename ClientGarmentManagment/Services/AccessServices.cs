using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientGarmentManagment.Models;
using ClientGarmentManagment.Helpers;
using System.Data.OleDb;
using System.Data;

namespace ClientGarmentManagment.Services
{
    public static class AccessServices
    {
        public static List<cDesign> GetDesignsByClientID(int ClientID)
        {
            OleDbConnection Conn = new OleDbConnection(); 
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MSAccess.mdb;Persist Security Info=False;")))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT dsn.*, duo.use_underbase as UB ");
                sql.Append("FROM designs dsn ");
                sql.Append("LEFT JOIN designs_underbase_override duo on dsn.design_id = duo.design_id ");
                sql.Append("WHERE company_id = "+ ClientID);
                OleDbDataAdapter da = new OleDbDataAdapter(sql.ToString(), con);
                da.Fill(dt);
            }

                return (ImportHelper.GetObjectsFromDataTable<cDesign>(dt));
        }
        public static List<cColor> GetColorsByClientID(int ClientID)
        {
            OleDbConnection Conn = new OleDbConnection();
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MSAccess.mdb;Persist Security Info=False;")))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT Distinct(bsct.color_description), bsct.hue, bsct.color_type, bsct.underbase_no_override, bsct.shirt_style_number ");
                sql.Append("FROM billing_shirt_color_types bsct ");
                sql.Append("INNER JOIN customer_buys cb on bsct.shirt_style_number = cb.style ");
                sql.Append("WHERE cb.discontinued = false AND bsct.discontinued = false AND cb.customer_name = " + ClientID);
                OleDbDataAdapter da = new OleDbDataAdapter(sql.ToString(), con);
                da.Fill(dt);
            }

            var colors = ImportHelper.GetObjectsFromDataTable<cColor>(dt);
            foreach(var c in colors)
            {
                c.Hex = (Globals.ColorHexDictionary.ContainsKey(c.Color_Description) ? Globals.ColorHexDictionary[c.Color_Description] : "#000000");
            }

            return (colors);
        }
        public static List<cSize> GetSizesByClientID(int ClientID)
        {
            OleDbConnection Conn = new OleDbConnection();
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MSAccess.mdb;Persist Security Info=False;")))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT shirt_style_number,weight,size_description,size_id ");
                sql.Append("FROM billing_shirt_weight ");
                sql.Append("INNER JOIN Customer_Buys ON billing_shirt_weight.Shirt_Style_Number = Customer_Buys.Style ");
                sql.Append("WHERE Customer_Buys.Customer_Name=" + ClientID + " AND customer_buys.discontinued=false");
                OleDbDataAdapter da = new OleDbDataAdapter(sql.ToString(), con);
                da.Fill(dt);
            }

            return (ImportHelper.GetObjectsFromDataTable<cSize>(dt));
        }
        public static List<cStyle> GetStylesByClientID(int ClientID)
        {
            OleDbConnection Conn = new OleDbConnection();
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MSAccess.mdb;Persist Security Info=False;")))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT billing_shirt_style.ID as ID, billing_shirt_style.* ");
                sql.Append("FROM billing_shirt_style ");
                sql.Append("INNER JOIN Customer_Buys ON billing_shirt_style.ID = Customer_Buys.Style ");
                sql.Append("WHERE Customer_Buys.Customer_Name=" + ClientID + " AND customer_buys.discontinued=false AND billing_shirt_style.discontinued = false ");
                OleDbDataAdapter da = new OleDbDataAdapter(sql.ToString(), con);
                da.Fill(dt);
            }
            var colors = GetColorsByClientID(ClientID);
            var sizes = GetSizesByClientID(ClientID);
            var pricing = GetPricingByClientID(ClientID);
            var styles = ImportHelper.GetObjectsFromDataTable<cStyle>(dt);
            foreach(var style in styles)
            {
                style.Gender = (Globals.GenderDictionary.ContainsKey(style.Gender) ? Globals.GenderDictionary[style.Gender] : "Other");

                style.ColorSize = new List<cColorSize>();
                int maxhue = 0;
                int minoverride = 1;
                var stylecolor = colors.Where(x => x.shirt_style_number == style.ID);
                var stylesizes = sizes.Where(x => x.Shirt_Style_Number == style.ID);
                var styleprices = pricing.Where(x => x.Shirt_Style == style.ID);
                foreach (var color in stylecolor)
                {
                    foreach (var size in stylesizes.OrderBy(x => x.size_id))
                    {
                        if (int.Parse(color.Hue) > 0)
                        {
                            maxhue = 1;
                        }
                        if (int.Parse(color.Underbase_NO_Override) < 1)
                        {
                            minoverride = 0;
                        }

                        cColorSize colorsize = new cColorSize { color = color, size = size };

                        var price = styleprices.Where(x => x.Size == size.Size_Description && x.Color_Type == color.Color_Type).FirstOrDefault();
                        if (price != null)
                        {
                            colorsize.Catalog_Price = price.Catalog_Price;
                        }

                        style.ColorSize.Add(colorsize);
                    }
                }
                style.MaxHue = maxhue;
                style.MinOverride = minoverride;
            }

            return (styles);
        }
        public static List<cPricing> GetPricingByClientID(int ClientID)
        {
            OleDbConnection Conn = new OleDbConnection();
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MSAccess.mdb;Persist Security Info=False;")))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT * ");
                sql.Append("FROM billing_size_colortype_pricing ");
                sql.Append("INNER JOIN Customer_Buys ON billing_size_colortype_pricing.shirt_style = Customer_Buys.Style ");
                sql.Append("WHERE Customer_Buys.Customer_Name=" + ClientID + " AND customer_buys.discontinued=false AND billing_size_colortype_pricing.discontinued = false ");
                OleDbDataAdapter da = new OleDbDataAdapter(sql.ToString(), con);
                da.Fill(dt);
            }

            return (ImportHelper.GetObjectsFromDataTable<cPricing>(dt));
        }
        public static cCustomer GetCustomerByClientID(int ClientID)
        {
            OleDbConnection Conn = new OleDbConnection();
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MSAccess.mdb;Persist Security Info=False;")))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT * ");
                sql.Append("FROM customers cust ");
                sql.Append("WHERE company_id = " + ClientID);
                OleDbDataAdapter da = new OleDbDataAdapter(sql.ToString(), con);
                da.Fill(dt);
            }

            return (ImportHelper.GetObjectsFromDataTable<cCustomer>(dt).FirstOrDefault());
        }
        public static List<cCustomer> GetAllActiveCustomersByClientID()
        {
            OleDbConnection Conn = new OleDbConnection();
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MSAccess.mdb;Persist Security Info=False;")))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT * ");
                sql.Append("FROM customers cust ");
                sql.Append("WHERE Inactive_Customer = FALSE");
                OleDbDataAdapter da = new OleDbDataAdapter(sql.ToString(), con);
                da.Fill(dt);
            }

            return (ImportHelper.GetObjectsFromDataTable<cCustomer>(dt));
        }

    }
}
