
/*****************************************************************************
 *                                                                           *
 *  Path.h                                                                   *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that represents a connection from one element to another.        *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Element.h"
#include "PathJoint.h"
#include "PathPathJointChildrenCollection.h"

using namespace System;
using namespace System::Drawing;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



ref class Element;
ref class PathJoint;
public ref class Path
{

public:

	Path(void);
	virtual ~Path(void);

	virtual void Render(Graphics^, Pen^, bool renderpathjoints);

	virtual property PathPathJointChildrenCollection^ PathJoints
	{
		PathPathJointChildrenCollection^ get(void);
	}

	virtual property Element^ To
	{
		Element^ get(void);
		void set(Element^);
	}
	virtual property Element^ From
	{
		Element^ get(void);
		void set(Element^);
	}

	event EventHandler^	ToChanging;
	event EventHandler^	FromChanging;
	event EventHandler^	ToChanged;
	event EventHandler^	FromChanged;

protected:

	virtual void OnToChanging(EventArgs^);
	virtual void OnFromChanging(EventArgs^);
	virtual void OnToChanged(EventArgs^);
	virtual void OnFromChanged(EventArgs^);

	PathPathJointChildrenCollection^	pathjoints;

	Element^							to;
	Element^							from;


private:

};



} } 

