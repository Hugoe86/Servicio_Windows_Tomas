namespace Formulario_Tomas
{
    partial class Frm_Pruebas
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
            this.Btn_Prueba = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Prueba
            // 
            this.Btn_Prueba.Location = new System.Drawing.Point(32, 22);
            this.Btn_Prueba.Name = "Btn_Prueba";
            this.Btn_Prueba.Size = new System.Drawing.Size(138, 23);
            this.Btn_Prueba.TabIndex = 0;
            this.Btn_Prueba.Text = "Prueba";
            this.Btn_Prueba.UseVisualStyleBackColor = true;
            this.Btn_Prueba.Click += new System.EventHandler(this.Btn_Prueba_Click);
            // 
            // Frm_Pruebas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 64);
            this.Controls.Add(this.Btn_Prueba);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Pruebas";
            this.Text = "Pruebas";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Prueba;
    }
}

