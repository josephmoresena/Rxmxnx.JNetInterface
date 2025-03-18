# Java Invocation API

The Invocation API allows loading a JVM instance within an application. `Rxmxnx.JNetInterface` provides access to this
API through the `JVirtualMachineLibrary` class.

To create an instance of this class, you can use the following static methods:

- `LoadLibrary(String)`: Returns a `JVirtualMachineLibrary` instance by providing the path to the JVM library.
- `Create(IntPtr)`: Returns a `JVirtualMachineLibrary` instance using the handle of the loaded library.

These methods will return a null instance if the exported symbols of the Invocation API cannot be found:

- `JNI_GetDefaultJavaVMInitArgs`: Returns a default configuration for the Java VM.
- `JNI_CreateJavaVM`: Loads and initializes a Java VM. The current thread becomes the main thread.
- `JNI_GetCreatedJavaVMs`: Returns all Java VMs that have been created.

The `JVirtualMachineLibrary` class exposes the following API:

- `Handle`: This property exposes the handle of the loaded library in the process.
- `GetLatestSupportedVersion()`: Returns the latest JNI version supported by the loaded library.
- `GetDefaultArgument(Int32)`: Returns the default initialization argument for the JVM based on the specified JNI
  version.
- `CreateVirtualMachine(JVirtualMachineInitArg, out IEnvironment)`: Creates a JVM instance from `Rxmxnx.JNetInterface`
  and initializes the environment in the current thread. The instance returned by this method implements the
  `IInvokedVirtualMachine` interface, as it is created by the local process and can
  also be disposed of by it.
- `GetCreatedVirtualMachines()`: Returns an array of all JVM instances loaded by the library. If any JVM instance is
  not yet initialized in `Rxmxnx.JNetInterface`, the initialization algorithm will be executed before returning the
  array.

For more information on using the Invocation API from `Rxmxnx.JNetInterface`, refer to
the [included example application](../src/ApplicationTest/README.md) in this repository.

# JVirtualMachine Class

The `JVirtualMachine` class implements the `IVirtualMachine` interface but also exposes APIs to manage and remove
`JVirtualMachine` references in `Rxmxnx.JNetInterface`.

This class provides the following static members:

- `TraceEnabled`: Indicates that the `JNetInterface.EnableTrace` feature switch is active. This feature uses
  `System.Diagnostics.Trace` for tracking scenarios and the associated JNI calls.
- `FinalUserTypeRuntimeEnabled`: Indicates that the `JNetInterface.EnableFinalUserTypeRuntime` feature switch is
  active.
  This feature tells `Rxmxnx.JNetInterface` that when attempting to obtain an instance of a mapped final data type, it
  should assume the final type without verifying the actual class of the instance.
- `CheckRefTypeNativeCallEnabled`: Indicates that the `JNetInterface.DisableCheckRefTypeNativeCall` feature switch is
  inactive. This feature allows skipping reference type verification when using a value as a parameter in a native Java
  call.
- `CheckClassRefNativeCallEnabled`: Indicates that the `JNetInterface.DisableCheckClassRefNativeCall` feature switch
  is
  inactive. This feature allows skipping verification that an object instance used as a class parameter in a native Java
  call is actually a `java.lang.Class<?>`.
- `MainClassesInformation`: A list of metadata for types marked as main classes.
- `IsAlive`: When the JVM instance has been created using the invocation interface, this property verifies that the
  instance has not been destroyed.
- `Register<TReference>()`: Registers the metadata of a mapped reference type in `Rxmxnx.JNetInterface` at runtime.
- `GetVirtualMachine(JVirtualMachineRef)`: Creates or retrieves the `IVirtualMachine` instance managing the given
  reference.
- `RemoveVirtualMachine(JVirtualMachineRef)`: Removes the `IVirtualMachine` instance associated with the given
  reference, if it exists.
- `SetMainClass<TReference>()`: Sets the mapped reference type as a main class.

This class also exposes some inherited members from the `IVirtualMachine` interface, such as `Reference` and
`FatalError`.

# JEnvironment Class

The `JEnvironment` class implements the `IEnvironment` interface. This class provides comparison operators to determine
whether two `JEnvironment` instances manage the same `JEnvironmentRef` reference.

The only additional properties are:

- `IsDisposable`: Indicates that the `JEnvironment` instance is actually managing the environment of a thread
  associated
  with the JVM at runtime.
- `IsAttached`: Indicates whether the `JEnvironment` instance is attached to the JVM.

# Exporting Native Java Functions

NativeAOT allows
creating [dynamic libraries from .NET code](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/libraries),
making it possible to build JNI libraries with `Rxmxnx.JNetInterface`.

Native Java calls implemented in JNI libraries must follow specific conventions for parameters and naming.
[Here](jni-accessing.md#defining-native-java-calls) is explained how a .NET method can be set as an implementation
for a Java native call at runtime, but for JNI to recognize an exported native symbol in a dynamic library, it must
follow the following naming convention:

Java_package_ClassName_methodName

More details about this convention can be found in
the [official documentation](https://docs.oracle.com/javase/8/docs/technotes/guides/jni/spec/design.html#resolving_native_method_names).

When a JNI library is loaded by the JVM, it invokes the native symbol `JNI_OnLoad`,
and when it is unloaded, it invokes the native symbol `JNI_OnUnload`.

To function correctly, these methods must be exported with these exact names
and must use the following parameters:

1. `JVirtualMachine`: A reference to the JVM that is loading or unloading the library.
2. `IntPtr`: This parameter has no actual use but must be part of the method declaration.
3. The return type of `JNI_OnLoad` is a 32-bit integer representing the minimum
   JNI version compatible with the library.
4. The return type of `JNI_OnUnload` is `void`.

More details on creating dynamic libraries with NativeAOT can be found in
the [official documentation](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/libraries).

**Notes:**

- The method used as `JNI_OnLoad` is ideal for calling `JVirtualMachine.GetVirtualMachine(JVirtualMachineRef)` and
  caching the `IVirtualMachine` instance.
- The method used as `JNI_OnUnload` is ideal for calling `JVirtualMachine.RemoveVirtualMachine(JVirtualMachineRef)`.
- The implementation of any Java native call must initialize a `JNativeCallAdapter` instance and finalize it at the end
  of the call. This cannot be used in the Visual Basic .NET language.
- The
  [UnmanagedCallersOnlyAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.unmanagedcallersonlyattribute)
  is only available in C#.
- Here are discussions about creating native libraries with [F#](https://github.com/dotnet/samples/issues/5647)
  and [Visual Basic .NET](https://github.com/dotnet/runtime/issues/96103).
- An example of a JNI library using `Rxmxnx.JNetInterface` can be found in the
  [included example library](../src/ApplicationTest/README.md) in this repository or in the
  [jnetinterface branch of the NativeAOT-AndroidHelloJniLib repository](https://github.com/josephmoresena/NativeAOT-AndroidHelloJniLib/tree/jnetinterface).