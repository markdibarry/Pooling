# <a id="GameCore_Pooling_ListPool"></a> ListPool `class`

Namespace: [GameCore.Pooling](GameCore.Pooling.md)

Represents a pool of Lists that can be borrowed and returned.

```csharp
public static class ListPool
```

## Methods

### <a id="GameCore_Pooling_ListPool_ClearPool__1"></a> ClearPool<T\>\(\)

Clears a pool of the Lists of the specified type.

```csharp
public static void ClearPool<T>()
```

#### Type Parameters

`T` 

### <a id="GameCore_Pooling_ListPool_ClearPools"></a> ClearPools\(\)

Clears all pools.

```csharp
public static void ClearPools()
```

### <a id="GameCore_Pooling_ListPool_Get__1"></a> Get<T\>\(\)

Retrieves a List from the pool of a registered type.
If the pool is empty, a new List is created.

```csharp
public static List<T> Get<T>()
```

#### Returns

 [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list\-1)<T\>

#### Type Parameters

`T` 

### <a id="GameCore_Pooling_ListPool_PrintPool"></a> PrintPool\(\)

Prints a snapshot of the pool's current state.

```csharp
public static void PrintPool()
```

### <a id="GameCore_Pooling_ListPool_Return__1_System_Collections_Generic_List___0__"></a> Return<T\>\(List<T\>\)

Returns the provided List to the pool of the underlying registered type.
If the List contains IPoolable objects, they will be returned to their pool as well.

```csharp
public static void Return<T>(List<T> list)
```

#### Parameters

[List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list\-1)<T\> `list`

#### Type Parameters

`T` 

### <a id="GameCore_Pooling_ListPool_SetLimit__1_System_Int32_"></a> SetLimit<T\>\(int\)

Sets a limit for the pool to store.

```csharp
public static void SetLimit<T>(int limit) where T : IPoolable, new()
```

#### Parameters

[int](https://learn.microsoft.com/dotnet/api/system.int32) `limit`

The limit.

#### Type Parameters

`T` 

The type of List to limit.

