# Java Data Types Mapping

As shown in the overview table, `Rxmxnx.JNetInterface` provides a mapping of Java data types. This is achieved through
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

- [Specialized Types](#specialized-types)
- [Object Hierarchy](#object-hierarchy)
- [Object Casting](#object-casting)
- [Data Type Registration](#data-type-registration)
    - [Main Types](#main-types)

## Specialized Types

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

## Object Hierarchy

All types interoperable with JNI implement the `IObject` interface, and all types either extend or can be converted to
the `JObject` class.

![InterfaceHierarchy](https://github.com/user-attachments/assets/b7bc1605-ad6b-48fb-abf2-8e937a433809)

All instances of objects interoperable with JNI are instances of the `JObject` class. In the case of primitives, the
conversion to `JObject` occurs through boxing.

![JavaObjectHierarchy](https://github.com/user-attachments/assets/5957fc7d-2f3c-41cf-9cdf-be85939419c5)

## Object Casting

In `Rxmxnx.JNetInterface`, primitive type conversion works the same as in Java, with promotion or truncation. To convert
a primitive to its wrapper object, use the `.ToObject(IEnvironment)` extension method.

To convert an `ILocalObject` instance to another type, you can use the `.CastTo<TReference>()` method. For
`JLocalObject` instances, use `.CastTo<TReference>(Boolean)`, where the boolean parameter indicates whether
`Rxmxnx.JNetInterface` should discard the original instance if the cast is to a type that is not a view.

Since `JGlobal` and `JWeak` instances cannot be directly manipulated in most cases, the `AsLocal(IEnvironment, Boolean)`
method allows creating a functional local object within the given environment. The boolean parameter specifies whether a
local reference should be created when performing the cast.

## Data Type Registration

One of the best features of `Rxmxnx.JNetInterface` is that, through data type mapping, it allows creating an object
instance using the closest mapped and registered CLR subclass to the actual class of the instance.

To register a data type, use the static method `JVirtualMachine.Register<T>()`.

**Notes:**

- This registration occurs at runtime and is performed statically to avoid reflection and maximize compatibility with
  NativeAOT.
- When registering a non-array type, its corresponding array type will also be registered.
- No registration is required for non-jagged array types.
- When registering an array type, its element type will also be registered. If it is a jagged array, the data types will
  be registered until reaching the fundamental element type.
- Primitive data types cannot be registered.
- Registering jagged array types is only necessary if runtime reflection cannot be used or if the feature switch
  `JNetInterface.DisableJaggedArrayAutoGeneration` is enabled. This applies to any jagged array type, including jagged
  arrays of primitive arrays.
- It is recommended to register data types before initializing `IVirtualMachine` instances and preferably within
  [module initializers](https://github.com/dotnet/runtime/blob/main/docs/design/specs/Ecma-335-Augments.md#module-initializer).

### Main Types

The main types are registered mapped types that will be initialized with global references when an `IVirtualMachine`
instance is created, allowing any environment within that JVM to use them without restrictions. These global references
will remain active until the JVM is terminated or removed from `Rxmxnx.JNetInterface`.

The functionality of this feature is detailed in the support documentation for
[native-Image](../native-image/README.md).