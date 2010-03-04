
/*****************************************************************************
 *                                                                           *
 *  Box.cs                                                                   *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
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
    public class Box : Entity
    {
        public Box()
        {
            _left = new BoxNeighborCollection(this, BoxOrientation.Left);
            _right = new BoxNeighborCollection(this, BoxOrientation.Right);
            _up = new BoxNeighborCollection(this, BoxOrientation.Up);
            _down = new BoxNeighborCollection(this, BoxOrientation.Down);
        }

        public override RectangleV GetBoundingBox()
        {
            return Rect;
        }

        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
        {
            RectangleV r;
            if (pen == null)
            {
                pen = Pens.Black;
            }
            r = this.Rect;
            RenderShape(g, pen, Brushes.White, r);
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

        public virtual void Move(Vector newlocation, Set<Box> collidedBoxes)
        {
            //maybe route this to the framework
            //
            //this->Framework->MoveBox(this, newlocation);
            //
            //that would be more efficient in terms of moving around
            //BUT!!! would it prevent us from customizing movement for
            //different kinds of boxes, e.g. Pathway moves/resizes with
            //PathingJunction?

            //if (Box._moveCallCount > Box._moveCallCountMax)
            //{
            //    Box._moveCallCount = Box._moveCallCount;
            //}


            //float					deltax = newlocation.X - this->X;
            //float					deltay = newlocation.Y - this->Y;
            //float					delta2;
            //PointF				p;
            ////List<
            //Set<IBox^>^			newneighbors1;
            //Set<IBox^>^			newneighbors2;
            //Set<IBox^>^			removeneighbors;
            //ICollection<IBox^>^	moveneighbors;

            //Object^				param;

            //param = ((Element^)(this))->Param;

            //if (deltax)// > 0)
            //{
            //	if (deltax > 0)
            //	{
            //		moveneighbors = this->RightNeighbors;
            //	}
            //	else
            //	{
            //		moveneighbors = this->LeftNeighbors;
            //	}
            //	for each (IBox^ ib in moveneighbors)
            //	{
            //		if (deltax > 0)
            //		{
            //			delta2 = this->Right + deltax - ib->Left;		//buffer space?
            //		}
            //		else
            //		{
            //			delta2 = ib->Right - this->Left - deltax;		//buffer space?
            //		}
            //		if (delta2 > 0)	
            //		{
            //			p = ib->Location;
            //			if (deltax > 0)
            //			{
            //				p.X += delta2;
            //			}
            //			else
            //			{
            //				p.X -= delta2;
            //			}
            //			ib->Move(p);
            //		}
            //	}
            //	if (deltax > this->Width || -deltax > this->Width)
            //	{
            //		//no overlap, discard up/down neighbors 
            //		this->UpNeighbors->Clear();
            //		this->DownNeighbors->Clear();
            //	}
            //	else
            //	{
            //		//overlap, discard some neighbors 

            //		//note: we don't care about whether or not they're obscured,
            //		//so we can just use simple bounds check instead of 
            //		//BoxFramework::GetNeighborsXXX

            //		removeneighbors = gcnew Set<IBox^>();		//maybe reuse this object
            //		for each (IBox^ ib in this->UpNeighbors)
            //		{
            //			if (deltax > 0)
            //			{
            //				if (ib->Right < this->Left + deltax)
            //				{
            //					removeneighbors->Add(ib);
            //				}
            //			}
            //			else
            //			{
            //				if (ib->Left > this->Right + deltax)
            //				{
            //					removeneighbors->Add(ib);
            //				}
            //			}
            //		}
            //		for each (IBox^ ib in removeneighbors)
            //		{
            //			this->UpNeighbors->Remove(ib);
            //		}
            //		removeneighbors->Clear();
            //		for each (IBox^ ib in this->DownNeighbors)
            //		{
            //			if (deltax > 0)
            //			{
            //				if (ib->Right < this->Left + deltax)
            //				{
            //					removeneighbors->Add(ib);
            //				}
            //			}
            //			else
            //			{
            //				if (ib->Left > this->Right + deltax)
            //				{
            //					removeneighbors->Add(ib);
            //				}
            //			}
            //		}
            //		for each (IBox^ ib in removeneighbors)
            //		{
            //			this->DownNeighbors->Remove(ib);
            //		}
            //	}

            //	if (deltax > 0)
            //	{
            //		newneighbors1 = this->Framework->GetNeighborsAbove(this->Top, Math::Max(this->Right, this->Left + deltax), this->Right + deltax);
            //		newneighbors2 = this->Framework->GetNeighborsBelow(this->Bottom, Math::Max(this->Right, this->Left + deltax), this->Right + deltax);
            //	}
            //	else
            //	{
            //		newneighbors1 = this->Framework->GetNeighborsAbove(this->Top, this->Left + deltax, Math::Min(this->Left, this->Right + deltax));
            //		newneighbors2 = this->Framework->GetNeighborsBelow(this->Bottom, this->Left + deltax, Math::Min(this->Left, this->Right + deltax));
            //	}
            //	this->X += deltax;
            //	for each (IBox^ ib in newneighbors1)
            //	{
            //		this->UpNeighbors->Add(ib);
            //	}
            //	for each (IBox^ ib in newneighbors2)
            //	{
            //		this->DownNeighbors->Add(ib);
            //	}
            //}
            ////else if (deltax < 0)
            ////{
            ////	//move left
            ////}

            //if (deltay)// > 0)
            //{
            //	if (deltay > 0)
            //	{
            //		moveneighbors = this->DownNeighbors;
            //	}
            //	else
            //	{
            //		moveneighbors = this->UpNeighbors;
            //	}
            //	for each (IBox^ ib in moveneighbors)
            //	{
            //		if (deltay > 0)
            //		{
            //			delta2 = this->Bottom + deltay - ib->Top;		//buffer space?
            //		}
            //		else
            //		{
            //			delta2 = ib->Bottom - this->Top - deltay;		//buffer space?
            //		}
            //		if (delta2 > 0)	
            //		{
            //			p = ib->Location;
            //			if (deltay > 0)
            //			{
            //				p.Y += delta2;
            //			}
            //			else
            //			{
            //				p.Y -= delta2;
            //			}
            //			ib->Move(p);
            //		}
            //	}
            //	if (deltay > this->Height|| -deltay > this->Height)
            //	{
            //		//no overlap, discard left/right neighbors 
            //		this->LeftNeighbors->Clear();
            //		this->RightNeighbors->Clear();
            //	}
            //	else
            //	{
            //		//overlap, discard some neighbors 

            //		//note: we don't care about whether or not they're obscured,
            //		//so we can just use simple bounds check instead of 
            //		//BoxFramework::GetNeighborsXXX

            //		removeneighbors = gcnew Set<IBox^>();		//maybe reuse this object
            //		for each (IBox^ ib in this->LeftNeighbors)
            //		{
            //			if (deltay > 0)
            //			{
            //				if (ib->Bottom < this->Top + deltay)
            //				{
            //					removeneighbors->Add(ib);
            //				}
            //			}
            //			else
            //			{
            //				if (ib->Top > this->Bottom + deltay)
            //				{
            //					removeneighbors->Add(ib);
            //				}
            //			}
            //		}
            //		for each (IBox^ ib in removeneighbors)
            //		{
            //			this->LeftNeighbors->Remove(ib);
            //		}
            //		removeneighbors->Clear();
            //		for each (IBox^ ib in this->RightNeighbors)
            //		{
            //			if (deltay > 0)
            //			{
            //				if (ib->Bottom < this->Top + deltay)
            //				{
            //					removeneighbors->Add(ib);
            //				}
            //			}
            //			else
            //			{
            //				if (ib->Top > this->Bottom + deltay)
            //				{
            //					removeneighbors->Add(ib);
            //				}
            //			}
            //		}
            //		for each (IBox^ ib in removeneighbors)
            //		{
            //			this->RightNeighbors->Remove(ib);
            //		}
            //	}

            //	if (deltay > 0)
            //	{
            //		newneighbors1 = this->Framework->GetNeighborsLeftward(this->Left, Math::Max(this->Bottom, this->Top + deltay), this->Bottom + deltay);
            //		newneighbors2 = this->Framework->GetNeighborsRightward(this->Right, Math::Max(this->Bottom, this->Top + deltay), this->Bottom + deltay);
            //	}
            //	else
            //	{
            //		newneighbors1 = this->Framework->GetNeighborsLeftward(this->Left, this->Top + deltay, Math::Min(this->Top, this->Bottom + deltay));
            //		newneighbors2 = this->Framework->GetNeighborsRightward(this->Right, this->Top + deltay, Math::Min(this->Top, this->Bottom + deltay));
            //	}
            //	this->Y += deltay;
            //	for each (IBox^ ib in newneighbors1)
            //	{
            //		this->LeftNeighbors->Add(ib);
            //	}
            //	for each (IBox^ ib in newneighbors2)
            //	{
            //		this->RightNeighbors->Add(ib);
            //	}
            //}
            ////else if (deltay < 0)
            ////{
            ////	//move up
            ////}

            Framework.Move(this, newlocation, collidedBoxes);
        }

        public virtual void Resize(SizeV newsize)
        {
            throw new NotImplementedException();
        }

        public Vector GetCenterOfBox()
        {
            return Rect.CalcCenter();
        }

        public void SetCenterOfBox(Vector pt)
        {
            this.Location = new Vector(pt.X - Width / 2, pt.Y - Height / 2);
        }

        private string _text;
        public virtual string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public virtual BoxFramework Framework
        {
            //get
            //{
            //    return _framework;
            //}
            //set
            //{
            //    if (Framework != value)
            //    {
            //        if (Framework != null) { Framework.Remove(this); }
            //        _framework = value;
            //        if (Framework != null) { Framework.Add(this); }

            //        InvalidateWithinParentControl();
            //        UpdateNeighbors();
            //    }
            //}
            get
            {
                if (ParentCrystallineControl != null)
                {
                    return ParentCrystallineControl.Framework;
                }
                else
                {
                    return null;
                }
            }
        }

        public virtual BoxNeighborCollection UpNeighbors
        {
            get { return _up; }
        }

        public virtual BoxNeighborCollection RightNeighbors
        {
            get { return _right; }
        }

        public virtual BoxNeighborCollection DownNeighbors
        {
            get { return _down; }
        }

        public virtual BoxNeighborCollection LeftNeighbors
        {
            get { return _left; }
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

        protected virtual void InvalidateWithinParentControl()
        {
            if (Framework != null && Framework.ParentControl != null)
            {
                Framework.ParentControl.InvalidateRectFromEntity(this);
            }
        }

        protected void UpdateNeighbors()
        {
            //UpNeighbors.Clear();
            //RightNeighbors.Clear();
            //DownNeighbors.Clear();
            //LeftNeighbors.Clear();

            //if (Framework != null)
            //{
            //    UpNeighbors.AddRange(Framework.GetNeighborsAbove(Top, Left, Right));
            //    RightNeighbors.AddRange(Framework.GetNeighborsRightward(Right, Top, Bottom));
            //    DownNeighbors.AddRange(Framework.GetNeighborsBelow(Bottom, Left, Right));
            //    LeftNeighbors.AddRange(Framework.GetNeighborsLeftward(Left, Top, Bottom));
            //}
        }

        //[NonSerialized]
        private BoxFramework _framework;
        //[NonSerialized]
        private BoxNeighborCollection _up;
        //[NonSerialized]
        private BoxNeighborCollection _right;
        //[NonSerialized]
        private BoxNeighborCollection _down;
        //[NonSerialized]
        private BoxNeighborCollection _left;
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
                        ParentCrystallineControl.Boxes.Remove(this);
                    }

                    SetParentCrystallineControl(value);

                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.Boxes.Add(this);
                    }
                }
            }
        }
    }
}
