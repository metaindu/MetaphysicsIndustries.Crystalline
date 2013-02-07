
/*****************************************************************************
 *                                                                           *
 *  Interval.cpp                                                             *
 *  20 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A value type that describes a closed interval on the numberline.         *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "Interval.h"



;
NAMESPACE_START
;



Interval::Interval(float min, float max)
{
	Min = min;
	Max = max;
}

//Interval::~Interval(void)
//{
//	
//}

Interval Interval::Merge(Interval a, Interval b)
{
	return Interval(Math::Min(a.Min, b.Min), Math::Max(a.Max, b.Max));
}

bool Interval::Intersects(Interval i)
{
	return (this->Min <= i.Max && this->Max >= i.Min);
}




;
NAMESPACE_END
;


