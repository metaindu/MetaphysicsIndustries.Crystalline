
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

            Render(g);
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

        private bool _shallRenderPathways = false;
        public virtual bool ShallRenderPathways
        {
            get { return _shallRenderPathways; }
            set { _shallRenderPathways = value; }
        }

        private bool _shallRenderPathingJunctions = false;
        public virtual bool ShallRenderPathingJunctions
        {
            get { return _shallRenderPathingJunctions; }
            set { _shallRenderPathingJunctions = value; }
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
                RenderElements(g);
            }

            //if (ShallRenderPathingJunctions)
            //{
            //    RenderPathingJunctions(g);
            //}

            //if (ShallRenderPathways)
            //{
            //    RenderPathways(g);
            //}

            RenderBoxes(g);

            if (_isDragSelecting && _isClick)
            {
                Vector topleft = new Vector(
                                Math.Min(_dragAnchorInDocument.X, _selectionBoxPointInDocument.X),
                                Math.Min(_dragAnchorInDocument.Y, _selectionBoxPointInDocument.Y));

                Vector bottomright = new Vector(
                                Math.Max(_dragAnchorInDocument.X, _selectionBoxPointInDocument.X),
                                Math.Max(_dragAnchorInDocument.Y, _selectionBoxPointInDocument.Y));


                RectangleV r = new RectangleV(topleft, bottomright);

                g.DrawRectangle(Pens.Gray, Rectangle.Truncate(r));
            }
        }

        //protected virtual void RenderPathways(Graphics g)
        //{
        //    foreach (Pathway p in Pathways)
        //    {
        //        RenderPathway(g, p);
        //    }
        //}

        //protected virtual void RenderPathway(Graphics g, Pathway p)
        //{
        //    Pen pen = ChoosePenForPathway(p);

        //    p.Render(g, pen, pen.Brush, Font);
        //}

        //protected virtual void RenderPathingJunctions(Graphics g)
        //{
        //    foreach (PathingJunction p in PathingJunctions)
        //    {
        //        RenderPathingJunction(g, p);
        //    }
        //}

        //protected virtual void RenderPathingJunction(Graphics g, PathingJunction p)
        //{
        //    Pen pen = ChoosePenForPathingJunction(p);

        //    p.Render(g, pen, pen.Brush, Font);
        //}

        protected virtual void RenderPaths(Graphics g)
        {
            //Set<Path> nojointpaths;
            //nojointpaths = new Set<Path>();
            //foreach (PathJoint pj in SelectionPathJoint)
            //{
            //    nojointpaths.Add(pj.ParentPath);
            //}
            foreach (Path p in Paths)
            {
                Pen pen = ChoosePenForPath(p);
                Brush brush = pen.Brush;

                //if (nojointpaths.Contains(p))
                //{
                //    p.Render(g, pen, brush, Font, false, ShowPathArrows);

                //    int i;
                //    int j;
                //    Vector pj;

                //    j = p.PathJoints.Count;
                //    if (p.To != null)
                //    {
                //        j--;
                //    }
                //    if (ShowPathJoints)
                //    {
                //        for (i = 0; i < j; i++)
                //        {
                //            pj = p.PathJoints[i];
                //            if (SelectionPathJoint.Contains(pj))
                //            {
                //                pj.Render(g, _selectionPen, null, null);
                //            }
                //            else
                //            {
                //                pj.Render(g, pen, null, null);
                //            }
                //        }
                //    }
                //    if (p.To != null)
                //    {
                //        pj = p.PathJoints[j];
                //        if (SelectionPathJoint.Contains(pj))
                //        {
                //            p.RenderArrow(g, _selectionPen, _selectionPen.Brush, Font, p.PathJoints[j - 1].Location, pj.Location);
                //        }
                //        else
                //        {
                //            p.RenderArrow(g, pen, brush, Font, p.PathJoints[j - 1].Location, pj.Location);
                //        }
                //    }
                //}
                //else
                //{
                    p.Render(g, pen, brush, Font, ShowPathJoints, ShowPathArrows);
                //}
            }
        }

        protected virtual void RenderElements(Graphics g)
        {
            foreach (Box box in Framework.ZOrder)
            {
                if (box is Element)
                {
                    RenderElement(g, (Element)box);
                }
            }
        }

        protected virtual void RenderElement(Graphics g, Element elem)
        {
            Pen pen;
            if (SelectionRenderStyle == SelectionRenderStyle.Highlight)
            {
                pen = ChoosePenForElement(elem);
            }
            else
            {
                pen = Pens.Black;

                if (Selection.Contains(elem))
                {
                    RectangleV rect = elem.Rect;
                    rect.Inflate(SelectionOutlineMargin, SelectionOutlineMargin);
                    g.DrawRectangle(_selectionOutlinePen, rect.Left, rect.Top, rect.Width, rect.Height);
                }
            }

            //Matrix transform = g.Transform;
            //g.TranslateTransform(elem.X, elem.Y);
            //g.RotateTransform(elem.Rotation);
            elem.Render(g, pen, Brushes.Black, Font);
            //g.Transform = transform;
        }

        protected virtual void RenderBoxes(Graphics g)
        {
            foreach (Box box in Framework)
            {
                if (!(box is Element)
                    //&& !(box is Pathway)
                    //&& !(box is PathingJunction)
                    )
                {
                    RenderBox(g, box);
                }
            }
        }

        protected virtual void RenderBox(Graphics g, Box box)
        {
            box.Render(g, Pens.Black, Brushes.Black, Font);
        }

        //protected virtual Pen ChoosePenForPathway(Pathway p)
        //{
        //    Pen pen;
        //    if (SelectionMode == SelectionModeType.Pathway && SelectionPathway.Contains(p))
        //    {
        //        pen = _selectionPen;
        //    }
        //    else
        //    {
        //        pen = Pens.Orange;
        //    }
        //    return pen;
        //}

        //protected virtual Pen ChoosePenForPathingJunction(PathingJunction p)
        //{
        //    Pen pen;
        //    if (SelectionMode == SelectionModeType.PathingJunction && SelectionPathingJunction.Contains(p))
        //    {
        //        pen = _selectionPen;
        //    }
        //    else
        //    {
        //        pen = Pens.Orange;
        //    }
        //    return pen;
        //}

        protected virtual Pen ChoosePenForPath(Path path)
        {
            return Pens.Black;
        }

        protected virtual Pen ChoosePenForElement(Element element)
        {
            Pen pen;
            if (Selection.Contains(element))
            {
                pen = _selectionPen;
            }
            else
            {
                pen = Pens.Black;
            }
            return pen;
        }

        protected virtual void RenderDebugInfo(Graphics g)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("elements = ");
            sb.Append(_elements.Count);
            sb.Append("\r\n");
            sb.Append("paths = ");
            sb.Append(_paths.Count);
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
            if (SelectionElement.Length > 0)
            {
                Element sel = null;
                foreach (Element element in SelectionElement)
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
            sfd.Filter = "Bitmap Images (*.bmp)|*.bmp|JPEG Images (*.jpg, *.jpeg)|*.jpg;*.jpeg|GIF Images (*.gif)|*.gif|PNG Images (*.png)|*.png*.png|All Files (*.*)|*.*";

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

        Pen _selectionPen;
        Pen _selectionOutlinePen;
    }
}
