
namespace Presentacion
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFormCliente = new System.Windows.Forms.Button();
            this.btnFormArticulo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFormCliente
            // 
            this.btnFormCliente.Location = new System.Drawing.Point(250, 122);
            this.btnFormCliente.Name = "btnFormCliente";
            this.btnFormCliente.Size = new System.Drawing.Size(266, 47);
            this.btnFormCliente.TabIndex = 0;
            this.btnFormCliente.Text = "Formulario Clientes";
            this.btnFormCliente.UseVisualStyleBackColor = true;
            this.btnFormCliente.Click += new System.EventHandler(this.btnFormCliente_Click);
            // 
            // btnFormArticulo
            // 
            this.btnFormArticulo.Location = new System.Drawing.Point(250, 190);
            this.btnFormArticulo.Name = "btnFormArticulo";
            this.btnFormArticulo.Size = new System.Drawing.Size(266, 47);
            this.btnFormArticulo.TabIndex = 1;
            this.btnFormArticulo.Text = "Formulario Artículos";
            this.btnFormArticulo.UseVisualStyleBackColor = true;
            this.btnFormArticulo.Click += new System.EventHandler(this.btnFormArticulo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnFormCliente);
            this.Controls.Add(this.btnFormArticulo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFormCliente;
        private System.Windows.Forms.Button btnFormArticulo;
    }
}

