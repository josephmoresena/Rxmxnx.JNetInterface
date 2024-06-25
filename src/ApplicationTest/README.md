# Application Test
This application is designed to show the capabilities and potential of using JNetInterface in .NET applications that interact with a JVM instance.

## Disclaimer
In this project, `JNetInterface` is utilized through `intermediate libraries` instead of direct `NuGet packages`.

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

import java.lang.management.ManagementFactory;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;

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
}
```

## Solution
Initially, the application utilizes a class named `JCompiler` (external to `JNetInterface`) to locate the installed JDK 
on the host computer.<br/>
Using `JNetInterface`, we define the `JHelloDotnetObject` class representing the Java class 
`com.rxmxnx.dotnet.test.HelloDotNet`. This class implements the static members of the `IClassType<JHelloDotnetObject>` 
interface, providing runtime metadata necessary for JNI interoperability. 
This interface is pivotal as it facilitates dynamic access and management of metadata associated with the Java class, 
allowing type-safe interactions with the JVM.<br/>
By implementing `IClassType<JHelloDotnetObject>`, the application maintains a structured approach to managing the Java 
class, enhancing method invocation, object handling, and JVM interaction.<br>
After locating the JDK, the Java code is compiled from `HelloDotnet.java` to `HelloDotnet.class`, 
from which the bytecode's binary information is extracted.</br>
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