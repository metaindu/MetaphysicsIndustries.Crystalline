using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetaphysicsIndustries.Crystalline;

namespace CrystallineTestApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //CrystallineAppForm<CrystallineControl> form = new CrystallineAppForm<CrystallineControl>();
            //form.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}