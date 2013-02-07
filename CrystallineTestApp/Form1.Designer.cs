namespace CrystallineTestApp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.crystallineControl1 = new MetaphysicsIndustries.Crystalline.CrystallineControl();
            this.SuspendLayout();
            // 
            // crystallineControl1
            // 
            this.crystallineControl1.AutoScroll = true;
            this.crystallineControl1.AutoScrollMinSize = new System.Drawing.Size(142, 142);
            this.crystallineControl1.BackColor = System.Drawing.Color.White;
            this.crystallineControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.crystallineControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystallineControl1.BoxCollisions = true;
            this.crystallineControl1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crystallineControl1.Location = new System.Drawing.Point(0, 0);
            this.crystallineControl1.Name = "crystallineControl1";
            this.crystallineControl1.ShallRenderElements = true;
            this.crystallineControl1.ShallRenderPaths = true;
            this.crystallineControl1.ShowDebugInfo = true;
            this.crystallineControl1.ShowPathArrows = true;
            this.crystallineControl1.ShowPathJoints = true;
            this.crystallineControl1.Size = new System.Drawing.Size(504, 485);
            this.crystallineControl1.TabIndex = 0;
            this.crystallineControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.crystallineControl1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 485);
            this.Controls.Add(this.crystallineControl1);
            this.Name = "Form1";
            this.Text = "Crystalline Test App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetaphysicsIndustries.Crystalline.CrystallineControl crystallineControl1;
    }
}