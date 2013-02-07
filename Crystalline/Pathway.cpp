
/*****************************************************************************
 *                                                                           *
 *  Pathway.cpp                                                              *
 *  30 January 2007                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  Acts as the pathway between two PathingJunction objects. Path objects    *
 *    follow it and turn at the junctions.                                   *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "Pathway.h"



;
NAMESPACE_START
;



Pathway::Pathway(void)
{
	this->Size = SizeF(50, 25);
}

Pathway::~Pathway(void)
{
}

void Pathway::Render(Graphics^ g, Pen^ pen)
{
	Graphics^ _g = g;

	RectangleF	r;

	if (pen == nullptr)
	{
		pen = Pens::Black;
	}

	if (this->IsVertical)
	{
		if (this->LeftUp)
		{
			if (this->RightDown)
			{
				r.X = Math::Min(this->LeftUp->X, this->RightDown->X);
				r.Width = Math::Max(this->LeftUp->X, this->RightDown->X) + 25;
				r.Height = Math::Abs(this->LeftUp->Y - this->RightDown->Y) - 25;
			}
			else
			{
				r.X = this->LeftUp->X;
				r.Width = 25;//this->LeftUp->Width;
				r.Height = 50;
			}
			r.Y = this->LeftUp->Y + 25;
		}
		else
		{
			if (this->RightDown)
			{
				r.X = this->RightDown->X;
				r.Width = 25;
				r.Y = this->RightDown->Y - 50;
				r.Height = 50;
			}
			else
			{
				r = RectangleF(0, 0, 0, 0);
			}
		}
	}
	else
	{
		if (this->LeftUp)
		{
			if (this->RightDown)
			{
				r.X = Math::Min(this->LeftUp->X, this->RightDown->X) + 25;
				r.Y = Math::Min(this->LeftUp->Y, this->RightDown->Y);
				r.Width = Math::Abs(this->LeftUp->X - this->RightDown->X) - 25;
				r.Height = Math::Max(this->LeftUp->Y, this->RightDown->Y) + 25;
			}
			else
			{
				r.X = this->LeftUp->X + 25;
				r.Y = this->LeftUp->Y;
				r.Width = 50;
				r.Height = 25;
			}
		}
		else
		{
			if (this->RightDown)
			{
				r.X = this->RightDown->X - 50;
				r.Y = this->RightDown->Y;
				r.Width = 50;
				r.Height = 25;
			}
			else
			{
				r = RectangleF(0, 0, 0, 0);
			}
		}
	}

	//r = RectangleF(this->X, this->Y, 25, 25);

	//g->DrawRectangle(pen, r.X+1, r.Y+1, r.Width-2, r.Height-2);
	r = RectangleF::Inflate(this->Rect, -1, -1);
	g->DrawRectangle(pen, r.X, r.Y, r.Width, r.Height);
}

PathingJunction^ Pathway::LeftUp::get(void)
{
	return this->leftup;
}
void Pathway::LeftUp::set(PathingJunction^ pj)
{
	if (this->leftup != pj)
	{
		this->leftup = pj;
	}
}

PathingJunction^ Pathway::RightDown::get(void)
{
	return this->rightdown;
}
void Pathway::RightDown::set(PathingJunction^ pj)
{
	if (this->rightdown != pj)
	{
		this->rightdown = pj;
	}
}

Element^ Pathway::LeftUpElement::get(void)
{
	return this->leftupelement;
}
void Pathway::LeftUpElement::set(Element^ e)
{
	if (this->leftupelement != e)
	{
		this->leftupelement = e;

		if (this->LeftUp && this->RightDown)
		{
			if (this->IsVertical)
			{
				this->LeftUp->X = this->RightDown->X;
			}
			else
			{
				this->LeftUp->Y = this->RightDown->Y;
			}
		}
		if (this->IsVertical)
		{
			this->LeftUp->DownPathway = this;
		}
		else
		{
			this->RightDown->UpPathway = this;
		}
	}
}

Element^ Pathway::RightDownElement::get(void)
{
	return this->rightdownelement;
}
void Pathway::RightDownElement::set(Element^ e)
{
	if (this->rightdownelement != e)
	{
		this->rightdownelement = e;
	}
}

bool Pathway::IsVertical::get(void)
{
	return this->isvertical;
}
void Pathway::IsVertical::set(bool b)
{
	if (this->isvertical != b)
	{
		this->isvertical = b;
		if (b)
		{
			this->Width = 25;
			this->Height = 50;
		}
		else
		{
			this->Width = 50;
			this->Height = 25;
		}
	}
}






;
NAMESPACE_END
;

