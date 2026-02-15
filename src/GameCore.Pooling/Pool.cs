using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GameCore.SceneHelpers;
using Godot;

namespace GameCore.Pooling;

/// <summary>
/// Represents a pool of objects that can be borrowed and returned.
/// </summary>
public static class Pool
{
    private static readonly Lock s_lock = new();
    private static readonly Dictionary<Type, LimitedQueue<IPoolable>> s_pool = [];
    private static readonly StringBuilder s_sb = new();

    /// <summary>
    /// Prints a snapshot of the pool's current state.
    /// </summary>
    public static void PrintPool()
    {
        s_sb.AppendLine();
        s_sb.Append("|--Current pool types and counts--|");
        s_sb.AppendLine();

        foreach (var kvp in s_pool)
        {
            s_sb.Append($"{kvp.Key}: {kvp.Value.Count}");
        }

        s_sb.AppendLine();
        GD.Print(s_sb.ToString());
        s_sb.Clear();
    }

    /// <summary>
    /// Clears the pool.
    /// </summary>
    public static void ClearPool()
    {
        s_pool.Clear();
    }

    /// <summary>
    /// Sets a limit for the pool to store.
    /// </summary>
    /// <typeparam name="T">The type of object to limit.</typeparam>
    /// <param name="limit">The limit.</param>
    public static void SetLimit<T>(int limit) where T : IPoolable, new()
    {
        Type type = typeof(T);
        LimitedQueue<IPoolable> limitedQueue = GetOrCreateLimitedQueue(type);
        limitedQueue.Limit = limit;
    }

    /// <summary>
    /// Populates the provided queue with the specified number of objects.
    /// </summary>
    /// <typeparam name="T">The pool type to allocate to.</typeparam>
    /// <param name="amount">The amount of objects to allocate to the pool.</param>
    public static void Allocate<T>(int amount) where T : IPoolable, new()
    {
        Type type = typeof(T);
        LimitedQueue<IPoolable> limitedQueue = GetOrCreateLimitedQueue(type);
        int toAllocate = Math.Max(amount, 0);

        if (limitedQueue.Limit != -1)
            toAllocate = Math.Min(toAllocate, limitedQueue.Limit - limitedQueue.Count);

        for (int i = 0; i < toAllocate; i++)
        {
            IPoolable obj = new T();
            limitedQueue.Enqueue(obj);
        }
    }

    /// <summary>
    /// Populates an existing pool in a thread-safe way.
    /// </summary>
    /// <typeparam name="T">The pool type to allocate to.</typeparam>
    /// <param name="amount">The amount of objects to allocate to the pool.</param>
    public static void AllocateSafe<T>(int amount) where T : IPoolable, new()
    {
        lock (s_lock)
        {
            Allocate<T>(amount);
        }
    }

    /// <summary>
    /// Populates the provided queue with the specified number of scenes.
    /// </summary>
    /// <typeparam name="T">The pool type to allocate to.</typeparam>
    /// <param name="amount">The amount of scenes to allocate to the pool.</param>
    public static void AllocateScene<T>(int amount)
        where T : Node, IScene, IPoolable
    {
        Type type = typeof(T);
        LimitedQueue<IPoolable> limitedQueue = GetOrCreateLimitedQueue(type);
        int toAllocate = Math.Max(amount, 0);

        if (limitedQueue.Limit != -1)
            toAllocate = Math.Min(toAllocate, limitedQueue.Limit - limitedQueue.Count);

        for (int i = 0; i < toAllocate; i++)
        {
            IPoolable obj = IScene.Instantiate<T>();
            limitedQueue.Enqueue(obj);
        }
    }

    /// <summary>
    /// Populates an existing pool in a thread-safe way.
    /// </summary>
    /// <typeparam name="T">The pool type to allocate to.</typeparam>
    /// <param name="amount">The amount of objects to allocate to the pool.</param>
    public static void AllocateSceneSafe<T>(int amount)
        where T : Node, IScene, IPoolable, new()
    {
        lock (s_lock)
        {
            AllocateScene<T>(amount);
        }
    }

    /// <summary>
    /// Gets an object of the same type if exists within the pool, otherwise returns null.
    /// </summary>
    /// <typeparam name="T">The type of the poolable object.</typeparam>
    /// <param name="poolable">The poolable object.</param>
    /// <returns>An object of the same type if exists within the pool, otherwise returns null.</returns>
    public static T? GetSameTypeOrNull<T>(this T poolable) where T : IPoolable
    {
        Type type = poolable.GetType();
        LimitedQueue<IPoolable>? limitedQueue = GetLimitedQueue(type);
        return limitedQueue?.Count > 0 ? (T)limitedQueue.Dequeue() : default;
    }

    /// <summary>
    /// Retrieves an object from the pool of a registered type.
    /// If the pool is empty, a new object is created.
    /// </summary>
    /// <typeparam name="T">The type of object to borrow.</typeparam>
    /// <returns>An object of the specified type</returns>
    public static T Get<T>() where T : IPoolable, new()
    {
        Type type = typeof(T);
        LimitedQueue<IPoolable>? limitedQueue = GetLimitedQueue(type);
        return limitedQueue?.Count > 0 ? (T)limitedQueue.Dequeue() : new();
    }

    /// <summary>
    /// Retrieves an object from the pool of a registered type in a thread-safe way.
    /// If the pool is empty, a new object is created.
    /// </summary>
    /// <typeparam name="T">The type of object to borrow.</typeparam>
    /// <returns>An object of the specified type</returns>
    public static T GetSafe<T>() where T : IPoolable, new()
    {
        lock (s_lock)
        {
            return Get<T>();
        }
    }

    /// <summary>
    /// Retrieves a scene from the pool of a registered type.
    /// If the pool is empty, a new scene is instantiated.
    /// </summary>
    /// <typeparam name="T">The type of scene to borrow.</typeparam>
    /// <returns>A scene of the specified type</returns>
    public static T GetScene<T>() where T : Node, IScene, IPoolable, new()
    {
        Type type = typeof(T);
        LimitedQueue<IPoolable>? limitedQueue = GetLimitedQueue(type);
        return limitedQueue?.Count > 0 ? (T)limitedQueue.Dequeue() : IScene.Instantiate<T>();
    }

    /// <summary>
    /// Retrieves a scene from the pool of a registered type.
    /// If the pool is empty, a new scene is instantiated.
    /// </summary>
    /// <typeparam name="T">The type of scene to borrow.</typeparam>
    /// <returns>A scene of the specified type</returns>
    public static T GetSceneSafe<T>() where T : Node, IScene, IPoolable, new()
    {
        lock (s_lock)
        {
            return GetScene<T>();
        }
    }

    /// <summary>
    /// Returns the provided object to the pool of the underlying registered type.
    /// </summary>
    /// <param name="poolable">The object to return.</param>
    public static void Return(IPoolable poolable)
    {
        if (poolable is Node node)
        {
            if (node.IsQueuedForDeletion())
                throw new ActiveNodeException(node.Name);

            if (node.IsInsideTree())
                node.GetParent().RemoveChild(node);
        }

        poolable.ClearObject();
        Type type = poolable.GetType();
        LimitedQueue<IPoolable> limitedQueue = GetOrCreateLimitedQueue(type);
        limitedQueue.Enqueue(poolable);
    }

    /// <summary>
    /// Returns the provided object to the pool of the underlying registered type in a thread-safe
    /// way.
    /// </summary>
    /// <param name="poolable">The object to return.</param>
    public static void ReturnSafe(IPoolable poolable)
    {
        lock (s_lock)
        {
            Return(poolable);
        }
    }

    /// <summary>
    /// Returns the provided object to the pool of the underlying registered type.
    /// </summary>
    /// <param name="poolable">The object to return.</param>
    public static void ReturnToPool(this IPoolable poolable) => Return(poolable);

    /// <summary>
    /// Returns the provided object to the pool of the underlying registered type in a thread-safe
    /// way.
    /// </summary>
    /// <param name="poolable">The object to return.</param>
    public static void ReturnToPoolSafe(this IPoolable poolable) => ReturnSafe(poolable);

    private static LimitedQueue<IPoolable>? GetLimitedQueue(Type type)
    {
        s_pool.TryGetValue(type, out LimitedQueue<IPoolable>? limitedQueue);
        return limitedQueue;
    }

    private static LimitedQueue<IPoolable> GetOrCreateLimitedQueue(Type type)
    {
        if (!s_pool.TryGetValue(type, out LimitedQueue<IPoolable>? limitedQueue))
        {
            limitedQueue = new();
            s_pool[type] = limitedQueue;
        }

        return limitedQueue;
    }

    /// <summary>
    /// An exception for accessing types that are not registered to the pool.
    /// </summary>
    [Serializable]
    private class UnregisteredTypeException : Exception
    {
        public UnregisteredTypeException(Type type)
            : base($"Type \"${type.Name}\" is not registered for Pool.")
        { }
    }

    [Serializable]
    private class ActiveNodeException : Exception
    {
        public ActiveNodeException(StringName name)
            : base($"Node \"${name}\" is queued for deletion or inside the scene tree.")
        { }
    }
}
