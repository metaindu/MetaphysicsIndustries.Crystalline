
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Render.cs                                             *
 *  26 January 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that manages rendering.                      *
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
using System.Drawing.Drawing2D;
using MetaphysicsIndustries.Utilities;
using System.Drawing.Imaging;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineControl : UserControl
    {
        private SelectionRenderStyle _selectionRenderStyle;
        protected virtual SelectionRenderStyle SelectionRenderStyle
        {
            get { return _selectionRenderStyle; }
            set { _selectionRenderStyle = value; }
        }
        public float SelectionOutlineMargin
        {
            get { return 10; }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                ProcessPaint(e);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, "There was an error: \r\n" + ex.ToString());
                ReportException(ex);
            }

            base.OnPaint(e);
        }


        protected virtual void ProcessPaint(PaintEventArgs e)
        {
            PaintEventArgs _e = e;

            Graphics g;

            g = e.Graphics;

            //if (_zoom != 1 && _zoom != 0)
            //{
            //    g.ScaleTransform(_zoom, _zoom);
            //}

            Render(g);

            //if (_zoom != 1 && _zoom != 0)
            //{
            //    float invzoom = 1.0f / _zoom;
            //    g.ScaleTransform(invzoom, invzoom);
            //}
        }

        private bool _shallRenderElements = true;
        public virtual bool ShallRenderElements
        {
            get { return _shallRenderElements; }
            set { _shallRenderElements = value; }
        }

        private bool _shallRenderPaths = true;
        public virtual bool ShallRenderPaths
        {
            get { return _shallRenderPaths; }
            set { _shallRenderPaths = value; }
        }

        protected virtual void Render(Graphics g)
        {
            if (ShowDebugInfo)
            {
                RenderDebugInfo(g);
            }

            RenderWithTopLeft(g, DocumentSpaceFromClientSpace(new Point(0, 0)));
        }

        private void RenderWithTopLeft(Graphics g, Vector topLeft)
        {
            //g.TranslateTransform(AutoScrollPosition.X
            //    + _scrollableAreaInDocument.X
            //    , AutoScrollPosition.Y
            //    + _scrollableAreaInDocument.Y
            //    );
            g.TranslateTransform(-topLeft.X, -topLeft.Y);

            if (ShowDebugInfo)
            {
                RenderDebugInfo2(g);
            }

            if (ShallRenderPaths)
            {
                RenderPaths(g);
            }

            if (ShallRenderElements)
            {
                RenderBoxes(g);
            }

            foreach (Entity ent in Entities)
            {
                if (!(ent is Element) &&
                    !(ent is Path))
                {
                    RenderEntity(g, ent);
                }
            }

            if (_isDragSelecting && _isClick)
            {
                RectangleV r = RectangleV.BoundingBoxFromPoints(_dragAnchorInDocument, _selectionBoxPointInDocument);

                g.DrawRectangle(Pens.Gray, Rectangle.Truncate(r));
            }
            if (_isDragSelecting || _isClick)
            {
            }
        }

        protected virtual void RenderEntity(Graphics g, Entity ent)
        {
            Pen pen = ChoosePenForEntity(ent);

            if (ent is Path)
            {
                (ent as Path).Render(g, pen, Brushes.Black, Font, ShowPathJoints, ShowPathArrows);
            }
            else
            {
                ent.Render(g, pen, Brushes.Black, Font);
            }
        }

        protected virtual void RenderPaths(Graphics g)
        {
            foreach (Path p in Entities.Extract<Path>())
            {
                RenderEntity(g, p);
            }
        }

        protected virtual void RenderBoxes(Graphics g)
        {
            foreach (Box box in Framework.ZOrder)
            {
                RenderEntity(g, box);
            }
        }

        public virtual Pen ChoosePenForEntity(Entity ent)
        {
            if (Selection.Contains(ent))
            {
                return _selectionPen;
            }
            else if (ent is Path)
            {
                return ChoosePenForPath(ent as Path);
            }
            else if (ent is Element)
            {
                return ChoosePenForElement(ent as Element);
            }
            else
            {
                return Pens.Black;
            }
        }

        protected virtual Pen ChoosePenForPath(Path path)
        {
            return Pens.Black;
        }

        protected virtual Pen ChoosePenForElement(Element element)
        {
            return Pens.Black;
        }

        protected virtual void RenderDebugInfo(Graphics g)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("elements = ");
            sb.Append(Entities.Extract<Element>().Length);
            sb.Append("\r\n");
            sb.Append("paths = ");
            sb.Append(Entities.Extract<Path>().Length);
            sb.Append("\r\n");
            //sb.Append("pathingjunctions = ");
            //sb.Append(_pathingJunctions.Count);
            //sb.Append("\r\n");
            //sb.Append("pathways = ");
            //sb.Append(_pathways.Count);
            //sb.Append("\r\n");
            sb.Append("\r\n");
            //sb.Append("SortElementsLeft = ");
            //sb.Append(SortElementsLeft.Count);
            //sb.Append("\r\n");
            //sb.Append("SortElementsRight = ");
            //sb.Append(SortElementsRight.Count);
            //sb.Append("\r\n");
            //sb.Append("SortElementsUp = ");
            //sb.Append(SortElementsTop.Count);
            //sb.Append("\r\n");
            //sb.Append("SortElementsDown = ");
            //sb.Append(SortElementsBottom.Count);
            //sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("LastRightClickInClient = ");
            sb.Append(LastRightClickInClient);
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("selection = ");
            sb.Append(Selection.Count);
            Element[] elements = Selection.Extract<Element>();
            if (elements.Length > 0)
            {
                Element sel = null;
                foreach (Element element in elements)
                {
                    sel = element;
                    break;
                }
                sb.Append(" { ");
                if (sel.Text != null)
                {
                    sb.Append("\"");
                    sb.Append(sel.Text.ToString());
                    sb.Append("\", ");
                }
                sb.Append(sel.Rect);
                //sb.Append(", R=");
                //sb.Append(sel.Rotation);
                sb.Append(" }");
            }
            sb.Append("\r\n");
            //sb.Append("selectionpath = ");
            //sb.Append(SelectionPathJoint.Count);
            //sb.Append("\r\n");
            //sb.Append("selectionpathingjunction = ");
            //sb.Append(SelectionPathingJunction.Count);
            //sb.Append("\r\n");
            //sb.Append("selectionpathway = ");
            //sb.Append(SelectionPathway.Count);
            //sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("isclick = ");
            sb.Append(_isClick);
            sb.Append("\r\n");
            sb.Append("draganchor = ");
            sb.Append(_dragAnchorInDocument);
            sb.Append("\r\n");
            sb.Append("isdragselecting = ");
            sb.Append(_isDragSelecting);
            sb.Append("\r\n");
            sb.Append("selectionboxpoint = ");
            sb.Append(_selectionBoxPointInDocument);
            sb.Append("\r\n");
            sb.Append("\r\n");

            //sb.Append("lastButtons = ");
            //sb.Append(_lastButtons.ToString());
            //sb.Append("\r\n");
            //sb.Append("\r\n");

            sb.Append("AutoScroll = ");
            sb.Append(AutoScroll);
            sb.Append("\r\n");

            sb.Append("AutoScrollPosition = { ");
            sb.Append(AutoScrollPosition.X);
            sb.Append(", ");
            sb.Append(AutoScrollPosition.Y);
            sb.Append(" }");
            sb.Append("\r\n");

            sb.Append("AutoScrollMargin = { ");
            sb.Append(AutoScrollMargin.Width);
            sb.Append(", ");
            sb.Append(AutoScrollMargin.Height);
            sb.Append(" }");
            sb.Append("\r\n");

            sb.Append("AutoScrollMinSize = { ");
            sb.Append(AutoScrollMinSize.Width);
            sb.Append(", ");
            sb.Append(AutoScrollMinSize.Height);
            sb.Append(" } ");
            sb.Append("\r\n");

            sb.Append("AutoScrollOffset = { ");
            sb.Append(AutoScrollOffset.X);
            sb.Append(", ");
            sb.Append(AutoScrollOffset.Y);
            sb.Append(" }");
            sb.Append("\r\n");

            sb.Append("Bounds = { ");
            sb.Append(Framework.Bounds.Left);
            sb.Append(", ");
            sb.Append(Framework.Bounds.Top);
            sb.Append(", ");
            sb.Append(Framework.Bounds.Right);
            sb.Append(", ");
            sb.Append(Framework.Bounds.Bottom);
            sb.Append(" }");
            sb.Append("\r\n");

            sb.Append("ScrollableAreaInDocument = { ");
            sb.Append(_scrollableAreaInDocument.Left);
            sb.Append(", ");
            sb.Append(_scrollableAreaInDocument.Top);
            sb.Append(", ");
            sb.Append(_scrollableAreaInDocument.Right);
            sb.Append(", ");
            sb.Append(_scrollableAreaInDocument.Bottom);
            sb.Append(" }");
            sb.Append("\r\n");

            sb.Append("ClientRectInDocument = ");
            sb.Append(GetClientRectInDocument());
            sb.Append("\r\n");

            g.DrawString(sb.ToString(), Font, Brushes.Blue, 5, 5);

            g.DrawRectangle(Pens.Green, new Rectangle(0, 0, 50, 50));
        }

        protected virtual void RenderDebugInfo2(Graphics g)
        {
            g.DrawRectangle(Pens.Red, Rectangle.Round(Framework.Bounds));
            g.DrawRectangle(Pens.Orange, new Rectangle(1, 1, 48, 48));
        }

        private bool _showDebugInfo = false;
        public virtual bool ShowDebugInfo
        {
            get { return _showDebugInfo; }
            set { _showDebugInfo = value; }
        }

        private bool _showPathJoints = false;
        public bool ShowPathJoints
        {
            get { return _showPathJoints; }
            set { _showPathJoints = value; }
        }

        private bool _showPathArrows = true;
        public bool ShowPathArrows
        {
            get { return _showPathArrows; }
            set { _showPathArrows = value; }
        }

        protected virtual RectangleV CalcInvalidationRectForBox(Box box)
        {
            RectangleV rect = box.Rect;
            rect.Inflate(4, 4);
            return rect;
        }

        public virtual void InvalidateRectInClient(Rectangle rect)
        {
            //Invalidate(Rectangle.Ceiling(rect));
            Invalidate(rect);
        }
        public virtual void InvalidateRectInDocument(RectangleV rect)
        {
            InvalidateRectInClient(ClientSpaceFromDocumentSpace(rect));
        }

        public virtual void InvalidateRectFromEntity(Entity entity)
        {
            RectangleV rect = entity.GetBoundingBox();
            rect = rect.Inflate(2, 2);
            InvalidateRectInDocument(rect);
        }

        public virtual void InvalidateRectFromEntities<T>(IEnumerable<T> entities)
            where T : Entity
        {
            foreach (Entity ent in entities)
            {
                InvalidateRectFromEntity(ent);
            }
        }

        //public virtual void InvalidateRectFromPath(Path path)
        //{
        //    if (path.PathJoints.Count > 0)
        //    {
        //        RectangleF rect = path.GetBoundingBox();

        //        rect.Inflate(2, 2);

        //        InvalidateRectInDocument(rect);

        //        PointF pt = path.PathJoints[path.PathJoints.Count - 1].Location;
        //        float size = path.ArrowSize;

        //        rect = new RectangleF(pt - new SizeF(size, size), new SizeF(2 * size, 2 * size));
        //        rect.Inflate(2, 2);

        //        InvalidateRectInDocument(rect);
        //    }
        //}

        //public void InvalidateRectFromElement(Element element)
        //{
        //    RectangleF rect = CalcInvalidationRectForBox(element);

        //    if (Selection.Contains(element))
        //    {
        //        float marginSize;
        //        switch (SelectionRenderStyle)
        //        {
        //            case SelectionRenderStyle.Outline: 
        //                marginSize = SelectionOutlineMargin; 
        //                break;
        //            default:
        //                marginSize = 2;
        //                break;
        //        }
        //        rect.Inflate(marginSize, marginSize);
        //    }

        //    InvalidateRectInDocument(rect);
        //}

        //public void InvalidateRectFromBox(Box box)
        //{
        //    RectangleF rect = CalcInvalidationRectForBox(box);
        //    InvalidateRectInDocument(rect);
        //}

        public void InvalidateRectFromPointsInDocument(params Vector[] pts)
        {
            float left = pts[0].X;
            float top = pts[0].Y;
            float right = left;
            float bottom = top;

            foreach (Vector v in pts)
            {
                left = Math.Min(left, v.X);
                top = Math.Min(top, v.Y);
                right = Math.Max(right, v.X);
                bottom = Math.Max(Bottom, v.Y);
            }

            RectangleV rect = new RectangleV(left, top, right - left, bottom - top);
            rect.Inflate(2, 2);
            InvalidateRectInDocument(rect);
        }

        protected void SaveContentsAsImagePrompt()
        {
            Image image = SaveContentsToImage();
            if (image != null)
            {
                SaveImagePrompt(image, this);
            }
        }

        protected Image SaveContentsToImage()
        {
            try
            {
                RectangleV rect = this.Framework.Bounds;
                rect = rect.Union(Entity.GetBoundingBoxFromEntities(Entities.ToArray()));
                rect = rect.Inflate(100, 100);

                Rectangle rect2 = ClientSpaceFromDocumentSpace(rect);
                rect2.X = 0;
                rect2.Y = 0;
                Bitmap bmp = new Bitmap(rect2.Width, rect2.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(Brushes.White, rect2);
                    this.RenderWithTopLeft(g, rect.TopLeft);
                }

                return bmp;
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }

            return null;
        }

        //move this to utilites
        public static void SaveImagePrompt(Image image)
        {
            SaveImagePrompt(image, null);
        }
        public static void SaveImagePrompt(Image image, IWin32Window owner)
        {
            if (image == null) { throw new ArgumentNullException("image"); }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bitmap Images (*.bmp)|*.bmp|JPEG Images (*.jpg, *.jpeg)|*.jpg;*.jpeg|GIF Images (*.gif)|*.gif|PNG Images (*.png)|*.png|All Files (*.*)|*.*";

            DialogResult res;
            if (owner == null)
            {
                res = sfd.ShowDialog();
            }
            else
            {
                res = sfd.ShowDialog(owner);
            }

            if (res == DialogResult.OK)
            {
                ImageFormat format = ImageFormat.Bmp;
                switch (sfd.FilterIndex)
                {
                    case 2: format = System.Drawing.Imaging.ImageFormat.Jpeg; break;
                    case 3: format = System.Drawing.Imaging.ImageFormat.Gif; break;
                    case 4: format = System.Drawing.Imaging.ImageFormat.Png; break;
                }

                string filename = sfd.FileName;

                SaveImage(image, filename, format);
            }
        }

        //move this to utilities
        public static void SaveImage(Image image, string filename)
        {
            SaveImage(image, filename, ImageFormat.Bmp);
        }
        public static void SaveImage(Image image, string filename, ImageFormat format)
        {
            if (image == null) { throw new ArgumentNullException("image"); }
            if (string.IsNullOrEmpty(filename)) { throw new ArgumentNullException("filename"); }

            image.Save(filename, format);
        }

        public void BringForward(Box box)
        {
            Framework.BringForward(box);
            InvalidateRectFromEntity(box);
        }
        public void SendBackward(Box box)
        {
            Framework.SendBackward(box);
            InvalidateRectFromEntity(box);
        }
        public void BringToFront(Box box)
        {
            Framework.BringToFront(box);
            InvalidateRectFromEntity(box);
        }
        public void SendToBack(Box box)
        {
            Framework.SendToBack(box);
            InvalidateRectFromEntity(box);
        }
        public void BringToFront(IEnumerable<Box> boxes)
        {
            foreach (Box box in Collection.Reverse<Box>(boxes))
            {
                BringToFront(box);
            }
        }

        Pen _selectionPen;
        Pen _selectionOutlinePen;
    }
}
