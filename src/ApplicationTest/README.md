# Disclaimer

In these projects, `JNetInterface` is used via *intermediate libraries* rather than through its packaged `NuGet`
distributions.

However, when built through **GitHub Actions**, the projects use a `NuGet` package generated dynamically from the source
code of the branch being executed in the workflow.

# LauncherTest

`LauncherTest` is an application designed to automate the build—and optionally, the execution—of multiple test projects
for all .NET versions and deployment modes supported by `JNetInterface`, including **R2R**, **NativeAOT**, and
**Self-Contained** builds. It is intended to be compiled using **NativeAOT** to maximize performance.

## Requirements

To run this application, the following prerequisites must be met:

- Internet connection
- Operating system: **Linux**, **Windows**, or **macOS**
- Latest version of the **.NET SDK** installed

## Usage

The application accepts the following parameters:

1. **Project directory** – Path to the directory containing the test projects (e.g., `src/ApplicationTest`)
2. **Output directory** – Path where the build artifacts will be stored.
3. **Execution mode** – Determines the application's behavior:
    - `"run"` – Executes the artifacts found in the output directory
    - `"compile"` – Only compiles the projects without running them
    - *(none specified)* – Performs both compilation and execution

## Supported Platforms and Targets

All test projects are built for each .NET version supported by `JNetInterface`, targeting the available platforms and
modes for that version:

- **Windows**: x86, x86-64, arm64
- **Linux**: x86-64, arm, arm64
- **macOS**: x86-64, arm64

> **Note**: The **reflection-free mode** is only available in .NET 8.0. If a project is not compatible with this mode,
> its corresponding artifact will be skipped.

The resulting binaries are compatible with **Java versions 6.0 through 24**, except for **macOS arm64**, where Java 6.0
is unavailable and testing begins from version 8.0.

## Automated Testing

Automated tests are executed for each non-GUI project using LTS versions of Java ranging from **6.0 to 21.0**.  
If a required JDK version is not found on the system, it will be downloaded automatically—subject to availability.

## Environment Variables

You can modify the behavior of the application using the following environment variables:

- `JNETINTERFACE_ONLY_NATIVE_TEST`:  
  If set, only NativeAOT builds will be performed.

- `GITHUB_ACTIONS`:  
  Used to detect if the application is running inside a GitHub Actions workflow.

- `JAVA_HOME_X_Y`:  
  Specifies the path to a specific JDK version `X` for architecture `Y`.

## Artifacts

The artifacts generated by GitHub Actions include **NativeAOT builds** of the test projects for each supported platform:

- **Windows**: x86, x86-64, arm64
- **Linux**: x86-64, arm, arm64
- **macOS**: x86-64, arm64

In addition to the binaries, a separate artifact is produced containing a set
of [NativeAOT build diagnostics](https://github.com/dotnet/runtime/blob/main/src/coreclr/nativeaot/docs/troubleshooting.md).  
These provide insight into the compilation process, indicating which components were trimmed or preserved.

---

# Application Test

This application is designed to show the capabilities and potential of using `JNetInterface` in .NET applications that
interact with a JVM instance.

## Library Test

The library test project contains the core of Application test. <br/>
If the library is published with NativeAOT, the resulting binary can be used as JNI library for
`com.rxmxnx.dotnet.test.HelloDotNet` natives methods registration.

## Use Case

We utilize a Java class `com.rxmxnx.dotnet.test.HelloDotNet` that needs to be compiled and executed on a local JVM on
the system where the .NET application runs. A minimum JDK version 6.0 is required.<br/>
This main Java class has several native methods implemented within the .NET application.
The class must be loaded into the JVM through an invocation interface.<br/>
The main Java class method needs to be executed from .NET, which in turn calls .NET code using JNI.<br/>

Additionally, one Java method showcases the main functionality of `JNetInterface`:
**Dynamic instantiation for registered types**.

```java
package com.rxmxnx.dotnet.test;

public class HelloDotnet {
    public static int COUNT;

    private String s_field;
    
    public native String getHelloString();
    public native int getThreadId();
    public native void printRuntimeInformation(String runtime_information);

    private void throwException() throws NullPointerException {
        throw new NullPointerException("Thrown from Java code.");
    }
    
    private native void nativeFieldAccess();
    private native void nativeThrow() throws IllegalArgumentException;

    public static void main(String[] args) {
        // Java implementation.
    }

    public static Object getObject(int value) {
        // Java implementation.
    }
    
    private static native Integer sumArray(int[] value);
    private static native int[][] getIntArrayArray(int length);
    private static native void printClass();
    private static native Class getVoidClass();
    private static native Class[] getPrimitiveClasses();
}
```

## Solution

Using `JNetInterface`, we define the `JHelloDotnetObject` class representing the Java class
`com.rxmxnx.dotnet.test.HelloDotNet`. This class implements the static members of the `IClassType<JHelloDotnetObject>`
interface, providing runtime metadata necessary for JNI interoperability.
This interface is pivotal as it facilitates dynamic access and management of metadata associated with the Java class,
allowing type-safe interactions with the JVM.<br/>
By implementing `IClassType<JHelloDotnetObject>`, the application maintains a structured approach to managing the Java
class, enhancing method invocation, object handling, and JVM interaction.<br>
`JNetInterface` then loads this into a JVM initialized via the `JVirtualMachineLibrary` API.

The native methods of `com.rxmxnx.dotnet.test.HelloDotNet` are handled through a strategy that differentiates the two
implicit concepts of `JNetInterface`: the managed abstraction and the unmanaged implementation of JNI.

### Managed abstraction

Implemented through the `IManagedCallback` interface, leveraging C#'s
[Static abstract members in interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-11.0/static-abstracts-in-interfaces)
to handle both instance and static native methods efficiently.<br/>

```c#
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal interface IManagedCallback
{
	static virtual IVirtualMachine TypeVirtualMachine { get; set; } = default!;

	IVirtualMachine VirtualMachine { get; }

	JStringObject? GetHelloString(JLocalObject jLocal);
	JInt GetThreadId(JLocalObject jLocal);
	void PrintRuntimeInformation(JLocalObject jLocal, JStringObject? jString);

	void ProcessField(JLocalObject jLocal);
	void Throw(JLocalObject jLocal);

	static abstract JIntegerObject? SumArray(JClassObject jClass, JArrayObject<JInt>? jArray);
	static abstract JArrayObject<JArrayObject<JInt>>? GetIntArrayArray(JClassObject jClass, Int32 length);
	static abstract void PrintClass(JClassObject jClass);
	static abstract JClassObject GetVoidClass(JClassObject jClass);
	static abstract JArrayObject<JClassObject> GetPrimitiveClasses(JClassObject jClass);
}
```

### Unmanaged Part

Managed by the `JniCallback` class, which acts as a bridge between the managed .NET code and unmanaged JNI code,
ensuring correct routing and execution of native method calls with the help of `JNativeCallAdapter`.

```c#
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
    private sealed class JniCallback(IManagedCallback managed)
    {
        static JniCallback()
        {
            JVirtualMachine.Register<JNullPointerExceptionObject>();
        }

        private JStringLocalRef GetString(JEnvironmentRef envRef, JObjectLocalRef localRef)
        {
            JNativeCallAdapter callAdapter = JNativeCallAdapter
                                             .Create(managed.VirtualMachine, envRef, localRef, out JLocalObject jLocal)
                                             .Build();
            JStringObject? result = managed.GetString(jLocal);
            return callAdapter.FinalizeCall(result);
        }
        // Rest of JNI instance methods.

        public static void RegisterNativeMethods<TManaged>(JClassObject helloDotnetClass, TManaged managed)
            where TManaged : IManagedCallback
        {
            JniCallback jniCallback = new(managed);
            TManaged.TypeVirtualMachine = managed.VirtualMachine;
            helloDotnetClass.Register(new List<JNativeCallEntry>
            {
                JNativeCallEntry.Create<GetStringDelegate>(
                    new JFunctionDefinition<JStringObject>.Parameterless("getNativeString"u8), jniCallback.GetString),
                // Rest of JNI instance method entries.
                JNativeCallEntry.Create<SumArrayDelegate>(
                    new PrimitiveSumArrayDefinition<JIntegerObject, JInt>("sumArray"u8),
                    JniCallback.SumArray<TManaged>),
                // Rest of JNI static method entries.
            });
        }

        private static JObjectLocalRef SumArray<TManaged>(JEnvironmentRef envRef, JClassLocalRef classRef,
            JIntArrayLocalRef intArrayRef) where TManaged : IManagedCallback
        {
            JNativeCallAdapter callAdapter = JNativeCallAdapter
                                             .Create(TManaged.TypeVirtualMachine, envRef, classRef,
                                                     out JClassObject jClass)
                                             .WithParameter(intArrayRef, out JArrayObject<JInt>? jArray).Build();
            JIntegerObject? result = TManaged.SumArray(jClass, jArray);
            return callAdapter.FinalizeCall(result);
        }
        // Rest of JNI static method entries.
    }
}
```

### Dynamic instantiation

The static method `getRandomObject` of the class `com.rxmxnx.dotnet.test.HelloDotNet` returns an instance of any class
depending on the input parameter value.
This method serves to demonstrate the adaptive capability of `JNetInterface` to correctly identify the managed type of
the returned object through a JNI call.

### Unit Testing

A [test project](./Rxmxnx.JNetInterface.ManagedTest/README.md) was created using Proxy objects to simulate the JVM
behavior in `JNetInterface`. The test project runs on the class that implements `IManagedCallback`.

---

# GUI Application Test

This application is designed to demonstrate the capabilities and potential of integrating `JNetInterface` into .NET
applications that utilize Swing and AWT graphical components.

To support this, a Java library named `NativeCallbacks.jar` was created (with its source code included in this
repository). It illustrates how .NET code can be invoked from the Java side using Listeners and Runnable instances,
following a specific interaction pattern. The library is compiled with JDK 6 compatibility, the minimum version
officially supported by `JNetInterface`.

This sample also includes a selection of types from the `javax.swing` and `java.awt` packages, which are necessary for
the application to function correctly.

The `JNativeCallback` class is tightly coupled to the JVM because it needs to handle native calls for each supported
type in `NativeCallbacks.jar`. However, the state and output of its instances can be separated. Using a factory pattern,
it's possible to simulate native callback behavior in a more modular and testable way.

# Android Test

Among the artifacts generated by the GitHub Actions workflow,
the [jnetinterface branch of NativeAOT-AndroidHelloJniLib](https://github.com/josephmoresena/NativeAOT-AndroidHelloJniLib/tree/jnetinterface)
is used to build an APK and verify the correct functionality of `JNetInterface` on Android.