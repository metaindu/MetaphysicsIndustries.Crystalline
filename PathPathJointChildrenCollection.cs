
/*****************************************************************************
 *                                                                           *
 *  PathPathJointChildrenCollection.cs                                       *
 *  1 December 2006                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  An unordered collection of PathJoint objects.                            *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class PathPathJointChildrenCollection : IList<PathJoint>
	{
        private PathPathJointChildrenCollection() { }
		public PathPathJointChildrenCollection(Path path)
		{
			_parent = path;
		}

		public  void Dispose()
		{
			Clear();

            //_list.Clear();
			//list.Dispose();
            //_list = null;
		}

		public virtual void Add(PathJoint pj)
		{
			if (!this.Contains(pj))
			{
				pj.ParentPath = null;
				_list.Add(pj);
				pj.ParentPath = _parent;
			}
		}

		public virtual void Clear()
		{
			List<PathJoint>	list;
			list = new List<PathJoint>();
			foreach (PathJoint pj in this)
			{
				list.Add(pj);
			}
			foreach (PathJoint pj in list)
			{
				this.Remove(pj);
			}
			list.Clear();
		}

		public virtual bool Contains(PathJoint pj)
		{
			return _list.Contains(pj);
		}

		public virtual void Insert(int i, PathJoint pj)
		{
			_list.Insert(i, pj);
		}

		public virtual IEnumerator<PathJoint> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		public virtual bool Remove(PathJoint pj)
		{
			bool	b;
			b = _list.Remove(pj);
			if (b && pj.ParentPath == _parent)
			{
				pj.ParentPath = null;
			}
			return b;
		}

		public virtual void CopyTo(PathJoint[] r, int i)
		{
			_list.CopyTo(r, i);
		}

		public virtual void RemoveAt(int i)
		{
			_list.RemoveAt(i);
		}

		public virtual int IndexOf(PathJoint pj)
		{
			return _list.IndexOf(pj);
		}

		public virtual PathJoint Last
		{
			get
			{
				return this[Count - 1];
			}
		}

		public virtual PathJoint this [ int index ]
		{
			get
			{
                return _list[index];
			}
			set
			{
                _list[index] = value;
			}
		}

		public virtual bool IsReadOnly
		{
			get
			{
				return _list.IsReadOnly;
			}
		}

		public virtual int Count
		{
			get
			{
				return _list.Count;
			}
		}

		public virtual PathJoint First
		{
			get
			{
				return this[0];
			}
		}

		  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
            return GetEnumerator();
		}

        private Path _parent;
        private IList<PathJoint> _list = new List<PathJoint>();
	}
}
