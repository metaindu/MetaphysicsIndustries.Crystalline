
/*****************************************************************************
 *                                                                           *
 *  PathwayCollection.h                                                      *
 *  29 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A class that holds a control's set of pathways, and coordinates them     *
 *    with the BoxFramework.                                                 *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Pathway.h"
#include "BoxCollection.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class PathwayCollection : BoxCollection<Pathway^>
{

public:

	PathwayCollection(BoxFramework^);
	//virtual ~PathwayCollection(void);

protected:

private:

};



} } 




