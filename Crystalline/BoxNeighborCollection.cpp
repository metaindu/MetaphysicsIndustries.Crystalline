
/*****************************************************************************
 *                                                                           *
 *  BoxNeighborCollection.cpp                                                        *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A default implementation of a collection of boxes that automatically     *
 *    keeps changes to boxes' neighbors current.                             *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "BoxNeighborCollection.h"



namespace MetaphysicsIndustries { namespace Crystalline { 
;



BoxNeighborCollection::BoxNeighborCollection(IBox^ __parent, BoxOrientation __orientation)
{
	this->_parent = __parent;
	this->_orientation = __orientation;
	this->_set = gcnew Set<IBox^>();
}

BoxNeighborCollection::~BoxNeighborCollection(void)
{
	this->Clear();

	this->_set->Clear();
	delete this->_set;
	this->_set = nullptr;
}

void BoxNeighborCollection::Add(IBox^ b)
{
	if (!b) { throw gcnew ArgumentNullException(__WCODESIG__); }

	//if (b == this->_parent)
	//{
	//	Debug::Fail("Adding a box to its own list of neighbors." + __WCODESIG__);
	//}

	if (b != this->_parent && !this->Contains(b))
	{
		if (this->Orientation == BoxOrientation::Left || 
			this->Orientation == BoxOrientation::Right || 
			this->Orientation == BoxOrientation::Up || 
			this->Orientation == BoxOrientation::Down)
		{
			this->_set->Add(b);
		}
		else
		{
			throw gcnew InvalidOperationException("unknown BoxOrientation" + __WCODESIG__);
		}

		if (this->Orientation == BoxOrientation::Left)
		{
			b->RightNeighbors->Add(this->_parent);
		}
		else if (this->Orientation == BoxOrientation::Right)
		{
			b->LeftNeighbors->Add(this->_parent);
		}
		else if (this->Orientation == BoxOrientation::Up)
		{
			b->DownNeighbors->Add(this->_parent);
		}
		else if (this->Orientation == BoxOrientation::Down)
		{
			b->UpNeighbors->Add(this->_parent);
		}
	}
}

bool BoxNeighborCollection::Remove(IBox^ b)
{
	if (!b) { throw gcnew ArgumentNullException(__WCODESIG__); }

	if (this->Contains(b))
	{
		if (this->Orientation == BoxOrientation::Left || 
			this->Orientation == BoxOrientation::Right || 
			this->Orientation == BoxOrientation::Up || 
			this->Orientation == BoxOrientation::Down)
		{
			if (!this->_set->Remove(b)) { return false; }
		}
		else
		{
			throw gcnew InvalidOperationException("unknown BoxOrientation" + __WCODESIG__);
		}

		if (this->Orientation == BoxOrientation::Left)
		{
			b->RightNeighbors->Remove(this->_parent);
		}
		else if (this->Orientation == BoxOrientation::Right)
		{
			b->LeftNeighbors->Remove(this->_parent);
		}
		else if (this->Orientation == BoxOrientation::Up)
		{
			b->DownNeighbors->Remove(this->_parent);
		}
		else if (this->Orientation == BoxOrientation::Down)
		{
			b->UpNeighbors->Remove(this->_parent);
		}

		return true;
	}

	return false;
}

bool BoxNeighborCollection::Contains(IBox^ b)
{
	return this->_set->Contains(b);
}

void BoxNeighborCollection::Clear(void)
{
	array<IBox^>^	r;
	r = gcnew array<IBox^>(this->Count);
	this->CopyTo(r, 0);
	for each (IBox^ b in r)
	{
		this->Remove(b);
	}
	this->_set->Clear();
}

void BoxNeighborCollection::CopyTo(array<IBox^>^ r, int i)
{
	this->_set->CopyTo(r, i);
}

IEnumerator<IBox^>^ BoxNeighborCollection::GetEnumerator(void)
{
	return this->_set->GetEnumerator();
}

int BoxNeighborCollection::Count::get(void)
{
	return this->_set->Count;
}

bool BoxNeighborCollection::IsReadOnly::get(void)
{
	return this->_set->IsReadOnly;
}

BoxOrientation BoxNeighborCollection::Orientation::get(void)
{
	return this->_orientation;
}

System::Collections::IEnumerator^ BoxNeighborCollection::GetEnumerator2(void)
{
	return this->GetEnumerator();
}






} } 


