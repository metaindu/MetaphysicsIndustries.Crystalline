
/*****************************************************************************
 *                                                                           *
 *  IntervalComparer.cpp                                                     *
 *  20 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that sorts intervals.                                            *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "IntervalComparer.h"



namespace MetaphysicsIndustries { namespace Crystalline { 
;



IntervalComparer::IntervalComparer(bool SortByMin)
{
	this->_sortbymin = SortByMin;
}

IntervalComparer::~IntervalComparer(void)
{
	
}

int IntervalComparer::Compare(Interval x, Interval y)
{
	if (this->_sortbymin)
	{
		return x.Min.CompareTo(y.Min);
	}
	else
	{
		return x.Max.CompareTo(y.Max);
	}
}




} } 



