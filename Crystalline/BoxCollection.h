
/*****************************************************************************
 *                                                                           *
 *  BoxCollection.h                                                          *
 *  28 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A class that holds a set of boxes, and coordinates them with the         *
 *    BoxFramework. This is the common base class for ElementCollection,     *
 *    PathwayCollection and PathingJunctionCollection.                       *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Element.h"
#include "IBox.h"
#include "BoxFramework.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



generic<typename T>
where T : IBox
public ref class BoxCollection : ICollection<T>
{

public:

	BoxCollection(BoxFramework^);
	virtual ~BoxCollection(void);

	virtual void Add(T boxToAdd);
	virtual bool Remove(T boxToRemove);
	virtual bool Contains(T boxToTest);
	virtual void Clear(void);
	virtual void CopyTo(array<T>^ array, int arrayIndex);
	virtual IEnumerator<T>^ GetEnumerator(void);

	virtual property int Count
	{
		int get(void);
	}
	virtual property bool IsReadOnly
	{
		bool get(void);
	}

protected:

	virtual System::Collections::IEnumerator^ GetEnumerator2(void) = System::Collections::IEnumerable::GetEnumerator;

	ICollection<T>^	collection;
	BoxFramework^	framework;

private:



};




} } 
