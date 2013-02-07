
/*****************************************************************************
 *                                                                           *
 *  SingleBoxNeighborCollection.h                                                    *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A sub-class of BoxNeighborCollection that only holds a single box at a time.     *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "BoxNeighborCollection.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public ref class SingleBoxNeighborCollection : BoxNeighborCollection
{

public:

	SingleBoxNeighborCollection(IBox^ __parent, BoxOrientation __orientation);
	virtual ~SingleBoxNeighborCollection(void);

	virtual void Add(IBox^) override;
	virtual bool Remove(IBox^) override;

	virtual property IBox^ Current
	{
		IBox^ get(void);
		void set(IBox^);
	}

	event EventHandler^	CurrentChanging;
	event EventHandler^	CurrentChanged;

protected:

	virtual void OnCurrentChanging(EventArgs^);
	virtual void OnCurrentChanged(EventArgs^);

	IBox^	_current;

private:

};



} } 



