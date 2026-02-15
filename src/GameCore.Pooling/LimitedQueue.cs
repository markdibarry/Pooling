using System;
using System.Collections.Generic;

namespace GameCore.Pooling;

/// <summary>
/// A queue wrapper with an upper limit.
/// </summary>
internal class LimitedQueue<T> : Queue<T>
{
    public LimitedQueue(int limit = -1)
    {
        Limit = Math.Max(-1, limit);
    }

    /// <summary>
    /// The upper limit for adding items to the queue.
    /// </summary>
    public int Limit { get; set; }

    /// <summary>
    /// Adds an object to the end of the queue.
    /// </summary>
    /// <param name="item">The object to add to the queue.</param>
    public new void Enqueue(T item)
    {
        if (Limit == -1 || Count < Limit)
            base.Enqueue(item);
    }
}

internal class PoolQueue<T> : LimitedQueue<T>
{
    public PoolQueue(int limit, Func<T> createFunc)
        : base(limit)
    {
        CreateFunc = createFunc;
    }

    /// <summary>
    /// A delegate to create a new object of the queue's underlying type.
    /// </summary>
    public Func<T> CreateFunc { get; set; }
}