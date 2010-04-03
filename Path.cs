
/*****************************************************************************
 *                                                                           *
 *  Path.cs                                                                  *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that represents a connection from one element to another.        *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Drawing;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    [Serializable]
    public class Path : Entity, IConnectionConduit<Element, Element, Path>
    {
        public Path()
        {
            //_pathJoints = new PathPathJointChildrenCollection(this);
        }

        public override RectangleV GetBoundingBox()
        {
            if (PathJoints.Count < 1) { return new RectangleV(); }

            return RectangleV.BoundingBoxFromPoints(PathJoints.ToArray());
        }

        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
        {
            Render(g, pen, brush, font, true, true);
        }
        public virtual void Render(Graphics g, Pen pen, Brush brush, Font font, bool renderPathJoints, bool renderArrow)
        {
            if (PathJoints.Count >= 2)
            {
                int n = PathJoints.Count;
                int i;
                for (i = 0; i < n - 1; i++)
                {
                    //if (renderPathJoints)
                    //{
                    //    PathJoints[i].Render(g, pen, brush, font);
                    //}
                    g.DrawLine(Pens.Black, PathJoints[i], PathJoints[i + 1]);
                }

                if (n > 1 && To != null)
                {
                    if (renderArrow)
                    {
                        RenderArrow(g, pen, brush, font, PathJoints[n - 2], PathJoints[n - 1]);
                    }
                }
                else if (n > 0)
                {
                    //if (renderPathJoints)
                    //{
                    //    PathJoints[j - 1].Render(g, pen, brush, font);
                    //}
                }
            }
            else if (From != null && To != null)
            {
                Vector from = From.GetOutboundConnectionPoint(this);
                Vector to = To.GetInboundConnectionPoint(this);

                g.DrawLine(pen, from, to);

                if (renderArrow)
                {
                    RenderArrow(g, pen, brush, font, from, to);
                }
            }
            else if (From != null)
            {
                Vector from = From.GetOutboundConnectionPoint(this);
                Vector center = From.GetCenterOfBox();

                g.DrawLine(pen, from, 10 * (from - center).Normalized());
            }
            else if (To != null)
            {
                Vector to = To.GetInboundConnectionPoint(this);
                Vector center = To.GetCenterOfBox();

                g.DrawLine(pen, 10 * (to - center).Normalized(), to);
            }
            //else if (PathJoints.Count == 1)
            //{
            //    PathJoints[0].Render(g, pen, brush, font);
            //}
            else
            {
                //no from, no to, no pathjoints
                //don't render anything
            }
        }

        public virtual void RenderArrow(Graphics g, Pen pen, Brush brush, Font font, Vector startLocation, Vector tipLocation)
        {

            Vector[] r = { new Vector(), new Vector(), new Vector() };
            Vector p;

            float angle;
            float size;

            size = ArrowSize;

            p = tipLocation - startLocation;

            angle = p.CalcTheta();
            r[0] = tipLocation;
            r[1] = Vector.FromPolar(angle + (float)(Math.PI * 5.0 / 6.0), size) + r[0];
            r[2] = Vector.FromPolar(angle - (float)(Math.PI * 5.0 / 6.0), size) + r[0];

            g.FillPolygon(pen.Brush, Vector.ConvertArray(r));
        }

        //private float _arrowSize;

        public float ArrowSize
        {
            get { return 10; }
            protected set { }
        }


        public virtual List<Vector> PathJoints
        {
            get { return _pathJoints; }
        }

        public virtual Element To
        {
            get { return _to; }
            set
            {
                if (_to != value)
                {
                    this.OnToChanging(new EventArgs());

                    if (_to != null)
                    {
                        _to.Inbound.Remove(this);
                    }

                    _to = value;

                    if (_to != null)
                    {
                        _to.Inbound.Add(this);
                    }

                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.RoutePath(this);
                    }

                    this.OnToChanged(new EventArgs());
                }
            }
        }

        public virtual Element From
        {
            get { return _from; }
            set
            {
                if (_from != value)
                {
                    this.OnFromChanging(new EventArgs());

                    if (_from != null)
                    {
                        _from.Outbound.Remove(this);
                    }

                    _from = value;

                    if (_from != null)
                    {
                        _from.Outbound.Add(this);
                    }

                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.RoutePath(this);
                    }

                    this.OnFromChanged(new EventArgs());
                }
            }
        }

        //[MetaphysicsIndustries.Serialization.Serializable(false)]
        //CrystallineControl _parentCrystallineControl;
        public override CrystallineControl ParentCrystallineControl
        {
            get { return base.ParentCrystallineControl; }
            set
            {
                if (ParentCrystallineControl != value)
                {
                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.Paths.Remove(this);
                    }

                    SetParentCrystallineControl(value);

                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.Paths.Add(this);
                    }
                }
            }
        }

        public event EventHandler ToChanging;

        public event EventHandler ToChanged;

        public event EventHandler FromChanged;

        public event EventHandler FromChanging;

        protected virtual void OnFromChanged(EventArgs e)
        {
            if (FromChanged != null)
            {
                this.FromChanged(this, e);
            }
        }

        protected virtual void OnToChanged(EventArgs e)
        {
            if (ToChanged != null)
            {
                this.ToChanged(this, e);
            }
        }

        protected virtual void OnFromChanging(EventArgs e)
        {
            if (FromChanging != null)
            {
                this.FromChanging(this, e);
            }
        }

        protected virtual void OnToChanging(EventArgs e)
        {
            if (ToChanging != null)
            {
                this.ToChanging(this, e);
            }
        }

        private List<Vector> _pathJoints = new List<Vector>();
        private Element _to;
        private Element _from;
    }
}
