[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=bugs)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=coverage)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=josephmoresena_Rxmxnx.JNetInterface&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=josephmoresena_Rxmxnx.JNetInterface)
---

| **Core Assembly**                                                                                                                                                                                                                                                                    | **Main Assembly**                                                                                                                                                                                                                                    |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![NuGet(Core)](https://img.shields.io/nuget/v/Rxmxnx.JNetInterface.Core)](https://www.nuget.org/packages/Rxmxnx.JNetInterface.Core/) [![fuget(Core)](https://www.fuget.org/packages/Rxmxnx.JNetInterface.Core/badge.svg)](https://www.fuget.org/packages/Rxmxnx.JNetInterface.Core) | [![NuGet](https://img.shields.io/nuget/v/Rxmxnx.JNetInterface)](https://www.nuget.org/packages/Rxmxnx.JNetInterface/) [![fuget](https://www.fuget.org/packages/Rxmxnx.JNetInterface/badge.svg)](https://www.fuget.org/packages/Rxmxnx.JNetInterface) |

---

## Description

`Rxmxnx.JNetInterface` provides an implementation of the Java Native Interface and Invocation API for use within the
.NET ecosystem.

`Rxmxnx.JNetInterface.Core` is a package that contains the necessary types and abstractions to work with
`JNetInterface`. This is useful because it allows the entire API to be used without burdening its consumers in any way
with the implementation or access to a real JVM.

Additionally, `Rxmxnx.JNetInterface.Core`, in its `Rxmxnx.JNetInterface.Proxies` namespace, includes some types that
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

### Java Data Types Mapping

As shown in the previous table, `Rxmxnx.JNetInterface` provides a mapping of Java data types. This is achieved through
the `IDataType` interface, and instances of `JDataTypeMetadata` are used for identification. <br/>  
The following table illustrates how data type mapping works.

| Java Declaration              | Base Type                                        | Interface                                   | Metadata                                                                                    | Inheritance                                                  |
|-------------------------------|--------------------------------------------------|---------------------------------------------|---------------------------------------------------------------------------------------------|--------------------------------------------------------------|
| class JavaClassName           | JLocalObject <sup>1</sup>                        | IClassType&lt;NetClassName&gt;              | JClassTypeMetadata&lt;NetClassName&gt; { ClassName = package/JavaClassName }                | : NetClassName <sup>2</sup>                                  |
| interface JavaInterfaceName   | JInterfaceObject&lt;NetInterfaceClassName&gt;    | IInterfaceType&lt;NetInterfaceClassName&gt; | JInterfaceTypeMetadata&lt;NetInterfaceClassName&gt; { ClassName = package/JavaInterfaceName | : IInterfaceObject&lt;NetInterfaceClassName&gt; <sup>3</sup> |
| enum JavaEnumName             | JEnumObject&lt;NetEnumClassName&gt;              | IEnumType&lt;NetEnumClassName&gt;           | JEnumTypeMetadata&lt;NetEnumClassName&gt; { ClassName = package/JavaEnumName }              | N/A                                                          |
| @interface JavaAnnotationName | JAnnotationObject&lt;NetAnnotationClassName>&gt; | IInterfaceType&lt;NetInterfaceClassName&gt; | JInterfaceTypeMetadata&lt;NetEnumClassName&gt; { ClassName = package/JavaAnnotationName }   | N/A <sup>4</sup>                                             |

1. The base type depends on the hierarchy of the mapped class type.
2. To extend classes, the superclass type must be set when creating the `TypeMetadataBuilder` instance to construct the
   class metadata.
3. To extend or implement interfaces, the interface type must be specified when constructing the type metadata object.
   Implementing the `IInterfaceObject<..>` interface enables covariance.
4. Annotations should not be inherited; however, both in Java and in the mapping, inheritance is allowed when treated as
   an interface.

#### Specialized Types

| Specialization | Java Declaration                                       | Base Type        | Interface                                                | Metadata                                                   | Inheritance                          |
|----------------|--------------------------------------------------------|------------------|----------------------------------------------------------|------------------------------------------------------------|--------------------------------------|
| Throwable      | class JavaThrowableName extends Throwable <sup>2</sup> | JThrowableObject | IThrowableType&lt;NetThrowableClassName&gt; <sup>2</sup> | JThrowableTypeMetadata&lt;NetThrowableClassName&gt;        | : NetThrowableClassName <sup>2</sup> |
| Primitive      | N/A                                                    | N/A              | IPrimitiveType&lt;..&gt;                                 | JPrimitiveType&lt;..&gt; { ClassName = JavaPrimitiveName } | N/A <sup>1</sup>                     |
| Array          | N/A                                                    | N/A              | IArrayType&lt;JArrayObject&lt;..&gt; &gt;                | JArrayTypeMetadata                                         | N/A <sup>2</sup>                     |

1. Primitive types cannot be inherited, just like in Java. Within the CLR, primitive types are value-type structures.
   The creation of additional primitive types is not allowed.
2. Arrays, like in Java, are views. Although inheritance is not allowed, the `IArrayObject<..>` interface enables
   covariance for class-type arrays. Thus, an instance of `JArrayObject<JStringObject>>` can be assigned to a variable
   of type `IArrayObject<JLocalObject>` or `IArrayObject<IInterfaceObject<JSerializableObject>>`.

#### Object Hierarchy

All types interoperable with JNI implement the `IObject` interface, and all types either extend or can be converted to
the `JObject` class.

                    ┌──  ILocalObject  ─┬──  IInterfaceObject<>  
       IObject  ────┤                   └──  IArrayObject<> 
                    │                   ┌──  IPrimitiveType  ──┐    
                    └──  IDataType  ────┼──  IDataType<>  ─────┼──  IPrimitiveType<>  
                                        └──  IReferenceType  ──┼──  IReferenceType<>  ─┐          
                                                               ├──  IArrayType         │
                                                               ├──  IEnumType  ────────┼──  IEnumType<> 
                                                               ├──  IInterfaceType  ───┼──  IInterfaceType<> 
                                                               └──  IClassType  ───────┼──  IClassType<>  ────────┬──  IThrowableType<> 
                                                                                       │                          ├──  IPrimitiveWrapperType<> 
                                                                                       └──  IUninstantiableType  ─┴──  IUninstantiableType<> 

All instances of objects interoperable with JNI are instances of the `JObject` class. In the case of primitives, the
conversion to `JObject` occurs through boxing.

                    ┌──  JPrimitiveObject (Internal Boxing)                                       ┌──  JLocalObject.ArrayView  ──  JArrayObject<>
       JObject  ────┤                        ┌──  JReferenceObject.View  ──  JLocalObject.View  ──┤
                    │                        │                   ┌──  JWeak                       └──  JLocalObject.InterfaceView  ──  JInterfaceObject<>  ──  JInterfaceObject<>  ──  JAnnotationObject<>
                    └──  JReferenceObject  ──┼──  JGlobalBase  ──┤
                                             │                   └──  JGlobal
                                             │                   ┌──  JArrayObject 
                                             └──  JLocalObject ──┼──  JNumberObject  ──  JNumberObject<>  ──  JNumberObject<,>
                                                                 └──  JThrowableObject

### JNI Reference Handling

All instances of global and weak global objects, as well as local objects or their local views (`ILocalObject`),
implement the `IDisposable` interface. When calling the `Dispose` method, `Rxmxnx.JNetInterface` will remove the JNI
references associated with these instances.

However, in some cases, calling `Dispose` might not immediately remove the JNI references. This can occur due to
the following reasons:

* The JNI reference is being used by another thread. This applies to global and weak global references.
* The JNI reference is required for native memory guarantees by the JVM. The native memory associated with the
  reference must be released before calling `Dispose()`.
* The JNI reference is shared among multiple `JLocalObject` instances. In this case, `Dispose()` must be called on
  each `JLocalObject` instance.
* The `JLocalObject` or `ILocalObject` instance does not have a loaded local reference. The active reference is held
  by an associated global or weak global reference.
* The `JLocalObject` or `ILocalObject` instance was created within the scope of a call from Java to JNI as a parameter.
  The behavior of JNI native call adapters will be detailed later.

**Note:** In some cases, it may not be necessary to explicitly release local references created within the active
environment frame, or some references may be released without calling the `Dispose()` method.

#### Local Reference Handling

All JNI calls that return references to Java objects return local references. These references remain valid as long
as the active environment frame at the time of their creation is maintained or explicitly removed. Local JNI references
are valid only for the environment thread that created them. This is why, in `Rxmxnx.JNetInterface`, all `JLocalObject`
or `ILocalObject` instances are bound to a specific `IEnvironment` instance.

##### Environment Frames

Environment frames are memory spaces where local references are stored. Although JNI does not impose a fixed limit on
the number of local JNI references an environment can create, the active frame may have such a limit.

There are three types of frames:

* **Initial Frame:** The initial frame is the active frame when a thread associates with the JVM from JNI and the
  invocation interface. This frame remains valid, along with all its associated local references, as long as the thread
  remains associated with the JVM.
* **Call Frame:** The call frame is a frame created by the JVM when invoking Java native calls. To notify
  `Rxmxnx.JNetInterface` of the creation of a temporary frame, a `JNativeCallAdapter` instance must be created and
  kept active throughout the duration of the native call.
* **Fixed Frame:** The fixed frame is a programmatically created frame using JNI that can only hold a predefined number
  of local references. If JNI calls during the frame’s duration generate more local references than the set limit, it
  will behave as a FIFO system, invalidating older references to accommodate the newly created ones.

The `IEnvironment` interface exposes the `LocalCapacity` property, which allows checking the set capacity of local
references for the current frame or ensuring that the current frame supports such a capacity.

**Notes:**

* The initial value of `LocalCapacity` will be `null` when the active frame is either the initial or a call frame,
  as this capacity is undetermined.
* The initial value of `LocalCapacity` in a fixed frame is the value set at the time of frame creation.
* The `LocalCapacity` value can only be increased beyond the previously established value.
* Attempting to set `LocalCapacity` in a fixed frame will throw an `InvalidOperationException`.
* Setting `LocalCapacity` when the active frame is either the initial or a call frame triggers the JNI
  `EnsureLocalCapacity`
  call. If this call fails, a `JniException` will be thrown.

###### Call Frame

As mentioned earlier, this type of frame is established by the JVM for the environment when executing a Java native
call.

The native implementation of a Java call must follow specific naming and parameter conventions.

The parameters of a native call are:

1. A reference to the environment (`JEnvironmentRef`).
2. A local reference to the instance of the object (`JObjectLocalRef`) on which the method is executed.
3. Additional parameters depend on the signature of the call. For example, if the native declaration in Java is:

    - `()`: No additional parameters.
    - `(String a)`: `JStringLocalRef`.
    - `(boolean a)`: `JBoolean`.
    - `(Class<?>)`: `JClassLocalRef`.
    - `(Character a)`: `JObjectLocalRef`.
    - `<T>(T a)`: `JObjectLocalRef`.
    - `<T>(T[] a, boolean[] b)`: `JObjectArrayLocalRef`, `JBooleanArrayLocalRef`.
    - `<T extends Number>(T a, char b)`: `JObjectLocalRef`, `JChar`.
    - `(int a, String b, long c, int[] d, Integer[] e)`: `JInt`, `JStringLocalRef`, `JLong`, `JIntArrayLocalRef`,
      `JArrayLocalRef`.

*Note:* The JNI convention for native call naming will be detailed later.

To create the call frame representation in `Rxmxnx.JNetInterface`, any static `Create` method of `JNativeCall` should be
used, followed by `Build()`.

* `Create(JEnvironmentRef)`: If the `IVirtualMachine` instance is not cached and the class or method is not relevant.
* `Create(JEnvironmentRef, JObjectLocalRef, out JLocalObject)`: Same as above, but the method is known to be an instance
  method.
* `Create(JEnvironmentRef, JClassLocalRef, out JClassObject)`: Similar to the first but for static methods.
* `Create<TObject>(JEnvironmentRef, JObjectLocalRef, out TObject)`: Same as the second, but with a known object type.
* `Create(IVirtualMachine, JEnvironmentRef)`: Similar to the first, but with a cached `IVirtualMachine` instance.
* `Create(IVirtualMachine, JEnvironmentRef, out JLocalObject)`: Same as the second, but with a cached `IVirtualMachine`
  instance.
* `Create(IVirtualMachine, JEnvironmentRef, JClassLocalRef, out JClassObject)`: Same as the third, but with a cached
  `IVirtualMachine` instance.
* `Create<>(IVirtualMachine, JEnvironmentRef, JObjectLocalRef, out TObject)`: Same as the fourth, but with a cached
  `IVirtualMachine` instance.

**Note:** Methods using `IVirtualMachine` are more efficient since `Rxmxnx.JNetInterface` supports multiple JVM
instances.

These methods return a `JNativeCallAdapter.Builder` instance, allowing further call description.

For the examples above, the creation sequence would be:

- `.Build()`: The call has no parameters.
- `.WithParameter(JStringLocalRef, out JStringObject).Build()`: Adds a `java.lang.String` parameter.
- `.Build()`: Although the call has a parameter, it's a primitive type and is omitted.
- `.WithParameter(JClassLocalRef, out JClassObject).Build()`: Adds a `java.lang.Class` parameter.
- `.WithParameter<JCharacterObject>(JObjectLocalRef, out JCharacterObject).Build()`: Adds a `java.lang.Character`
  parameter.
- `.WithParameter(JObjectLocalRef, out JLocalObject).Build()`: Adds a `java.lang.Object` parameter.
- `.WithParameter<JLocalObject>(JArrayObjectLocalRef, out JArrayObject<JLocalObject>).WithParameter(
  JBooleanArrayLocalRef, out JArrayObject<JBoolean>).Build()`: Adds `java.lang.Object[]` and `boolean[]` parameters.
- `.WithParameter<JNumberObject>(JObjectLocalRef, out JNumberObject).Build()`: Adds a `java.lang.Number` parameter.
- `.WithParameter(JStringLocalRef, out JStringObject).WithParameter(JIntArrayLocalRef, out JArrayObject<JInt>)
  .WithParameter<JIntegerObject>(JArrayLocalRef, out JArrayObject<JIntegerObject>).Build()`: Adds `java.lang.String`,
  `int[]`, and `java.lang.Integer[]` parameters.

**Note:** Local references associated with the target instance/class and parameters cannot be manually released.
Calling `.Dispose()` has no effect on these references.

To finalize a call (and remove the call frame in `Rxmxnx.JNetInterface`), the following methods can be used:

* `FinalizeCall()`: Ends a call and removes the associated instances within `Rxmxnx.JNetInterface` (without removing
  references).
* FinalizeCall<TPrimitive>(TPrimitive value): Finalizes a call with a primitive result (boolean, byte, char,
  double, float, int, long, short).
* FinalizeCall(JLocalObject?): Finalizes a call with a result of type java.lang.Object.
* FinalizeCall(JLocalObject.InterfaceView): Finalizes a call with a result of type java.lang.Object.
* FinalizeCall(JClassObject?): Finalizes a call with a result of type java.lang.Class<?>.
* FinalizeCall(JThrowableObject?): Finalizes a call with a result of type java.lang.Throwable.
* FinalizeCall(JStringObject?): Finalizes a call with a result of type java.lang.String.
* FinalizeCall(JArrayObject): Finalizes a call with a result of type array.
* FinalizeCall(JArrayObject<JBoolean>?): Finalizes a call with a result of type boolean[].
* FinalizeCall(JArrayObject<JByte>?): Finalizes a call with a result of type byte[].
* FinalizeCall(JArrayObject<JChar>?): Finalizes a call with a result of type char[].
* FinalizeCall(JArrayObject<JDouble>?): Finalizes a call with a result of type double[].
* FinalizeCall(JArrayObject<JFloat>?): Finalizes a call with a result of type float[].
* FinalizeCall(JArrayObject<JInt>?): Finalizes a call with a result of type int[].
* FinalizeCall(JArrayObject<JLong>?): Finalizes a call with a result of type long[].
* FinalizeCall(JArrayObject<JShort>?): Finalizes a call with a result of type short[].
* FinalizeCall<TElement>(JArrayObject<TElement>?): Finalizes a call with a result of type java.lang.Object[].

#### Type Metadata

Type metadata objects allow `Rxmxnx.JNetInterface` to identify at runtime the types of Java objects referenced through
JNI.  
By leveraging the strong typing features of the .NET platform, it is possible to perform operations on object instances
or their corresponding Java class instances.

The metadata exposes the following properties:

* **ClassName**: This name is the JNI name of the class identified by the metadata. For example, the class
  `java.lang.String` (`JStringObject`) has the JNI name `java/lang/String`.
* **Signature**: This signature allows identifying instances of the class represented by the metadata to access Java
  fields or methods via JNI.  
  For example:

- `long` (`JLong`) has the signature `J`.
- `java.lang.String` has the signature `Ljava/lang/String;`.

* **ArraySignature**: This signature enables the automatic creation of the generic array type for the class identified
  by the metadata.  
  For example:
    - `char` -> `char[]` (`JArrayObject<JChar>`) with the signature `[C`.
    - `java.lang.String` -> `java.lang.String[]` (`JArrayObject<JStringObject>`) with the signature
      `[Ljava/lang/String;`.
* **SizeOf**: This property allows JNI calls to determine the memory size required to store the value returned by JNI.
* **ArgumentMetadata**: Combines the signature of the current type with its memory size.
* **Type**: CLR type of the .NET class or structure representing the class identified by the metadata.
* **Kind**: Identifies the type of the Java class represented by the metadata. For example:
    - `boolean` (`JBoolean`): Primitive.
    - `java.lang.String`: Class.
    - `java.lang.Object[]` (`JArrayObject<JLocalObject>`): Array.
    - `java.io.Serializable` (`JSerializableObject`): Interface.
    - `java.lang.annotation.ElementType` (`JElementTypeObject`): Enum.
    - `java.lang.annotation.Target` (`JTargetObject`): Annotation.
* **Modifier**: Identifies the modifier of the Java class represented by the metadata.
    - `java.lang.String`: Final.
    - `java.lang.Object`: Extensible.
    - `java.lang.Number` (`JNumberObject`): Abstract.

**Notes:**

* The hash of a data type (just like `JClassObject` instances) is the UTF-16 buffer used to store the UTF-8
  sequence containing the class name, the JNI signature of the class, and the name/signature of the array for the data
  type.
* Type metadata has a special implementation of the `.ToString()` method, which may be unnecessary and inconvenient in a
  release version of a product using `Rxmxnx.JNetInterface`. To disable this implementation, the feature switch
  `JNetInterface.DisableTypeMetadataToString` can be used.

##### Metadata Builder

Builders are classes found in the base classes used to initialize type metadata.  
It is recommended to use a single metadata instance to improve runtime performance. <br/>  
These types are `ref struct`, so they are not compatible with the Visual Basic language.

* **`JLocalObject.TypeMetadataBuilder<>`**:  
  This builder allows creating class-type metadata (`IClassType<>`).  
  It must always be initialized with the JNI class name. It is also possible to specify whether the class is `Abstract`
  or `Final`,  
  or by default `Extensible` (which allows other classes to extend it). This can be done using the static method  
  `Create(ReadOnlySpan<Byte>, JTypeModifier)`. <br/>  
  To specify that the metadata class extends another class, the static method of the superclass builder  
  `Create<>(ReadOnlySpan<Byte>, JTypeModifier)` must be used. <br/>  
  The builder also allows specifying that the metadata class implements interfaces through the `.Implements<>()`
  method.  
  Note that the CLR type must implement the `IInterfaceObject<>` interface.

* **`JLocalObject.InterfaceView.TypeMetadataBuilder<>`**:  
  This builder allows creating interface or annotation-type metadata (`IInterfaceType<>`).  
  It must always be initialized with the JNI class name. This can be done using the static method  
  `Create(ReadOnlySpan<Byte>, JTypeModifier)`. <br/>  
  The builder also allows specifying that the metadata interface extends other interfaces through the `.Extends<>()`
  method.  
  Note that the CLR type must implement the `IInterfaceObject<>` interface.

* **`JThrowableObject.TypeMetadataBuilder<>`**:  
  This builder allows creating throwable-type metadata (`IThrowableType<>`).  
  It must always be initialized with the JNI class name. <br/>  
  This builder is identical to `JLocalObject.TypeMetadataBuilder<>`, but the throwable superclass must always be
  specified.

* **`JEnumObject.TypeMetadataBuilder<>`**:  
  This builder allows creating enum-type metadata (`IEnumObject<>`).  
  It must always be initialized with the JNI class name. <br/>  
  This builder is identical to `JLocalObject.TypeMetadataBuilder<>`, but no superclass can be specified,  
  as all enum types extend the `java.lang.Enum` class.

**Note:** All `TypeMetadataBuilder<>` instances perform runtime validations during their construction. However, for a
release build, this validation can be disabled using the feature switch `JNetInterface.DisableMetadataValidation`,
as its primary purpose is design-time validation.

##### Jagged Array Type Metadata

In Java, unlike .NET, there are no multidimensional arrays. Instead, Java uses arrays of arrays. Due to this definition,
`Rxmxnx.JNetInterface` uses reflection to create the metadata for this type of array at runtime to ensure compatibility
with NativeAOT.

However, in reflection-free AOT mode, the automatic creation of these metadata at runtime is not possible.
Therefore, a mechanism must be used to ensure the definition of `JArrayObject<..JArrayObject<...>..>`
is available at runtime.

Even if automatic metadata creation at runtime is not desired, this functionality can be disabled
using the feature switch `JNetInterface.DisableJaggedArrayAutoGeneration`.

#### Argument Metadata

Argument metadata objects allow defining access to Java methods and fields from JNI.  
As previously mentioned, type metadata exposes a property to obtain the argument metadata for a specified type.  
However, it is also possible to retrieve it using the static method `JArgumentMetadata.Get<T>()`, where `T` is a mapped
type.

Additionally, if a mapped type for the argument is not available, it can be created using the method
`JArgumentMetadata.Create(ReadOnlySpan<Byte>)`,  
where the read-only binary span contains the JNI type signature.

For example, to create the signature for `java.util.Dictionary<K,V>`, its signature should be `Ljava/util/Dictionary;`.

**Note:** Creating metadata for primitive types is not supported; to obtain them, the method
`JArgumentMetadata.Get<TPrimitive>()` should be used.

### Java Class Handling

In JNI, handling `java.lang.Class<?>` instances is essential.

By design, `Rxmxnx.JNetInterface` prioritizes class definition availability over maintaining a valid JNI reference. As a
result, `JClassObject` instances can exist symbolically without an actual JNI reference backing them.

In `Rxmxnx.JNetInterface`, class handling is managed through the `JClassObject` class using the following static
methods:

* `GetClass<TDataType>(IEnvironment)`: Returns the loaded instance for the mapped data type. Since it is a mapped type,
  JNI is not used to obtain a local reference to the class.
* `GetClass(IEnvironment, ReadOnlySpan<Byte>)`: Returns the loaded instance for the class whose JNI name is given in the
  read-only span. If the class has been identified previously, JNI is not used to obtain a local reference. Otherwise,
  it uses the JNI `FindClass` function. Note that `FindClass` cannot locate primitive classes, so only non-primitive
  types can be retrieved using this method.
* `GetVoidClass(IEnvironment)`: Returns the loaded instance for the `void` type. Since `void` cannot be represented in
  .NET as an `IDataType`, this special method provides access to it.
  If primitive classes were set as main when initializing interoperability with the JVM,
  a global reference to it will have been loaded.
* `LoadClass(IEnvironment, ReadOnlySpan<Byte>, ReadOnlySpan<Byte>, JClassLoaderObject?)`: Uses the JNI `DefineClass`
  function to define a class within the current JVM instance. The class name is given in the first read-only span, and
  the bytecode is contained in the second span. The returned class will have a valid JNI reference.
* `LoadClass<TReferenceType>(IEnvironment, ReadOnlySpan<Byte>, JClassLoaderObject?)`: Uses the JNI `DefineClass`
  function to define a mapped class within the JVM instance, with its bytecode stored in the provided span. The returned
  class will have a valid JNI reference.

`JClassObject` instances expose the following properties:

* `Name`: The JNI name of the class.
* `Reference`: The JNI reference of the class. If no active JNI reference exists, it returns null.
* `ClassSignature`: The JNI signature of the type represented by the class.
* `Hash`: Equivalent to the hash mentioned for data type metadata.
* `IsFinal`: Indicates whether the class allows extensions.
* `IsArray`: Indicates whether the class represents an array type.
* `IsPrimitive`: Indicates whether the class is a primitive type.
* `IsInterface`: Indicates whether the class is an interface type.
* `IsEnum`: Indicates whether the class is an enum type.
* `IsAnnotation`: Indicates whether the class is an annotation type.
* `ArrayDimension`: Indicates the depth of the array type represented by this class. For example:
    - `java.lang.Error`: 0
    - `java.lang.String[]`: 1
    - `byte[][][]`: 3

The following operations perform JNI calls to retrieve results:

* `GetClassName(out Boolean)`: Invokes Java's `getName()` function and the `isPrimitive()` method.
* `GetInterfaces()`: Calls the Java `getInterfaces()` function.
* `GetInformation()`: Returns the registered type information in `Rxmxnx.JNetInterface` for the class. If it is a mapped
  type, this information should match the type metadata.

Additional operations executed directly in JNI:

* `GetSuperClass()`: Returns the `JClassObject` instance representing the superclass of the current class. If the class
  is identified as an `Array`, `Interface`, or `Enum`, JNI calls are not required, and symbolic instances may be
  returned. Otherwise, a valid JNI reference is always returned.
* `IsAssignableTo(JClassObject)`: Determines whether an object of the current class type can be assigned to a variable
  of the given class type using the JNI `IsAssignableFrom` function.
* `Register(..JNativeCallEntry..)`: Defines Java native methods mapped to .NET local methods using the JNI
  `RegisterNatives` function.
* `UnregisterNativeCalls()`: Removes the registration of Java native methods mapped to .NET using the JNI
  `UnregisterNatives` function.
* `GetModule()`: This method is only effective in JVM instances compatible with Java 9.0+.

**Notes:**

* When using a class, if it has not been loaded (i.e., there is no active JNI reference in the current
  context), a local reference will be loaded in the active frame.
* The functionality of the `JNativeCallEntry` class will be detailed later.

For more information on using these methods, refer to
the [included example application](./src/ApplicationTest/README.md) in this repository.

For more information about essential and main classes, please refer to the documentation on compatibility with
[GraalVM Native Image](./native-image/README.md), where the implementation details are explained in greater depth.

### Java String Handling

JNI allows special handling of `java.lang.String` instances (Java Strings). `Rxmxnx.JNetInterface` exposes these APIs
through the `JStringObject` class.

#### String Creation

To create a new array through JNI, `Rxmxnx.JNetInterface` exposes the following static methods in the class
`JStringObject`:

* **Create(IEnvironment, String?)**: Creates a Java `String` from a .NET `String` instance. The .NET `String` is kept in
  memory associated with the created instance. If the given `String` is null, it will return null. The value of the
  `String` can be accessed through the `Value` property.
* **Create(IEnvironment, ReadOnlySpan<Char>)**: Creates a Java `String` from a read-only span of UTF-16 characters. The
  `Value` property will generate a JNI call to retrieve the instance's value.
* **Create(IEnvironment, CString?)**: Creates a Java `String` from a `CString` instance. If the given `CString` is null,
  it will return null. The `Utf8Length` property will be initialized with the length of the `CString`.
* **Create(JClassObject, ReadOnlySpan<Byte>)**: Creates a Java `String` from a read-only span of UTF-8 bytes. The
  `Utf8Length` property will be initialized with the length of the read-only span.

*Note:* UTF-8 based creation methods comply with the following characteristics:

- The UTF-8 text must end with a null UTF-8 character.
- The termination character must not be part of the sequence. This is ensured in certain `CString` instances or .NET
  UTF-8/ASCII literals.
- The UTF-8 text must not contain null UTF-8 characters.

The properties exposed by this class are:

* **Length**: Number of UTF-16 characters in the Java string. If not initialized, a call to the JNI method
  `GetStringLength` will be made.
* **Utf8Length**: Number of UTF-8 units in the Java string. If not initialized, a call to the JNI method
  `GetStringUTFLength` will be made.
* **Value**: String value of the current Java string. If not initialized, a call to the JNI method `GetStringRegion`
  will be made.
* **Reference**: JNI reference to the instance.

**Notes:**

* The `JStringObject` class implements the following interfaces: `IEnumerable<Char>`, `IComparable`,
  `IComparable<String?>`, and `IComparable<JStringObject?>`.
* The characters and UTF-8 units of Java strings are represented by the CLR structures `System.Char` and `System.Byte`,
  respectively.

#### Native Characters

JNI allows native access to the characters of a Java string. `Rxmxnx.JNetInterface` provides this functionality through
the following methods:

* **GetNativeChars(JMemoryReferenceKind)**: Equivalent to JNI's `GetStringChars` calls. The `JMemoryReferenceKind`
  parameter allows `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference to back native memory
  allocation.
* **GetCriticalChars(JMemoryReferenceKind)**: Equivalent to the `GetStringCritical` call. The `JMemoryReferenceKind`
  parameter enables `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference to pin memory.
* **GetNativeUtf8Chars(JMemoryReferenceKind)**: Equivalent to JNI's `GetStringUTFChars` calls. The
  `JMemoryReferenceKind` parameter allows `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference to
  back native memory allocation.
* **Get(Span<Char>, Int32)**: Equivalent to JNI's `GetStringRegion` calls. The integer serves as the offset for the
  copy.
* **GetUtf8(Span<Byte>, Int32)**: Equivalent to JNI's `GetStringUTFRegion` calls. The integer serves as the offset for
  the copy.
* **GetChars(Int32)**: Equivalent to JNI's `GetStringRegion` calls. The given integer serves as the starting point of
  the substring.
* **GetChars(Int32, Int32)**: Equivalent to JNI's `GetStringRegion` calls. The first given integer serves as the
  starting point of the substring, and the second defines its length.
* **GetUtf8(Int32)**: Equivalent to JNI's `GetStringUTFRegion` calls. The given integer serves as the starting point of
  the UTF-8 substring.
* **GetUtf8(Int32, Int32)**: Equivalent to JNI's `GetStringUTFRegion` calls. The first given integer serves as the
  starting point of the UTF-8 substring, and the second defines its length.

**Notes:**

* The `GetNativeChars`, `GetNativeUtf8Chars` and `GetCriticalChars` methods return a `JNativeMemory<T>` instance, which
  represents native/pinned read-only memory accessible via JNI. This memory must be released using the `Dispose()`
  method.
* The `JMemoryReferenceKind` value specifies whether to create an additional JNI reference to manage the native memory.
  This is intended to allow free manipulation of that memory across different threads.
* When the Value property is initialized the `GetChars` methods use it to create the substrings.

##### Native Memory

Native memory in `Rxmxnx.JNetInterface` is represented as a fixed memory context through a memory adapter
encapsulated in an instance of the `JNativeMemomory<T>` class. This memory is read-only.

**Properties:**

* **ReleaseMode**: Sets the release mode of the native memory. If the memory is critical (directly mapped to the string
  in the JVM), this property is `null` and cannot be set.
* **Values**: Provides access to native memory via a rad-only span.
* **Copy**: Indicates whether the native memory is a copy of the string data.
* **Critical**: Indicates whether the native memory is directly the string memory pinned by the JVM.
* **Pointer**: Pointer to the native memory.

**Note:** If the native memory was allocated with an exclusive JNI reference, releasing the memory will also release
the associated JNI reference.

### Java Array Handling

JNI allows the manipulation and creation of Java arrays from any type. `Rxmxnx.JNetInterface` exposes APIs through
the `JArrayObject<>` class to achieve this goal.

#### Array Creation

To create a new array through JNI, `Rxmxnx.JNetInterface` exposes the following static methods in the generic class
`JArrayObject<>`:

* **Create(IEnvironment, Int32)**: Creates an array of the type specified by the generic class with the length given by
  the integer value.
* **Create(IEnvironment, Int32, T)**: Creates an array of the type specified by the generic class with the length given
  by the integer value and fills it with the specified value instance.
* **Create(IEnvironment, Int32, JClassObject)**: Creates an array of the type specified by the generic class with the
  length given by the integer value, using the provided class as the element type.
* **Create(JClassObject, Int32, T)**: Creates an array of the type specified by the generic class with the length given
  by the integer value and fills it with the specified value instance, using the provided class as the element type.

**Notes:**

* If an array is created by specifying the element class, it must be compatible with the element type. In the
  case of primitive arrays, only the corresponding primitive array class will be considered compatible.
* Methods that initialize arrays with a specified value are only recommended for creating non-primitive arrays.

#### Non-Generic Class

In Java, arrays are views. For example, an instance of `java.lang.String[][]` can be treated as an instance of
`java.lang.Object[]`, `java.lang.Object[][]`, or even `java.io.Serializable[][]`. However, despite the inherent
polymorphism of views, arrays are instances of specific types. This instance is represented in `Rxmxnx.JNetInterface`
as a non-generic `JArrayObject`.

The properties exposed by this class are:

* **Length**: Number of elements in the Java array.
* **Reference**: JNI reference to the instance.

#### Generic Class

In `Rxmxnx.JNetInterface`, the generic class `JArrayObject<T>` encapsulates a non-generic `JArrayObject` instance
to enable element retrieval and assignment operations. The generic type of this class represents the element type
contained or "viewed" in the array instance.

The properties exposed by this class are:

* **Length**: Number of elements in the Java array.
* **Reference**: JNI reference to the instance.

**Note:** These properties are read directly from the underlying non-generic instance that supports the generic view.

To get or set an array element, the class exposes an indexer. Additionally, this class implements various .NET
interfaces such as `IList<T>` and `IReadOnlyList<T>`.

#### Primitive Arrays

Unlike non-primitive arrays, primitive-type arrays in Java are not polymorphic. Through JNI, it is possible to
directly access the memory segment occupied by these arrays.

For this reason, when the generic array type is detected as primitive, the following extensions become available:

* **GetElements(JMemoryReferenceKind)**: Equivalent to JNI's `Get<PrimitiveType>ArrayElements` calls. The
  `JMemoryReferenceKind` parameter allows `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference
  to back native memory allocation.
* **GetPrimitiveArrayCritical(JMemoryReferenceKind)**: Equivalent to the `GetPrimitiveArrayCritical` call. The
  `JMemoryReferenceKind` parameter enables `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference
  to pin memory.
* **ToArray(Int32)**: Creates a .NET array of the primitive type, using `Get<PrimitiveType>ArrayRegion`. The integer
  parameter acts as an offset for the copy.
* **ToArray(Int32, Int32)**: Creates a .NET array of the primitive type, using `Get<PrimitiveType>ArrayRegion`. The
  first integer serves as an offset for the copy, while the second specifies how many elements should be copied.
* **Get(Span<T>, Int32)**: Uses `Get<PrimitiveType>ArrayRegion` to copy array elements into the provided span. The
  integer parameter serves as an offset, and the span's length determines the number of elements to copy.
* **Set(ReadOnlySpan<T>, Int32)**: Uses `Set<PrimitiveType>ArrayRegion` to copy elements from the span to the array.
  The integer parameter serves as an offset, and the span's length determines the number of elements to copy.

**Note:**

* The `GetElements` and `GetPrimitiveArrayCritical` methods return a `JPrimitiveMemory<T>` instance, which
  represents native/pinned memory accessible via JNI. This memory must be released using the `Dispose()` method.
* The `JMemoryReferenceKind` value specifies whether to create an additional JNI reference to manage the native memory.
  This is intended to allow free manipulation of that memory across different threads.

##### Primitive Memory

Primitive array memory in `Rxmxnx.JNetInterface` is represented as a fixed memory context through a memory adapter
encapsulated in an instance of the `JPrimitiveMemory<T>` class. This memory allows both reading and writing.

**Properties:**

* **ReleaseMode**: Sets the release mode of the native memory. If the memory is critical (directly mapped to the array
  in the JVM), this property is `null` and cannot be set.
* **Values**: Provides access to native memory via a span.
* **Copy**: Indicates whether the native memory is a copy of the array data.
* **Critical**: Indicates whether the native memory is directly the array memory pinned by the JVM.
* **Pointer**: Pointer to the native memory.

**Note:** If the native memory was allocated with an exclusive JNI reference, releasing the memory will also release
the associated JNI reference.

### Java Member Handling

JNI allows the handling to Java class members. Accessible definitions are objects that allow access to Java methods and
fields (both class and instance) via JNI. Every accessible definition is an instance of `JAccessibleObjectDefinition`.

**Note:** Accessible definitions can be identified by their name and descriptor.  
The hash of a definition is the UTF-16 buffer used to store the UTF-8 sequence containing these two elements.

#### Accessing Java Fields

JNI allows access to Java fields, both class (static) and instance fields.  
This access can be read (`get`) or write (`set`).  
`Rxmxnx.JNetInterface` provides the same functionality through `JFieldDefinition` instances.

                                                            ┌──  JFieldDefinition<> (Generic)
       JAccessibleObjectDefinition  ──  JFieldDefinition  ──┤
                                                            └──  JNonTypedFieldDefinition

**Notes:**

* The `JFieldDefinition<..>` type allows defining a Java field of a generic `IDataType<..>`, while  
  `JNonTypedFieldDefinition` allows defining fields of object types whose class is not mapped in  
  `Rxmxnx.JNetInterface`.
* Creating a `JNonTypedFieldDefinition` definition for a primitive field is not supported; instead,  
  a `JFieldDefinition<..>` instance must be used.

The APIs exposed by `JNonTypedFieldDefinition` are identical to those of a `JFieldDefinition<JLocalObject>` instance.

To get or set a field using `Rxmxnx.JNetInterface`, the following options are available:

* `Get(JLocalObject)`: Retrieves a field from the given instance. The declaring class is the object's own class.
* `Get(JLocalObject, JClassObject)`: Retrieves a field from the given instance, using the provided class  
  as the declaring class. This is useful for efficiently handling field declarations in the superclasses  
  of multiple instances.
* `GetReflected(JFieldObject, JLocalObject)`: Retrieves the reflected field in the given instance.
* `StaticGet(JClassObject)`: Retrieves a static field from the given class.
* `StaticGetReflected(JFieldObject)`: Retrieves the reflected static field.
* `Set(JLocalObject, T)`: Sets a field in the given instance. The declaring class is the object's own class.
* `Set(JLocalObject, JClassObject, T)`: Sets a field in the given instance, using the provided class  
  as the declaring class.
* `SetReflected(JFieldObject, JLocalObject, T)`: Sets the reflected field in the given instance.
* `StaticSet(JClassObject, T)`: Sets a static field in the given class.
* `StaticSetReflected(JFieldObject, T)`: Sets a static field in the reflected class.

**Note:** When using a class, if it has not been loaded (i.e., there is no active JNI reference in the current
context), a local reference will be loaded in the active frame.

#### Accessing Java Calls

JNI allows access to both instance and class (static) methods in Java. When methods return a primitive value or an  
object, they are referred to as functions.  
`Rxmxnx.JNetInterface` provides the same functionality through `JCallDefinition` instances. Constructors are not  
considered static functions in JNI but share a strong similarity with them when invoked.

                                                           ┌──  JMethodDefinition  ──  JMethodDefinition.Parameterless      
       JAccessibleObjectDefinition  ──  JCallDefinition  ──┼── JConstructorDefinition  ──  JConstructorDefinition.Parameterless
                                                           │                           ┌──  JFunctionDefinition<> (Generic)  ──  JFunctionDefinition<>.Parameterless
                                                           └──  JFunctionDefinition  ──┤
                                                                                       └──  JNonTypedFunctionDefinition

**Notes:**

* The `JFunctionDefinition<..>` type allows defining a Java function with a generic `IDataType<..>`, while  
  `JNonTypedFunctionDefinition` allows defining functions returning an object whose class is not mapped in  
  `Rxmxnx.JNetInterface`.
* Creating a `JNonTypedFunctionDefinition` for a primitive function is not supported; a `JFunctionDefinition<..>`  
  instance must be used instead.
* `Parameterless` types are definitions of functions, methods, or constructors that do not take parameters.
* The APIs exposed by `JNonTypedFunctionDefinition` are public and impose no restrictions on the arguments of the  
  defined calls.
* Call definitions also expose the call parameter count and the call's byte size.

To invoke methods and functions using `Rxmxnx.JNetInterface`, the following options are available:

* **Invoke(JLocalObject, ReadOnlySpan<IObject?>)**: Invokes the method or function on the given instance using  
  the objects in the read-only span as arguments. The declaring class is inferred from the object's class.
* **InvokeReflected(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)**: Invokes the reflected method or  
  function on the given instance using the objects in the read-only span as arguments.
* **Invoke(JLocalObject, JClassObject, ReadOnlySpan<IObject?>)**: Invokes the method or function on the given  
  instance using the objects in the read-only span as arguments. The declaring class is explicitly provided.  
  This is useful for efficiently managing method calls on superclass declarations across multiple instances.
* **InvokeNonVirtual(JLocalObject, JClassObject, ReadOnlySpan<IObject?>)**: Invokes the method or function on the  
  given instance using the objects in the read-only span as arguments, but it forces the implementation from  
  the explicitly provided class.
* **InvokeNonVirtualReflected(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)**: Invokes the reflected  
  method or function on the given instance using the objects in the read-only span as arguments, but it forces  
  the implementation from the class where the method was originally reflected.
* **StaticInvoke(JClassObject, ReadOnlySpan<IObject?>)**: Invokes a static method or function in the given class  
  using the objects in the read-only span as arguments.
* **InvokeStaticReflected(JMethodObject, ReadOnlySpan<IObject?>)**: Invokes a reflected static method or function  
  in the declaring class of the reflected method using the objects in the read-only span as arguments.

To create instances using constructors in `Rxmxnx.JNetInterface`, the following options are available:

* **New(JClassObject, ReadOnlySpan<IObject?>)**: Creates a new instance of the given class using the constructor,  
  passing the objects in the read-only span as arguments.
* **New<>(IEnvironment, ReadOnlySpan<IObject?>)**: Creates a new instance of the generic type using the constructor,  
  passing the objects in the read-only span as arguments.
* **NewReflected(JConstructorObject, ReadOnlySpan<IObject?>)**: Creates a new instance using the reflected
  constructor,  
  passing the objects in the read-only span as arguments.
* **NewReflected<>(IEnvironment, ReadOnlySpan<IObject?>)**: Creates a new instance of the generic type using the  
  reflected constructor, passing the objects in the read-only span as arguments. The generic type must be a superclass  
  of the class where the reflected constructor is defined.

**Notes:**

* These APIs in the `JMethodDefinition`, `JFunctionDefinition<..>`, and `JConstructorDefinition` classes are exposed  
  as protected.
* These APIs in `Parameterless` classes are public and do not receive arguments.
* In .NET 9.0+, the `ReadOnlySpan<IObject?>` parameter uses the `params` keyword.
* When using classes, either as the declaring class or as a function argument, if the class has not been loaded,  
  meaning there is no active JNI reference in the current context, a local reference will be loaded in the active frame.

##### Indeterminate Calls

Indeterminate calls encapsulate Java call definitions (`JCallDefinition`), allowing methods, functions, and constructors
to be defined and executed in a flexible and dynamic manner.

###### Creating Definitions

The following static methods can be used to create definitions:

* `CreateConstructorDefinition(ReadOnlySpan<JArgumentMetadata>)`: Creates an `IndeterminateCall` instance that
  encapsulates the definition of a constructor whose parameters are defined by the metadata in the read-only span.
* `CreateMethodDefinition(ReadOnlySpan<JArgumentMetadata>)`: Creates an `IndeterminateCall` instance that encapsulates
  the definition of a method whose parameters are defined by the metadata in the read-only span.
* `CreateMethodDefinition(ReadOnlySpan<Byte>, ReadOnlySpan<JArgumentMetadata>)`: Creates an `IndeterminateCall` instance
  that encapsulates the definition of a method whose name is represented by the first read-only span, and whose
  parameters are defined by the metadata in the second read-only span. If the method name is `<init>`, it encapsulates a
  constructor.

###### Method Calls

The `IndeterminateCall` class provides the following options for method calls:

* `MethodCall(JLocalObject, ReadOnlySpan<IObject?>)`: Calls a method on the current object using its class as the
  declaring class. If the call corresponds to a function, it invokes it and discards the result.
* `MethodCall(JLocalObject, JClassObject, Boolean, ReadOnlySpan<IObject?>)`: Calls a method on the current object using
  the specified class as the declaring class. If the boolean parameter is `true`, it performs a non-virtual call. If the
  call corresponds to a function, it invokes it and discards the result.
* `StaticMethodCall(JClassObject, ReadOnlySpan<IObject?>)`: Calls a static method on the specified class. If the call
  corresponds to a function, it invokes it and discards the result. If the call corresponds to a constructor, it calls
  the constructor and discards the created instance.
* `ReflectedMethodCall(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`: Calls a reflected method on the current
  object. If the call corresponds to a function, it invokes it and discards the result.
* `ReflectedMethodCall(JMethodObject, JLocalObject, Boolean, ReadOnlySpan<IObject?>)`: Calls a reflected method on the
  current object. If the boolean parameter is `true`, it performs a non-virtual call. If the call corresponds to a
  function, it invokes it and discards the result.
* `ReflectedStaticMethodCall(JExecutableObject, ReadOnlySpan<IObject?>)`: Calls a reflected static method. If the call
  corresponds to a function, it invokes it and discards the result. If the call corresponds to a constructor, it calls
  the constructor and discards the created instance.

###### Constructor Calls

The `IndeterminateCall` class provides the following options for constructor calls:

* `NewCall(JClassObject, ReadOnlySpan<IObject?>)`: Calls the constructor in the specified class and returns the created
  instance.
* `NewCall<TObject>(IEnvironment, ReadOnlySpan<IObject?>)`: Calls the constructor in the class of the specified type and
  returns the created instance.
* `ReflectedNewCall(JConstructorObject, ReadOnlySpan<IObject?>)`: Calls the reflected constructor and returns the
  created instance.
* `ReflectedNewCall<TObject>(JConstructorObject, ReadOnlySpan<IObject?>)`: Calls the reflected constructor and returns
  the created instance. The generic type must be a superclass of the constructor’s class.

*Note:* If the call does not correspond to a constructor, an exception will be thrown.

###### Function Calls

The `IndeterminateCall` class provides the following options for function calls, each returning an instance of
`IndeterminateResult`:

* `FunctionCall(JLocalObject, ReadOnlySpan<IObject?>)`: Calls a function on the current object using its class as the
  declaring class. If the call corresponds to a method, it invokes the method and returns an empty result.
* `FunctionCall(JLocalObject, JClassObject, Boolean, ReadOnlySpan<IObject?>)`: Calls a function on the current object
  using the given class as the declaring class. If the boolean parameter is `true`, it performs a non-virtual call. If
  the call corresponds to a method, it invokes the method and returns an empty result.
* `StaticFunctionCall(JClassObject, ReadOnlySpan<IObject?>)`: Calls a static function on the given class. If the call
  corresponds to a method, it invokes the method and returns an empty result. If the call corresponds to a constructor,
  it calls the constructor and returns the created instance in the result.
* `ReflectedFunctionCall(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`: Performs a reflected function call on
  the current object. If the call corresponds to a method, it invokes the method and returns an empty result.
* `ReflectedFunctionCall(JMethodObject, JLocalObject, Boolean, ReadOnlySpan<IObject?>)`: Performs a reflected function
  call on the current object. If the boolean parameter is `true`, it performs a non-virtual call. If the call
  corresponds to a method, it invokes the method and returns an empty result.
* `ReflectedStaticFunctionCall(JExecutableObject, ReadOnlySpan<IObject?>)`: Performs a reflected static function call.
  If the call corresponds to a method, it invokes the method and returns an empty result. If the call corresponds to a
  constructor, it calls the constructor and returns the created instance in the result.

*Note:* `IndeterminateResult` is a `ref struct`, making it incompatible with Visual Basic.

##### Indeterminate Result

`IndeterminateResult` is a `ref struct` that securely stores the result of indeterminate access to a Java value.
To process its value, the following properties are available:

* **Signature**: Contains the UTF-8 name of the signature of the instance represented in the indeterminate result.
* **BooleanValue**: Returns the boolean value of the result.
* **ByteValue**: Returns the byte value of the result.
* **CharValue**: Returns the char value of the result.
* **DoubleValue**: Returns the double value of the result.
* **FloatValue**: Returns the float value of the result.
* **IntValue**: Returns the int value of the result.
* **LongValue**: Returns the long value of the result.
* **ShortValue**: Returns the short value of the result.
* **Object**: Returns the object from the result.

**Note:** The `Object` property will only have a value when the signature in the result does not correspond to a
primitive type.
Primitive values in the result are available even if the signature in the result does not correspond to a primitive type
or its specific primitive type.

Thus:

* The `BooleanValue` will always be `true` if the primitive value in the result is different from its default value or
  if the object in the result is not null.
* The other primitive values might be different from their default values if the object in the result is an instance of
  `java.lang.Character` (`JCharObject`) or `java.lang.Number`, or if the actual primitive value in the result, when
  converted to the primitive type, differs from the default value.

#### Defining Native Java Calls

The `JNativeCallEntry` class allows defining the implementation of Java native methods and functions with compatible
implementations created in .NET.  
These methods must follow the JNI convention for parameter definitions, but they do not require the JNI naming
convention.

To create `JNativeCallEntry` instances, `Rxmxnx.JNetInterface` exposes the following static methods:

- `Create(JMethodDefinition, IntPtr)`: Creates an instance that allows defining the .NET implementation of the native
  method that matches the definition of the `JMethodDefinition` instance using an unmanaged pointer to the method.
- `Create(JFunctionDefinition, IntPtr)`: Creates an instance that allows defining the .NET implementation of the native
  function that matches the definition of the `JFunctionDefinition` instance using an unmanaged pointer to the method.
- `Create<T>(JMethodDefinition, T)`: Creates an instance that allows defining the .NET implementation of the native
  method that matches the definition of the `JMethodDefinition` instance using a managed delegate to the method.
- `Create<T>(JFunctionDefinition, T)`: Creates an instance that allows defining the .NET implementation of the native
  function that matches the definition of the `JFunctionDefinition` instance using a managed delegate to the method.
- `CreateParameterless(ParameterlessInstanceMethodDelegate)`: Creates an instance that allows defining the .NET
  implementation of a parameterless native instance method using a managed delegate to the method.
- `CreateParameterless(ParameterlessStaticMethodDelegate)`: Creates an instance that allows defining the .NET
  implementation of a parameterless native static method using a managed delegate to the method.

**Notes:**

- Creating instances with pointers is ideal when using unsafe contexts
  and [function pointers](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code#function-pointers).
- Creating instances with delegates keeps the delegate instance in memory and will only be collected by the GC when the
  native implementation is replaced or removed.
- Creating instances with delegates
  uses [Marshalling](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.getfunctionpointerfordelegate).
- The delegate type `ParameterlessInstanceMethodDelegate` represents a JNI native function without parameters (
  `JEnvironmentRef, JObjectLocalRef`).
- The delegate type `ParameterlessStaticMethodDelegate` represents a JNI native function without parameters (
  `JEnvironmentRef, JClassLocalRef`).

The `JNativeCallEntry` class exposes the following properties:

- `Name`: Name of the native call. This property comes from the call definition used when creating the instance.
- `Descriptor`: Descriptor of the native call. This property comes from the call definition used when creating the
  instance.
- `Hash`: Hash of the native call definition.
- `Pointer`: Unmanaged pointer to the native implementation of the call.
- `Delegate`: Delegate instance used for the native implementation of the call.

### Direct Buffer Handling

Since JNI 1.4, native handling of Java NIO (New Input/Output) buffers and the creation of direct buffers have been
supported.

The NIO object hierarchy may vary depending on the JVM implementation, but in `Rxmxnx.JNetInterface`, the following
hierarchy is established:

                                                ┌──  JDirectBufferObject
       IInterfaceObject<JDirectBufferObject>  ──┴──  IDirectBufferObject  ──  IDirectBufferObject<>  ──┐
                                           ┌──  JByteBuffer  ──  JMappedByteBufferObject  ─────────────┴──  JDirectByteBufferObject 
                                           ├──  JCharBufferObject 
                                           ├──  JDoubleBufferObject 
       JBufferObject  ──  JByteBuffer<>  ──┼──  JFloatBufferObject 
                                           ├──  JIntBufferObject 
                                           ├──  JLongBufferObject 
                                           └──  JShortBufferObject

#### `JBufferObject` exposes the following properties:

- **IsDirect**: Indicates whether the buffer is direct or managed by the JVM.
- **Capacity**: The capacity in units of the primitive type of the buffer.
- **Address**: Pointer to the buffer's memory. Valid only when the buffer is direct.

**Note:** If this feature is not going to be used, it is recommended to disable the automatic registration of NIO types
to improve performance using the feature switch `JNetInterface.DisableNioAutoRegistration`.

#### Direct Buffer Creation

Direct buffer creation is supported in `Rxmxnx.JNetInterface` through the following static methods of the `JByteBuffer`
class:

- `CreateDirectBuffer<T>(IEnvironment, Memory<T>)`: Creates a direct byte buffer using a `System.Memory<T>` instance,
  which remains pinned until the created instance is discarded. The memory item type must be unmanaged.
- `WithDirectBuffer(IEnvironment, Int32, Action<JByteBufferObject>)`: Creates a temporary direct buffer of the specified
  capacity and executes the delegate. Once execution is completed, the buffer is discarded.
- `WithDirectBuffer<TState>(IEnvironment, Int32, TState, Action<JByteBufferObject, TState>)`: Creates a temporary direct
  buffer of the specified capacity and executes the delegate, including a state object. Once execution is completed, the
  buffer is discarded.
- `WithDirectBuffer<TResult>(IEnvironment, Int32, Func<JByteBufferObject, TResult>)`: Creates a temporary direct buffer
  of the specified capacity and executes the delegate, returning its result. Once execution is completed, the buffer is
  discarded.
- `WithDirectBuffer<TState, TResult>(IEnvironment, Int32, TState, Func<JByteBufferObject, TState, TResult>)`: Creates a
  temporary direct buffer of the specified capacity and executes the delegate, including a state object and returning
  its result. Once execution is completed, the buffer is discarded.

**Notes:**

- The `WithDirectBuffer` methods may allocate memory on the stack if the configured usable stack byte limit is not
  exceeded.
- In .NET 9.0+, the generic state type parameter allows `ref struct`.

### Java Error Handling

JNI allows handling errors and exceptions within native code. `Rxmxnx.JNetInterface` follows the same principle but in a
more .NET-friendly manner.

Just like in Java, the `java.Lang.Throwable` class hierarchy is:

                           ┌──  JErrorObject
                           │
       JThrowableObject  ──┴──  JExceptionObject  ──  JRuntimeExceptionObject

**Note:** `Rxmxnx.JNetInterface` provides several built-in `Throwable` types that may be thrown during basic operations.
However, this can introduce additional memory and performance costs. To optimize performance, the automatic registration
of these types can be disabled using the feature switch `JNetInterface.DisableBuiltInThrowableAutoRegistration`.

The additional throwable types are:

- `java.lang.LinkageError` (`JLinkageErrorObject`)
- `java.lang.ClassCircularityError` (`JClassCircularityErrorObject`)
- `java.lang.UnsatisfiedLinkError` (`JUnsatisfiedLinkErrorObject`)
- `java.lang.ClassFormatError` (`JClassFormatErrorObject`)
- `java.lang.ExceptionInInitializerError` (`JExceptionInInitializerErrorObject`)
- `java.lang.IncompatibleClassChangeError` (`JIncompatibleClassChangeErrorObject`)
- `java.lang.NoSuchFieldError` (`JNoSuchFieldErrorObject`)
- `java.lang.NoSuchMethodError` (`JNoSuchMethodErrorObject`)
- `java.lang.NoClassDefFoundError` (`JNoClassDefFoundErrorObject`)
- `java.lang.VirtualMachineError` (`JVirtualMachineErrorObject`)
- `java.lang.InternalError` (`JInternalErrorObject`)
- `java.lang.OutOfMemoryError` (`JOutOfMemoryErrorObject`)
- `java.lang.SecurityException` (`JSecurityExceptionObject`)
- `java.lang.InterruptedException` (`JInterruptedExceptionObject`)
- `java.text.ParseException` (`JParseExceptionObject`)
- `java.io.IOException` (`JIoExceptionObject`)
- `java.io.FileNotFoundException` (`JFileNotFoundExceptionObject`)
- `java.net.MalformedURLException` (`JMalformedUrlExceptionObject`)
- `java.lang.ReflectiveOperationException` (`JReflectiveOperationExceptionObject`)
- `java.lang.InstantiationException` (`JInstantiationExceptionObject`)
- `java.lang.ClassNotFoundException` (`JClassNotFoundExceptionObject`)
- `java.lang.IllegalAccessException` (`JIllegalAccessExceptionObject`)
- `java.lang.reflect.InvocationTargetException` (`JInvocationTargetExceptionObject`)
- `java.lang.ArrayStoreException` (`JArrayStoreExceptionObject`)
- `java.lang.NullPointerException` (`JNullPointerExceptionObject`)
- `java.lang.IllegalStateException` (`JIllegalStateExceptionObject`)
- `java.lang.ClassCastException` (`JClassCastExceptionObject`)
- `java.lang.ArithmeticException` (`JArithmeticExceptionObject`)
- `java.lang.IllegalArgumentException` (`JIllegalArgumentExceptionObject`)
- `java.lang.NumberFormatException` (`JNumberFormatExceptionObject`)
- `java.lang.IndexOutOfBoundsException` (`JIndexOutOfBoundsExceptionObject`)
- `java.lang.ArrayIndexOutOfBoundsException` (`JArrayIndexOutOfBoundsExceptionObject`)
- `java.lang.StringIndexOutOfBoundsException` (`JStringIndexOutOfBoundsExceptionObject`)

#### JNI Error Handling

JNI can generate errors when executing a process. `Rxmxnx.JNetInterface` maps these errors to throw exceptions managed
by the CLR.

The hierarchy of managed exceptions is:

                       ┌──  CriticalException
                       │
       JniException  ──┴──  ThrowableException

`JniException` exceptions occur when a JNI call returns a value other than Ok.
The identified values of JResult are: Ok, Error, DetachedThreadError, VersionError, MemoryError, ExitingError,
InvalidArgumentsError.

To determine the result that triggered the exception, the `Result` property can be consulted.

Although a JNI method can return any arbitrary negative JResult value, only the value `0 (Ok)` is considered a non-error
result. All other values are considered errors.

`ThrowableException` exceptions represent pending throwable instances thrown in the JVM.

These exceptions are instances of `IThrowableException`. If the throwable type thrown in the JVM is mapped and
registered (or one of its superclasses) in `Rxmxnx.JNetInterface`, the `ThrowableException` instance will be of the
`IThrowableException<>` type of the registered throwable.

Unlike pure `JniException` exceptions (resulting from a JNI error response), `ThrowableException` instances require
special handling. Similar to JNI, `Rxmxnx.JNetInterface` restricts API usage in the presence of a pending exception.

The `ThrowableException` class exposes the following properties:

- **EnvironmentRef**: Reference to the environment (JVM-attached thread) where the throwable instance was thrown.
- **ThreadId**: Identifier of the CLR-managed thread where the throwable instance was thrown.
- **ThrowableRef**: Global JNI reference to the thrown throwable instance. This reference will be released when the CLR
  garbage collector finds no strong references to the `ThrowableException` instance.

When a `ThrowableException` is pending,
only [certain specific JNI functions](https://developer.android.com/training/articles/perf-jni?hl=en#exceptions) can be
executed. Any process involving a different type of JNI call will result in an `InvalidOperationException` being thrown
in the CLR.

When the environment in which the exception occurs is in a critical state, `Rxmxnx.JNetInterface` will throw a
`CriticalException` in the CLR. Only when the current environment exits the critical state can the
`ThrowableException` be thrown.

To retrieve and set (throw) the pending exception in the JVM, the `PendingException` property of the `IEnvironment`
interface can be used.

- If the property returns `null`, it means the current thread has no pending exceptions in the JVM.
- Setting the property to `null` clears the current pending exception, equivalent to the JNI `ExceptionClear` call.
- If attempt to use the property to retrieve the pending exception or to rethrow an exception while in a critical
  state, a `CriticalException` will be thrown in the CLR.

To safely manipulate the `JThrowableObject` instance behind the exception, the `ThrowableException` class provides the
following APIs:

- `WithSafeInvoke(Action<JThrowableObject>)`: Creates a `JThrowableObject` instance and executes the delegate in a safe
  environment for any JNI call.
- `WithSafeInvoke<TResult>(Func<JThrowableObject, TResult>)`: Creates a `JThrowableObject` instance and executes the
  delegate in a safe environment for any JNI call, returning the execution result.

The `IThrowableException<>` interface exposes the same methods for the specific throwable type. For example, if the
exception is an instance of `IThrowableException<JErrorObject>`, the delegate parameters change to
`Action<JErrorObject>` and `Func<JErrorObject, TResult>`.

**Notes:**

- The `WithSafeInvoke` methods ensure a clean execution environment by creating a new thread attached to the JVM and
  executing the delegates on that thread.
- If exception information only needs to be sent to the error reporting channel, the `DescribeException()` method of the
  `IEnvironment` instance with the pending exception can be used.
- `ThrowableException` instances cannot be created directly, meaning the environment's `PendingException` property can
  only be used to rethrow exceptions already created in the system.
- To throw exceptions from a specific `JThrowableObject` instance, the `Throw(Boolean)` method can be used. This will
  create the exception, set it as pending in the environment, and throw it in the CLR if the boolean parameter specifies
  so.

The `JThrowableObject` class allows exceptions instance of any throwable type to be thrown by specifying the message via
JNI:

- `Throw(JClassObject, String, Boolean)`: Throws an exception instanceof the given class type with the specified
  message. It will throw in the CLR if the boolean parameter specifies so.
- `Throw(JClassObject, CString, Boolean)`: Throws an exception instance of the given class type with the specified
  message. It will throw in the CLR if the boolean parameter specifies so.
- `Throw<TThrowable>(IEnvironment, String, Boolean)`: Throws an exception instance of the generic throwable type with
  the specified message. It will throw in the CLR if the boolean parameter specifies so.
- `Throw<TThrowable>(IEnvironment, CString, Boolean)`: Throws an exception instanceof the generic throwable type with
  the specified message. It will throw in the CLR if the boolean parameter specifies so.

**Note:** The static methods `Throw` and `Throw<>` use the JNI `ThrowNew` call.