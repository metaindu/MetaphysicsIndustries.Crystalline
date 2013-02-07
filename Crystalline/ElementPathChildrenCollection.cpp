
//////////// source file ////////////


/*****************************************************************************
 *                                                                           *
 *  ElementPathChildrenCollection.cpp                                        *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  An unordered collection of Path objects.                                 *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "ElementPathChildrenCollection.h"
#include "Element.h"
#include "Path.h"



NAMESPACE_START;


ElementPathChildrenCollection::ElementPathChildrenCollection(Element^ element)
{
	this->parent = element;
	this->set = gcnew Set<Path^>;
}

ElementPathChildrenCollection::~ElementPathChildrenCollection(void)
{
	this->Clear();
	delete this->set;
}

void ElementPathChildrenCollection::Add(Path^ path)
{
	if (!this->Contains(path))
	{
		//path->ParentElement = nullptr;
		this->SetChildsParent(path, nullptr);
		this->set->Add(path);
		//path->ParentElement = this->parent;
		this->SetChildsParent(path, this->parent);
	}
}

bool ElementPathChildrenCollection::Contains(Path^ path)
{
	return this->set->Contains(path);
}

bool ElementPathChildrenCollection::Remove(Path^ path)
{
	bool	b;

	b = this->set->Remove(path);
//	if (b && path->ParentElement == this->parent)
	if (b && this->GetChildsParent(path) == this->parent)
	{
		//path->ParentElement = nullptr;
		this->SetChildsParent(path, nullptr);
	}

	return b;
}

void ElementPathChildrenCollection::Clear(void)
{
	List<Path^>^	list;

	list = gcnew List<Path^>;
	for each (Path^ path in this)
	{
		list->Add(path);
	}

	for each (Path^ path in list)
	{
		this->Remove(path);
	}

	list->Clear();
	this->set->Clear();
}

void ElementPathChildrenCollection::CopyTo(array<Path^>^ r, int i)
{
	this->set->CopyTo(r, i);
}

IEnumerator<Path^>^ ElementPathChildrenCollection::GetEnumerator(void)
{
	return this->set->GetEnumerator();
}

int ElementPathChildrenCollection::Count::get(void)
{
	return this->set->Count;
}

bool ElementPathChildrenCollection::IsReadOnly::get(void)
{
	return this->set->IsReadOnly;
}

System::Collections::IEnumerator^ ElementPathChildrenCollection::GetEnumerator2(void)
{
	return this->GetEnumerator();
}



NAMESPACE_END;



