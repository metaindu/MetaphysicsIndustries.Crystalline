
/*****************************************************************************
 *                                                                           *
 *  BoxNeighborCollection.cs                                                 *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A default implementation of a collection of boxes that automatically     *
 *    keeps changes to boxes' neighbors current.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    [Serializable]
	public class BoxNeighborCollection : ICollection<Box>
	{
        private BoxNeighborCollection() { }
		public BoxNeighborCollection(Box parent, BoxOrientation orientation)
		{
			_parent = parent;
			_orientation = orientation;
			_set = new Set<Box>();
		}

		public virtual void Dispose()
		{
			this.Clear();
		}

		public virtual void Add(Box b)
		{
			if (b == null) { throw new ArgumentNullException("b"); }
			
			
			
			
			if (b != _parent && !this.Contains(b))
			{
				if (Orientation == BoxOrientation.Left ||
					Orientation == BoxOrientation.Right ||
					Orientation == BoxOrientation.Up ||
					Orientation == BoxOrientation.Down)
				{
					_set.Add(b);
				}
				else
				{
					throw new InvalidOperationException("unknown BoxOrientation");
				}
				if (Orientation == BoxOrientation.Left)
				{
					b.RightNeighbors.Add(_parent);
				}
				else if (Orientation == BoxOrientation.Right)
				{
					b.LeftNeighbors.Add(_parent);
				}
				else if (Orientation == BoxOrientation.Up)
				{
					b.DownNeighbors.Add(_parent);
				}
				else if (Orientation == BoxOrientation.Down)
				{
					b.UpNeighbors.Add(_parent);
				}
			}
		}

        public virtual void AddRange(IEnumerable<Box> items)
        {
            foreach (Box box in items)
            {
                Add(box);
            }
        }

		public virtual void Clear()
		{
			Box[]	r;
			r = new Box[Count];
			this.CopyTo(r, 0);
			foreach (Box b in r)
			{
				this.Remove(b);
			}
			_set.Clear();
		}

		public virtual bool Contains(Box b)
		{
			return _set.Contains(b);
		}

		public virtual bool Remove(Box b)
		{
			if (b == null) { throw new ArgumentNullException("b"); }
			if (this.Contains(b))
			{
				if (Orientation == BoxOrientation.Left ||
					Orientation == BoxOrientation.Right ||
					Orientation == BoxOrientation.Up ||
					Orientation == BoxOrientation.Down)
				{
					if (!_set.Remove(b)) { return false; }
				}
				else
				{
					throw new InvalidOperationException("unknown BoxOrientation");
				}
				if (Orientation == BoxOrientation.Left)
				{
					b.RightNeighbors.Remove(_parent);
				}
				else if (Orientation == BoxOrientation.Right)
				{
					b.LeftNeighbors.Remove(_parent);
				}
				else if (Orientation == BoxOrientation.Up)
				{
					b.DownNeighbors.Remove(_parent);
				}
				else if (Orientation == BoxOrientation.Down)
				{
					b.UpNeighbors.Remove(_parent);
				}
				return true;
			}
			return false;
		}

		public virtual void CopyTo(Box[] r, int i)
		{
			_set.CopyTo(r, i);
		}

		public virtual IEnumerator<Box> GetEnumerator()
		{
			return _set.GetEnumerator();
		}

		public virtual BoxOrientation Orientation
		{
			get
			{
				return _orientation;
			}
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

		  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
            return GetEnumerator();
		}

        private Box _parent;
        private BoxOrientation _orientation;
        private Set<Box> _set;
	}
}
