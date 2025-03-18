# Type Metadata

Type metadata objects allow `Rxmxnx.JNetInterface` to identify at runtime the types of Java objects referenced through
JNI.
By leveraging the strong typing features of the .NET platform, it is possible to perform operations on object instances
or their corresponding Java class instances.

![DataTypeMetadataHierarchy](https://github.com/user-attachments/assets/30c7d610-e874-40a3-957f-4ea4149626e2)

The metadata exposes the following properties:

- `ClassName`: This name is the JNI name of the class identified by the metadata. For example, the class
  `java.lang.String` (`JStringObject`) has the JNI name `java/lang/String`.
- `Signature`: This signature allows identifying instances of the class represented by the metadata to access Java
  fields or methods via JNI.
  For example:

- `long` (`JLong`) has the signature `J`.
- `java.lang.String` has the signature `Ljava/lang/String;`.

- `ArraySignature`: This signature enables the automatic creation of the generic array type for the class identified
  by the metadata.
  For example:
    - `char` -> `char[]` (`JArrayObject<JChar>`) with the signature `[C`.
    - `java.lang.String` -> `java.lang.String[]` (`JArrayObject<JStringObject>`) with the signature
      `[Ljava/lang/String;`.
- `SizeOf`: This property allows JNI calls to determine the memory size required to store the value returned by JNI.
- `ArgumentMetadata`: Combines the signature of the current type with its memory size.
- `Type`: CLR type of the .NET class or structure representing the class identified by the metadata.
- `Kind`: Identifies the type of the Java class represented by the metadata. For example:
    - `boolean` (`JBoolean`): Primitive.
    - `java.lang.String`: Class.
    - `java.lang.Object[]` (`JArrayObject<JLocalObject>`): Array.
    - `java.io.Serializable` (`JSerializableObject`): Interface.
    - `java.lang.annotation.ElementType` (`JElementTypeObject`): Enum.
    - `java.lang.annotation.Target` (`JTargetObject`): Annotation.
- `Modifier`: Identifies the modifier of the Java class represented by the metadata.
    - `java.lang.String`: Final.
    - `java.lang.Object`: Extensible.
    - `java.lang.Number` (`JNumberObject`): Abstract.

**Notes:**

- The hash of a data type (just like `JClassObject` instances) is the UTF-16 buffer used to store the UTF-8
  sequence containing the class name, the JNI signature of the class, and the name/signature of the array for the data
  type.
- Type metadata has a special implementation of the `.ToString()` method, which may be unnecessary and inconvenient in a
  release version of a product using `Rxmxnx.JNetInterface`. To disable this implementation, the feature switch
  `JNetInterface.DisableTypeMetadataToString` can be used.

## Metadata Builder

Builders are classes found in the base classes used to initialize type metadata.
It is recommended to use a single metadata instance to improve runtime performance. <br/>
These types are `ref struct`, so they are not compatible with the Visual Basic language.

- `JLocalObject.TypeMetadataBuilder<>`: This builder allows creating class-type metadata (`IClassType<>`). It must
  always be initialized with the JNI class name. It is also possible to specify whether the class is `Abstract`
  or `Final`, or by default `Extensible` (which allows other classes to extend it). This can be done using the static
  method `Create(ReadOnlySpan<Byte>, JTypeModifier)`.
  To specify that the metadata class extends another class, the static method of the superclass builder
  `Create<>(ReadOnlySpan<Byte>, JTypeModifier)` must be used.
  The builder also allows specifying that the metadata class implements interfaces through the `.Implements<>()`
  method. Note that the CLR type must implement the `IInterfaceObject<>` interface.

- `JLocalObject.InterfaceView.TypeMetadataBuilder<>`: This builder allows creating interface or annotation-type
  metadata (`IInterfaceType<>`). It must always be initialized with the JNI class name. This can be done using the
  static method `Create(ReadOnlySpan<Byte>, JTypeModifier)`.
  The builder also allows specifying that the metadata interface extends other interfaces through the `.Extends<>()`
  method. Note that the CLR type must implement the `IInterfaceObject<>` interface.

- `JThrowableObject.TypeMetadataBuilder<>`:
  This builder allows creating throwable-type metadata (`IThrowableType<>`).
  It must always be initialized with the JNI class name.
  This builder is identical to `JLocalObject.TypeMetadataBuilder<>`, but the throwable superclass must always be
  specified.

- `JEnumObject.TypeMetadataBuilder<>`:
  This builder allows creating enum-type metadata (`IEnumObject<>`). It must always be initialized with the JNI class
  name.
  This builder is identical to `JLocalObject.TypeMetadataBuilder<>`, but no superclass can be specified, as all enum
  types extend the `java.lang.Enum` class.

**Note:** All `TypeMetadataBuilder<>` instances perform runtime validations during their construction. However, for a
release build, this validation can be disabled using the feature switch `JNetInterface.DisableMetadataValidation`,
as its primary purpose is design-time validation.

## Jagged Array Type Metadata

In Java, unlike .NET, there are no multidimensional arrays. Instead, Java uses arrays of arrays. Due to this definition,
`Rxmxnx.JNetInterface` uses reflection to create the metadata for this type of array at runtime to ensure compatibility
with NativeAOT.

However, in reflection-free AOT mode, the automatic creation of these metadata at runtime is not possible.
Therefore, a mechanism must be used to ensure the definition of `JArrayObject<..JArrayObject<...>..>`
is available at runtime.

Even if automatic metadata creation at runtime is not desired, this functionality can be disabled
using the feature switch `JNetInterface.DisableJaggedArrayAutoGeneration`.

## Argument Metadata

Argument metadata objects allow defining access to Java methods and fields from JNI.
As previously mentioned, type metadata exposes a property to obtain the argument metadata for a specified type.
However, it is also possible to retrieve it using the static method `JArgumentMetadata.Get<T>()`, where `T` is a mapped
type.

Furthermore, if a mapped type for the argument is not available, it can be created using the method
`JArgumentMetadata.Create(ReadOnlySpan<Byte>)`,
where the read-only binary span contains the JNI type signature.

For example, to create the signature for `java.util.Dictionary<K,V>`, its signature should be `Ljava/util/Dictionary;`.

**Note:** Creating metadata for primitive types is not supported; to obtain them, the method
`JArgumentMetadata.Get<TPrimitive>()` should be used.