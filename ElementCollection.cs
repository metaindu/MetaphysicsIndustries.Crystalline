
/*****************************************************************************
 *                                                                           *
 *  ElementCollection.cs                                                     *
 *  25 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that holds the control's set of elements, and coordinates them   *
 *    with the BoxFramework. This class and PathwayCollection and            *
 *    PathingJunctionCollection may inherit from a common base class at      *
 *    some later point in time.                                              *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
	public class ElementCollection : BoxCollection<Element>
	{
        public ElementCollection(BoxFramework f)
            : base(f)
        {
        }

        //	virtual ~ElementCollection(void);
        //
        //	virtual void Add(Element item);
        //	virtual bool Remove(Element item);
        //	virtual bool Contains(Element item);
        //	virtual void Clear(void);
        //	virtual void CopyTo(Element[] array, int arrayIndex);
        //
        //	virtual IEnumerator<Element> GetEnumerator(void);
        //
        //	virtual property int Count
        //	{
        //		int get(void);
        //	}
        //
        //	virtual property bool IsReadOnly
        //	{
        //		bool get(void);
        //	}
        //
        //protected:
        //
        //	virtual System::Collections::IEnumerator GetEnumerator2(void) = System::Collections::IEnumerable::GetEnumerator;
        //
        //private:
        //
        //	Set<Element>	elements;
        //	BoxFramework	framework;

	}
}
