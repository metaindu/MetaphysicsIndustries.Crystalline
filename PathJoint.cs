
/*****************************************************************************
 *                                                                           *
 *  PathJoint.cs                                                             *
 *  26 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Drawing;

namespace MetaphysicsIndustries.Crystalline
{
    public class PathJoint : Entity
	{
		public PathJoint()
            : this(0, 0)
		{
		}

        public PathJoint(float x, float y)
            : this(new PointF(x, y))
        {
        }

		public PathJoint(PointF location)
		{
			_location = location;
		}


        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
        {
			RectangleF	r = new RectangleF();
            r.Location = Location - new SizeF(1, 1);
            r.Size = new SizeF(3, 3);
			g.FillRectangle(pen.Brush, r);
		}

		public virtual Path ParentPath
		{
            get { return _parentPath; }
			set
			{
				if (_parentPath != value)
				{
					if (_parentPath != null)
					{
						_parentPath.PathJoints.Remove(this);
					}
                    _parentPath = value;
					if (_parentPath != null)
					{
						_parentPath.PathJoints.Add(this);
					}
				}
			}
		}

		public virtual PointF Location
		{
            get { return _location; }
			set
			{
                if (_location != value)
				{
                    _location = value;
				}
			}
		}

        //private int _index;
        private PointF _location;
        private Path _parentPath;
	}
}
