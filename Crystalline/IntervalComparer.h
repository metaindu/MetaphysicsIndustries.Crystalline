
/*****************************************************************************
 *                                                                           *
 *  IntervalComparer.h                                                       *
 *  20 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that sorts intervals.                                            *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Interval.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class IntervalComparer : IComparer<Interval>
{

public:

	IntervalComparer(bool SortByMin);
	virtual ~IntervalComparer(void);

	virtual int Compare(Interval x, Interval y);

protected:

	bool	_sortbymin;

private:

};



} } 


