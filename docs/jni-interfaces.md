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

- **`Version`**: Java runtime version. The type of this property is `JRuntimeVersion` enum.
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
- **`Version`**: The JNI version of the environment. The type of this property is `Int32`.
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

# `JRuntimeVersion` enum

The `JRuntimeVersion` enum represents the versions of the different Java specifications in a format compatible with the
JNI version number, making them comparable.

The following versions are currently defined:

* **SEd0**: Java 1.0 (0x00010000)
* **SEd1**: Java 1.1 (0x00010001)
* **SEd2**: Java 1.2 (0x00010002)
* **SEd3**: Java 1.3 (0x00010003)
* **SEd4**: Java 1.4 (0x00010004)
* **J5**: Java 1.5 (0x00010005)
* **J6**: Java 1.6 (0x00010006)
* **J7**: Java 1.7 (0x00010007)
* **J8**: Java 1.8 (0x00010008)
* **J9**: Java 9 (0x00090000)
* **J10**: Java 10 (0x000a0000)
* **J11**: Java 11 (0x000b0000)
* **J12**: Java 12 (0x000c0000)
* **J13**: Java 13 (0x000d0000)
* **J14**: Java 14 (0x000e0000)
* **J15**: Java 15 (0x000f0000)
* **J16**: Java 16 (0x00100000)
* **J17**: Java 17 (0x00110000)
* **J18**: Java 18 (0x00120000)
* **J19**: Java 19 (0x00130000)
* **J20**: Java 20 (0x00140000)
* **J21**: Java 21 (0x00150000)
* **J22**: Java 22 (0x00160000)
* **J23**: Java 23 (0x00170000)
* **J24**: Java 24 (0x00180000)
* **J25**: Java 25 (0x00190000)

