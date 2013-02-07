
/*****************************************************************************
 *                                                                           *
 *  PathingJunctionCollection.h                                              *
 *  31 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A class that holds a control's set of PathingJunctions, and              *
 *    coordinates them with the BoxFramework.                                *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "PathingJunction.h"
#include "BoxCollection.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class PathingJunctionCollection : BoxCollection<PathingJunction^>
{

public:

	PathingJunctionCollection(BoxFramework^ f);
	//virtual ~PathingJunctionCollection(void);

protected:

private:

};



} } 


