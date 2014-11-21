namespace FlexCore
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.tableHeader = new System.Windows.Forms.TableLayoutPanel();
            this.menuItems = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.personsMenu = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mainMenuPanel = new System.Windows.Forms.Panel();
            this.mainItem = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.versionComboBox = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.secPlusButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.secText = new System.Windows.Forms.TextBox();
            this.dayText = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dayPlusButton = new System.Windows.Forms.Button();
            this.minPlusButton = new System.Windows.Forms.Button();
            this.monthText = new System.Windows.Forms.TextBox();
            this.minText = new System.Windows.Forms.TextBox();
            this.monthPlusButton = new System.Windows.Forms.Button();
            this.hourPlusButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.hourText = new System.Windows.Forms.TextBox();
            this.yearText = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.yearPlusButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.tableHeader.SuspendLayout();
            this.menuItems.SuspendLayout();
            this.mainMenuPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.panelMenu.Controls.Add(this.tableHeader);
            this.panelMenu.Controls.Add(this.label6);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(984, 37);
            this.panelMenu.TabIndex = 0;
            // 
            // tableHeader
            // 
            this.tableHeader.ColumnCount = 3;
            this.tableHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableHeader.Controls.Add(this.menuItems, 2, 0);
            this.tableHeader.Controls.Add(this.mainMenuPanel, 0, 0);
            this.tableHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableHeader.Location = new System.Drawing.Point(0, 0);
            this.tableHeader.Name = "tableHeader";
            this.tableHeader.RowCount = 1;
            this.tableHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableHeader.Size = new System.Drawing.Size(984, 37);
            this.tableHeader.TabIndex = 6;
            this.tableHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // menuItems
            // 
            this.menuItems.Controls.Add(this.label7);
            this.menuItems.Controls.Add(this.personsMenu);
            this.menuItems.Controls.Add(this.label3);
            this.menuItems.Controls.Add(this.label5);
            this.menuItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuItems.Location = new System.Drawing.Point(687, 3);
            this.menuItems.Name = "menuItems";
            this.menuItems.Size = new System.Drawing.Size(294, 31);
            this.menuItems.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Cierres";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // personsMenu
            // 
            this.personsMenu.AutoSize = true;
            this.personsMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.personsMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personsMenu.ForeColor = System.Drawing.Color.White;
            this.personsMenu.Location = new System.Drawing.Point(222, 9);
            this.personsMenu.Name = "personsMenu";
            this.personsMenu.Size = new System.Drawing.Size(66, 16);
            this.personsMenu.TabIndex = 1;
            this.personsMenu.Text = "Personas";
            this.personsMenu.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.personsMenu.Click += new System.EventHandler(this.personsMenu_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(150, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Clientes";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(70, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Reportes";
            // 
            // mainMenuPanel
            // 
            this.mainMenuPanel.Controls.Add(this.mainItem);
            this.mainMenuPanel.Location = new System.Drawing.Point(3, 3);
            this.mainMenuPanel.Name = "mainMenuPanel";
            this.mainMenuPanel.Size = new System.Drawing.Size(136, 31);
            this.mainMenuPanel.TabIndex = 2;
            // 
            // mainItem
            // 
            this.mainItem.AutoSize = true;
            this.mainItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mainItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainItem.ForeColor = System.Drawing.Color.White;
            this.mainItem.Location = new System.Drawing.Point(3, 9);
            this.mainItem.Name = "mainItem";
            this.mainItem.Size = new System.Drawing.Size(62, 16);
            this.mainItem.TabIndex = 0;
            this.mainItem.Text = "FlexCore";
            this.mainItem.Click += new System.EventHandler(this.mainItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(646, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Cierres";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelContent, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 37);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 408);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 287F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 504F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 373);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(984, 35);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.versionComboBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(287, 35);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Version de la base de datos";
            // 
            // versionComboBox
            // 
            this.versionComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.versionComboBox.FormattingEnabled = true;
            this.versionComboBox.Location = new System.Drawing.Point(153, 7);
            this.versionComboBox.Name = "versionComboBox";
            this.versionComboBox.Size = new System.Drawing.Size(121, 21);
            this.versionComboBox.TabIndex = 1;
            this.versionComboBox.SelectedIndexChanged += new System.EventHandler(this.versionComboBox_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.secPlusButton);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.secText);
            this.panel3.Controls.Add(this.dayText);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.dayPlusButton);
            this.panel3.Controls.Add(this.minPlusButton);
            this.panel3.Controls.Add(this.monthText);
            this.panel3.Controls.Add(this.minText);
            this.panel3.Controls.Add(this.monthPlusButton);
            this.panel3.Controls.Add(this.hourPlusButton);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.hourText);
            this.panel3.Controls.Add(this.yearText);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.yearPlusButton);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(480, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(504, 35);
            this.panel3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha";
            // 
            // secPlusButton
            // 
            this.secPlusButton.Location = new System.Drawing.Point(475, 7);
            this.secPlusButton.Name = "secPlusButton";
            this.secPlusButton.Size = new System.Drawing.Size(23, 23);
            this.secPlusButton.TabIndex = 19;
            this.secPlusButton.Text = "+";
            this.secPlusButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(103, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 25);
            this.label8.TabIndex = 3;
            this.label8.Text = "/";
            // 
            // secText
            // 
            this.secText.Location = new System.Drawing.Point(441, 9);
            this.secText.Name = "secText";
            this.secText.Size = new System.Drawing.Size(31, 20);
            this.secText.TabIndex = 18;
            // 
            // dayText
            // 
            this.dayText.Location = new System.Drawing.Point(47, 9);
            this.dayText.Name = "dayText";
            this.dayText.Size = new System.Drawing.Size(31, 20);
            this.dayText.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(424, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 25);
            this.label10.TabIndex = 17;
            this.label10.Text = ":";
            // 
            // dayPlusButton
            // 
            this.dayPlusButton.Location = new System.Drawing.Point(81, 7);
            this.dayPlusButton.Name = "dayPlusButton";
            this.dayPlusButton.Size = new System.Drawing.Size(23, 23);
            this.dayPlusButton.TabIndex = 5;
            this.dayPlusButton.Text = "+";
            this.dayPlusButton.UseVisualStyleBackColor = true;
            // 
            // minPlusButton
            // 
            this.minPlusButton.Location = new System.Drawing.Point(402, 7);
            this.minPlusButton.Name = "minPlusButton";
            this.minPlusButton.Size = new System.Drawing.Size(23, 23);
            this.minPlusButton.TabIndex = 16;
            this.minPlusButton.Text = "+";
            this.minPlusButton.UseVisualStyleBackColor = true;
            // 
            // monthText
            // 
            this.monthText.Location = new System.Drawing.Point(120, 9);
            this.monthText.Name = "monthText";
            this.monthText.Size = new System.Drawing.Size(31, 20);
            this.monthText.TabIndex = 6;
            // 
            // minText
            // 
            this.minText.Location = new System.Drawing.Point(368, 9);
            this.minText.Name = "minText";
            this.minText.Size = new System.Drawing.Size(31, 20);
            this.minText.TabIndex = 15;
            // 
            // monthPlusButton
            // 
            this.monthPlusButton.Location = new System.Drawing.Point(154, 7);
            this.monthPlusButton.Name = "monthPlusButton";
            this.monthPlusButton.Size = new System.Drawing.Size(23, 23);
            this.monthPlusButton.TabIndex = 7;
            this.monthPlusButton.Text = "+";
            this.monthPlusButton.UseVisualStyleBackColor = true;
            // 
            // hourPlusButton
            // 
            this.hourPlusButton.Location = new System.Drawing.Point(329, 7);
            this.hourPlusButton.Name = "hourPlusButton";
            this.hourPlusButton.Size = new System.Drawing.Size(23, 23);
            this.hourPlusButton.TabIndex = 14;
            this.hourPlusButton.Text = "+";
            this.hourPlusButton.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(176, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 25);
            this.label9.TabIndex = 8;
            this.label9.Text = "/";
            // 
            // hourText
            // 
            this.hourText.Location = new System.Drawing.Point(295, 9);
            this.hourText.Name = "hourText";
            this.hourText.Size = new System.Drawing.Size(31, 20);
            this.hourText.TabIndex = 13;
            // 
            // yearText
            // 
            this.yearText.Location = new System.Drawing.Point(193, 9);
            this.yearText.Name = "yearText";
            this.yearText.Size = new System.Drawing.Size(31, 20);
            this.yearText.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(351, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 25);
            this.label11.TabIndex = 12;
            this.label11.Text = ":";
            // 
            // yearPlusButton
            // 
            this.yearPlusButton.Location = new System.Drawing.Point(227, 7);
            this.yearPlusButton.Name = "yearPlusButton";
            this.yearPlusButton.Size = new System.Drawing.Size(23, 23);
            this.yearPlusButton.TabIndex = 10;
            this.yearPlusButton.Text = "+";
            this.yearPlusButton.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Gray;
            this.label12.Location = new System.Drawing.Point(263, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Hora";
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.AutoScrollMinSize = new System.Drawing.Size(5, 5);
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Margin = new System.Windows.Forms.Padding(0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(984, 373);
            this.panelContent.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 445);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelMenu);
            this.Name = "MainWindow";
            this.Text = "FlexCore";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.tableHeader.ResumeLayout(false);
            this.menuItems.ResumeLayout(false);
            this.menuItems.PerformLayout();
            this.mainMenuPanel.ResumeLayout(false);
            this.mainMenuPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label mainItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label personsMenu;
        private System.Windows.Forms.TableLayoutPanel tableHeader;
        private System.Windows.Forms.Panel menuItems;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel mainMenuPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox versionComboBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button secPlusButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox secText;
        private System.Windows.Forms.TextBox dayText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button dayPlusButton;
        private System.Windows.Forms.Button minPlusButton;
        private System.Windows.Forms.TextBox monthText;
        private System.Windows.Forms.TextBox minText;
        private System.Windows.Forms.Button monthPlusButton;
        private System.Windows.Forms.Button hourPlusButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox hourText;
        private System.Windows.Forms.TextBox yearText;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button yearPlusButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panelContent;

    }
}

