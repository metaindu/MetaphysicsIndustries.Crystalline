
/*****************************************************************************
 *                                                                           *
 *  InboundToElementPathChildrenCollection.cpp                               *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A collection that manages the path connections inbound to an element.    *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "InboundToElementPathChildrenCollection.h"



;
NAMESPACE_START
;



InboundToElementPathChildrenCollection::InboundToElementPathChildrenCollection(Element^ _parent) : ElementPathChildrenCollection(_parent)
{
	
}

InboundToElementPathChildrenCollection::~InboundToElementPathChildrenCollection(void)
{
	
}

Element^ InboundToElementPathChildrenCollection::GetChildsParent(Path^ p)
{
	return p->To;
}

void InboundToElementPathChildrenCollection::SetChildsParent(Path^ p, Element^ e)
{
	p->To = e;
}




;
NAMESPACE_END
;



