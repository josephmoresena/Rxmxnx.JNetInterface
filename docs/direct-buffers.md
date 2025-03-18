# Direct Buffer Handling

Since JNI 1.4, native handling of Java NIO (New Input/Output) buffers and the creation of direct buffers have been
supported.

The NIO object hierarchy may vary depending on the JVM implementation, but in `Rxmxnx.JNetInterface`, the following
hierarchy is established:

![BufferHierarchy](https://github.com/user-attachments/assets/c9fc3743-da67-47ea-aa00-a4c4bf633d92)

`JBufferObject` class exposes the following properties:

- `IsDirect`: Indicates whether the buffer is direct or managed by the JVM.
- `Capacity`: The capacity in units of the primitive type of the buffer.
- `Address`: Pointer to the buffer's memory. Valid only when the buffer is direct.

**Note:** If this feature is not going to be used, it is recommended to disable the automatic registration of NIO types
to improve performance using the feature switch `JNetInterface.DisableNioAutoRegistration`.

## Direct Buffer Creation

Direct buffer creation is supported in `Rxmxnx.JNetInterface` through the following static methods of the `JByteBuffer`
class:

- `CreateDirectBuffer<T>(IEnvironment, Memory<T>)`: Creates a direct byte buffer using a `System.Memory<T>` instance,
  which remains pinned until the created instance is discarded. The memory item type must be unmanaged.
- `WithDirectBuffer(IEnvironment, Int32, Action<JByteBufferObject>)`: Creates a temporary direct buffer of the specified
  capacity and executes the delegate. Once execution is completed, the buffer is discarded.
- `WithDirectBuffer<TState>(IEnvironment, Int32, TState, Action<JByteBufferObject, TState>)`: Creates a temporary direct
  buffer of the specified capacity and executes the delegate, including a state object. Once execution is completed, the
  buffer is discarded.
- `WithDirectBuffer<TResult>(IEnvironment, Int32, Func<JByteBufferObject, TResult>)`: Creates a temporary direct buffer
  of the specified capacity and executes the delegate, returning its result. Once execution is completed, the buffer is
  discarded.
- `WithDirectBuffer<TState, TResult>(IEnvironment, Int32, TState, Func<JByteBufferObject, TState, TResult>)`: Creates a
  temporary direct buffer of the specified capacity and executes the delegate, including a state object and returning
  its result. Once execution is completed, the buffer is discarded.

**Notes:**

- The `WithDirectBuffer` methods may allocate memory on the stack if the configured usable stack byte limit is not
  exceeded.
- In .NET 9.0+, the generic state type parameter allows `ref struct`.