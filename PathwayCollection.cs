
/*****************************************************************************
 *                                                                           *
 *  PathwayCollection.cs                                                     *
 *  29 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that holds a control's set of pathways, and coordinates them     *
 *    with the BoxFramework.                                                 *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class PathwayCollection : BoxCollection<Pathway>
	{
        private PathwayCollection() { }
		public PathwayCollection(BoxFramework f)
            : base(f)
		{
		}

	}
}
