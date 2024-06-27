# GraalVM Native Image Support
In order to support the use of `JNetInterface` in binaries compiled with `native-image`, 
it is necessary to explicitly allow the use of certain types, fields, methods, and constructors through JNI.

The `jni-config.json` provided here could be used as base in any binary compiled using `native-image`. 

Please note that you must add the specific types, fields, methods, and constructors that your application or library requires access to through `JNetInterface`.

## java.lang.Void
This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class `void`.
## java.lang.Boolean
This type is accessed to find the static field `TYPE` which contains the reference to the primitive class `boolean`. Additionally, access to the method `booleanValue()` is required if there is a need to access its value, and also to the constructor `(boolean)` to create instances from .NET code.
## java.lang.Byte
This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class `byte`, and also to the constructor `(byte)` to create instances from .NET code.
## java.lang.Character
This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class `char`. In addition, access to the method `charValue()` is required if there is a need to access its value, and also to the constructor `(char)` to create instances from .NET code.
## java.lang.Double
This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class `double`, and also to the constructor `(double)` to create instances from .NET code.
## java.lang.Float
This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class `float`, and also to the constructor `(float)` to create instances from .NET code.
## java.lang.Integer
This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class `int`, and also to the constructor `(int)` to create instances from .NET code.
## java.lang.Long
This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class `long`, and also to the constructor `(long)` to create instances from .NET code.
## java.lang.Short
This type is accessed in order to find the static field `TYPE` which contains the reference to the primitive class `short`, and also to the constructor `(short)` to create instances from .NET code.
## java.lang.Number
This type is accessed in order to access the methods `byteValue()`, `doubleValue()`, `floatValue()`, `intValue()`, `longValue()`, and `shortValue()` to access the internal values of instances like `java.lang.Byte`, `java.lang.Double`, `java.lang.Float`, `java.lang.Integer`, `java.lang.Long`, `java.lang.Short`, or any type that extends `java.lang.Number`.
## Primitive Arrays
These types are accessed from JNI to create primitive arrays from .NET.
## java.lang.Class
This type is required for various uses within `JNetInterface` by accessing the methods `getName()`, `isPrimitive()`, `getModifiers()`, and `getInterfaces()`.
## java.lang.Throwable
This type is required for handling Java errors and exceptions within `JNetInterface` by accessing the methods `getMessage()` and `getStackTrace()`.
## java.lang.StackTraceElement
This type is required for handling Java errors and exceptions within `JNetInterface` by accessing the methods `getClassName()`, `getLineNumber()`, `getFileName()`, `getMethodName()`, and `isNativeMethod()`.
## java.lang.Enum
This type is required for handling enum type instances within `JNetInterface` by accessing the methods `name()` and `ordinal()`.
## java.nio.Buffer
This type is required for handling buffer type instances within `JNetInterface` by accessing the methods `isDirect()` and `capacity()`.
## java.lang.reflect.Member
This type is required for reflection support within `JNetInterface` by accessing the method `getDeclaringClass()`.
## java.lang.reflect.Executable
This type is required for reflection support within `JNetInterface` by accessing the method `getParameterTypes()`.
## java.lang.reflect.Method
This type is required for reflection support within `JNetInterface` by accessing the method `getReturnType()`.
## java.lang.reflect.Field
This type is required for reflection support within `JNetInterface` by accessing the method `getType()`.
