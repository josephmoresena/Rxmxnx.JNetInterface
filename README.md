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

## How to use it?

`Rxmxnx.JNetInterface` allows the use of JNI through high-level, safe APIs (Core Assembly) and a compatible
implementation (Main Assembly) with the JVM. <br/>

The following table shows the equivalence between the different common JNI types and `Rxmxnx.JNetInterface`.

| Java Type                | JNI Type      | Managed Type                  | Unmanaged Type        |
|--------------------------|---------------|-------------------------------|-----------------------|
| boolean                  | jboolean      | N/A                           | JBoolean              |
| byte                     | jbyte         | N/A                           | JByte                 |
| char                     | jchar         | N/A                           | JChar                 |
| double                   | jdouble       | N/A                           | JDouble               |
| float                    | jfloat        | N/A                           | JFloat                |
| int                      | jint          | N/A                           | JInt                  |
| long                     | jlong         | N/A                           | JLong                 |
| short                    | jshort        | N/A                           | JShort                |
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

### Data Types

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

                    ┌──  IViewObject  ──┬──  ILocalViewObject 
       IObject  ────┼──  ILocalObject  ─┼──  IInterfaceObject<>  
                    │                   └──  IArrayObject<> 
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

### Type Metadata

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
