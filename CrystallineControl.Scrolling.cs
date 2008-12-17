
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Scrolling.cs                                          *
 *  26 January 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that manages scrolling and coordinate        *
 *    systems.                                                               *
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

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineControl : UserControl
    {

        public Point ClientSpaceFromDocumentSpace(PointF locationInDocumentSpace)
        {
            //return Point.Round(locationInDocumentSpace) + new Size(AutoScrollPosition);
            return Point.Truncate(ClientSpaceFromScrollableSpace(ScrollableSpaceFromDocumentSpace(locationInDocumentSpace)));
        }

        public PointF DocumentSpaceFromClientSpace(Point locationInClientSpace)
        {
            //return locationInClientSpace - new Size(AutoScrollPosition);
            return DocumentSpaceFromScrollableSpace(ScrollableSpaceFromClientSpace(locationInClientSpace));
        }

        protected PointF ClientSpaceFromScrollableSpace(PointF locationInScrollableSpace)
        {
            return locationInScrollableSpace + new SizeF(AutoScrollPosition);
        }

        protected PointF ScrollableSpaceFromClientSpace(PointF locationInClientSpace)
        {
            return locationInClientSpace - new SizeF(AutoScrollPosition);
        }

        protected PointF ScrollableSpaceFromDocumentSpace(PointF locationInDocumentSpace)
        {
            return locationInDocumentSpace - new SizeF(_scrollableAreaInDocument.Location);
        }

        protected PointF DocumentSpaceFromScrollableSpace(PointF locationInScrollableSpace)
        {
            return locationInScrollableSpace + new SizeF(_scrollableAreaInDocument.Location);
        }



        public Rectangle ClientSpaceFromDocumentSpace(RectangleF rectInDocumentSpace)
        {
            return Rectangle.Truncate(ClientSpaceFromScrollableSpace(ScrollableSpaceFromDocumentSpace(rectInDocumentSpace)));
        }

        public RectangleF DocumentSpaceFromClientSpace(Rectangle rectInClientSpace)
        {
            return DocumentSpaceFromScrollableSpace(ScrollableSpaceFromClientSpace(rectInClientSpace));
        }

        protected RectangleF ClientSpaceFromScrollableSpace(RectangleF rectInScrollableSpace)
        {
            return new RectangleF(ClientSpaceFromScrollableSpace(rectInScrollableSpace.Location), rectInScrollableSpace.Size);
        }

        protected RectangleF ScrollableSpaceFromClientSpace(RectangleF rectInClientSpace)
        {
            return new RectangleF(ScrollableSpaceFromClientSpace(rectInClientSpace.Location), rectInClientSpace.Size);
        }

        protected RectangleF ScrollableSpaceFromDocumentSpace(RectangleF rectInDocumentSpace)
        {
            return new RectangleF(ScrollableSpaceFromDocumentSpace(rectInDocumentSpace.Location), rectInDocumentSpace.Size);
        }

        protected RectangleF DocumentSpaceFromScrollableSpace(RectangleF rectInScrollableSpace)
        {
            return new RectangleF(DocumentSpaceFromScrollableSpace(rectInScrollableSpace.Location), rectInScrollableSpace.Size);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            UpdateScrolls();

            if (ShowDebugInfo)
            {
                Invalidate();
            }

            base.OnScroll(se);
        }

        public virtual void UpdateScrolls()
        {
            RectangleF client = GetClientRectInDocument();
            RectangleF rect = client;
            if (!Framework.Bounds.IsEmpty)
            {
                rect = RectangleF.Union(rect, RectangleF.Inflate(Framework.Bounds, 25, 25));
            }

            if (_scrollableAreaInDocument != rect)
            {

                //_scrollableAreaInDocument - new SizeF(client.Location);

                //PointF positionInDocument = DocumentSpaceFromClientSpace(AutoScrollPosition);
                Point newAutoScrollPosition = Point.Truncate(rect.Location - new SizeF(client.Location));

                PointF origin = DocumentSpaceFromClientSpace(new Point(0, 0));

                if (_scrollableAreaInDocument.X < 0)
                {
                    //Debug.Fail("_scrollableAreaInDocument.X < 0");
                }

                //AutoScroll = false;
                AutoScrollPosition = new Point(-newAutoScrollPosition.X, -newAutoScrollPosition.Y);
                AutoScrollMinSize = rect.Size.ToSize();//(new SizeF(Framework.Bounds.Location + Framework.Bounds.Size)).ToSize();
                _scrollableAreaInDocument = rect;
                //AutoScroll = true;

                PointF origin2 = DocumentSpaceFromClientSpace(new Point(0, 0));

                Invalidate();
            }
        }

        private RectangleF GetClientRectInDocument()
        {
            PointF p1 = DocumentSpaceFromClientSpace(new Point(0, 0));
            PointF p2 = DocumentSpaceFromClientSpace(new Point(ClientSize));
            RectangleF rect = new RectangleF(p1, new SizeF(p2 - new SizeF(p1)));
            return rect;
        }

        RectangleF _scrollableAreaInDocument;
    }
}
