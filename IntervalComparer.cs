
/*****************************************************************************
 *                                                                           *
 *  IntervalComparer.cs                                                      *
 *  20 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that sorts intervals.                                            *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class IntervalComparer : IComparer<Interval>
	{
		public IntervalComparer(bool SortByMin)
		{
			_sortByMin = SortByMin;
		}

		public  void Dispose()
		{
			
		}

		public virtual int Compare(Interval x, Interval y)
		{
			if (_sortByMin)
			{
				return x.Min.CompareTo(y.Min);
			}
			else
			{
				return x.Max.CompareTo(y.Max);
			}
		}

        private bool _sortByMin;
	}
}
