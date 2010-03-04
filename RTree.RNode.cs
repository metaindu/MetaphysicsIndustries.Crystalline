using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Collections;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class RTree
    {
        protected class RNode
        {
            private RectangleV _rect;
            public virtual RectangleV Rect
            {
                get { return _rect; }
                set { _rect = value; }
            }

            //public RNode()
            //{
            //    _rNodes = new RNodeRNodeParentChildrenCollection(this);
            //}

            ////private RNodeRNodeParentChildrenCollection _rNodes;
            ////public RNodeRNodeParentChildrenCollection RNodes
            //{
            //    get { return _rNodes; }
            //}

            //private RNode _parentRNode;
            //public RNode ParentRNode
            //{
            //    get { return _parentRNode; }
            //    set
            //    {
            //        if (value != _parentRNode)
            //        {
            //            if (_parentRNode != null)
            //            {
            //                _parentRNode.RNodes.Remove(this);
            //            }

            //            _parentRNode = value;

            //            if (_parentRNode != null)
            //            {
            //                _parentRNode.RNodes.Add(this);
            //            }
            //        }
            //    }
            //}

            public static int ChildCount = 4;
            //public RNode[] Children = new RNode[RNode.ChildCount];

            public virtual bool IsLeafNode
            {
                get { return false; }
            }
            public virtual bool IsLeafContainer
            {
                get
                {
                    throw new NotImplementedException();
                    //return RNodes.Count > 0 && Collection.GetFirst<RNode>(RNodes).IsLeafNode;
                }
            }
        }

        protected class EntityRNodeAdapter : RNode
        {
            public EntityRNodeAdapter(Entity entity)
            {
                if (entity == null) { throw new ArgumentNullException("entity"); }
                _entity = entity;
            }

            private Entity _entity;
            public override RectangleV Rect
            {
                get { return _entity.GetBoundingBox(); }
                set { }
            }

            public override bool IsLeafNode
            {
                get { return true; }
            }
            public override bool IsLeafContainer
            {
                get { return false; }
            }
        }

        //protected class RNodeRNodeParentChildrenCollection : ICollection<RNode>, IDisposable
        //{
        //    public RNodeRNodeParentChildrenCollection(RNode container)
        //    {
        //        _container = container;
        //    }

        //    public virtual void Dispose()
        //    {
        //        Clear();
        //    }

        //    public void AddRange(params RNode[] items)
        //    {
        //        AddRange((IEnumerable<RNode>)items);
        //    }
        //    public void AddRange(IEnumerable<RNode> items)
        //    {
        //        foreach (RNode item in items)
        //        {
        //            Add(item);
        //        }
        //    }
        //    public void RemoveRange(params RNode[] items)
        //    {
        //        RemoveRange((IEnumerable<RNode>)items);
        //    }
        //    public void RemoveRange(IEnumerable<RNode> items)
        //    {
        //        foreach (RNode item in items)
        //        {
        //            Remove(item);
        //        }
        //    }

        //    //ICollection<RNode>
        //    public virtual void Add(RNode item)
        //    {
        //        if (!Contains(item))
        //        {
        //            _set.Add(item);
        //            item.ParentRNode = _container;

        //            //if (item.IsLeafNode)
        //            //{
        //            //    _container.IsLeafContainer = true;
        //            //}
        //        }
        //    }

        //    public virtual bool Contains(RNode item)
        //    {
        //        return _set.Contains(item);
        //    }

        //    public virtual bool Remove(RNode item)
        //    {
        //        if (Contains(item))
        //        {
        //            bool ret = _set.Remove(item);
        //            item.ParentRNode = null;

        //            //if (Count < 1)
        //            //{
        //            //    _container.IsLeafContainer = false;
        //            //}

        //            return ret;
        //        }

        //        return false;
        //    }

        //    public virtual void Clear()
        //    {
        //        RNode[] array = new RNode[Count];

        //        CopyTo(array, 0);

        //        foreach (RNode item in array)
        //        {
        //            Remove(item);
        //        }

        //        _set.Clear();
        //    }

        //    public virtual void CopyTo(RNode[] array, int arrayIndex)
        //    {
        //        _set.CopyTo(array, arrayIndex);
        //    }

        //    public virtual IEnumerator<RNode> GetEnumerator()
        //    {
        //        return _set.GetEnumerator();
        //    }

        //    //ICollection<RNode>
        //    public virtual int Count
        //    {
        //        get { return _set.Count; }
        //    }

        //    public virtual bool IsReadOnly
        //    {
        //        get { return (_set as ICollection<RNode>).IsReadOnly; }
        //    }

        //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //    {
        //        return GetEnumerator();
        //    }

        //    private RNode _container;
        //    private Set<RNode> _set = new Set<RNode>();
        //}

    }
}
