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
            this.label4 = new System.Windows.Forms.Label();
            this.mainMenuPanel = new System.Windows.Forms.Panel();
            this.mainItem = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.tableHeader.SuspendLayout();
            this.menuItems.SuspendLayout();
            this.mainMenuPanel.SuspendLayout();
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
            this.tableHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 392F));
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
            this.menuItems.Controls.Add(this.label4);
            this.menuItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuItems.Location = new System.Drawing.Point(595, 3);
            this.menuItems.Name = "menuItems";
            this.menuItems.Size = new System.Drawing.Size(386, 31);
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
            // 
            // personsMenu
            // 
            this.personsMenu.AutoSize = true;
            this.personsMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.personsMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personsMenu.ForeColor = System.Drawing.Color.White;
            this.personsMenu.Location = new System.Drawing.Point(311, 9);
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
            this.label3.Location = new System.Drawing.Point(235, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Clientes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(74, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Reportes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(158, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cuentas";
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
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.AutoScrollMinSize = new System.Drawing.Size(5, 5);
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 37);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(984, 363);
            this.panelContent.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 400);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.Name = "MainWindow";
            this.Text = "FlexCore";
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.tableHeader.ResumeLayout(false);
            this.menuItems.ResumeLayout(false);
            this.menuItems.PerformLayout();
            this.mainMenuPanel.ResumeLayout(false);
            this.mainMenuPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label mainItem;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label personsMenu;
        private System.Windows.Forms.TableLayoutPanel tableHeader;
        private System.Windows.Forms.Panel menuItems;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel mainMenuPanel;

    }
}

