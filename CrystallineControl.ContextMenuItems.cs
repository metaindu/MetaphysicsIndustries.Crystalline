
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
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineControl : UserControl
    {
        protected virtual void SetupContextMenuItems()
        {
            ToolStripMenuItem item;

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

            //item = _createPathingJunctionItem = new ToolStripMenuItem();
            //item.Text = "Create PathingJunction";
            //item.Name = "CreatePathingJunctionItem";
            //item.Click += new EventHandler(CreatePathingJunctionItem_Click);
            //ContextMenuStrip.Items.Add(item);

            //item = _createPathwayItem = new ToolStripMenuItem();
            //item.Text = "Create Pathway";
            //item.Name = "CreatePathwayItem";
            //item.Click += new EventHandler(CreatePathwayItem_Click);
            //ContextMenuStrip.Items.Add(item);

            ContextMenuStrip.Items.Add(new ToolStripSeparator());

            item = _deleteItem = new ToolStripMenuItem();
            item.Text = "Delete";
            item.Name = "DeleteItem";
            item.Click += new EventHandler(DeleteItem_Click);
            ContextMenuStrip.Items.Add(item);

            ContextMenuStrip.Items.Add(new ToolStripSeparator());

            item = _cleanupItem = new ToolStripMenuItem();
            item.Text = "Cleanup";
            item.Name = "CleanupItem";
            item.Click += new EventHandler(CleanupItem_Click);
            ContextMenuStrip.Items.Add(item);

            ContextMenuStrip.Items.Add(new ToolStripSeparator());

            //item2 = _addPathwayItem = new ToolStripMenuItem();
            //item2.Text = "Add Pathway";
            //item2.Name = "AddPathwayItem";
            //ContextMenuStrip.Items.Add(item2);

            //item = _addPathwayLeftItem = new ToolStripMenuItem();
            //item.Text = "Left";
            //item.Name = "AddPathwayLeftItem";
            //item.Click += new EventHandler(AddPathwayLeftItem_Click);
            //item2.DropDownItems.Add(item);

            //item = _addPathwayRightItem = new ToolStripMenuItem();
            //item.Text = "Right";
            //item.Name = "AddPathwayRightItem";
            //item.Click += new EventHandler(AddPathwayRightItem_Click);
            //item2.DropDownItems.Add(item);

            //item = _addPathwayUpItem = new ToolStripMenuItem();
            //item.Text = "Up";
            //item.Name = "AddPathwayUpItem";
            //item.Click += new EventHandler(AddPathwayUpItem_Click);
            //item2.DropDownItems.Add(item);

            //item = _addPathwayDownItem = new ToolStripMenuItem();
            //item.Text = "Down";
            //item.Name = "AddPathwayDownItem";
            //item.Click += new EventHandler(AddPathwayDownItem_Click);
            //item2.DropDownItems.Add(item);

            //ContextMenuStrip.Items.Add(new ToolStripSeparator());

            item = _showDebugInfoItem = new ToolStripMenuItem();
            item.Text = "Show Debug Info";
            item.Name = "ShowDebugInfoItem";
            item.Click += new EventHandler(ShowDebugInfoItem_Click);
            ContextMenuStrip.Items.Add(item);

        }

        protected virtual void UpdateContextMenuItems()
        {
                if (SelectionElement.Length> 0)
                {
                    _deleteItem.Enabled = true;
                }
                else
                {
                    _deleteItem.Enabled = false;
                }

            _deleteItem.Enabled = false;
            _cleanupItem.Enabled = false;

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
            element.Size = new SizeV(50, 20);
            element.Text = _names[Elements.Count % _names.Count];
            AddElement(element);
        }

        protected void AddMenuItemForElement<T>(ToolStripItem item, ToolStripMenuItem menu)
            where T : Element, new()
        {
            EventHandler clickHandler = new EventHandler(AddElement_Click<T>);
            AddMenuItem(item, menu, clickHandler);
        }

        public void AddMenuItem(ToolStripItem item, ToolStripMenuItem menu)
        {
            AddMenuItem(item, menu, null);
        }

        public void AddMenuItem(ToolStripItem item, ToolStripMenuItem menu, EventHandler clickHandler)
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

        protected void AddElementAtLocation<T>(T t, Vector location)
            where T : Element, new()
        {
            t.Location = location;
            AddElement(t);
        }


        protected virtual void CreatePathItem_Click(Object sender, EventArgs e)
        {
            Path p;
            Vector pj;

            //p = CreatePath();
            p = new Path();

            pj = (DocumentSpaceFromClientSpace(LastRightClickInClient));
            p.PathJoints.Add(pj);

            pj = (DocumentSpaceFromClientSpace(LastRightClickInClient) + new Vector(10, 0));
            p.PathJoints.Add(pj);

            AddPath(p);
            Invalidate();
        }

        protected virtual void DeleteItem_Click(Object sender, EventArgs e)
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
                    Selection.Remove(element);
                    //element.Dispose();
                }
                Selection.RemoveRange<Element>(s);
            

            Invalidate();
        }

        protected virtual void CleanupItem_Click(object sender, EventArgs e)
        {
            Cleanup();
        }

        protected virtual void ShowDebugInfoItem_Click(object sender, EventArgs e)
        {
            ShowDebugInfo = !ShowDebugInfo;

            Invalidate();
        }

        ToolStripMenuItem _createElementItem;
        ToolStripMenuItem _createPathItem;
        ToolStripMenuItem _deleteItem;
        ToolStripMenuItem _cleanupItem;

        ToolStripMenuItem _showDebugInfoItem;
    }
}
