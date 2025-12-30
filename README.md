[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=alert_status)![Bugs](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=bugs)![Coverage](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=coverage)![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=ncloc)![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=reliability_rating)![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=sqale_rating)![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)

[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/josephmoresena/Rxmxnx.JNetInterface)

#### Package Information

| **Core Assembly**                                                                                                                                                                                                                         | **Main Assembly**                                                                                                                                                                                                    |
|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![NuGet(Core)](https://img.shields.io/nuget/v/Rxmxnx.JNetInterface.Core)![Downloads](https://img.shields.io/nuget/dt/Rxmxnx.JNetInterface.Core?style=flat-square&color=blue)](https://www.nuget.org/packages/Rxmxnx.JNetInterface.Core/) | [![NuGet](https://img.shields.io/nuget/v/Rxmxnx.JNetInterface)![Downloads](https://img.shields.io/nuget/dt/Rxmxnx.JNetInterface?style=flat-square&color=blue)](https://www.nuget.org/packages/Rxmxnx.JNetInterface/) |

# Description

`Rxmxnx.JNetInterface` provides an implementation of the Java Native Interface (JNI) and the Invocation API for .NET
applications.

## Core Features

- **`Rxmxnx.JNetInterface.Core`**: Provides essential types and abstractions to work with JNI without requiring an
  actual JVM instance.
- **Unit Testing Support**: The `Rxmxnx.JNetInterface.Proxies` namespace includes types that enable unit testing without
  a JVM.

**Note:** Some features may not be available in [Visual Basic .NET](https://github.com/dotnet/vblang/issues/625) and may
require additional configuration in [F#](https://github.com/dotnet/fsharp/issues/17605).

---

# Getting Started

## Installation

Install via NuGet:

```cmd
dotnet add package Rxmxnx.JNetInterface
```

If you only need the core functionality without linking to a JVM, install:

```cmd
dotnet add package Rxmxnx.JNetInterface.Core
```

**Supported Frameworks:**  
This package supports **.NET 7.0 and later**. Ensure your project targets a compatible framework.

---  

## Overview

`Rxmxnx.JNetInterface` provides safe, high-level APIs to interact with JNI and the JVM.

The table below shows how common JNI types map to `Rxmxnx.JNetInterface`.

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

1. When initialized via the Invocation API, `IInvokedVirtualMachine` is used. When a thread is attached to the JVM,
   `IThread` is used.
2. `JClassObject` and `JGlobal` references to classes may not have active JNI references and are loaded as needed.
3. Definitions serve as keys for JNI access calls.

**Note:** As mentioned in
[Inconsistent Interop Behavior for Blittable Floating-Point Wrapper Structs in Windows](https://github.com/dotnet/runtime/issues/117778#issuecomment-3085491218),
it is not possible to use `JDouble` and `JFloat` in native JNI calls due to differences with the ABI, even though these
structs are binary-equivalent to `System.Double` and `System.Single`, respectively.

Therefore, any JNI call that returns or receives a `double` or `float` value as a parameter must use the CLR native
types when declaring methods, function pointers, or delegates.

---  

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

## Disclaimer:

> The software is provided "as is," without warranty of any kind. The authors are not liable for any damages or issues
> that may arise from its use.

For more details, refer to the full license text included in the [LICENSE](LICENSE.md) file.

---

# Contributing

We warmly welcome contributions to this open-source project! Whether you're here to report issues, propose enhancements,
or contribute directly to the codebase, your help is greatly appreciated.

For more details, refer to the [CONTRIBUTING](CONTRIBUTING.md) file.

## Translations

We currently support only a few languages, but we are open to adding more! If you'd like to help with translations,
please open an issue or reach out to us. Your contributions to expanding the project's accessibility are highly valued.

This library currently supports translations for the following languages:

* **English**
* **Arabic**
* **Chinese**
* **French**
* **German**
* **Italian**
* **Japanese**
* **Portuguese**
* **Russian**
* **Spanish**

## Collaboration Guidelines

When contributing, please be respectful and constructive. We aim to create an inclusive and welcoming environment for
everyone.

Thank you for considering contributing to this project! Your involvement, whether through reporting, coding, or
translating, helps make this project better for everyone. 🚀