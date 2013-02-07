
/*****************************************************************************
 *                                                                           *
 *  Pathway.h                                                                *
 *  30 January 2007                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  Acts as the pathway between two PathingJunction objects. Path objects    *
 *    follow it and turn at the junctions.                                   *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Element.h"
#include "Path.h"
#include "PathingJunction.h"
#include "Box.h"

using namespace System;
using namespace System::Drawing;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class Pathway : Box
{

public:

	Pathway(void);
	virtual ~Pathway(void);

	virtual void Render(Graphics^, Pen^);

	virtual property PathingJunction^ LeftUp
	{
		PathingJunction^ get(void);
		void set(PathingJunction^);
	}
	virtual property PathingJunction^ RightDown
	{
		PathingJunction^ get(void);
		void set(PathingJunction^);
	}

	virtual property Element^ LeftUpElement
	{
		Element^ get(void);
		void set(Element^);
	}
	virtual property Element^ RightDownElement
	{
		Element^ get(void);
		void set(Element^);
	}

	virtual property bool IsVertical
	{
		bool get(void);
		void set(bool);
	}


protected:

	PathingJunction^	leftup;
	PathingJunction^	rightdown;

	Element^	leftupelement;
	Element^	rightdownelement;

	bool	isvertical;

private:

};



} } 


