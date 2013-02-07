
/*****************************************************************************
 *                                                                           *
 *  PathJoint.cpp                                                            *
 *  26 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "PathJoint.h"
#include "Path.h"



;
NAMESPACE_START
;



PathJoint::PathJoint(void)
{
	this->location = PointF(0, 0);
}

PathJoint::PathJoint(PointF _location)
{
	this->location = _location;
}

//PathJoint::~PathJoint(void)
//{
//	
//}

Path^ PathJoint::ParentPath::get(void)
{
	return this->parentpath;
}
void PathJoint::ParentPath::set(Path^ _parentpath)
{
	if (this->parentpath != _parentpath)
	{
		if (this->parentpath)
		{
			this->parentpath->PathJoints->Remove(this);
		}

		this->parentpath = _parentpath;

		if (this->parentpath)
		{
			this->parentpath->PathJoints->Add(this);
		}
	}
}

//int PathJoint::Index::get(void)
//{
//	return this->index;
//}
//void PathJoint::Index::set(int i)
//{
//	if (this->index != i)
//	{
//		this->index = i;
//	}
//}

PointF PathJoint::Location::get(void)
{
	return this->location;
}
void PathJoint::Location::set(PointF pf)
{
	if (this->location != pf)
	{
		this->location = pf;
	}
}


void PathJoint::Render(Graphics^ g, Pen^ pen)
{
	RectangleF	r;
	r.Location = this->Location - SizeF(1, 1);
	r.Size = SizeF(3, 3);
	g->FillRectangle(pen->Brush, r);
}

void PathJoint::RenderArrow(Graphics^ g, Pen^ pen, PointF previousjointlocation)
{
	array<PointF>^	r = { PointF(), PointF(), PointF() };
	PointF	p;
	float	angle;
	float	angle2;
	float	size;

	size = 10;

	p = this->Location - SizeF(previousjointlocation);
	angle = (float)Math::Atan2(p.Y, p.X);

	r[0] = this->Location;
	angle2 = angle + (float)(Math::PI * 5.0 / 6.0);
	r[1] = PointF(size * (float)Math::Cos(angle2), size * (float)Math::Sin(angle2)) + SizeF(r[0]);
	angle2 = angle - (float)(Math::PI * 5.0 / 6.0);
	r[2] = PointF(size * (float)Math::Cos(angle2), size * (float)Math::Sin(angle2)) + SizeF(r[0]);

	g->FillPolygon(pen->Brush, r);
}






;
NAMESPACE_END
;



