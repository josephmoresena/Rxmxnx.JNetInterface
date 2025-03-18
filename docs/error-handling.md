# Java Error Handling

JNI allows handling errors and exceptions within native code. `Rxmxnx.JNetInterface` follows the same principle but in a
more .NET-friendly manner.

Just like in Java, the `java.Lang.Throwable` class hierarchy is:

![ThrowableHierarchy](https://github.com/user-attachments/assets/bd0d9f7e-1c1e-40da-b4c8-cdf56e70867e)

**Note:** `Rxmxnx.JNetInterface` provides several built-in `Throwable` types that may be thrown during basic operations.
However, this can introduce additional memory and performance costs. To optimize performance, the automatic registration
of these types can be disabled using the feature switch `JNetInterface.DisableBuiltInThrowableAutoRegistration`.

The additional throwable types are:

- `java.lang.LinkageError` (`JLinkageErrorObject`)
- `java.lang.ClassCircularityError` (`JClassCircularityErrorObject`)
- `java.lang.UnsatisfiedLinkError` (`JUnsatisfiedLinkErrorObject`)
- `java.lang.ClassFormatError` (`JClassFormatErrorObject`)
- `java.lang.ExceptionInInitializerError` (`JExceptionInInitializerErrorObject`)
- `java.lang.IncompatibleClassChangeError` (`JIncompatibleClassChangeErrorObject`)
- `java.lang.NoSuchFieldError` (`JNoSuchFieldErrorObject`)
- `java.lang.NoSuchMethodError` (`JNoSuchMethodErrorObject`)
- `java.lang.NoClassDefFoundError` (`JNoClassDefFoundErrorObject`)
- `java.lang.VirtualMachineError` (`JVirtualMachineErrorObject`)
- `java.lang.InternalError` (`JInternalErrorObject`)
- `java.lang.OutOfMemoryError` (`JOutOfMemoryErrorObject`)
- `java.lang.SecurityException` (`JSecurityExceptionObject`)
- `java.lang.InterruptedException` (`JInterruptedExceptionObject`)
- `java.text.ParseException` (`JParseExceptionObject`)
- `java.io.IOException` (`JIoExceptionObject`)
- `java.io.FileNotFoundException` (`JFileNotFoundExceptionObject`)
- `java.net.MalformedURLException` (`JMalformedUrlExceptionObject`)
- `java.lang.ReflectiveOperationException` (`JReflectiveOperationExceptionObject`)
- `java.lang.InstantiationException` (`JInstantiationExceptionObject`)
- `java.lang.ClassNotFoundException` (`JClassNotFoundExceptionObject`)
- `java.lang.IllegalAccessException` (`JIllegalAccessExceptionObject`)
- `java.lang.reflect.InvocationTargetException` (`JInvocationTargetExceptionObject`)
- `java.lang.ArrayStoreException` (`JArrayStoreExceptionObject`)
- `java.lang.NullPointerException` (`JNullPointerExceptionObject`)
- `java.lang.IllegalStateException` (`JIllegalStateExceptionObject`)
- `java.lang.ClassCastException` (`JClassCastExceptionObject`)
- `java.lang.ArithmeticException` (`JArithmeticExceptionObject`)
- `java.lang.IllegalArgumentException` (`JIllegalArgumentExceptionObject`)
- `java.lang.NumberFormatException` (`JNumberFormatExceptionObject`)
- `java.lang.IndexOutOfBoundsException` (`JIndexOutOfBoundsExceptionObject`)
- `java.lang.ArrayIndexOutOfBoundsException` (`JArrayIndexOutOfBoundsExceptionObject`)
- `java.lang.StringIndexOutOfBoundsException` (`JStringIndexOutOfBoundsExceptionObject`)

## JNI Error Handling

JNI can generate errors when executing a process. `Rxmxnx.JNetInterface` maps these errors to throw exceptions managed
by the CLR.

The hierarchy of managed exceptions is:

![ExceptionHierarchy](https://github.com/user-attachments/assets/67ed7df3-bc05-4768-ad1e-d95e3df8ed6a)

`JniException` exceptions occur when a JNI call returns a value other than Ok.
The identified values of JResult are: Ok, Error, DetachedThreadError, VersionError, MemoryError, ExitingError,
InvalidArgumentsError.

To determine the result that triggered the exception, the `Result` property can be consulted.

Although a JNI method can return any arbitrary negative JResult value, only the value `0 (Ok)` is considered a non-error
result. All other values are considered errors.

`ThrowableException` exceptions represent pending throwable instances thrown in the JVM.

These exceptions are instances of `IThrowableException`. If the throwable type thrown in the JVM is mapped and
registered (or one of its superclasses) in `Rxmxnx.JNetInterface`, the `ThrowableException` instance will be of the
`IThrowableException<>` type of the registered throwable.

Unlike pure `JniException` exceptions (resulting from a JNI error response), `ThrowableException` instances require
special handling. Similar to JNI, `Rxmxnx.JNetInterface` restricts API usage in the presence of a pending exception.

The `ThrowableException` class exposes the following properties:

- `EnvironmentRef`: Reference to the environment (JVM-attached thread) where the throwable instance was thrown.
- `ThreadId`: Identifier of the CLR-managed thread where the throwable instance was thrown.
- `ThrowableRef`: Global JNI reference to the thrown throwable instance. This reference will be released when the CLR
  garbage collector finds no strong references to the `ThrowableException` instance.

When a `ThrowableException` is pending,
only [certain specific JNI functions](https://developer.android.com/training/articles/perf-jni?hl=en#exceptions) can be
executed. Any process involving a different type of JNI call will result in an `InvalidOperationException` being thrown
in the CLR.

When the environment in which the exception occurs is in a critical state, `Rxmxnx.JNetInterface` will throw a
`CriticalException` in the CLR. Only when the current environment exits the critical state can the
`ThrowableException` be thrown.

To retrieve and set (throw) the pending exception in the JVM, the `PendingException` property of the `IEnvironment`
interface can be used.

- If the property returns `null`, it means the current thread has no pending exceptions in the JVM.
- Setting the property to `null` clears the current pending exception, equivalent to the JNI `ExceptionClear` call.
- If attempt to use the property to retrieve the pending exception or to rethrow an exception while in a critical
  state, a `CriticalException` will be thrown in the CLR.

To safely manipulate the `JThrowableObject` instance behind the exception, the `ThrowableException` class provides the
following APIs:

- `WithSafeInvoke(Action<JThrowableObject>)`: Creates a `JThrowableObject` instance and executes the delegate in a safe
  environment for any JNI call.
- `WithSafeInvoke<TResult>(Func<JThrowableObject, TResult>)`: Creates a `JThrowableObject` instance and executes the
  delegate in a safe environment for any JNI call, returning the execution result.

The `IThrowableException<>` interface exposes the same methods for the specific throwable type. For example, if the
exception is an instance of `IThrowableException<JErrorObject>`, the delegate parameters change to
`Action<JErrorObject>` and `Func<JErrorObject, TResult>`.

**Notes:**

- The `WithSafeInvoke` methods ensure a clean execution environment by creating a new thread attached to the JVM and
  executing the delegates on that thread.
- If exception information only needs to be sent to the error reporting channel, the `DescribeException()` method of the
  `IEnvironment` instance with the pending exception can be used.
- `ThrowableException` instances cannot be created directly, meaning the environment's `PendingException` property can
  only be used to rethrow exceptions already created in the system.
- To throw exceptions from a specific `JThrowableObject` instance, the `Throw(Boolean)` method can be used. This will
  create the exception, set it as pending in the environment, and throw it in the CLR if the boolean parameter specifies
  so.

The `JThrowableObject` class allows exceptions instance of any throwable type to be thrown by specifying the message via
JNI:

- `Throw(JClassObject, String, Boolean)`: Throws an exception instanceof the given class type with the specified
  message. It will throw in the CLR if the boolean parameter specifies so.
- `Throw(JClassObject, CString, Boolean)`: Throws an exception instance of the given class type with the specified
  message. It will throw in the CLR if the boolean parameter specifies so.
- `Throw<TThrowable>(IEnvironment, String, Boolean)`: Throws an exception instance of the generic throwable type with
  the specified message. It will throw in the CLR if the boolean parameter specifies so.
- `Throw<TThrowable>(IEnvironment, CString, Boolean)`: Throws an exception instanceof the generic throwable type with
  the specified message. It will throw in the CLR if the boolean parameter specifies so.

**Note:** The static methods `Throw` and `Throw<>` use the JNI `ThrowNew` call.