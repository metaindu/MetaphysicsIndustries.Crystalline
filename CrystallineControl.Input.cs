
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Input.cs                                              *
 *  26 January 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that manages user input.                     *
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
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                ProcessMouseDown(e);
            }
            catch (Exception ee)
            {
                ReportException(ee);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                ProcessMouseMove(e);

                LastMouseMoveInClient = e.Location;
            }
            catch (Exception ee)
            {
                ReportException(ee);
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                ProcessMouseUp(e);
            }
            catch (Exception ee)
            {
                ReportException(ee);
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            try
            {
                ProcessMouseDoubleClick(e);
            }
            catch (Exception ee)
            {
                ReportException(ee);
            }

            base.OnMouseDoubleClick(e);
        }

        public enum SelectionQuantityE
        {
            Single,   //clicked within an item's bounding box
            Multiple, //dragging a selection box
        }

        public enum SelectionOperationE
        {
            None,
            Add,
            Remove,
        }

        private SelectionQuantityE _selectionQuantity;
        public SelectionQuantityE SelectionQuantity
        {
            get { return _selectionQuantity; }
        }

        public SelectionOperationE SelectionOperation
        {
            get
            {
                if (ModifierKeys == (Keys.Shift | Keys.Control))
                {
                }
                else if (ModifierKeys == Keys.Control)
                {
                }
                else if (ModifierKeys == Keys.Shift)
                {
                }

                return SelectionOperationE.None;
            }
        }

        protected virtual void ProcessMouseDown(MouseEventArgs e)
        {
            //selectionelement.Clear();
            //selectionpathjoint.Clear();

            //get element at cursor
            //if none, start drag-selection-box

            //Invalidate();

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Vector clickLocationInDocument = DocumentSpaceFromClientSpace(e.Location);

                _isClick = true;
                _dragAnchorInDocument = clickLocationInDocument;

                Box[] s1;

                s1 = Collection.Filter(GetEntitiesAtPointInDocument<Box>(_dragAnchorInDocument), Entity.IsSelectableEntity);

                if (s1.Length > 0)
                {
                    Box[] s2 = Set<Box>.Intersection(s1, Collection.Extract<Entity, Box>(Selection));
                    if (s2.Length > 0)
                    {
                        //clicked a previously-selected element

                        //if the shift key is down, remove it
                        if ((ModifierKeys & Keys.Shift) != Keys.None)
                        {
                            Selection.RemoveRange<Box>(s2);
                        }
                    }
                    else
                    {
                        //clicked a different element
                        //need to update z-order

                        //if the control key is down, add it
                        //otherwise, clear the selection
                        if ((ModifierKeys & Keys.Control) != Keys.None)
                        {
                        }
                        else
                        {
                            Selection.Clear();
                        }

                        Box frontmost = GetFrontmostBoxAtPoint<Box>(_dragAnchorInDocument, Entity.IsSelectableEntity);
                        if (frontmost != null)
                        {
                            BringToFront(frontmost);
                            Selection.Add(frontmost);
                        }
                    }
                    _isDragSelecting = false;
                }
                else if (ModifierKeys == Keys.None)
                {
                    Selection.Clear();
                    _isDragSelecting = true;
                }
                else if (ModifierKeys == Keys.Control)
                {
                }
                else if (ModifierKeys == Keys.Shift)
                {
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
            }
            else
            {
            }
        }

        protected virtual void ProcessMouseMove(MouseEventArgs e)
        {
            Vector clickLocationInDocument = DocumentSpaceFromClientSpace(e.Location);

            //_lastButtons = e.Button;
            ////Refresh();
            //Invalidate();

            //if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            //{
            //    e = e;
            //}

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //Invalidate();

                if (_isClick)
                {
                    if (_isDragSelecting)
                    {
                        InvalidateRectFromPointsInDocument(_selectionBoxPointInDocument, clickLocationInDocument);

                        //draw the box (do nothing here)
                        _selectionBoxPointInDocument = clickLocationInDocument;
                    }
                    else
                    {
                        Vector delta;
                        delta = clickLocationInDocument - _dragAnchorInDocument;

                        //move the selection
                        MoveElements(delta);

                        _dragAnchorInDocument = clickLocationInDocument;
                    }
                    Refresh();
                }
            }
            //else if (e.Button == MouseButtons.None)
            //{
            //    if (_isClick)
            //    {
            //    }
            //}
            //else
            //{
            //}
        }

        protected virtual void MovePaths(Vector delta)
        {
            Path p = null;
            Vector pt = new Vector();
            bool first = false; ;

            //if (SelectionPathJoint.Count == 1)
            //{
            //    foreach (PathJoint pj in SelectionPathJoint)
            //    {
            //        if (pj == pj.ParentPath.PathJoints.First || pj == pj.ParentPath.PathJoints.Last)
            //        {
            //            if (pj == pj.ParentPath.PathJoints.First)
            //            {
            //                first = true;
            //            }
            //            p = pj.ParentPath;
            //            pt = pj.Location;
            //        }
            //    }
            //}

            if (p != null)
            {
                Element[] s = GetEntitiesAtPointInDocument<Element>(pt);

                if (s.Length > 0)
                {
                    foreach (Element element in s)
                    {
                        if (first)
                        {
                            p.From = element;
                        }
                        else
                        {
                            p.To = element;
                        }

                        break;
                    }
                }
                else
                {
                    if (first)
                    {
                        p.From = null;
                    }
                    else
                    {
                        p.To = null;
                    }
                }
            }
        }

        private void MoveElements(Vector delta)
        {
            Set<Path> wholepaths = new Set<Path>();
            Set<Path> pathsToRoute = new Set<Path>();

            Box[] selectedBoxes = Collection.Extract<Entity, Box>(Selection);

            foreach (Element ee in SelectionElement)
            {
                foreach (Path p in ee.Inbound)
                {
                    if (!wholepaths.Contains(p) &&
                        p.To != null && Selection.Contains(p.To) &&
                        p.From != null && Selection.Contains(p.From))
                    {
                        wholepaths.Add(p);
                    }
                }
            }

            Set<Box> collidedBoxes = new Set<Box>();

            foreach (Box box in selectedBoxes)
            {
                //ve.Location += delta;
                if (box.CanBeMoved)
                {
                    if (BoxCollisions)
                    {
                        Framework.Move(box, box.Location + delta, collidedBoxes);
                    }
                    else
                    {
                        box.Location += delta;
                    }

                    if (box is Element)
                    {
                        Element elem = box as Element;

                        foreach (Path p in elem.Inbound)
                        {
                            pathsToRoute.Add(p);
                            //if (!wholepaths.Contains(p))
                            //{
                            //    //p.PathJoints[p.PathJoints.Count - 1].Location += delta;
                            //    RoutePath(p);
                            //}
                        }
                        foreach (Path p in elem.Outbound)
                        {
                            pathsToRoute.Add(p);
                            //if (!wholepaths.Contains(p))
                            //{
                            //    //p.PathJoints[0].Location += delta;
                            //    RoutePath(p);
                            //}
                        }
                    }
                }
            }

            if (BoxCollisions)
            {
                foreach (Box ib in collidedBoxes)
                {
                    if (ib is Element)
                    {
                        Element element = ib as Element;
                        foreach (Path path in element.Inbound)
                        {
                            pathsToRoute.Add(path);
                        }
                        foreach (Path path in element.Outbound)
                        {
                            pathsToRoute.Add(path);
                        }
                    }
                }
            }

            foreach (Path pathToRoute in pathsToRoute)
            {
                RoutePath(pathToRoute);
            }

            //int i;
            //int j;
            //foreach (Path p in wholepaths)
            //{
            //    j = p.PathJoints.Count;
            //    for (i = 0; i < j; i++)
            //    {
            //        p.PathJoints[i].Location += delta;
            //    }
            //}
        }

        protected virtual void ProcessMouseUp(MouseEventArgs e)
        {
            Vector clickLocationInDocument = DocumentSpaceFromClientSpace(e.Location);

            //Invalidate();

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (_isDragSelecting)
                {
                    if ((_dragAnchorInDocument - clickLocationInDocument).Length < 3)
                    {
                        Selection.Clear();

                        Vector pointInDocSpace = _dragAnchorInDocument;
                        Box frontmost = GetFrontmostBoxAtPoint<Box>(pointInDocSpace, Entity.IsSelectableEntity);
                        if (frontmost != null)
                        {
                            BringToFront(frontmost);
                            Selection.Add(frontmost);
                        }
                    }
                    else
                    {
                        RectangleV r = RectangleV.BoundingBoxFromPoints(_dragAnchorInDocument, clickLocationInDocument);

                        Box[] elems = Collection.Filter(GetEntitiesIntersectingRectInDocument<Box>(r), Entity.IsSelectableEntity);
                        Selection.Clear();
                        Selection.AddRange<Box>(elems);
                        BringToFront(elems);
                    }
                }
                else
                {
                    if (_dragAnchorInDocument == clickLocationInDocument)
                    {
                    }
                    else
                    {
                        //SortAllElementLists();
                    }
                }

                _isClick = false;
                _isDragSelecting = false;
                Refresh();
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                _isClick = false;
                _isDragSelecting = false;

                LastRightClickInClient = e.Location;
                UpdateContextMenuItems();
                ContextMenuStrip.Show(this, e.Location);
            }

        }

        protected virtual void ProcessMouseDoubleClick(MouseEventArgs e)
        {
            Vector docSpace = DocumentSpaceFromClientSpace(e.Location);
            Box box = GetFrontmostBoxAtPoint<Box>(docSpace);

            if (box != null)
            {
                try
                {
                    if (box.ShallProcessDoubleClick)
                    {
                        box.ProcessDoubleClick(this);
                    }
                }
                catch (Exception ee)
                {
                    //MessageBox.Show(this, "There was an exception while processing the double-click: " + ee.ToString());
                    ReportException(ee);
                }
            }
        }

        private Point _lastRightClickInClient;
        public Point LastRightClickInClient
        {
            get { return _lastRightClickInClient; }
            protected set
            {
                _lastRightClickInClient = value;
                _lastRightClickInDocument = DocumentSpaceFromClientSpace(value);
            }
        }
        private Vector _lastRightClickInDocument;
        public Vector LastRightClickInDocument
        {
            get { return _lastRightClickInDocument; }
        }


        private Point _lastMouseMoveInClient;
        public Point LastMouseMoveInClient
        {
            get { return _lastMouseMoveInClient; }
            protected set
            {
                _lastMouseMoveInClient = value;
                _lastMouseMoveInDocument = DocumentSpaceFromClientSpace(value);
            }
        }
        private Vector _lastMouseMoveInDocument;
        public Vector LastMouseMoveInDocument
        {
            get { return _lastMouseMoveInDocument; }
        }


        bool _isClick;
        Vector _dragAnchorInDocument;
        public bool _isDragSelecting;
        Vector _selectionBoxPointInDocument;
    }
}
