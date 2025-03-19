# `IVirtualMachine` Interface

The `IVirtualMachine` interface represents a `JavaVM` instance managed by `Rxmxnx.JNetInterface`.

## Static Members

- **`MinimalVersion`**: Specifies the minimum JNI version supported by `Rxmxnx.JNetInterface`.
    - Using a lower version may lead to functional failures.
- **`MetadataValidationEnabled`**: Indicates whether runtime validation of type metadata construction is enabled.
- **`JaggedArrayAutoGenerationEnabled`**: Indicates whether metadata generation for jagged arrays at runtime is enabled.
- **`TypeMetadataToStringEnabled`**: Indicates whether the `ToString()` implementation of type metadata provides
  detailed output.

## Instance Properties & Methods

- **`Reference`**: The `JVirtualMachineRef` reference managed by this instance.
- **`GetEnvironment()`**: Retrieves the JNI environment (`JNIEnv`), equivalent to the JNI `GetEnv` call.
- **`InitializeThread(CString?, JGlobalBase?, Int32)`**: Attaches the current thread to the JVM (`AttachCurrentThread`).
    - Parameters:
        1. Thread name (`CString?`).
        2. A global reference to a `java.lang.ThreadGroup` object (`JGlobalBase?`).
        3. The compatible JNI version (`Int32`).
- **`InitializeDaemon(CString?, JGlobalBase?, Int32)`**: Similar to `InitializeThread`, but attaches the thread as a
  daemon (`AttachCurrentThreadAsDaemon`).
- **`FatalError(CString?)` / `FatalError(String?)`**: Triggers a fatal error in the JVM (`FatalError`).

## Testing

In test environments, this interface can be simulated using the `VirtualMachineProxy` proxy type.

---  

# `IEnvironment` Interface

The `IEnvironment` interface represents a `JNIEnv` instance managed by `Rxmxnx.JNetInterface`.

## Properties

- **`Reference`**: The `JEnvironmentRef` reference managed by this instance.
- **`VirtualMachine`**: The `IVirtualMachine` instance to which this environment belongs.
- **`Version`**: The JNI version of the environment.
- **`LocalCapacity`**: The capacity of local references.
- **`PendingException`**: The pending exception in the environment.
- **`UsedStackBytes`**: The number of bytes consumed by `Rxmxnx.JNetInterface` in the current stack.
- **`UsableStackBytes`**: Gets or sets the number of bytes available for JNI calls in the stack.
    - Must be greater than 128 and exceed the currently used amount.
    - A value below 1MB is recommended.

## Methods

- **`GetReferenceType(JObject)`**: Determines the JNI reference type of the specified object.
- **`IsSameObject(JObject, JObject)`**: Checks whether both instances are equivalent in Java.
- **`JniSecure()`**: Indicates whether it is safe to make JNI calls in the current environment.
- **`IsVirtual(JThreadObject)`**: Determines if a `java.lang.Thread` instance is virtual.
    - This method requires JNI version 19+.

## Testing

In test environments, this interface can be simulated using the `EnvironmentProxy` proxy type. For the `IThread`
interface, the `ThreadProxy` type can be used.

For more details on using proxy objects, refer to
the [example application](../src/ApplicationTest/Rxmxnx.JNetInterface.ManagedTest/README.md) in this repository.  
