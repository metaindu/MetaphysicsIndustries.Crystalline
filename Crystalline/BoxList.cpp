
/*****************************************************************************
 *                                                                           *
 *  BoxList.cpp                                                              *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that BoxFramework uses to expose a sorted, read-only list of     *
 *    boxes.                                                                 *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "BoxList.h"



namespace MetaphysicsIndustries { namespace Crystalline { 
;



BoxList::BoxList(IList<IBox^>^ __list)
{
	this->_list = __list;
}

BoxList::~BoxList(void)
{
	this->_list = nullptr;
}

int BoxList::IndexOf(IBox^ b)
{
	return this->_list->IndexOf(b);
}

bool BoxList::Contains(IBox^ b)
{
	return this->_list->Contains(b);
}

void BoxList::CopyTo(array<IBox^>^ r, int i)
{
	return this->_list->CopyTo(r, i);
}

IEnumerator<IBox^>^ BoxList::GetEnumerator(void)
{
	return this->_list->GetEnumerator();
}

IBox^ BoxList::default::get(int i)
{
	return this->_list[i];
}

int BoxList::Count::get(void)
{
	return this->_list->Count;
}



} } 



