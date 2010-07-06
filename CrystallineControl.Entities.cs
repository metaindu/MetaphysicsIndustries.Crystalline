
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

        //private CrystallineControlBoxParentChildrenCollection _boxes;
        //public CrystallineControlBoxParentChildrenCollection Boxes
        //{
        //    get { return _boxes; }
        //}

        //public virtual ElementCollection Elements
        //{
        //    get
        //    {
        //        return _elements;
        //    }
        //}
        //public virtual CrystallineControlPathParentChildrenCollection Paths
        //{
        //    get
        //    {
        //        return _paths;
        //    }
        //}


        void Entities_ItemAdded(Entity item)
        {
            if (item is Box)
            {
                Framework.Add(item as Box);
            }

            InvalidateRectFromEntity(item);
        }

        void Entities_ItemRemoved(Entity item)
        {
            if (item is Box)
            {
                Framework.Remove(item as Box);
            }

            Selection.Remove(item);

            InvalidateRectFromEntity(item);
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
            Entities.Add(entity);

            if (entity is Element)
            {
                InternalAddElement(entity as Element);
            }

            InvalidateRectFromEntity(entity);
        }

        protected virtual void InternalAddElement(Element element)
        {
            Entities.Add(element);

            foreach (Path path in element.Inbound)
            {
                AddEntity(path);
            }
            foreach (Path path in element.Outbound)
            {
                AddEntity(path);
            }
        }

        public virtual void DisconnectAndRemoveEntity(Entity ent)
        {
            if (ent != null)
            {
                Entity[] entitiesToRemove;
                ent.Disconnect(out entitiesToRemove);

                Entities.Remove(ent);

                if (entitiesToRemove != null)
                {
                    foreach (Entity ent2 in entitiesToRemove)
                    {
                        if (ent2 != null && Entities.Contains(ent2))
                        {
                            DisconnectAndRemoveEntity(ent2);
                        }
                    }
                }
            }
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



        //ElementCollection _elements;
        //CrystallineControlPathParentChildrenCollection _paths;
        //PathingJunctionCollection _pathingJunctions;
        //PathwayCollection _pathways;


        public void ImportEntities(Entity[] entities)
        {
            Element[] elements = Collection.Extract<Entity, Element>(entities);

            Dictionary<Element, Path[]> outbound = new Dictionary<Element, Path[]>();
            Dictionary<Element, Path[]> inbound = new Dictionary<Element, Path[]>();

            foreach (Element elem in elements)
            {
                outbound[elem] = elem.Outbound.ToArray();
                inbound[elem] = elem.Inbound.ToArray();
            }

            Entities.AddRange(entities);

            foreach (Element elem in elements)
            {
                Collection.AddRange(elem.Outbound, outbound[elem]);
                Collection.AddRange(elem.Inbound, inbound[elem]);
            }
        }

        public virtual void ResetContent()
        {
            Entities.Clear();
        }

        //eventually replace these with IClonable on Entity
        protected Entity[] CloneEntities(IEnumerable<Entity> entities)
        {
            Set<Entity> ents = new Set<Entity>(entities);
            foreach (Entity ent in entities)
            {
                Entity[] ents2 = GetAdditionalClonableEntities(ent);
                ents.AddRange(ents2);
            }


            List<Entity> clones = new List<Entity>();
            Dictionary<Entity, Entity> matchup = new Dictionary<Entity, Entity>();

            foreach (Entity ent in ents)
            {
                Entity clone = InstantiateEntityClone(ent);
                if (clone != null)
                {
                    matchup[ent] = clone;
                    matchup[clone] = ent;
                    clones.Add(clone);
                }
            }
            foreach (Entity clone in clones)
            {
                PopulateEntityClone(clone, matchup);
            }

            return clones.ToArray();
        }
        protected Entity[] GetAdditionalClonableEntities(Entity ent)
        {
            Set<Entity> ents = new Set<Entity>();
            GetAdditionalClonableEntities(ent, ents);
            return ents.ToArray();
        }
        protected virtual void GetAdditionalClonableEntities(Entity ent, Set<Entity> ents)
        {
            if (ent is Element)
            {
                Element elem = (Element)ent;
                foreach (Path p in elem.Inbound)
                {
                    if (ents.Contains(p.From))
                    {
                        ents.Add(p);
                    }
                }
                foreach (Path p in elem.Outbound)
                {
                    if (ents.Contains(p.From))
                    {
                        ents.Add(p);
                    }
                }
            }
        }
        protected virtual Entity InstantiateEntityClone(Entity ent)
        {
            if (ent == null) { throw new ArgumentNullException("ent"); }

            //if (ent is Path)
            //{
            //    return new Path();
            //}
            //if (ent is Element)
            //{
            //    return new Element();
            //}

            throw new InvalidOperationException("Unknown entity type: " + ent.GetType().FullName);
        }
        protected virtual void PopulateEntityClone(Entity clone, Dictionary<Entity, Entity> matchup)
        {
            if (clone == null) { throw new ArgumentNullException("clone"); }
            if (matchup == null) { throw new ArgumentNullException("matchup"); }

            if (clone is Path)
            {
                Path src = (Path)matchup[clone];
                if (src.From != null &&
                    matchup.ContainsKey(src.From))
                {
                    ((Path)clone).From = (Element)matchup[src.From];
                }
                if (src.To != null &&
                    matchup.ContainsKey(src.To))
                {
                    ((Path)clone).To = (Element)matchup[src.To];
                }
            }
            if (clone is Box)
            {
                Box src = (Box)matchup[clone];
                ((Box)clone).Rect = src.Rect;
                ((Box)clone).Text = src.Text;
            }
            if (clone is Element)
            {
                Element src = (Element)matchup[clone];
                Element elem = (Element)clone;

                foreach (Path p in src.Inbound)
                {
                    if (matchup.ContainsKey(p))
                    {
                        elem.Inbound.Add((Path)matchup[p]);
                    }
                }
                foreach (Path p in src.Outbound)
                {
                    if (matchup.ContainsKey(p))
                    {
                        elem.Outbound.Add((Path)matchup[p]);
                    }
                }
            }
        }
    }
}
