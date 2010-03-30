using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Collections;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    public class EntityMovement : Functionality
    {
        public EntityMovement(CrystallineEngine engine)
            : base(engine)
        {
        }

        bool _isClick;
        Vector _dragAnchorInDocument;

        //public override void ProcessMouseDown(System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (false && e.Button == System.Windows.Forms.MouseButtons.Left)
        //    {
        //        Vector clickLocationInDocument = DocumentSpaceFromClientSpace(e.Location);

        //        _isClick = true;
        //        _dragAnchorInDocument = clickLocationInDocument;

        //        if (ParentCrystallineEngine.Control.SelectionMode == CrystallineControl.SelectionModeType.Element)
        //        {
        //            Set<Element> s1;
        //            Set<Element> s2;

        //            s1 = ParentCrystallineEngine.Control.GetElementsAtPoint(_dragAnchorInDocument);
        //            if (s1.Count > 0)
        //            {
        //                s2 = ParentCrystallineEngine.Control.SelectionElement.Intersection(s1);
        //                if (s2.Count > 0)
        //                {
        //                    //clicked a previously-selected element
        //                }
        //                else
        //                {
        //                    //clicked a different element
        //                    //need to update z-order
        //                    ParentCrystallineEngine.Control.Selection.Clear();
        //                    Element frontmost = s1.GetFirst();
        //                    int index = ParentCrystallineEngine.Control.Framework.ZOrder.IndexOf(frontmost);
        //                    foreach (Element ee in s1)
        //                    {
        //                        //Selection.Add(ee);
        //                        int index2 = ParentCrystallineEngine.Control.Framework.ZOrder.IndexOf(ee);
        //                        if (index2 > index)
        //                        {
        //                            index = index2;
        //                            frontmost = ee;
        //                        }
        //                    }
        //                    ParentCrystallineEngine.Control.BringToFront(frontmost);
        //                    ParentCrystallineEngine.Control.Selection.Add(frontmost);
        //                }
        //                ParentCrystallineEngine.Control._isDragSelecting = false;
        //            }
        //            else
        //            {
        //                ParentCrystallineEngine.Control.Selection.Clear();
        //                _isDragSelecting = true;
        //            }
        //        }
        //        else if (ParentCrystallineEngine.Control.SelectionMode == CrystallineControl.SelectionModeType.Path)
        //        {
        //            Set<PathJoint> s1;
        //            Set<PathJoint> s2;

        //            s1 = GetPathJointsAtPoint(clickLocationInDocument);
        //            if (s1.Count > 0)
        //            {
        //                s2 = SelectionPathJoint.Intersection(s1);
        //                if (s2.Count > 0)
        //                {
        //                    //clicked a previously-selected element
        //                }
        //                else
        //                {
        //                    //clicked a different element
        //                    SelectionPathJoint.Clear();
        //                    foreach (PathJoint ee in s1)
        //                    {
        //                        SelectionPathJoint.Add(ee);
        //                    }
        //                }
        //                _isDragSelecting = false;
        //            }
        //            else
        //            {
        //                SelectionPathJoint.Clear();
        //                _isDragSelecting = true;
        //            }
        //        }
        //        else if (SelectionMode == SelectionModeType.PathingJunction)
        //        {
        //            Set<PathingJunction> s1;
        //            Set<PathingJunction> s2;

        //            s1 = GetPathingJunctionsAtPoint(clickLocationInDocument);
        //            if (s1.Count > 0)
        //            {
        //                s2 = SelectionPathingJunction.Intersection(s1);
        //                if (s2.Count > 0)
        //                {
        //                    //clicked a previously-selected element
        //                }
        //                else
        //                {
        //                    //clicked a different element
        //                    SelectionPathingJunction.Clear();
        //                    foreach (PathingJunction ee in s1)
        //                    {
        //                        SelectionPathingJunction.Add(ee);
        //                    }
        //                }
        //                _isDragSelecting = false;
        //            }
        //            else
        //            {
        //                SelectionPathingJunction.Clear();
        //                _isDragSelecting = true;
        //            }
        //        }
        //        else if (SelectionMode == SelectionModeType.Pathway)
        //        {
        //            Set<Pathway> s1;
        //            Set<Pathway> s2;

        //            s1 = GetPathwaysAtPoint(clickLocationInDocument);
        //            if (s1.Count > 0)
        //            {
        //                s2 = SelectionPathway.Intersection(s1);
        //                if (s2.Count > 0)
        //                {
        //                    //clicked a previously-selected element
        //                }
        //                else
        //                {
        //                    //clicked a different element
        //                    SelectionPathway.Clear();
        //                    foreach (Pathway ee in s1)
        //                    {
        //                        SelectionPathway.Add(ee);
        //                    }
        //                }
        //                _isDragSelecting = false;
        //            }
        //            else
        //            {
        //                SelectionPathway.Clear();
        //                _isDragSelecting = true;
        //            }
        //        }

        //    }
        //}



        //public Point ClientSpaceFromDocumentSpace(Vector locationInDocumentSpace)
        //{
        //    //return Point.Round(locationInDocumentSpace) + new Size(AutoScrollPosition);
        //    return Point.Truncate(ClientSpaceFromScrollableSpace(ScrollableSpaceFromDocumentSpace(locationInDocumentSpace)));
        //}

        //public Vector DocumentSpaceFromClientSpace(Point locationInClientSpace)
        //{
        //    //return locationInClientSpace - new Size(AutoScrollPosition);
        //    return DocumentSpaceFromScrollableSpace(ScrollableSpaceFromClientSpace(locationInClientSpace));
        //}

        //protected Vector ClientSpaceFromScrollableSpace(Vector locationInScrollableSpace)
        //{
        //    return locationInScrollableSpace + new SizeF(AutoScrollPosition);
        //}

        //protected Vector ScrollableSpaceFromClientSpace(Vector locationInClientSpace)
        //{
        //    return locationInClientSpace - new SizeF(AutoScrollPosition);
        //}

        //protected Vector ScrollableSpaceFromDocumentSpace(Vector locationInDocumentSpace)
        //{
        //    return locationInDocumentSpace - new SizeF(_scrollableAreaInDocument.Location);
        //}

        //protected Vector DocumentSpaceFromScrollableSpace(Vector locationInScrollableSpace)
        //{
        //    return locationInScrollableSpace + new SizeF(_scrollableAreaInDocument.Location);
        //}



        //public Rectangle ClientSpaceFromDocumentSpace(RectangleF rectInDocumentSpace)
        //{
        //    return Rectangle.Truncate(ClientSpaceFromScrollableSpace(ScrollableSpaceFromDocumentSpace(rectInDocumentSpace)));
        //}

        //public RectangleF DocumentSpaceFromClientSpace(Rectangle rectInClientSpace)
        //{
        //    return DocumentSpaceFromScrollableSpace(ScrollableSpaceFromClientSpace(rectInClientSpace));
        //}

        //protected RectangleF ClientSpaceFromScrollableSpace(RectangleF rectInScrollableSpace)
        //{
        //    return new RectangleF(ClientSpaceFromScrollableSpace(rectInScrollableSpace.Location), rectInScrollableSpace.Size);
        //}

        //protected RectangleF ScrollableSpaceFromClientSpace(RectangleF rectInClientSpace)
        //{
        //    return new RectangleF(ScrollableSpaceFromClientSpace(rectInClientSpace.Location), rectInClientSpace.Size);
        //}

        //protected RectangleF ScrollableSpaceFromDocumentSpace(RectangleF rectInDocumentSpace)
        //{
        //    return new RectangleF(ScrollableSpaceFromDocumentSpace(rectInDocumentSpace.Location), rectInDocumentSpace.Size);
        //}

        //protected RectangleF DocumentSpaceFromScrollableSpace(RectangleF rectInScrollableSpace)
        //{
        //    return new RectangleF(DocumentSpaceFromScrollableSpace(rectInScrollableSpace.Location), rectInScrollableSpace.Size);
        //}
    }
}
