using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MetaphysicsIndustries.Crystalline
{
    public abstract partial class CrystallineAppForm<T> : Form
        where T : CrystallineControl, new()
    {
        public CrystallineAppForm()
        {
            InitializeComponent();

            this.crystallineControl1 = InitControl();

            openFileDialog1.Filter = Filter;
            openFileDialog1.DefaultExt = DefaultExtension;

            saveFileDialog1.Filter = Filter;
            saveFileDialog1.DefaultExt = DefaultExtension;
        }


        protected virtual T InitControl()
        {
            T control = new T();

            this.toolStripContainer1.ContentPanel.Controls.Add(control);

            control.AutoScroll = true;
            control.AutoScrollMinSize = new System.Drawing.Size(142, 142);
            control.BackColor = System.Drawing.SystemColors.Window;
            control.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            control.BoxCollisions = true;
            control.Dock = System.Windows.Forms.DockStyle.Fill;
            control.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            control.Location = new System.Drawing.Point(0, 0);
            control.Name = "crystallineControl1";
            control.ShallRenderElements = true;
            control.ShallRenderPaths = true;
            control.ShowDebugInfo = false;
            control.ShowPathArrows = true;
            control.ShowPathJoints = false;
            control.Size = new System.Drawing.Size(292, 249);
            control.TabIndex = 0;

            return control;
        }

        private T crystallineControl1;

        protected T CrystallineControl
        {
            get { return crystallineControl1; }
        }

        protected abstract string DefaultExtension
        {
            get;
        }
        protected abstract string Filter
        {
            get;
        }
        protected abstract string AppTitle
        {
            get;
        }

        private void CrystallineAppForm_Load(object sender, EventArgs e)
        {
            CurrentFilename = string.Empty;
            FileHasChanged = false;

            string[] args = Environment.GetCommandLineArgs();
            if (args != null && args.Length > 1)
            {
                string filename = args[1];
                OpenFile(filename);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (FileHasChanged)
            {
                DialogResult r = MessageBox.Show(this, "The diagram has changed.\r\n\r\nDo you want to save the changes?", "Amethyst", MessageBoxButtons.YesNoCancel);

                if (r == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (r == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(CurrentFilename))
                    {
                        if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                        {
                            e.Cancel = true;
                            return;
                        }
                        CurrentFilename = saveFileDialog1.FileName;
                    }

                    SaveFile(CurrentFilename);
                }
            }
            
            base.OnFormClosing(e);
        }
    }
}