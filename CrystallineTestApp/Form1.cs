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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Element _rotationTestElement = new Element();

        private void Form1_Load(object sender, EventArgs e)
        {
            //_rotationTestElement.Location = new PointF(100, 100);
            //_rotationTestElement.Text = "Rotate";
            //crystallineControl1.AddElement(_rotationTestElement);
        }

        private void crystallineControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //_rotationTestElement.Rotation += 90;
            //crystallineControl1.Invalidate();
        }
    }
}