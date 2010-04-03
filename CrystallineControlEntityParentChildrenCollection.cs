/*****************************************************************************
 *                                                                           *
 *  CrystallineControlEntityParentChildrenCollection.cs                      *
 *  7 December 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  An unordered collection of Entity objects.                               *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class CrystallineControlEntityParentChildrenCollection : ICollection<Entity>, IDisposable
    {
        public CrystallineControlEntityParentChildrenCollection(CrystallineControl container)
        {
            _container = container;
        }

        public virtual void Dispose()
        {
            Clear();
        }

        public void AddRange(params Entity[] items)
        {
            AddRange((IEnumerable<Entity>)items);
        }
        public void AddRange(IEnumerable<Entity> items)
        {
            foreach (Entity item in items)
            {
                Add(item);
            }
        }
        public void RemoveRange(params Entity[] items)
        {
            RemoveRange((IEnumerable<Entity>)items);
        }
        public void RemoveRange(IEnumerable<Entity> items)
        {
            foreach (Entity item in items)
            {
                Remove(item);
            }
        }

        public T[] Extract<T>()
            where T : Entity
        {
            return Collection.Extract<Entity, T>(this);
        }

        //ICollection<Entity>
        public virtual void Add(Entity item)
        {
            if (!Contains(item))
            {
                _set.Add(item);
                item.ParentCrystallineControl = _container;
                OnItemAdded(item);
             }
        }

        public virtual bool Contains(Entity item)
        {
            return _set.Contains(item);
        }

        public virtual bool Remove(Entity item)
        {
            if (Contains(item))
            {
                bool ret = _set.Remove(item);
                OnItemRemoved(item);
                item.ParentCrystallineControl = null;
                return ret;
            }

            return false;
        }

        public virtual void Clear()
        {
            Entity[] array = new Entity[Count];

            CopyTo(array, 0);

            foreach (Entity item in array)
            {
                Remove(item);
            }

            _set.Clear();
        }

        public virtual void CopyTo(Entity[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<Entity> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        //ICollection<Entity>
        public virtual int Count
        {
            get { return _set.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return (_set as ICollection<Entity>).IsReadOnly; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public delegate void InterlinkingEventHandler<T>(T item);

        public event InterlinkingEventHandler<Entity> ItemAdded;
        public event InterlinkingEventHandler<Entity> ItemRemoved;

        protected void OnItemAdded(Entity item)
        {
            if (ItemAdded != null)
            {
                ItemAdded(item);
            }
        }
        protected void OnItemRemoved(Entity item)
        {
            if (ItemRemoved != null)
            {
                ItemRemoved(item);
            }
        }

        private CrystallineControl _container;
        private Set<Entity> _set = new Set<Entity>();

        public Entity[] ToArray()
        {
            return Collection.ToArray(this);
        }
    }
}
