# Java String Handling

JNI allows special handling of `java.lang.String` instances (Java Strings). `Rxmxnx.JNetInterface` exposes these APIs
through the `JStringObject` class.

- [String Creation](#string-creation)
- [Native Characters](#native-characters)
    - [Native Memory](#native-memory)

## String Creation

To create a new array through JNI, `Rxmxnx.JNetInterface` exposes the following static methods in the class
`JStringObject`:

- `Create(IEnvironment, String?)`: Creates a Java `String` from a .NET `String` instance. The .NET `String` is kept in
  memory associated with the created instance. If the given `String` is null, it will return null. The value of the
  `String` can be accessed through the `Value` property.
- `Create(IEnvironment, ReadOnlySpan<Char>)`: Creates a Java `String` from a read-only span of UTF-16 characters. The
  `Value` property will generate a JNI call to retrieve the instance's value.
- `Create(IEnvironment, CString?)`: Creates a Java `String` from a `CString` instance. If the given `CString` is null,
  it will return null. The `Utf8Length` property will be initialized with the length of the `CString`.
- `Create(JClassObject, ReadOnlySpan<Byte>)`: Creates a Java `String` from a read-only span of UTF-8 bytes. The
  `Utf8Length` property will be initialized with the length of the read-only span.

*Note:* UTF-8 based creation methods comply with the following characteristics:

- The UTF-8 text must end with a null UTF-8 character.
- The termination character must not be part of the sequence. This is ensured in certain `CString` instances or .NET
  UTF-8/ASCII literals.
- The UTF-8 text must not contain null UTF-8 characters.

The properties exposed by this class are:

- `Length`: Number of UTF-16 characters in the Java string. If not initialized, a call to the JNI method
  `GetStringLength` will be made.
- `Utf8Length`: Number of UTF-8 units in the Java string. If not initialized, a call to the JNI method
  `GetStringUTFLength` will be made.
- `Value`: String value of the current Java string. If not initialized, a call to the JNI method `GetStringRegion`
  will be made.
- `Reference`: JNI reference to the instance.

**Notes:**

- The `JStringObject` class implements the following interfaces: `IEnumerable<Char>`, `IComparable`,
  `IComparable<String?>`, and `IComparable<JStringObject?>`.
- The characters and UTF-8 units of Java strings are represented by the CLR structures `System.Char` and `System.Byte`,
  respectively.

## Native Characters

JNI allows native access to the characters of a Java string. `Rxmxnx.JNetInterface` provides this functionality through
the following methods:

- `GetNativeChars(JMemoryReferenceKind)`: Equivalent to JNI's `GetStringChars` calls. The `JMemoryReferenceKind`
  parameter allows `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference to back native memory
  allocation.
- `GetCriticalChars(JMemoryReferenceKind)`: Equivalent to the `GetStringCritical` call. The `JMemoryReferenceKind`
  parameter enables `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference to pin memory.
- `GetNativeUtf8Chars(JMemoryReferenceKind)`: Equivalent to JNI's `GetStringUTFChars` calls. The
  `JMemoryReferenceKind` parameter allows `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference to
  back native memory allocation.
- `Get(Span<Char>, Int32)`: Equivalent to JNI's `GetStringRegion` calls. The integer serves as the offset for the
  copy.
- `GetUtf8(Span<Byte>, Int32)`: Equivalent to JNI's `GetStringUTFRegion` calls. The integer serves as the offset for
  the copy.
- `GetChars(Int32)`: Equivalent to JNI's `GetStringRegion` calls. The given integer serves as the starting point of
  the substring.
- `GetChars(Int32, Int32)`: Equivalent to JNI's `GetStringRegion` calls. The first given integer serves as the
  starting point of the substring, and the second defines its length.
- `GetUtf8(Int32)`: Equivalent to JNI's `GetStringUTFRegion` calls. The given integer serves as the starting point of
  the UTF-8 substring.
- `GetUtf8(Int32, Int32)`: Equivalent to JNI's `GetStringUTFRegion` calls. The first given integer serves as the
  starting point of the UTF-8 substring, and the second defines its length.

**Notes:**

- The `GetNativeChars`, `GetNativeUtf8Chars` and `GetCriticalChars` methods return a `JNativeMemory<T>` instance, which
  represents native/pinned read-only memory accessible via JNI. This memory must be released using the `Dispose()`
  method.
- The `JMemoryReferenceKind` value specifies whether to create an additional JNI reference to manage the native memory.
  This is intended to allow free manipulation of that memory across different threads.
- When the Value property is initialized the `GetChars` methods use it to create the substrings.

### Native Memory

Native memory in `Rxmxnx.JNetInterface` is represented as a fixed memory context through a memory adapter
encapsulated in an instance of the `JNativeMemomory<T>` class. This memory is read-only.

**Properties:**

- `ReleaseMode`: Sets the release mode of the native memory. If the memory is critical (directly mapped to the string
  in the JVM), this property is `null` and cannot be set.
- `Values`: Provides access to native memory via a rad-only span.
- `Copy`: Indicates whether the native memory is a copy of the string data.
- `Critical`: Indicates whether the native memory is directly the string memory pinned by the JVM.
- `Pointer`: Pointer to the native memory.

**Note:** If the native memory was allocated with an exclusive JNI reference, releasing the memory will also release
the associated JNI reference.

# Java Array Handling

JNI allows the manipulation and creation of Java arrays from any type. `Rxmxnx.JNetInterface` exposes APIs through
the `JArrayObject<>` class to achieve this goal.

- [Array Creation](#array-creation)
- [Non-Generic Class](#non-generic-class)
- [Generic Class](#generic-class)
- [Primitive Arrays](#primitive-arrays)
    - [Primitive Memory](#primitive-memory)

## Array Creation

To create a new array through JNI, `Rxmxnx.JNetInterface` exposes the following static methods in the generic class
`JArrayObject<>`:

- `Create(IEnvironment, Int32)`: Creates an array of the type specified by the generic class with the length given by
  the integer value.
- `Create(IEnvironment, Int32, T)`: Creates an array of the type specified by the generic class with the length given
  by the integer value and fills it with the specified value instance.
- `Create(IEnvironment, Int32, JClassObject)`: Creates an array of the type specified by the generic class with the
  length given by the integer value, using the provided class as the element type.
- `Create(JClassObject, Int32, T)`: Creates an array of the type specified by the generic class with the length given
  by the integer value and fills it with the specified value instance, using the provided class as the element type.

**Notes:**

- If an array is created by specifying the element class, it must be compatible with the element type. In the
  case of primitive arrays, only the corresponding primitive array class will be considered compatible.
- Methods that initialize arrays with a specified value are only recommended for creating non-primitive arrays.

## Non-Generic Class

In Java, arrays are views. For example, an instance of `java.lang.String[][]` can be treated as an instance of
`java.lang.Object[]`, `java.lang.Object[][]`, or even `java.io.Serializable[][]`. However, despite the inherent
polymorphism of views, arrays are instances of specific types. This instance is represented in `Rxmxnx.JNetInterface`
as a non-generic `JArrayObject`.

The properties exposed by this class are:

- `Length`: Number of elements in the Java array.
- `Reference`: JNI reference to the instance.

## Generic Class

In `Rxmxnx.JNetInterface`, the generic class `JArrayObject<T>` encapsulates a non-generic `JArrayObject` instance
to enable element retrieval and assignment operations. The generic type of this class represents the element type
contained or "viewed" in the array instance.

The properties exposed by this class are:

- `Length`: Number of elements in the Java array.
- `Reference`: JNI reference to the instance.

**Note:** These properties are read directly from the underlying non-generic instance that supports the generic view.

To get or set an array element, the class exposes an indexer. Furthermore, this class implements various .NET
interfaces such as `IList<T>` and `IReadOnlyList<T>`.

## Primitive Arrays

Unlike non-primitive arrays, primitive-type arrays in Java are not polymorphic. Through JNI, it is possible to
directly access the memory segment occupied by these arrays.

For this reason, when the generic array type is detected as primitive, the following extensions become available:

- `GetElements(JMemoryReferenceKind)`: Equivalent to JNI's `Get<PrimitiveType>ArrayElements` calls. The
  `JMemoryReferenceKind` parameter allows `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference
  to back native memory allocation.
- `GetPrimitiveArrayCritical(JMemoryReferenceKind)`: Equivalent to the `GetPrimitiveArrayCritical` call. The
  `JMemoryReferenceKind` parameter enables `Rxmxnx.JNetInterface` to safely and efficiently use another JNI reference
  to pin memory.
- `ToArray(Int32)`: Creates a .NET array of the primitive type, using `Get<PrimitiveType>ArrayRegion`. The integer
  parameter acts as an offset for the copy.
- `ToArray(Int32, Int32)`: Creates a .NET array of the primitive type, using `Get<PrimitiveType>ArrayRegion`. The
  first integer serves as an offset for the copy, while the second specifies how many elements should be copied.
- `Get(Span<T>, Int32)`: Uses `Get<PrimitiveType>ArrayRegion` to copy array elements into the provided span. The
  integer parameter serves as an offset, and the span's length determines the number of elements to copy.
- `Set(ReadOnlySpan<T>, Int32)`: Uses `Set<PrimitiveType>ArrayRegion` to copy elements from the span to the array.
  The integer parameter serves as an offset, and the span's length determines the number of elements to copy.

**Note:**

- The `GetElements` and `GetPrimitiveArrayCritical` methods return a `JPrimitiveMemory<T>` instance, which
  represents native/pinned memory accessible via JNI. This memory must be released using the `Dispose()` method.
- The `JMemoryReferenceKind` value specifies whether to create an additional JNI reference to manage the native memory.
  This is intended to allow free manipulation of that memory across different threads.

### Primitive Memory

Primitive array memory in `Rxmxnx.JNetInterface` is represented as a fixed memory context through a memory adapter
encapsulated in an instance of the `JPrimitiveMemory<T>` class. This memory allows both reading and writing.

**Properties:**

- `ReleaseMode`: Sets the release mode of the native memory. If the memory is critical (directly mapped to the array
  in the JVM), this property is `null` and cannot be set.
- `Values`: Provides access to native memory via a span.
- `Copy`: Indicates whether the native memory is a copy of the array data.
- `Critical`: Indicates whether the native memory is directly the array memory pinned by the JVM.
- `Pointer`: Pointer to the native memory.

**Note:** If the native memory was allocated with an exclusive JNI reference, releasing the memory will also release
the associated JNI reference.