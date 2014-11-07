namespace FlexCore.closures
{
    partial class Closure
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
            this.dateText = new System.Windows.Forms.Label();
            this.stateText = new System.Windows.Forms.Label();
            this.timeText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateText
            // 
            this.dateText.AutoSize = true;
            this.dateText.Font = new System.Drawing.Font("Microsoft MHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dateText.Location = new System.Drawing.Point(16, 12);
            this.dateText.Name = "dateText";
            this.dateText.Size = new System.Drawing.Size(133, 28);
            this.dateText.TabIndex = 1;
            this.dateText.Text = "Día/Mes/Año";
            this.dateText.MouseEnter += new System.EventHandler(this.Closure_MouseEnter);
            this.dateText.MouseLeave += new System.EventHandler(this.Closure_MouseLeave);
            // 
            // stateText
            // 
            this.stateText.AutoSize = true;
            this.stateText.Cursor = System.Windows.Forms.Cursors.Default;
            this.stateText.Font = new System.Drawing.Font("Microsoft MHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stateText.ForeColor = System.Drawing.Color.Gray;
            this.stateText.Location = new System.Drawing.Point(17, 61);
            this.stateText.Name = "stateText";
            this.stateText.Size = new System.Drawing.Size(77, 21);
            this.stateText.TabIndex = 2;
            this.stateText.Text = "Completo";
            this.stateText.MouseEnter += new System.EventHandler(this.Closure_MouseEnter);
            this.stateText.MouseLeave += new System.EventHandler(this.Closure_MouseLeave);
            // 
            // timeText
            // 
            this.timeText.AutoSize = true;
            this.timeText.Cursor = System.Windows.Forms.Cursors.Default;
            this.timeText.Font = new System.Drawing.Font("Microsoft MHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeText.ForeColor = System.Drawing.Color.Gray;
            this.timeText.Location = new System.Drawing.Point(17, 40);
            this.timeText.Name = "timeText";
            this.timeText.Size = new System.Drawing.Size(176, 21);
            this.timeText.TabIndex = 3;
            this.timeText.Text = "Hora:Minutos:Segundos";
            // 
            // Closure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.timeText);
            this.Controls.Add(this.stateText);
            this.Controls.Add(this.dateText);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximumSize = new System.Drawing.Size(725, 102);
            this.MinimumSize = new System.Drawing.Size(725, 102);
            this.Name = "Closure";
            this.Size = new System.Drawing.Size(725, 102);
            this.MouseEnter += new System.EventHandler(this.Closure_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Closure_MouseLeave);
            this.MouseHover += new System.EventHandler(this.Closure_MouseHover);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dateText;
        private System.Windows.Forms.Label stateText;
        private System.Windows.Forms.Label timeText;
    }
}
