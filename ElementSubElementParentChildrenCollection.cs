/*****************************************************************************
 *                                                                           *
 *  ElementSubElementParentChildrenCollection.cs                             *
 *  24 November 2007                                                         *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  An unordered collection of SubElement objects.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    //public class ElementSubElementParentChildrenCollection : ICollection<SubElement>, IDisposable
    //{
    //    public ElementSubElementParentChildrenCollection(Element parent)
    //    {
    //        _parent = parent;
    //    }

    //    public virtual void Dispose()
    //    {
    //        Clear();
    //        //this._set.Dispose();
    //        _set = null;
    //    }


    //    //ICollection<SubElement>
    //    public virtual void Add(SubElement item)
    //    {
    //        if (!Contains(item))
    //        {
    //            item.ParentElement = null;
    //            _set.Add(item);
    //            item.ParentElement = _parent;
    //        }
    //    }

    //    public virtual bool Contains(SubElement item)
    //    {
    //        return _set.Contains(item);
    //    }

    //    public virtual bool Remove(SubElement item)
    //    {
    //        if (Contains(item))
    //        {
    //            item.ParentElement = null;
    //            return _set.Remove(item);
    //        }

    //        return false;
    //    }

    //    public virtual void Clear()
    //    {
    //        SubElement[] r = new SubElement[Count];

    //        CopyTo(r, 0);

    //        foreach (SubElement item in r)
    //        {
    //            Remove(item);
    //        }

    //        _set.Clear();
    //    }

    //    public virtual void CopyTo(SubElement[] array, int arrayIndex)
    //    {
    //        _set.CopyTo(array, arrayIndex);
    //    }

    //    public virtual IEnumerator<SubElement> GetEnumerator()
    //    {
    //        return _set.GetEnumerator();
    //    }


    //    //ICollection<SubElement>
    //    public virtual int Count
    //    {
    //        get
    //        {
    //            return _set.Count;
    //        }
    //    }

    //    public virtual bool IsReadOnly
    //    {
    //        get
    //        {
    //            return (_set as ICollection<SubElement>).IsReadOnly;
    //        }
    //    }

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }


    //    private Element _parent;
    //    private Set<SubElement> _set = new Set<SubElement>();
    //}
}



