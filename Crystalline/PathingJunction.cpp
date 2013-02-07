
/*****************************************************************************
 *                                                                           *
 *  PathingJunction.cpp                                                      *
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



#include "stdafx.h"
#include "PathingJunction.h"
#include "Element.h"
#include "Path.h"
#include "Pathway.h"



;
NAMESPACE_START
;



PathingJunction::PathingJunction(void)
{
	//this->pathsup = gcnew Set<Path^>();
	//this->pathsdown = gcnew Set<Path^>();
	//this->pathsleft = gcnew Set<Path^>();
	//this->pathsright = gcnew Set<Path^>();
	this->Width = 25.0f;
	this->Height = 25.0f;
}

PathingJunction::~PathingJunction(void)
{
	
}

Pathway^ PathingJunction::UpPathway::get(void)
{
	return this->uppathway;
}
void PathingJunction::UpPathway::set(Pathway^ p)
{
	if (this->uppathway != p)
	{
		if (this->uppathway)
		{
			this->UpPathway->RightDown = nullptr;
		}

		this->uppathway = p;

		if (p)
		{
			p->IsVertical = true;
			p->RightDown = this;

			this->EUp = nullptr;
		}
	}
}

Pathway^ PathingJunction::DownPathway::get(void)
{
	return this->downpathway;
}
void PathingJunction::DownPathway::set(Pathway^ p)
{
	if (this->downpathway != p)
	{
		this->downpathway = p;

		if (p)
		{
			this->EDown = nullptr;
		}
	}
}

Pathway^ PathingJunction::LeftPathway::get(void)
{
	return this->leftpathway;
}
void PathingJunction::LeftPathway::set(Pathway^ p)
{
	if (this->leftpathway != p)
	{
		this->leftpathway = p;

		if (p)
		{
			this->ELeft = nullptr;
		}
	}
}

Pathway^ PathingJunction::RightPathway::get(void)
{
	return this->rightpathway;
}
void PathingJunction::RightPathway::set(Pathway^ p)
{
	if (this->rightpathway != p)
	{
		this->rightpathway = p;

		if (p)
		{
			this->ERight = nullptr;
		}
	}
}

Element^ PathingJunction::EUp::get(void)
{
	return this->eup;
}
void PathingJunction::EUp::set(Element^ e)
{
	if (this->eup != e)
	{
		this->eup = e;

		if (e)
		{
			this->UpPathway = nullptr;
		}
	}
}

Element^ PathingJunction::EDown::get(void)
{
	return this->edown;
}
void PathingJunction::EDown::set(Element^ e)
{
	if (this->edown != e)
	{
		this->edown = e;

		if (e)
		{
			this->DownPathway = nullptr;
		}
	}
}

Element^ PathingJunction::ELeft::get(void)
{
	return this->eleft;
}
void PathingJunction::ELeft::set(Element^ e)
{
	if (this->eleft != e)
	{
		this->eleft = e;

		if (e)
		{
			this->LeftPathway = nullptr;
		}
	}
}

Element^ PathingJunction::ERight::get(void)
{
	return this->eright;
}
void PathingJunction::ERight::set(Element^ e)
{
	if (this->eright != e)
	{
		this->eright = e;

		if (e)
		{
			this->RightPathway = nullptr;
		}
	}
}

//Set<Path^>^ PathingJunction::PathsUp::get(void)
//{
//	return this->pathsup;
//}
//Set<Path^>^ PathingJunction::PathsDown::get(void)
//{
//	return this->pathsdown;
//}
//Set<Path^>^ PathingJunction::PathsLeft::get(void)
//{
//	return this->pathsleft;
//}
//Set<Path^>^ PathingJunction::PathsRight::get(void)
//{
//	return this->pathsright;
//}

//float PathingJunction::X::get(void)
//{
//	return this->x;
//}
//void PathingJunction::X::set(float f)
//{
//	if (this->x != f)
//	{
//		this->x = f;
//	}
//}

//float PathingJunction::Y::get(void)
//{
//	return this->y;
//}
//void PathingJunction::Y::set(float f)
//{
//	if (this->y != f)
//	{
//		this->y = f;
//	}
//}

void PathingJunction::Render(Graphics^ g, Pen^ pen)
{
	Graphics^ _g = g;

	//RectangleF	r;

	if (pen == nullptr)
	{
		pen = Pens::Black;
	}

	//r = RectangleF(this->X, this->Y, 25, 25);

	g->DrawRectangle(pen, this->X, this->Y, this->Width, this->Height);
}






;
NAMESPACE_END
;


