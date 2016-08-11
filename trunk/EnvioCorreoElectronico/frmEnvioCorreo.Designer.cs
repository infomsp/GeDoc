namespace EnvioCorreoElectronico
{
    partial class frmEnvioCorreo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnvioCorreo));
            this.pgbProgreso = new System.Windows.Forms.ProgressBar();
            this.lblProgreso = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbProgreso2 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pgbProgreso
            // 
            this.pgbProgreso.Location = new System.Drawing.Point(112, 35);
            this.pgbProgreso.Name = "pgbProgreso";
            this.pgbProgreso.Size = new System.Drawing.Size(389, 23);
            this.pgbProgreso.TabIndex = 0;
            this.pgbProgreso.UseWaitCursor = true;
            this.pgbProgreso.Value = 50;
            // 
            // lblProgreso
            // 
            this.lblProgreso.AutoSize = true;
            this.lblProgreso.Location = new System.Drawing.Point(110, 17);
            this.lblProgreso.Name = "lblProgreso";
            this.lblProgreso.Size = new System.Drawing.Size(161, 13);
            this.lblProgreso.TabIndex = 1;
            this.lblProgreso.Text = "Enviando Correos Electrónicos...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EnvioCorreoElectronico.Properties.Resources.Vista__274_;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(-26, -26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 165);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pbProgreso2
            // 
            this.pbProgreso2.Location = new System.Drawing.Point(112, 64);
            this.pbProgreso2.Name = "pbProgreso2";
            this.pbProgreso2.Size = new System.Drawing.Size(389, 23);
            this.pbProgreso2.TabIndex = 3;
            this.pbProgreso2.UseWaitCursor = true;
            this.pbProgreso2.Value = 50;
            // 
            // frmEnvioCorreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 102);
            this.ControlBox = false;
            this.Controls.Add(this.pbProgreso2);
            this.Controls.Add(this.lblProgreso);
            this.Controls.Add(this.pgbProgreso);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmEnvioCorreo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Envío Automático de Correos Electrónicos";
            this.Load += new System.EventHandler(this.frmEnvioCorreo_Load);
            this.Shown += new System.EventHandler(this.frmEnvioCorreo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbProgreso;
        private System.Windows.Forms.Label lblProgreso;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar pbProgreso2;
    }
}

