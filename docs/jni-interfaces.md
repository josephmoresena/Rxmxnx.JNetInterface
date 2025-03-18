# IVirtualMachine Interface

The `IVirtualMachine` interface represents a JavaVM instance managed by `Rxmxnx.JNetInterface`.

This interface exposes the following static members:

- `MinimalVersion`: The minimum JNI version supported by `Rxmxnx.JNetInterface`; attempting to use a lower version may
  lead to functional failures.
- `MetadataValidationEnabled`: Indicates whether metadata type construction validation at runtime is enabled.
- `JaggedArrayAutoGenerationEnabled`: Indicates whether metadata generation for jagged arrays at runtime is enabled.
- `TypeMetadataToStringEnabled`: Indicates whether the `ToString()` implementation of type metadata provides detailed
  output.

This interface exposes the following instance properties and methods:

- `Reference`: The `JVirtualMachineRef` reference being managed.
- `GetEnvironment()`: Equivalent to the JNI `GetEnv` call.
- `InitializeThread(CString?, JGlobalBase?, Int32)`: Equivalent to the JNI `AttachCurrentThread` call. The first
  parameter names the thread in the JVM, the second must be a global instance of a `java.lang.ThreadGroup` object, and
  the last specifies the JNI version compatible with the thread.
- `InitializeDaemon(CString?, JGlobalBase?, Int32)`: Similar to `InitializeThread`, but equivalent to the JNI
  `AttachCurrentThreadAsDaemon` call.
- `FatalError(CString?)`: Equivalent to the JNI `FatalError` call.
- `FatalError(String?)`: Equivalent to the JNI `FatalError` call.

In testing environments, this instance can be simulated using the `VirtualMachineProxy` proxy type.

# IEnvironment Interface

The `IEnvironment` interface represents a `JNIEnv` instance managed by `Rxmxnx.JNetInterface`.

This interface provides the following properties:

- `Reference`: The `JEnvironmentRef` reference being managed.
- `VirtualMachine`: The `IVirtualMachine` instance to which this environment belongs.
- `Version`: The JNI version of the environment.
- `LocalCapacity`: The capacity of local references. This property will be explained later.
- `PendingException`: The pending exception in the environment. This property will be explained later.
- `UsedStackBytes`: Indicates the number of bytes consumed by `Rxmxnx.JNetInterface` in the current stack.
- `UsableStackBytes`: Gets or sets the number of bytes usable by the stack for JNI calls. This value must be greater
  than 128 and greater than the currently used amount. A value below 1MB is recommended.

This interface also exposes some methods, including:

- `GetReferenceType(JObject)`: Determines the JNI reference type of the object.
- `IsSameObject(JObject, JObject)`: Indicates whether both instances are equivalent in Java.
- `JniSecure()`: Indicates whether it is safe to make JNI calls in the current environment.
- `IsVirtual(JThreadObject)`: Indicates whether the `java.lang.Thread` instance is virtual. This method only works for
  JNI version 19+.

This interface exposes additional methods used by various classes in `Rxmxnx.JNetInterface`.

In testing environments, this instance can be simulated using the `EnvironmentProxy` proxy type, and for the `IThread`
interface, the `ThreadProxy` type.

For more information on using proxy objects, refer to
the [included example application](../src/ApplicationTest/Rxmxnx.JNetInterface.ManagedTest/README.md) in this
repository.