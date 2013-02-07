/*****************************************************************************
 *                                                                           *
 *  ElementPathChildrenCollection.h                                          *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  An unordered collection of Path objects.                                 *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Element.h"
#include "Path.h"

using namespace MetaphysicsIndustries::Collections;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



ref class Element;
ref class Path;
public ref class ElementPathChildrenCollection abstract : ICollection<Path^>
{

public:

	ElementPathChildrenCollection(Element^);
	virtual ~ElementPathChildrenCollection(void);

	virtual void Add(Path^);
	virtual bool Contains(Path^);
	virtual bool Remove(Path^);
	virtual void Clear(void);
	virtual void CopyTo(array<Path^>^, int);
	virtual IEnumerator<Path^>^ GetEnumerator(void);

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
	virtual Element^ GetChildsParent(Path^) abstract;
	virtual void SetChildsParent(Path^, Element^) abstract;

	Element^	parent;
	Set<Path^>^	set;

};



} } 



