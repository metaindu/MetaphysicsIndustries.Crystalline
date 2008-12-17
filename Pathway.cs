
/*****************************************************************************
 *                                                                           *
 *  Pathway.cs                                                               *
 *  30 January 2007                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  Acts as the pathway between two PathingJunction objects. Path objects    *
 *    follow it and turn at the junctions.                                   *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Drawing;

namespace MetaphysicsIndustries.Crystalline
{
	public class Pathway : Box
	{
		public Pathway()
		{
			this.Size = new SizeF(50, 25);
		}

		public override void Dispose()
		{
            LeftUp = null;
            RightDown = null;

            base.Dispose();
		}

        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
		{
			Graphics _g = g;
			RectangleF	r = new RectangleF();
			if (pen == null)
			{
				pen = Pens.Black;
			}
			if (IsVertical)
			{
				if (LeftUp != null)
				{
					if (RightDown != null)
					{
						r.X = Math.Min(LeftUp.X, RightDown.X);
						r.Width = Math.Max(LeftUp.X, RightDown.X) + 25;
						r.Height = Math.Abs(LeftUp.Y - RightDown.Y) - 25;
					}
					else
					{
						r.X = LeftUp.X;
						r.Width = 25;
						r.Height = 50;
					}
					r.Y = LeftUp.Y + 25;
				}
				else
				{
					if (RightDown != null)
					{
						r.X = RightDown.X;
						r.Width = 25;
						r.Y = RightDown.Y - 50;
						r.Height = 50;
					}
					else
					{
						r = new RectangleF(0, 0, 0, 0);
					}
				}
			}
			else
			{
				if (LeftUp != null)
				{
					if (RightDown != null)
					{
						r.X = Math.Min(LeftUp.X, RightDown.X) + 25;
						r.Y = Math.Min(LeftUp.Y, RightDown.Y);
						r.Width = Math.Abs(LeftUp.X - RightDown.X) - 25;
						r.Height = Math.Max(LeftUp.Y, RightDown.Y) + 25;
					}
					else
					{
						r.X = LeftUp.X + 25;
						r.Y = LeftUp.Y;
						r.Width = 50;
						r.Height = 25;
					}
				}
				else
				{
					if (RightDown != null)
					{
						r.X = RightDown.X - 50;
						r.Y = RightDown.Y;
						r.Width = 50;
						r.Height = 25;
					}
					else
					{
						r = new RectangleF(0, 0, 0, 0);
					}
				}
			}
			
			
			r = RectangleF.Inflate(this.Rect, -1, -1);
			g.DrawRectangle(pen, r.X, r.Y, r.Width, r.Height);
		}

		public virtual bool IsVertical
		{
			get
			{
				return _isVertical;
			}
			set
			{
                if (_isVertical != value)
				{
                    _isVertical = value;

                    LeftUp = null;
                    RightDown = null;


                    //!@#$ --- fix this
                    if (value)
					{
						this.Width = 25;
						this.Height = 50;
					}
					else
					{
						this.Width = 50;
						this.Height = 25;
					}
				}
			}
		}

		public virtual PathingJunction LeftUp
		{
			get
			{
				return _leftUp;
			}
			set
			{
                if (_leftUp != value)
				{
                    if (_leftUp != null)
                    {
                        if (IsVertical)
                        {
                            _leftUp.DownPathway = null;
                        }
                        else
                        {
                            _leftUp.RightPathway = null;
                        }
                    }

                    _leftUp = value;

                    if (_leftUp != null)
                    {
                        if (IsVertical)
                        {
                            _leftUp.DownPathway = this;
                        }
                        else
                        {
                            _leftUp.RightPathway = this;
                        }
                    }

                    //UpdateLeftUpRect();
				}
			}
		}

		public virtual PathingJunction RightDown
		{
			get
			{
				return _rightDown;
			}
			set
			{
                if (_rightDown != value)
				{
                    if (_rightDown != null)
                    {
                        if (IsVertical)
                        {
                            _rightDown.UpPathway = null;
                        }
                        else
                        {
                            _rightDown.LeftPathway = null;
                        }
                    }

                    _rightDown = value;

                    if (_rightDown != null)
                    {
                        if (IsVertical)
                        {
                            _rightDown.UpPathway = this;
                        }
                        else
                        {
                            _rightDown.LeftPathway = this;
                        }
                    }

                    //UpdateRightDownRect();
                }
			}
		}

        protected void UpdateLeftUpRect()
        {
            if (LeftUp == null) { return; }

            if (IsVertical)
            {
                float height = LeftUp.Height;
                LeftUp.Rect = new RectangleF(Left, Top - height, Width, height);
            }
            else
            {
                float width = LeftUp.Width;
                LeftUp.Rect = new RectangleF(Left - width, Top, width, Height);
            }
        }

        protected void UpdateRightDownRect()
        {
            if (RightDown == null) { return; }

            if (IsVertical)
            {
                float height = RightDown.Height;
                RightDown.Rect = new RectangleF(Left, Bottom, Width, height);
            }
            else
            {
                float width = RightDown.Width;
                RightDown.Rect = new RectangleF(Right, Top, width, Height);
            }
        }

        protected void UpdateLeftUpRectByRelocate()
        {
            if (LeftUp == null) { return; }

            RectangleF rect;

            if (IsVertical)
            {
                float height = LeftUp.Height;
                rect = new RectangleF(Left, Top - height, Width, height);
            }
            else
            {
                float width = LeftUp.Width;
                rect = new RectangleF(Left - width, Top, width, Height);
            }

            //change this to Framework.Relocate
            LeftUp.Size = rect.Size;
            Framework.Move(LeftUp, rect.Location);
        }

        protected void UpdateRightDownRectByRelocate()
        {
            if (RightDown == null) { return; }

            RectangleF rect;

            if (IsVertical)
            {
                float height = RightDown.Height;
                rect = new RectangleF(Left, Bottom, Width, height);
            }
            else
            {
                float width = RightDown.Width;
                rect = new RectangleF(Right, Top, width, Height);
            }

            //change this to Framework.Relocate
            RightDown.Size = rect.Size;
            Framework.Move(RightDown, rect.Location);
        }

        protected override void OnRectChanged(EventArgs e)
        {
            //UpdateLeftUpRectByRelocate();
            //UpdateRightDownRectByRelocate();

            base.OnRectChanged(e);
        }

        private bool _isVertical;
        private PathingJunction _leftUp;
        private PathingJunction _rightDown;
	}
}
