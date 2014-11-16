namespace FlexCore.persons
{
    partial class PersonData
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.itemText = new System.Windows.Forms.Label();
            this.editValue = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.editOption1 = new System.Windows.Forms.Label();
            this.editOption2 = new System.Windows.Forms.Label();
            this.eraseOption = new System.Windows.Forms.Label();
            this.itemTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Label();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.itemText);
            this.flowLayoutPanel2.Controls.Add(this.editValue);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 22);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(600, 31);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // itemText
            // 
            this.itemText.AutoSize = true;
            this.itemText.Font = new System.Drawing.Font("Microsoft MHei", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.itemText.Location = new System.Drawing.Point(3, 0);
            this.itemText.MaximumSize = new System.Drawing.Size(600, 1000000);
            this.itemText.Name = "itemText";
            this.itemText.Size = new System.Drawing.Size(47, 22);
            this.itemText.TabIndex = 7;
            this.itemText.Text = "valor";
            // 
            // editValue
            // 
            this.editValue.Font = new System.Drawing.Font("Microsoft MHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editValue.Location = new System.Drawing.Point(56, 3);
            this.editValue.Name = "editValue";
            this.editValue.Size = new System.Drawing.Size(494, 25);
            this.editValue.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.editOption1);
            this.flowLayoutPanel1.Controls.Add(this.editOption2);
            this.flowLayoutPanel1.Controls.Add(this.eraseOption);
            this.flowLayoutPanel1.Controls.Add(this.cancelButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 53);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(600, 22);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // editOption1
            // 
            this.editOption1.AutoSize = true;
            this.editOption1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editOption1.Font = new System.Drawing.Font("Microsoft MHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editOption1.ForeColor = System.Drawing.Color.Blue;
            this.editOption1.Location = new System.Drawing.Point(3, 0);
            this.editOption1.MaximumSize = new System.Drawing.Size(600, 1000000);
            this.editOption1.Name = "editOption1";
            this.editOption1.Size = new System.Drawing.Size(40, 17);
            this.editOption1.TabIndex = 9;
            this.editOption1.Text = "editar";
            this.editOption1.Click += new System.EventHandler(this.editButton_Click);
            // 
            // editOption2
            // 
            this.editOption2.AutoSize = true;
            this.editOption2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editOption2.Font = new System.Drawing.Font("Microsoft MHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editOption2.ForeColor = System.Drawing.Color.Blue;
            this.editOption2.Location = new System.Drawing.Point(49, 0);
            this.editOption2.MaximumSize = new System.Drawing.Size(600, 1000000);
            this.editOption2.Name = "editOption2";
            this.editOption2.Size = new System.Drawing.Size(51, 17);
            this.editOption2.TabIndex = 10;
            this.editOption2.Text = "guardar";
            this.editOption2.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // eraseOption
            // 
            this.eraseOption.AutoSize = true;
            this.eraseOption.Cursor = System.Windows.Forms.Cursors.Hand;
            this.eraseOption.Font = new System.Drawing.Font("Microsoft MHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eraseOption.ForeColor = System.Drawing.Color.Blue;
            this.eraseOption.Location = new System.Drawing.Point(106, 0);
            this.eraseOption.MaximumSize = new System.Drawing.Size(600, 1000000);
            this.eraseOption.Name = "eraseOption";
            this.eraseOption.Size = new System.Drawing.Size(41, 17);
            this.eraseOption.TabIndex = 11;
            this.eraseOption.Text = "borrar";
            this.eraseOption.Click += new System.EventHandler(this.eraseOption_Click);
            // 
            // itemTitle
            // 
            this.itemTitle.AutoSize = true;
            this.itemTitle.Font = new System.Drawing.Font("Microsoft MHei", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.itemTitle.Location = new System.Drawing.Point(3, 0);
            this.itemTitle.MaximumSize = new System.Drawing.Size(600, 100000);
            this.itemTitle.Name = "itemTitle";
            this.itemTitle.Size = new System.Drawing.Size(68, 22);
            this.itemTitle.TabIndex = 5;
            this.itemTitle.Text = "Campo:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.itemTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 75);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSize = true;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft MHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.Blue;
            this.cancelButton.Location = new System.Drawing.Point(153, 0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(600, 1000000);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 17);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "cancelar";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // PersonData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(600, 100000);
            this.MinimumSize = new System.Drawing.Size(600, 0);
            this.Name = "PersonData";
            this.Size = new System.Drawing.Size(600, 75);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label itemText;
        private System.Windows.Forms.TextBox editValue;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label editOption1;
        private System.Windows.Forms.Label editOption2;
        private System.Windows.Forms.Label itemTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label eraseOption;
        private System.Windows.Forms.Label cancelButton;

    }
}
