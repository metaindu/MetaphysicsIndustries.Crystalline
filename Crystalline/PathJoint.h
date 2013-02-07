
/*****************************************************************************
 *                                                                           *
 *  PathJoint.h                                                              *
 *  26 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Path.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Drawing;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



ref class Path;
public ref class PathJoint// : KeyValuePair<Path^, int>
{

public:

	PathJoint(void);
	PathJoint(PointF Location);
	//virtual ~PathJoint(void);

	virtual void Render(Graphics^, Pen^);
	virtual void RenderArrow(Graphics^, Pen^, PointF previousjointlocation);

	virtual property Path^ ParentPath
	{
		Path^ get(void);
		void set(Path^);
	}
	//virtual property int Index
	//{
	//	int get(void);
	//	void set(int);
	//}
	virtual property PointF Location
	{
		PointF get(void);
		void set(PointF);
	}

protected:

	Path^	parentpath;
	int		index;
	PointF	location;

private:

};



} } 

