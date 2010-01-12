
/*****************************************************************************
 *                                                                           *
 *  BoxCollection.cs                                                         *
 *  28 March 2007                                                            *
 *  Project: Crystalline                                                     *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2007 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 10 November 2007                             *
 *                                                                           *
 *  A class that holds a set of boxes, and coordinates them with the         *
 *    BoxFramework. This is the common base class for ElementCollection,     *
 *    PathwayCollection and PathingJunctionCollection.                       *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Crystalline
{
    public class BoxCollection<T> : ICollection<T>
        where T : Box
    {
        protected BoxCollection() { }
        public BoxCollection(BoxFramework f)
        {
            if (f == null) { throw new ArgumentNullException("f"); }

            this._framework = f;
            this._collection = new Set<T>();
        }

        public  void Dispose()
        {
            Clear();

            //_collection.Clear();
            //collection.Dispose();
            //_collection = null;

            //_framework = null;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            //if (array == null) { throw new ArgumentNullException("array"); }
            //if (arrayIndex < 0) { throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument is out of range"); }
            //if (array.Rank > 1) { throw new ArgumentException("Only one-dimensional arrays are allowed", "array"); }
            //if (this.Count > array.Length - arrayIndex) { throw new ArgumentException("Not enough available space in the array"); }

            //foreach (T t in this)
            //{
            //    array[arrayIndex] = t;
            //    arrayIndex++;
            //}
            _collection.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(T boxToRemove)
        {
            this._framework.Remove(boxToRemove);
            return this._collection.Remove(boxToRemove);

        }

        public virtual void RemoveRange<U>(params U[] boxesToRemove)
            where U : T
        {
            RemoveRange((ICollection<T>)boxesToRemove);
        }

        public virtual void RemoveRange<U>(ICollection<U> boxesToRemove)
            where U : T
        {
            foreach (U t in boxesToRemove)
            {
                Remove(t);
            }
        }

        public virtual void Add(T boxToAdd)
        {
            this._framework.Add(boxToAdd);
            this._collection.Add(boxToAdd);

        }

        public virtual void AddRange<U>(IEnumerable<U> items)
            where U : T
        {
            foreach (U item in items)
            {
                Add(item);
            }
        }

        public virtual void Clear()
        {
            T[]	r;
	        r = new T[this.Count];
            CopyTo(r, 0);
	        foreach (T t in r)
	        {
		        this.Remove(t);
	        }

	        this._collection.Clear();
        }

        public virtual bool Contains(T boxToTest)
        {
            return this._collection.Contains(boxToTest);

        }

        public virtual bool IsReadOnly
        {
            get
            {
                return _collection.IsReadOnly;
            }
        }

        public virtual int Count
        {
            get
            {
                return _collection.Count;
            }
        }

          System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //[NonSerialized]
        private BoxFramework _framework;
        private ICollection<T> _collection;
    }
}
