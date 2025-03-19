# Direct Buffer Handling

Since JNI 1.4, native handling of Java NIO (New Input/Output) buffers and the creation of direct buffers have been
supported.

The NIO object hierarchy may vary depending on the JVM implementation, but in `Rxmxnx.JNetInterface`, the following
hierarchy is established:

![BufferHierarchy](https://github.com/user-attachments/assets/c9fc3743-da67-47ea-aa00-a4c4bf633d92)

## `JBufferObject` Properties

The `JBufferObject` class exposes the following properties:

- **`IsDirect`**: Indicates whether the buffer is direct or managed by the JVM.
- **`Capacity`**: Represents the buffer's capacity in units of its primitive type.
- **`Address`**: Points to the buffer's memory. This is only valid when the buffer is direct.

##### Note

If this feature is not needed, it is recommended to disable the automatic registration of NIO types to improve
performance. This can be done using the feature switch `JNetInterface.DisableNioAutoRegistration`.

## Direct Buffer Creation

Direct buffer creation is supported in `Rxmxnx.JNetInterface` through the following static methods of the `JByteBuffer`
class:

- **`CreateDirectBuffer<T>(IEnvironment, Memory<T>)`**
    - Creates a direct byte buffer using a `System.Memory<T>` instance, which remains pinned until the created instance
      is discarded.
    - The memory item type must be unmanaged.

- **`WithDirectBuffer(IEnvironment, int, Action<JByteBufferObject>)`**
    - Creates a temporary direct buffer of the specified capacity and executes the provided delegate.
    - Once execution is complete, the buffer is discarded.

- **`WithDirectBuffer<TState>(IEnvironment, int, TState, Action<JByteBufferObject, TState>)`**
    - Creates a temporary direct buffer of the specified capacity and executes the provided delegate, including a state
      object.
    - Once execution is complete, the buffer is discarded.

- **`WithDirectBuffer<TResult>(IEnvironment, int, Func<JByteBufferObject, TResult>)`**
    - Creates a temporary direct buffer of the specified capacity and executes the provided delegate, returning its
      result.
    - Once execution is complete, the buffer is discarded.

- **`WithDirectBuffer<TState, TResult>(IEnvironment, int, TState, Func<JByteBufferObject, TState, TResult>)`**
    - Creates a temporary direct buffer of the specified capacity and executes the provided delegate, including a state
      object, returning its result.
    - Once execution is complete, the buffer is discarded.

##### Notes

- The `WithDirectBuffer` methods may allocate memory on the stack if the configured usable stack byte limit is not
  exceeded.
- In .NET 9.0+, the generic state type parameter allows `ref struct`.  
