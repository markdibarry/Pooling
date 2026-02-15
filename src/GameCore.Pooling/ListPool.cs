using System;
using System.Collections.Generic;
using System.Text;
using Godot;

namespace GameCore.Pooling;

/// <summary>
/// Represents a pool of objects that can be borrowed and returned.
/// </summary>
public static class ListPool
{
    private static readonly Dictionary<Type, LimitedQueue<object>> s_listPool = [];
    private static readonly StringBuilder s_sb = new();

    /// <summary>
    /// Prints a snapshot of the pool's current state.
    /// </summary>
    public static void PrintPool()
    {
        s_sb.AppendLine();
        s_sb.Append("|--Current pool types and counts--|");
        s_sb.AppendLine();

        foreach (var kvp in s_listPool)
        {
            s_sb.Append($"List<{kvp.Key}>: {kvp.Value.Count}");
        }

        s_sb.AppendLine();
        GD.Print(s_sb.ToString());
        s_sb.Clear();
    }

    /// <summary>
    /// Clears all pools.
    /// </summary>
    public static void ClearPools()
    {
        s_listPool.Clear();
    }

    /// <summary>
    /// Clears a pool of the Lists of the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void ClearPool<T>()
    {
        Type type = typeof(T);
        LimitedQueue<object>? limitedQueue = GetLimitedQueue(type);
        limitedQueue?.Clear();
    }

    /// <summary>
    /// Sets a limit for the pool to store.
    /// </summary>
    /// <typeparam name="T">The type of List to limit.</typeparam>
    /// <param name="limit">The limit.</param>
    public static void SetLimit<T>(int limit) where T : IPoolable, new()
    {
        Type type = typeof(T);
        LimitedQueue<object> limitedQueue = GetOrCreateLimitedQueue(type);
        limitedQueue.Limit = limit;
    }

    /// <summary>
    /// Retrieves a List from the pool of a registered type.
    /// If the pool is empty, a new List is created.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> Get<T>()
    {
        Type type = typeof(T);
        LimitedQueue<object>? limitedQueue = GetLimitedQueue(type);
        return limitedQueue?.Count > 0 ? (List<T>)limitedQueue.Dequeue() : [];
    }

    /// <summary>
    /// Returns the provided List to the pool of the underlying registered type.
    /// If the List contains IPoolable objects, they will be returned to their pool as well.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Return<T>(List<T> list)
    {
        Type type = typeof(T);

        foreach (T item in list)
        {
            if (item is IPoolable poolable)
                Pool.Return(poolable);
        }

        list.Clear();
        LimitedQueue<object> limitedQueue = GetOrCreateLimitedQueue(type);
        limitedQueue.Enqueue(list);
    }

    private static LimitedQueue<object>? GetLimitedQueue(Type type)
    {
        s_listPool.TryGetValue(type, out LimitedQueue<object>? limitedQueue);
        return limitedQueue;
    }

    private static LimitedQueue<object> GetOrCreateLimitedQueue(Type type)
    {
        if (!s_listPool.TryGetValue(type, out LimitedQueue<object>? limitedQueue))
        {
            limitedQueue = new();
            s_listPool[type] = limitedQueue;
        }

        return limitedQueue;
    }
}
