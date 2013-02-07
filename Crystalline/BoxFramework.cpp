
/*****************************************************************************
 *                                                                           *
 *  BoxFramework.cpp                                                         *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that keeps track of boxes for fast and efficient searching.      *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "BoxFramework.h"
#include "Box.h"
#include "Element.h"



namespace MetaphysicsIndustries { namespace Crystalline { 
;



BoxFramework::BoxFramework(void)
{
	this->_set = gcnew Set<IBox^>();

	this->_sorterleft = gcnew BoxComparer(true, false);
	this->_sorterup = gcnew BoxComparer(true, true);
	this->_sorterright = gcnew BoxComparer(false, false);
	this->_sorterdown = gcnew BoxComparer(false, true);

	this->_left = gcnew List<IBox^>();
	this->_right = gcnew List<IBox^>();
	this->_up = gcnew List<IBox^>();
	this->_down = gcnew List<IBox^>();

	this->_roleft = gcnew BoxList(this->_left);
	this->_roright = gcnew BoxList(this->_right);
	this->_roup = gcnew BoxList(this->_up);
	this->_rodown = gcnew BoxList(this->_down);

	this->_intervalsorter = gcnew IntervalComparer(true);
}

BoxFramework::~BoxFramework(void)
{
	this->Clear();


	delete this->_set;
	this->_set = nullptr;

}

BoxList^ BoxFramework::Left::get(void)
{
	return this->_roleft;
}
BoxList^ BoxFramework::Right::get(void)
{
	return this->_roright;
}
BoxList^ BoxFramework::Up::get(void)
{
	return this->_roup;
}
BoxList^ BoxFramework::Down::get(void)
{
	return this->_rodown;
}

void BoxFramework::Add(IBox^ b)
{
	if (!this->Contains(b))
	{
		this->_set->Add(b);
		b->Framework = this;

		int	i;

		i = this->_left->BinarySearch(b, this->_sorterleft);
		if (i < 0) { i = ~i; }
		this->_left->Insert(i, b);

		i = this->_right->BinarySearch(b, this->_sorterright);
		if (i < 0) { i = ~i; }
		this->_right->Insert(i, b);

		i = this->_up->BinarySearch(b, this->_sorterup);
		if (i < 0) { i = ~i; }
		this->_up->Insert(i, b);

		i = this->_down->BinarySearch(b, this->_sorterdown);
		if (i < 0) { i = ~i; }
		this->_down->Insert(i, b);

		Set<IBox^>^	neighbors;

		neighbors = this->GetNeighborsLeftward(b->Left, b->Top, b->Bottom);
		for each (IBox^ ib in neighbors) { b->LeftNeighbors->Add(ib); }

		neighbors = this->GetNeighborsRightward(b->Right, b->Top, b->Bottom);
		for each (IBox^ ib in neighbors) { b->RightNeighbors->Add(ib); }

		neighbors = this->GetNeighborsAbove(b->Top, b->Left, b->Right);
		for each (IBox^ ib in neighbors) { b->UpNeighbors->Add(ib); }

		neighbors = this->GetNeighborsBelow(b->Bottom, b->Left, b->Right);
		for each (IBox^ ib in neighbors) { b->DownNeighbors->Add(ib); }

		//attach delegate !@#$
		//b->RectChanged += gcnew EventHandler(this, &BoxFramework::Box_RectChanged);
	}
}

bool BoxFramework::Remove(IBox^ b)
{
	if (this->Contains(b))
	{
		//detach delegate !@#$
		//b->RectChanged -= gcnew EventHandler(this, &BoxFramework::Box_RectChanged);

		this->_left->Remove(b);
		this->_right->Remove(b);
		this->_up->Remove(b);
		this->_down->Remove(b);

		bool	ret;
		ret = this->_set->Remove(b);
		b->Framework = nullptr;
		return ret;
	}

	return false;
}

bool BoxFramework::Contains(IBox^ b)
{
	return this->_set->Contains(b);
}

void BoxFramework::Clear(void)
{
	array<IBox^>^	r;
	r = gcnew array<IBox^>(this->Count);
	this->CopyTo(r, 0);
	for each (IBox^ b in r)
	{
		this->Remove(b);
	}

	this->_set->Clear();
	this->_left->Clear();
	this->_right->Clear();
	this->_up->Clear();
	this->_down->Clear();
}

void BoxFramework::CopyTo(array<IBox^>^ r, int i)
{
	return this->_set->CopyTo(r, i);
}

IEnumerator<IBox^>^ BoxFramework::GetEnumerator(void)
{
	return this->_set->GetEnumerator();
}

int BoxFramework::Count::get(void)
{
	return this->_set->Count;
}

System::Collections::IEnumerator^ BoxFramework::GetEnumerator2(void)
{
	return this->GetEnumerator();
}

bool BoxFramework::IsReadOnly::get(void)
{
	return false;
}

void BoxFramework::Box_RectChanged(Object^, EventArgs^)
{
	Debug::Fail(__WCODESIG__);
}

Set<IBox^>^ BoxFramework::GetNeighborsAbove(float bottombound, float leftbound, float rightbound)
{
	return this->GetNeighbors(BoxOrientation::Up, bottombound, leftbound, rightbound);
}

Set<IBox^>^ BoxFramework::GetNeighborsBelow(float topbound, float leftbound, float rightbound)
{
	return this->GetNeighbors(BoxOrientation::Down, topbound, leftbound, rightbound);
}

Set<IBox^>^ BoxFramework::GetNeighborsLeftward(float rightbound, float topbound, float bottombound)
{
	return this->GetNeighbors(BoxOrientation::Left, rightbound, topbound, bottombound);
}

Set<IBox^>^ BoxFramework::GetNeighborsRightward(float leftbound, float topbound, float bottombound)
{
	return this->GetNeighbors(BoxOrientation::Right, leftbound, topbound, bottombound);
}

Set<IBox^>^ BoxFramework::GetNeighbors(BoxOrientation orientation, float mainbound, float crossmin, float crossmax)
{
	List<IBox^>^	list;
	BoxComparer^	comp;
	Set<IBox^>^		boxes;
	int				i;
	IBox^			bb = gcnew Box();
	bool			reverse = false;
	List<Interval>^	intervals;

	//have to rethink this
	//this->_left ISN'T sorted right-to-left, as would be useful 
	//for the current form of the algorithm...

	//throw gcnew NotImplementedException(__WCODESIG__);

	if (orientation == BoxOrientation::Left)
	{
		list = (gcnew List<IBox^>(this->_right));
		//list->Reverse();
		comp = this->_sorterright;
		bb->X = mainbound - 1;
		bb->Width = 1;
		reverse = true;
	}
	else if (orientation == BoxOrientation::Right)
	{
		list = this->_left;
		comp = this->_sorterleft;
		bb->X = mainbound;
	}
	else if (orientation == BoxOrientation::Up)
	{
		list = (gcnew List<IBox^>(this->_down));
		//list->Reverse();
		comp = this->_sorterdown;
		bb->Y = mainbound - 1;
		bb->Height = 1;
		reverse = true;
	}
	else if (orientation == BoxOrientation::Down)
	{
		list = this->_up;
		comp = this->_sorterup;
		bb->Y = mainbound;
	}
	else
	{
		throw gcnew ArgumentException("invalid orientation" + __WCODESIG__);
	}

	i = list->BinarySearch(bb, comp);
	if (i < 0) { i = ~i; }
	//if (!reverse)
	//{
	//	i--;
	//	if (i < 0) { i = 0; }
	//}
	if (reverse)
	{
		i = list->Count - i;
		list = gcnew List<IBox^>(list);		//this is extremely inefficient
		list->Reverse();					//need to either reverse forloopvar direction
											//or make a ListReverer<T> class
	}

	List<String^>^	names;
	names = gcnew List<String^>(list->Count);
	for each (IBox^ ib in list)
	{
		if (dynamic_cast<Element^>(ib))
		{
			names->Add(((Element^)(ib))->Param->ToString());
		}
		else
		{
			names->Add(nullptr);
		}
	}

	intervals = gcnew List<Interval>();		//maybe reuse this and clear it each time the method is called?

	boxes = gcnew Set<IBox^>();

	int end  = ( reverse ? 0 : list->Count );
	int step = ( reverse ? -1 : 1 );
	for (i = i; i < list->Count; i++)//i != end; i += step)
	{
		bb = list[i];

		bool	within;

		if (orientation == BoxOrientation::Left || 
			orientation == BoxOrientation::Right)
		{
			within = (bb->Top <= crossmax && bb->Bottom >= crossmin);
		}
		else// if (orientation == BoxOrientation::Up || 
			//	 orientation == BoxOrientation::Down)
		{
			within = (bb->Left <= crossmax && bb->Right >= crossmin);
		}

		if (within)
		{
			Interval	ii;
			Interval	current;
			int			k;

			if (orientation == BoxOrientation::Left || 
				orientation == BoxOrientation::Right)
			{
				ii = Interval(bb->Top, bb->Bottom);
			}
			else// if (orientation == BoxOrientation::Up || 
				//	 orientation == BoxOrientation::Down)
			{
				ii = Interval(bb->Left, bb->Right);
			}

			if (intervals->Count > 0)
			{
				k = intervals->BinarySearch(ii, this->_intervalsorter);
				if (k < 0) { k = ~k; }
				k--;
				if (k < 0) { k = 0; }

				current = intervals[k];
				if (current.Min < ii.Min && current.Max > ii.Max)
				{
					//box is obscured. go to the next one
					continue;
				}
				while (current.Intersects(ii) && intervals->Count > k)	//cascade-merge
				{
					intervals->RemoveAt(k);
					ii = Interval::Merge(ii, current);
					if (intervals->Count < 1)
					{
						break;
					}
					current = intervals[k];	//why should this go out of bounds?
				}
				intervals->Insert(k, ii);
			}
			else
			{
				intervals->Add(ii);
			}

			boxes->Add(bb);

			if (intervals->Count == 1 && intervals[0].Min <= crossmin && intervals[0].Max >= crossmax)
			{
				//region is obscured, as are all further boxes.
				//return
				break;
			}
		}
	}

	return boxes;
}

void BoxFramework::Move(IBox^ boxToMove, PointF newLocation)
{
	if (!boxToMove) { throw gcnew ArgumentNullException("boxToMove" + __WCODESIG__); }

	float					deltax = newLocation.X - boxToMove->X;
	float					deltay = newLocation.Y - boxToMove->Y;
	float					delta2;
	PointF				p;
	//List<
	Set<IBox^>^			newneighbors1;
	Set<IBox^>^			newneighbors2;
	Set<IBox^>^			removeneighbors;
	ICollection<IBox^>^	moveneighbors;

	Object^				param;

	if (dynamic_cast<Element^>(boxToMove))
	{
		param = ((Element^)(boxToMove))->Param;
	}
	else
	{
		param = boxToMove->ToString();
	}

	if (deltax)// > 0)
	{
		if (deltax > 0)
		{
			moveneighbors = boxToMove->RightNeighbors;
		}
		else
		{
			moveneighbors = boxToMove->LeftNeighbors;
		}
		for each (IBox^ ib in moveneighbors)
		{
			if (deltax > 0)
			{
				delta2 = boxToMove->Right + deltax - ib->Left;		//buffer space?
			}
			else
			{
				delta2 = ib->Right - boxToMove->Left - deltax;		//buffer space?
			}
			if (delta2 > 0)	
			{
				p = ib->Location;
				if (deltax > 0)
				{
					p.X += delta2;
				}
				else
				{
					p.X -= delta2;
				}
				ib->Move(p);
			}
		}
		if (deltax > boxToMove->Width || -deltax > boxToMove->Width)
		{
			//no overlap, discard up/down neighbors 
			boxToMove->UpNeighbors->Clear();
			boxToMove->DownNeighbors->Clear();
		}
		else
		{
			//overlap, discard some neighbors 

			//note: we don't care about whether or not they're obscured,
			//so we can just use simple bounds check instead of 
			//BoxFramework::GetNeighborsXXX

			removeneighbors = gcnew Set<IBox^>();		//maybe reuse boxToMove object
			for each (IBox^ ib in boxToMove->UpNeighbors)
			{
				if (deltax > 0)
				{
					if (ib->Right < boxToMove->Left + deltax)
					{
						removeneighbors->Add(ib);
					}
				}
				else
				{
					if (ib->Left > boxToMove->Right + deltax)
					{
						removeneighbors->Add(ib);
					}
				}
			}
			for each (IBox^ ib in removeneighbors)
			{
				boxToMove->UpNeighbors->Remove(ib);
			}
			removeneighbors->Clear();
			for each (IBox^ ib in boxToMove->DownNeighbors)
			{
				if (deltax > 0)
				{
					if (ib->Right < boxToMove->Left + deltax)
					{
						removeneighbors->Add(ib);
					}
				}
				else
				{
					if (ib->Left > boxToMove->Right + deltax)
					{
						removeneighbors->Add(ib);
					}
				}
			}
			for each (IBox^ ib in removeneighbors)
			{
				boxToMove->DownNeighbors->Remove(ib);
			}
		}

		if (deltax > 0)
		{
			newneighbors1 = boxToMove->Framework->GetNeighborsAbove(boxToMove->Top, Math::Max(boxToMove->Right, boxToMove->Left + deltax), boxToMove->Right + deltax);
			newneighbors2 = boxToMove->Framework->GetNeighborsBelow(boxToMove->Bottom, Math::Max(boxToMove->Right, boxToMove->Left + deltax), boxToMove->Right + deltax);
		}
		else
		{
			newneighbors1 = boxToMove->Framework->GetNeighborsAbove(boxToMove->Top, boxToMove->Left + deltax, Math::Min(boxToMove->Left, boxToMove->Right + deltax));
			newneighbors2 = boxToMove->Framework->GetNeighborsBelow(boxToMove->Bottom, boxToMove->Left + deltax, Math::Min(boxToMove->Left, boxToMove->Right + deltax));
		}
		boxToMove->X += deltax;
		for each (IBox^ ib in newneighbors1)
		{
			boxToMove->UpNeighbors->Add(ib);
		}
		for each (IBox^ ib in newneighbors2)
		{
			boxToMove->DownNeighbors->Add(ib);
		}
	}
	//else if (deltax < 0)
	//{
	//	//move left
	//}

	if (deltay)// > 0)
	{
		if (deltay > 0)
		{
			moveneighbors = boxToMove->DownNeighbors;
		}
		else
		{
			moveneighbors = boxToMove->UpNeighbors;
		}
		for each (IBox^ ib in moveneighbors)
		{
			if (deltay > 0)
			{
				delta2 = boxToMove->Bottom + deltay - ib->Top;		//buffer space?
			}
			else
			{
				delta2 = ib->Bottom - boxToMove->Top - deltay;		//buffer space?
			}
			if (delta2 > 0)	
			{
				p = ib->Location;
				if (deltay > 0)
				{
					p.Y += delta2;
				}
				else
				{
					p.Y -= delta2;
				}
				ib->Move(p);
			}
		}
		if (deltay > boxToMove->Height|| -deltay > boxToMove->Height)
		{
			//no overlap, discard left/right neighbors 
			boxToMove->LeftNeighbors->Clear();
			boxToMove->RightNeighbors->Clear();
		}
		else
		{
			//overlap, discard some neighbors 

			//note: we don't care about whether or not they're obscured,
			//so we can just use simple bounds check instead of 
			//BoxFramework::GetNeighborsXXX

			removeneighbors = gcnew Set<IBox^>();		//maybe reuse boxToMove object
			for each (IBox^ ib in boxToMove->LeftNeighbors)
			{
				if (deltay > 0)
				{
					if (ib->Bottom < boxToMove->Top + deltay)
					{
						removeneighbors->Add(ib);
					}
				}
				else
				{
					if (ib->Top > boxToMove->Bottom + deltay)
					{
						removeneighbors->Add(ib);
					}
				}
			}
			for each (IBox^ ib in removeneighbors)
			{
				boxToMove->LeftNeighbors->Remove(ib);
			}
			removeneighbors->Clear();
			for each (IBox^ ib in boxToMove->RightNeighbors)
			{
				if (deltay > 0)
				{
					if (ib->Bottom < boxToMove->Top + deltay)
					{
						removeneighbors->Add(ib);
					}
				}
				else
				{
					if (ib->Top > boxToMove->Bottom + deltay)
					{
						removeneighbors->Add(ib);
					}
				}
			}
			for each (IBox^ ib in removeneighbors)
			{
				boxToMove->RightNeighbors->Remove(ib);
			}
		}

		if (deltay > 0)
		{
			newneighbors1 = boxToMove->Framework->GetNeighborsLeftward(boxToMove->Left, Math::Max(boxToMove->Bottom, boxToMove->Top + deltay), boxToMove->Bottom + deltay);
			newneighbors2 = boxToMove->Framework->GetNeighborsRightward(boxToMove->Right, Math::Max(boxToMove->Bottom, boxToMove->Top + deltay), boxToMove->Bottom + deltay);
		}
		else
		{
			newneighbors1 = boxToMove->Framework->GetNeighborsLeftward(boxToMove->Left, boxToMove->Top + deltay, Math::Min(boxToMove->Top, boxToMove->Bottom + deltay));
			newneighbors2 = boxToMove->Framework->GetNeighborsRightward(boxToMove->Right, boxToMove->Top + deltay, Math::Min(boxToMove->Top, boxToMove->Bottom + deltay));
		}
		boxToMove->Y += deltay;
		for each (IBox^ ib in newneighbors1)
		{
			boxToMove->LeftNeighbors->Add(ib);
		}
		for each (IBox^ ib in newneighbors2)
		{
			boxToMove->RightNeighbors->Add(ib);
		}
	}
	//else if (deltay < 0)
	//{
	//	//move up
	//}

	//we can use BinarySearch on the new position coordinates in conjunction
	//with Sort(int, int, IComparer<T>) to more efficiently sort these lists.
	//or we could store info about the boxes' positions in a better data
	//structure (BSP?)

	this->_left->Sort(this->_sorterleft);
	this->_right->Sort(this->_sorterright);
	this->_up->Sort(this->_sorterup);
	this->_down->Sort(this->_sorterdown);
}

} } 


