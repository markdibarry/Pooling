# <a id="GameCore_Pooling_Pool"></a> Pool `class`

Namespace: [GameCore.Pooling](GameCore.Pooling.md)

Represents a pool of objects that can be borrowed and returned.

```csharp
public static class Pool
```

## Methods

### <a id="GameCore_Pooling_Pool_Allocate__1_System_Int32_"></a> Allocate<T\>\(int\)

Populates the provided queue with the specified number of objects.

```csharp
public static void Allocate<T>(int amount) where T : IPoolable, new()
```

#### Parameters

[int](https://learn.microsoft.com/dotnet/api/system.int32) `amount` 

The amount of objects to allocate to the pool.

#### Type Parameters

`T` 

The pool type to allocate to.

### <a id="GameCore_Pooling_Pool_AllocateSafe__1_System_Int32_"></a> AllocateSafe<T\>\(int\)

Populates an existing pool in a thread-safe way.

```csharp
public static void AllocateSafe<T>(int amount) where T : IPoolable, new()
```

#### Parameters

[int](https://learn.microsoft.com/dotnet/api/system.int32) `amount`

The amount of objects to allocate to the pool.

#### Type Parameters

`T` 

The pool type to allocate to.

### <a id="GameCore_Pooling_Pool_AllocateScene__1_System_Int32_"></a> AllocateScene<T\>\(int\)

Populates the provided queue with the specified number of scenes.

```csharp
public static void AllocateScene<T>(int amount) where T : Node, IScene, IPoolable
```

#### Parameters

[int](https://learn.microsoft.com/dotnet/api/system.int32) `amount`

The amount of scenes to allocate to the pool.

#### Type Parameters

`T` 

The pool type to allocate to.

### <a id="GameCore_Pooling_Pool_AllocateSceneSafe__1_System_Int32_"></a> AllocateSceneSafe<T\>\(int\)

Populates an existing pool in a thread-safe way.

```csharp
public static void AllocateSceneSafe<T>(int amount) where T : Node, IScene, IPoolable, new()
```

#### Parameters

[int](https://learn.microsoft.com/dotnet/api/system.int32) `amount`

The amount of objects to allocate to the pool.

#### Type Parameters

`T` 

The pool type to allocate to.

### <a id="GameCore_Pooling_Pool_ClearPool"></a> ClearPool\(\)

Clears the pool.

```csharp
public static void ClearPool()
```

### <a id="GameCore_Pooling_Pool_Get__1"></a> Get<T\>\(\)

Retrieves an object from the pool of a registered type.
If the pool is empty, a new object is created.

```csharp
public static T Get<T>() where T : IPoolable, new()
```

#### Returns

 T

An object of the specified type

#### Type Parameters

`T` 

The type of object to borrow.

### <a id="GameCore_Pooling_Pool_GetSafe__1"></a> GetSafe<T\>\(\)

Retrieves an object from the pool of a registered type in a thread-safe way.
If the pool is empty, a new object is created.

```csharp
public static T GetSafe<T>() where T : IPoolable, new()
```

#### Returns

 T

An object of the specified type

#### Type Parameters

`T` 

The type of object to borrow.

### <a id="GameCore_Pooling_Pool_GetSameTypeOrNull__1___0_"></a> GetSameTypeOrNull<T\>\(T\)

Gets an object of the same type if exists within the pool, otherwise returns null.

```csharp
public static T? GetSameTypeOrNull<T>(this T poolable) where T : IPoolable
```

#### Parameters

T `poolable`

The poolable object.

#### Returns

 T?

An object of the same type if exists within the pool, otherwise returns null.

#### Type Parameters

`T` 

The type of the poolable object.

### <a id="GameCore_Pooling_Pool_GetScene__1"></a> GetScene<T\>\(\)

Retrieves a scene from the pool of a registered type.
If the pool is empty, a new scene is instantiated.

```csharp
public static T GetScene<T>() where T : Node, IScene, IPoolable, new()
```

#### Returns

 T

A scene of the specified type

#### Type Parameters

`T` 

The type of scene to borrow.

### <a id="GameCore_Pooling_Pool_GetSceneSafe__1"></a> GetSceneSafe<T\>\(\)

Retrieves a scene from the pool of a registered type.
If the pool is empty, a new scene is instantiated.

```csharp
public static T GetSceneSafe<T>() where T : Node, IScene, IPoolable, new()
```

#### Returns

 T

A scene of the specified type

#### Type Parameters

`T` 

The type of scene to borrow.

### <a id="GameCore_Pooling_Pool_PrintPool"></a> PrintPool\(\)

Prints a snapshot of the pool's current state.

```csharp
public static void PrintPool()
```

### <a id="GameCore_Pooling_Pool_Return_GameCore_Pooling_IPoolable_"></a> Return\(IPoolable\)

Returns the provided object to the pool of the underlying registered type.

```csharp
public static void Return(IPoolable poolable)
```

#### Parameters

[IPoolable](GameCore.Pooling.IPoolable.md) `poolable`

The object to return.

### <a id="GameCore_Pooling_Pool_ReturnSafe_GameCore_Pooling_IPoolable_"></a> ReturnSafe\(IPoolable\)

Returns the provided object to the pool of the underlying registered type in a thread-safe
way.

```csharp
public static void ReturnSafe(IPoolable poolable)
```

#### Parameters

[IPoolable](GameCore.Pooling.IPoolable.md) `poolable`

The object to return.

### <a id="GameCore_Pooling_Pool_ReturnToPool_GameCore_Pooling_IPoolable_"></a> ReturnToPool\(IPoolable\)

Returns the provided object to the pool of the underlying registered type.

```csharp
public static void ReturnToPool(this IPoolable poolable)
```

#### Parameters

[IPoolable](GameCore.Pooling.IPoolable.md) `poolable`

The object to return.

### <a id="GameCore_Pooling_Pool_ReturnToPoolSafe_GameCore_Pooling_IPoolable_"></a> ReturnToPoolSafe\(IPoolable\)

Returns the provided object to the pool of the underlying registered type in a thread-safe
way.

```csharp
public static void ReturnToPoolSafe(this IPoolable poolable)
```

#### Parameters

[IPoolable](GameCore.Pooling.IPoolable.md) `poolable`

The object to return.

### <a id="GameCore_Pooling_Pool_SetLimit__1_System_Int32_"></a> SetLimit<T\>\(int\)

Sets a limit for the pool to store.

```csharp
public static void SetLimit<T>(int limit) where T : IPoolable, new()
```

#### Parameters

[int](https://learn.microsoft.com/dotnet/api/system.int32) `limit`

The limit.

#### Type Parameters

`T` 

The type of object to limit.

