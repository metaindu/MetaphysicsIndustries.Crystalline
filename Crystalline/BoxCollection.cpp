
/*****************************************************************************
 *                                                                           *
 *  BoxCollection.cpp                                                        *
 *  28 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A class that holds a set of boxes, and coordinates them with the         *
 *    BoxFramework. This is the common base class for ElementCollection,     *
 *    PathwayCollection and PathingJunctionCollection.                       *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "BoxCollection.h"



namespace MetaphysicsIndustries { namespace Crystalline { 
;



generic<typename T>
where T : IBox
BoxCollection<T>::BoxCollection(BoxFramework^ f)
{
	if (!f) { throw gcnew ArgumentNullException("f" + __WCODESIG__); }

	this->framework = f;
	this->collection = gcnew Set<T>();
}

generic<typename T>
where T : IBox
BoxCollection<T>::~BoxCollection(void)
{
	this->Clear();

	this->collection->Clear();
	delete this->collection;
	this->collection = nullptr;

	this->framework = nullptr;
}

generic<typename T>
where T : IBox
void BoxCollection<T>::Add(T boxToAdd)
{
	this->framework->Add(boxToAdd);
	this->collection->Add(boxToAdd);
}

generic<typename T>
where T : IBox
bool BoxCollection<T>::Remove(T boxToRemove)
{
	this->framework->Remove(boxToRemove);
	return this->collection->Remove(boxToRemove);
}

generic<typename T>
where T : IBox
bool BoxCollection<T>::Contains(T boxToTest)
{
	return this->collection->Contains(boxToTest);
}

generic<typename T>
where T : IBox
void BoxCollection<T>::Clear(void)
{
	array<T>^	r;
	r = gcnew array<T>(this->Count);
	for each (T t in r)
	{
		this->Remove(t);
	}

	this->collection->Clear();
}

generic<typename T>
where T : IBox
void BoxCollection<T>::CopyTo(array<T>^ r, int arrayIndex)
{
	if (!r) { throw gcnew ArgumentNullException(__WCODESIG__); }
	if (arrayIndex < 0) { throw gcnew ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument is out of range" + __WCODESIG__); }
	if (r->Rank > 1) { throw gcnew ArgumentException("Only one-dimensional arrays are allowed" + __WCODESIG__, "array"); }
	if (this->Count > r->Length - arrayIndex) { throw gcnew ArgumentException("Not enough available space in the array" + __WCODESIG__); }

	for each (T t in this)
	{
		r[arrayIndex] = t;
		arrayIndex++;
	}
}

generic<typename T>
where T : IBox
IEnumerator<T>^ BoxCollection<T>::GetEnumerator(void)
{
	return this->collection->GetEnumerator();
}

generic<typename T>
where T : IBox
int BoxCollection<T>::Count::get(void)
{
	return this->collection->Count;
}

generic<typename T>
where T : IBox
bool BoxCollection<T>::IsReadOnly::get(void)
{
	return this->collection->IsReadOnly;	//probably should always be false
}

generic<typename T>
where T : IBox
System::Collections::IEnumerator^ BoxCollection<T>::GetEnumerator2(void)
{
	return this->GetEnumerator();
}



} } 




