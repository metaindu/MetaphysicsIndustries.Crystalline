
/*****************************************************************************
 *                                                                           *
 *  UndoEngine.cs                                                            *
 *  16 April 2010                                                            *
 *  Project: MetaphysicsIndustries.Crystalline                               *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2010 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A class that manages the list of actions that can be undone and          *
 *    redone. Taken from the PaperDollAnimator project.                      *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    public class UndoEngine
    {
        public UndoEngine()
        {
            undostack = new Stack<UndoEvent>();
            redostack = new Stack<UndoEvent>();
        }

        public void Dispose()
        {
            undostack.Clear();
            redostack.Clear();
            //undostack.Dispose();
            //redostack.Dispose();
        }

        public void Redo()
        {
            if (CanRedo)
            {
                UndoEvent e;
                e = redostack.Pop();
                e.Redo();
                undostack.Push(e);
            }
        }

        public void Undo()
        {
            if (CanUndo)
            {
                UndoEvent e;
                e = undostack.Pop();
                e.Undo();
                redostack.Push(e);
            }
        }

        public void Add(UndoEvent e)
        {
            undostack.Push(e);
            redostack.Clear();
        }

        public void Clear()
        {
            undostack.Clear();
            redostack.Clear();
        }

        public bool CanUndo
        {
            get
            {
                return (undostack.Count > 0);
            }
        }

        public bool CanRedo
        {
            get
            {
                return (redostack.Count > 0);
            }
        }

        public string RedoText
        {
            get
            {
                if (CanRedo)
                {
                    return redostack.Peek().Text;
                }
                return null;
            }
        }

        public string UndoText
        {
            get
            {
                if (CanUndo)
                {
                    return undostack.Peek().Text;
                }
                return null;
            }
        }

        Stack<UndoEvent> undostack;
        Stack<UndoEvent> redostack;
    }
}
