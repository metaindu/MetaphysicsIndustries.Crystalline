
/*****************************************************************************
 *                                                                           *
 *  BoxOrientation.h                                               *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright � 2007 Metaphysics Industries                                  *
 *                                                                           *
 *  A set of values that indicate which of a box's sets of neighbors a       *
 *    BoxNeighborCollection governs.                                                 *
 *                                                                           *
 *****************************************************************************/



#pragma once

using namespace System;



namespace MetaphysicsIndustries { namespace Crystalline { 
;



public enum class BoxOrientation
{
	Left,
	Right,
	Up,
	Down,
};



} } 


