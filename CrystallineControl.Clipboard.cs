
/*****************************************************************************
 *                                                                           *
 *  CrystallineControl.Clipboard.cs                                          *
 *  16 April 2010                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2010 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  The class that serves as the UI for all of Crystalline's functionality.  *
 *                                                                           *
 *  This file contains the code that cut/copy/paste operations.              *
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
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineControl : UserControl
    {
        List<Entity> _clipboard = new List<Entity>();

        void CopyEntitiesToClipboard(Entity[] entities)
        {
            Entity[] clones = CloneEntities(entities);

            _clipboard.Clear();
            _clipboard.AddRange(clones);
        }

        protected void PasteEntitiesAtLocation(Vector location)
        {
            Entity[] clones = CloneEntities(_clipboard);

            RectangleV rect = Entity.GetBoundingBoxFromEntities(clones);
            Vector center = rect.CalcCenter();
            Vector delta = location - center;

            foreach (Entity ent in clones)
            {
                if (ent is Box)
                {
                    ((Box)ent).Location += delta;
                }
            }

            foreach (Entity ent in clones)
            {
                AddEntity(ent);
            }
        }


    }
}
