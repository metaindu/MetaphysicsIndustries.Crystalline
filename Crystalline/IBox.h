
/*****************************************************************************
 *                                                                           *
 *  IBox.h                                                                   *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A interface inherited by objects that are visually represented by        *
 *    rectangles and interact with each other when moved.                    *
 *                                                                           *
 *****************************************************************************/



#pragma once

using namespace System;
using namespace System::Drawing;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



ref class BoxFramework;
public interface class IBox
{
	virtual void Move(PointF newlocation);
	virtual void Resize(SizeF newsize);

	virtual property RectangleF Rect
	{
		RectangleF get(void);
		void set(RectangleF);
	}

	virtual property float Left
	{
		float get(void);
	}
	virtual property float Right
	{
		float get(void);
	}
	virtual property float Top
	{
		float get(void);
	}
	virtual property float Bottom
	{
		float get(void);
	}

	virtual property float X
	{
		float get(void);
		void set(float);
	}
	virtual property float Y
	{
		float get(void);
		void set(float);
	}
	virtual property float Width
	{
		float get(void);
		void set(float);
	}
	virtual property float Height
	{
		float get(void);
		void set(float);
	}

	virtual property PointF Location
	{
		PointF get(void);
		void set(PointF);
	}
	virtual property SizeF Size
	{
		SizeF get(void);
		void set(SizeF);
	}

	virtual property ICollection<IBox^>^ LeftNeighbors
	{
		ICollection<IBox^>^ get(void);
	}
	virtual property ICollection<IBox^>^ RightNeighbors
	{
		ICollection<IBox^>^ get(void);
	}
	virtual property ICollection<IBox^>^ UpNeighbors
	{
		ICollection<IBox^>^ get(void);
	}
	virtual property ICollection<IBox^>^ DownNeighbors
	{
		ICollection<IBox^>^ get(void);
	}

	virtual property BoxFramework^ Framework
	{
		BoxFramework^ get(void);
		void set(BoxFramework^);
	}

	//virtual event EventHandler^	RectChanging;		//System::ComponentModel::CancelEventHandler?
	//virtual event EventHandler^ RectChanged;
};



} } 



