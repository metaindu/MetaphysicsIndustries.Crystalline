
/*****************************************************************************
 *                                                                           *
 *  BoxComparer.cs                                                           *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that sorts boxes within a framework by position.                 *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class BoxComparer : IComparer<Box>
	{
		public BoxComparer(bool _LeftTopVsRightBottom, bool _Vertical)
		{
			_leftTopVsRightBottom = _LeftTopVsRightBottom;
			_vertical = _Vertical;
		}

		public virtual int Compare(Box x, Box y)
		{
			if (_leftTopVsRightBottom)
			{
				if (_vertical)
				{
					
					return x.Location.Y.CompareTo(y.Location.Y);
				}
				else
				{
					
					return x.Location.X.CompareTo(y.Location.X);
				}
			}
			else
			{
				if (_vertical)
				{
					
					return x.Rect.Bottom.CompareTo(y.Rect.Bottom);
				}
				else
				{
					
					return x.Rect.Right.CompareTo(y.Rect.Right);
				}
			}
		}

        private bool _vertical;
        private bool _leftTopVsRightBottom;
	}
}
