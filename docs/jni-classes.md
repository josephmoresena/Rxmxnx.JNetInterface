# Java Invocation API

The Invocation API enables loading a JVM instance within an application. `Rxmxnx.JNetInterface` provides access to this
API through the `JVirtualMachineLibrary` class.

## Creating a `JVirtualMachineLibrary` Instance

To create an instance, use the following static methods:

- **`LoadLibrary(String)`**: Loads the JVM library from the specified path and returns a `JVirtualMachineLibrary`
  instance.
- **`Create(IntPtr)`**: Creates a `JVirtualMachineLibrary` instance using the handle of an already loaded JVM library.

These methods return `null` if the required Invocation API symbols are not found:

- `JNI_GetDefaultJavaVMInitArgs`: Returns the default configuration for the JVM.
- `JNI_CreateJavaVM`: Loads and initializes a JVM instance, attaching the current thread as the main thread.
- `JNI_GetCreatedJavaVMs`: Retrieves all currently created JVM instances.

## `JVirtualMachineLibrary` API

- **`Handle`**: Returns the handle of the loaded JVM library.
- **`GetLatestSupportedVersion()`**: Retrieves the latest JNI version supported by the loaded library.
- **`GetDefaultArgument(Int32)`**: Returns the default JVM initialization arguments for the specified JNI version.
- **`CreateVirtualMachine(JVirtualMachineInitArg, out IEnvironment)`**: Creates a JVM instance and initializes the
  environment in the current thread.
    - The returned instance implements `IInvokedVirtualMachine`, as it is created and managed by the local process.
- **`GetCreatedVirtualMachines()`**: Retrieves all JVM instances loaded by the library.
    - If any JVM is not yet initialized in `Rxmxnx.JNetInterface`, the initialization process is executed before
      returning the list.

For additional details, refer to the [example application](../src/ApplicationTest/README.md).

---

# `JVirtualMachine` Class

The `JVirtualMachine` class implements `IVirtualMachine` and provides additional APIs for managing and removing JVM
references.

## Static Members

- **`TraceEnabled`**: Indicates whether the `JNetInterface.EnableTrace` feature switch is active.
    - This enables tracking of JNI calls via `System.Diagnostics.Trace`.
- **`FinalUserTypeRuntimeEnabled`**: Indicates whether `JNetInterface.EnableFinalUserTypeRuntime` is active.
    - This allows assuming final mapped data types without verifying their actual class.
- **`CheckRefTypeNativeCallEnabled`**: Indicates whether `JNetInterface.DisableCheckRefTypeNativeCall` is inactive.
    - This allows skipping reference type verification in native Java calls.
- **`CheckClassRefNativeCallEnabled`**: Indicates whether `JNetInterface.DisableCheckClassRefNativeCall` is inactive.
    - This allows skipping verification that an object used as a class parameter is actually a `java.lang.Class<?>`.
- **`MainClassesInformation`**: Provides metadata for main-class types.
- **`IsAlive`**: Checks whether the JVM instance (created via the invocation API) is still active.
- **`Register<TReference>()`**: Registers the metadata of a mapped reference type at runtime.
- **`GetVirtualMachine(JVirtualMachineRef)`**: Retrieves the `IVirtualMachine` instance managing a given reference.
- **`RemoveVirtualMachine(JVirtualMachineRef)`**: Removes the `IVirtualMachine` instance associated with a reference.
- **`SetMainClass<TReference>()`**: Marks a mapped reference type as a main class.

Additionally, this class exposes inherited members from `IVirtualMachine`, such as `Reference` and `FatalError`.

---  

# `JEnvironment` Class

The `JEnvironment` class implements `IEnvironment`, providing comparison operators to check whether two instances manage
the same `JEnvironmentRef`.

## Additional Properties

- **`IsDisposable`**: Indicates whether the instance is managing a runtime-attached JVM thread.
- **`IsAttached`**: Checks whether the `JEnvironment` instance is attached to the JVM.

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
