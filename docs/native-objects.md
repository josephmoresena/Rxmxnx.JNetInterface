# Java String Handling

JNI provides special handling for `java.lang.String` instances. `Rxmxnx.JNetInterface` exposes these functionalities
through the `JStringObject` class.

##### Topics

- [String Creation](#string-creation)
- [Native Characters](#native-characters)
    - [Native Memory](#native-memory)

---  

## String Creation

`Rxmxnx.JNetInterface` exposes the following static methods for creating Java `String` instances:

- **`Create(IEnvironment, String?)`**:
    - Creates a Java `String` from a .NET `String` instance.
    - If `null`, returns `null`.
    - The string value can be accessed via the `Value` property.

- **`Create(IEnvironment, ReadOnlySpan<Char>)`**:
    - Creates a Java `String` from a UTF-16 character span.
    - The `Value` property generates a JNI call to retrieve the string.

- **`Create(IEnvironment, CString?)`**:
    - Creates a Java `String` from a `CString` instance.
    - If `null`, returns `null`.
    - The `Utf8Length` property stores the string’s UTF-8 length.

- **`Create(JClassObject, ReadOnlySpan<Byte>)`**:
    - Creates a Java `String` from a UTF-8 byte span.
    - The `Utf8Length` property stores the byte span’s length.

### UTF-8 Creation Requirements

- The text must **end** with a **null UTF-8 character**.
- The null terminator **must not** be part of the sequence (ensured in `CString` instances and UTF-8 literals).
- The text **must not contain** null UTF-8 characters.

### Properties of `JStringObject`

- **`Length`**: UTF-16 character count. If not initialized, calls `GetStringLength`.
- **`Utf8Length`**: UTF-8 byte count. If not initialized, calls `GetStringUTFLength`.
- **`Value`**: The string content. If not initialized, calls `GetStringRegion`.
- **`Reference`**: JNI reference to the string instance.

##### Notes

- `JStringObject` implements:
    - `IEnumerable<Char>`
    - `IComparable`
    - `IComparable<String?>`
    - `IComparable<JStringObject?>`
- Java characters (`char`) and UTF-8 units (`byte`) are represented as `System.Char` and `System.Byte` in .NET.

---  

## Native Characters

JNI allows direct access to Java string characters. `Rxmxnx.JNetInterface` provides the following methods:

- **Native Character Access**
    - `GetNativeChars(JMemoryReferenceKind)`: Equivalent to `GetStringChars`.
    - `GetCriticalChars(JMemoryReferenceKind)`: Equivalent to `GetStringCritical`.
    - `GetNativeUtf8Chars(JMemoryReferenceKind)`: Equivalent to `GetStringUTFChars`.

- **Character Retrieval**
    - `Get(Span<Char>, Int32)`: Equivalent to `GetStringRegion`, using an offset.
    - `GetUtf8(Span<Byte>, Int32)`: Equivalent to `GetStringUTFRegion`, using an offset.
    - `GetChars(Int32)`: Equivalent to `GetStringRegion`, starting at the given index.
    - `GetChars(Int32, Int32)`: Retrieves a substring starting at index with the given length.
    - `GetUtf8(Int32)`: Retrieves a UTF-8 substring starting at index.
    - `GetUtf8(Int32, Int32)`: Retrieves a UTF-8 substring starting at index with the given length.

##### Notes

- `GetNativeChars`, `GetNativeUtf8Chars`, and `GetCriticalChars` return a `JNativeMemory<T>` instance.
- The `JMemoryReferenceKind` parameter determines whether to create an additional JNI reference for cross-thread access.
- If `Value` is initialized, `GetChars` methods use it instead of calling JNI functions.

---  

### Native Memory

Native memory in `Rxmxnx.JNetInterface` is represented using a **memory adapter** in `JNativeMemory<T>`. This memory is
**read-only**.

### Properties

- **`ReleaseMode`**: Defines how memory is released. If the memory is critical (directly mapped to JVM memory), this is
  `null`.
- **`Values`**: Provides access via a read-only span.
- **`Copy`**: Indicates whether the memory is a copy of the string data.
- **`Critical`**: Indicates if the memory is directly pinned by the JVM.
- **`Pointer`**: The pointer to the native memory.

##### Note

If native memory is allocated with an **exclusive JNI reference**, releasing the memory will also release the JNI
reference.

---  

# Java Array Handling

JNI allows direct manipulation of Java arrays. `Rxmxnx.JNetInterface` provides this functionality through the
`JArrayObject<>` class.

##### Topics

- [Array Creation](#array-creation)
- [Non-Generic Class](#non-generic-class)
- [Generic Class](#generic-class)
- [Primitive Arrays](#primitive-arrays)
    - [Primitive Memory](#primitive-memory)

---  

## Array Creation

The `JArrayObject<>` class exposes the following static methods:

- **`Create(IEnvironment, Int32)`**: Creates an array with the specified length.
- **`Create(IEnvironment, Int32, T)`**: Creates and initializes an array with a specified value.
- **`Create(IEnvironment, Int32, JClassObject)`**: Creates an array with a specified element class.
- **`Create(JClassObject, Int32, T)`**: Creates an array with a specified element class and initializes it.

##### Notes

- Specifying an **element class** requires compatibility with the array type.
- Initializing arrays with a default value is **only recommended** for **non-primitive** arrays.

---  

## Non-Generic Class

Java arrays are **view types** and can be treated as multiple array types. For example:

- `java.lang.String[][]` can be treated as:
    - `java.lang.Object[]`
    - `java.lang.Object[][]`
    - `java.io.Serializable[][]`

Despite this polymorphism, arrays **are instances of specific types**. In `Rxmxnx.JNetInterface`, **non-generic arrays**
are represented by `JArrayObject`.

### Properties

- **`Length`**: Number of elements in the array.
- **`Reference`**: JNI reference to the instance.

---  

## Generic Class

The `JArrayObject<T>` class enables **typed** element retrieval and assignment. It **wraps** a `JArrayObject` to expose
element access operations.

### Properties

- **`Length`**: Number of elements in the array.
- **`Reference`**: JNI reference to the instance.

##### Notes

- These properties **directly reference** the underlying non-generic instance.
- `JArrayObject<T>` implements `IList<T>` and `IReadOnlyList<T>`.

---  

## Primitive Arrays

Unlike non-primitive arrays, **primitive arrays are not polymorphic**. JNI allows direct access to **primitive array
memory segments**.

### Special Methods for Primitive Arrays

- **Native Memory Access**
    - `GetElements(JMemoryReferenceKind)`: Equivalent to `Get<PrimitiveType>ArrayElements`.
    - `GetPrimitiveArrayCritical(JMemoryReferenceKind)`: Equivalent to `GetPrimitiveArrayCritical`.

- **Copying Elements**
    - `ToArray(Int32)`: Copies elements into a .NET array.
    - `ToArray(Int32, Int32)`: Copies a specific range into a .NET array.
    - `Get(Span<T>, Int32)`: Copies elements into a provided span.
    - `Set(ReadOnlySpan<T>, Int32)`: Copies elements from a span into the Java array.

##### Notes

- `GetElements` and `GetPrimitiveArrayCritical` return `JPrimitiveMemory<T>`.
- The `JMemoryReferenceKind` parameter determines whether to create an additional JNI reference.

---  

### Primitive Memory

Primitive array memory is **mutable** and is managed using `JPrimitiveMemory<T>`.

#### Properties

- **`ReleaseMode`**: Defines how memory is released. If critical, this is `null`.
- **`Values`**: Provides access via a span.
- **`Copy`**: Indicates if the memory is a copy.
- **`Critical`**: Indicates if the memory is pinned by the JVM.
- **`Pointer`**: Pointer to the memory.

#### Note

If allocated with an **exclusive JNI reference**, releasing the memory will also release the reference.