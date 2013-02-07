
/*****************************************************************************
 *                                                                           *
 *  Box.cpp                                                                  *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A default implementation of the IBox interface which uses                *
 *    BoxNeighborCollection to store links to neighbors. This class can be used      *
 *    as an automator.                                                       *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "Box.h"
#include "BoxNeighborCollection.h"
#include "BoxFramework.h"

#include "Element.h"



;
NAMESPACE_START
;



Box::Box(void)
{
	this->_left = gcnew BoxNeighborCollection(this, BoxOrientation::Left);
	this->_right = gcnew BoxNeighborCollection(this, BoxOrientation::Right);
	this->_up = gcnew BoxNeighborCollection(this, BoxOrientation::Up);
	this->_down = gcnew BoxNeighborCollection(this, BoxOrientation::Down);
}

Box::~Box(void)
{
	this->_left->Clear();
	delete this->_left;
	this->_left = nullptr;

	this->_right->Clear();
	delete this->_right;
	this->_right = nullptr;

	this->_up->Clear();
	delete this->_up;
	this->_up = nullptr;

	this->_down->Clear();
	delete this->_down;
	this->_down = nullptr;
}

void Box::Move(PointF newlocation)
{
	//maybe route this to the framework
	//
	//this->Framework->MoveBox(this, newlocation);
	//
	//that would be more efficient in terms of moving around
	//BUT!!! would it prevent us from customizing movement for
	//different kinds of boxes, e.g. Pathway moves/resizes with
	//PathingJunction?

	Box::movecallcount++;
	if (Box::movecallcount > Box::movecallcountmax)
	{
		Box::movecallcount = Box::movecallcount;
	}


	//float					deltax = newlocation.X - this->X;
	//float					deltay = newlocation.Y - this->Y;
	//float					delta2;
	//PointF				p;
	////List<
	//Set<IBox^>^			newneighbors1;
	//Set<IBox^>^			newneighbors2;
	//Set<IBox^>^			removeneighbors;
	//ICollection<IBox^>^	moveneighbors;

	//Object^				param;

	//param = ((Element^)(this))->Param;

	//if (deltax)// > 0)
	//{
	//	if (deltax > 0)
	//	{
	//		moveneighbors = this->RightNeighbors;
	//	}
	//	else
	//	{
	//		moveneighbors = this->LeftNeighbors;
	//	}
	//	for each (IBox^ ib in moveneighbors)
	//	{
	//		if (deltax > 0)
	//		{
	//			delta2 = this->Right + deltax - ib->Left;		//buffer space?
	//		}
	//		else
	//		{
	//			delta2 = ib->Right - this->Left - deltax;		//buffer space?
	//		}
	//		if (delta2 > 0)	
	//		{
	//			p = ib->Location;
	//			if (deltax > 0)
	//			{
	//				p.X += delta2;
	//			}
	//			else
	//			{
	//				p.X -= delta2;
	//			}
	//			ib->Move(p);
	//		}
	//	}
	//	if (deltax > this->Width || -deltax > this->Width)
	//	{
	//		//no overlap, discard up/down neighbors 
	//		this->UpNeighbors->Clear();
	//		this->DownNeighbors->Clear();
	//	}
	//	else
	//	{
	//		//overlap, discard some neighbors 

	//		//note: we don't care about whether or not they're obscured,
	//		//so we can just use simple bounds check instead of 
	//		//BoxFramework::GetNeighborsXXX

	//		removeneighbors = gcnew Set<IBox^>();		//maybe reuse this object
	//		for each (IBox^ ib in this->UpNeighbors)
	//		{
	//			if (deltax > 0)
	//			{
	//				if (ib->Right < this->Left + deltax)
	//				{
	//					removeneighbors->Add(ib);
	//				}
	//			}
	//			else
	//			{
	//				if (ib->Left > this->Right + deltax)
	//				{
	//					removeneighbors->Add(ib);
	//				}
	//			}
	//		}
	//		for each (IBox^ ib in removeneighbors)
	//		{
	//			this->UpNeighbors->Remove(ib);
	//		}
	//		removeneighbors->Clear();
	//		for each (IBox^ ib in this->DownNeighbors)
	//		{
	//			if (deltax > 0)
	//			{
	//				if (ib->Right < this->Left + deltax)
	//				{
	//					removeneighbors->Add(ib);
	//				}
	//			}
	//			else
	//			{
	//				if (ib->Left > this->Right + deltax)
	//				{
	//					removeneighbors->Add(ib);
	//				}
	//			}
	//		}
	//		for each (IBox^ ib in removeneighbors)
	//		{
	//			this->DownNeighbors->Remove(ib);
	//		}
	//	}

	//	if (deltax > 0)
	//	{
	//		newneighbors1 = this->Framework->GetNeighborsAbove(this->Top, Math::Max(this->Right, this->Left + deltax), this->Right + deltax);
	//		newneighbors2 = this->Framework->GetNeighborsBelow(this->Bottom, Math::Max(this->Right, this->Left + deltax), this->Right + deltax);
	//	}
	//	else
	//	{
	//		newneighbors1 = this->Framework->GetNeighborsAbove(this->Top, this->Left + deltax, Math::Min(this->Left, this->Right + deltax));
	//		newneighbors2 = this->Framework->GetNeighborsBelow(this->Bottom, this->Left + deltax, Math::Min(this->Left, this->Right + deltax));
	//	}
	//	this->X += deltax;
	//	for each (IBox^ ib in newneighbors1)
	//	{
	//		this->UpNeighbors->Add(ib);
	//	}
	//	for each (IBox^ ib in newneighbors2)
	//	{
	//		this->DownNeighbors->Add(ib);
	//	}
	//}
	////else if (deltax < 0)
	////{
	////	//move left
	////}

	//if (deltay)// > 0)
	//{
	//	if (deltay > 0)
	//	{
	//		moveneighbors = this->DownNeighbors;
	//	}
	//	else
	//	{
	//		moveneighbors = this->UpNeighbors;
	//	}
	//	for each (IBox^ ib in moveneighbors)
	//	{
	//		if (deltay > 0)
	//		{
	//			delta2 = this->Bottom + deltay - ib->Top;		//buffer space?
	//		}
	//		else
	//		{
	//			delta2 = ib->Bottom - this->Top - deltay;		//buffer space?
	//		}
	//		if (delta2 > 0)	
	//		{
	//			p = ib->Location;
	//			if (deltay > 0)
	//			{
	//				p.Y += delta2;
	//			}
	//			else
	//			{
	//				p.Y -= delta2;
	//			}
	//			ib->Move(p);
	//		}
	//	}
	//	if (deltay > this->Height|| -deltay > this->Height)
	//	{
	//		//no overlap, discard left/right neighbors 
	//		this->LeftNeighbors->Clear();
	//		this->RightNeighbors->Clear();
	//	}
	//	else
	//	{
	//		//overlap, discard some neighbors 

	//		//note: we don't care about whether or not they're obscured,
	//		//so we can just use simple bounds check instead of 
	//		//BoxFramework::GetNeighborsXXX

	//		removeneighbors = gcnew Set<IBox^>();		//maybe reuse this object
	//		for each (IBox^ ib in this->LeftNeighbors)
	//		{
	//			if (deltay > 0)
	//			{
	//				if (ib->Bottom < this->Top + deltay)
	//				{
	//					removeneighbors->Add(ib);
	//				}
	//			}
	//			else
	//			{
	//				if (ib->Top > this->Bottom + deltay)
	//				{
	//					removeneighbors->Add(ib);
	//				}
	//			}
	//		}
	//		for each (IBox^ ib in removeneighbors)
	//		{
	//			this->LeftNeighbors->Remove(ib);
	//		}
	//		removeneighbors->Clear();
	//		for each (IBox^ ib in this->RightNeighbors)
	//		{
	//			if (deltay > 0)
	//			{
	//				if (ib->Bottom < this->Top + deltay)
	//				{
	//					removeneighbors->Add(ib);
	//				}
	//			}
	//			else
	//			{
	//				if (ib->Top > this->Bottom + deltay)
	//				{
	//					removeneighbors->Add(ib);
	//				}
	//			}
	//		}
	//		for each (IBox^ ib in removeneighbors)
	//		{
	//			this->RightNeighbors->Remove(ib);
	//		}
	//	}

	//	if (deltay > 0)
	//	{
	//		newneighbors1 = this->Framework->GetNeighborsLeftward(this->Left, Math::Max(this->Bottom, this->Top + deltay), this->Bottom + deltay);
	//		newneighbors2 = this->Framework->GetNeighborsRightward(this->Right, Math::Max(this->Bottom, this->Top + deltay), this->Bottom + deltay);
	//	}
	//	else
	//	{
	//		newneighbors1 = this->Framework->GetNeighborsLeftward(this->Left, this->Top + deltay, Math::Min(this->Top, this->Bottom + deltay));
	//		newneighbors2 = this->Framework->GetNeighborsRightward(this->Right, this->Top + deltay, Math::Min(this->Top, this->Bottom + deltay));
	//	}
	//	this->Y += deltay;
	//	for each (IBox^ ib in newneighbors1)
	//	{
	//		this->LeftNeighbors->Add(ib);
	//	}
	//	for each (IBox^ ib in newneighbors2)
	//	{
	//		this->RightNeighbors->Add(ib);
	//	}
	//}
	////else if (deltay < 0)
	////{
	////	//move up
	////}
this->Framework->Move(this, newlocation);

	Box::movecallcount--;
}

void Box::Resize(SizeF newsize)
{
	throw gcnew NotImplementedException(__WCODESIG__);
}

BoxFramework^ Box::Framework::get(void)
{
	return this->_framework;
}

void Box::Framework::set(BoxFramework^ f)
{
	if (this->Framework != f)
	{
		if (this->Framework) { this->Framework->Remove(this); }
		this->_framework = f;
		if (this->Framework) { this->Framework->Add(this); }
	}
}

ICollection<IBox^>^ Box::LeftNeighbors::get(void)
{
	return this->_left;
}
ICollection<IBox^>^ Box::RightNeighbors::get(void)
{
	return this->_right;
}
ICollection<IBox^>^ Box::UpNeighbors::get(void)
{
	return this->_up;
}
ICollection<IBox^>^ Box::DownNeighbors::get(void)
{
	return this->_down;
}

SizeF Box::Size::get(void)
{
	return this->Rect.Size;
}
void Box::Size::set(SizeF s)
{
	this->Rect = RectangleF(this->Location, s);
}

PointF Box::Location::get(void)
{
	return this->Rect.Location;
}
void Box::Location::set(PointF p)
{
	this->Rect = RectangleF(p, this->Size);
}

float Box::X::get(void)
{
	return this->Rect.X;
}
void Box::X::set(float i)
{
	this->Rect = RectangleF(i, this->Y, this->Width, this->Height);
}

float Box::Y::get(void)
{
	return this->Rect.Y;
}
void Box::Y::set(float i)
{
	this->Rect = RectangleF(this->X, i, this->Width, this->Height);
}

float Box::Width::get(void)
{
	return this->Rect.Width;
}
void Box::Width::set(float i)
{
	this->Rect = RectangleF(this->X, this->Y, i, this->Height);
}

float Box::Height::get(void)
{
	return this->Rect.Height;
}
void Box::Height::set(float i)
{
	this->Rect = RectangleF(this->X, this->Y, this->Width, i);
}

RectangleF Box::Rect::get(void)
{
	return this->_rect;
}
void Box::Rect::set(RectangleF r)
{
	//Debug::WriteLine("Need to make Box::Rect::set to use Move() ?" + __WCODESIG__);
	//Debug::WriteLine("Definitely neew to make Box::Rect::set update neighbor collections" + __WCODESIG__);

	if (this->_rect != r)
	{
		this->OnRectChanging(gcnew EventArgs);

		this->_rect = r;

		this->OnRectChanged(gcnew EventArgs);
	}
}


void Box::OnRectChanging(EventArgs^ e)
{
	this->RectChanging(this, e);
}
void Box::OnRectChanged(EventArgs^ e)
{
	this->RectChanged(this, e);
}

float Box::Left::get(void)
{
	return this->Rect.Left;
}
float Box::Right::get(void)
{
	return this->Rect.Right;
}
float Box::Top::get(void)
{
	return this->Rect.Top;
}
float Box::Bottom::get(void)
{
	return this->Rect.Bottom;
}






;
NAMESPACE_END
;


