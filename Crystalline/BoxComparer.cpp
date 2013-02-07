
/*****************************************************************************
 *                                                                           *
 *  BoxComparer.cpp                                                          *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that sorts boxes within a framework by position.                 *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "BoxComparer.h"



namespace MetaphysicsIndustries { namespace Crystalline { 
;



BoxComparer::BoxComparer(bool _LeftTopVsRightBottom, bool _Vertical)
{
	this->lefttopvsrightbottom = _LeftTopVsRightBottom;
	this->vertical = _Vertical;
}

int BoxComparer::Compare(IBox^ x, IBox^ y)
{

	if (this->lefttopvsrightbottom)
	{
		if (this->vertical)
		{
			//top
			return x->Location.Y.CompareTo(y->Location.Y);
		}
		else
		{
			//left
			return x->Location.X.CompareTo(y->Location.X);
		}
	}
	else
	{
		if (this->vertical)
		{
			//bottom
			return x->Rect.Bottom.CompareTo(y->Rect.Bottom);
		}
		else
		{
			//right
			return x->Rect.Right.CompareTo(y->Rect.Right);
		}
	}

	return 0;
}



} } 




