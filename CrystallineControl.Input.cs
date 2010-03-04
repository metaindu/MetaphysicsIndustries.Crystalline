
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

                if (SelectionMode == SelectionModeType.Element)
                {
                    Element[] s1;
                    Set<Element> s2;

                    s1 = GetElementsAtPoint(_dragAnchorInDocument).ToArray();
                    if (s1.Length > 0)
                    {
                        s2 = Set<Element>.Intersection(SelectionElement,s1);
                        if (s2.Count > 0)
                        {
                            //clicked a previously-selected element

                            //if the shift key is down, remove it
                            if ((ModifierKeys & Keys.Shift) != Keys.None)
                            {
                                SelectionElement.RemoveRange(s2);
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
                                SelectionElement.Clear();
                            }

                            Element frontmost = s1[0];//.GetFirst();
                            int index = Framework.ZOrder.IndexOf(frontmost);
                            foreach (Element ee in s1)
                            {
                                //SelectionElement.Add(ee);
                                int index2 = Framework.ZOrder.IndexOf(ee);
                                if (index2 > index)
                                {
                                    index = index2;
                                    frontmost = ee;
                                }
                            }
                            BringToFront(frontmost);
                            SelectionElement.Add(frontmost);
                        }
                        _isDragSelecting = false;
                    }
                    else if (ModifierKeys == Keys.None)
                    {
                        SelectionElement.Clear();
                        _isDragSelecting = true;
                    }
                    else if (ModifierKeys == Keys.Control)
                    {
                    }
                    else if (ModifierKeys == Keys.Shift)
                    {
                    }

                }
                else if (SelectionMode == SelectionModeType.Path)
                {
                    Set<PathJoint> s1;
                    Set<PathJoint> s2;

                    s1 = GetPathJointsAtPoint(clickLocationInDocument);
                    if (s1.Count > 0)
                    {
                        s2 = Set<PathJoint>.Intersection(s1, SelectionPathJoint);
                        if (s2.Count > 0)
                        {
                            //clicked a previously-selected element
                        }
                        else
                        {
                            //clicked a different element
                            SelectionPathJoint.Clear();
                            foreach (PathJoint ee in s1)
                            {
                                SelectionPathJoint.Add(ee);
                            }
                        }
                        _isDragSelecting = false;
                    }
                    else
                    {
                        SelectionPathJoint.Clear();
                        _isDragSelecting = true;
                    }
                }
                //else if (SelectionMode == SelectionModeType.PathingJunction)
                //{
                //    Set<PathingJunction> s1;
                //    Set<PathingJunction> s2;

                //    s1 = GetPathingJunctionsAtPoint(clickLocationInDocument);
                //    if (s1.Count > 0)
                //    {
                //        s2 = Set<PathingJunction>.Intersection(s1, SelectionPathingJunction);
                //        if (s2.Count > 0)
                //        {
                //            //clicked a previously-selected element
                //        }
                //        else
                //        {
                //            //clicked a different element
                //            SelectionPathingJunction.Clear();
                //            foreach (PathingJunction ee in s1)
                //            {
                //                SelectionPathingJunction.Add(ee);
                //            }
                //        }
                //        _isDragSelecting = false;
                //    }
                //    else
                //    {
                //        SelectionPathingJunction.Clear();
                //        _isDragSelecting = true;
                //    }
                //}
                //else if (SelectionMode == SelectionModeType.Pathway)
                //{
                //    Set<Pathway> s1;
                //    Set<Pathway> s2;

                //    s1 = GetPathwaysAtPoint(clickLocationInDocument);
                //    if (s1.Count > 0)
                //    {
                //        s2 = Set<Pathway>.Intersection(s1, SelectionPathway);
                //        if (s2.Count > 0)
                //        {
                //            //clicked a previously-selected element
                //        }
                //        else
                //        {
                //            //clicked a different element
                //            SelectionPathway.Clear();
                //            foreach (Pathway ee in s1)
                //            {
                //                SelectionPathway.Add(ee);
                //            }
                //        }
                //        _isDragSelecting = false;
                //    }
                //    else
                //    {
                //        SelectionPathway.Clear();
                //        _isDragSelecting = true;
                //    }
                //}

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
                        if (SelectionMode == SelectionModeType.Element)
                        {
                            MoveElements(delta);
                        }
                        else if (SelectionMode == SelectionModeType.Path)	//selectingelement
                        {
                            MovePaths(delta);
                        }
                        //else if (SelectionMode == SelectionModeType.PathingJunction)
                        //{
                        //    foreach (PathingJunction p in SelectionPathingJunction)
                        //    {
                        //        //p.X += delta.Width;
                        //        //p.Y += delta.Height;
                        //        p.Move(p.Location + delta, null);
                        //    }
                        //}
                        //else if (SelectionMode == SelectionModeType.Pathway)
                        //{
                        //    foreach (Pathway p in SelectionPathway)
                        //    {
                        //        p.Move(p.Location + delta, null);
                        //    }
                        //}

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

            if (SelectionPathJoint.Count == 1)
            {
                foreach (PathJoint pj in SelectionPathJoint)
                {
                    if (pj == pj.ParentPath.PathJoints.First || pj == pj.ParentPath.PathJoints.Last)
                    {
                        if (pj == pj.ParentPath.PathJoints.First)
                        {
                            first = true;
                        }
                        p = pj.ParentPath;
                        pt = pj.Location;
                    }
                }
            }

            if (p != null)
            {
                Set<Element> s;
                s = GetElementsAtPoint(pt);
                if (s.Count > 0)
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

            foreach (PathJoint ve in SelectionPathJoint)
            {
                ve.Location += delta;
                if (p == null)
                {
                    if (ve == ve.ParentPath.PathJoints.First)
                    {
                        ve.ParentPath.From = null;
                    }
                    else if (ve == ve.ParentPath.PathJoints.Last)
                    {
                        ve.ParentPath.To = null;
                    }
                }
            }
        }

        private void MoveElements(Vector delta)
        {
            Set<Path> wholepaths;
            wholepaths = new Set<Path>();
            Set<Path> pathsToRoute = new Set<Path>();

            foreach (Element ee in SelectionElement)
            {
                foreach (Path p in ee.Inbound)
                {
                    if (!wholepaths.Contains(p) &&
                        p.To != null && SelectionElement.Contains(p.To) &&
                        p.From != null && SelectionElement.Contains(p.From))
                    {
                        wholepaths.Add(p);
                    }
                }
            }

            Set<Box> collidedBoxes = new Set<Box>();

            foreach (Element elem in SelectionElement)
            {
                //ve.Location += delta;
                if (elem.CanBeMoved)
                {
                    if (ElementCollisions)
                    {
                        elem.Move(elem.Location + delta, collidedBoxes);
                    }
                    else
                    {
                        elem.Location += delta;
                    }
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

            if (ElementCollisions)
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
                    if ((_dragAnchorInDocument- clickLocationInDocument).Length<3)
                    {
                        if (SelectionMode == SelectionModeType.Element)
                        {
                            SelectionElement.Clear();

                            Vector pointInDocSpace = _dragAnchorInDocument;
                            Element frontmost = GetFrontmostElementAtPointInDocumentSpace(pointInDocSpace);
                            if (frontmost != null)
                            {
                                BringToFront(frontmost);
                                SelectionElement.Add(frontmost);
                            }
                        }
                        else if (SelectionMode == SelectionModeType.Path)
                        {
                            Set<PathJoint> s1;
                            s1 = GetPathJointsAtPoint(_dragAnchorInDocument);
                            SelectionPathJoint.Clear();
                            foreach (PathJoint ee in s1)
                            {
                                SelectionPathJoint.Add(ee);
                            }
                        }
                        //else if (SelectionMode == SelectionModeType.PathingJunction)
                        //{
                        //    Set<PathingJunction> s1;
                        //    s1 = GetPathingJunctionsAtPoint(_dragAnchorInDocument);
                        //    SelectionPathingJunction.Clear();
                        //    foreach (PathingJunction p in s1)
                        //    {
                        //        SelectionPathingJunction.Add(p);
                        //    }
                        //}
                        //else if (SelectionMode == SelectionModeType.Pathway)
                        //{
                        //    Set<Pathway> s1;
                        //    s1 = GetPathwaysAtPoint(_dragAnchorInDocument);
                        //    SelectionPathway.Clear();
                        //    foreach (Pathway p in s1)
                        //    {
                        //        SelectionPathway.Add(p);
                        //    }
                        //}
                    }
                    else
                    {
                        RectangleV r = new RectangleV();
                        Vector pt;
                        SizeV s = new SizeV();

                        pt = clickLocationInDocument;//MainPanel.PointToClient(MainPanel.MousePosition);

                        //s.Width = Math.Abs(pt.X - _dragAnchorInDocument.X);
                        //s.Height = Math.Abs(pt.Y - _dragAnchorInDocument.Y);
                        s = new SizeV(
                            Math.Abs(pt.X - _dragAnchorInDocument.X),
                            Math.Abs(pt.Y - _dragAnchorInDocument.Y));

                        //pt.X = Math.Min(_dragAnchorInDocument.X, pt.X);
                        //pt.Y = Math.Min(_dragAnchorInDocument.Y, pt.Y);
                        pt = new Vector(
                                    Math.Min(_dragAnchorInDocument.X, pt.X),
                                    Math.Min(_dragAnchorInDocument.Y, pt.Y));

                        //r.Location = pt;
                        //r.Size = s;
                        r = new RectangleV(pt, s);

                        if (SelectionMode == SelectionModeType.Element)
                        {
                            Set<Element> set;
                            set = GetElementsInRect(r);
                            SelectionElement.Clear();
                            foreach (Element ee in set)
                            {
                                SelectionElement.Add(ee);
                            }
                            //need to update z-order
                        }
                        else if (SelectionMode == SelectionModeType.Path)
                        {
                            Set<PathJoint> set;
                            set = GetPathJointsInRect(r);
                            SelectionPathJoint.Clear();
                            foreach (PathJoint ee in set)
                            {
                                SelectionPathJoint.Add(ee);
                            }
                        }
                        //else if (SelectionMode == SelectionModeType.PathingJunction)
                        //{
                        //    Set<PathingJunction> set;
                        //    set = GetPathingJunctionsInRect(r);
                        //    SelectionPathingJunction.Clear();
                        //    foreach (PathingJunction p in set)
                        //    {
                        //        SelectionPathingJunction.Add(p);
                        //    }
                        //}
                        //else if (SelectionMode == SelectionModeType.Pathway)
                        //{
                        //    Set<Pathway> set;
                        //    set = GetPathwaysInRect(r);
                        //    SelectionPathway.Clear();
                        //    foreach (Pathway p in set)
                        //    {
                        //        SelectionPathway.Add(p);
                        //    }
                        //}
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
            Element element = GetFrontmostElementAtPointInDocumentSpace(docSpace);

            if (element != null)
            {
                try
                {
                    if (element.ShallProcessDoubleClick)
                    {
                        element.ProcessDoubleClick(this);
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
