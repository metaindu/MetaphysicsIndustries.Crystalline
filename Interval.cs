
/*****************************************************************************
 *                                                                           *
 *  Interval.cs                                                              *
 *  20 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A value type that describes a closed interval on the numberline.         *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    public struct Interval
    {
        public Interval(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public static Interval Merge(Interval a, Interval b)
        {
            return new Interval(
                Math.Min(a.Min, b.Min),
                Math.Max(a.Max, b.Max));
        }

        public bool Intersects(Interval i)
        {
            return (this.Min <= i.Max && this.Max >= i.Min);
        }

        public float Min;
        public float Max;
    }
}
