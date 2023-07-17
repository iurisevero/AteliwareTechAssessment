using System;
using DataStructures.FibonacciHeap;

// https://github.com/erdiizgi/DSUnity/tree/v0.1.0
namespace DataStructures.PriorityQueue
{
    public class PriorityQueue<TElement, TPriority> : IPriorityQueue<TElement, TPriority>
        where TPriority : IComparable<TPriority>
    {
        private readonly FibonacciHeap<TElement, TPriority> heap;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="minPriority">Minimum value of the priority - to be used for comparing.</param>
        public PriorityQueue(TPriority minPriority)
        {
            heap = new FibonacciHeap<TElement, TPriority>(minPriority);
        }

        public void Insert(TElement item, TPriority priority)
        {
            heap.Insert(new FibonacciHeapNode<TElement, TPriority>(item, priority));
        }

        public TElement Top()
        {
            if(heap.IsEmpty())
                return default(TElement);
            return heap.Min().Data;
        }

        public TElement Pop()
        {
            if(heap.IsEmpty())
                return default(TElement);
            return heap.RemoveMin().Data;
        }

        public bool IsEmpty()
        {
            return heap.IsEmpty();
        }
    }
}


