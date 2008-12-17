
/*****************************************************************************
 *                                                                           *
 *  BoxList.cs                                                               *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that BoxFramework uses to expose a sorted, read-only list of     *
 *    boxes.                                                                 *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class BoxList : IEnumerable<Box>
	{
		public BoxList(IList<Box> list)
		{
			_list = list;
		}

		public  void Dispose()
		{
            _list.Clear();
		}

		public virtual IEnumerator<Box> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		public virtual void CopyTo(Box[] r, int i)
		{
			_list.CopyTo(r, i);
		}

		public virtual int IndexOf(Box b)
		{
			return _list.IndexOf(b);
		}

		public virtual bool Contains(Box b)
		{
			return _list.Contains(b);
		}

		public virtual int Count
		{
			get
			{
				return _list.Count;
			}
		}

		public virtual Box this [ int index ]
		{
			get
			{
				return _list[index];
			}
		}

        private IList<Box> _list;

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
