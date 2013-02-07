
/*****************************************************************************
 *                                                                           *
 *  Path.cpp                                                                 *
 *  25 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that represents a connection from one element to another.        *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "Path.h"
#include "InboundToElementPathChildrenCollection.h"
#include "OutboundFromElementPathChildrenCollection.h"



;
NAMESPACE_START
;



Path::Path(void)
{
	this->pathjoints = gcnew PathPathJointChildrenCollection(this);
}

Path::~Path(void)
{
	this->pathjoints->Clear();
	delete this->pathjoints;
	this->pathjoints = nullptr;
}

Element^ Path::To::get(void)
{
	return this->to;
}
void Path::To::set(Element^ e)
{
	if (this->to != e)
	{
		this->OnToChanging(gcnew EventArgs);

		if (this->to)
		{
			this->to->Inbound->Remove(this);
		}

		this->to = e;

		if (this->to)
		{
			this->to->Inbound->Add(this);
		}

		this->OnToChanged(gcnew EventArgs);
	}
}

Element^ Path::From::get(void)
{
	return this->from;
}
void Path::From::set(Element^ e)
{
	if (this->from != e)
	{
		this->OnFromChanging(gcnew EventArgs);

		if (this->from)
		{
			this->from->Outbound->Remove(this);
		}

		this->from = e;

		if (this->from)
		{
			this->from->Outbound->Add(this);
		}

		this->OnFromChanged(gcnew EventArgs);
	}
}


void Path::OnToChanging(EventArgs^ e)
{
	this->ToChanging(this, e);
}
void Path::OnToChanged(EventArgs^ e)
{
	this->ToChanged(this, e);
}
void Path::OnFromChanging(EventArgs^ e)
{
	this->FromChanging(this, e);
}
void Path::OnFromChanged(EventArgs^ e)
{
	this->FromChanged(this, e);
}

void Path::Render(Graphics^ g, Pen^ pen, bool renderpathjoints)
{
	Graphics^ _g = g;

	int	i;
	int	j;
	j = this->PathJoints->Count;
	for (i = 0; i < j - 1; i++)
	{
		if (renderpathjoints)
		{
			this->PathJoints[i]->Render(g, pen);
		}
		g->DrawLine(Pens::Black, this->PathJoints[i]->Location, this->PathJoints[i + 1]->Location);
	}

	if (renderpathjoints)
	{
		if (j > 1 && this->To)
		{
			//array<PointF>^	r = { PointF(), PointF(), PointF() };
			//PointF	p;
			//float	angle;
			//float	angle2;
			//float	size;

			//size = 10;

			//p = this->PathJoints[j - 1]->Location - SizeF(this->PathJoints[j - 2]->Location);
			//angle = (float)Math::Atan2(p.Y, p.X);

			//r[0] = this->PathJoints[j - 1]->Location;
			//angle2 = angle + (float)(Math::PI * 5.0 / 6.0);
			//r[1] = PointF(size * (float)Math::Cos(angle2), size * (float)Math::Sin(angle2)) + SizeF(r[0]);
			//angle2 = angle - (float)(Math::PI * 5.0 / 6.0);
			//r[2] = PointF(size * (float)Math::Cos(angle2), size * (float)Math::Sin(angle2)) + SizeF(r[0]);

			//g->FillPolygon(Brushes::Black, r);

			this->PathJoints[j - 1]->RenderArrow(g, pen, this->PathJoints[j - 2]->Location);
		}
		else if (j > 0)
		{
			this->PathJoints[j - 1]->Render(g, pen);
		}
	}

}

PathPathJointChildrenCollection^ Path::PathJoints::get(void)
{
	return this->pathjoints;
}




;
NAMESPACE_END
;




