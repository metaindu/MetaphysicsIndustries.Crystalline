
/*****************************************************************************
 *                                                                           *
 *  ElementCollection.cpp                                                    *
 *  25 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A class that holds the control's set of elements, and coordinates them   *
 *    with the BoxFramework. This class and PathwayCollection and            *
 *    PathingJunctionCollection may inherit from a common base class at      *
 *    some later point in time.                                              *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "ElementCollection.h"



namespace MetaphysicsIndustries { namespace Crystalline { 
;



ElementCollection::ElementCollection(BoxFramework^ f) : BoxCollection(f)
{
	//if (!f) { throw gcnew ArgumentNullException("f " + __WCODESIG__); }

	//this->framework = f;

	//this->elements = gcnew Set<Element^>();
}

//ElementCollection::~ElementCollection(void)
//{
//	this->Clear();
//	this->elements->Clear();
//	delete this->elements;
//	this->elements = nullptr;
//}
//
//void ElementCollection::Add(Element^ e)
//{
//	this->elements->Add(e);
//	this->framework->Add(e);
//}
//
//
//bool ElementCollection::Remove(Element^ e)
//{
//	this->framework->Remove(e);
//	return this->elements->Remove(e);
//}
//
//
//bool ElementCollection::Contains(Element^ e)
//{
//	return this->elements->Contains(e);
//}
//
//
//void ElementCollection::Clear(void)
//{
//	array<Element^>^	r;
//	r = gcnew array<Element^>(this->Count);
//	this->CopyTo(r, 0);
//	for each (Element^ e in r)
//	{
//		this->Remove(e);
//	}
//
//	this->elements->Clear();
//}
//
//void ElementCollection::CopyTo(array<Element^>^ array, int arrayIndex)
//{
//	this->elements->CopyTo(array, arrayIndex);
//}
//
//IEnumerator<Element^>^ ElementCollection::GetEnumerator(void)
//{
//	return this->elements->GetEnumerator();
//}
//
//System::Collections::IEnumerator^ ElementCollection::GetEnumerator2(void)
//{
//	return this->GetEnumerator();
//}
//
//int ElementCollection::Count::get(void)
//{
//	return this->elements->Count;
//}
//
//bool ElementCollection::IsReadOnly::get(void)
//{
//	return this->elements->IsReadOnly;
//}


} } 



