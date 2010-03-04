
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
using MetaphysicsIndustries.Utilities;

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
        //public virtual PathingJunctionCollection PathingJunctions
        //{
        //    get
        //    {
        //        return _pathingJunctions;
        //    }
        //}
        //public virtual PathwayCollection Pathways
        //{
        //    get
        //    {
        //        return _pathways;
        //    }
        //}

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

        public Entity[] GetEntitiesAtPointInDocument(Vector v)
        {
            return GetEntitiesAtPointInDocument<Entity>(v);
        }

        public T[] GetEntitiesAtPointInDocument<T>(Vector v)
            where T : Entity
        {
            //replace this with R-Tree implementation

            Set<T> set = new Set<T>();

            foreach (T ent in Collection.Extract<Entity, T>(Entities))
            {
                if (ent.GetBoundingBox().Contains(v))
                {
                    set.Add(ent);
                }
            }

            return set.ToArray();
        }

        public T[] GetEntitiesIntersectingRectInDocument<T>(RectangleV rect)
            where T : Entity
        {
            //replace this with R-Tree implementation

            Set<T> set = new Set<T>();

            foreach (T ent in Collection.Extract<Entity, T>(Entities))
            {
                if (ent.GetBoundingBox().IntersectsWith(rect))
                {
                    set.Add(ent);
                }
            }

            return set.ToArray();
        }

        public virtual Set<Element> GetElementsAtPoint(Vector v)
        {
            return new Set<Element>(GetEntitiesAtPointInDocument<Element>(v));
        }
        protected virtual Set<Element> GetElementsInRect(RectangleV r)
        {
            RectangleV _r = r;

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
        protected virtual Set<PathJoint> GetPathJointsAtPoint(Vector pf)
        {
            Set<PathJoint> set;
            set = new Set<PathJoint>();

            RectangleV r = new RectangleV();
            float size;

            size = 3;

            //r.Location = pf;
            //r.X -= size;
            //r.Y -= size;
            //r.Width = 2 * size + 1;
            //r.Height = 2 * size + 1;
            r = new RectangleV(pf.X - size, pf.Y - size, 2 * size + 1, 2 * size + 1);

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
        protected virtual Set<PathJoint> GetPathJointsInRect(RectangleV r)
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
        //protected virtual Set<PathingJunction> GetPathingJunctionsAtPoint(Vector pf)
        //{
        //    Set<PathingJunction> set;

        //    set = new Set<PathingJunction>();

        //    foreach (PathingJunction p in this.PathingJunctions)
        //    {
        //        if (p.Rect.Contains(pf))
        //        {
        //            set.Add(p);
        //        }
        //    }

        //    return set;
        //}
        //protected virtual Set<PathingJunction> GetPathingJunctionsInRect(RectangleV r)
        //{
        //    RectangleV _r = r;

        //    Set<PathingJunction> outJunctions;

        //    outJunctions = new Set<PathingJunction>();
        //    foreach (PathingJunction p in this.PathingJunctions)
        //    {
        //        if (p.Rect.IntersectsWith(r))
        //        {
        //            outJunctions.Add(p);
        //        }
        //    }

        //    return outJunctions;
        //}
        //protected virtual Set<Pathway> GetPathwaysAtPoint(Vector pf)
        //{
        //    PointF _pf = pf;

        //    Set<Pathway> set;

        //    set = new Set<Pathway>();

        //    foreach (Pathway p in this.Pathways)
        //    {
        //        if (p.Rect.Contains(pf))
        //        {
        //            set.Add(p);
        //        }
        //    }

        //    return set;
        //}
        //protected virtual Set<Pathway> GetPathwaysInRect(RectangleV r)
        //{
        //    RectangleV _r = r;

        //    Set<Pathway> outPathways;

        //    outPathways = new Set<Pathway>();
        //    foreach (Pathway p in this.Pathways)
        //    {
        //        if (p.Rect.IntersectsWith(r))
        //        {
        //            outPathways.Add(p);
        //        }
        //    }

        //    return outPathways;
        //}

        protected virtual Element GetFrontmostElementAtPointInDocumentSpace(Vector pointInDocSpace)
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
            InvalidateRectFromEntity(elementToAdd);
        }

        public virtual void AddPath(Path pathToAdd)
        {
            Paths.Add(pathToAdd);
            //InvalidateRectFromBox(pathToAdd, 2);
            InvalidateRectFromEntity(pathToAdd);
        }

        //public virtual void AddPathway(Pathway pathwayToAdd)
        //{
        //    Pathways.Add(pathwayToAdd);
        //    InvalidateRectFromEntity(pathwayToAdd);
        //}

        //public virtual void AddPathingJunction(PathingJunction pathingJunctionToAdd)
        //{
        //    PathingJunctions.Add(pathingJunctionToAdd);
        //    InvalidateRectFromEntity(pathingJunctionToAdd);
        //}

        public virtual void RemoveElement(Element elementToRemove)
        {
            InvalidateRectFromEntity(elementToRemove);

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
            Boxes.Remove(elementToRemove);
            Entities.Remove(elementToRemove);
            Framework.Remove(elementToRemove);

            SelectionElement.Remove(elementToRemove);

            //Invalidate();
        }

        public virtual void RemovePath(Path pathToRemove)
        {
            InvalidateRectFromEntity(pathToRemove);

            Paths.Remove(pathToRemove);

            foreach (PathJoint pj in pathToRemove.PathJoints)
            {
                SelectionPathJoint.Remove(pj);
            }

            //Invalidate();
        }

        //public virtual void RemovePathway(Pathway pathwayToRemove)
        //{
        //    InvalidateRectFromEntity(pathwayToRemove);

        //    Pathways.Remove(pathwayToRemove);

        //    SelectionPathway.Remove(pathwayToRemove);
        //}

        //public virtual void RemovePathingJunction(PathingJunction pathingJunctionToRemove)
        //{
        //    InvalidateRectFromEntity(pathingJunctionToRemove);

        //    pathingJunctionToRemove.UpPathway = null;
        //    pathingJunctionToRemove.DownPathway = null;
        //    pathingJunctionToRemove.LeftPathway = null;
        //    pathingJunctionToRemove.RightPathway = null;

        //    PathingJunctions.Remove(pathingJunctionToRemove);

        //    SelectionPathingJunction.Remove(pathingJunctionToRemove);
        //}


        Set<Path> _ResetBoxNeighbors_inbound = new Set<Path>();
        Set<Path> _ResetBoxNeighbors_outbound = new Set<Path>();
        IEnumerable<Box> _ResetBoxNeighbors_lastBoxesToReset;
        Set<Box> _ResetBoxNeighbors_boxen = new Set<Box>();

        protected void ResetBoxNeighbors(IEnumerable<Box> boxesToReset)
        {
            //This method is a kludge necessary because changing the location
            //of a box doesn't automatically update its neighbors collections.
            //This will no longer be necessary once the R-Tree is in place,
            //instead of the BoxFramework.

            _ResetBoxNeighbors_boxen.Clear();
            _ResetBoxNeighbors_boxen.AddRange(boxesToReset);

            foreach (Box box in _ResetBoxNeighbors_boxen)
            {
                Element elem = (box as Element);
                if (elem != null)
                {
                    InvalidateRectFromEntities(elem.Inbound);
                    InvalidateRectFromEntities(elem.Outbound);

                    _ResetBoxNeighbors_inbound.Clear();
                    _ResetBoxNeighbors_outbound.Clear();

                    _ResetBoxNeighbors_inbound.AddRange(elem.Inbound);
                    _ResetBoxNeighbors_outbound.AddRange(elem.Outbound);
                }

                InvalidateRectFromEntity(box);

                Framework.Remove(box);
                box.UpNeighbors.Clear();
                box.DownNeighbors.Clear();
                box.LeftNeighbors.Clear();
                box.RightNeighbors.Clear();
                Framework.Add(box);

                if (elem != null)
                {
                    Collection.AddRange<Path, Path>(elem.Inbound, _ResetBoxNeighbors_inbound);
                    Collection.AddRange<Path, Path>(elem.Outbound, _ResetBoxNeighbors_outbound);

                    InvalidateRectFromEntities(elem.Inbound);
                    InvalidateRectFromEntities(elem.Outbound);
                }

                InvalidateRectFromEntity(box);
            }
        }
        protected void ResetAllBoxNeighbors()
        {
            ResetBoxNeighbors(Boxes);
        }

        protected static RectangleV GetBoundingBoxFromEntities(Entity[] entities)
        {
            return Entity.GetBoundingBoxFromEntities(entities);
        }

        protected static RectangleV GetBoundingBoxFromCenters(Entity[] entities)
        {
            return Entity.GetBoundingBoxFromCenters(entities);
        }



        ElementCollection _elements;
        CrystallineControlPathParentChildrenCollection _paths;
        //PathingJunctionCollection _pathingJunctions;
        //PathwayCollection _pathways;

    }
}
