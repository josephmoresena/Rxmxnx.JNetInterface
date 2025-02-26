# GraalVM Native Image Support

In order to support the use of `JNetInterface` in binaries compiled with `native-image`,
it is necessary to explicitly allow the use of certain types, fields, methods, and constructors through JNI.

The `jni-config.json` provided here could be used as base in any binary compiled using `native-image`.

Please note that you must add the specific types, fields, methods, and constructors that your application or library
requires access to through `JNetInterface`.

**Note**: These requirements are not limited to consumers compiled with `native-image` but apply to any JVM
instantiation in `JNetInterface`, whether created through
`JVirtualMachineLibrary.CreateVirtualMachine(JVirtualMachineInitArg, out IEnvironment)` or injected via
`JVirtualMachine.GetVirtualMachine()`.

## Essential Classes

Essential classes are those special non-optional classes. These must be loaded for the JVM instantiated in
`JNetInterface`, and their reference is always global and non-disposable.

### java.lang.Class

This type is required for various uses within `JNetInterface` by accessing the methods `getName()`, `isPrimitive()`,
`getModifiers()`, and `getInterfaces()`.

### java.lang.Throwable

This type is required for handling Java errors and exceptions within `JNetInterface` by accessing the methods
`getMessage()` and `getStackTrace()`.

### java.lang.StackTraceElement

This type is required for handling Java errors and exceptions within `JNetInterface` by accessing the methods
`getClassName()`, `getLineNumber()`, `getFileName()`, `getMethodName()`, and `isNativeMethod()`.

## Primitive and Primitive Wrapper Classes

Primitive classes are considered main classes by design. This means they, like essential classes, must be loaded for the
JVM instantiated in `JNetInterface`, and their reference is always global. This is useful for locating object
definitions accessible through JNI.
Since references to primitive classes can only be found through the wrapper classes that encapsulate the primitive types
they represent, these wrapper classes share the same behavior. This is useful, for example, for caching constructors of
these object types.

### java.lang.Void

This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class
`void`.

### java.lang.Boolean

This type is accessed to find the static field `TYPE` which contains the reference to the primitive class `boolean`.
Additionally, access to the method `booleanValue()` is required if there is a need to access its value, and also to the
constructor `(boolean)` to create instances using `JNetInterface`.

### java.lang.Byte

This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class
`byte`, and also to the constructor `(byte)` to create instances using `JNetInterface`.

### java.lang.Character

This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class
`char`. In addition, access to the method `charValue()` is required if there is a need to access its value, and also to
the constructor `(char)` to create instances using `JNetInterface`.

### java.lang.Double

This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class
`double`, and also to the constructor `(double)` to create instances using `JNetInterface`.

### java.lang.Float

This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class
`float`, and also to the constructor `(float)` to create instances using `JNetInterface`.

### java.lang.Integer

This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class
`int`, and also to the constructor `(int)` to create instances using `JNetInterface`.

### java.lang.Long

This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class
`long`, and also to the constructor `(long)` to create instances using `JNetInterface`.

### java.lang.Short

This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class
`short`, and also to the constructor `(short)` to create instances using `JNetInterface`.

### java.lang.Number

This type is accessed in order to access the methods `byteValue()`, `doubleValue()`, `floatValue()`, `intValue()`,
`longValue()`, and `shortValue()` to access the internal values of instances like `java.lang.Byte`, `java.lang.Double`,
`java.lang.Float`, `java.lang.Integer`, `java.lang.Long`, `java.lang.Short`, or any type that extends
`java.lang.Number`.

However, these classes are not mandatory and can be disabled through feature switches if the application meets the
following conditions:

1. It does not use reflection to create objects of type `java.lang.Field`, `java.lang.Method`, or
   `java.lang.Constructor`.
2. It does not use objects of primitive wrapper types.

To disable the loading of primitive classes at JVM instantiation, the `JNetInterface.DisablePrimitiveMainClasses`
feature switch can be used.

To disable the creation of global, non-disposable references for wrapper classes when the JVM is instantiated (even if
the primitive class must be loaded at that time), the following feature switches are available:

- **`JNetInterface.DisableBooleanObjectMainClass`**: Sets `java.lang.Boolean` as a non-main class.
- **`JNetInterface.DisableByteObjectMainClass`**: Sets `java.lang.Byte` as a non-main class.
- **`JNetInterface.DisableCharacterObjectMainClass`**: Sets `java.lang.Character` as a non-main class.
- **`JNetInterface.DisableDoubleObjectMainClass`**: Sets `java.lang.Double` as a non-main class.
- **`JNetInterface.DisableFloatObjectMainClass`**: Sets `java.lang.Float` as a non-main class.
- **`JNetInterface.DisableIntegerObjectMainClass`**: Sets `java.lang.Integer` as a non-main class.
- **`JNetInterface.DisableLongObjectMainClass`**: Sets `java.lang.Long` as a non-main class.
- **`JNetInterface.DisableShortObjectMainClass`**: Sets `java.lang.Short` as a non-main class.

**Notes**:

* The `java.lang.Void` class is a non-main class by default. Thus, while it is loaded when the JVM is instantiated, a
  permanent global reference is not created unless the `JNetInterface.EnableVoidObjectMainClass` feature switch is used.
* The classes of primitive arrays are used for creating primitive arrays using `JNetInterface`.
* If any wrapper class that is a subclass of `java.lang.Number` is set as a main class, `java.lang.Number` is also set
  as a main class.

## Optional features classes

These classes support some of the features built into `JNetInterface` to simplify the handling of certain types. By
default, these classes are not considered main classes, but they can be manually set as such using the static method
`JVirtualMachine.SetMainClass<>()`.

### java.lang.Enum

This type is required for handling enum type instances within `JNetInterface` by accessing the methods `name()` and
`ordinal()`.

### java.nio.Buffer

This type is required for handling buffer type instances within `JNetInterface` by accessing the methods `isDirect()`
and `capacity()`.

### java.lang.reflect.Member

This type is required for reflection support within `JNetInterface` by accessing the method `getDeclaringClass()`.

### java.lang.reflect.Executable

This type is required for reflection support within `JNetInterface` by accessing the method `getParameterTypes()`.

### java.lang.reflect.Method

This type is required for reflection support within `JNetInterface` by accessing the method `getReturnType()`.

### java.lang.reflect.Field

This type is required for reflection support within `JNetInterface` by accessing the method `getType()`.

**Note**: Keep in mind that if the method `JVirtualMachine.SetMainClass<>()` is called before the JVM is instantiated,
it will behave like any other main class. However, if it is called afterward, its main class characteristics will only
activate the first time a reference to that class is loaded.