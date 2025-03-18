# Java Member Handling

JNI allows the handling to Java class members. Accessible definitions are objects that allow access to Java methods and
fields (both class and instance) via JNI. Every accessible definition is an instance of `JAccessibleObjectDefinition`.

**Note:** Accessible definitions can be identified by their name and descriptor.
The hash of a definition is the UTF-16 buffer used to store the UTF-8 sequence containing these two elements.

- [Accessing Java Fields](#accessing-java-fields)
    - [Accessing Java Calls](#accessing-java-calls)
        - [Indeterminate Calls](#indeterminate-calls)
            - [Method Calls](#method-calls)
            - [Constructor Calls](#constructor-calls)
            - [Function Calls](#function-calls)
        - [Indeterminate Result](#indeterminate-result)
    - [Defining Native Java Calls](#defining-native-java-calls)

## Accessing Java Fields

JNI allows access to Java fields, both class (static) and instance fields.
This access can be read (`get`) or write (`set`).
`Rxmxnx.JNetInterface` provides the same functionality through `JFieldDefinition` instances.

![FieldDefinitionHierarchy](https://github.com/user-attachments/assets/691a60f9-091a-4752-8813-b2c84576bc0f)

**Notes:**

- The `JFieldDefinition<..>` type allows defining a Java field of a generic `IDataType<..>`, while
  `JNonTypedFieldDefinition` allows defining fields of object types whose class is not mapped in
  `Rxmxnx.JNetInterface`.
- Creating a `JNonTypedFieldDefinition` definition for a primitive field is not supported; instead,
  a `JFieldDefinition<..>` instance must be used.

The APIs exposed by `JNonTypedFieldDefinition` are identical to those of a `JFieldDefinition<JLocalObject>` instance.

To get or set a field using `Rxmxnx.JNetInterface`, the following options are available:

- `Get(JLocalObject)`: Retrieves a field from the given instance. The declaring class is the object's own class.
- `Get(JLocalObject, JClassObject)`: Retrieves a field from the given instance, using the provided class
  as the declaring class. This is useful for efficiently handling field declarations in the superclasses
  of multiple instances.
- `GetReflected(JFieldObject, JLocalObject)`: Retrieves the reflected field in the given instance.
- `StaticGet(JClassObject)`: Retrieves a static field from the given class.
- `StaticGetReflected(JFieldObject)`: Retrieves the reflected static field.
- `Set(JLocalObject, T)`: Sets a field in the given instance. The declaring class is the object's own class.
- `Set(JLocalObject, JClassObject, T)`: Sets a field in the given instance, using the provided class
  as the declaring class.
- `SetReflected(JFieldObject, JLocalObject, T)`: Sets the reflected field in the given instance.
- `StaticSet(JClassObject, T)`: Sets a static field in the given class.
- `StaticSetReflected(JFieldObject, T)`: Sets a static field in the reflected class.

**Note:** When using a class, if it has not been loaded (i.e., there is no active JNI reference in the current
context), a local reference will be loaded in the active frame.

## Accessing Java Calls

JNI allows access to both instance and class (static) methods in Java. When methods return a primitive value or an
object, they are referred to as functions.
`Rxmxnx.JNetInterface` provides the same functionality through `JCallDefinition` instances. Constructors are not
considered static functions in JNI but share a strong similarity with them when invoked.

![CallDefinitionHierarchy](https://github.com/user-attachments/assets/835e54d4-f2c3-4d8e-a9df-cb1aeed00d49)

**Notes:**

- The `JFunctionDefinition<..>` type allows defining a Java function with a generic `IDataType<..>`, while
  `JNonTypedFunctionDefinition` allows defining functions returning an object whose class is not mapped in
  `Rxmxnx.JNetInterface`.
- Creating a `JNonTypedFunctionDefinition` for a primitive function is not supported; a `JFunctionDefinition<..>`
  instance must be used instead.
- `Parameterless` types are definitions of functions, methods, or constructors that do not take parameters.
- The APIs exposed by `JNonTypedFunctionDefinition` are public and impose no restrictions on the arguments of the
  defined calls.
- Call definitions also expose the call parameter count and the call's byte size.

To invoke methods and functions using `Rxmxnx.JNetInterface`, the following options are available:

- `Invoke(JLocalObject, ReadOnlySpan<IObject?>)`: Invokes the call on the given instance using
  the objects in the read-only span as arguments. The declaring class is inferred from the object's class.
- `InvokeReflected(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`: Invokes the reflected method or
  function on the given instance using the objects in the read-only span as arguments.
- `Invoke(JLocalObject, JClassObject, ReadOnlySpan<IObject?>)`: Invokes the call on the given
  instance using the objects in the read-only span as arguments. The declaring class is explicitly provided.
  This is useful for efficiently managing method calls on superclass declarations across multiple instances.
- `InvokeNonVirtual(JLocalObject, JClassObject, ReadOnlySpan<IObject?>)`: Invokes the call on the
  given instance using the objects in the read-only span as arguments, but it forces the implementation from
  the explicitly provided class.
- `InvokeNonVirtualReflected(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`: Invokes the reflected
  call on the given instance using the objects in the read-only span as arguments, but it forces
  the implementation from the class where the method was originally reflected.
- `StaticInvoke(JClassObject, ReadOnlySpan<IObject?>)`: Invokes a static call in the given class
  using the objects in the read-only span as arguments.
- `InvokeStaticReflected(JMethodObject, ReadOnlySpan<IObject?>)`: Invokes a reflected static call
  in the declaring class of the reflected method using the objects in the read-only span as arguments.

To create instances using constructors in `Rxmxnx.JNetInterface`, the following options are available:

- `New(JClassObject, ReadOnlySpan<IObject?>)`: Creates a new instance of the given class using the constructor,
  passing the objects in the read-only span as arguments.
- `New<>(IEnvironment, ReadOnlySpan<IObject?>)`: Creates a new instance of the generic type using the constructor,
  passing the objects in the read-only span as arguments.
- `NewReflected(JConstructorObject, ReadOnlySpan<IObject?>)`: Creates a new instance using the reflected
  constructor,
  passing the objects in the read-only span as arguments.
- `NewReflected<>(IEnvironment, ReadOnlySpan<IObject?>)`: Creates a new instance of the generic type using the
  reflected constructor, passing the objects in the read-only span as arguments. The generic type must be a superclass
  of the class where the reflected constructor is defined.

**Notes:**

- These APIs in the `JMethodDefinition`, `JFunctionDefinition<..>`, and `JConstructorDefinition` classes are exposed
  as protected.
- These APIs in `Parameterless` classes are public and do not receive arguments.
- In .NET 9.0+, the `ReadOnlySpan<IObject?>` parameter uses the `params` keyword.
- When using classes, either as the declaring class or as a function argument, if the class has not been loaded,
  meaning there is no active JNI reference in the current context, a local reference will be loaded in the active frame.

### Indeterminate Calls

Indeterminate calls encapsulate Java call definitions (`JCallDefinition`), allowing methods, functions, and constructors
to be defined and executed in a flexible and dynamic manner.

The following static methods can be used to create indeterminate call definitions:

- `CreateConstructorDefinition(ReadOnlySpan<JArgumentMetadata>)`: Creates an `IndeterminateCall` instance that
  encapsulates the definition of a constructor whose parameters are defined by the metadata in the read-only span.
- `CreateMethodDefinition(ReadOnlySpan<JArgumentMetadata>)`: Creates an `IndeterminateCall` instance that encapsulates
  the definition of a method whose parameters are defined by the metadata in the read-only span.
- `CreateMethodDefinition(ReadOnlySpan<Byte>, ReadOnlySpan<JArgumentMetadata>)`: Creates an `IndeterminateCall` instance
  that encapsulates the definition of a method whose name is represented by the first read-only span, and whose
  parameters are defined by the metadata in the second read-only span. If the method name is `<init>`, it encapsulates a
  constructor.

#### Method Calls

The `IndeterminateCall` class provides the following options for method calls:

- `MethodCall(JLocalObject, ReadOnlySpan<IObject?>)`: Calls a method on the current object using its class as the
  declaring class. If the call corresponds to a function, it invokes it and discards the result.
- `MethodCall(JLocalObject, JClassObject, Boolean, ReadOnlySpan<IObject?>)`: Calls a method on the current object using
  the specified class as the declaring class. If the boolean parameter is `true`, it performs a non-virtual call. If the
  call corresponds to a function, it invokes it and discards the result.
- `StaticMethodCall(JClassObject, ReadOnlySpan<IObject?>)`: Calls a static method on the specified class. If the call
  corresponds to a function, it invokes it and discards the result. If the call corresponds to a constructor, it calls
  the constructor and discards the created instance.
- `ReflectedMethodCall(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`: Calls a reflected method on the current
  object. If the call corresponds to a function, it invokes it and discards the result.
- `ReflectedMethodCall(JMethodObject, JLocalObject, Boolean, ReadOnlySpan<IObject?>)`: Calls a reflected method on the
  current object. If the boolean parameter is `true`, it performs a non-virtual call. If the call corresponds to a
  function, it invokes it and discards the result.
- `ReflectedStaticMethodCall(JExecutableObject, ReadOnlySpan<IObject?>)`: Calls a reflected static method. If the call
  corresponds to a function, it invokes it and discards the result. If the call corresponds to a constructor, it calls
  the constructor and discards the created instance.

#### Constructor Calls

The `IndeterminateCall` class provides the following options for constructor calls:

- `NewCall(JClassObject, ReadOnlySpan<IObject?>)`: Calls the constructor in the specified class and returns the created
  instance.
- `NewCall<TObject>(IEnvironment, ReadOnlySpan<IObject?>)`: Calls the constructor in the class of the specified type and
  returns the created instance.
- `ReflectedNewCall(JConstructorObject, ReadOnlySpan<IObject?>)`: Calls the reflected constructor and returns the
  created instance.
- `ReflectedNewCall<TObject>(JConstructorObject, ReadOnlySpan<IObject?>)`: Calls the reflected constructor and returns
  the created instance. The generic type must be a superclass of the constructor’s class.

*Note:* If the call does not correspond to a constructor, an exception will be thrown.

#### Function Calls

The `IndeterminateCall` class provides the following options for function calls, each returning an instance of
`IndeterminateResult`:

- `FunctionCall(JLocalObject, ReadOnlySpan<IObject?>)`: Calls a function on the current object using its class as the
  declaring class. If the call corresponds to a method, it invokes the method and returns an empty result.
- `FunctionCall(JLocalObject, JClassObject, Boolean, ReadOnlySpan<IObject?>)`: Calls a function on the current object
  using the given class as the declaring class. If the boolean parameter is `true`, it performs a non-virtual call. If
  the call corresponds to a method, it invokes the method and returns an empty result.
- `StaticFunctionCall(JClassObject, ReadOnlySpan<IObject?>)`: Calls a static function on the given class. If the call
  corresponds to a method, it invokes the method and returns an empty result. If the call corresponds to a constructor,
  it calls the constructor and returns the created instance in the result.
- `ReflectedFunctionCall(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`: Performs a reflected function call on
  the current object. If the call corresponds to a method, it invokes the method and returns an empty result.
- `ReflectedFunctionCall(JMethodObject, JLocalObject, Boolean, ReadOnlySpan<IObject?>)`: Performs a reflected function
  call on the current object. If the boolean parameter is `true`, it performs a non-virtual call. If the call
  corresponds to a method, it invokes the method and returns an empty result.
- `ReflectedStaticFunctionCall(JExecutableObject, ReadOnlySpan<IObject?>)`: Performs a reflected static function call.
  If the call corresponds to a method, it invokes the method and returns an empty result. If the call corresponds to a
  constructor, it calls the constructor and returns the created instance in the result.

*Note:* `IndeterminateResult` is a `ref struct`, making it incompatible with Visual Basic.

### Indeterminate Result

`IndeterminateResult` is a `ref struct` that securely stores the result of indeterminate access to a Java value.
To process its value, the following properties are available:

- `Signature`: Contains the UTF-8 name of the signature of the instance represented in the indeterminate result.
- `BooleanValue`: Returns the boolean value of the result.
- `ByteValue`: Returns the byte value of the result.
- `CharValue`: Returns the char value of the result.
- `DoubleValue`: Returns the double value of the result.
- `FloatValue`: Returns the float value of the result.
- `IntValue`: Returns the int value of the result.
- `LongValue`: Returns the long value of the result.
- `ShortValue`: Returns the short value of the result.
- `Object`: Returns the object from the result.

**Note:** The `Object` property will only have a value when the signature in the result does not correspond to a
primitive type.
Primitive values in the result are available even if the signature in the result does not correspond to a primitive type
or its specific primitive type.

Thus:

- The `BooleanValue` will always be `true` if the primitive value in the result is different from its default value or
  if the object in the result is not null.
- The other primitive values might be different from their default values if the object in the result is an instance of
  `java.lang.Character` (`JCharObject`) or `java.lang.Number`, or if the actual primitive value in the result, when
  converted to the primitive type, differs from the default value.

## Defining Native Java Calls

The `JNativeCallEntry` class allows defining the implementation of Java native methods and functions with compatible
implementations created in .NET.
These methods must follow the JNI convention for parameter definitions, but they do not require the JNI naming
convention.

To create `JNativeCallEntry` instances, `Rxmxnx.JNetInterface` exposes the following static methods:

- `Create(JMethodDefinition, IntPtr)`: Creates an instance that allows defining the .NET implementation of the native
  method that matches the definition of the `JMethodDefinition` instance using an unmanaged pointer to the method.
- `Create(JFunctionDefinition, IntPtr)`: Creates an instance that allows defining the .NET implementation of the native
  function that matches the definition of the `JFunctionDefinition` instance using an unmanaged pointer to the method.
- `Create<T>(JMethodDefinition, T)`: Creates an instance that allows defining the .NET implementation of the native
  method that matches the definition of the `JMethodDefinition` instance using a managed delegate to the method.
- `Create<T>(JFunctionDefinition, T)`: Creates an instance that allows defining the .NET implementation of the native
  function that matches the definition of the `JFunctionDefinition` instance using a managed delegate to the method.
- `CreateParameterless(ParameterlessInstanceMethodDelegate)`: Creates an instance that allows defining the .NET
  implementation of a parameterless native instance method using a managed delegate to the method.
- `CreateParameterless(ParameterlessStaticMethodDelegate)`: Creates an instance that allows defining the .NET
  implementation of a parameterless native static method using a managed delegate to the method.

**Notes:**

- Creating instances with pointers is ideal when using unsafe contexts
  and [function pointers](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code#function-pointers).
- Creating instances with delegates keeps the delegate instance in memory and will only be collected by the GC when the
  native implementation is replaced or removed.
- Creating instances with delegates
  uses [Marshalling](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.getfunctionpointerfordelegate).
- The delegate type `ParameterlessInstanceMethodDelegate` represents a JNI native function without parameters (
  `JEnvironmentRef, JObjectLocalRef`).
- The delegate type `ParameterlessStaticMethodDelegate` represents a JNI native function without parameters (
  `JEnvironmentRef, JClassLocalRef`).

The `JNativeCallEntry` class exposes the following properties:

- `Name`: Name of the native call. This property comes from the call definition used when creating the instance.
- `Descriptor`: Descriptor of the native call. This property comes from the call definition used when creating the
  instance.
- `Hash`: Hash of the native call definition.
- `Pointer`: Unmanaged pointer to the native implementation of the call.
- `Delegate`: Delegate instance used for the native implementation of the call.