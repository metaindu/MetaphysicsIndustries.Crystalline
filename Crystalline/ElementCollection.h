
/*****************************************************************************
 *                                                                           *
 *  ElementCollection.h                                                      *
 *  25 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A class that holds the control's set of elements, and coordinates them   *
 *    with the BoxFramework. This class and PathwayCollection and            *
 *    PathingJunctionCollection may inherit from a common base class at      *
 *    some later point in time.                                              *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Element.h"
#include "BoxFramework.h"
#include "BoxCollection.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class ElementCollection : BoxCollection<Element^>//ICollection<Element^>
{

public:

	ElementCollection(BoxFramework^ f);
//	virtual ~ElementCollection(void);
//
//	virtual void Add(Element^ e);
//	virtual bool Remove(Element^ e);
//	virtual bool Contains(Element^ e);
//	virtual void Clear(void);
//	virtual void CopyTo(array<Element^>^ array, int arrayIndex);
//
//	virtual IEnumerator<Element^>^ GetEnumerator(void);
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
//	virtual System::Collections::IEnumerator^ GetEnumerator2(void) = System::Collections::IEnumerable::GetEnumerator;
//
//private:
//
//	Set<Element^>^	elements;
//	BoxFramework^	framework;

};



} } 

