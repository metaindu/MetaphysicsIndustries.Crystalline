
/*****************************************************************************
 *                                                                           *
 *  Element.cs                                                               *
 *  23 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that represents the visual aspects of the elements which are     *
 *    connected together in the graph.                                       *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Drawing;

namespace MetaphysicsIndustries.Crystalline
{
    [Serializable]
    public class Element : Box, IConnector<Element, Element, Path>, IConnectee<Element, Element, Path>
	{
		public Element()
		{
            //_subElements = new ElementSubElementParentChildrenCollection(this);
			_inbound = new InboundToElementPathChildrenCollection(this);
			_outbound = new OutboundFromElementPathChildrenCollection(this);

            Size = new SizeF(50, 20);
		}

        //public override void Dispose()
        //{
        //    //_subElements.Clear();
        //    //_subElements.Dispose();

        //    //_inbound.Clear();
        //    _inbound.Dispose();

        //    //_outbound.Clear();
        //    _outbound.Dispose();

        //    base.Dispose();
        //}

        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
        {
            Graphics _g = g;
			float	min;
			float	max;

            //extract all debug stuff out of entity classes and into crystallinecontrol classes
            if (Framework.ParentControl.ShowDebugInfo)
            {
                foreach (Box ib in this.LeftNeighbors)
                {
                    min = Math.Max(this.Top, ib.Top);
                    max = Math.Min(this.Bottom, ib.Bottom);
                    min = (min + max) / 2;
                    min++;
                    g.DrawLine(Pens.Blue, this.Left, min, ib.Right, min);
                }
                foreach (Box ib in this.UpNeighbors)
                {
                    min = Math.Max(this.Left, ib.Left);
                    max = Math.Min(this.Right, ib.Right);
                    min = (min + max) / 2;
                    min++;
                    g.DrawLine(Pens.Blue, min, this.Top, min, ib.Bottom);
                }
                foreach (Box ib in this.RightNeighbors)
                {
                    min = Math.Max(this.Top, ib.Top);
                    max = Math.Min(this.Bottom, ib.Bottom);
                    min = (min + max) / 2;
                    min--;
                    g.DrawLine(Pens.Orange, this.Right, min, ib.Left, min);
                }
                foreach (Box ib in this.DownNeighbors)
                {
                    min = Math.Max(this.Left, ib.Left);
                    max = Math.Min(this.Right, ib.Right);
                    min = (min + max) / 2;
                    min--;
                    g.DrawLine(Pens.Orange, min, this.Bottom, min, ib.Top);
                }
            }

            base.Render(g, pen, brush, font);
		}

        protected override void RenderText(Graphics g, Pen pen, Brush brush, Font font)
        {
            //if (Framework.ParentControl.ShowDebugInfo)
            //{
                base.RenderText(g, pen, brush, font);
            //}
        }

		public virtual InboundToElementPathChildrenCollection Inbound
		{
			get
			{
				return _inbound;
			}
            protected set
            {
                _inbound = value;
            }
		}

        //public virtual object Param
        //{
        //    get
        //    {
        //        return _param;
        //    }
        //    set
        //    {
        //        if (_param != value)
        //        {
        //            this.OnParamChanging(new EventArgs());
        //            _param = value;
        //            this.OnParamChanged(new EventArgs());
        //        }
        //    }
        //}

		public virtual OutboundFromElementPathChildrenCollection Outbound
		{
			get
			{
				return _outbound;
			}
            protected set
            {
                _outbound = value;
            }
		}

		public virtual float TerminalSpacing
		{
			get
			{
				return 10;
			}
		}

		public  event EventHandler LocationChanged;

		public  event EventHandler SizeChanging;

		public  event EventHandler LocationChanging;

		public  event EventHandler SizeChanged;

		protected virtual void OnSizeChanging(EventArgs e)
		{
            if (SizeChanging != null)
            {
                this.SizeChanging(this, e);
            }
		}

		protected virtual void OnLocationChanging(EventArgs e)
		{
            if (LocationChanging != null)
            {
                this.LocationChanging(this, e);
            }
		}

		protected virtual void OnSizeChanged(EventArgs e)
		{
            if (SizeChanged != null)
            {
                this.SizeChanged(this, e);
            }
		}

		protected virtual void OnLocationChanged(EventArgs e)
		{
            if (LocationChanged != null)
            {
                this.LocationChanged(this, e);
            }
		}

        private InboundToElementPathChildrenCollection _inbound;
        private OutboundFromElementPathChildrenCollection _outbound;

        public virtual PointF GetOutboundConnectionPoint(Path path)
        {
            return new PointF(Rect.Right, Location.Y + Size.Height / 2);
        }

        public virtual PointF GetInboundConnectionPoint(Path path)
        {
            return new PointF(Location.X, Location.Y + path.To.Size.Height / 2);
        }

        //private ElementSubElementParentChildrenCollection _subElements;
        //public virtual ElementSubElementParentChildrenCollection SubElements
        //{
        //    get { return _subElements; }
        //    protected set { _subElements = value; }
        //}

        public virtual bool CanBeMoved
        {
            get { return true; }
        }

        public virtual bool ShallProcessDoubleClick
        {
            get { return false; }
        }

        public virtual void ProcessDoubleClick(CrystallineControl control)
        {
        }


    }
}
