
/*****************************************************************************
 *                                                                           *
 *  Element.cpp                                                              *
 *  23 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that represents the visual aspects of the elements which are     *
 *    connected together in the graph.                                       *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "Element.h"
#include "InboundToElementPathChildrenCollection.h"
#include "OutboundFromElementPathChildrenCollection.h"



;
NAMESPACE_START
;



Element::Element(void)
{
	this->inbound = gcnew InboundToElementPathChildrenCollection(this);
	this->outbound = gcnew OutboundFromElementPathChildrenCollection(this);
}

Element::~Element(void)
{
	
}

void Element::Render(Graphics^ g, Pen^ pen, Font^ font)
{
	Graphics^ _g = g;

	float	min;
	float	max;

	for each (IBox^ ib in this->LeftNeighbors)
	{
		min = Math::Max(this->Top, ib->Top);
		max = Math::Min(this->Bottom, ib->Bottom);
		min = (min + max) / 2;
		min++;
		g->DrawLine(Pens::Blue, this->Left, min, ib->Right, min);
	}
	for each (IBox^ ib in this->UpNeighbors)
	{
		min = Math::Max(this->Left, ib->Left);
		max = Math::Min(this->Right, ib->Right);
		min = (min + max) / 2;
		min++;
		g->DrawLine(Pens::Blue, min, this->Top, min, ib->Bottom);
	}

	for each (IBox^ ib in this->RightNeighbors)
	{
		min = Math::Max(this->Top, ib->Top);
		max = Math::Min(this->Bottom, ib->Bottom);
		min = (min + max) / 2;
		min--;
		g->DrawLine(Pens::Orange, this->Right, min, ib->Left, min);
	}
	for each (IBox^ ib in this->DownNeighbors)
	{
		min = Math::Max(this->Left, ib->Left);
		max = Math::Min(this->Right, ib->Right);
		min = (min + max) / 2;
		min--;
		g->DrawLine(Pens::Orange, min, this->Bottom, min, ib->Top);
	}


	RectangleF	r;

	if (pen == nullptr)
	{
		pen = Pens::Black;
	}

	r = this->Rect;
	g->DrawRectangle(pen, r.X, r.Y, r.Width, r.Height);
	if (this->Param)
	{
		g->DrawString(this->Param->ToString(), font, pen->Brush, r.X + 2, r.Y + 2);
	}
}

//RectangleF Element::Rect::get(void)
//{
//	return RectangleF(this->Location, this->Size);
//}

InboundToElementPathChildrenCollection^ Element::Inbound::get(void)
{
	return this->inbound;
}

OutboundFromElementPathChildrenCollection^ Element::Outbound::get(void)
{
	return this->outbound;
}

System::Object^ Element::Param::get(void)
{
	return this->param;
}
void Element::Param::set(System::Object^ o)
{
	if (this->param != o)
	{
		this->OnParamChanging(gcnew EventArgs);

		this->param = o;

		this->OnParamChanged(gcnew EventArgs);
	}
}


void Element::OnParamChanging(EventArgs^ e)
{
	this->ParamChanging(this, e);
}
void Element::OnParamChanged(EventArgs^ e)
{
	this->ParamChanged(this, e);
}

//PointF Element::Location::get(void)
//{
//	return this->location;
//}
//void Element::Location::set(PointF pf)
//{
//	if (this->location != pf)
//	{
//		this->OnLocationChanging(gcnew EventArgs);
//
//		this->location = pf;
//
//		this->OnLocationChanged(gcnew EventArgs);
//	}
//}


void Element::OnLocationChanging(EventArgs^ e)
{
	this->LocationChanging(this, e);
}
void Element::OnLocationChanged(EventArgs^ e)
{
	this->LocationChanged(this, e);
}

//SizeF Element::Size::get(void)
//{
//	return this->size;
//}
//void Element::Size::set(SizeF sf)
//{
//	if (this->size != sf)
//	{
//		this->OnSizeChanging(gcnew EventArgs);
//
//		this->size = sf;
//
//		this->OnSizeChanged(gcnew EventArgs);
//	}
//}


void Element::OnSizeChanging(EventArgs^ e)
{
	this->SizeChanging(this, e);
}
void Element::OnSizeChanged(EventArgs^ e)
{
	this->SizeChanged(this, e);
}

float Element::TerminalSpacing::get(void)
{
	return 10;
}



;
NAMESPACE_END
;


