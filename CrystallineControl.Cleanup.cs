
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Cleanup.cs                                            *
 *  26 January 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that manages the cleaning up the items.      *
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
        protected class CleanupComparer : IComparer<Element>
        {
            public CleanupComparer(bool SortByY)
            {
                sortbyy = SortByY;
            }

            public virtual int Compare(Element x, Element y)
            {
                //if (sortbyy)
                //{
                //	if (x.Location.Y > y.Location.Y) return 1;
                //	if (x.Location.Y < y.Location.Y) return -1;
                //	if (x.Location.X > y.Location.X) return 1;
                //	if (x.Location.X < y.Location.X) return -1;
                //	return 0;
                //}
                //else
                //{
                //	if (x.Location.X > y.Location.X) return 1;
                //	if (x.Location.X < y.Location.X) return -1;
                //	if (x.Location.Y > y.Location.Y) return 1;
                //	if (x.Location.Y < y.Location.Y) return -1;
                //	return 0;
                //}

                if (sortbyy)
                {
                    if (x.Location.Y > y.Location.Y) return 1;
                    if (x.Location.Y < y.Location.Y) return -1;
                }
                if (x.Location.X > y.Location.X) return 1;
                if (x.Location.X < y.Location.X) return -1;
                if (!sortbyy)
                {
                    if (x.Location.Y > y.Location.Y) return 1;
                    if (x.Location.Y < y.Location.Y) return -1;
                }

                return 0;
            }

            private bool sortbyy;
        }

        protected virtual void Cleanup()
        {
            int i;
            int j;
            int k;

            j = Framework.Left.Count;

            RectangleV[] rects = new RectangleV[Framework.Left.Count];

            bool cont = true;
            while (cont)
            {
                cont = false;

                for (i = 0; i < j; i++)
                {
                    rects[i] = Framework.Left[i].Rect;
                }

                for (i = 0; i < j; i++)
                {
                    for (k = i + 1; k < j; k++)
                    {
                        if (rects[k].Left > rects[i].Right) { break; }

                        if (rects[k].IntersectsWith(rects[i]))
                        {
                            //collision
                            float delta = rects[i].Right - rects[k].Left;
                            Vector newLocation = rects[k].Location + Vector.OffsetY(delta + 10);
                            Framework.Move(Framework.Left[k], newLocation, null, null);
                            cont = true;
                            break;
                        }
                    }

                    if (cont)
                    {
                        break;
                    }
                }
            }

            foreach (Path path in Entities.Extract<Path>())
            {
                RoutePath(path);
            }
        }
    }
}
