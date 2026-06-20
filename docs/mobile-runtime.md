# .NET for Android Interoperability

Because `.NET for Android` internally relies on the `Java.Interop` JNI wrapper, the standard implementation of
`Rxmxnx.JNetInterface` is not compatible with it due to differences in thread ownership and lifecycle management.

To address these limitations, `Rxmxnx.JNetInterface.Mobile` provides a `.NET for Android`-specific implementation that
integrates safely with `Java.Interop` without interfering with its management of JNI threads and references.

The library is built around the following public types, which allow JNI APIs to be used safely while preserving the
runtime behavior provided by `Java.Interop`.

**Note**: `Rxmxnx.JNetInterface.Mobile` is not compatible with Visual Basic .NET because it relies extensively on
`ref struct` types.

## `AndroidJniHost` Class

The `AndroidJniHost` class implements `IVirtualMachine` and provides additional APIs for managing JVM references and
creating JNI execution contexts.

### Static Members

- **`ApiLevel`**: The Android API level of the current runtime. This property is not affected by the
  `JNetInterface.FixedRuntime.*` feature switches.
- **`TraceEnabled`**: Indicates whether the `JNetInterface.EnableTrace` feature switch is active.
    - This enables tracking of JNI calls via `System.Diagnostics.Trace`.
- **`FinalUserTypeRuntimeEnabled`**: Indicates whether `JNetInterface.EnableFinalUserTypeRuntime` is active.
    - This allows assuming final mapped data types without verifying their actual class.
- **`MainClassesInformation`**: Provides metadata for main-class types.
- **`Register<TReference>()`**: Registers the metadata of a mapped reference type at runtime.
- **`SetMainClass<TReference>()`**: Marks a mapped reference type as a main class.
- **`CreateSyncContext()`**: Creates a `SyncContextBuilder`.
- **`CreateAsyncContext()`**: Creates a `AsyncContextBuilder`.

## `AndroidJniContext` Structure

`AndroidJniContext` represents the safe execution context in which JNI calls can be performed through
`Rxmxnx.JNetInterface.Mobile`.

It also acts as a bridge between a pure `.NET for Android` execution context (managed by `Java.Interop`) and a
`Rxmxnx.JNetInterface` execution context, allowing primitive values and Java object references to be transferred
between them.

### Properties

| Property      | Description                                                                |
|---------------|----------------------------------------------------------------------------|
| `Environment` | The `IEnvironment` instance associated with the current execution context. |
| `Booleans`    | Assigned `boolean` primitive values.                                       |
| `Bytes`       | Assigned `byte` primitive values.                                          |
| `Chars`       | Assigned `char` primitive values.                                          |
| `Shorts`      | Assigned `short` primitive values.                                         |
| `Ints`        | Assigned `int` primitive values.                                           |
| `Longs`       | Assigned `long` primitive values.                                          |
| `Floats`      | Assigned `float` primitive values.                                         |
| `Doubles`     | Assigned `double` primitive values.                                        |
| `Objects`     | Assigned Java object references.                                           |

`AndroidJniContext` is only available through the following delegate types:

* `AndroidJniAction`
* `AndroidJniAction<TState>`
* `AndroidJniFunc<TResult>`
* `AndroidJniFunc<TState, TResult>`

Instances of `AndroidJniContext` are created exclusively through `SyncContextBuilder` and `AsyncContextBuilder`.

## `SyncContextBuilder` and `AsyncContextBuilder` Structures

These structures construct `Rxmxnx.JNetInterface` execution contexts at runtime from within `.NET for Android` code.

Values originating from the `Java.Interop` execution context can be supplied through one or more `With(...)` calls
before executing the constructed JNI context.

### `SyncContextBuilder` Structure

`SyncContextBuilder` is a ref struct that creates a JNI execution context to be executed synchronously on a thread
managed by `Java.Interop`.

#### `With(...)` Methods

The following overload families are available:

| Overload family                                                                                                        | Assigned values             |
|------------------------------------------------------------------------------------------------------------------------|-----------------------------|
| `With(ReadOnlySpan<JBoolean>)` / `With(params JBoolean[])`<br>`With(ReadOnlySpan<Boolean>)` / `With(params Boolean[])` | `boolean` primitive values. |
| `With(ReadOnlySpan<JByte>)` / `With(params JByte[])`<br>`With(ReadOnlySpan<SByte>)` / `With(params SByte[])`           | `byte` primitive values.    |
| `With(ReadOnlySpan<JChar>)` / `With(params JChar[])`<br>`With(ReadOnlySpan<Char>)` / `With(params Char[])`             | `char` primitive values.    |
| `With(ReadOnlySpan<JShort>)` / `With(params JShort[])`<br>`With(ReadOnlySpan<Int16>)` / `With(params Int16[])`         | `short` primitive values.   |
| `With(ReadOnlySpan<JInt>)` / `With(params JInt[])`<br>`With(ReadOnlySpan<Int32>)` / `With(params Int32[])`             | `int` primitive values.     |
| `With(ReadOnlySpan<JLong>)` / `With(params JLong[])`<br>`With(ReadOnlySpan<Int64>)` / `With(params Int64[])`           | `long` primitive values.    |
| `With(ReadOnlySpan<JFloat>)` / `With(params JFloat[])`<br>`With(ReadOnlySpan<Single>)` / `With(params Single[])`       | `float` primitive values.   |
| `With(ReadOnlySpan<JDouble>)` / `With(params JDouble[])`<br>`With(ReadOnlySpan<Double>)` / `With(params Double[])`     | `double` primitive values.  |
| `With(ReadOnlySpan<IJavaPeerable>)` / `With(params IJavaPeerable[])`                                                   | Java object references.     |

Primitive values may be supplied either as JNI wrapper types (`JInt`, `JBoolean`, etc.) or as their corresponding CLR
primitive types.

#### Execution Methods

`SyncContextBuilder` provides two execution models:

| Method        | Execution                                                                                                                                   |
|---------------|---------------------------------------------------------------------------------------------------------------------------------------------|
| `Invoke(...)` | Executes the constructed JNI context immediately on the current thread managed by `Java.Interop`.                                           |
| `Send(...)`   | Executes the constructed JNI context synchronously on the specified `SynchronizationContext`, blocking the calling thread until completion. |

Each execution model supports the following delegate families:

| Delegate                                   | `Invoke` signature                                                 | `Send` signature                                                                         | Description                                                                                                      |
|--------------------------------------------|--------------------------------------------------------------------|------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------|
| `AndroidJniAction`                         | `Invoke(AndroidJniAction)`                                         | `Send(SynchronizationContext, AndroidJniAction)`                                         | Executes without state or return value.                                                                          |
| `AndroidJniAction<TState>`                 | `Invoke<TState>(TState, AndroidJniAction<TState>)`                 | `Send<TState>(SynchronizationContext, TState, AndroidJniAction<TState>)`                 | Executes using a state object.                                                                                   |
| `AndroidJniFunc<TResult>`                  | `Invoke<TResult>(AndroidJniFunc<TResult>)`                         | `Send<TResult>(SynchronizationContext, AndroidJniFunc<TResult>)`                         | Returns a managed value.                                                                                         |
| `AndroidJniFunc<TState, TResult>`          | `Invoke<TState, TResult>(TState, AndroidJniFunc<TState, TResult>)` | `Send<TState, TResult>(SynchronizationContext, TState, AndroidJniFunc<TState, TResult>)` | Returns a managed value using a state object.                                                                    |
| `AndroidJniFunc<JReferenceObject>`         | `Invoke<TResult>(AndroidJniFunc<JReferenceObject>)`                | `Send<TResult>(SynchronizationContext, AndroidJniFunc<JReferenceObject>)`                | Returns a `Rxmxnx.JNetInterface` JNI object converted into a `Java.Interop`-managed object.                      |
| `AndroidJniFunc<TState, JReferenceObject>` | `Invoke<TState>(TState, AndroidJniFunc<TState, JReferenceObject>)` | `Send<TState>(SynchronizationContext, TState, AndroidJniFunc<TState, JReferenceObject>)` | Returns a `Rxmxnx.JNetInterface` JNI object converted into a `Java.Interop`-managed object using a state object. |

The values returned by delegates producing `JReferenceObject` instances are automatically converted into equivalent
objects managed by `Java.Interop` before being returned to the original execution context.

### `AsyncContextBuilder` Structure

`AsyncContextBuilder` is a `struct` that creates a JNI execution context for asynchronous execution on a thread managed
by `Rxmxnx.JNetInterface.Mobile`.

The calling thread must be managed by `Java.Interop`.

#### `With(...)` Methods

The following overload families are available:

| Overload family                                      | Assigned values             |
|------------------------------------------------------|-----------------------------|
| `With(params JBoolean[])` / `With(params Boolean[])` | `boolean` primitive values. |
| `With(params JByte[])` / `With(params SByte[])`      | `byte` primitive values.    |
| `With(params JChar[])` / `With(params Char[])`       | `char` primitive values.    |
| `With(params JShort[])` / `With(params Int16[])`     | `short` primitive values.   |
| `With(params JInt[])` / `With(params Int32[])`       | `int` primitive values.     |
| `With(params JLong[])` / `With(params Int64[])`      | `long` primitive values.    |
| `With(params JFloat[])` / `With(params Single[])`    | `float` primitive values.   |
| `With(params JDouble[])` / `With(params Double[])`   | `double` primitive values.  |
| `With(params IJavaPeerable[])`                       | Java object references.     |

Primitive values may be supplied either as JNI wrapper types (`JBoolean`, `JInt`, etc.) or as their corresponding CLR
primitive types.

#### Execution Methods

`AsyncContextBuilder` provides three execution models:

| Method               | Execution                                                                                                                                                                     |
|----------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `InvokeAsync(...)`   | Creates a new thread managed by `Rxmxnx.JNetInterface.Mobile`, executes the constructed JNI execution context, and returns a `Task` representing the operation.               |
| `Run(..., out Task)` | Creates a new thread managed by `Rxmxnx.JNetInterface.Mobile`, executes the constructed JNI execution context, and returns the associated `Task` through the `out` parameter. |
| `Post(...)`          | Posts the constructed JNI execution context to the specified `SynchronizationContext` without blocking the calling thread.                                                    |

Each execution model supports the following delegate families:

| Delegate                                   | `InvokeAsync` signature                                                          | `Run` signature                                           | `Post` signature                                                                                  | Description                                                                                                                                                                                                    |
|--------------------------------------------|----------------------------------------------------------------------------------|-----------------------------------------------------------|---------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `AndroidJniAction`                         | `InvokeAsync(AndroidJniAction, Boolean)`                                         | `Run(AndroidJniAction, out Task)`                         | `Post(SynchronizationContext, AndroidJniAction, Boolean)`                                         | Executes without state or a return value. `InvokeAsync` and `Post` return a `Task` that can be awaited, whereas `Run` returns `void` and exposes the underlying `Task` through the `out` parameter.            |
| `AndroidJniAction<TState>`                 | `InvokeAsync<TState>(TState, AndroidJniAction<TState>, Boolean)`                 | `Run<TState>(TState, AndroidJniAction<TState>, out Task)` | `Post<TState>(SynchronizationContext, TState, AndroidJniAction<TState>, Boolean)`                 | Executes using a state object. `InvokeAsync` and `Post` return a `Task` that can be awaited, whereas `Run` returns `void` and exposes the underlying `Task` through the `out` parameter.                       |
| `AndroidJniFunc<TResult>`                  | `InvokeAsync<TResult>(AndroidJniFunc<TResult>, Boolean)`                         | —                                                         | `Post<TResult>(SynchronizationContext, AndroidJniFunc<TResult>, Boolean)`                         | Returns a managed value. Both `InvokeAsync` and `Post` return a `Task<TResult>` that can be awaited.                                                                                                           |
| `AndroidJniFunc<TState, TResult>`          | `InvokeAsync<TState, TResult>(TState, AndroidJniFunc<TState, TResult>, Boolean)` | —                                                         | `Post<TState, TResult>(SynchronizationContext, TState, AndroidJniFunc<TState, TResult>, Boolean)` | Returns a managed value using a state object. Both `InvokeAsync` and `Post` return a `Task<TResult>` that can be awaited.                                                                                      |
| `AndroidJniFunc<JReferenceObject>`         | `InvokeAsync(AndroidJniFunc<JReferenceObject>, Boolean)`                         | —                                                         | `Post(SynchronizationContext, AndroidJniFunc<JReferenceObject>, Boolean)`                         | Returns a `Rxmxnx.JNetInterface` JNI object converted into an equivalent `Java.Interop`-managed object. Both `InvokeAsync` and `Post` return a `Task<T>` containing the converted object.                      |
| `AndroidJniFunc<TState, JReferenceObject>` | `InvokeAsync<TState>(TState, AndroidJniFunc<TState, JReferenceObject>, Boolean)` | —                                                         | `Post<TState>(SynchronizationContext, TState, AndroidJniFunc<TState, JReferenceObject>, Boolean)` | Returns a `Rxmxnx.JNetInterface` JNI object converted into an equivalent `Java.Interop`-managed object using a state object. Both `InvokeAsync` and `Post` return a `Task<T>` containing the converted object. |

Delegates returning `JReferenceObject` automatically convert the resulting JNI object into the corresponding
`Java.Interop`-managed object before returning it to the original execution context.

The final optional boolean parameter of `InvokeAsync` and `Post` determines whether the JNI references passed to the
created context are weak global references. By default, this parameter is `true`.

## Execution Delegates

Execution delegates receive an `AndroidJniContext` instance, providing a safe execution context for performing JNI
operations.

| Delegate                          | Signature                             | Description                                                                                                    |
|-----------------------------------|---------------------------------------|----------------------------------------------------------------------------------------------------------------|
| `AndroidJniAction`                | `void (AndroidJniContext)`            | Represents a JNI action that neither receives a state object nor returns a value.                              |
| `AndroidJniAction<TState>`        | `void (AndroidJniContext, TState)`    | Represents a JNI action that receives a state object of type `TState` and does not return a value.             |
| `AndroidJniFunc<TResult>`         | `TResult (AndroidJniContext)`         | Represents a JNI function that returns a value of type `TResult` and does not receive a state object.          |
| `AndroidJniFunc<TState, TResult>` | `TResult (AndroidJniContext, TState)` | Represents a JNI function that receives a state object of type `TState` and returns a value of type `TResult`. |

When an execution delegate completes, every local JNI reference created and managed by `Rxmxnx.JNetInterface.Mobile`
during its execution is automatically released.

For this reason, delegates returning `JReferenceObject` are supported by `SyncContextBuilder` and `AsyncContextBuilder`.
Before returning to the original execution context, the resulting JNI object is automatically converted into an
equivalent object managed by `Java.Interop`, allowing it to outlive the temporary `Rxmxnx.JNetInterface.Mobile`
execution context.

## JNI Extensions

The `AndroidJniExtensions` class provides extension methods that further integrate `Rxmxnx.JNetInterface.Mobile` with
`Java.Interop`.

| Extension method                | Description                                                                                                                                                                                                                |
|---------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `IsValid(this JGlobalBase)`     | Determines whether a `JGlobalBase` instance from `Rxmxnx.JNetInterface.Mobile` is still valid from a pure `.NET for Android` execution context.                                                                            |
| `ToJniObject(this JGlobalBase)` | Creates a `Java.Interop`-managed object from a `JGlobalBase` instance. The created object owns its own local JNI reference managed by `Java.Interop` and is completely independent of the original `JGlobalBase` instance. |
