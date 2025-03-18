# Java Error Handling

JNI allows handling errors and exceptions within native code. `Rxmxnx.JNetInterface` follows this principle but in a
more .NET-friendly manner.

The `java.lang.Throwable` class hierarchy in Java is as follows:

![ThrowableHierarchy](https://github.com/user-attachments/assets/bd0d9f7e-1c1e-40da-b4c8-cdf56e70867e)

## Additional Throwable Types

The following Java throwable types are mapped in `Rxmxnx.JNetInterface`:

- **Errors**
    - `java.lang.LinkageError` → `JLinkageErrorObject`
    - `java.lang.ClassCircularityError` → `JClassCircularityErrorObject`
    - `java.lang.UnsatisfiedLinkError` → `JUnsatisfiedLinkErrorObject`
    - `java.lang.ClassFormatError` → `JClassFormatErrorObject`
    - `java.lang.ExceptionInInitializerError` → `JExceptionInInitializerErrorObject`
    - `java.lang.IncompatibleClassChangeError` → `JIncompatibleClassChangeErrorObject`
    - `java.lang.NoSuchFieldError` → `JNoSuchFieldErrorObject`
    - `java.lang.NoSuchMethodError` → `JNoSuchMethodErrorObject`
    - `java.lang.NoClassDefFoundError` → `JNoClassDefFoundErrorObject`
    - `java.lang.VirtualMachineError` → `JVirtualMachineErrorObject`
    - `java.lang.InternalError` → `JInternalErrorObject`
    - `java.lang.OutOfMemoryError` → `JOutOfMemoryErrorObject`

- **Exceptions**
    - `java.lang.SecurityException` → `JSecurityExceptionObject`
    - `java.lang.InterruptedException` → `JInterruptedExceptionObject`
    - `java.text.ParseException` → `JParseExceptionObject`
    - `java.io.IOException` → `JIoExceptionObject`
    - `java.io.FileNotFoundException` → `JFileNotFoundExceptionObject`
    - `java.net.MalformedURLException` → `JMalformedUrlExceptionObject`
    - `java.lang.ReflectiveOperationException` → `JReflectiveOperationExceptionObject`
    - `java.lang.InstantiationException` → `JInstantiationExceptionObject`
    - `java.lang.ClassNotFoundException` → `JClassNotFoundExceptionObject`
    - `java.lang.IllegalAccessException` → `JIllegalAccessExceptionObject`
    - `java.lang.reflect.InvocationTargetException` → `JInvocationTargetExceptionObject`
    - `java.lang.ArrayStoreException` → `JArrayStoreExceptionObject`
    - `java.lang.NullPointerException` → `JNullPointerExceptionObject`
    - `java.lang.IllegalStateException` → `JIllegalStateExceptionObject`
    - `java.lang.ClassCastException` → `JClassCastExceptionObject`
    - `java.lang.ArithmeticException` → `JArithmeticExceptionObject`
    - `java.lang.IllegalArgumentException` → `JIllegalArgumentExceptionObject`
    - `java.lang.NumberFormatException` → `JNumberFormatExceptionObject`
    - `java.lang.IndexOutOfBoundsException` → `JIndexOutOfBoundsExceptionObject`
    - `java.lang.ArrayIndexOutOfBoundsException` → `JArrayIndexOutOfBoundsExceptionObject`
    - `java.lang.StringIndexOutOfBoundsException` → `JStringIndexOutOfBoundsExceptionObject`

However, using them may introduce additional memory and performance costs. To optimize performance, automatic
registration of these types can be disabled with the feature switch
`JNetInterface.DisableBuiltInThrowableAutoRegistration`.

## JNI Error Handling

JNI can generate errors during execution, and `Rxmxnx.JNetInterface` maps these errors to managed CLR exceptions.

The managed exception hierarchy is as follows:

![ExceptionHierarchy](https://github.com/user-attachments/assets/67ed7df3-bc05-4768-ad1e-d95e3df8ed6a)

### `JniException`

A `JniException` occurs when a JNI call returns a value other than `Ok`.  
The possible `JResult` values include:

- `Ok`
- `Error`
- `DetachedThreadError`
- `VersionError`
- `MemoryError`
- `ExitingError`
- `InvalidArgumentsError`

Only `0 (Ok)` is considered a non-error result. Any other value is treated as an error. To determine the specific result
that triggered an exception, use the `Result` property.

### `ThrowableException`

A `ThrowableException` represents a pending throwable instance raised in the JVM. These exceptions implement
`IThrowableException`. If the thrown type is mapped and registered (or if one of its superclasses is registered), the
`ThrowableException` instance will be of covariant type `IThrowableException<>`.

Unlike `JniException`, `ThrowableException` requires special handling. Similar to JNI, `Rxmxnx.JNetInterface` restricts
API usage when an exception is pending.

#### Properties

- **`EnvironmentRef`**: Reference to the JVM-attached thread where the exception occurred.
- **`ThreadId`**: Identifier of the CLR-managed thread where the exception occurred.
- **`ThrowableRef`**: A global JNI reference to the thrown instance.
    - This reference is released when the CLR garbage collector finds no strong references to the `ThrowableException`
      instance.

### Handling Pending Exceptions

When a `ThrowableException` is pending, only a limited set of JNI functions can be executed. Any attempt to use
restricted JNI functions will result in an `InvalidOperationException` in the CLR.

If the environment is in a critical state, `Rxmxnx.JNetInterface` throws a `CriticalException` in the CLR. Once the
critical state ends, the `ThrowableException` can be properly handled.

To retrieve or rethrow a pending exception in the JVM, use the `PendingException` property of the `IEnvironment`
interface:

- If `PendingException` returns `null`, no exception is pending.
- Setting `PendingException` to `null` clears the current exception (equivalent to JNI `ExceptionClear`).
- Attempting to retrieve or rethrow an exception in a critical state will result in a `CriticalException`.

### Safe Exception Handling

To safely manipulate a `JThrowableObject` from an exception, use:

- **`WithSafeInvoke(Action<JThrowableObject>)`**
- **`WithSafeInvoke<TResult>(Func<JThrowableObject, TResult>)`**

These methods execute in a clean JNI environment, ensuring safe handling.

If the exception type is mapped, the interface `IThrowableException<TThrowable>` provides type-specific variants:

- **`WithSafeInvoke(Action<TThrowable>)`**
- **`WithSafeInvoke<TResult>(Func<TThrowable, TResult>)`**

#### Notes

- `WithSafeInvoke` ensures a clean execution environment by running in a new thread attached to the JVM. During the
  execution of these methods, a global-weak reference is created to reference the `java.lang.Throwable` instance that
  represents the exception.
- To log exception details without throwing, use `DescribeException()` on the `IEnvironment` instance.
- `ThrowableException` instances cannot be manually created. They can only be retrieved from `PendingException`.
- To throw a specific `JThrowableObject`, use `Throw(Boolean)`.

## Throwing Exceptions via JNI

The `JThrowableObject` class allows throwing Java exceptions with a custom message:

```csharp
Throw(JClassObject, String, Boolean)
Throw(JClassObject, CString, Boolean)
Throw<TThrowable>(IEnvironment, String, Boolean)
Throw<TThrowable>(IEnvironment, CString, Boolean)
```  

These methods use the JNI `ThrowNew` call to throw an exception in the JVM. If the boolean parameter is `true`, the
exception is also thrown in the CLR.  
