
/*****************************************************************************
 *                                                                           *
 *  SingleBoxNeighborCollection.cs                                                   *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A sub-class of BoxNeighborCollection that only holds a single box at a time.     *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    //public class SingleBoxNeighborCollection : BoxNeighborCollection
    //{
    //    public SingleBoxNeighborCollection(Box __parent, BoxOrientation __orientation)
    //        : base(__parent, __orientation)
    //    {
    //    }

    //    public override void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool Remove(Box ib)
    //    {
    //        if (ib == null) { throw new ArgumentNullException("ib"); }
    //        if (Current != ib) { return false; }
    //        Current = null;
    //        return true;
    //    }

    //    public override void Add(Box ib)
    //    {
    //        if (ib == null) { throw new ArgumentNullException("ib"); }
    //        if (Current != ib)
    //        {
    //            Current = ib;
    //        }
    //    }

    //    public virtual Box Current
    //    {
    //        get
    //        {
    //            return _current;
    //        }
    //        set
    //        {
    //            if (Current != value)
    //            {
    //                this.OnCurrentChanging(new EventArgs());
    //                if (Current != null) { base.Remove(Current); }
    //                _current = value;
    //                if (Current != null) { base.Add(Current); }
    //                this.OnCurrentChanged(new EventArgs());
    //            }
    //        }
    //    }

    //    public  event EventHandler CurrentChanging;

    //    public  event EventHandler CurrentChanged;

    //    protected virtual void OnCurrentChanging(EventArgs e)
    //    {
    //        if (CurrentChanging != null)
    //        {
    //            this.CurrentChanging(this, e);
    //        }
    //    }

    //    protected virtual void OnCurrentChanged(EventArgs e)
    //    {
    //        if (CurrentChanged != null)
    //        {
    //            this.CurrentChanged(this, e);
    //        }
    //    }

    //    private Box _current;
    //}
}
