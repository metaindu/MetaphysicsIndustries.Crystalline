
/*****************************************************************************
 *                                                                           *
 *  Box.cs                                                                   *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright � 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A default implementation of the IBox interface which uses                *
 *    BoxNeighborCollection to store links to neighbors. This class can be used      *
 *    as an automator.                                                       *
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
    public abstract class Box : Entity
    {
        public override RectangleV GetBoundingBox()
        {
            return Rect;
        }

        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
        {
            if (pen == null)
            {
                pen = Pens.Black;
            }
            RenderShape(g, pen, Brushes.White, Rect);
            RenderText(g, pen, brush, font);
        }

        protected virtual void RenderShape(Graphics g, Pen pen, Brush fillBrush, RectangleV rect)
        {
            g.FillRectangle(fillBrush, rect);
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        protected virtual void RenderText(Graphics g, Pen pen, Brush brush, Font font)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                g.DrawString(Text, font, pen.Brush, Left + 2, Top + 2);
            }
        }

        public virtual void Resize(SizeV newsize)
        {
            throw new NotImplementedException();
        }

        public Vector GetCenterOfBox()
        {
            return Rect.CalcCenter();
        }

        public void SetCenterOfBox(float x, float y)
        {
            SetCenterOfBox(new Vector(x, y));
        }
        public void SetCenterOfBox(Vector pt)
        {
            this.Location = new Vector(pt.X - Width / 2, pt.Y - Height / 2);
        }

        private string _text;
        public virtual string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;

                    OnTextChanged(new EventArgs());
                }
            }
        }
        protected virtual void OnTextChanged(EventArgs e)
        {
        }

        public virtual RectangleV Rect
        {
            get { return _rect; }
            set
            {
                //Debug::WriteLine("Need to make Box::Rect::set to use Move() ?" + __WCODESIG__);
                //Debug::WriteLine("Definitely need to make Box::Rect::set update neighbor collections" + __WCODESIG__);

                if (_rect != value)
                {
                    this.OnRectChanging(new EventArgs());

                    _rect = value;

                    this.OnRectChanged(new EventArgs());

                    //UpdateNeighbors();
                }
            }
        }

        public Vector Location
        {
            get { return Rect.Location; }
            set { Rect = new RectangleV(value, Size); }
        }

        public float X
        {
            get { return Rect.X; }
            set { Rect = new RectangleV(value, Y, Width, Height); }
        }

        public float Y
        {
            get { return Rect.Y; }
            set { Rect = new RectangleV(X, value, Width, Height); }
        }

        public SizeV Size
        {
            get { return Rect.Size; }
            set { Rect = new RectangleV(Location, value); }
        }

        public float Width
        {
            get { return Rect.Width; }
            set { Rect = new RectangleV(X, Y, value, Height); }
        }

        public float Height
        {
            get { return Rect.Height; }
            set { Rect = new RectangleV(X, Y, Width, value); }
        }

        public float Left
        {
            get { return Rect.Left; }
        }

        public float Right
        {
            get { return Rect.Right; }
        }

        public float Top
        {
            get { return Rect.Top; }
        }

        public float Bottom
        {
            get { return Rect.Bottom; }
        }

        [field: NonSerialized]
        public virtual event EventHandler RectChanged;

        //public virtual event EventHandler RectChanging;

        protected virtual void OnRectChanged(EventArgs e)
        {
            InvalidateWithinParentControl();

            if (RectChanged != null)
            {
                this.RectChanged(this, e);
            }
        }

        protected virtual void OnRectChanging(EventArgs e)
        {
            InvalidateWithinParentControl();

            //if (RectChanging != null)
            //{
            //    this.RectChanging(this, e);
            //}
        }

        //protected virtual void InvalidateWithinParentControl()
        //{
        //    if (ParentCrystallineControl != null)
        //    {
        //        ParentCrystallineControl.InvalidateRectFromEntity(this);
        //    }
        //}

        //[NonSerialized]
        private RectangleV _rect;

        //because the collection classes interact, this override is unnecessary
        public override CrystallineControl ParentCrystallineControl
        {
            get { return base.ParentCrystallineControl; }
            set
            {
                if (value != ParentCrystallineControl)
                {
                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.Entities.Remove(this);
                    }

                    SetParentCrystallineControl(value);

                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.Entities.Add(this);
                    }
                }
            }
        }

        public override bool IsSelectable
        {
            get { return true; }
        }

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
