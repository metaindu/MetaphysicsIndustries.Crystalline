/*****************************************************************************
 *                                                                           *
 *  CrystallineEngineFunctionalityOrderedParentChildrenCollection.cs         *
 *  24 December 2008                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  An ordered collection of Functionality objects.                          *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class CrystallineEngineFunctionalityOrderedParentChildrenCollection : IList<Functionality>, IDisposable
    {
        public CrystallineEngineFunctionalityOrderedParentChildrenCollection(CrystallineEngine container)
        {
            _container = container;
        }

        public virtual void Dispose()
        {
            Clear();
        }

        public void AddRange(params Functionality[] items)
        {
            AddRange((IEnumerable<Functionality>)items);
        }
        public void AddRange(IEnumerable<Functionality> items)
        {
            foreach (Functionality item in items)
            {
                Add(item);
            }
        }
        public void RemoveRange(params Functionality[] items)
        {
            RemoveRange((IEnumerable<Functionality>)items);
        }
        public void RemoveRange(IEnumerable<Functionality> items)
        {
            foreach (Functionality item in items)
            {
                Remove(item);
            }
        }

        //ICollection<Functionality>
        public virtual void Add(Functionality item)
        {
            if (!Contains(item))
            {
                _list.Add(item);
                item.ParentCrystallineEngine = _container;
             }
        }

        public virtual bool Contains(Functionality item)
        {
            return _list.Contains(item);
        }

        public virtual bool Remove(Functionality item)
        {
            if (Contains(item))
            {
                bool ret = _list.Remove(item);
                item.ParentCrystallineEngine = null;
                return ret;
            }

            return false;
        }

        public virtual void Clear()
        {
            Functionality[] array = new Functionality[Count];

            CopyTo(array, 0);

            foreach (Functionality item in array)
            {
                Remove(item);
            }

            _list.Clear();
        }

        public virtual void CopyTo(Functionality[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<Functionality> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        //IList<Functionality>
        public virtual int IndexOf(Functionality item)
        {
            return _list.IndexOf(item);
        }

        public virtual void Insert(int index, Functionality item)
        {
            if (Contains(item))
            {
                if (IndexOf(item) < index)
                {
                    index--;
                }

                Remove(item);
            }

            item.ParentCrystallineEngine = null;
            _list.Insert(index, item);
            item.ParentCrystallineEngine = _container;
        }

        public virtual void RemoveAt(int index)
        {
            Remove(this[index]);
        }

        //ICollection<Functionality>
        public virtual int Count
        {
            get { return _list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return (_list as ICollection<Functionality>).IsReadOnly; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //IList<Functionality>
        public virtual Functionality this [ int index ]
        {
            get { return _list[index]; }
            set
            {
                RemoveAt(index);
                Insert(index, value);
            }
        }

        private CrystallineEngine _container;
        private List<Functionality> _list = new List<Functionality>();
    }
}
