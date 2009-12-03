
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.cs                                                    *
 *  19 March 2007/10 November 2007                                           *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MetaphysicsIndustries.Collections;
using System.Diagnostics;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineControl : UserControl
    {

        public CrystallineControl()
        {
            //int atdValue = 121;
            //ConvertToTemp(atdValue);
            //int[] temps = new int[256];
            //for (int i = 0; i < 256; i++)
            //{
            //    temps[i] = ConvertToTemp(i);
            //}


            InitializeComponent();

            //BackColor = Color.White;
            //BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //AutoScrollMargin = new Size(25, 25);

            _engine = InitEngine();

            InitEntities();

            InitFunctionalities();

            UpdateScrolls();
        }

        protected virtual CrystallineEngine InitEngine()
        {
            return new CrystallineEngine(this);
        }

        CrystallineEngine _engine;

        //private static int ConvertToTemp(int atdValue)
        //{
        //    int Ra = 10000;
        //    int Rx = 10000;

        //    if (atdValue == 255) { return -5725; }

        //    int a = atdValue * Ra;
        //    int b = 255 - atdValue;
        //    int c = a / b;
        //    int d = c - Rx;
        //    int e = d / -440;
        //    int f = e + 25;

        //    int temp = (int)(((atdValue / (255.0f - atdValue)) * Ra - 10000) / (-440)) + 25;

        //    return f;
        //}

        public void InitFunctionalities()
        {
            //_functionalities = new CrystallineControlFunctionalityOrderedParentChildrenCollection(this);
            InternalInitFunctionalities();
        }

        protected virtual void InternalInitFunctionalities()
        {
            _menuDisplayEngine = new MenuDisplayEngine(_engine);
        }

        private void InitEntities()
        {
            _framework = new BoxFramework(this);

            _entities = new CrystallineControlEntityParentChildrenCollection(this);
            _boxes = new CrystallineControlBoxParentChildrenCollection(_entities);

            Framework.BoundsChanged += new EventHandler(Framework_BoundsChanged);

            _elements = new ElementCollection(Framework);
            _paths = new CrystallineControlPathParentChildrenCollection(_entities);
            _pathingJunctions = new PathingJunctionCollection(Framework);
            _pathways = new PathwayCollection(Framework);

            //_selectionElement = new Set<Element>();
            //_selectionPathJoint = new Set<PathJoint>();
            //_selectionPathingJunction = new Set<PathingJunction>();
            //_selectionPathway = new Set<Pathway>();

            //selectingelement = true;
            //SelectionMode = SelectionModeType.Element;

            InternalInitEntities();
        }

        protected virtual void InternalInitEntities()
        {
        }

        private MenuDisplayEngine _menuDisplayEngine;

        protected override void OnLoad(EventArgs e)
        {
            ProcessLoad();

            base.OnLoad(e);
        }

        protected virtual void ProcessLoad()
        {
            ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
            SetupContextMenuItems();


            _selectionPen = new Pen(Color.FromArgb(191, 191, 255), 2);
            //_courierFont = new System.Drawing.Font("courier new", 8);
            _selectionOutlinePen = new Pen(Color.Black);
            _selectionOutlinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            _names = new List<String>();

            _names.Add("Clive");
            _names.Add("Thomas");
            _names.Add("Frederick");
            _names.Add("Jaques");
            _names.Add("Larry");
            _names.Add("Clarence");
            _names.Add("Robert");
            _names.Add("Andrew");
            _names.Add("Jeremy");
        }
        
        [NonSerialized]
        BoxFramework _framework;
        public BoxFramework Framework
        {
            get { return _framework; }
        }
        private void Framework_BoundsChanged(object sender, EventArgs e)
        {
            UpdateScrolls();
        }

        public void ReportException(Exception ee)
        {
            MessageBox.Show(this, "There was an error: \r\n" + ee.ToString());
        }

        private double Dist(PointF p1, PointF p2)
        {
            float xx;
            float yy;
            xx = (p1.X - p2.X);
            yy = (p1.Y - p2.Y);
            return Math.Sqrt(xx * xx + yy * yy);
        }

        //private CrystallineControlFunctionalityOrderedParentChildrenCollection _functionalities;
        //public CrystallineControlFunctionalityOrderedParentChildrenCollection Functionalities
        //{
        //    get { return _functionalities; }
        //}

        List<String>				_names;
    }
}
