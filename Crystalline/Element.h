
/*****************************************************************************
 *                                                                           *
 *  Element.h                                                                *
 *  23 November 2006                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A class that represents the visual aspects of the elements which are     *
 *    connected together in the graph.                                       *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Path.h"
//#include "InboundToElementPathChildrenCollection.h"
//#include "OutboundFromElementPathChildrenCollection.h"
#include "Box.h"

using namespace System;
using namespace System::Drawing;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



ref class Path;
ref class InboundToElementPathChildrenCollection;
ref class OutboundFromElementPathChildrenCollection;
public ref class Element : Box
{

public:

	Element(void);
	virtual ~Element(void);

	void Render(Graphics^, Pen^, Font^);

	virtual property System::Object^ Param
	{
		System::Object^ get(void);
		void set(System::Object^);
	}

	//virtual property RectangleF Rect
	//{
	//	RectangleF get(void) override;
	//}
	//virtual property PointF Location
	//{
	//	PointF get(void) override;
	//	void set(PointF) override;
	//}
	//virtual property SizeF Size
	//{
	//	SizeF get(void) override;
	//	void set(SizeF) override;
	//}

	virtual property InboundToElementPathChildrenCollection^ Inbound
	{
		InboundToElementPathChildrenCollection^ get(void);
	}
	virtual property OutboundFromElementPathChildrenCollection^ Outbound
	{
		OutboundFromElementPathChildrenCollection^ get(void);
	}

	event EventHandler^	ParamChanging;
	event EventHandler^	ParamChanged;
	event EventHandler^	LocationChanging;
	event EventHandler^	LocationChanged;
	event EventHandler^	SizeChanging;
	event EventHandler^	SizeChanged;

	///// pathing and routing

	//virtual void SuggestAttachIncoming(PointF%, PointF%);
	//virtual void SuggestAttachOutgoing(PointF%, PointF%);

	virtual property float TerminalSpacing
	{
		float get(void);
	}

	//Box

protected:

	virtual void OnParamChanging(EventArgs^);
	virtual void OnParamChanged(EventArgs^);
	virtual void OnLocationChanging(EventArgs^);
	virtual void OnLocationChanged(EventArgs^);
	virtual void OnSizeChanging(EventArgs^);
	virtual void OnSizeChanged(EventArgs^);

	System::Object^								param;

	//PointF										location;
	//SizeF										size;

	InboundToElementPathChildrenCollection^		inbound;
	OutboundFromElementPathChildrenCollection^	outbound;

private:

};



} } 

