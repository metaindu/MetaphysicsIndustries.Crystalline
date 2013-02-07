/*****************************************************************************
 *                                                                           *
 *  PathPathJointChildrenCollection.h                                        *
 *  1 December 2006                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  An unordered collection of PathJoint objects.                            *
 *                                                                           *
 *****************************************************************************/



#pragma once

using namespace MetaphysicsIndustries::Collections;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



ref class Path;
ref class PathJoint;
public ref class PathPathJointChildrenCollection : IList<PathJoint^>
{

public:

	PathPathJointChildrenCollection(Path^);
	virtual ~PathPathJointChildrenCollection(void);

	virtual int IndexOf(PathJoint^);
	virtual void Insert(int, PathJoint^);
	virtual void RemoveAt(int);
	virtual void Add(PathJoint^);
	virtual bool Contains(PathJoint^);
	virtual bool Remove(PathJoint^);
	virtual void Clear(void);
	virtual void CopyTo(array<PathJoint^>^, int);
	virtual IEnumerator<PathJoint^>^ GetEnumerator(void);

	virtual property PathJoint^ default [ int ]
	{
		PathJoint^ get(int);
		void set(int, PathJoint^);
	}

	virtual property int Count
	{
		int get(void);
	}
	virtual property bool IsReadOnly
	{
		bool get(void);
	}

	virtual property PathJoint^ First
	{
		PathJoint^ get(void);
	}
	virtual property PathJoint^ Last
	{
		PathJoint^ get(void);
	}

protected:

	virtual System::Collections::IEnumerator^ GetEnumerator2(void) = System::Collections::IEnumerable::GetEnumerator;

	Path^				parent;
	IList<PathJoint^>^	list;

};



} } 



