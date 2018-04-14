using ClientGarmentManagment.Forms;
using ClientGarmentManagment.Models;
using ClientGarmentManagment.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGarmentManagment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Form Member
        //Allows design styles accessable for all functions
        List<cDesignStyle> _designStyles;

        //Client/Customer 
        cCustomer _customer = null;

        //In case there is some whacky stuff put in he text box
        int _clientID = 0;

        private void GetClientgarments()
        {
            dgvColors.DataSource = null;
            dgvDeisgns.DataSource = null;
            dgvSizes.DataSource = null;
            dgvStyles.DataSource = null;

            if (int.TryParse(txtClientID.Text, out _clientID))
            {
                frmLoading loading = new Forms.frmLoading();
                loading.Show();
                Application.DoEvents();
                _designStyles = DesignServices.GetDesignStylesByClientID(_clientID);
                _customer = AccessServices.GetCustomerByClientID(_clientID);
                loading.Close();

            }
            else if (Directory.Exists(txtClientID.Text))
            {
                _designStyles = DesignServices.GetDesignStylesFromSpreadsheets(txtClientID.Text);
            }
            else
            {
                var allActiveClients = AccessServices.GetAllActiveCustomersByClientID();
                foreach(var customer in allActiveClients)
                {
                    lblCost.Text = string.Format("Creating JSON For {0}", customer.Company_Name);
                    Application.DoEvents();
                    var cstyles = AccessServices.GetStylesByClientID(customer.Company_ID);
                    var cdesigns = AccessServices.GetDesignsByClientID(customer.Company_ID);
                    var jsonDesigns = JsonConvert.SerializeObject(JSONGenerator.JSONGeneratorHelpers.CreateDesigns(cdesigns));
                    var jsonStyles = JsonConvert.SerializeObject(JSONGenerator.JSONGeneratorHelpers.CreateStyles(cstyles));
                    string jsonCustomer = JsonConvert.SerializeObject(JSONGenerator.JSONGeneratorHelpers.CreateCustomer(customer));


                    using (StreamWriter sw = new StreamWriter(@"OuputPath\JSON\" + customer.Company_ID.ToString() + "_styledesigns.js"))
                    {
                        string json = string.Format("var styles = {0} \n\r  var designs = {1} \n\r var user = {2}", jsonStyles, jsonDesigns, jsonCustomer);

                        sw.Write(json);
                    }

                }
                lblCost.Text = string.Format("Done");
                return;
            }

            var designs = _designStyles.Select(x => x.Design).Distinct().ToList();
            var styles = _designStyles.Select(x => x.Style).Distinct().ToList();

            dgvDeisgns.DataSource = designs;
            dgvStyles.DataSource = styles.OrderBy(x => x.Gender).Select(x => new { Number = x.Style_Number, Description = x.Gender + " " + x.Website_Description, MaxHue = x.MaxHue, MinOverride = x.MinOverride }).ToList();
            if(_customer != null)
            {
                lblCost.Text = _customer.Company_Name;
            }

        }

        private void btnGetClientGarments_Click(object sender, EventArgs e)
        {
            GetClientgarments();
        }

        private void dgvStyles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvColors.DataSource = null;
            dgvSizes.DataSource = null;
            var cell = dgvStyles.SelectedCells[0];
            var stylenumber = dgvStyles[0, cell.RowIndex].Value.ToString();
            var style = _designStyles.Where(x => x.Style.Style_Number == stylenumber).Select(x => x.Style).FirstOrDefault();
            var colors = style.ColorSize.Select(x => x.color).Distinct().ToList();
            dgvColors.DataSource = colors;
            dgvColors.Columns[0].Visible = false;
        }

        private void dgvColors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSizes.DataSource = null;
            var cell = dgvColors.SelectedCells[0];
            var stylenumber = dgvColors[0, cell.RowIndex].Value.ToString();
            var style = _designStyles.Where(x => x.Style.ID == stylenumber).Select(x => x.Style).FirstOrDefault();
            if(style == null)
            {
                style = _designStyles.Where(x => x.Style.Style_Number == stylenumber).Select(x => x.Style).FirstOrDefault();
            }
            var sizes = style.ColorSize.Where(x=>x.color.Color_Description == dgvColors[1, cell.RowIndex].Value.ToString())
                .Select(x => x.size).Distinct().ToList();
            dgvSizes.DataSource = sizes
                .Select(x=> new
                {
                    Size = x.Size_Description,
                    Weight = x.Weight,
                    Cost = style.ColorSize.Where(y => y.color.Color_Description == dgvColors[1, cell.RowIndex].Value.ToString() && y.size == x).FirstOrDefault().Catalog_Price
                }
            ).ToList();
        }

        private void txtClientID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                GetClientgarments();
            }
        }

        private void btnGenerateShopify_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            DesignServices.GenerateShopifyProductFile(_designStyles);
            StyleServices.GenerateShopifyStyleSnippits(_designStyles.Select(x => x.Style).Distinct().ToList());
            lblStatus.Text = "Done Exporting Shopify Data";
        }

        private void btnGenerateJSON_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            var designs = _designStyles.Select(x => x.Design).Distinct().ToList();
            var styles = _designStyles.Select(x => x.Style).Distinct().ToList();

            var jsonDesigns = JsonConvert.SerializeObject(JSONGenerator.JSONGeneratorHelpers.CreateDesigns(designs));
            var jsonStyles = JsonConvert.SerializeObject(JSONGenerator.JSONGeneratorHelpers.CreateStyles(styles));
            string jsonCustomer = "";
            if(_customer != null)
            {
                jsonCustomer = JsonConvert.SerializeObject(JSONGenerator.JSONGeneratorHelpers.CreateCustomer(_customer));
            }
           

            using (StreamWriter sw = new StreamWriter(@"OuputPath\JSON\" + _clientID + "_styledesigns.js"))
            {
                string json = string.Format("var styles = {0} \n\r  var designs = {1} \n\r var user = {2}", jsonStyles, jsonDesigns,jsonCustomer);
                
                sw.Write(json);
            }
            lblStatus.Text = "Done Exporting JSON";
        }

        private void btnGenerateSkus_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            StringBuilder sbSkus = new StringBuilder();
            sbSkus.Append("SKU\tDesign\tStyle\tColor\tSize\n");
            foreach(var dsgnstl in _designStyles.Where(x=>!x.Design.Design_ID.Contains("BLANK")))
            {

                foreach(var clrsze in dsgnstl.Style.ColorSize)
                {
                    sbSkus.AppendFormat("{0}_{1}_{2}_{3}\t{4}\t{5}\t{6}\t{7}\n",
                        (dsgnstl.Design.Design_ID.Length < 5 ? dsgnstl.Design.Design_ID + "A" : dsgnstl.Design.Design_ID), dsgnstl.Style.Style_Number,
                        clrsze.color.Color_Description.Replace(" ", "-"), clrsze.size.Size_Description, dsgnstl.Design.Design_Description
                        , dsgnstl.Style.Website_Description, clrsze.color.Color_Description, clrsze.size.Size_Description);
                }
            }

            using (StreamWriter sw = new StreamWriter(@"OuputPath\ShopifySKU\" + _customer.Company_Name + "_SKUs.xls"))
            {
                sw.Write(sbSkus.ToString());
            }
            lblStatus.Text = "Done Exporting SKUs";
        }
    }
}
