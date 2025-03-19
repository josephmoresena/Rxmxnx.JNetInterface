# Java Data Types Mapping

`Rxmxnx.JNetInterface` provides a structured mapping of Java data types through the `IDataType` interface, with
instances of `JDataTypeMetadata` used for identification.  
The following table illustrates how data type mapping works.

| Java Declaration                | Base Type                                   | Interface                               | Metadata                                                                                    | Inheritance                                             |
|---------------------------------|---------------------------------------------|-----------------------------------------|---------------------------------------------------------------------------------------------|---------------------------------------------------------|
| `class JavaClassName`           | `JLocalObject`<sup>1</sup>                  | `IClassType<NetClassName>`              | `JClassTypeMetadata<NetClassName> { ClassName = package/JavaClassName }`                    | `: NetClassName`<sup>2</sup>                            |
| `interface JavaInterfaceName`   | `JInterfaceObject<NetInterfaceClassName>`   | `IInterfaceType<NetInterfaceClassName>` | `JInterfaceTypeMetadata<NetInterfaceClassName> { ClassName = package/JavaInterfaceName }`   | `: IInterfaceObject<NetInterfaceClassName>`<sup>3</sup> |
| `enum JavaEnumName`             | `JEnumObject<NetEnumClassName>`             | `IEnumType<NetEnumClassName>`           | `JEnumTypeMetadata<NetEnumClassName> { ClassName = package/JavaEnumName }`                  | N/A                                                     |
| `@interface JavaAnnotationName` | `JAnnotationObject<NetAnnotationClassName>` | `IInterfaceType<NetInterfaceClassName>` | `JInterfaceTypeMetadata<NetAnnotationClassName> { ClassName = package/JavaAnnotationName }` | N/A<sup>4</sup>                                         |

1. The base type depends on the hierarchy of the mapped class.
2. To extend a class, the superclass must be defined when constructing the `TypeMetadataBuilder` instance.
3. To extend or implement interfaces, the corresponding interface type must be specified in the metadata. Implementing
   `IInterfaceObject<..>` enables covariance.
4. While Java annotations (`@interface`) are not meant to be inherited, they are treated as interfaces in the mapping,
   allowing inheritance.

## Specialized Types

| Specialization | Java Declaration                                        | Base Type          | Interface                                           | Metadata                                               | Inheritance                           |
|----------------|---------------------------------------------------------|--------------------|-----------------------------------------------------|--------------------------------------------------------|---------------------------------------|
| **Throwable**  | `class JavaThrowableName extends Throwable`<sup>2</sup> | `JThrowableObject` | `IThrowableType<NetThrowableClassName>`<sup>2</sup> | `JThrowableTypeMetadata<NetThrowableClassName>`        | `: NetThrowableClassName`<sup>2</sup> |
| **Primitive**  | N/A                                                     | N/A                | `IPrimitiveType<..>`                                | `JPrimitiveType<..> { ClassName = JavaPrimitiveName }` | N/A<sup>1</sup>                       |
| **Array**      | N/A                                                     | N/A                | `IArrayType<JArrayObject<..>>`                      | `JArrayTypeMetadata`                                   | N/A<sup>2</sup>                       |

1. Primitive types cannot be inherited, just like in Java. In CLR, they are value-type structures, and additional
   primitive types cannot be created.
2. Arrays, like in Java, are views. While inheritance is not allowed, the `IArrayObject<..>` interface enables
   covariance for class-type arrays. For example, a `JArrayObject<JStringObject>` instance can be assigned to a variable
   of type `IArrayObject<JLocalObject>` or `IArrayObject<IInterfaceObject<JSerializableObject>>`.

## Object Hierarchy

All JNI-interoperable types implement the `IObject` interface and extend or can be converted to `JObject`.

![InterfaceHierarchy](https://github.com/user-attachments/assets/b7bc1605-ad6b-48fb-abf2-8e937a433809)

All JNI-interoperable objects are instances of `JObject`. Primitives undergo boxing when converted to `JObject`.

![JavaObjectHierarchy](https://github.com/user-attachments/assets/5957fc7d-2f3c-41cf-9cdf-be85939419c5)

## Object Casting

Primitive type conversion in `Rxmxnx.JNetInterface` follows Java's promotion and truncation rules. To convert a
primitive type to its wrapper object, use the `.ToObject(IEnvironment)` extension method.

For `ILocalObject` instances, conversion to another type can be done using `.CastTo<TReference>()`.  
For `JLocalObject` instances, use `.CastTo<TReference>(Boolean)`, where the boolean parameter determines whether the
original instance should be discarded when casting to a non-view type.

Since `JGlobal` and `JWeak` instances are not directly manipulable in most cases, the `AsLocal(IEnvironment, Boolean)`
method allows creating a functional local object within a given environment. The boolean parameter controls whether a
new local reference is created when performing the cast.

## Data Type Registration

One of `Rxmxnx.JNetInterface`'s key features is its ability to instantiate objects using the closest mapped and
registered CLR subclass for the actual instance class.

To register a data type, use the static method `JVirtualMachine.Register<T>()`.

##### Notes

- Registration occurs at runtime but is performed statically to avoid reflection and improve NativeAOT compatibility.
- When a non-array type is registered, its corresponding array type is also registered.
- Non-jagged array types do not require registration.
- Registering an array type also registers its element type. For jagged arrays, registration propagates to the
  fundamental element type.
- Primitive data types cannot be registered.
- Registering jagged array types is necessary if runtime reflection is unavailable or if
  `JNetInterface.DisableJaggedArrayAutoGeneration` is enabled.
- It is recommended to register data types before initializing `IVirtualMachine` instances, ideally
  within [module initializers](https://github.com/dotnet/runtime/blob/main/docs/design/specs/Ecma-335-Augments.md#module-initializer).

### Main Types

Main types are mapped and registered types that are initialized with global references when an `IVirtualMachine`
instance is created. This ensures that any environment within the JVM can access them without restrictions. These global
references remain active until the JVM is terminated or explicitly removed from `Rxmxnx.JNetInterface`.

For further details, see the documentation on [GraalVM Native Image](../native-image/README.md).  
