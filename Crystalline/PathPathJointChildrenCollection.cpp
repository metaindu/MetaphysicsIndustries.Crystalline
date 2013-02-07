
//////////// source file ////////////


/*****************************************************************************
 *                                                                           *
 *  PathPathJointChildrenCollection.cpp                                      *
 *  1 December 2006                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  An unordered collection of PathJoint objects.                            *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "PathPathJointChildrenCollection.h"
#include "Path.h"
#include "PathJoint.h"



NAMESPACE_START;


PathPathJointChildrenCollection::PathPathJointChildrenCollection(Path^ path)
{
	this->parent = path;
	this->list = gcnew List<PathJoint^>;
}

PathPathJointChildrenCollection::~PathPathJointChildrenCollection(void)
{
	this->Clear();
	delete this->list;
}

void PathPathJointChildrenCollection::Add(PathJoint^ pj)
{
	if (!this->Contains(pj))
	{
		pj->ParentPath = nullptr;
		this->list->Add(pj);
		pj->ParentPath = this->parent;
	}
}

bool PathPathJointChildrenCollection::Contains(PathJoint^ pj)
{
	return this->list->Contains(pj);
}

bool PathPathJointChildrenCollection::Remove(PathJoint^ pj)
{
	bool	b;

	b = this->list->Remove(pj);
	if (b && pj->ParentPath == this->parent)
	{
		pj->ParentPath = nullptr;
	}

	return b;
}

void PathPathJointChildrenCollection::Clear(void)
{
	List<PathJoint^>^	list;

	list = gcnew List<PathJoint^>;
	for each (PathJoint^ pj in this)
	{
		list->Add(pj);
	}

	for each (PathJoint^ pj in list)
	{
		this->Remove(pj);
	}

	this->list->Clear();
}

void PathPathJointChildrenCollection::CopyTo(array<PathJoint^>^ r, int i)
{
	this->list->CopyTo(r, i);
}

IEnumerator<PathJoint^>^ PathPathJointChildrenCollection::GetEnumerator(void)
{
	return this->list->GetEnumerator();
}

int PathPathJointChildrenCollection::Count::get(void)
{
	return this->list->Count;
}

bool PathPathJointChildrenCollection::IsReadOnly::get(void)
{
	return this->list->IsReadOnly;
}

System::Collections::IEnumerator^ PathPathJointChildrenCollection::GetEnumerator2(void)
{
	return this->GetEnumerator();
}

int PathPathJointChildrenCollection::IndexOf(PathJoint^ pj)
{
	return this->list->IndexOf(pj);
}

void PathPathJointChildrenCollection::Insert(int i, PathJoint^ pj)
{
	this->list->Insert(i, pj);
}

void PathPathJointChildrenCollection::RemoveAt(int i)
{
	this->list->RemoveAt(i);
}

PathJoint^ PathPathJointChildrenCollection::default::get(int i)
{
	return this->list[i];
}

void PathPathJointChildrenCollection::default::set(int i, PathJoint^ pj)
{
	this->list[i] = pj;
}

PathJoint^ PathPathJointChildrenCollection::First::get(void)
{
	return this[0];
}
PathJoint^ PathPathJointChildrenCollection::Last::get(void)
{
	return this[this->Count - 1];
}



NAMESPACE_END;



