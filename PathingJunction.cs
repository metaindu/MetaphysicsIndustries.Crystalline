
/*****************************************************************************
 *                                                                           *
 *  PathingJunction.cs                                                       *
 *  24 January 2007                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that serves as a node in the graph of path between elements.     *
 *    Associated with PathJoints. Directs the flow of paths around and       *
 *    between elements so everything is neat and tidy.                       *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Drawing;

namespace MetaphysicsIndustries.Crystalline
{
    //public class PathingJunction : Box
    //{
    //    public PathingJunction()
    //    {
    //        Width = 25.0f;
    //        Height = 25.0f;
    //    }

    //    //public override void Dispose()
    //    //{
    //    //    LeftPathway = null;
    //    //    UpPathway = null;
    //    //    RightPathway = null;
    //    //    DownPathway = null;

    //    //    base.Dispose();
    //    //}

    //    //public override void Render(Graphics g, Pen pen, Brush brush, Font font)
    //    //{
    //    //    Graphics _g = g;
			
    //    //    if (pen == null)
    //    //    {
    //    //        pen = Pens.Black;
    //    //    }
			
    //    //    g.DrawRectangle(pen, this.X, this.Y, this.Width, this.Height);
    //    //}

    //    public virtual Pathway LeftPathway
    //    {
    //        get
    //        {
    //            return _leftPathway;
    //        }
    //        set
    //        {
    //            if (_leftPathway != value)
    //            {
    //                if (_leftPathway != null)
    //                {
    //                    _leftPathway.RightDown = null;
    //                }

    //                _leftPathway = value;

    //                if (_leftPathway != null)
    //                {
    //                    _leftPathway.RightDown = this;
    //                }
    //            }
    //        }
    //    }

    //    public virtual Pathway UpPathway
    //    {
    //        get
    //        {
    //            return _upPathway;
    //        }
    //        set
    //        {
    //            if (_upPathway != value)
    //            {
    //                if (_upPathway != null)
    //                {
    //                    _upPathway.RightDown = null;
    //                }

    //                _upPathway = value;

    //                if (_upPathway != null)
    //                {
    //                    _upPathway.RightDown = this;
    //                }
    //            }
    //        }
    //    }

    //    public virtual Pathway RightPathway
    //    {
    //        get
    //        {
    //            return _rightPathway;
    //        }
    //        set
    //        {
    //            if (_rightPathway != value)
    //            {
    //                if (_rightPathway != null)
    //                {
    //                    _rightPathway.LeftUp = null;
    //                }

    //                _rightPathway = value;

    //                if (_rightPathway != null)
    //                {
    //                    _rightPathway.LeftUp = this;
    //                }
    //            }
    //        }
    //    }

    //    public virtual Pathway DownPathway
    //    {
    //        get
    //        {
    //            return _downPathway;
    //        }
    //        set
    //        {
    //            if (_downPathway != value)
    //            {
    //                if (_downPathway != null)
    //                {
    //                    _downPathway.LeftUp = null;
    //                }

    //                _downPathway = value;

    //                if (_downPathway != null)
    //                {
    //                    _downPathway.LeftUp = this;
    //                }
    //            }
    //        }
    //    }

    //    protected void UpdateUpPathwayRect()
    //    {
    //        if (UpPathway == null) { return; }


    //    }

    //    protected void UpdateRightPathway()
    //    {
    //    }

    //    private Pathway _rightPathway;
    //    private Pathway _downPathway;
    //    private Pathway _upPathway;
    //    private Pathway _leftPathway;
    //}
}
