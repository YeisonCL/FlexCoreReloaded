namespace FlexCore.persons
{
    partial class Person
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Person));
            this.photo = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.Label();
            this.typeText = new System.Windows.Forms.Label();
            this.idCradTitle = new System.Windows.Forms.Label();
            this.cifTtitle = new System.Windows.Forms.Label();
            this.idCardText = new System.Windows.Forms.Label();
            this.cifText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // photo
            // 
            this.photo.BackColor = System.Drawing.Color.Transparent;
            this.photo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.photo.Image = ((System.Drawing.Image)(resources.GetObject("photo.Image")));
            this.photo.Location = new System.Drawing.Point(5, 5);
            this.photo.Margin = new System.Windows.Forms.Padding(5);
            this.photo.Name = "photo";
            this.photo.Size = new System.Drawing.Size(150, 150);
            this.photo.TabIndex = 0;
            this.photo.Click += new System.EventHandler(this.Person_Click);
            this.photo.MouseEnter += new System.EventHandler(this.Person_MouseEnter);
            this.photo.MouseLeave += new System.EventHandler(this.Person_MouseLeave);
            // 
            // nameText
            // 
            this.nameText.AutoSize = true;
            this.nameText.Font = new System.Drawing.Font("Microsoft MHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.nameText.Location = new System.Drawing.Point(163, 14);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(268, 28);
            this.nameText.TabIndex = 1;
            this.nameText.Text = "Nombre Apellido1 Apellido2";
            this.nameText.Click += new System.EventHandler(this.Person_Click);
            this.nameText.MouseEnter += new System.EventHandler(this.Person_MouseEnter);
            this.nameText.MouseLeave += new System.EventHandler(this.Person_MouseLeave);
            // 
            // typeText
            // 
            this.typeText.AutoSize = true;
            this.typeText.Cursor = System.Windows.Forms.Cursors.Default;
            this.typeText.Font = new System.Drawing.Font("Microsoft MHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeText.ForeColor = System.Drawing.Color.Gray;
            this.typeText.Location = new System.Drawing.Point(164, 42);
            this.typeText.Name = "typeText";
            this.typeText.Size = new System.Drawing.Size(120, 21);
            this.typeText.TabIndex = 2;
            this.typeText.Text = "Tipo de persona";
            this.typeText.MouseEnter += new System.EventHandler(this.Person_MouseEnter);
            this.typeText.MouseLeave += new System.EventHandler(this.Person_MouseLeave);
            // 
            // idCradTitle
            // 
            this.idCradTitle.AutoSize = true;
            this.idCradTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.idCradTitle.Font = new System.Drawing.Font("Microsoft MHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idCradTitle.ForeColor = System.Drawing.Color.Gray;
            this.idCradTitle.Location = new System.Drawing.Point(165, 86);
            this.idCradTitle.Name = "idCradTitle";
            this.idCradTitle.Size = new System.Drawing.Size(62, 21);
            this.idCradTitle.TabIndex = 3;
            this.idCradTitle.Text = "Cédula:";
            this.idCradTitle.Click += new System.EventHandler(this.label4_Click);
            this.idCradTitle.MouseEnter += new System.EventHandler(this.Person_MouseEnter);
            this.idCradTitle.MouseLeave += new System.EventHandler(this.Person_MouseLeave);
            // 
            // cifTtitle
            // 
            this.cifTtitle.AutoSize = true;
            this.cifTtitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.cifTtitle.Font = new System.Drawing.Font("Microsoft MHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cifTtitle.ForeColor = System.Drawing.Color.Gray;
            this.cifTtitle.Location = new System.Drawing.Point(165, 113);
            this.cifTtitle.Name = "cifTtitle";
            this.cifTtitle.Size = new System.Drawing.Size(36, 21);
            this.cifTtitle.TabIndex = 4;
            this.cifTtitle.Text = "CIF:";
            this.cifTtitle.MouseEnter += new System.EventHandler(this.Person_MouseEnter);
            this.cifTtitle.MouseLeave += new System.EventHandler(this.Person_MouseLeave);
            // 
            // idCardText
            // 
            this.idCardText.AutoSize = true;
            this.idCardText.Cursor = System.Windows.Forms.Cursors.Default;
            this.idCardText.Font = new System.Drawing.Font("Microsoft MHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idCardText.ForeColor = System.Drawing.Color.Gray;
            this.idCardText.Location = new System.Drawing.Point(233, 86);
            this.idCardText.Name = "idCardText";
            this.idCardText.Size = new System.Drawing.Size(81, 21);
            this.idCardText.TabIndex = 5;
            this.idCardText.Text = "123456789";
            this.idCardText.Click += new System.EventHandler(this.label6_Click);
            this.idCardText.MouseEnter += new System.EventHandler(this.Person_MouseEnter);
            this.idCardText.MouseLeave += new System.EventHandler(this.Person_MouseLeave);
            // 
            // cifText
            // 
            this.cifText.AutoSize = true;
            this.cifText.Cursor = System.Windows.Forms.Cursors.Default;
            this.cifText.Font = new System.Drawing.Font("Microsoft MHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cifText.ForeColor = System.Drawing.Color.Gray;
            this.cifText.Location = new System.Drawing.Point(207, 113);
            this.cifText.Name = "cifText";
            this.cifText.Size = new System.Drawing.Size(81, 21);
            this.cifText.TabIndex = 6;
            this.cifText.Text = "987654321";
            this.cifText.MouseEnter += new System.EventHandler(this.Person_MouseEnter);
            this.cifText.MouseLeave += new System.EventHandler(this.Person_MouseLeave);
            // 
            // Person
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cifText);
            this.Controls.Add(this.idCardText);
            this.Controls.Add(this.cifTtitle);
            this.Controls.Add(this.idCradTitle);
            this.Controls.Add(this.typeText);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.photo);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximumSize = new System.Drawing.Size(725, 160);
            this.MinimumSize = new System.Drawing.Size(725, 160);
            this.Name = "Person";
            this.Size = new System.Drawing.Size(725, 160);
            this.Load += new System.EventHandler(this.Person_Load);
            this.Click += new System.EventHandler(this.Person_Click);
            this.MouseEnter += new System.EventHandler(this.Person_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Person_MouseLeave);
            this.MouseHover += new System.EventHandler(this.Person_MouseHover);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label photo;
        private System.Windows.Forms.Label nameText;
        private System.Windows.Forms.Label typeText;
        private System.Windows.Forms.Label idCradTitle;
        private System.Windows.Forms.Label cifTtitle;
        private System.Windows.Forms.Label idCardText;
        private System.Windows.Forms.Label cifText;
    }
}
