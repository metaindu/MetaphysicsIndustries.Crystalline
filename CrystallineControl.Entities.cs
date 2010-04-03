
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

        public Entity[] GetEntitiesAtPointInDocument(Vector v)
        {
            return GetEntitiesAtPointInDocument<Entity>(v);
        }

        public T[] GetEntitiesAtPointInDocument<T>(Vector v)
            where T : Entity
        {
            //eventually replace this with R-Tree implementation

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
            return GetEntitiesIntersectingRectInDocument<T>(rect, true);
        }

        public T[] GetEntitiesIntersectingRectInDocument<T>(RectangleV rect, bool inclusive)
            where T : Entity
        {
            //replace this with R-Tree implementation

            Set<T> set = new Set<T>();

            foreach (T ent in Collection.Extract<Entity, T>(Entities))
            {
                if (ent.GetBoundingBox().IntersectsWith(rect, inclusive))
                {
                    set.Add(ent);
                }
            }

            return set.ToArray();
        }

        //public virtual Element[] GetElementsAtPoint(Vector v)
        //{
        //    return GetEntitiesAtPointInDocument<Element>(v);
        //}
        //protected virtual Set<Element> GetElementsInRect(RectangleV r)
        //{
        //    RectangleV _r = r;

        //    Set<Element> outElements;

        //    outElements = new Set<Element>();
        //    foreach (Element ve in this.Elements)
        //    {
        //        if (ve.Rect.IntersectsWith(r))
        //        {
        //            outElements.Add(ve);
        //        }
        //    }

        //    return outElements;
        //}

        protected bool AlwaysTrue<T>(T t)
        {
            return true;
        }
        protected T GetFrontmostBoxAtPoint<T>(Vector pointInDocSpace)
            where T : Box
        {
            return GetFrontmostBoxAtPoint<T>(pointInDocSpace, AlwaysTrue<T>);
        }

        private T GetFrontmostBoxAtPoint<T>(Vector pointInDocSpace, Predicate<T> predicate)
            where T : Box
        {
            foreach (T item in Collection.Reverse(Collection.Filter<T>(Collection.Extract<Box, T>(Framework.ZOrder), predicate)))
            {
                if (item.Rect.Contains(pointInDocSpace))
                {
                    return item;
                }
            }
            return null;
        }


        private bool _boxCollisions = true;
        public bool BoxCollisions
        {
            get { return _boxCollisions; }
            set { _boxCollisions = value; }
        }

        public virtual void AddEntity(Entity entity)
        {
            InvalidateRectFromEntity(entity);

            Entities.Add(entity);

            if (entity is Box)
            {
                InternalAddBox(entity as Box);
            }
            else if (entity is Path)
            {
                InternalAddPath(entity as Path);
            }

            InvalidateRectFromEntity(entity);
        }

        protected virtual void InternalAddBox(Box box)
        {
            Boxes.Add(box);
            Framework.Add(box);

            if (box is Element)
            {
                InternalAddElement(box as Element);
            }
        }

        protected virtual void InternalAddElement(Element element)
        {
            Elements.Add(element);

            foreach (Path path in element.Inbound)
            {
                AddEntity(path);
            }
            foreach (Path path in element.Outbound)
            {
                AddEntity(path);
            }
        }

        protected virtual void InternalAddPath(Path pathToAdd)
        {
            Paths.Add(pathToAdd);
        }

        public virtual void RemoveEntity(Entity ent)
        {
            InvalidateRectFromEntity(ent);

            Entities.Remove(ent);

            Selection.Remove(ent);

            if (ent is Box)
            {
                InternalRemoveBox(ent as Box);
            }
            else if (ent is Path)
            {
                InternalRemovePath(ent as Path);
            }

            InvalidateRectFromEntity(ent);
        }

        protected virtual void InternalRemoveBox(Box box)
        {
            Boxes.RemoveRange(box);
            Framework.Remove(box);

            if (box is Element)
            {
                InternalRemoveElement(box as Element);
            }
        }

        protected virtual void InternalRemoveElement(Element element)
        {
            Elements.Remove(element);

            foreach (Path path in element.Inbound)
            {
                RoutePath(path);
            }
            foreach (Path path in element.Outbound)
            {
                RoutePath(path);
            }
        }

        protected virtual void InternalRemovePath(Path pathToRemove)
        {
            Paths.Remove(pathToRemove);

            //foreach (PathJoint pj in pathToRemove.PathJoints)
            //{
            //    SelectionPathJoint.Remove(pj);
            //}
        }



        //Set<Path> _ResetBoxNeighbors_inbound = new Set<Path>();
        //Set<Path> _ResetBoxNeighbors_outbound = new Set<Path>();
        //IEnumerable<Box> _ResetBoxNeighbors_lastBoxesToReset;
        //Set<Box> _ResetBoxNeighbors_boxen = new Set<Box>();

        //protected void ResetBoxNeighbors(IEnumerable<Box> boxesToReset)
        //{
        //    //This method is a kludge necessary because changing the location
        //    //of a box doesn't automatically update its neighbors collections.
        //    //This will no longer be necessary once the R-Tree is in place,
        //    //instead of the BoxFramework.

        //    _ResetBoxNeighbors_boxen.Clear();
        //    _ResetBoxNeighbors_boxen.AddRange(boxesToReset);

        //    foreach (Box box in _ResetBoxNeighbors_boxen)
        //    {
        //        Element elem = (box as Element);
        //        if (elem != null)
        //        {
        //            InvalidateRectFromEntities(elem.Inbound);
        //            InvalidateRectFromEntities(elem.Outbound);

        //            _ResetBoxNeighbors_inbound.Clear();
        //            _ResetBoxNeighbors_outbound.Clear();

        //            _ResetBoxNeighbors_inbound.AddRange(elem.Inbound);
        //            _ResetBoxNeighbors_outbound.AddRange(elem.Outbound);
        //        }

        //        InvalidateRectFromEntity(box);

        //        Framework.Remove(box);
        //        Framework.Add(box);

        //        if (elem != null)
        //        {
        //            Collection.AddRange<Path, Path>(elem.Inbound, _ResetBoxNeighbors_inbound);
        //            Collection.AddRange<Path, Path>(elem.Outbound, _ResetBoxNeighbors_outbound);

        //            InvalidateRectFromEntities(elem.Inbound);
        //            InvalidateRectFromEntities(elem.Outbound);
        //        }

        //        InvalidateRectFromEntity(box);
        //    }
        //}
        //protected void ResetAllBoxNeighbors()
        //{
        //    ResetBoxNeighbors(Boxes);
        //}

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


        public void ImportEntities(Entity[] entities)
        {
            Set<Element> elements = new Set<Element>();
            Set<Path> paths = new Set<Path>();
            Set<Entity> others = new Set<Entity>();

            //foreach (Entity ent in entities)
            //{
            //    if (ent is EmeraldElement)
            //    {
            //        elements.Add((EmeraldElement)ent);
            //    }
            //    else if (ent is EmeraldPath)
            //    {
            //        paths.Add((EmeraldPath)ent);
            //    }
            //}
            foreach (Entity ent in entities)
            {
                if (ent is Element)
                {
                    elements.Add((Element)ent);
                }
                else if (ent is Path)
                {
                    paths.Add((Path)ent);
                }
                else
                {
                    others.Add(ent);
                }
            }

            Dictionary<Element, Path[]> outbound = new Dictionary<Element, Path[]>();
            Dictionary<Element, Path[]> inbound = new Dictionary<Element, Path[]>();

            foreach (Element elem in elements)
            {
                outbound[elem] = Collection.ToArray(elem.Outbound);
                inbound[elem] = Collection.ToArray(elem.Inbound);
            }

            Elements.AddRange(elements);
            Paths.AddRange(paths);
            Entities.AddRange(others);

            foreach (Element elem in elements)
            {
                Collection.AddRange(elem.Outbound, outbound[elem]);
                Collection.AddRange(elem.Inbound, inbound[elem]);
            }
        }

        public virtual void ResetContent()
        {
            Selection.Clear();
            Elements.Clear();
            Boxes.Clear();
            Paths.Clear();
            Entities.Clear();
        }

    }
}
