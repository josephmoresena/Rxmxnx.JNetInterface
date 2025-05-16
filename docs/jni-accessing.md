# Java Member Handling

JNI provides access to Java class members, including methods and fields (both instance and static).  
`Rxmxnx.JNetInterface` simplifies this interaction through `JAccessibleObjectDefinition` instances.

Accessible definitions are identified by their name and descriptor. The hash of a definition is derived from the UTF-16
buffer storing the UTF-8 sequence containing these two elements.

## Accessing Java Fields

JNI allows reading (`get`) and writing (`set`) Java fields. `Rxmxnx.JNetInterface` provides this functionality through
`JFieldDefinition` instances.

![FieldDefinitionHierarchy](https://github.com/user-attachments/assets/691a60f9-091a-4752-8813-b2c84576bc0f)

##### Notes

- `JFieldDefinition<..>` defines a Java field for a generic `IDataType<..>`, while `JNonTypedFieldDefinition` defines
  fields for object types not mapped in `Rxmxnx.JNetInterface`.
- `JNonTypedFieldDefinition` cannot be used for primitive fields. Instead, `JFieldDefinition<..>` must be used.
- The API of `JNonTypedFieldDefinition` is identical to that of `JFieldDefinition<JLocalObject>`.

### Field Access APIs

- **Instance Fields**
    - `Get(JLocalObject)`: Retrieves a field from an instance.
    - `Get(JLocalObject, JClassObject)`: Retrieves a field using a superclass as the declaring class.
    - `GetReflected(JFieldObject, JLocalObject)`: Retrieves a reflected field.
    - `Set(JLocalObject, T)`: Sets a field in an instance.
    - `Set(JLocalObject, JClassObject, T)`: Sets a field using a superclass as the declaring class.
    - `SetReflected(JFieldObject, JLocalObject, T)`: Sets a reflected field.

- **Static Fields**
    - `StaticGet(JClassObject)`: Retrieves a static field from a class.
    - `StaticGetReflected(JFieldObject)`: Retrieves a reflected static field.
    - `StaticSet(JClassObject, T)`: Sets a static field in a class.
    - `StaticSetReflected(JFieldObject, T)`: Sets a static field in a reflected class.

If a class has not been loaded (i.e., no active JNI reference exists), a local reference will be created in the active
frame.

## Accessing Java Calls

JNI allows access to both instance and static methods in Java. Functions return a value, while constructors initialize
new instances. `Rxmxnx.JNetInterface` provides this functionality through `JCallDefinition` instances.

![CallDefinitionHierarchy](https://github.com/user-attachments/assets/835e54d4-f2c3-4d8e-a9df-cb1aeed00d49)

##### Notes

- `JFunctionDefinition<..>` defines Java functions with a generic `IDataType<..>`, while `JNonTypedFunctionDefinition`
  is used for unmapped object types.
- `JNonTypedFunctionDefinition` cannot be used for primitive functions; `JFunctionDefinition<..>` must be used instead.
- `Parameterless` types define methods, functions, or constructors without parameters.
- Call definitions expose the parameter count and byte size.

### Method and Function Invocation APIs

- **Instance Methods**
    - `Invoke(JLocalObject, ReadOnlySpan<IObject?>)`: Invokes an instance method.
    - `Invoke(JLocalObject, JClassObject, ReadOnlySpan<IObject?>)`: Invokes a method using a superclass.
    - `InvokeNonVirtual(JLocalObject, JClassObject, ReadOnlySpan<IObject?>)`: Invokes a non-virtual method from a
      superclass.
    - `InvokeReflected(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`: Invokes a reflected method.
    - `InvokeNonVirtualReflected(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`: Invokes a reflected non-virtual
      method.

- **Static Methods**
    - `StaticInvoke(JClassObject, ReadOnlySpan<IObject?>)`: Invokes a static method.
    - `InvokeStaticReflected(JMethodObject, ReadOnlySpan<IObject?>)`: Invokes a reflected static method.

### Constructor Invocation APIs

- **`New(JClassObject, ReadOnlySpan<IObject?>)`**: Creates a new instance of a class.
- **`New<TObject>(IEnvironment, ReadOnlySpan<IObject?>)`**: Creates a new instance of a generic type.
- **`NewReflected(JConstructorObject, ReadOnlySpan<IObject?>)`**: Creates a new instance using a reflected constructor.
- **`NewReflected<TObject>(IEnvironment, ReadOnlySpan<IObject?>)`**: Creates a new instance using a reflected
  constructor for a generic type.

##### Note

- `JMethodDefinition`, `JFunctionDefinition<..>`, and `JConstructorDefinition` expose these APIs as protected.
- `Parameterless` variants do not accept arguments.
- In .NET 9.0+, `ReadOnlySpan<IObject?>` supports `params`.
- If a class is used as a declaring class or function argument but is not loaded, a local reference is created.

## Indeterminate Fields

Indeterminate fields encapsulate Java field definitions (`JFieldDefinition`), allowing fields to be dynamically
executed.

### Indeterminate field Creation

- **`Create(JArgumentMetadata, ReadOnlySpan<Byte>)`**: Defines a field with specified type and name.
- **`Create<TResult>(ReadOnlySpan<Byte>)`**: Defines a field with specified type and name.

### Getting and Setting field retrieving

- **`Get(JLocalObject)`**: Retrieves the field value for the encapsulated definition.
- **`Get(JLocalObject, JClassObject)`**: Retrieves the field value for the encapsulated definition with a superclass.
- **`StaticGet(JClassObject)`**: Retrieves the static field value for the encapsulated definition.
- **`ReflectedGet(JFieldObject, JLocalObject)`**: Retrieves the reflected field value with the encapsulated definition.
- **`ReflectedStaticGet(JFieldObject)`**: Retrieves the reflected static field value with the encapsulated definition.
- **`Set<TValue>(JLocalObject, TValue?)`**: Sets the field value for the encapsulated definition.
- **`Set<TValue>(JLocalObject, JClassObject, TValue?)`**: Sets the field value for the encapsulated definition with a
  superclass.
- **`StaticSet<TValue>(JClassObject, TValue?)`**: Sets the static field value for the encapsulated definition.
- **`ReflectedSet<TValue>(JFieldObject, JLocalObject, TValue?)`**: Sets the reflected field value with the encapsulated
  definition.
- **`ReflectedStaticSet<TValue>(JFieldObject, TValue?)`**: Sets the reflected static field value with the encapsulated
  definition.

##### Note

- `TValue` generic parameter is any `IObject` implementing type.

## Indeterminate Calls

Indeterminate calls encapsulate Java call definitions (`JCallDefinition`), allowing methods, functions, and constructors
to be dynamically executed.

### Indeterminate Call Creation

- **`CreateConstructorDefinition(ReadOnlySpan<JArgumentMetadata>)`**: Defines a constructor with specified parameters.
- **`CreateMethodDefinition(ReadOnlySpan<Byte>, ReadOnlySpan<JArgumentMetadata>)`**: Defines a method with specified
  parameters.
- **`CreateMethodDefinition(JArgumentMetadata, ReadOnlySpan<Byte>, ReadOnlySpan<JArgumentMetadata>)`**: Defines a
  function by return type, name and parameter metadata.
- **`CreateFunctionDefinition<TResult>(ReadOnlySpan<Byte>, ReadOnlySpan<JArgumentMetadata>)`**: Defines a function by
  return type (defined by generic parameter), name and parameter metadata.

### Constructor Calls

- **`NewCall(JClassObject, ReadOnlySpan<IObject?>)`**: Calls a constructor for the encapsulated definition.
- **`NewCall<TObject>(IEnvironment, ReadOnlySpan<IObject?>)`**: Calls a constructor for the encapsulated definition. The
  class for `TObject` is used as declaring class.
- **`ReflectedNewCall(JConstructorObject, ReadOnlySpan<IObject?>)`**: Calls a reflected constructor with the
  encapsulated definition.
- **`ReflectedNewCallReflectedNewCall<TObject>(JConstructorObject, ReadOnlySpan<IObject?>)`**: Calls a reflected
  constructor with the encapsulated definition. `TObject` is used as the resulting instance type.

### Method Calls

- **`MethodCall(JLocalObject, ReadOnlySpan<IObject?>)`**: Calls the method for the encapsulated definition.
- **`MethodCall(JLocalObject, JClassObject, Boolean, ReadOnlySpan<IObject?>)`**: Calls the method for the encapsulated
  definition with a superclass, optionally non-virtual.
- **`StaticMethodCall(JClassObject, ReadOnlySpan<IObject?>)`**: Calls the static method for the encapsulated definition.
- **`ReflectedMethodCall(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`**: Calls a reflected method with the
  encapsulated definition.
- **`ReflectedMethodCall(JMethodObject, JLocalObject, Boolean, ReadOnlySpan<IObject?>)`**: Calls a reflected method with
  the encapsulated definition, optionally non-virtual.
- **`ReflectedStaticMethodCall(JExecutableObject, ReadOnlySpan<IObject?>)`**: Calls a reflected static method with the
  encapsulated definition.

### Function Calls

- **`FunctionCall(JLocalObject, ReadOnlySpan<IObject?>)`**: Calls the function for the encapsulated definition.
- **`FunctionCall(JLocalObject, JClassObject, Boolean, ReadOnlySpan<IObject?>)`**: Calls the function for the
  encapsulated definition with a superclass, optionally non-virtual.
- **`StaticFunctionCall(JClassObject, ReadOnlySpan<IObject?>)`**: Calls the static function for the encapsulated
  definition.
- **`ReflectedFunctionCall(JMethodObject, JLocalObject, ReadOnlySpan<IObject?>)`**: Calls a reflected function with the
  encapsulated definition.
- **`ReflectedStaticFunctionCall(JExecutableObject, ReadOnlySpan<IObject?>)`**: Calls a reflected static function with
  the encapsulated definition.

## Indeterminate Result Handling

`IndeterminateResult` securely stores the result of indeterminate Java value access.

### Properties of `IndeterminateResult`

- **`Signature`**: UTF-8 signature name.
- **`BooleanValue`**, **`ByteValue`**, **`CharValue`**, **`DoubleValue`**, **`FloatValue`**, **`IntValue`**,
  **`LongValue`**, **`ShortValue`**: Retrieve primitive values.
- **`Object`**: Retrieves the object value if applicable.

##### Note

- If `Signature` represents a non-primitive type, `Object` contains the result; otherwise, primitive values are used.
- If the result is non-null, `BooleanValue` is always `true`.
- `IndeterminateResult` is a `ref struct` type, making it incompatible with Visual Basic.

## Defining Native Java Calls

The `JNativeCallEntry` class maps Java native methods to .NET implementations. These methods follow [JNI parameter
conventions](jni-references.md#jni-native-call-parameters) but do not
require [JNI naming](jni-classes.md#naming-conventions).

### Creating Native Call Entries

- **`Create(JMethodDefinition, IntPtr)`**: Defines a native method using an unmanaged pointer.
- **`Create(JFunctionDefinition, IntPtr)`**: Defines a native function using an unmanaged pointer.
- **`Create<TDelegate>(JMethodDefinition, TDelegate)`**: Defines a native method using a delegate.
- **`Create<TDelegate>(JFunctionDefinition, TDelegate)`**: Defines a native function using a delegate.
- **`CreateParameterless(ParameterlessInstanceMethodDelegate)`**: Defines a parameterless native instance method.
- **`CreateParameterless(ParameterlessStaticMethodDelegate)`**: Defines a parameterless native static method.

##### Notes

- Using pointers is ideal for `unsafe` contexts
  and [function pointers](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code#function-pointers).
- Delegates persist in memory until the native implementation is replaced or removed.
- Delegates
  use [marshalling](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.getfunctionpointerfordelegate).

### `JNativeCallEntry` Properties

- **`Name`**: Native call name.
- **`Descriptor`**: Call descriptor.
- **`Hash`**: Call definition hash.
- **`Pointer`**: Unmanaged function pointer.
- **`Delegate`**: Delegate instance used as the native implementation.