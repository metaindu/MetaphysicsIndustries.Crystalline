
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Entities.cs                                           *
 *  19 March 2007/11 December 2008                                           *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that manages the entities within the         *
 *    control's purview.                                                     *
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
        private CrystallineControlEntityParentChildrenCollection _entities;
        public CrystallineControlEntityParentChildrenCollection Entities
        {
            get { return _entities; }
        }

        private CrystallineControlBoxParentChildrenCollection _boxes;
        public CrystallineControlBoxParentChildrenCollection Boxes
        {
            get { return _boxes; }
        }

        public virtual ElementCollection Elements
        {
            get
            {
                return _elements;
            }
        }
        public virtual CrystallineControlPathParentChildrenCollection Paths
        {
            get
            {
                return _paths;
            }
        }
        public virtual PathingJunctionCollection PathingJunctions
        {
            get
            {
                return _pathingJunctions;
            }
        }
        public virtual PathwayCollection Pathways
        {
            get
            {
                return _pathways;
            }
        }

        //protected virtual Element CreateElement()
        //{
        //    Element e = new Element();
        //    //e->Framework = this->Framework;
        //    return e;
        //}
        //protected virtual Path CreatePath()
        //{
        //    return new Path();
        //}
        //protected virtual PathingJunction CreatePathingJunction()
        //{
        //    return new PathingJunction();
        //}
        //protected virtual Pathway CreatePathway()
        //{
        //    return new Pathway();
        //}

        public virtual Set<Element> GetElementsAtPoint(PointF pf)
        {
            Set<Element> set;

            set = new Set<Element>();

            foreach (Element e in this.Elements)
            {
                if (e.Rect.Contains(pf))
                {
                    set.Add(e);
                }
            }

            return set;
        }
        protected virtual Set<Element> GetElementsInRect(RectangleF r)
        {
            RectangleF _r = r;

            Set<Element> outElements;

            outElements = new Set<Element>();
            foreach (Element ve in this.Elements)
            {
                if (ve.Rect.IntersectsWith(r))
                {
                    outElements.Add(ve);
                }
            }

            return outElements;
        }
        protected virtual Set<PathJoint> GetPathJointsAtPoint(PointF pf)
        {
            Set<PathJoint> set;
            set = new Set<PathJoint>();

            RectangleF r = new RectangleF();
            float size;

            size = 3;

            r.Location = pf;
            r.X -= size;
            r.Y -= size;
            r.Width = 2 * size + 1;
            r.Height = 2 * size + 1;

            //int	i;
            //int	j;
            //for each (Path^ p in this->Paths)
            //{
            //	j = p->Points->Count;
            //	for (i = 0; i < j; i++)
            //	{
            //		if (r.Contains(p->Points[i]))
            //		{
            //			set->Add(PathJoint(p, i));
            //		}
            //	}
            //}

            //return set;

            return this.GetPathJointsInRect(r);
        }
        protected virtual Set<PathJoint> GetPathJointsInRect(RectangleF r)
        {
            Set<PathJoint> set;
            set = new Set<PathJoint>();

            int i;
            int j;
            foreach (Path p in this.Paths)
            {
                j = p.PathJoints.Count;
                for (i = 0; i < j; i++)
                {
                    if (r.Contains(p.PathJoints[i].Location))
                    {
                        set.Add(p.PathJoints[i]);
                    }
                }
            }

            return set;
        }
        protected virtual Set<PathingJunction> GetPathingJunctionsAtPoint(PointF pf)
        {
            Set<PathingJunction> set;

            set = new Set<PathingJunction>();

            foreach (PathingJunction p in this.PathingJunctions)
            {
                if (p.Rect.Contains(pf))
                {
                    set.Add(p);
                }
            }

            return set;
        }
        protected virtual Set<PathingJunction> GetPathingJunctionsInRect(RectangleF r)
        {
            RectangleF _r = r;

            Set<PathingJunction> outJunctions;

            outJunctions = new Set<PathingJunction>();
            foreach (PathingJunction p in this.PathingJunctions)
            {
                if (p.Rect.IntersectsWith(r))
                {
                    outJunctions.Add(p);
                }
            }

            return outJunctions;
        }
        protected virtual Set<Pathway> GetPathwaysAtPoint(PointF pf)
        {
            PointF _pf = pf;

            Set<Pathway> set;

            set = new Set<Pathway>();

            foreach (Pathway p in this.Pathways)
            {
                if (p.Rect.Contains(pf))
                {
                    set.Add(p);
                }
            }

            return set;
        }
        protected virtual Set<Pathway> GetPathwaysInRect(RectangleF r)
        {
            RectangleF _r = r;

            Set<Pathway> outPathways;

            outPathways = new Set<Pathway>();
            foreach (Pathway p in this.Pathways)
            {
                if (p.Rect.IntersectsWith(r))
                {
                    outPathways.Add(p);
                }
            }

            return outPathways;
        }

        protected virtual Element GetFrontmostElementAtPointInDocumentSpace(PointF pointInDocSpace)
        {
            Element frontmost = null;

            Set<Element> s1 = GetElementsAtPoint(pointInDocSpace);
            if (s1.Count > 0)
            {
                frontmost = s1.GetFirst();
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
            }
            return frontmost;
        }


        private bool _elementCollisions = true;
        public bool ElementCollisions
        {
            get { return _elementCollisions; }
            set { _elementCollisions = value; }
        }


        public virtual void AddElement(Element elementToAdd)
        {
            Elements.Add(elementToAdd);
            InvalidateRectFromBox(elementToAdd);
        }

        public virtual void AddPath(Path pathToAdd)
        {
            Paths.Add(pathToAdd);
            //InvalidateRectFromBox(pathToAdd, 2);
            InvalidateRectFromPath(pathToAdd);
        }

        public virtual void AddPathway(Pathway pathwayToAdd)
        {
            Pathways.Add(pathwayToAdd);
            InvalidateRectFromBox(pathwayToAdd);
        }

        public virtual void AddPathingJunction(PathingJunction pathingJunctionToAdd)
        {
            PathingJunctions.Add(pathingJunctionToAdd);
            InvalidateRectFromBox(pathingJunctionToAdd);
        }

        public virtual void RemoveElement(Element elementToRemove)
        {
            InvalidateRectFromBox(elementToRemove);

            Set<Path> paths = new Set<Path>();
            paths.AddRange(elementToRemove.Inbound);
            paths.AddRange(elementToRemove.Outbound);

            elementToRemove.Inbound.Clear();
            elementToRemove.Outbound.Clear();

            foreach (Path path in paths)
            {
                RoutePath(path);
            }

            Elements.Remove(elementToRemove);

            SelectionElement.Remove(elementToRemove);

            //Invalidate();
        }

        public virtual void RemovePath(Path pathToRemove)
        {
            InvalidateRectFromPath(pathToRemove);

            Paths.Remove(pathToRemove);

            foreach (PathJoint pj in pathToRemove.PathJoints)
            {
                SelectionPathJoint.Remove(pj);
            }

            //Invalidate();
        }

        public virtual void RemovePathway(Pathway pathwayToRemove)
        {
            InvalidateRectFromBox(pathwayToRemove);

            Pathways.Remove(pathwayToRemove);

            SelectionPathway.Remove(pathwayToRemove);
        }

        public virtual void RemovePathingJunction(PathingJunction pathingJunctionToRemove)
        {
            InvalidateRectFromBox(pathingJunctionToRemove);

            pathingJunctionToRemove.UpPathway = null;
            pathingJunctionToRemove.DownPathway = null;
            pathingJunctionToRemove.LeftPathway = null;
            pathingJunctionToRemove.RightPathway = null;

            PathingJunctions.Remove(pathingJunctionToRemove);

            SelectionPathingJunction.Remove(pathingJunctionToRemove);
        }

        ElementCollection _elements;
        CrystallineControlPathParentChildrenCollection _paths;
        PathingJunctionCollection _pathingJunctions;
        PathwayCollection _pathways;

    }
}
