
/*****************************************************************************
 *                                                                           *
 *  BoxList.h                                                                *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that BoxFramework uses to expose a sorted, read-only list of     *
 *    boxes.                                                                 *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "IBox.h"
#include "IBoxList.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class BoxList// : IBoxList, ReadOnlyList<IBox^>???
{

public:

	BoxList(IList<IBox^>^);
	virtual ~BoxList(void);

	//add more search methods from List<IBox^>

	//IList
	virtual int IndexOf(IBox^);
	//ICollection
	virtual bool Contains(IBox^);
	virtual void CopyTo(array<IBox^>^, int);
	//IEnumerable
	virtual IEnumerator<IBox^>^ GetEnumerator(void);

	//IList
	virtual property IBox^ default[int]
	{
		IBox^ get(int);
	}
	//ICollection
	virtual property int Count
	{
		int get(void);
	}

protected:

	IList<IBox^>^	_list;

private:

};



} } 



