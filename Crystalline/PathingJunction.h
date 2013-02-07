
/*****************************************************************************
 *                                                                           *
 *  PathingJunction.h                                                        *
 *  24 January 2007                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that serves as a node in the graph of path between elements.     *
 *    Associated with PathJoints. Directs the flow of paths around and       *
 *    between elements so everything is neat and tidy.                       *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Box.h"

using namespace System;
using namespace System::Drawing;
using namespace MetaphysicsIndustries::Collections;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



ref class Element;
ref class Path;
ref class Pathway;
public ref class PathingJunction : Box
{

public:

	PathingJunction(void);
	virtual ~PathingJunction(void);

	virtual void Render(Graphics^, Pen^);

	virtual property Pathway^ UpPathway
	{
		Pathway^ get(void);
		void set(Pathway^);
	}
	virtual property Pathway^ DownPathway
	{
		Pathway^ get(void);
		void set(Pathway^);
	}
	virtual property Pathway^ LeftPathway
	{
		Pathway^ get(void);
		void set(Pathway^);
	}
	virtual property Pathway^ RightPathway
	{
		Pathway^ get(void);
		void set(Pathway^);
	}

	virtual property Element^ EUp
	{
		Element^ get(void);
		void set(Element^);
	}
	virtual property Element^ EDown
	{
		Element^ get(void);
		void set(Element^);
	}
	virtual property Element^ ELeft
	{
		Element^ get(void);
		void set(Element^);
	}
	virtual property Element^ ERight
	{
		Element^ get(void);
		void set(Element^);
	}

	//virtual property Set<Path^>^ PathsUp
	//{
	//	Set<Path^>^ get(void);
	//}
	//virtual property Set<Path^>^ PathsDown
	//{
	//	Set<Path^>^ get(void);
	//}
	//virtual property Set<Path^>^ PathsLeft
	//{
	//	Set<Path^>^ get(void);
	//}
	//virtual property Set<Path^>^ PathsRight
	//{
	//	Set<Path^>^ get(void);
	//}

	//virtual property float X
	//{
	//	float get(void);
	//	void set(float);
	//}
	//virtual property float Y
	//{
	//	float get(void);
	//	void set(float);
	//}

protected:

	Pathway^	uppathway;
	Pathway^	downpathway;
	Pathway^	leftpathway;
	Pathway^	rightpathway;

	Element^	eup;
	Element^	edown;
	Element^	eleft;
	Element^	eright;

	//Set<Path^>^	pathsup;
	//Set<Path^>^	pathsdown;
	//Set<Path^>^	pathsleft;
	//Set<Path^>^	pathsright;

	//float	x;
	//float	y;

private:

};



} } 



