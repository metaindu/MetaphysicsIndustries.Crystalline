
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
                    //Selection.Add(ee);
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

            Selection.Remove(elementToRemove);

            //Invalidate();
        }

        public virtual void RemovePath(Path pathToRemove)
        {
            InvalidateRectFromEntity(pathToRemove);

            Paths.Remove(pathToRemove);

            //foreach (PathJoint pj in pathToRemove.PathJoints)
            //{
            //    SelectionPathJoint.Remove(pj);
            //}

            //Invalidate();
        }



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

            ResetBoxNeighbors(elements.ToArray());
            ResetBoxNeighbors(Collection.Extract<Entity, Box>(others));
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
