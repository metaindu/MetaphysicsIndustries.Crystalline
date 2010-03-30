
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Routing.cs                                            *
 *  26 January 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that manages the routing of Paths.           *
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
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineControl : UserControl
    {
        public virtual void RouteAllPaths()
        {
            foreach (Path p in Paths)
            {
                RoutePath(p);
            }
        }

        public void RoutePath(Path path)
        {
            if (path == null) { throw new ArgumentNullException("path"); }
            if (!Paths.Contains(path)) { return; } //throw?

            InvalidateRectFromEntity(path);

            InternalRoutePath(path);

            InvalidateRectFromEntity(path);
        }

        protected virtual void InternalRoutePath(Path path)
        {

            if (path.To == null && path.From == null)
            {
                float x;
                float y;

                x = 0;
                y = 0;
                foreach (Vector pj in path.PathJoints)
                {
                    x += pj.X;
                    y += pj.Y;
                }

                if (path.PathJoints.Count > 0)
                {
                    x /= path.PathJoints.Count;
                    y /= path.PathJoints.Count;
                }

                path.PathJoints.Clear();
                path.PathJoints.Add(new Vector(x, y));
                path.PathJoints.Add(new Vector(x + 20, y));
            }
            else
            {
                path.PathJoints.Clear();

                float x1 = 0;
                float y1 = 0;
                float x2 = 0;
                float y2 = 0;
                float x3 = 0;

                if (path.From != null)
                {
                    Vector p1 = path.From.GetOutboundConnectionPoint(path);
                    x1 = p1.X;
                    y1 = p1.Y;
                }
                if (path.To != null)
                {
                    Vector p2 = path.To.GetInboundConnectionPoint(path);
                    x2 = p2.X;
                    y2 = p2.Y;
                }

                if (path.From != null && path.To != null)
                {
                    x3 = (x1 + x2) / 2;
                }

                if (path.From != null && path.To != null)
                {
                    path.PathJoints.Add(new Vector(x1, y1));
                    if (y1 != y2)
                    {
                        path.PathJoints.Add(new Vector(x3, y1));
                        path.PathJoints.Add(new Vector(x3, y2));
                    }
                    path.PathJoints.Add(new Vector(x2, y2));
                }
                else if (path.From != null)
                {
                    path.PathJoints.Add(new Vector(x1, y1));
                    path.PathJoints.Add(new Vector(x1 + 20, y1));
                }
                else if (path.To != null)
                {
                    path.PathJoints.Add(new Vector(x2 - 20, y2));
                    path.PathJoints.Add(new Vector(x2, y2));
                }
            }
        }
    }
}
