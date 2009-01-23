
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

namespace MetaphysicsIndustries.Crystalline
{
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
            _roleft = new BoxList(_left);
            _roright = new BoxList(_right);
            _roup = new BoxList(_up);
            _rodown = new BoxList(_down);
            _intervalSorter = new IntervalComparer(true);

            _roZOrder = new BoxList(_zOrder);
        }

        public virtual void Dispose()
        {
            this.Clear();
            //_set.Dispose();
            //_set = null;
        }

        public virtual Set<Box> GetNeighborsAbove(float bottombound, float leftbound, float rightbound)
        {
            return this.GetNeighbors(BoxOrientation.Up, bottombound, leftbound, rightbound);
        }

        public virtual Set<Box> GetNeighborsRightward(float leftbound, float topbound, float bottombound)
        {
            return this.GetNeighbors(BoxOrientation.Right, leftbound, topbound, bottombound);
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

                Set<Box> neighbors;

                neighbors = this.GetNeighborsLeftward(b.Left, b.Top, b.Bottom);
                foreach (Box ib in neighbors)
                {
                    b.LeftNeighbors.Add(ib);
                }

                neighbors = this.GetNeighborsRightward(b.Right, b.Top, b.Bottom);
                foreach (Box ib in neighbors)
                {
                    b.RightNeighbors.Add(ib);
                }

                neighbors = this.GetNeighborsAbove(b.Top, b.Left, b.Right);
                foreach (Box ib in neighbors)
                {
                    b.UpNeighbors.Add(ib);
                }

                neighbors = this.GetNeighborsBelow(b.Bottom, b.Left, b.Right);
                foreach (Box ib in neighbors)
                {
                    b.DownNeighbors.Add(ib);
                }

                b.RectChanged += Box_RectChanged;

                UpdateBounds();
            }
        }

        public virtual void Move(Box boxToMove, PointF newLocation)
        {
            Move(boxToMove, newLocation, null);
        }

        private static int _moveCallCount = 0;
        public virtual void Move(Box boxToMove, PointF newLocation, Set<Box> collidedBoxes)
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
            PointF p;

            Set<Box> newneighbors1;
            Set<Box> newneighbors2;
            Set<Box> removeneighbors;
            ICollection<Box> moveneighbors;
            object param;

            //if (boxToMove is Element)
            //{
            //    param = ((Element)(boxToMove)).Text;
            //}
            //else
            //{
            //    param = boxToMove.ToString();
            //}
            if (deltax != 0)
            {
                if (deltax > 0)
                {
                    moveneighbors = boxToMove.RightNeighbors;
                }
                else
                {
                    moveneighbors = boxToMove.LeftNeighbors;
                }
                foreach (Box ib in moveneighbors)
                {
                    if (ib is Element && ParentControl.SelectionElement.Contains((Element)ib))
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
                            p.X += delta2;
                        }
                        else
                        {
                            p.X -= delta2;
                        }
                        ib.Move(p, collidedBoxes);
                        if (collidedBoxes != null)
                        {
                            collidedBoxes.Add(ib);
                        }
                    }
                }
                if (deltax > boxToMove.Width || -deltax > boxToMove.Width)
                {
                    //no overlap, discard up/down neighbors 
                    boxToMove.UpNeighbors.Clear();
                    boxToMove.DownNeighbors.Clear();
                }
                else
                {
                    //overlap, discard some neighbors 

                    //note: we don't care about whether or not they're obscured,
                    //so we can just use simple bounds check instead of 
                    //BoxFramework::GetNeighborsXXX

                    removeneighbors = new Set<Box>();
                    foreach (Box ib in boxToMove.UpNeighbors)
                    {
                        if (deltax > 0)
                        {
                            if (ib.Right < boxToMove.Left + deltax)
                            {
                                removeneighbors.Add(ib);
                            }
                        }
                        else
                        {
                            if (ib.Left > boxToMove.Right + deltax)
                            {
                                removeneighbors.Add(ib);
                            }
                        }
                    }
                    foreach (Box ib in removeneighbors)
                    {
                        boxToMove.UpNeighbors.Remove(ib);
                    }
                    removeneighbors.Clear();
                    foreach (Box ib in boxToMove.DownNeighbors)
                    {
                        if (deltax > 0)
                        {
                            if (ib.Right < boxToMove.Left + deltax)
                            {
                                removeneighbors.Add(ib);
                            }
                        }
                        else
                        {
                            if (ib.Left > boxToMove.Right + deltax)
                            {
                                removeneighbors.Add(ib);
                            }
                        }
                    }
                    foreach (Box ib in removeneighbors)
                    {
                        boxToMove.DownNeighbors.Remove(ib);
                    }
                }
                if (deltax > 0)
                {
                    newneighbors1 = boxToMove.Framework.GetNeighborsAbove(boxToMove.Top, Math.Max(boxToMove.Right, boxToMove.Left + deltax), boxToMove.Right + deltax);
                    newneighbors2 = boxToMove.Framework.GetNeighborsBelow(boxToMove.Bottom, Math.Max(boxToMove.Right, boxToMove.Left + deltax), boxToMove.Right + deltax);
                }
                else
                {
                    newneighbors1 = boxToMove.Framework.GetNeighborsAbove(boxToMove.Top, boxToMove.Left + deltax, Math.Min(boxToMove.Left, boxToMove.Right + deltax));
                    newneighbors2 = boxToMove.Framework.GetNeighborsBelow(boxToMove.Bottom, boxToMove.Left + deltax, Math.Min(boxToMove.Left, boxToMove.Right + deltax));
                }
                boxToMove.X += deltax;
                foreach (Box ib in newneighbors1)
                {
                    boxToMove.UpNeighbors.Add(ib);
                }
                foreach (Box ib in newneighbors2)
                {
                    boxToMove.DownNeighbors.Add(ib);
                }
            }
            //else if (deltax < 0)
            //{
            //	//move left
            //}

            if (deltay != 0)
            {
                if (deltay > 0)
                {
                    moveneighbors = boxToMove.DownNeighbors;
                }
                else
                {
                    moveneighbors = boxToMove.UpNeighbors;
                }
                foreach (Box ib in moveneighbors)
                {
                    if (ib is Element && ParentControl.SelectionElement.Contains((Element)ib))
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
                            p.Y += delta2;
                        }
                        else
                        {
                            p.Y -= delta2;
                        }
                        ib.Move(p, collidedBoxes);
                        if (collidedBoxes != null)
                        {
                            collidedBoxes.Add(ib);
                        }
                    }
                }
                if (deltay > boxToMove.Height || -deltay > boxToMove.Height)
                {
                    //no overlap, discard left/right neighbors 
                    boxToMove.LeftNeighbors.Clear();
                    boxToMove.RightNeighbors.Clear();
                }
                else
                {
                    //overlap, discard some neighbors 

                    //note: we don't care about whether or not they're obscured,
                    //so we can just use simple bounds check instead of 
                    //BoxFramework::GetNeighborsXXX

                    removeneighbors = new Set<Box>();
                    foreach (Box ib in boxToMove.LeftNeighbors)
                    {
                        if (deltay > 0)
                        {
                            if (ib.Bottom < boxToMove.Top + deltay)
                            {
                                removeneighbors.Add(ib);
                            }
                        }
                        else
                        {
                            if (ib.Top > boxToMove.Bottom + deltay)
                            {
                                removeneighbors.Add(ib);
                            }
                        }
                    }
                    foreach (Box ib in removeneighbors)
                    {
                        boxToMove.LeftNeighbors.Remove(ib);
                    }
                    removeneighbors.Clear();
                    foreach (Box ib in boxToMove.RightNeighbors)
                    {
                        if (deltay > 0)
                        {
                            if (ib.Bottom < boxToMove.Top + deltay)
                            {
                                removeneighbors.Add(ib);
                            }
                        }
                        else
                        {
                            if (ib.Top > boxToMove.Bottom + deltay)
                            {
                                removeneighbors.Add(ib);
                            }
                        }
                    }
                    foreach (Box ib in removeneighbors)
                    {
                        boxToMove.RightNeighbors.Remove(ib);
                    }
                }
                if (deltay > 0)
                {
                    newneighbors1 = boxToMove.Framework.GetNeighborsLeftward(boxToMove.Left, Math.Max(boxToMove.Bottom, boxToMove.Top + deltay), boxToMove.Bottom + deltay);
                    newneighbors2 = boxToMove.Framework.GetNeighborsRightward(boxToMove.Right, Math.Max(boxToMove.Bottom, boxToMove.Top + deltay), boxToMove.Bottom + deltay);
                }
                else
                {
                    newneighbors1 = boxToMove.Framework.GetNeighborsLeftward(boxToMove.Left, boxToMove.Top + deltay, Math.Min(boxToMove.Top, boxToMove.Bottom + deltay));
                    newneighbors2 = boxToMove.Framework.GetNeighborsRightward(boxToMove.Right, boxToMove.Top + deltay, Math.Min(boxToMove.Top, boxToMove.Bottom + deltay));
                }
                boxToMove.Y += deltay;
                foreach (Box ib in newneighbors1)
                {
                    boxToMove.LeftNeighbors.Add(ib);
                }
                foreach (Box ib in newneighbors2)
                {
                    boxToMove.RightNeighbors.Add(ib);
                }
            }
            //else if (deltay < 0)
            //{
            //	//move up
            //}

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

        protected virtual void Resize(Box boxToResize, SizeF newSize)
        {
            Resize(boxToResize, newSize, null);
        }

        protected virtual void Resize(Box boxToResize, SizeF newSize, Set<Box> collidedBoxes)
        {
            throw new NotImplementedException();

            if (boxToResize == null) { throw new ArgumentNullException("boxToResize"); }
            if (newSize.Width < 0 || newSize.Height < 0) { return; }

            float deltaw = newSize.Width - boxToResize.Width;
            float deltah = newSize.Height - boxToResize.Height;
            float delta2 = 0;
            PointF newLocation;

            if (deltaw > 0)
            {
                foreach (Box ib in boxToResize.RightNeighbors)
                {
                    delta2 = boxToResize.Right + deltaw - ib.Left;
                    if (delta2 > 0)
                    {
                        newLocation = ib.Location;
                        newLocation.X += delta2;
                        ib.Move(newLocation, collidedBoxes);
                        if (collidedBoxes != null)
                        {
                            collidedBoxes.Add(ib);
                        }
                    }
                }
                //now we have to update the lists of 
                //up-neighbors and down-neighbors

                //deltaw is always >= 0, and within this if 
                //block it's always > 0, so we might be adding
                //neighbors.

                //but! if we shrink the box, if deltaw < 0, we 
                //still have to update the neighbors lists
            }
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

        public virtual Set<Box> GetNeighborsLeftward(float rightbound, float topbound, float bottombound)
        {
            return this.GetNeighbors(BoxOrientation.Left, rightbound, topbound, bottombound);
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

                bool ret;
                ret = _set.Remove(b);

                Box[] neighbors;

                neighbors = new Box[b.LeftNeighbors.Count];
                b.LeftNeighbors.CopyTo(neighbors, 0);
                foreach (Box box in neighbors)
                {
                    box.RightNeighbors.Remove(b);
                }

                neighbors = new Box[b.UpNeighbors.Count];
                b.UpNeighbors.CopyTo(neighbors, 0);
                foreach (Box box in neighbors)
                {
                    box.DownNeighbors.Remove(b);
                }

                neighbors = new Box[b.RightNeighbors.Count];
                b.RightNeighbors.CopyTo(neighbors, 0);
                foreach (Box box in neighbors)
                {
                    box.LeftNeighbors.Remove(b);
                }

                neighbors = new Box[b.DownNeighbors.Count];
                b.DownNeighbors.CopyTo(neighbors, 0);
                foreach (Box box in neighbors)
                {
                    box.UpNeighbors.Remove(b);
                }

                b.ParentCrystallineControl = null;// Framework = null;

                UpdateBounds();

                return ret;
            }
            return false;
        }

        public virtual Set<Box> GetNeighborsBelow(float topbound, float leftbound, float rightbound)
        {
            return this.GetNeighbors(BoxOrientation.Down, topbound, leftbound, rightbound);
        }

        public void BringForward(Box box)
        {
            if (box.Framework != this) { throw new InvalidOperationException("The box is not within this framework."); }

            int index = _zOrder.IndexOf(box);
            if (index < _zOrder.Count - 1)
            {
                _zOrder.RemoveAt(index);
                _zOrder.Insert(index + 1, box);
            }
        }
        public void SendBackward(Box box)
        {
            if (box.Framework != this) { throw new InvalidOperationException("The box is not within this framework."); }

            int index = _zOrder.IndexOf(box);
            if (index > 0)
            {
                _zOrder.RemoveAt(index);
                _zOrder.Insert(index - 1, box);
            }

        }
        public void BringToFront(Box box)
        {
            if (box.Framework != this) { throw new InvalidOperationException("The box is not within this framework."); }

            int index = _zOrder.IndexOf(box);
            if (index < _zOrder.Count - 1)
            {
                _zOrder.RemoveAt(index);
                _zOrder.Insert(_zOrder.Count, box);
            }
        }
        public void SendToBack(Box box)
        {
            if (box.Framework != this) { throw new InvalidOperationException("The box is not within this framework."); }

            int index = _zOrder.IndexOf(box);
            if (index > 0)
            {
                _zOrder.RemoveAt(index);
                _zOrder.Insert(0, box);
            }
        }

        CrystallineControl _parentControl;
        public CrystallineControl ParentControl
        {
            get
            {
                return _parentControl;
            }
        }

        public virtual BoxList Right
        {
            get
            {
                return _roright;
            }
        }

        public virtual BoxList Up
        {
            get
            {
                return _roup;
            }
        }

        public virtual int Count
        {
            get
            {
                return _set.Count;
            }
        }

        public virtual BoxList Down
        {
            get
            {
                return _rodown;
            }
        }

        public virtual BoxList Left
        {
            get
            {
                return _roleft;
            }
        }

        public virtual BoxList ZOrder
        {
            get { return _roZOrder; }
        }

        protected virtual Set<Box> GetNeighbors(BoxOrientation orientation, float mainbound, float crossmin, float crossmax)
        {
            List<Box> list;
            BoxComparer comp;
            Set<Box> boxes;
            int i;
            Box bb = new Box();
            bool reverse = false;
            List<Interval> intervals;

            //have to rethink this
            //this->_left ISN'T sorted right-to-left, as would be useful 
            //for the current form of the algorithm...

            //throw gcnew NotImplementedException(__WCODESIG__);

            if (orientation == BoxOrientation.Left)
            {
                list = (new List<Box>(_right));
                //list->Reverse();
                comp = _sorterRight;
                bb.X = mainbound - 1;
                bb.Width = 1;
                reverse = true;
            }
            else if (orientation == BoxOrientation.Right)
            {
                list = _left;
                comp = _sorterLeft;
                bb.X = mainbound;
            }
            else if (orientation == BoxOrientation.Up)
            {
                list = (new List<Box>(_down));
                //list->Reverse();
                comp = _sorterDown;
                bb.Y = mainbound - 1;
                bb.Height = 1;
                reverse = true;
            }
            else if (orientation == BoxOrientation.Down)
            {
                list = _up;
                comp = _sorterUp;
                bb.Y = mainbound;
            }
            else
            {
                throw new ArgumentException("invalid orientation");
            }
            i = list.BinarySearch(bb, comp);
            if (i < 0) { i = ~i; }
            //if (!reverse)
            //{
            //	i--;
            //	if (i < 0) { i = 0; }
            //}
            if (reverse)
            {
                i = list.Count - i;
                list = new List<Box>(list);
                list.Reverse();

            }
            List<string> names;
            names = new List<string>(list.Count);
            foreach (Box ib in list)
            {
                if (ib is Element)
                {
                    if ((ib as Element).Text != null)
                    {
                        names.Add(((Element)(ib)).Text.ToString());
                    }
                }
                else
                {
                    names.Add(null);
                }
            }
            intervals = new List<Interval>();
            boxes = new Set<Box>();
            int end = (reverse ? 0 : list.Count);
            int step = (reverse ? -1 : 1);
            for (; i < list.Count; i++)
            {
                bb = list[i];
                bool within;
                if (orientation == BoxOrientation.Left ||
                    orientation == BoxOrientation.Right)
                {
                    within = (bb.Top <= crossmax && bb.Bottom >= crossmin);
                }
                else
                {
                    within = (bb.Left <= crossmax && bb.Right >= crossmin);
                }
                if (within)
                {
                    Interval ii;
                    Interval current;
                    int k;
                    if (orientation == BoxOrientation.Left ||
                        orientation == BoxOrientation.Right)
                    {
                        ii = new Interval(bb.Top, bb.Bottom);
                    }
                    else
                    {
                        ii = new Interval(bb.Left, bb.Right);
                    }
                    if (intervals.Count > 0)
                    {
                        k = intervals.BinarySearch(ii, _intervalSorter);
                        if (k < 0) { k = ~k; }
                        k--;
                        if (k < 0) { k = 0; }
                        current = intervals[k];
                        if (current.Min < ii.Min && current.Max > ii.Max)
                        {

                            continue;
                        }
                        while (current.Intersects(ii) && intervals.Count > k)
                        {
                            intervals.RemoveAt(k);
                            ii = Interval.Merge(ii, current);
                            if (intervals.Count < 1)
                            {
                                break;
                            }
                            if (k >= intervals.Count) { k = intervals.Count - 1; }
                            current = intervals[k];
                        }
                        intervals.Insert(k, ii);
                    }
                    else
                    {
                        intervals.Add(ii);
                    }
                    boxes.Add(bb);
                    if (intervals.Count == 1 && intervals[0].Min <= crossmin && intervals[0].Max >= crossmax)
                    {


                        break;
                    }
                }
            }
            return boxes;
        }

        private RectangleF _bounds;
        public RectangleF Bounds
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
                RectangleF r = new RectangleF();

                r.X = Left[0].Left;
                r.Y = Up[0].Top;
                r.Width = Right[Right.Count - 1].Right - r.X;
                r.Height = Down[Down.Count - 1].Bottom - r.Y;

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

        private BoxList _roleft;
        private IntervalComparer _intervalSorter;
        private BoxList _roup;
        private BoxComparer _sorterRight;
        private BoxComparer _sorterDown;
        private List<Box> _down;
        private List<Box> _left;
        private BoxList _rodown;
        private BoxList _roright;
        private List<Box> _up;
        private BoxComparer _sorterLeft;
        private Set<Box> _set;
        private List<Box> _right;
        private BoxComparer _sorterUp;
        private List<Box> _zOrder = new List<Box>();
        private BoxList _roZOrder;
    }
}
