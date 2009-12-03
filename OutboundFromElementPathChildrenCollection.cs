
/*****************************************************************************
 *                                                                           *
 *  OutboundFromElementPathChildrenCollection.cs                             *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A collection that manages the path connections outbound from an          *
 *    element.                                                               *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    [Serializable]
    public class OutboundFromElementPathChildrenCollection : ElementPathChildrenCollection
	{
        private OutboundFromElementPathChildrenCollection() { }
		public OutboundFromElementPathChildrenCollection(Element parent)
            : base(parent)
		{
		}

        //public override void Dispose()
        //{
        //}

		protected override void SetParentElementOfPath(Path p, Element e)
		{
			p.From = e;
		}

		protected override Element GetParentElementOfPath(Path p)
		{
			return p.From;
		}

	}
}
