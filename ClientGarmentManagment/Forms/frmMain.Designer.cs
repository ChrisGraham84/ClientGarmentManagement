namespace ClientGarmentManagment
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.btnGetClientGarments = new System.Windows.Forms.Button();
            this.dgvDeisgns = new System.Windows.Forms.DataGridView();
            this.dgvStyles = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvSizes = new System.Windows.Forms.DataGridView();
            this.dgvColors = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.btnGenerateShopify = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnGenerateJSON = new System.Windows.Forms.Button();
            this.btnGenerateSkus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeisgns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStyles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client ID";
            // 
            // txtClientID
            // 
            this.txtClientID.Location = new System.Drawing.Point(89, 16);
            this.txtClientID.Margin = new System.Windows.Forms.Padding(4);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(132, 22);
            this.txtClientID.TabIndex = 1;
            this.txtClientID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClientID_KeyPress);
            // 
            // btnGetClientGarments
            // 
            this.btnGetClientGarments.Location = new System.Drawing.Point(237, 15);
            this.btnGetClientGarments.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetClientGarments.Name = "btnGetClientGarments";
            this.btnGetClientGarments.Size = new System.Drawing.Size(100, 28);
            this.btnGetClientGarments.TabIndex = 2;
            this.btnGetClientGarments.Text = "Get Garments";
            this.btnGetClientGarments.UseVisualStyleBackColor = true;
            this.btnGetClientGarments.Click += new System.EventHandler(this.btnGetClientGarments_Click);
            // 
            // dgvDeisgns
            // 
            this.dgvDeisgns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeisgns.Location = new System.Drawing.Point(595, 86);
            this.dgvDeisgns.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDeisgns.Name = "dgvDeisgns";
            this.dgvDeisgns.Size = new System.Drawing.Size(955, 315);
            this.dgvDeisgns.TabIndex = 3;
            // 
            // dgvStyles
            // 
            this.dgvStyles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStyles.Location = new System.Drawing.Point(16, 86);
            this.dgvStyles.Margin = new System.Windows.Forms.Padding(4);
            this.dgvStyles.Name = "dgvStyles";
            this.dgvStyles.Size = new System.Drawing.Size(560, 315);
            this.dgvStyles.TabIndex = 3;
            this.dgvStyles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStyles_CellDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Styles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(591, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Designs";
            // 
            // dgvSizes
            // 
            this.dgvSizes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSizes.Location = new System.Drawing.Point(809, 426);
            this.dgvSizes.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSizes.Name = "dgvSizes";
            this.dgvSizes.Size = new System.Drawing.Size(740, 454);
            this.dgvSizes.TabIndex = 5;
            // 
            // dgvColors
            // 
            this.dgvColors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColors.Location = new System.Drawing.Point(16, 426);
            this.dgvColors.Margin = new System.Windows.Forms.Padding(4);
            this.dgvColors.Name = "dgvColors";
            this.dgvColors.Size = new System.Drawing.Size(773, 454);
            this.dgvColors.TabIndex = 5;
            this.dgvColors.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColors_CellDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(805, 406);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sizes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 406);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Colors";
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(595, 18);
            this.lblCost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(36, 17);
            this.lblCost.TabIndex = 6;
            this.lblCost.Text = "Cost";
            // 
            // btnGenerateShopify
            // 
            this.btnGenerateShopify.Location = new System.Drawing.Point(1367, 887);
            this.btnGenerateShopify.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateShopify.Name = "btnGenerateShopify";
            this.btnGenerateShopify.Size = new System.Drawing.Size(183, 28);
            this.btnGenerateShopify.TabIndex = 7;
            this.btnGenerateShopify.Text = "Generate Shopify Files";
            this.btnGenerateShopify.UseVisualStyleBackColor = true;
            this.btnGenerateShopify.Click += new System.EventHandler(this.btnGenerateShopify_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(689, 894);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TabIndex = 6;
            // 
            // btnGenerateJSON
            // 
            this.btnGenerateJSON.Location = new System.Drawing.Point(16, 887);
            this.btnGenerateJSON.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateJSON.Name = "btnGenerateJSON";
            this.btnGenerateJSON.Size = new System.Drawing.Size(183, 28);
            this.btnGenerateJSON.TabIndex = 7;
            this.btnGenerateJSON.Text = "Generate JSON";
            this.btnGenerateJSON.UseVisualStyleBackColor = true;
            this.btnGenerateJSON.Click += new System.EventHandler(this.btnGenerateJSON_Click);
            // 
            // btnGenerateSkus
            // 
            this.btnGenerateSkus.Location = new System.Drawing.Point(207, 888);
            this.btnGenerateSkus.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateSkus.Name = "btnGenerateSkus";
            this.btnGenerateSkus.Size = new System.Drawing.Size(183, 28);
            this.btnGenerateSkus.TabIndex = 7;
            this.btnGenerateSkus.Text = "Generate SKUs";
            this.btnGenerateSkus.UseVisualStyleBackColor = true;
            this.btnGenerateSkus.Click += new System.EventHandler(this.btnGenerateSkus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1565, 926);
            this.Controls.Add(this.btnGenerateSkus);
            this.Controls.Add(this.btnGenerateJSON);
            this.Controls.Add(this.btnGenerateShopify);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.dgvColors);
            this.Controls.Add(this.dgvSizes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvStyles);
            this.Controls.Add(this.dgvDeisgns);
            this.Controls.Add(this.btnGetClientGarments);
            this.Controls.Add(this.txtClientID);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Client Garment Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeisgns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStyles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSizes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.Button btnGetClientGarments;
        private System.Windows.Forms.DataGridView dgvDeisgns;
        private System.Windows.Forms.DataGridView dgvStyles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvSizes;
        private System.Windows.Forms.DataGridView dgvColors;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Button btnGenerateShopify;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnGenerateJSON;
        private System.Windows.Forms.Button btnGenerateSkus;
    }
}

