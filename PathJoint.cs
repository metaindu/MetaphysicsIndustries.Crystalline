
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
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    //public class PathJoint : Entity
    //{
    //    public PathJoint()
    //        : this(0, 0)
    //    {
    //    }

    //    public PathJoint(float x, float y)
    //        : this(new Vector(x, y))
    //    {
    //    }

    //    public PathJoint(Vector location)
    //    {
    //        _location = location;
    //    }

    //    public override RectangleV GetBoundingBox()
    //    {
    //        return new RectangleV(Location, new SizeV(2, 2));
    //    }

    //    public override void Render(Graphics g, Pen pen, Brush brush, Font font)
    //    {
    //        RenderPathJoint(g, pen, brush, font, Location);
    //    }

    //    public static void RenderPathJoint(Graphics g, Pen pen, Brush brush, Font font, Vector location)
    //    {
    //        RectangleV r = new RectangleV(
    //            location - new Vector(1, 1),
    //            new SizeV(3, 3));
    //        g.FillRectangle(pen.Brush, r);
    //    }

    //    public virtual Path ParentPath
    //    {
    //        get { return _parentPath; }
    //        set
    //        {
    //            if (_parentPath != value)
    //            {
    //                if (_parentPath != null)
    //                {
    //                    _parentPath.PathJoints.Remove(this);
    //                }
    //                _parentPath = value;
    //                if (_parentPath != null)
    //                {
    //                    _parentPath.PathJoints.Add(this);
    //                }
    //            }
    //        }
    //    }

    //    public virtual Vector Location
    //    {
    //        get { return _location; }
    //        set
    //        {
    //            if (_location != value)
    //            {
    //                _location = value;
    //            }
    //        }
    //    }

    //    //private int _index;
    //    private Vector _location;
    //    private Path _parentPath;
    //}
}
