using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineContainerForm : Form
    {
        public CrystallineContainerForm(CrystallineControl control)
            : this(control, "Crystalline")
        {
        }

        public CrystallineContainerForm(CrystallineControl control, string caption)
        {
            if (control == null) { throw new ArgumentNullException("control"); }

            InitializeComponent();

            _control = control;

            Controls.Add(_control);
            _control.Dock = System.Windows.Forms.DockStyle.Fill;

            Text = caption;
        }

        CrystallineControl _control;
    }
}