
/*****************************************************************************
 *                                                                           *
 *  BoxNeighborCollection.h                                                          *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A default implementation of a collection of boxes that automatically     *
 *    keeps changes to boxes' neighbors current.                             *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "IBox.h"
#include "BoxOrientation.h"

using namespace System;
using namespace MetaphysicsIndustries::Collections;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class BoxNeighborCollection : ICollection<IBox^>
{

public:

	BoxNeighborCollection(IBox^ __parent, BoxOrientation __orientation);
	virtual ~BoxNeighborCollection(void);

	virtual void Add(IBox^);
	virtual bool Remove(IBox^);
	virtual bool Contains(IBox^);
	virtual void Clear(void);
	virtual void CopyTo(array<IBox^>^, int);

	virtual IEnumerator<IBox^>^ GetEnumerator(void);

	virtual property int Count
	{
		int get(void);
	}
	virtual property bool IsReadOnly
	{
		bool get(void);
	}

	virtual property BoxOrientation Orientation
	{
		BoxOrientation get(void);
	}

protected:

	virtual System::Collections::IEnumerator^ GetEnumerator2(void) = System::Collections::IEnumerable::GetEnumerator;

	IBox^						_parent;
	BoxOrientation	_orientation;
	Set<IBox^>^					_set;

private:

};



} } 


