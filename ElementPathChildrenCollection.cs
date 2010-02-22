
/*****************************************************************************
 *                                                                           *
 *  ElementPathChildrenCollection.cs                                         *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  An unordered collection of Path objects.                                 *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    [Serializable]
    public abstract class ElementPathChildrenCollection : ICollection<Path>//, IDisposable
	{
        protected ElementPathChildrenCollection() { }

		public ElementPathChildrenCollection(Element element)
		{
			_parent = element;
			_set = new Set<Path>();
		}

        //public virtual void Dispose()
        //{
        //    this.Clear();
        //    //_set.Dispose();
        //}

		public virtual void Add(Path path)
		{
			if (!this.Contains(path))
			{
				
				this.SetParentElementOfPath(path, null);
				_set.Add(path);
				
				this.SetParentElementOfPath(path, _parent);
			}
		}

		public virtual void Clear()
		{
			List<Path>	list;
			list = new List<Path>();
			foreach (Path path in this)
			{
				list.Add(path);
			}
			foreach (Path path in list)
			{
				this.Remove(path);
			}
			list.Clear();
			_set.Clear();
		}

		public virtual bool Contains(Path path)
		{
			return _set.Contains(path);
		}

		public virtual bool Remove(Path path)
		{
			bool	b;
			b = _set.Remove(path);
			if (b && this.GetParentElementOfPath(path) == _parent)
			{
				
				this.SetParentElementOfPath(path, null);
			}
			return b;
		}

		public virtual void CopyTo(Path[] r, int i)
		{
			_set.CopyTo(r, i);
		}

		public virtual IEnumerator<Path> GetEnumerator()
		{
			return _set.GetEnumerator();
		}

		public virtual bool IsReadOnly
		{
			get
			{
				return _set.IsReadOnly;
			}
		}

		public virtual int Count
		{
			get
			{
				return _set.Count;
			}
		}

        protected abstract void SetParentElementOfPath(Path path, Element element);

		protected abstract Element GetParentElementOfPath(Path path);

        private Element _parent;
        private Set<Path> _set;

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
