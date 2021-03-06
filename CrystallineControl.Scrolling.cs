
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Scrolling.cs                                          *
 *  26 January 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright � 2008 Metaphysics Industries, Inc.                            *
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
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineControl : UserControl
    {
        //////////////////////////////////////////////////////////////////
        // space conversions

        // client space
        // scrollable space
        // document space

        protected Point ClientSpaceFromScrollableSpace(Vector locationInScrollableSpace)
        {
            return new Point((int)Math.Round(locationInScrollableSpace.X / Zoom + AutoScrollPosition.X),
                             (int)Math.Round(locationInScrollableSpace.Y / Zoom + AutoScrollPosition.Y));
        }
        protected Vector ScrollableSpaceFromClientSpace(Point locationInClientSpace)
        {
            return Zoom * (Vector)((PointF)locationInClientSpace) - ((PointF)AutoScrollPosition);
        }

        protected Vector ScrollableSpaceFromDocumentSpace(Vector locationInDocumentSpace)
        {
            return locationInDocumentSpace - _scrollableAreaInDocument.Location;
        }
        protected Vector DocumentSpaceFromScrollableSpace(Vector locationInScrollableSpace)
        {
            return (locationInScrollableSpace + _scrollableAreaInDocument.Location)/_zoom;
        }

        public Point ClientSpaceFromDocumentSpace(Vector locationInDocumentSpace)
        {
            return ClientSpaceFromScrollableSpace(ScrollableSpaceFromDocumentSpace(locationInDocumentSpace));
        }
        public Vector DocumentSpaceFromClientSpace(Point locationInClientSpace)
        {
            return DocumentSpaceFromScrollableSpace(ScrollableSpaceFromClientSpace(locationInClientSpace));
        }

        //////////////////////////////////////////////////////////////////
        // rectangle helpers

        public Rectangle ClientSpaceFromDocumentSpace(RectangleV rectInDocumentSpace)
        {
            return Rectangle.Truncate(ClientSpaceFromScrollableSpace(ScrollableSpaceFromDocumentSpace(rectInDocumentSpace)));
        }

        public RectangleV DocumentSpaceFromClientSpace(Rectangle rectInClientSpace)
        {
            return DocumentSpaceFromScrollableSpace(ScrollableSpaceFromClientSpace(rectInClientSpace));
        }

        protected Rectangle ClientSpaceFromScrollableSpace(RectangleV rectInScrollableSpace)
        {
            Point location = ClientSpaceFromScrollableSpace(rectInScrollableSpace.Location);
            return new Rectangle(location.X, location.Y, (int)rectInScrollableSpace.Width, (int)rectInScrollableSpace.Height);
        }

        protected RectangleV ScrollableSpaceFromClientSpace(Rectangle rectInClientSpace)
        {
            Vector location = ScrollableSpaceFromClientSpace(rectInClientSpace.Location);
            return new RectangleV(location.X, location.Y, rectInClientSpace.Width, rectInClientSpace.Height);
        }

        protected RectangleV ScrollableSpaceFromDocumentSpace(RectangleV rectInDocumentSpace)
        {
            return new RectangleV(ScrollableSpaceFromDocumentSpace(rectInDocumentSpace.Location), rectInDocumentSpace.Size);
        }

        protected RectangleV DocumentSpaceFromScrollableSpace(RectangleV rectInScrollableSpace)
        {
            return new RectangleV(DocumentSpaceFromScrollableSpace(rectInScrollableSpace.Location), rectInScrollableSpace.Size);
        }
        //////////////////////////////////////////////////////////////////
        // zoom

        float _zoom = 1;
        public float Zoom
        {
            get { return _zoom; }
            protected set
            {
                if (_zoom != value)
                {
                    _zoom = value;

                    OnZoomChanged(new EventArgs());
                }
            }
        }

        protected virtual void OnZoomChanged(EventArgs eventArgs)
        {
        }

        //////////////////////////////////////////////////////////////////
        // event handlers, forms stuff, etc.

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
            RectangleV client = GetClientRectInDocument();
            RectangleV bounds = Framework.Bounds;
            RectangleV rect = client;
            if (!Framework.Bounds.IsEmpty 
                //&&
                //(client.Width < bounds.Width || client.Height < bounds.Height)
                )
            {
                float padx = 25;// ClientRectangle.Width / 2;
                float pady = 25;// ClientRectangle.Height / 2;
                rect = rect.Union(bounds.Inflate(padx, pady));
            }

            if (_scrollableAreaInDocument != rect)
            {

                //_scrollableAreaInDocument - new SizeF(client.Location);

                //PointF positionInDocument = DocumentSpaceFromClientSpace(AutoScrollPosition);
                Point newAutoScrollPosition = Point.Truncate(rect.Location - client.Location);

                Vector origin = DocumentSpaceFromClientSpace(new Point(0, 0));

                if (_scrollableAreaInDocument.X < 0)
                {
                    //Debug.Fail("_scrollableAreaInDocument.X < 0");
                }

                //AutoScroll = false;
                AutoScrollPosition = new Point(-newAutoScrollPosition.X, -newAutoScrollPosition.Y);
                AutoScrollMinSize = (new SizeF(rect.Width, rect.Height)).ToSize(); //rect.Size.ToSize();//(new SizeF(Framework.Bounds.Location + Framework.Bounds.Size)).ToSize();
                _scrollableAreaInDocument = rect;
                //AutoScroll = true;

                Vector origin2 = DocumentSpaceFromClientSpace(new Point(0, 0));

                Invalidate();
            }
        }

        private RectangleV GetClientRectInDocument()
        {
            Vector p1 = DocumentSpaceFromClientSpace(new Point(0, 0));
            Vector p2 = DocumentSpaceFromClientSpace(new Point(ClientSize));
            RectangleV rect = new RectangleV(p1, new SizeV(p2 - p1));
            return rect;
        }

        RectangleV _scrollableAreaInDocument;

        protected void CenterViewOnPointInDocument(Vector center)
        {
            throw new NotImplementedException();
        }
    }
}
