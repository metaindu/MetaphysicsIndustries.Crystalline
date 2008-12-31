
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
        public enum SelectionModeType
        {
            Element,
            Path,
            PathingJunction,
            Pathway,
        };

        public virtual SelectionModeType SelectionMode
        {
            get
            {
                return _selectionMode;
            }
            set
            {
                if (_selectionMode != value)
                {
                    _selectionMode = value;
                }
            }
        }

        public int SelectionEntitiesCount
        {
            get
            {
                switch (SelectionMode)
                {
                    case SelectionModeType.Element: return SelectionElement.Count;
                    case SelectionModeType.Path: return SelectionPathJoint.Count;
                    case SelectionModeType.PathingJunction: return SelectionPathingJunction.Count;
                    case SelectionModeType.Pathway: return SelectionPathway.Count;
                }
                return 0;
            }
        }


        Set<Element> _selectionElement = new Set<Element>();
        public Set<Element> SelectionElement
        {
            get { return _selectionElement; }
        }
        Set<PathJoint> _selectionPathJoint = new Set<PathJoint>();
        public Set<PathJoint> SelectionPathJoint
        {
            get { return _selectionPathJoint; }
        }
        Set<PathingJunction> _selectionPathingJunction = new Set<PathingJunction>();
        public Set<PathingJunction> SelectionPathingJunction
        {
            get { return _selectionPathingJunction; }
        }
        Set<Pathway> _selectionPathway = new Set<Pathway>();
        public Set<Pathway> SelectionPathway
        {
            get { return _selectionPathway; }
        }

        SelectionModeType _selectionMode = SelectionModeType.Element;
    }
}
