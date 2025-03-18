[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=bugs)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=coverage)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)

| **Core Assembly**                                                                                                                                                                                                                                                                    | **Main Assembly**                                                                                                                                                                                                                                    |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![NuGet(Core)](https://img.shields.io/nuget/v/Rxmxnx.JNetInterface.Core)](https://www.nuget.org/packages/Rxmxnx.JNetInterface.Core/) [![fuget(Core)](https://www.fuget.org/packages/Rxmxnx.JNetInterface.Core/badge.svg)](https://www.fuget.org/packages/Rxmxnx.JNetInterface.Core) | [![NuGet](https://img.shields.io/nuget/v/Rxmxnx.JNetInterface)](https://www.nuget.org/packages/Rxmxnx.JNetInterface/) [![fuget](https://www.fuget.org/packages/Rxmxnx.JNetInterface/badge.svg)](https://www.fuget.org/packages/Rxmxnx.JNetInterface) |

# Description

`Rxmxnx.JNetInterface` provides an implementation of the Java Native Interface and Invocation API for use within the
.NET ecosystem.

`Rxmxnx.JNetInterface.Core` is a core package that provides essential types and abstractions to work with
`JNetInterface`. This enables the entire API to be used without burdening its consumers in any way
with the implementation or access to a real JVM.

Furthermore, `Rxmxnx.JNetInterface.Core`, in its `Rxmxnx.JNetInterface.Proxies` namespace, includes some types that
enable the implementation of unit tests without requiring a JVM.

Unfortunately, some features of `JNetInterface` will not be available
in [Visual Basic .NET](https://github.com/dotnet/vblang/issues/625), and some may require additional code to be used
in [F#](https://github.com/dotnet/fsharp/issues/17605).

---

# Getting Started

## Installation

Install the library via NuGet:

```cmd
dotnet add package Rxmxnx.JNetInterface
```

If you don't need to link any actual JVM instance to your projects, you can simply use the core package.

```cmd
dotnet add package Rxmxnx.JNetInterface.Core
```

**Note:** This package currently supports .NET 8 and higher. Ensure your project targets a compatible framework before
installing.

## Overview

`Rxmxnx.JNetInterface` allows the use of JNI through high-level, safe APIs (Core Assembly) and a compatible
implementation (Main Assembly) with the JVM. <br/>

The following table shows the equivalence between the different common JNI types and `Rxmxnx.JNetInterface`.

| Java Type                | JNI Type      | Managed Type                  | Unmanaged Type        |
|--------------------------|---------------|-------------------------------|-----------------------|
| boolean                  | jboolean      | IWrapper<System.Boolean>      | JBoolean              |
| byte                     | jbyte         | IWrapper<System.SByte>        | JByte                 |
| char                     | jchar         | IWrapper<System.Char>         | JChar                 |
| double                   | jdouble       | IWrapper<System.Double>       | JDouble               |
| float                    | jfloat        | IWrapper<System.Single>       | JFloat                |
| int                      | jint          | IWrapper<System.Int32>        | JInt                  |
| long                     | jlong         | IWrapper<System.Int64>        | JLong                 |
| short                    | jshort        | IWrapper<System.Int16>        | JShort                |
| java.lang.Object         | jobject       | JLocalObject                  | JLocalRef             |
| java.lang.Class&lt;?&gt; | jclass        | JClassObject <sup>2</sup>     | JClassLocalRef        |
| java.lang.String         | jstring       | JStringObject                 | JStringLocalRef       |
| java.lang.Throwable      | jthrowable    | JThrowableObject              | JThrowableLocalRef    |
| []                       | jarray        | JArrayObject                  | JArrayLocalRef        |
| boolean[]                | jbooleanArray | JArrayObject&lt;JBoolean&gt;  | JBooleanArrayLocalRef |
| byte[]                   | jbyteArray    | JArrayObject&lt;JByte&gt;     | JByteArrayLocalRef    |
| char[]                   | jcharArray    | JArrayObject&lt;JChar&gt;     | JCharArrayLocalRef    |
| double[]                 | jdoubleArray  | JArrayObject&lt;JDouble&gt;   | JDoubleArrayLocalRef  |
| float[]                  | jfloatArray   | JArrayObject&lt;JFloat&gt;    | JFloatArrayLocalRef   |
| int[]                    | jintArray     | JArrayObject&lt;JInt&gt;      | JIntArrayLocalRef     |
| long[]                   | jlongArray    | JArrayObject&lt;JLong&gt;     | JLongArrayLocalRef    |
| short[]                  | jshortArray   | JArrayObject&lt;JShort&gt;    | JShortArrayLocalRef   |
| T[]                      | jobjectArray  | JArrayObject&lt;T&gt;         | JObjectArrayLocalRef  |
| N/A                      | JavaVM*       | IVirtualMachine <sup>1</sup>  | JVirtualMachineRef    |
| N/A                      | JNIEnv*       | IEnvironment <sup>1</sup>     | JEnvironmentRef       |
| N/A                      | jglobal       | JGlobal <sup>2</sup>          | JGlobalRef            |
| N/A                      | jweak         | JWeak                         | JWeakRef              |
| N/A                      | jmethodID     | JCallDefinition <sup>3</sup>  | JMethodId             |
| N/A                      | jfieldID      | JFieldDefinition <sup>3</sup> | JFieldId              |

1. When the JVM is initialized through the Invocation API, `IInvokedVirtualMachine` is used, and when a thread is
   attached to the JVM, `IThread` is used.
2. Instances of `JClassObject` or `JGlobal` that globally reference classes may not have active JNI references and are
   handled specially to be loaded when needed.
3. Definitions expose the APIs for performing JNI access calls and are internally used as keys to the identifiers.

- [Java Data Types Mapping](docs/data-types-mapping.md)
- [IVirtualMachine and IEnvironment Interfaces](docs/jni-interfaces.md)
- [JNI Reference Handling](docs/jni-references.md)
- [Data Type Metadata](docs/data-types-metadata.md)
- [Java Class Handling](docs/class-object.md)
- [Java String and Java Array Handling](docs/native-objects.md)
- [Java Member Handling](docs/jni-accessing.md)
- [Direct Buffer Handling](docs/direct-buffers.md)
- [Java Error Handling](docs/error-handling.md)
- [Invocation API, JVirtualMachine and JEnvironment classes](docs/jni-classes.md)