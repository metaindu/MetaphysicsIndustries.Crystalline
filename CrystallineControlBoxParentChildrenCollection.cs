/*****************************************************************************
 *                                                                           *
 *  CrystallineControlBoxParentChildrenCollection.cs                         *
 *  7 December 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  An unordered collection of Box objects.                                  *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    public class CrystallineControlBoxParentChildrenCollection : ICollection<Box>, IDisposable
    {
        public CrystallineControlBoxParentChildrenCollection(CrystallineControlEntityParentChildrenCollection parentInterlinking)
        {
            //if (container == null) { throw new ArgumentNullException("container"); }
            if (parentInterlinking == null) { throw new ArgumentNullException("parentInterlinking"); }

            //_container = container;
            _parentInterlinking = parentInterlinking;
            //parentInterlinking.ItemAdded += parentInterlinking_ItemAdded;
            //parentInterlinking.ItemRemoved += parentInterlinking_ItemRemoved;

            foreach (Box item in this)
            {
                _count++;
            }
        }

        //void parentInterlinking_ItemAdded(Entity item)
        //{
        //    if (item is Box)
        //    {
        //        Add((Box)item);
        //    }
        //}

        //void parentInterlinking_ItemRemoved(Entity item)
        //{
        //    if (item is Box)
        //    {
        //        Remove((Box)item);
        //    }
        //}

        public virtual void Dispose()
        {
            Clear();
        }

        public void AddRange(params Box[] items)
        {
            AddRange((IEnumerable<Box>)items);
        }
        public void AddRange(IEnumerable<Box> items)
        {
            foreach (Box item in items)
            {
                Add(item);
            }
        }
        public void RemoveRange(params Box[] items)
        {
            RemoveRange((IEnumerable<Box>)items);
        }
        public void RemoveRange(IEnumerable<Box> items)
        {
            foreach (Box item in items)
            {
                Remove(item);
            }
        }

        //ICollection<Box>
        public virtual void Add(Box item)
        {
            if (!Contains(item))
            {
                _count++;
            }

            _parentInterlinking.Add(item);
        }

        public virtual bool Contains(Box item)
        {
            return _parentInterlinking.Contains(item);
        }

        public virtual bool Remove(Box item)
        {
            if (Contains(item))
            {
                _count--;
            }

            return _parentInterlinking.Remove(item);
        }

        public virtual void Clear()
        {
            Box[] array = new Box[Count];

            CopyTo(array, 0);

            foreach (Box item in array)
            {
                Remove(item);
            }
        }

        public virtual void CopyTo(Box[] array, int arrayIndex)
        {
            List<Box> list = new List<Box>(Count);
            foreach (Box item in this)
            {
                list.Add(item);
            }
            list.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<Box> GetEnumerator()
        {
            foreach (object item in _parentInterlinking)
            {
                if (item is Box)
                {
                    yield return (Box)item;
                }
            }

            yield break;
        }

        //ICollection<Box>
        public virtual int Count
        {
            get { return _count; }
        }

        public virtual bool IsReadOnly
        {
            get { return _parentInterlinking.IsReadOnly; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //private CrystallineControl _container;
        //private Set<Box> _set = new Set<Box>();
        private CrystallineControlEntityParentChildrenCollection _parentInterlinking;
        int _count = 0;
    }
}
