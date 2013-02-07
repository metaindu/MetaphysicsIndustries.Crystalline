
/*****************************************************************************
 *                                                                           *
 *  BoxComparer.h                                                            *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that sorts boxes within a framework by position.                 *
 *                                                                           *
 *****************************************************************************/

#pragma once

#include "IBox.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class BoxComparer : IComparer<IBox^>
{

public:

	BoxComparer(bool LeftTopVsRightBottom, bool Vertical);
	virtual int Compare(IBox^, IBox^);

protected:

	bool	lefttopvsrightbottom;
	bool	vertical;

private:

};



} } 





