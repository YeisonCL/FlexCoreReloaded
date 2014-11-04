namespace FlexCore.persons
{
    partial class PhotoField
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
            this.photo = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // photo
            // 
            this.photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.photo.Location = new System.Drawing.Point(9, 10);
            this.photo.Margin = new System.Windows.Forms.Padding(0);
            this.photo.Name = "photo";
            this.photo.Size = new System.Drawing.Size(200, 200);
            this.photo.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.AutoSize = true;
            this.searchButton.Font = new System.Drawing.Font("Microsoft MHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.ForeColor = System.Drawing.Color.Blue;
            this.searchButton.Location = new System.Drawing.Point(169, 210);
            this.searchButton.MaximumSize = new System.Drawing.Size(600, 1000000);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(40, 17);
            this.searchButton.TabIndex = 11;
            this.searchButton.Text = "editar";
            // 
            // PhotoField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.photo);
            this.Name = "PhotoField";
            this.Size = new System.Drawing.Size(219, 234);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label photo;
        private System.Windows.Forms.Label searchButton;
    }
}
