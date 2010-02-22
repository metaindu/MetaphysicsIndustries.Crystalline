
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

namespace MetaphysicsIndustries.Crystalline
{
    [Serializable]
    public class Path : Entity, IConnectionConduit<Element, Element, Path>
	{
		public Path()
		{
			_pathJoints = new PathPathJointChildrenCollection(this);
		}

        public override RectangleF GetBoundingBox()
        {
            if (PathJoints.Count < 1) { return new RectangleF(); }

            RectangleF rect = new RectangleF(PathJoints[0].Location, new SizeF(0, 0));

            foreach (PathJoint pj in PathJoints)
            {
                rect = RectangleF.Union(rect, new RectangleF(pj.Location, new SizeF(0, 0)));
            }

            return rect;
        }

        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
        {
            Render(g, pen, brush, font, true, true);
        }
        public virtual void Render(Graphics g, Pen pen, Brush brush, Font font, bool renderPathJoints, bool renderArrow)
        {
            int i;
            int j;
            j = PathJoints.Count;
            for (i = 0; i < j - 1; i++)
            {
                if (renderPathJoints)
                {
                    PathJoints[i].Render(g, pen, brush, font);
                }
                g.DrawLine(Pens.Black, PathJoints[i].Location, PathJoints[i + 1].Location);
            }

            if (j > 1 && To != null)
            {
                if (renderArrow)
                {
                    //array<PointF>^	r = { PointF(), PointF(), PointF() };
                    //PointF	p;
                    //float	angle;
                    //float	angle2;
                    //float	size;

                    //size = 10;

                    //p = this->PathJoints[j - 1]->Location - SizeF(this->PathJoints[j - 2]->Location);
                    //angle = (float)Math::Atan2(p.Y, p.X);

                    //r[0] = this->PathJoints[j - 1]->Location;
                    //angle2 = angle + (float)(Math::PI * 5.0 / 6.0);
                    //r[1] = PointF(size * (float)Math::Cos(angle2), size * (float)Math::Sin(angle2)) + SizeF(r[0]);
                    //angle2 = angle - (float)(Math::PI * 5.0 / 6.0);
                    //r[2] = PointF(size * (float)Math::Cos(angle2), size * (float)Math::Sin(angle2)) + SizeF(r[0]);

                    //g->FillPolygon(Brushes::Black, r);

                    RenderArrow(g, pen, brush, font, PathJoints[j - 2].Location, PathJoints[j - 1].Location);
                }
            }
            else if (j > 0)
            {
                if (renderPathJoints)
                {
                    PathJoints[j - 1].Render(g, pen, brush, font);
                }
            }
        }

        public virtual void RenderArrow(Graphics g, Pen pen, Brush brush, Font font, PointF previousJointLocation, PointF tipLocation)
        {

            PointF[] r = { new PointF(), new PointF(), new PointF() };
            PointF p;

            float angle;
            float angle2;
            float size;

            size = ArrowSize;

            p = tipLocation - new SizeF(previousJointLocation);

            angle = (float)Math.Atan2(p.Y, p.X);
            r[0] = tipLocation;
            angle2 = angle + (float)(Math.PI * 5.0 / 6.0);
            r[1] = new PointF(size * (float)Math.Cos(angle2), size * (float)Math.Sin(angle2)) + new SizeF(r[0]);
            angle2 = angle - (float)(Math.PI * 5.0 / 6.0);
            r[2] = new PointF(size * (float)Math.Cos(angle2), size * (float)Math.Sin(angle2)) + new SizeF(r[0]);

            g.FillPolygon(pen.Brush, r);
        }

        //private float _arrowSize;

        public float ArrowSize
        {
            get { return 10; }
            protected set { }
        }


        public virtual PathPathJointChildrenCollection PathJoints
		{
			get
			{
				return _pathJoints;
			}
		}

		public virtual Element To
		{
			get
			{
				return _to;
			}
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
			get
			{
				return _from;
			}
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

		public  event EventHandler ToChanging;

		public  event EventHandler ToChanged;

		public  event EventHandler FromChanged;

		public  event EventHandler FromChanging;

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

        private PathPathJointChildrenCollection _pathJoints;
        private Element _to;
        private Element _from;
	}
}
