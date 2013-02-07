
/*****************************************************************************
 *                                                                           *
 *  BoxFramework.h                                                           *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that keeps track of boxes for fast and efficient searching.      *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "BoxComparer.h"
#include "BoxList.h"
#include "BoxOrientation.h"
#include "IntervalComparer.h"

using namespace System;
using namespace MetaphysicsIndustries::Collections;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class BoxFramework : ICollection<IBox^>
{

public:

	BoxFramework(void);
	virtual ~BoxFramework(void);

	virtual Set<IBox^>^ GetNeighborsAbove(float bottombound, float leftbound, float rightbound);
	virtual Set<IBox^>^ GetNeighborsBelow(float topbound, float leftbound, float rightbound);
	virtual Set<IBox^>^ GetNeighborsLeftward(float rightbound, float topbound, float bottombound);
	virtual Set<IBox^>^ GetNeighborsRightward(float leftbound, float topbound, float bottombound);

	virtual void Move(IBox^ boxToMove, PointF newLocation);

	//ICollection
	virtual void Add(IBox^);
	virtual bool Remove(IBox^);
	virtual bool Contains(IBox^);
	virtual void Clear(void);
	virtual void CopyTo(array<IBox^>^, int);

	//IEnumerable
	virtual IEnumerator<IBox^>^ GetEnumerator(void);

	//ICollection
	virtual property int Count
	{
		int get(void);
	}

	virtual property BoxList^ Left
	{
		BoxList^ get(void);
	}
	virtual property BoxList^ Right
	{
		BoxList^ get(void);
	}
	virtual property BoxList^ Up
	{
		BoxList^ get(void);
	}
	virtual property BoxList^ Down
	{
		BoxList^ get(void);
	}

protected:

	virtual Set<IBox^>^ GetNeighbors(BoxOrientation orientation, float mainbound, float crossmin, float crossmax);

	virtual System::Collections::IEnumerator^ GetEnumerator2(void) = System::Collections::IEnumerable::GetEnumerator;

	virtual property bool IsReadOnly
	{
		bool get(void) = ICollection<IBox^>::IsReadOnly::get;
	}

	virtual void Box_RectChanged(Object^, EventArgs^);

	Set<IBox^>^			_set;

	List<IBox^>^		_left;
	List<IBox^>^		_right;
	List<IBox^>^		_up;
	List<IBox^>^		_down;

	BoxComparer^		_sorterleft;
	BoxComparer^		_sorterright;
	BoxComparer^		_sorterup;
	BoxComparer^		_sorterdown;

	//readonly
	BoxList^			_roleft;
	BoxList^			_roright;
	BoxList^			_roup;
	BoxList^			_rodown;

	IntervalComparer^	_intervalsorter;

private:

};



} } 



