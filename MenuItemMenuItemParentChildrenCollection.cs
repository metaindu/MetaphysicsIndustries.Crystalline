/*****************************************************************************
 *                                                                           *
 *  MenuItemMenuItemParentChildrenCollection.cs                              *
 *  4 December 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  An unordered collection of MenuItem objects.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class MenuItemMenuItemParentChildrenCollection : ICollection<MenuItem>, IDisposable
    {
        public MenuItemMenuItemParentChildrenCollection(MenuItem container)
        {
            _container = container;
        }

        public virtual void Dispose()
        {
            Clear();
        }

        public void AddRange(params MenuItem[] items)
        {
            AddRange((IEnumerable<MenuItem>)items);
        }
        public void AddRange(IEnumerable<MenuItem> items)
        {
            foreach (MenuItem item in items)
            {
                Add(item);
            }
        }
        public void RemoveRange(params MenuItem[] items)
        {
            RemoveRange((IEnumerable<MenuItem>)items);
        }
        public void RemoveRange(IEnumerable<MenuItem> items)
        {
            foreach (MenuItem item in items)
            {
                Remove(item);
            }
        }

        //ICollection<MenuItem>
        public virtual void Add(MenuItem item)
        {
            if (!Contains(item))
            {
                _set.Add(item);
                item.ParentMenuItem = _container;
             }
        }

        public virtual bool Contains(MenuItem item)
        {
            return _set.Contains(item);
        }

        public virtual bool Remove(MenuItem item)
        {
            if (Contains(item))
            {
                bool ret = _set.Remove(item);
                item.ParentMenuItem = null;
                return ret;
            }

            return false;
        }

        public virtual void Clear()
        {
            MenuItem[] array = new MenuItem[Count];

            CopyTo(array, 0);

            foreach (MenuItem item in array)
            {
                Remove(item);
            }

            _set.Clear();
        }

        public virtual void CopyTo(MenuItem[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<MenuItem> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        //ICollection<MenuItem>
        public virtual int Count
        {
            get { return _set.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return (_set as ICollection<MenuItem>).IsReadOnly; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private MenuItem _container;
        private Set<MenuItem> _set = new Set<MenuItem>();
    }
}
