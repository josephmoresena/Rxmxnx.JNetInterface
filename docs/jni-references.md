# JNI Reference Handling

All instances of global and weak global objects, as well as local objects or their local views (`ILocalObject`),
implement the `IDisposable` interface. When calling the `Dispose` method, `Rxmxnx.JNetInterface` will remove the JNI
references associated with these instances.

However, in some cases, calling `Dispose` might not immediately remove the JNI references. This can occur due to
the following reasons:

- The JNI reference is being used by another thread. This applies to global and weak global references.
- The JNI reference is required for native memory guarantees by the JVM. The native memory associated with the
  reference must be released before calling `Dispose()`.
- The JNI reference is shared among multiple `JLocalObject` instances. In this case, `Dispose()` must be called on
  each `JLocalObject` instance.
- The `JLocalObject` or `ILocalObject` instance does not have a loaded local reference. The active reference is held
  by an associated global or weak global reference.
- The `JLocalObject` or `ILocalObject` instance was created within the scope of a call from Java to JNI as a parameter.
  The behavior of JNI native call adapters will be detailed later.

**Note:** In some cases, it may not be necessary to explicitly release local references created within the active
environment frame, or some references may be released without calling the `Dispose()` method.

- [Local Reference Handling](#local-reference-handling)
    - [Environment Frames](#environment-frames)
        - [Call Frame](#call-frame)
        - [Fixed Frame](#fixed-frame)
- [Global Reference Handling](#global-reference-handling)
- [Global-Weak Reference Handling](#global-weak-reference-handling)

## Local Reference Handling

All JNI calls that return references to Java objects return local references. These references remain valid as long
as the active environment frame at the time of their creation is maintained or explicitly removed. Local JNI references
are valid only for the environment thread that created them. This is why, in `Rxmxnx.JNetInterface`, all `JLocalObject`
or `ILocalObject` instances are bound to a specific `IEnvironment` instance.

### Environment Frames

Environment frames are memory spaces where local references are stored. Although JNI does not impose a fixed limit on
the number of local JNI references an environment can create, the active frame may have such a limit.

There are three types of frames:

- **Initial Frame:** The initial frame is the active frame when a thread associates with the JVM from JNI and the
  invocation interface. This frame remains valid, along with all its associated local references, as long as the thread
  remains associated with the JVM.
- **Call Frame:** The call frame is a frame created by the JVM when invoking Java native calls. To notify
  `Rxmxnx.JNetInterface` of the creation of a temporary frame, a `JNativeCallAdapter` instance must be created and
  kept active throughout the duration of the native call.
- **Fixed Frame:** The fixed frame is a programmatically created frame using JNI that can only hold a predefined number
  of local references. If JNI calls during the frame’s duration generate more local references than the set limit, it
  will behave as a FIFO system, invalidating older references to accommodate the newly created ones.

The `IEnvironment` interface exposes the `LocalCapacity` property, which allows checking the set capacity of local
references for the current frame or ensuring that the current frame supports such a capacity.

**Notes:**

- The initial value of `LocalCapacity` will be `null` when the active frame is either the initial or a call frame,
  as this capacity is undetermined.
- The initial value of `LocalCapacity` in a fixed frame is the value set at the time of frame creation.
- The `LocalCapacity` value can only be increased beyond the previously established value.
- Attempting to set `LocalCapacity` in a fixed frame will throw an `InvalidOperationException`.
- Setting `LocalCapacity` when the active frame is either the initial or a call frame triggers the JNI
  `EnsureLocalCapacity` call. If this call fails, a `JniException` will be thrown.
- A single environment can only have one active frame at a time. However, each frame is initialized on top of the active
  frame at the moment of its creation, which is why it is possible to use local references from parent frames.
- Once the active frame is finalized, its parent frame will become active again. Any references created within the
  finalized frame will no longer be valid.

#### Call Frame

As mentioned earlier, this type of frame is established by the JVM for the environment when executing a Java native
call.

The native implementation of a Java call must follow specific naming and parameter conventions.

The parameters of a native call are:

1. A reference to the environment (`JEnvironmentRef`).
2. A local reference to the instance of the object (`JObjectLocalRef`) on which the method is executed.
3. Additional parameters depend on the signature of the call. For example, if the native declaration in Java is:

- `()`: No additional parameters.
- `(String a)`: `JStringLocalRef`.
- `(boolean a)`: `JBoolean`.
- `(Class<?>)`: `JClassLocalRef`.
- `(Character a)`: `JObjectLocalRef`.
- `<T>(T a)`: `JObjectLocalRef`.
- `<T>(T[] a, boolean[] b)`: `JObjectArrayLocalRef`, `JBooleanArrayLocalRef`.
- `<T extends Number>(T a, char b)`: `JObjectLocalRef`, `JChar`.
- `(int a, String b, long c, int[] d, Integer[] e)`: `JInt`, `JStringLocalRef`, `JLong`, `JIntArrayLocalRef`,
  `JArrayLocalRef`.

*Note:* The JNI convention for native call naming will be detailed later.

To create the call frame representation in `Rxmxnx.JNetInterface`, any static `Create` method of `JNativeCall` should be
used, followed by `Build()`.

- `Create(JEnvironmentRef)`: If the `IVirtualMachine` instance is not cached and the class or method is not relevant.
- `Create(JEnvironmentRef, JObjectLocalRef, out JLocalObject)`: Same as above, but the method is known to be an instance
  method.
- `Create(JEnvironmentRef, JClassLocalRef, out JClassObject)`: Similar to the first but for static methods.
- `Create<TObject>(JEnvironmentRef, JObjectLocalRef, out TObject)`: Same as the second, but with a known object type.
- `Create(IVirtualMachine, JEnvironmentRef)`: Similar to the first, but with a cached `IVirtualMachine` instance.
- `Create(IVirtualMachine, JEnvironmentRef, out JLocalObject)`: Same as the second, but with a cached `IVirtualMachine`
  instance.
- `Create(IVirtualMachine, JEnvironmentRef, JClassLocalRef, out JClassObject)`: Same as the third, but with a cached
  `IVirtualMachine` instance.
- `Create<>(IVirtualMachine, JEnvironmentRef, JObjectLocalRef, out TObject)`: Same as the fourth, but with a cached
  `IVirtualMachine` instance.

**Note:** Methods using `IVirtualMachine` are more efficient since `Rxmxnx.JNetInterface` supports multiple JVM
instances.

These methods return a `JNativeCallAdapter.Builder` instance, allowing further call description.

For the examples above, the creation sequence would be:

- `.Build()`: The call has no parameters.
- `.WithParameter(JStringLocalRef, out JStringObject).Build()`: Adds a `java.lang.String` parameter.
- `.Build()`: Although the call has a parameter, it's a primitive type and is omitted.
- `.WithParameter(JClassLocalRef, out JClassObject).Build()`: Adds a `java.lang.Class` parameter.
- `.WithParameter<JCharacterObject>(JObjectLocalRef, out JCharacterObject).Build()`: Adds a `java.lang.Character`
  parameter.
- `.WithParameter(JObjectLocalRef, out JLocalObject).Build()`: Adds a `java.lang.Object` parameter.
- `.WithParameter<JLocalObject>(JArrayObjectLocalRef, out JArrayObject<JLocalObject>).WithParameter(
 JBooleanArrayLocalRef, out JArrayObject<JBoolean>).Build()`: Adds `java.lang.Object[]` and `boolean[]` parameters.
- `.WithParameter<JNumberObject>(JObjectLocalRef, out JNumberObject).Build()`: Adds a `java.lang.Number` parameter.
- `.WithParameter(JStringLocalRef, out JStringObject).WithParameter(JIntArrayLocalRef, out JArrayObject<JInt>)
 .WithParameter<JIntegerObject>(JArrayLocalRef, out JArrayObject<JIntegerObject>).Build()`: Adds `java.lang.String`,
  `int[]`, and `java.lang.Integer[]` parameters.

**Note:** Local references associated with the target instance/class and parameters cannot be manually released.
Calling `.Dispose()` has no effect on these references.

To finalize a call (and remove the call frame in `Rxmxnx.JNetInterface`), the following methods can be used:

- `FinalizeCall()`: Ends a call and removes the associated instances within `Rxmxnx.JNetInterface` (without removing
  references).
- `FinalizeCall<TPrimitive>(TPrimitive value)`: Finalizes a call with a primitive result (boolean, byte, char,
  double, float, int, long, short).
- `FinalizeCall(JLocalObject?)`: Finalizes a call with a result of type java.lang.Object.
- `FinalizeCall(JLocalObject.InterfaceView)`: Finalizes a call with a result of type java.lang.Object.
- `FinalizeCall(JClassObject?)`: Finalizes a call with a result of type java.lang.Class<?>.
- `FinalizeCall(JThrowableObject?)`: Finalizes a call with a result of type java.lang.Throwable.
- `FinalizeCall(JStringObject?)`: Finalizes a call with a result of type java.lang.String.
- `FinalizeCall(JArrayObject)`: Finalizes a call with a result of type array.
- `FinalizeCall(JArrayObject<JBoolean>?)`: Finalizes a call with a result of type boolean[].
- `FinalizeCall(JArrayObject<JByte>?)`: Finalizes a call with a result of type byte[].
- `FinalizeCall(JArrayObject<JChar>?)`: Finalizes a call with a result of type char[].
- `FinalizeCall(JArrayObject<JDouble>?)`: Finalizes a call with a result of type double[].
- `FinalizeCall(JArrayObject<JFloat>?)`: Finalizes a call with a result of type float[].
- `FinalizeCall(JArrayObject<JInt>?)`: Finalizes a call with a result of type int[].
- `FinalizeCall(JArrayObject<JLong>?)`: Finalizes a call with a result of type long[].
- `FinalizeCall(JArrayObject<JShort>?)`: Finalizes a call with a result of type short[].
- `FinalizeCall<TElement>(JArrayObject<TElement>?)`: Finalizes a call with a result of type java.lang.Object[].

**Notes:**

- Both `JNativeCallAdapter` and `JNativeCallAdapter.Builder` are `ref struct` types, making them incompatible
  with the Visual Basic .NET language.
- Once the `Build()` method is called, it is always required to call the `Finalize` method on the created instance.
  Failing to do so may affect the behavior of the `IEnvironment` instance, as it could treat invalid local references as
  immutable.

#### Fixed Frame

This type of frame allows setting the maximum number of local references the environment can hold while it remains the
active frame.
Furthermore, as previously mentioned, it functions as a FIFO system in which, if a new local reference needs to be
stored and the frame is already full, the oldest reference is invalidated by JNI.

Creating this type of frame uses the JNI `PushLocalFrame` call, and its finalization uses the JNI `PopLocalFrame` call.

In `Rxmxnx.JNetInterface`, due to the nature of this frame, executions using it are performed through delegates.

To create or use a fixed frame, the `IEnvironment` interface offers the following options:

- `WithFrame(Int32, Action)`: Executes the delegate within the scope of a fixed frame with the specified capacity.
- `WithFrame<TState>(Int32, TState, Action<TState>)`: Executes the delegate, passing a state object within the scope of
  a fixed frame with the specified capacity.
- `WithFrame<TResult>(Int32, Func<TResult>)`: Executes the delegate within the scope of a fixed frame with the specified
  capacity and returns its result.
- `WithFrame<TResult, TState>(Int32, Func<TResult, TState>)`: Executes the delegate, passing a state object within the
  scope of a fixed frame with the specified capacity and returns its result.

**Notes:**

- It is more efficient if delegate instances come
  from [static methods](https://devblogs.microsoft.com/dotnet/understanding-the-cost-of-csharp-delegates/).
- In .NET 9.0+, the generic state type parameter allows `ref struct`.

## Global Reference Handling

Global reference management in JNI with `Rxmxnx.JNetInterface` is handled through `JGlobal` instances.

To create (or obtain) the `JGlobal` instance associated with a `JLocalObject` instance, use the `Global` property.
To remove the global reference from a `JGlobal` instance, call its `.Dispose()` method.

**Notes:**

- The `JGlobal` instance of `JClassObject` instances is shared among all instances of the same class across all
  environments of the same JVM.
- `JGlobal` instances associated with `JClassObject` instances are not collected by the GC, even if their global
  reference is removed.
- If a `JLocalObject` instance is associated with a `JGlobal` instance, its validity is randomly checked.
  If found invalid, the reference is removed, and if possible, a new one is created.
- `JGlobal` instances associated with `JClassObject` instances of classes marked as main are not validated.
- If a valid `JGlobal` instance is associated with a `JLocalObject` instance, that reference will always be used, even
  if a local reference is loaded for that instance.

## Global-Weak Reference Handling

Global weak reference management in JNI with `Rxmxnx.JNetInterface` is handled through `JWeak` instances.

To create (or obtain) the `JWeak` instance associated with a `JLocalObject` instance, use the `Global` property.
To remove the global reference from a `JWeak` instance, call its `.Dispose()` method.

** Notes: **

- If a `JLocalObject` instance is associated with a `JWeak` instance, its validity is randomly checked.
  If found invalid, the reference is removed, and if possible, a new one is created. This check is performed
  much more frequently than for `JGlobal` instances.
- If a valid `JWeak` instance is associated with a `JLocalObject` instance, that reference will always be used, even if
  a local reference is loaded for that instance, unless a valid `JGlobal` instance exists for the same object.