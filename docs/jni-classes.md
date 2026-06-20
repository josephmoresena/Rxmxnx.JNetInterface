# Java Invocation API

The Invocation API enables loading a JVM instance within an application. `Rxmxnx.JNetInterface` provides access to this
API through the `JVirtualMachineLibrary` class.

**Note:** `Rxmxnx.JNetInterface.Mobile` does not support the Invocation API.

## Creating a `JVirtualMachineLibrary` Instance

To create an instance, use the following static methods:

- **`LoadLibrary(String)`**: Loads the JVM library from the specified path and returns a `JVirtualMachineLibrary`
  instance.
- **`Create(IntPtr)`**: Creates a `JVirtualMachineLibrary` instance using the handle of an already loaded JVM library.
- **`LoadStaticLibrary()`**: Creates a JVirtualMachineLibrary instance from a statically linked JVM library. This method
  can only be used in NativeAOT builds.
- **`Create<TLibrary>()`**: Creates a `JVirtualMachineLibrary` instance using the specified `IVirtualMachineLibraryType`
  implementation.

These methods return `null` if the required Invocation API symbols are not found:

- `JNI_GetDefaultJavaVMInitArgs`: Returns the default configuration for the JVM.
- `JNI_CreateJavaVM`: Loads and initializes a JVM instance, attaching the current thread as the main thread.
- `JNI_GetCreatedJavaVMs`: Retrieves all currently created JVM instances.

### `IVirtualMachineLibraryType` interface

The `IVirtualMachineLibraryType` interface allows custom JVM library implementations to provide the required P/Invoke
bindings for the JNI Invocation API. The following static members must be implemented:

* `GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue)`: Static abstraction of
  `JNI_GetDefaultJavaVMInitArgs`.
* `CreateVirtualMachine(out JVirtualMachineRef, out JEnvironmentRef, in VirtualMachineInitArgumentValue)`: Static
  abstraction of `JNI_CreateJavaVM`.
* `GetCreatedVirtualMachines(ValPtr<JVirtualMachineRef>, Int32, out Int32)`: Static abstraction of
  `JNI_GetCreatedJavaVMs`.

**Notes**

* `VirtualMachineInitArgumentValue` is the public equivalent of the native `JavaVMInitArgs` structure. For safety, its
  internal fields are intentionally not exposed.
* Although the native `JavaVMInitArgs` structure contains pointers, `VirtualMachineInitArgumentValue` stores pointer
  values rather than exposing pointer-typed fields. This design allows the structure to be used safely without requiring
  an `unsafe` context.
* `GetCreatedVirtualMachines` receives a `ValPtr<JVirtualMachineRef>` instead of a `ref` or `out JVirtualMachineRef`
  because the underlying JNI function writes to an array of `JavaVM*` values rather than returning a single instance.
* Due to current language limitations, this interface can only be implemented in C#. Visual Basic .NET does not support
  static abstract interface members, and F# does not support internal virtual interface members (whether static or
  instance).

## `JVirtualMachineLibrary` API

- **`Handle`**: Returns the handle of the loaded JVM library.
- **`GetLatestSupportedVersion()`**: Retrieves the latest JNI version supported by the loaded library.
- **`GetDefaultArgument(Int32)`**: Returns the default JVM initialization arguments for the specified JNI version.
- **`CreateVirtualMachine(JVirtualMachineInitArg, out IEnvironment)`**: Creates a JVM instance and initializes the
  environment in the current thread.
- **`GetCreatedVirtualMachines()`**: Retrieves all JVM instances loaded by the library.

For additional details, refer to the [example application](../src/ApplicationTest/README.md).

**Notes**:

* The instance returned by `CreateVirtualMachine(JVirtualMachineInitArg, out IEnvironment)` implements
  `IInvokedVirtualMachine` because it is created and managed by `Rxmxnx.JNetInterface`.
* If a JVM has not yet been initialized by `Rxmxnx.JNetInterface` when `GetCreatedVirtualMachines()` is called, it is
  initialized before the method returns the list of JVM instances.

---

# `JVirtualMachine` Class

The `JVirtualMachine` class implements `IVirtualMachine` and provides additional APIs for managing and removing JVM
references. This type is available only in `Rxmxnx.JNetInterface`. The equivalent type in `Rxmxnx.JNetInterface.Mobile`
is `AndroidJniHost`.

## Static Members

- **`AndroidApiLevel`: The Android API level of the current runtime. A value of `null` indicates a non-Android
  environment. This property is not affected by the `JNetInterface.FixedRuntime.*` feature switches.
- **`TraceEnabled`**: Indicates whether the `JNetInterface.EnableTrace` feature switch is active.
    - This enables tracking of JNI calls via `System.Diagnostics.Trace`.
- **`FinalUserTypeRuntimeEnabled`**: Indicates whether `JNetInterface.EnableFinalUserTypeRuntime` is active.
    - This allows assuming final mapped data types without verifying their actual class.
- **`CheckRefTypeNativeCallEnabled`**: Indicates whether `JNetInterface.DisableCheckRefTypeNativeCall` is inactive.
    - This allows skipping reference type verification in native Java calls.
- **`CheckClassRefNativeCallEnabled`**: Indicates whether `JNetInterface.DisableCheckClassRefNativeCall` is inactive.
    - This allows skipping verification that an object used as a class parameter is actually a `java.lang.Class<?>`.
- **`MainClassesInformation`**: Provides metadata for main-class types.
- **`Register<TReference>()`**: Registers the metadata of a mapped reference type at runtime.
- **`GetVirtualMachine(JVirtualMachineRef)`**: Retrieves the `IVirtualMachine` instance managing a given reference.
- **`RemoveVirtualMachine(JVirtualMachineRef)`**: Removes the `IVirtualMachine` instance associated with a reference.
- **`SetMainClass<TReference>()`**: Marks a mapped reference type as a main class.

## Instance Members

- **`IsAlive`**: Indicates whether the JVM instance created through the JNI Invocation API is still active.
- **`Version`**: The Java runtime version reported by the current JVM. This property is not affected by the
  `JNetInterface.FixedRuntime.*` feature switches.
- **`IsDisposable`**: Indicates whether the current JVM instance can be destroyed through the JNI Invocation API.
- **`Reference`**: The `JVirtualMachineRef` managed by this instance.
- **`FatalError(CString?)` / `FatalError(String?)`**: Invokes the JVM `FatalError` function with the specified error
  message.

---  

# `JEnvironment` Class

The `JEnvironment` class implements `IEnvironment` and provides comparison operators to determine whether two instances
manage the same `JEnvironmentRef`. This type is available only in `Rxmxnx.JNetInterface`. There is no public equivalent
in `Rxmxnx.JNetInterface.Mobile`.

## Additional Instance Members

The following members are specific to the `JEnvironment` implementation provided by `Rxmxnx.JNetInterface`:

- **`Name`**: Gets the name assigned when the current thread was attached by `Rxmxnx.JNetInterface`.
- **`IsDisposable`**: Indicates whether the current instance manages a thread attached by `Rxmxnx.JNetInterface`.
- **`IsDaemon`**: Indicates whether the current thread was attached as a daemon by `Rxmxnx.JNetInterface`.
- **`Version`**:  The actual JNI version of the current environment. This property is not affected by the
  `JNetInterface.FixedRuntime.*` feature switches.
- **`IsAttached`**: Indicates whether the current thread is still attached to the JVM through `Rxmxnx.JNetInterface`.
- **`PendingException`**: Gets the pending exception in the current environment.
- **`Reference`**: Gets the `JEnvironmentRef` managed by this instance.
- **`VirtualMachine`**: Gets the `IVirtualMachine` instance to which this environment belongs.
- **`UsedStackBytes`**: Gets the number of stack bytes currently consumed by `Rxmxnx.JNetInterface`.
- **`UsableStackBytes`**: Gets or sets the number of stack bytes available for JNI calls.
- **`JniSecure()`**: Indicates whether `Rxmxnx.JNetInterface` considers the current environment safe for JNI calls.
- **`DescribeException()`**: Invokes `ExceptionDescribe` for the current pending exception.

---

# `AndroidJniHost` class

The `AndroidJniHost` class implements `IVirtualMachine` and provides additional APIs for managing and removing JVM
references. This type is available only in `Rxmxnx.JNetInterface.Mobile`.

Unlike `JVirtualMachine` from `Rxmxnx.JNetInterface`, `AndroidJniHost` only supports interactions with the
`Rxmxnx.JNetInterface` APIs through the `AndroidJniContext` ref struct.

For more details about using `Rxmxnx.JNetInterface.Mobile` API refer to [.NET Android interop](mobile-runtime.md).

---

# Exporting Native Java Functions

NativeAOT allows the creation
of [dynamic libraries from .NET code](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/libraries),
enabling JNI library development using `Rxmxnx.JNetInterface`.

## Naming Conventions

Native Java calls implemented in JNI libraries must follow specific conventions for parameters and naming.  
For a Java native method to be recognized by JNI, it must adhere to the following naming convention:

```
Java_package_ClassName_methodName
```  

For more details, refer to
the [official JNI documentation](https://docs.oracle.com/javase/8/docs/technotes/guides/jni/spec/design.html#resolving_native_method_names).

## Lifecycle Methods

When a JNI library is loaded or unloaded, the JVM invokes specific native symbols:

- **`JNI_OnLoad`**
    - Triggered when the library is loaded.
    - Must return a 32-bit integer indicating the minimum supported JNI version.

- **`JNI_OnUnload`**
    - Triggered when the library is unloaded.
    - Must return `void`.

These methods must be exported with their exact names and have the following parameters:

1. **`JVirtualMachine`**: A reference to the JVM loading or unloading the library.
2. **`IntPtr`**: A placeholder parameter that must be included in the method signature.

##### Notes

- `JNI_OnLoad` is useful for calling `JVirtualMachine.GetVirtualMachine(JVirtualMachineRef)` and caching the
  `IVirtualMachine` instance.
- `JNI_OnUnload` is useful for calling `JVirtualMachine.RemoveVirtualMachine(JVirtualMachineRef)`.
- Any Java native call implementation must initialize a `JNativeCallAdapter` instance and finalize it at the end of the
  call.
    - This is **not** compatible with Visual Basic .NET.
- The `[UnmanagedCallersOnly]` attribute is only available in C#.
- Discussions on creating native libraries with [F#](https://github.com/dotnet/samples/issues/5647)
  and [Visual Basic .NET](https://github.com/dotnet/runtime/issues/96103) exist.
- A sample JNI library using `Rxmxnx.JNetInterface` can be found in:
    - The [example library](../src/ApplicationTest/README.md) in this repository.
    - The [
      `jnetinterface` branch of the NativeAOT-AndroidHelloJniLib repository](https://github.com/josephmoresena/NativeAOT-AndroidHelloJniLib/tree/jnetinterface).  
