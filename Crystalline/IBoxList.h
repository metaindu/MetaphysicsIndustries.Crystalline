
/*****************************************************************************
 *                                                                           *
 *  IBoxList.h                                                               *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  An interface that describes read-only access to a sorted list of         *
 *    boxes.                                                                 *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "IBox.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public interface class IBoxList : IList<IBox^>
{
	////IList
	//virtual int IndexOf(IBox^);
	////ICollection
	//virtual bool Contains(IBox^);
	//virtual void CopyTo(array<IBox^>^, int);
	////IEnumerable
	//virtual IEnumerator<IBox^>^ GetEnumerator(void);

	////IList
	//virtual property IBox^ default[int]
	//{
	//	IBox^ get(int);
	//}
	////ICollection
	//virtual property int Count
	//{
	//	int get(void);
	//}
};



} } 




