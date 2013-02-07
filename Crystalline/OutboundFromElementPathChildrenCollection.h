
/*****************************************************************************
 *                                                                           *
 *  OutboundFromElementPathChildrenCollection.h                              *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A collection that manages the path connections outbound from an          *
 *    element.                                                               *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "ElementPathChildrenCollection.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class OutboundFromElementPathChildrenCollection : ElementPathChildrenCollection
{

public:

	OutboundFromElementPathChildrenCollection(Element^);
	virtual ~OutboundFromElementPathChildrenCollection(void);

protected:

	virtual Element^ GetChildsParent(Path^) override;
	virtual void SetChildsParent(Path^, Element^) override;

private:

};



} } 


