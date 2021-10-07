[TOC]

# MGS.UCommon.dll

## Summary
- Common code for Unity project develop.

## Environment
- .Net Framework 3.5 or above.
- Unity 5.0 or above.

## Dependence
- System.dll
- MGS.Common.dll
- MGS.Logger.dll
- UnityEngine.dll
- UnityEngine.UI.dll

## Module

### Extension

```C#
public static class GameObjectExtention{}
public static class TerrainExtension{}
public static class Texture2DExtention{}
```

### Generic

```C#
public enum Axis{}
public enum ColorBlendMode{}
public enum LerpMode{}
public enum MirrorMode{}

public struct PlaneArea{}
public struct Range{}
```

### Listener

```C#
public class MonoDragListener : MonoBehaviour,
IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler,
IDropHandler, IScrollHandler, IMoveHandler{}

public class MonoPointerListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler{}

public class MonoSelectListener : MonoBehaviour, ISelectHandler, IUpdateSelectedHandler, IDeselectHandler, ISubmitHandler, ICancelHandler{}
```

### Serialization

```C#
public class ListAvatar<T>{}
public class DictionaryAvatar<TKey, TValue> : ISerializationCallbackReceiver{}
public sealed class JsonUtilityPro{}
```

### Threading

```C#
public sealed class Dispatcher : MonoBehaviour{}
```

### Utility

```c#
public sealed class ColorBlendUtility{}
public sealed class EventSystemUtility{}
public sealed class MeshUtility{}
public sealed class RectUtility{}
public sealed class VectorUtility{}
```

------

[Previous](../../README.md)

------

Copyright © 2021 Mogoson.	mogoson@outlook.com