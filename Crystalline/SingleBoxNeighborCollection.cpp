
/*****************************************************************************
 *                                                                           *
 *  SingleBoxNeighborCollection.cpp                                                  *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A sub-class of BoxNeighborCollection that only holds a single box at a time.     *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "SingleBoxNeighborCollection.h"



;
NAMESPACE_START
;



SingleBoxNeighborCollection::SingleBoxNeighborCollection(IBox^ __parent, BoxOrientation __orientation) : BoxNeighborCollection(__parent, __orientation)
{
	
}

SingleBoxNeighborCollection::~SingleBoxNeighborCollection(void)
{
	
}

IBox^ SingleBoxNeighborCollection::Current::get(void)
{
	return this->_current;
}
void SingleBoxNeighborCollection::Current::set(IBox^ ib)
{
	if (this->Current != ib)
	{
		this->OnCurrentChanging(gcnew EventArgs);

		if (this->Current) { this->BoxNeighborCollection::Remove(this->Current); }
		this->_current = ib;
		if (this->Current) { this->BoxNeighborCollection::Add(this->Current); }

		this->OnCurrentChanged(gcnew EventArgs);
	}
}


void SingleBoxNeighborCollection::OnCurrentChanging(EventArgs^ e)
{
	this->CurrentChanging(this, e);
}
void SingleBoxNeighborCollection::OnCurrentChanged(EventArgs^ e)
{
	this->CurrentChanged(this, e);
}

void SingleBoxNeighborCollection::Add(IBox^ ib)
{
	if (!ib) { throw gcnew ArgumentNullException(__WCODESIG__); }

	if (this->Current != ib)
	{
		this->Current = ib;
	}
}

bool SingleBoxNeighborCollection::Remove(IBox^ ib)
{
	if (!ib) { throw gcnew ArgumentNullException(__WCODESIG__); }

	if (this->Current != ib) { return false; }

	this->Current = nullptr;

	return true;
}





;
NAMESPACE_END
;


