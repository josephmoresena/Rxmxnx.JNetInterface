# Functional Interfaces

`Rxmxnx.JNetInterface.Functional` carries JNI work on a class or a struct that the call receives in place of a delegate.
A struct avoids a heap allocation and is the faster shape.

The samples use both. Classes: `JniCallback` and `MainActivity`. Structs: the GUI callbacks and `ToastRequest`.

On .NET 9.0 and later, these interfaces also accept `ref struct` implementations. Earlier versions accept a `class` or
a `struct`.

##### Topics

- [Frame Interfaces](#frame-interfaces)
- [JNI Native Calls](#jni-native-calls)
- [Fixed Frames](#fixed-frames)
- [Call Arguments](#call-arguments)
- [.NET for Android](#net-for-android)

---

## Frame Interfaces

`IFrameAction` and `IFrameFunction<TOutput>` are part of `Rxmxnx.JNetInterface.Core`. They run against an
`IEnvironment` and a span of reference objects that belong to the active frame.

| Interface                 | Method                                                  | Result                |
|---------------------------|---------------------------------------------------------|-----------------------|
| `IFrameAction`            | `Accept(IEnvironment, ReadOnlySpan<JReferenceObject?>)` | No result.            |
| `IFrameFunction<TOutput>` | `Apply(IEnvironment, ReadOnlySpan<JReferenceObject?>)`  | A value of `TOutput`. |

The span contains only the reference-type values supplied by the caller. Primitive values stay on the implementation.
A fixed frame passes an empty span. A JNI native call passes one `JReferenceObject` for each `JReferenceTypeToken`.

##### Notes

- A large struct can use the `ref` overload. On .NET 9.0 every overload accepts a `ref struct`.
- A `null` action or function is ignored.
- `ref struct` implementations are incompatible with Visual Basic .NET.

---

## JNI Native Calls

A Java native method runs on a call frame created by the JVM. `WithNativeFrame` enters that frame, materializes the
reference arguments, invokes an `IFrameAction` or `IFrameFunction<TOutput>`, and leaves the frame when the call
returns.

`WithNativeFrame` is available in `Rxmxnx.JNetInterface` and `Rxmxnx.JNetInterface.Mobile`.

```csharp
private sealed class RunAction : IFrameAction
{
    public void Accept(IEnvironment env, scoped ReadOnlySpan<JReferenceObject?> objects)
    {
        if (objects.IsEmpty || objects[0] is not JLocalObject instance)
            return;

        // JNI work for this native call uses instance.
    }
}

[UnmanagedCallersOnly]
private static void Run(JEnvironmentRef envRef, JObjectLocalRef localRef)
{
    RunAction action = new();
    envRef.WithNativeFrame(action, localRef);
}
```

The library and GUI samples implement these interfaces. `JniCallback` is a class. The GUI callbacks are structs.

Reference arguments are passed as `JReferenceTypeToken` values. The struct is defined in
`Rxmxnx.JNetInterface.Core` and is described in
[JReferenceTypeToken](jni-references.md#jreferencetypetoken). Well-known JNI local references convert implicitly:

| JNI reference           | Managed object in the span |
|-------------------------|----------------------------|
| `JObjectLocalRef`       | `JLocalObject`             |
| `JClassLocalRef`        | `JClassObject`             |
| `JStringLocalRef`       | `JStringObject`            |
| `JThrowableLocalRef`    | `JThrowableObject`         |
| `JArrayLocalRef`        | `JArrayObject`             |
| `JObjectArrayLocalRef`  | object array               |
| `JBooleanArrayLocalRef` | `JArrayObject<JBoolean>`   |
| `JByteArrayLocalRef`    | `JArrayObject<JByte>`      |
| `JCharArrayLocalRef`    | `JArrayObject<JChar>`      |
| `JDoubleArrayLocalRef`  | `JArrayObject<JDouble>`    |
| `JFloatArrayLocalRef`   | `JArrayObject<JFloat>`     |
| `JIntArrayLocalRef`     | `JArrayObject<JInt>`       |
| `JLongArrayLocalRef`    | `JArrayObject<JLong>`      |
| `JShortArrayLocalRef`   | `JArrayObject<JShort>`     |

When the token carries no metadata, the object is created from its runtime class using the closest mapped type.
`JNativeCallAdapter` keeps that lookup in `Rxmxnx.JNetInterface`. Sync and async calls keep it in
`Rxmxnx.JNetInterface.Mobile`. `Create<TReference>` attaches the metadata of a mapped type before the call:

```csharp
action.WithNativeFrame(envRef, JReferenceTypeToken.Create<JIntegerObject>(integerRef));
```

A default JNI reference becomes a `null` entry in the span. Primitive parameters stay on the functional value, and the
reference in the span decides what to add:

```csharp
[UnmanagedCallersOnly]
private static int Sum(JEnvironmentRef envRef, JObjectLocalRef localRef, int left, int right)
{
    SumFunction function = new(left, right);
    function.WithNativeFrame(envRef, out JInt result, localRef);
    return result.Value;
}

private readonly struct SumFunction(int left, int right) : IFrameFunction<JInt>
{
    public JInt Apply(IEnvironment env, scoped ReadOnlySpan<JReferenceObject?> objects)
    {
        int adjustment = objects[0] switch
        {
            null => -2,
            JStringObject => 4,
            JNumberObject => -1,
            JClassObject => 10,
            _ => 0,
        };
        return left + right + adjustment;
    }
}
```

### Overloads

| Overload                                                                    | Use                                                                                      |
|-----------------------------------------------------------------------------|------------------------------------------------------------------------------------------|
| `WithNativeFrame(this JEnvironmentRef, TAction?, ...)`                      | Class or struct. Resolves the environment from `JNIEnv*`.                                |
| `WithNativeFrame(this ref TAction, JEnvironmentRef, ...)`                   | Struct passed by reference. Resolves the environment from `JNIEnv*`.                     |
| `WithNativeFrame(this IVirtualMachine?, JEnvironmentRef, TAction?, ...)`    | Class or struct. Resolves the environment from an `IVirtualMachine` instance.            |
| `WithNativeFrame(this ref TAction, IVirtualMachine?, JEnvironmentRef, ...)` | Struct passed by reference. Resolves the environment from an `IVirtualMachine` instance. |

The `...` is the reference tokens. `IFrameFunction<TOutput>` uses the same shapes, and the `out` parameter comes before
those tokens:

| Overload                                                                           |
|------------------------------------------------------------------------------------|
| `WithNativeFrame(this JEnvironmentRef, TFunction?, out, ...)`                      |
| `WithNativeFrame(this ref TFunction, JEnvironmentRef, out, ...)`                   |
| `WithNativeFrame(this IVirtualMachine?, JEnvironmentRef, TFunction?, out, ...)`    |
| `WithNativeFrame(this ref TFunction, IVirtualMachine?, JEnvironmentRef, out, ...)` |

The `out` type follows `TOutput`:

| `TOutput`                     | `out` result            |
|-------------------------------|-------------------------|
| `IPrimitiveType<TResult>`     | `TResult`               |
| `JReferenceObject?`           | `JObjectLocalRef`       |
| `JClassObject?`               | `JClassLocalRef`        |
| `JStringObject?`              | `JStringLocalRef`       |
| `JThrowableObject?`           | `JThrowableLocalRef`    |
| `JArrayObject?`               | `JArrayLocalRef`        |
| `JArrayObject<JBoolean>?`     | `JBooleanArrayLocalRef` |
| `JArrayObject<JByte>?`        | `JByteArrayLocalRef`    |
| `JArrayObject<JChar>?`        | `JCharArrayLocalRef`    |
| `JArrayObject<JDouble>?`      | `JDoubleArrayLocalRef`  |
| `JArrayObject<JFloat>?`       | `JFloatArrayLocalRef`   |
| `JArrayObject<JInt>?`         | `JIntArrayLocalRef`     |
| `JArrayObject<JLong>?`        | `JLongArrayLocalRef`    |
| `JArrayObject<JShort>?`       | `JShortArrayLocalRef`   |
| `IArrayObject<ILocalObject>?` | `JObjectArrayLocalRef`  |

The `ref` rows pass a struct. `JEnvironmentRef` overloads are in `Rxmxnx.JNetInterface` and
`Rxmxnx.JNetInterface.Mobile`.

On .NET 9.0 and later the tokens are a `params ReadOnlySpan<JReferenceTypeToken>`. Earlier versions take one span. A
collection expression such as `[localRef, stringRef]` satisfies that parameter. See
[JReferenceTypeToken](jni-references.md#jreferencetypetoken).

### Return values

`IFrameAction` covers a `void` native method. A `null` reference result becomes a default JNI reference. That JNI
reference is copied out before the call frame ends, so the unmanaged method can return it to Java.

`JDouble` and `JFloat` can be the `TOutput` of the frame function. The unmanaged method that returns the value to Java
must still use `System.Double` or `System.Single`, because those JNI types are not ABI-compatible with their wrapper
structs. Read `Value` from the `out` result and return that CLR value.

##### Notes

- `IVirtualMachine` overloads are only available in `Rxmxnx.JNetInterface`. That instance is what lets `IEnvironment`
  initialize against the right JVM when the process hosts more than one, as with `JNativeCallAdapter`. A proxy
  instance is ignored.
- A default `JEnvironmentRef`, a `null` action or function, or a proxy `IVirtualMachine` skips the call. The `out`
  result of a skipped function call is left uninitialized.
- Local objects in the span and the `IEnvironment` should stay inside the call.
- `JNetInterface.DisableCheckRefTypeNativeCall` skips the check that an incoming reference is a local reference. The
  switch also covers `WithNativeFrame` in `Rxmxnx.JNetInterface` and `Rxmxnx.JNetInterface.Mobile`.
- `JNetInterface.DisableCheckClassRefNativeCall` skips the check that a class parameter is a `java.lang.Class<?>`. The
  switch also covers `WithNativeFrame` in `Rxmxnx.JNetInterface` and `Rxmxnx.JNetInterface.Mobile`.

---

## Fixed Frames

`WithFrame` runs the same frame interfaces inside a fixed local frame. It is part of `Rxmxnx.JNetInterface.Core`, so it
is available with either implementation package. The object span passed to `Accept` or `Apply` is empty; any state
lives on the functional value.

```csharp
private readonly struct LoadAction : IFrameAction
{
    public void Accept(IEnvironment env, scoped ReadOnlySpan<JReferenceObject?> objects)
    {
        // objects is empty.
    }
}

LoadAction action = new();
action.WithFrame(environment, 16);
```

| Extension                                                                              | Behavior                                                   |
|----------------------------------------------------------------------------------------|------------------------------------------------------------|
| `WithFrame(this IEnvironment?, Int32, TAction?)`                                       | Runs a class or a struct in a frame of the given capacity. |
| `WithFrame(this ref TAction, IEnvironment?, Int32)`                                    | Runs a struct passed by reference.                         |
| `WithFrame<TFunction, TResult>(this IEnvironment?, Int32, TFunction?, out TResult)`    | Runs a class or a struct and returns its result.           |
| `WithFrame<TFunction, TResult>(this ref TFunction, IEnvironment?, Int32, out TResult)` | Runs a struct passed by reference and returns its result.  |

The behavior of local references when the frame ends is documented in
[Environment Frames](jni-references.md#environment-frames).

`IEnvironment` also keeps the delegate overloads `WithFrame(Int32, Action)` and
`WithFrame<TResult>(Int32, Func<TResult>)`.
A struct implementation of the functional overload avoids that delegate allocation.

---

## Call Arguments

`ICallArgument` is part of `Rxmxnx.JNetInterface.Core`. It supplies the arguments of a call into Java.
`Configure<TSlot>(TSlot slot, JCallDefinition callDefinition)`
writes each argument into an `IParameterSlot`:

- `SetParameterValue<TObject>(Byte, TObject?)` writes one argument.
- `SetParameterValues<TObject>(Byte, ReadOnlySpan<TObject?>)` writes a sequence from the given index.
- `SetParameterNull(Byte)` writes a `null` argument.

`ICallArgument.Empty`, also available as `ICallArgument.Default`, configures a parameterless call.

`ICallArgument` is an optimization for primitive arguments. A primitive stored in `ReadOnlySpan<IObject?>` is boxed,
because the span holds the `IObject` interface. An `ICallArgument` struct keeps each primitive in its own field and
writes it through the generic `SetParameterValue<TObject>`, so the call stays unboxed.

A reference argument uses the `ReadOnlySpan<IObject?>` overload. A reference type is already a reference, so that span
does not box it. That is the appropriate overload for a single reference argument:

```csharp
call.MethodCall(instance, [text]);
```

Each primitive wrapper implements `ICallArgument` and writes itself at index `0`. A call whose only argument is a
primitive passes that wrapper directly:

```csharp
call.MethodCall(instance, (JInt)42);
```

This applies to `JBoolean`, `JByte`, `JChar`, `JDouble`, `JFloat`, `JInt`, `JLong`, and `JShort`.

Several primitives, of one type or of different types, use a custom struct. The same struct also carries a mix of
primitives and references. Each primitive stays in its field, and each reference is stored as a reference, so the span
would box only the primitives of that mix.

```csharp
internal readonly struct IntAndLong(JInt index, JLong size) : ICallArgument
{
    public void Configure<TSlot>(TSlot slot, JCallDefinition callDefinition) where TSlot : IParameterSlot
    {
        slot.SetParameterValue(0, index);
        slot.SetParameterValue(1, size);
    }
}

internal readonly struct IndexAndText(JInt index, JStringObject? text) : ICallArgument
{
    public void Configure<TSlot>(TSlot slot, JCallDefinition callDefinition) where TSlot : IParameterSlot
    {
        slot.SetParameterValue(0, index);
        slot.SetParameterValue(1, text);
    }
}

IntAndLong args = new(index, size);
call.MethodCall(instance, ref args);
```

The `ref` overload passes the struct through to the call and is the overload constrained to a struct. Use the `ref`
overload when the argument value is large enough that copying it matters. On .NET 9.0 and later it also accepts a
`ref struct`.

A `null` argument value selects `ICallArgument.Default`. A class or a struct can implement `ICallArgument`. The `ref`
overload receives a struct.

### `IndeterminateCall`

These public methods accept `TArgs` by value, and a `ref TArgs` overload when `TArgs` is a struct.

| Method                                                             | Call                                                                     |
|--------------------------------------------------------------------|--------------------------------------------------------------------------|
| `MethodCall<TArgs>(JLocalObject, TArgs?)`                          | Instance method.                                                         |
| `MethodCall<TArgs>(JLocalObject, JClassObject, Boolean, TArgs?)`   | Instance method on a superclass. The Boolean selects a non-virtual call. |
| `StaticMethodCall<TArgs>(JClassObject, TArgs?)`                    | Static method.                                                           |
| `FunctionCall<TArgs>(JLocalObject, TArgs?)`                        | Instance function. Returns `IndeterminateResult`.                        |
| `FunctionCall<TArgs>(JLocalObject, JClassObject, Boolean, TArgs?)` | Instance function on a superclass.                                       |
| `StaticFunctionCall<TArgs>(JClassObject, TArgs?)`                  | Static function.                                                         |
| `NewCall<TArgs>(JClassObject, TArgs?)`                             | Constructor. Returns `JLocalObject`.                                     |
| `NewCall<TObject, TArgs>(IEnvironment, TArgs?)`                    | Constructor of `TObject`.                                                |

### Call definitions

`JMethodDefinition`, `JFunctionDefinition<TResult>`, `JNonTypedFunctionDefinition`, and `JConstructorDefinition`
expose the same argument type through protected methods. Derived definitions call them. Each method has a by-value
overload and a `ref` overload constrained to a struct.

| Definition                     | Protected methods                                                                                                                                 |
|--------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------|
| `JMethodDefinition`            | `Invoke`, `Invoke` with a superclass, `InvokeNonVirtual`, `StaticInvoke`, `InvokeReflected`, `InvokeNonVirtualReflected`, `InvokeStaticReflected` |
| `JFunctionDefinition<TResult>` | The same set. Each method returns `TResult`.                                                                                                      |
| `JNonTypedFunctionDefinition`  | The same set. The result is an object reference.                                                                                                  |
| `JConstructorDefinition`       | `New`, `New<TObject>`, `NewReflected`, `NewReflected<TObject>`                                                                                    |

`ReadOnlySpan<IObject?>` is documented in [Java Member Handling](jni-accessing.md). Use it for reference arguments.
Use `ICallArgument` when the call includes primitives, either alone or mixed with references, and those primitives
should stay unboxed. `Parameterless` definitions call the empty argument internally.

---

## .NET for Android

`IAndroidNativeAction` and `IAndroidNativeFunction<TOutput>` run inside an `AndroidJniContext`. They complement the
`AndroidJniAction` and `AndroidJniFunc` delegates documented in [.NET for Android Interoperability](mobile-runtime.md).

| Interface                         | Method                      |
|-----------------------------------|-----------------------------|
| `IAndroidNativeAction`            | `Accept(AndroidJniContext)` |
| `IAndroidNativeFunction<TOutput>` | `Apply(AndroidJniContext)`  |

The mobile test uses both shapes. `MainActivity` implements `IAndroidNativeFunction<TOutput>` and passes itself to
`Invoke`:

```csharp
AndroidJniHost.CreateSyncContext().With(activity).Invoke(activity, out MobileState state);
```

`ToastRequest` implements `IAndroidNativeAction` as a struct and is posted to another thread:

```csharp
await AndroidJniHost.CreateAsyncContext().Post(synchronizationContext, new ToastRequest
{
    AndroidContext = state.AndroidContext,
    Text = text,
});
```

Synchronous calls and `Send` accept a struct passed by `ref`. On .NET 9.0 those overloads also allow `ref struct`. A
function result is returned through an `out` parameter. A `null` action or function is ignored, and a skipped function
leaves the result uninitialized.

`InvokeAsync`, `Run`, and `Post` accept a class or a struct. Their signatures are listed in
[.NET for Android Interoperability](mobile-runtime.md#asynccontextbuilder-structure).

| Method                                                                 | Execution                                                                                |
|------------------------------------------------------------------------|------------------------------------------------------------------------------------------|
| `InvokeAsync<TAction>(TAction, Boolean)`                               | Runs a class or a struct on a `Rxmxnx.JNetInterface.Mobile` thread and returns a `Task`. |
| `InvokeAsync<TResult, TFunction>(TFunction, Boolean)`                  | Runs a class or a struct and returns a `Task<TResult>`.                                  |
| `Run<TAction>(TAction, out Task)`                                      | Runs a class or a struct and exposes the `Task` through `out`.                           |
| `Post<TAction>(SynchronizationContext, TAction, Boolean)`              | Posts a class or a struct and returns a `Task`.                                          |
| `Post<TResult, TFunction>(SynchronizationContext, TFunction, Boolean)` | Posts a class or a struct and returns a `Task<TResult>`.                                 |

The optional Boolean selects weak global references for the values passed into the context. It defaults to `true` for
`InvokeAsync` and `Post`. `Run` uses strong references.

A function that must return a `Java.Interop` object uses the existing `AndroidJniFunc<JReferenceObject>` overloads.
Those overloads convert the `JReferenceObject` before the temporary context releases its local references.

JNI native methods in a .NET for Android process use `WithNativeFrame`, as described in
[JNI Native Calls](#jni-native-calls). The mobile extensions are listed in
[JNI Extensions](mobile-runtime.md#jni-extensions).
