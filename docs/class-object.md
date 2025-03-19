# Java Class Handling in JNI

Handling `java.lang.Class<?>` instances is essential in JNI.

In `Rxmxnx.JNetInterface`, class definition availability takes priority over maintaining a persistent JNI reference. As
a result, `JClassObject` instances can exist symbolically without requiring an active JNI reference.

## Class Handling with `JClassObject`

Class management in `Rxmxnx.JNetInterface` is performed through the `JClassObject` class, which provides the following
static methods:

- **`GetClass<TDataType>(IEnvironment)`**: Returns the loaded instance for the mapped data type. Since it is a mapped
  type, JNI is not used to obtain a local reference.
- **`GetClass(IEnvironment, ReadOnlySpan<Byte>)`**: Returns the loaded instance for a class based on its JNI name,
  provided in a read-only span. If the class was previously identified, JNI is not used; otherwise, it calls the JNI
  `FindClass` function. Note that `FindClass` cannot locate primitive classes, so only non-primitive types can be
  retrieved using this method.
- **`GetVoidClass(IEnvironment)`**: Returns the loaded instance for the `void` type. Since `void` cannot be represented
  as an `IDataType` in .NET, this method provides access to it. If primitive classes were marked as main during JVM
  interoperability initialization, a global reference to the `void` class would have been loaded.
- **`LoadClass(IEnvironment, ReadOnlySpan<Byte>, ReadOnlySpan<Byte>, JClassLoaderObject?)`**: Uses the JNI `DefineClass`
  function to define a new class in the current JVM instance. The first span contains the class name, while the second
  span holds the bytecode. The returned class will have a valid JNI reference.
- **`LoadClass<TReferenceType>(IEnvironment, ReadOnlySpan<Byte>, JClassLoaderObject?)`**: Similar to the previous method
  but used for defining a mapped class within the JVM instance. The bytecode is stored in the provided span, and the
  resulting class has a valid JNI reference.

## Properties of `JClassObject`

Each `JClassObject` instance exposes the following properties:

- **`Name`**: The JNI name of the class.
- **`Reference`**: The JNI reference of the class. If no active JNI reference exists, it returns `null`.
- **`ClassSignature`**: The JNI signature representing the class type.
- **`Hash`**: The unique hash associated with the class, equivalent to its data type metadata hash.
- **`IsFinal`**: Indicates whether the class is non-extendable.
- **`IsArray`**: Specifies whether the class represents an array type.
- **`IsPrimitive`**: Specifies whether the class is a primitive type.
- **`IsInterface`**: Determines whether the class is an interface type.
- **`IsEnum`**: Identifies if the class represents an enum type.
- **`IsAnnotation`**: Identifies if the class is an annotation type.
- **`ArrayDimension`**: Indicates the array depth for this class. Examples:
    - `java.lang.Error`: 0
    - `java.lang.String[]`: 1
    - `byte[][][]`: 3

## JNI-Dependent Operations

These operations require JNI calls:

- **`GetClassName(out Boolean)`**: Retrieves the class name using Java’s `getName()` and checks if it is a primitive
  type via `isPrimitive()`.
- **`GetInterfaces()`**: Obtains the list of implemented interfaces using the Java `getInterfaces()` function.
- **`GetInformation()`**: Returns the registered type information in `Rxmxnx.JNetInterface`. If the class is a mapped
  type, the retrieved information should match the type metadata.

## Additional JNI-Based Operations

- **`GetSuperClass()`**: Retrieves the `JClassObject` instance representing the superclass. If the class is an `Array`,
  `Interface`, or `Enum`, JNI calls are not required, and symbolic instances may be returned. Otherwise, a valid JNI
  reference is guaranteed.
- **`IsAssignableTo(JClassObject)`**: Determines if instances of the current class type can be assigned to a variable of
  the given class type using the JNI `IsAssignableFrom` function.
- **`Register(..JNativeCallEntry..)`**: Registers Java native methods mapped to .NET local methods via JNI
  `RegisterNatives`.
- **`UnregisterNativeCalls()`**: Removes registered Java native methods mapped to .NET using JNI `UnregisterNatives`.
- **`GetModule()`**: Available only in JVM versions compatible with Java 9.0 or later.

##### Notes

- If a class is not yet loaded (i.e., there is no active JNI reference in the current context), a local reference is
  automatically loaded in the active frame.
- The `JNativeCallEntry` class, which facilitates native method registration, is explained in
  the [JNI Accessing Documentation](jni-accessing.md).

For detailed usage examples, refer to the [example application](.././src/ApplicationTest/README.md) in this repository.

For information on essential and main classes, see the documentation on compatibility
with [GraalVM Native Image](../native-image/README.md), which provides further implementation details.
