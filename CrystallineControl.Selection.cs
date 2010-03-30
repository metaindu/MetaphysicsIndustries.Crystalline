
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Selection.cs                                          *
 *  26 January 2008                                                          *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that manages the selecting of items.         *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MetaphysicsIndustries.Collections;
using System.Diagnostics;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineControl : UserControl
    {
        

        public int SelectionEntitiesCount
        {
            get
            {
                return Selection.Count;
            }
        }


        Set<Entity> _selectionElement = new Set<Entity>();
        public Set<Entity> Selection
        {
            get { return _selectionElement; }
        }
        public Element[] SelectionElement
        {
            get { return Selection.Extract<Element>(); }
        }


    }
}
