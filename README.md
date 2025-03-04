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

**Notes:**

* The hash of a data type (just like `JClassObject` instances) is the UTF-16 buffer used to store the UTF-8
  sequence containing the class name, the JNI signature of the class, and the name/signature of the array for the data
  type.
* Type metadata has a special implementation of the `.ToString()` method, which may be unnecessary and inconvenient in a
  release version of a product using `Rxmxnx.JNetInterface`. To disable this implementation, the feature switch
  `JNetInterface.DisableTypeMetadataToString` can be used.

#### Metadata Builder

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

#### Jagged Array Type Metadata

In Java, unlike .NET, there are no multidimensional arrays. Instead, Java uses arrays of arrays. Due to this definition,
`Rxmxnx.JNetInterface` uses reflection to create the metadata for this type of array at runtime to ensure compatibility
with NativeAOT.

However, in reflection-free AOT mode, the automatic creation of these metadata at runtime is not possible.
Therefore, a mechanism must be used to ensure the definition of `JArrayObject<..JArrayObject<...>..>`
is available at runtime.

Even if automatic metadata creation at runtime is not desired, this functionality can be disabled
using the feature switch `JNetInterface.DisableJaggedArrayAutoGeneration`.

### Argument Metadata

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

### Accessible Definitions

Accessible definitions are objects that allow access to Java methods and fields (both class and instance) via JNI.  
Every accessible definition is an instance of `JAccessibleObjectDefinition`.

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

#### Accessing Java Methods

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
