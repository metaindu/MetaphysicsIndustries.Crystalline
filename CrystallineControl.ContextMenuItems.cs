
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.ContextMenuItems.cs                                   *
 *  26 January 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that manages items on the context menu.      *
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
        protected virtual void SetupContextMenuItems()
        {
            ToolStripMenuItem item;
            ToolStripMenuItem item2;

            item = _createElementItem = new ToolStripMenuItem();
            item.Text = "Create Element";
            item.Name = "CreateElementItem";
            item.Click += new EventHandler(CreateElementItem_Click);
            ContextMenuStrip.Items.Add(item);

            item = _createPathItem = new ToolStripMenuItem();
            item.Text = "Create Path";
            item.Name = "CreatePathItem";
            item.Click += new EventHandler(CreatePathItem_Click);
            ContextMenuStrip.Items.Add(item);

            item = _createPathingJunctionItem = new ToolStripMenuItem();
            item.Text = "Create PathingJunction";
            item.Name = "CreatePathingJunctionItem";
            item.Click += new EventHandler(CreatePathingJunctionItem_Click);
            ContextMenuStrip.Items.Add(item);

            item = _createPathwayItem = new ToolStripMenuItem();
            item.Text = "Create Pathway";
            item.Name = "CreatePathwayItem";
            item.Click += new EventHandler(CreatePathwayItem_Click);
            ContextMenuStrip.Items.Add(item);

            ContextMenuStrip.Items.Add(new ToolStripSeparator());

            item = _deleteItem = new ToolStripMenuItem();
            item.Text = "Delete";
            item.Name = "DeleteItem";
            item.Click += new EventHandler(DeleteItem_Click);
            ContextMenuStrip.Items.Add(item);

            ContextMenuStrip.Items.Add(new ToolStripSeparator());

            item = _selectElementItem = new ToolStripMenuItem();
            item.Text = "Select Element";
            item.Name = "SelectElementItem";
            item.Click += new EventHandler(SelectElementItem_Click);
            ContextMenuStrip.Items.Add(item);

            item = _selectPathItem = new ToolStripMenuItem();
            item.Text = "Select Path";
            item.Name = "SelectPathItem";
            item.Click += new EventHandler(SelectPathItem_Click);
            ContextMenuStrip.Items.Add(item);

            item = _selectPathingJunctionItem = new ToolStripMenuItem();
            item.Text = "Select PathingJunction";
            item.Name = "SelectPathingJunctionItem";
            item.Click += new EventHandler(SelectPathingJunctionItem_Click);
            ContextMenuStrip.Items.Add(item);

            item = _selectPathwayItem = new ToolStripMenuItem();
            item.Text = "Select Pathway";
            item.Name = "SelectPathwayItem";
            item.Click += new EventHandler(SelectPathwayItem_Click);
            ContextMenuStrip.Items.Add(item);

            ContextMenuStrip.Items.Add(new ToolStripSeparator());

            item = _cleanupItem = new ToolStripMenuItem();
            item.Text = "Cleanup";
            item.Name = "CleanupItem";
            item.Click += new EventHandler(CleanupItem_Click);
            ContextMenuStrip.Items.Add(item);

            ContextMenuStrip.Items.Add(new ToolStripSeparator());

            item2 = _addPathwayItem = new ToolStripMenuItem();
            item2.Text = "Add Pathway";
            item2.Name = "AddPathwayItem";
            ContextMenuStrip.Items.Add(item2);

            item = _addPathwayLeftItem = new ToolStripMenuItem();
            item.Text = "Left";
            item.Name = "AddPathwayLeftItem";
            item.Click += new EventHandler(AddPathwayLeftItem_Click);
            item2.DropDownItems.Add(item);

            item = _addPathwayRightItem = new ToolStripMenuItem();
            item.Text = "Right";
            item.Name = "AddPathwayRightItem";
            item.Click += new EventHandler(AddPathwayRightItem_Click);
            item2.DropDownItems.Add(item);

            item = _addPathwayUpItem = new ToolStripMenuItem();
            item.Text = "Up";
            item.Name = "AddPathwayUpItem";
            item.Click += new EventHandler(AddPathwayUpItem_Click);
            item2.DropDownItems.Add(item);

            item = _addPathwayDownItem = new ToolStripMenuItem();
            item.Text = "Down";
            item.Name = "AddPathwayDownItem";
            item.Click += new EventHandler(AddPathwayDownItem_Click);
            item2.DropDownItems.Add(item);

            ContextMenuStrip.Items.Add(new ToolStripSeparator());

            item = _showDebugInfoItem = new ToolStripMenuItem();
            item.Text = "Show Debug Info";
            item.Name = "ShowDebugInfoItem";
            item.Click += new EventHandler(ShowDebugInfoItem_Click);
            ContextMenuStrip.Items.Add(item);

        }

        protected virtual void UpdateContextMenuItems()
        {
            if (SelectionMode == SelectionModeType.Element)
            {
                if (SelectionElement.Count > 0)
                {
                    _deleteItem.Enabled = true;
                }
                else
                {
                    _deleteItem.Enabled = false;
                }

                _selectElementItem.Checked = true;
                _selectPathItem.Checked = false;
                _selectPathingJunctionItem.Checked = false;
                _selectPathwayItem.Checked = false;

                _addPathwayItem.Enabled = false;
            }
            else if (SelectionMode == SelectionModeType.Path)
            {
                if (SelectionPathJoint.Count > 0)
                {
                    _deleteItem.Enabled = true;
                }
                else
                {
                    _deleteItem.Enabled = false;
                }

                _selectElementItem.Checked = false;
                _selectPathItem.Checked = true;
                _selectPathingJunctionItem.Checked = false;
                _selectPathwayItem.Checked = false;

                _addPathwayItem.Enabled = false;
            }
            else if (SelectionMode == SelectionModeType.PathingJunction)
            {
                if (SelectionPathingJunction.Count > 0)
                {
                    _deleteItem.Enabled = true;
                }
                else
                {
                    _deleteItem.Enabled = false;
                }

                _selectElementItem.Checked = false;
                _selectPathItem.Checked = false;
                _selectPathingJunctionItem.Checked = true;
                _selectPathwayItem.Checked = false;

                if (SelectionPathingJunction.Count > 0)
                {
                    _addPathwayItem.Enabled = true;
                }
                else
                {
                    _addPathwayItem.Enabled = false;
                }
            }
            else if (SelectionMode == SelectionModeType.Pathway)
            {
                if (SelectionPathway.Count > 0)
                {
                    _deleteItem.Enabled = true;
                }
                else
                {
                    _deleteItem.Enabled = false;
                }

                _selectElementItem.Checked = false;
                _selectPathItem.Checked = false;
                _selectPathingJunctionItem.Checked = false;
                _selectPathwayItem.Checked = true;

                _addPathwayItem.Enabled = false;
            }

            _createPathingJunctionItem.Enabled = false;
            _createPathwayItem.Enabled = false;
            _selectPathingJunctionItem.Enabled = false;
            _selectPathwayItem.Enabled = false;
            _deleteItem.Enabled = false;
            _cleanupItem.Enabled = false;
            //_addPathwayItem.Enabled = false;

            _showDebugInfoItem.Checked = ShowDebugInfo;
        }

        public virtual void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
        }

        protected virtual void CreateElementItem_Click(Object sender, EventArgs e)
        {
            Element element;
            element = new Element();// CreateElement();
            element.Location = DocumentSpaceFromClientSpace(LastRightClickInClient);
            element.Size = new SizeF(50, 20);
            element.Text = _names[Elements.Count % _names.Count];
            AddElement(element);
        }

        protected void AddMenuItemForElement<T>(ToolStripItem item, ToolStripMenuItem menu)
            where T : Element, new()
        {
            EventHandler clickHandler = new EventHandler(AddElement_Click<T>);
            AddMenuItem(item, menu, clickHandler);
        }

        protected void AddMenuItem(ToolStripItem item, ToolStripMenuItem menu)
        {
            AddMenuItem(item, menu, null);
        }

        protected void AddMenuItem(ToolStripItem item, ToolStripMenuItem menu, EventHandler clickHandler)
        {
            if (item == null) { throw new ArgumentNullException("item"); }
							
            if (clickHandler != null)
            {
                item.Click += clickHandler;
            }

            if (menu != null)
            {
                menu.DropDownItems.Add(item);
            }
            else
            {
                ContextMenuStrip.Items.Add(item);
            }
        }

        protected void AddElement_Click<T>(object sender, EventArgs e)
            where T : Element, new()
        {
            T t = new T();
            AddElementAtLocation<T>(t, LastRightClickInDocument);
        }

        protected void AddElementAtLocation<T>(T t, PointF location) 
            where T : Element, new()
        {
            t.Location = location;
            AddElement(t);
        }


        protected virtual void CreatePathItem_Click(Object sender, EventArgs e)
        {
            Path p;
            PathJoint pj;

            //p = CreatePath();
            p = new Path();

            pj = new PathJoint(DocumentSpaceFromClientSpace(LastRightClickInClient));
            p.PathJoints.Add(pj);

            pj = new PathJoint(DocumentSpaceFromClientSpace(LastRightClickInClient) + new System.Drawing.Size(10, 0));
            p.PathJoints.Add(pj);

            AddPath(p);
            Invalidate();
        }

        protected virtual void DeleteItem_Click(Object sender, EventArgs e)
        {
            if (SelectionMode == SelectionModeType.Element)
            {
                Set<Element> s;
                s = new Set<Element>();
                foreach (Element element in SelectionElement)
                {
                    s.Add(element);
                }
                Set<Path> removepaths;
                removepaths = new Set<Path>();
                foreach (Element element in s)
                {
                    element.Inbound.Clear();
                    element.Outbound.Clear();
                    Elements.Remove(element);
                    SelectionElement.Remove(element);
                    element.Dispose();
                }
                SelectionElement.Clear();
            }
            else if (SelectionMode == SelectionModeType.Path)
            {
                Set<Path> s;
                s = new Set<Path>();
                foreach (PathJoint pathJoint in SelectionPathJoint)
                {
                    s.Add(pathJoint.ParentPath);
                    if (pathJoint.ParentPath.From != null && pathJoint == pathJoint.ParentPath.PathJoints[0])
                    {
                        pathJoint.ParentPath.From = null;
                    }
                    if (pathJoint.ParentPath.To != null && pathJoint == pathJoint.ParentPath.PathJoints[pathJoint.ParentPath.PathJoints.Count - 1])
                    {
                        pathJoint.ParentPath.To = null;
                    }
                    pathJoint.ParentPath = null;
                }
                //foreach (Path e in s)
                //{
                //	//Paths.Remove(e);
                //	if (
                //}
                SelectionPathJoint.Clear();
            }
            else if (SelectionMode == SelectionModeType.PathingJunction)
            {
                Set<PathingJunction> s;
                s = new Set<PathingJunction>();
                foreach (PathingJunction p in SelectionPathingJunction)
                {
                    s.Add(p);
                }
                Set<Path> removepaths;
                removepaths = new Set<Path>();
                foreach (PathingJunction p in s)
                {
                    RemovePathingJunction(p);
                }
                SelectionPathingJunction.Clear();
            }
            else if (SelectionMode == SelectionModeType.Pathway)
            {
                Debug.Fail("SelectionMode == SelectionModeType.Pathway");
            }

            Invalidate();
        }

        protected virtual void SelectElementItem_Click(object sender, EventArgs e)
        {
            this.SelectionMode = SelectionModeType.Element;
            this.Invalidate();
        }
        protected virtual void SelectPathItem_Click(object sender, EventArgs e)
        {
            this.SelectionMode = SelectionModeType.Path;
            this.Invalidate();
        }
        protected virtual void CleanupItem_Click(object sender, EventArgs e)
        {
            Cleanup();
        }

        protected virtual void CreatePathingJunctionItem_Click(object sender, EventArgs e)
        {
            PathingJunction p;
            p = new PathingJunction();// this.CreatePathingJunction();

            //p->Location = this->lastrightclick;
            //p.X = (float)DocumentSpaceFromClientSpace(LastRightClickInClient).X;
            //p.Y = (float)DocumentSpaceFromClientSpace(LastRightClickInClient).Y;
            p.Location = DocumentSpaceFromClientSpace(LastRightClickInClient);

            //p->Size = SizeF(50, 20);

            this.PathingJunctions.Add(p);
            this.Invalidate();

        }
        protected virtual void SelectPathingJunctionItem_Click(object sender, EventArgs e)
        {
            this.SelectionMode = SelectionModeType.PathingJunction;
            this.Invalidate();
        }
        protected virtual void CreatePathwayItem_Click(object sender, EventArgs e)
        {
            Pathway p;
            p = new Pathway();// this.CreatePathway();

            //p->Location = this->lastrightclick;
            //p->X = (float)this->lastrightclick.X;
            //p->Y = (float)this->lastrightclick.Y;

            //p->Size = SizeF(50, 20);

            this.Pathways.Add(p);
            this.Invalidate();
        }
        protected virtual void SelectPathwayItem_Click(object sender, EventArgs e)
        {
            this.SelectionMode = SelectionModeType.Pathway;
            this.Invalidate();
        }

        protected virtual void AddPathwayLeftItem_Click(object sender, EventArgs e)
        {
            if (this.SelectionMode == SelectionModeType.PathingJunction)
            {
                foreach (PathingJunction pj in this.SelectionPathingJunction)
                {
                    Pathway pw;

                    pw = new Pathway();// this.CreatePathway();
                    pw.IsVertical = false;
                    pw.X = pj.Left - pw.Width;
                    pw.Y = pj.Y;
                    pj.LeftPathway = pw;
                    pw.RightDown = pj;
                    this.Pathways.Add(pw);
                }

                this.Invalidate();
            }
        }
        protected virtual void AddPathwayRightItem_Click(object sender, EventArgs e)
        {
            if (this.SelectionMode == SelectionModeType.PathingJunction)
            {
                foreach (PathingJunction pj in this.SelectionPathingJunction)
                {
                    Pathway pw;

                    pw = new Pathway();//this.CreatePathway();
                    pw.IsVertical = false;
                    pw.X = pj.Right;
                    pw.Y = pj.Y;
                    pj.RightPathway = pw;
                    pw.LeftUp = pj;
                    this.Pathways.Add(pw);
                }

                this.Invalidate();
            }
        }
        protected virtual void AddPathwayUpItem_Click(object sender, EventArgs e)
        {
            if (this.SelectionMode == SelectionModeType.PathingJunction)
            {
                foreach (PathingJunction pj in this.SelectionPathingJunction)
                {
                    Pathway pw;

                    pw = new Pathway();//this.CreatePathway();
                    pw.IsVertical = true;
                    pw.X = pj.X;
                    pw.Y = pj.Y - pw.Height;
                    pj.UpPathway = pw;
                    pw.RightDown = pj;
                    this.Pathways.Add(pw);
                }

                this.Invalidate();
            }
        }
        protected virtual void AddPathwayDownItem_Click(object sender, EventArgs e)
        {
            if (this.SelectionMode == SelectionModeType.PathingJunction)
            {
                foreach (PathingJunction pj in this.SelectionPathingJunction)
                {
                    Pathway pw;

                    pw = new Pathway();//this.CreatePathway();
                    pw.IsVertical = true;
                    pw.X = pj.X;
                    pw.Y = pj.Bottom;
                    pj.DownPathway = pw;
                    pw.LeftUp = pj;
                    this.Pathways.Add(pw);
                }

                this.Invalidate();
            }
        }
        protected virtual void ShowDebugInfoItem_Click(object sender, EventArgs e)
        {
            ShowDebugInfo = !ShowDebugInfo;

            Invalidate();
        }
        
        ToolStripMenuItem _createElementItem;
        ToolStripMenuItem _createPathItem;
        ToolStripMenuItem _deleteItem;
        ToolStripMenuItem _selectElementItem;
        ToolStripMenuItem _selectPathItem;
        ToolStripMenuItem _cleanupItem;

        ToolStripMenuItem _createPathingJunctionItem;
        ToolStripMenuItem _selectPathingJunctionItem;

        ToolStripMenuItem _createPathwayItem;
        ToolStripMenuItem _selectPathwayItem;

        ToolStripMenuItem _addPathwayItem;
        ToolStripMenuItem _addPathwayLeftItem;
        ToolStripMenuItem _addPathwayRightItem;
        ToolStripMenuItem _addPathwayUpItem;
        ToolStripMenuItem _addPathwayDownItem;

        ToolStripMenuItem _showDebugInfoItem;
        //ToolStripMenuItem
    }
}
