
/*****************************************************************************
 *                                                                           *
 *  Interval.h                                                               *
 *  20 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A value type that describes a closed interval on the numberline.         *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "IBox.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public value class Interval
{

public:

	Interval(float min, float max);
	//virtual ~Interval(void);

	static Interval Merge(Interval a, Interval b);

	bool Intersects(Interval i);

	float	Min;
	float	Max;

protected:

private:

};



} } 

