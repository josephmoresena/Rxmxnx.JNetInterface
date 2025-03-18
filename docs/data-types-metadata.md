# Type Metadata

Type metadata objects enable `Rxmxnx.JNetInterface` to identify Java object types referenced through JNI at runtime.  
By leveraging .NET's strong typing features, it is possible to perform operations on object instances or their
corresponding Java class instances.

![DataTypeMetadataHierarchy](https://github.com/user-attachments/assets/30c7d610-e874-40a3-957f-4ea4149626e2)

## Metadata Properties

Each metadata object exposes the following properties:

- **`ClassName`**: Represents the JNI name of the class identified by the metadata.  
  Example: The class `java.lang.String` (`JStringObject`) has the JNI name `java/lang/String`.
- **`Signature`**: Defines the JNI signature used to identify class instances when accessing Java fields or methods.  
  Examples:
    - `long` (`JLong`) → `J`
    - `java.lang.String` → `Ljava/lang/String;`
- **`ArraySignature`**: Used for automatic creation of generic array types.  
  Examples:
    - `char` → `char[]` (`JArrayObject<JChar>`) → `[C`
    - `java.lang.String` → `java.lang.String[]` (`JArrayObject<JStringObject>`) → `[Ljava/lang/String;`
- **`SizeOf`**: Determines the memory size required to store values returned by JNI.
- **`ArgumentMetadata`**: Combines the type signature with its memory size.
- **`Type`**: The CLR type corresponding to the Java class or structure identified by the metadata.
- **`Kind`**: Specifies the Java class type.  
  Examples:
    - `boolean` (`JBoolean`) → Primitive
    - `java.lang.String` → Class
    - `java.lang.Object[]` (`JArrayObject<JLocalObject>`) → Array
    - `java.io.Serializable` (`JSerializableObject`) → Interface
    - `java.lang.annotation.ElementType` (`JElementTypeObject`) → Enum
    - `java.lang.annotation.Target` (`JTargetObject`) → Annotation
- **`Modifier`**: Indicates the class modifier.  
  Examples:
    - `java.lang.String` → Final
    - `java.lang.Object` → Extensible
    - `java.lang.Number` (`JNumberObject`) → Abstract

### Notes

- The hash of a data type (similar to `JClassObject` instances) is derived from the UTF-16 buffer storing the UTF-8
  sequence containing the class name, JNI signature, and array signature.
- Type metadata includes a `.ToString()` implementation, which may be unnecessary in release builds. To disable it, use
  the feature switch `JNetInterface.DisableTypeMetadataToString`.

## Metadata Builders

Metadata builders are base classes used to initialize type metadata.  
For optimal runtime performance, a single metadata instance should be used.  
These builders are `ref struct` types, making them incompatible with Visual Basic.

- **`JLocalObject.TypeMetadataBuilder<>`**
    - Creates class-type metadata (`IClassType<>`).
    - Requires a JNI class name.
    - Allows specifying whether the class is `Abstract`, `Final`, or `Extensible` (default).
    - Use `Create(ReadOnlySpan<Byte>, JTypeModifier)` for initialization.
    - To define a superclass, use `Create<>(ReadOnlySpan<Byte>, JTypeModifier)` from the superclass builder.
    - Supports interface implementation via `.Implements<>()`, where the CLR type must implement `IInterfaceObject<>`.

- **`JLocalObject.InterfaceView.TypeMetadataBuilder<>`**
    - Creates interface or annotation-type metadata (`IInterfaceType<>`).
    - Requires a JNI class name.
    - Use `Create(ReadOnlySpan<Byte>)` for initialization.
    - Supports extending other interfaces via `.Extends<>()`, requiring CLR types to implement `IInterfaceObject<>`.

- **`JThrowableObject.TypeMetadataBuilder<>`**
    - Creates throwable-type metadata (`IThrowableType<>`).
    - Requires a JNI class name.
    - Identical to `JLocalObject.TypeMetadataBuilder<>`, except that specifying a throwable superclass is mandatory.

- **`JEnumObject.TypeMetadataBuilder<>`**
    - Creates enum-type metadata (`IEnumObject<>`).
    - Requires a JNI class name.
    - Identical to `JLocalObject.TypeMetadataBuilder<>`, but enums cannot have a superclass since they all extend
      `java.lang.Enum`.
    - Allows adding individual values to the enum type definition using `AppendValue(Int32, CString)`.
    - Allows adding a sequence of values to the enum type definition by specifying the offset for the ordinals using  
      `AppendValues(Int32, ReadOnlySpan<CString>)`.

### Note

All `TypeMetadataBuilder<>` instances perform runtime validation during construction.  
For release builds, validation can be disabled using the feature switch `JNetInterface.DisableMetadataValidation`, as it
is primarily intended for design-time verification.

## Jagged Array Type Metadata

Unlike .NET, Java does not support multidimensional arrays—only arrays of arrays. `Rxmxnx.JNetInterface` uses reflection
to generate metadata for these arrays at runtime, ensuring NativeAOT compatibility.

In reflection-free AOT mode, runtime metadata generation is not possible. To ensure
`JArrayObject<..JArrayObject<...>..>` definitions are available at runtime, manual registration is required.

To disable automatic metadata creation, use `JNetInterface.DisableJaggedArrayAutoGeneration`.

## Argument Metadata

Argument metadata defines how Java methods and fields are accessed via JNI. Each type metadata object exposes a property
to retrieve its argument metadata.

Alternatively, it can be obtained using `JArgumentMetadata.Get<T>()`, where `T` is a mapped type.

If a mapped type is unavailable, use `JArgumentMetadata.Create(ReadOnlySpan<Byte>)` with a binary span containing the
JNI type signature.  
Example: To create metadata for `java.util.Dictionary<K,V>`, use the signature `Ljava/util/Dictionary;`.

### Note

Creating metadata for primitive types is not supported. To obtain primitive argument metadata, use
`JArgumentMetadata.Get<TPrimitive>()`.  
