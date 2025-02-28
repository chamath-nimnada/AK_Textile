namespace AK_Textile
{
    partial class InventoryInventoryAdd
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
            this.button8 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.itemName = new System.Windows.Forms.TextBox();
            this.invID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.itemQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.invCategory = new System.Windows.Forms.ComboBox();
            this.stkLevel = new System.Windows.Forms.ComboBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(114)))), ((int)(((byte)(159)))));
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(400, 439);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(151, 47);
            this.button8.TabIndex = 20;
            this.button8.Text = "Add";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(114)))), ((int)(((byte)(159)))));
            this.button1.Location = new System.Drawing.Point(268, 439);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 47);
            this.button1.TabIndex = 23;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.dateTimePicker);
            this.groupBox2.Controls.Add(this.stkLevel);
            this.groupBox2.Controls.Add(this.invCategory);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.itemQty);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.itemName);
            this.groupBox2.Controls.Add(this.invID);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(539, 421);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Material Details";
            // 
            // itemName
            // 
            this.itemName.BackColor = System.Drawing.SystemColors.MenuBar;
            this.itemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemName.Location = new System.Drawing.Point(188, 157);
            this.itemName.Name = "itemName";
            this.itemName.Size = new System.Drawing.Size(339, 32);
            this.itemName.TabIndex = 5;
            // 
            // invID
            // 
            this.invID.BackColor = System.Drawing.Color.CornflowerBlue;
            this.invID.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invID.Location = new System.Drawing.Point(188, 56);
            this.invID.Name = "invID";
            this.invID.Size = new System.Drawing.Size(251, 32);
            this.invID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            this.label3.Location = new System.Drawing.Point(48, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Item Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            this.label2.Location = new System.Drawing.Point(48, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Category";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            this.label1.Location = new System.Drawing.Point(48, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inventory ID";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(114)))), ((int)(((byte)(159)))));
            this.button2.Location = new System.Drawing.Point(12, 439);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 47);
            this.button2.TabIndex = 25;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // itemQty
            // 
            this.itemQty.BackColor = System.Drawing.SystemColors.MenuBar;
            this.itemQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemQty.Location = new System.Drawing.Point(188, 207);
            this.itemQty.Name = "itemQty";
            this.itemQty.Size = new System.Drawing.Size(339, 32);
            this.itemQty.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            this.label4.Location = new System.Drawing.Point(48, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "Quantity";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            this.label5.Location = new System.Drawing.Point(48, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(68)))), ((int)(((byte)(114)))));
            this.label6.Location = new System.Drawing.Point(48, 314);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 26);
            this.label6.TabIndex = 10;
            this.label6.Text = "Stock Level";
            // 
            // invCategory
            // 
            this.invCategory.FormattingEnabled = true;
            this.invCategory.Location = new System.Drawing.Point(188, 109);
            this.invCategory.Name = "invCategory";
            this.invCategory.Size = new System.Drawing.Size(339, 34);
            this.invCategory.TabIndex = 11;
            // 
            // stkLevel
            // 
            this.stkLevel.FormattingEnabled = true;
            this.stkLevel.Items.AddRange(new object[] {
            "Low",
            "Normal",
            "High"});
            this.stkLevel.Location = new System.Drawing.Point(188, 311);
            this.stkLevel.Name = "stkLevel";
            this.stkLevel.Size = new System.Drawing.Size(339, 34);
            this.stkLevel.TabIndex = 12;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(188, 258);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(339, 32);
            this.dateTimePicker.TabIndex = 13;
            // 
            // InventoryInventoryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 489);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InventoryInventoryAdd";
            this.Text = "Add Materials";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.InventoryInventoryAdd_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox itemName;
        private System.Windows.Forms.TextBox invID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox itemQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox stkLevel;
        private System.Windows.Forms.ComboBox invCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}