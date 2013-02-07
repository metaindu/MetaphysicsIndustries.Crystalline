
/*****************************************************************************
 *                                                                           *
 *  OutboundFromElementPathChildrenCollection.cpp                            *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A collection that manages the path connections outbound from an          *
 *    element.                                                               *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "OutboundFromElementPathChildrenCollection.h"



;
NAMESPACE_START
;



OutboundFromElementPathChildrenCollection::OutboundFromElementPathChildrenCollection(Element^ e) : ElementPathChildrenCollection(e)
{
	
}

OutboundFromElementPathChildrenCollection::~OutboundFromElementPathChildrenCollection(void)
{
	
}

Element^ OutboundFromElementPathChildrenCollection::GetChildsParent(Path^ p)
{
	return p->From;
}

void OutboundFromElementPathChildrenCollection::SetChildsParent(Path^ p, Element^ e)
{
	p->From = e;
}



;
NAMESPACE_END
;




