# Java Class Handling

In JNI, handling `java.lang.Class<?>` instances is essential.

By design, `Rxmxnx.JNetInterface` prioritizes class definition availability over maintaining a valid JNI reference. As a
result, `JClassObject` instances can exist symbolically without an actual JNI reference backing them.

In `Rxmxnx.JNetInterface`, class handling is managed through the `JClassObject` class using the following static
methods:

- `GetClass<TDataType>(IEnvironment)`: Returns the loaded instance for the mapped data type. Since it is a mapped type,
  JNI is not used to obtain a local reference to the class.
- `GetClass(IEnvironment, ReadOnlySpan<Byte>)`: Returns the loaded instance for the class whose JNI name is given in the
  read-only span. If the class has been identified previously, JNI is not used to obtain a local reference. Otherwise,
  it uses the JNI `FindClass` function. Note that `FindClass` cannot locate primitive classes, so only non-primitive
  types can be retrieved using this method.
- `GetVoidClass(IEnvironment)`: Returns the loaded instance for the `void` type. Since `void` cannot be represented in
  .NET as an `IDataType`, this special method provides access to it.
  If primitive classes were set as main when initializing interoperability with the JVM,
  a global reference to it will have been loaded.
- `LoadClass(IEnvironment, ReadOnlySpan<Byte>, ReadOnlySpan<Byte>, JClassLoaderObject?)`: Uses the JNI `DefineClass`
  function to define a class within the current JVM instance. The class name is given in the first read-only span, and
  the bytecode is contained in the second span. The returned class will have a valid JNI reference.
- `LoadClass<TReferenceType>(IEnvironment, ReadOnlySpan<Byte>, JClassLoaderObject?)`: Uses the JNI `DefineClass`
  function to define a mapped class within the JVM instance, with its bytecode stored in the provided span. The returned
  class will have a valid JNI reference.

`JClassObject` instances expose the following properties:

- `Name`: The JNI name of the class.
- `Reference`: The JNI reference of the class. If no active JNI reference exists, it returns null.
- `ClassSignature`: The JNI signature of the type represented by the class.
- `Hash`: Equivalent to the hash mentioned for data type metadata.
- `IsFinal`: Indicates whether the class allows extensions.
- `IsArray`: Indicates whether the class represents an array type.
- `IsPrimitive`: Indicates whether the class is a primitive type.
- `IsInterface`: Indicates whether the class is an interface type.
- `IsEnum`: Indicates whether the class is an enum type.
- `IsAnnotation`: Indicates whether the class is an annotation type.
- `ArrayDimension`: Indicates the depth of the array type represented by this class. For example:
    - `java.lang.Error`: 0
    - `java.lang.String[]`: 1
    - `byte[][][]`: 3

The following operations perform JNI calls to retrieve results:

- `GetClassName(out Boolean)`: Invokes Java's `getName()` function and the `isPrimitive()` method.
- `GetInterfaces()`: Calls the Java `getInterfaces()` function.
- `GetInformation()`: Returns the registered type information in `Rxmxnx.JNetInterface` for the class. If it is a mapped
  type, this information should match the type metadata.

Additional operations executed directly in JNI:

- `GetSuperClass()`: Returns the `JClassObject` instance representing the superclass of the current class. If the class
  is identified as an `Array`, `Interface`, or `Enum`, JNI calls are not required, and symbolic instances may be
  returned. Otherwise, a valid JNI reference is always returned.
- `IsAssignableTo(JClassObject)`: Determines whether an object of the current class type can be assigned to a variable
  of the given class type using the JNI `IsAssignableFrom` function.
- `Register(..JNativeCallEntry..)`: Defines Java native methods mapped to .NET local methods using the JNI
  `RegisterNatives` function.
- `UnregisterNativeCalls()`: Removes the registration of Java native methods mapped to .NET using the JNI
  `UnregisterNatives` function.
- `GetModule()`: This method is only effective in JVM instances compatible with Java 9.0+.

**Notes:**

- When using a class, if it has not been loaded (i.e., there is no active JNI reference in the current
  context), a local reference will be loaded in the active frame.
- The functionality of the `JNativeCallEntry` class is detailed in the [accessing documentation](jni-accessing.md).

For more information on using these methods, refer to
the [included example application](.././src/ApplicationTest/README.md) in this repository.

For more information about essential and main classes, please refer to the documentation on compatibility with
[GraalVM Native Image](../native-image/README.md), where the implementation details are explained in greater depth.