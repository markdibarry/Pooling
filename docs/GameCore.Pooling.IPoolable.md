# <a id="GameCore_Pooling_IPoolable"></a> IPoolable `interface`

Namespace: [GameCore.Pooling](GameCore.Pooling.md)

Represents a type that can be stored in a pool. Pools maintain
pre-instantiated instances that can be borrowed and returned to avoid
excess memory allocations. Pooled objects are "reset" whenever they are
returned to the pool so that they can be reused.

```csharp
public interface IPoolable
```

#### Extension Methods

[Pool.GetSameTypeOrNull<IPoolable\>\(IPoolable\)](GameCore.Pooling.Pool.md\#GameCore\_Pooling\_Pool\_GetSameTypeOrNull\_\_1\_\_\_0\_), 
[Pool.ReturnToPool\(IPoolable\)](GameCore.Pooling.Pool.md\#GameCore\_Pooling\_Pool\_ReturnToPool\_GameCore\_Pooling\_IPoolable\_), 
[Pool.ReturnToPoolSafe\(IPoolable\)](GameCore.Pooling.Pool.md\#GameCore\_Pooling\_Pool\_ReturnToPoolSafe\_GameCore\_Pooling\_IPoolable\_)

## Methods

### <a id="GameCore_Pooling_IPoolable_ClearObject"></a> ClearObject\(\)

Resets the object to its default state

```csharp
void ClearObject()
```

