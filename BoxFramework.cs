
/*****************************************************************************
 *                                                                           *
 *  BoxFramework.cs                                                          *
 *  19 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that keeps track of boxes for fast and efficient searching.      *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Drawing;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    [Serializable]
    public class BoxFramework : ICollection<Box>, IDisposable
    {
        public BoxFramework(CrystallineControl parentControl)
        {
            _parentControl = parentControl;
            _set = new Set<Box>();
            _sorterLeft = new BoxComparer(true, false);
            _sorterUp = new BoxComparer(true, true);
            _sorterRight = new BoxComparer(false, false);
            _sorterDown = new BoxComparer(false, true);
            _left = new List<Box>();
            _right = new List<Box>();
            _up = new List<Box>();
            _down = new List<Box>();
            _roleft = new ReadOnlyList<Box>(_left);
            _roright = new ReadOnlyList<Box>(_right);
            _roup = new ReadOnlyList<Box>(_up);
            _rodown = new ReadOnlyList<Box>(_down);
            //_intervalSorter = new IntervalComparer(true);

            _roZOrder = new ReadOnlyList<Box>(_zOrder);
        }

        public virtual void Dispose()
        {
            this.Clear();
            //_set.Dispose();
            //_set = null;
        }

        public virtual void Add(Box b)
        {
            if (!this.Contains(b))
            {
                _set.Add(b);
                b.ParentCrystallineControl = this.ParentControl;

                int i;

                _zOrder.Add(b);

                i = _left.BinarySearch(b, _sorterLeft);
                if (i < 0) { i = ~i; }
                _left.Insert(i, b);

                i = _right.BinarySearch(b, _sorterRight);
                if (i < 0) { i = ~i; }
                _right.Insert(i, b);

                i = _up.BinarySearch(b, _sorterUp);
                if (i < 0) { i = ~i; }
                _up.Insert(i, b);

                i = _down.BinarySearch(b, _sorterDown);
                if (i < 0) { i = ~i; }
                _down.Insert(i, b);

                b.RectChanged += Box_RectChanged;

                UpdateBounds();
            }
        }

        [NonSerialized]
        private static int _moveCallCount = 0;
        public virtual void Move(Box boxToMove, Vector newLocation, Set<Box> collidedBoxes, Set<Box> doNotCollide)
        {
            if (boxToMove == null) { throw new ArgumentNullException("boxToMove"); }

            _moveCallCount++;
            if (_moveCallCount > 100)
            {
                _moveCallCount = 0;
                throw new InvalidOperationException("Call count to BoxFramework.Move(Box, PointF, Set<Box>) exceeds 100");
            }

            float deltax = newLocation.X - boxToMove.X;
            float deltay = newLocation.Y - boxToMove.Y;
            float delta2;
            Vector p;


            if (deltax != 0 && deltay != 0)
            {
            }


            if (deltax != 0)
            {
                Set<Box> moveneighbors=new Set<Box>();
                if (deltax > 0)
                {
                    RectangleV rect = RectangleV.FromLTRB(boxToMove.Right, boxToMove.Top, boxToMove.Right+deltax, boxToMove.Bottom);
                    Box[] temp = ParentControl.GetEntitiesIntersectingRectInDocument<Box>(rect,false);
                    if (temp.Length > 1)
                    {
                    }
                    foreach (Box b in temp)
                    {
                        if (b != boxToMove && b.Left >= boxToMove.Right)
                        {
                            moveneighbors.Add(b);
                        }
                    }
                    if (moveneighbors.Count > 0)
                    {
                    }
                }
                else
                {
                    RectangleV rect = RectangleV.FromLTRB(newLocation.X, boxToMove.Top, boxToMove.Left, boxToMove.Bottom);
                    Box[] temp = ParentControl.GetEntitiesIntersectingRectInDocument<Box>(rect, false);
                    foreach (Box b in temp)
                    {
                        if (b != boxToMove && b.Right <= boxToMove.Left)
                        {
                            moveneighbors.Add(b);
                        }
                    }
                }
                moveneighbors.RemoveRange(doNotCollide);
                foreach (Box ib in moveneighbors)
                {
                    if (ib is Element && ParentControl.Selection.Contains((Element)ib))
                    {
                        continue;
                    }

                    if (deltax > 0)
                    {
                        delta2 = boxToMove.Right + deltax - ib.Left;
                    }
                    else
                    {
                        delta2 = ib.Right - boxToMove.Left - deltax;
                    }
                    if (delta2 > 0)
                    {
                        p = ib.Location;
                        if (deltax > 0)
                        {
                            p = new Vector(p.X + delta2, p.Y);
                        }
                        else
                        {
                            p = new Vector(p.X - delta2, p.Y);
                        }
                        Move(ib, p, collidedBoxes, doNotCollide);
                        if (collidedBoxes != null)
                        {
                            collidedBoxes.Add(ib);
                        }
                    }
                }

                boxToMove.X += deltax;
            }




            if (deltay != 0)
            {
                Set<Box> moveneighbors = new Set<Box>();
                if (deltay > 0)
                {
                    RectangleV rect = RectangleV.FromLTRB(boxToMove.Left, boxToMove.Bottom, boxToMove.Right, boxToMove.Bottom+deltay);
                    Box[] temp = ParentControl.GetEntitiesIntersectingRectInDocument<Box>(rect, false);
                    foreach (Box b in temp)
                    {
                        if (b != boxToMove && b.Top >= boxToMove.Bottom)
                        {
                            moveneighbors.Add(b);
                        }
                    }
                }
                else
                {
                    RectangleV rect = RectangleV.FromLTRB(boxToMove.Left, newLocation.Y, boxToMove.Right, boxToMove.Top);
                    Box[] temp = ParentControl.GetEntitiesIntersectingRectInDocument<Box>(rect, false);
                    foreach (Box b in temp)
                    {
                        if (b != boxToMove && b.Bottom <= boxToMove.Top)
                        {
                            moveneighbors.Add(b);
                        }
                    }
                }
                moveneighbors.RemoveRange(doNotCollide);
                foreach (Box ib in moveneighbors)
                {
                    if (ib is Element && ParentControl.Selection.Contains((Element)ib))
                    {
                        continue;
                    }

                    if (deltay > 0)
                    {
                        delta2 = boxToMove.Bottom + deltay - ib.Top;
                    }
                    else
                    {
                        delta2 = ib.Bottom - boxToMove.Top - deltay;
                    }
                    if (delta2 > 0)
                    {
                        p = ib.Location;
                        if (deltay > 0)
                        {
                            p = new Vector(p.X, p.Y + delta2);
                        }
                        else
                        {
                            p = new Vector(p.X, p.Y - delta2);
                        }
                        Move(ib, p, collidedBoxes,doNotCollide);
                        if (collidedBoxes != null)
                        {
                            collidedBoxes.Add(ib);
                        }
                    }
                }

                boxToMove.Y += deltay;
            }


            //we can use BinarySearch on the new position coordinates in conjunction
            //with Sort(int, int, IComparer<T>) to more efficiently sort these lists.
            //or we could store info about the boxes' positions in a better data
            //structure (BSP?)


            _left.Sort(_sorterLeft);
            _right.Sort(_sorterRight);
            _up.Sort(_sorterUp);
            _down.Sort(_sorterDown);

            _moveCallCount--;
        }

        protected virtual void Resize(Box boxToResize, SizeV newSize)
        {
            Resize(boxToResize, newSize, null);
        }

        protected virtual void Resize(Box boxToResize, SizeV newSize, Set<Box> collidedBoxes)
        {
            throw new NotImplementedException();
        }

        public virtual void Clear()
        {
            Box[] r;
            r = new Box[Count];
            this.CopyTo(r, 0);
            foreach (Box b in r)
            {
                this.Remove(b);
            }
            _set.Clear();
            _left.Clear();
            _right.Clear();
            _up.Clear();
            _down.Clear();
            _zOrder.Clear();
        }

        public virtual bool Contains(Box b)
        {
            return _set.Contains(b);
        }

        public virtual IEnumerator<Box> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        public virtual void CopyTo(Box[] r, int i)
        {
            _set.CopyTo(r, i);
        }

        public virtual bool Remove(Box b)
        {
            if (this.Contains(b))
            {
                b.RectChanged -= Box_RectChanged;

                _left.Remove(b);
                _right.Remove(b);
                _up.Remove(b);
                _down.Remove(b);
                _zOrder.Remove(b);

                bool ret = _set.Remove(b);

                //b.ParentCrystallineControl = null;// Framework = null;

                UpdateBounds();

                return ret;
            }
            return false;
        }

        public void BringForward(Box box)
        {
            if (!this._set.Contains(box)) { throw new InvalidOperationException("The box is not within this framework."); }

            int index = _zOrder.IndexOf(box);
            if (index < _zOrder.Count - 1)
            {
                _zOrder.RemoveAt(index);
                _zOrder.Insert(index + 1, box);
            }
        }
        public void SendBackward(Box box)
        {
            if (!this._set.Contains(box)) { throw new InvalidOperationException("The box is not within this framework."); }

            int index = _zOrder.IndexOf(box);
            if (index > 0)
            {
                _zOrder.RemoveAt(index);
                _zOrder.Insert(index - 1, box);
            }

        }
        public void BringToFront(Box box)
        {
            if (!this._set.Contains(box)) { throw new InvalidOperationException("The box is not within this framework."); }

            int index = _zOrder.IndexOf(box);
            if (index < _zOrder.Count - 1)
            {
                _zOrder.RemoveAt(index);
                _zOrder.Insert(_zOrder.Count, box);
            }
        }
        public void SendToBack(Box box)
        {
            if (!this._set.Contains(box)) { throw new InvalidOperationException("The box is not within this framework."); }

            int index = _zOrder.IndexOf(box);
            if (index > 0)
            {
                _zOrder.RemoveAt(index);
                _zOrder.Insert(0, box);
            }
        }

        [NonSerialized]
        CrystallineControl _parentControl;
        public CrystallineControl ParentControl
        {
            get { return _parentControl; }
        }

        public virtual ReadOnlyList<Box> Right
        {
            get { return _roright; }
        }

        public virtual ReadOnlyList<Box> Up
        {
            get { return _roup; }
        }

        public virtual int Count
        {
            get { return _set.Count; }
        }

        public virtual ReadOnlyList<Box> Down
        {
            get { return _rodown; }
        }

        public virtual ReadOnlyList<Box> Left
        {
            get { return _roleft; }
        }

        public virtual ReadOnlyList<Box> ZOrder
        {
            get { return _roZOrder; }
        }

        [NonSerialized]
        private RectangleV _bounds;
        public RectangleV Bounds
        {
            get { return _bounds; }
            protected set
            {
                if (_bounds != value)
                {
                    _bounds = value;

                    OnBoundsChanged(new EventArgs());
                }
            }
        }

        public event EventHandler BoundsChanged;

        protected virtual void OnBoundsChanged(EventArgs e)
        {
            if (BoundsChanged != null)
            {
                BoundsChanged(this, e);
            }
        }

        protected virtual void UpdateBounds()
        {
            if (Left.Count > 0)
            {
                RectangleV r =
                    new RectangleV(
                            Left[0].Left,
                            Up[0].Top,
                            Right[Right.Count - 1].Right - Left[0].Left,
                            Down[Down.Count - 1].Bottom - Up[0].Top);

                Bounds = r;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void Box_RectChanged(object sender, EventArgs e)
        {
            UpdateBounds();
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        //bool ICollection<IBox>.IsReadOnly
        //{
        //    get { return IsReadOnly; }
        //}

        [NonSerialized]
        private ReadOnlyList<Box> _roleft;
        //[NonSerialized]
        //private IntervalComparer _intervalSorter;
        [NonSerialized]
        private ReadOnlyList<Box> _roup;
        [NonSerialized]
        private BoxComparer _sorterRight;
        [NonSerialized]
        private BoxComparer _sorterDown;
        [NonSerialized]
        private List<Box> _down;
        [NonSerialized]
        private List<Box> _left;
        [NonSerialized]
        private ReadOnlyList<Box> _rodown;
        [NonSerialized]
        private ReadOnlyList<Box> _roright;
        [NonSerialized]
        private List<Box> _up;
        [NonSerialized]
        private BoxComparer _sorterLeft;
        [NonSerialized]
        private Set<Box> _set;
        [NonSerialized]
        private List<Box> _right;
        [NonSerialized]
        private BoxComparer _sorterUp;
        [NonSerialized]
        private List<Box> _zOrder = new List<Box>();
        [NonSerialized]
        private ReadOnlyList<Box> _roZOrder;
    }
}
