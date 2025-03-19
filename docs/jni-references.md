# JNI Reference Handling

All instances of global, weak global, and local objects (or their local views via `ILocalObject`) implement the
`IDisposable` interface.  
When calling the `Dispose` method, `Rxmxnx.JNetInterface` removes the associated JNI references.

However, in some cases, calling `Dispose` may not immediately remove the JNI references due to:

- The JNI reference being used by another thread (applies to global and weak global references).
- The JVM requiring the reference for native memory guarantees. The memory must be released before calling `Dispose()`.
- The JNI reference being shared among multiple `JLocalObject` instances, requiring `Dispose()` to be called on each
  one.
- The `JLocalObject` or `ILocalObject` instance lacking a loaded local reference, relying on an associated global or
  weak global reference.
- The `JLocalObject` or `ILocalObject` instance being created as a parameter within a Java-to-JNI call.

##### Notes

- Local references created within the active environment frame may not require explicit release.
- Some references may be released automatically, even without calling `Dispose()`.

##### Topics

- [Local Reference Handling](#local-reference-handling)
    - [Environment Frames](#environment-frames)
        - [Fixed Frame](#fixed-frame)
        - [Call Frame](#call-frame)
            - [JNI Native Call Parameters](#jni-native-call-parameters)
- [Global Reference Handling](#global-reference-handling)
- [Global Weak Reference Handling](#global-weak-reference-handling)

---  

## Local Reference Handling

All JNI calls that return Java object references return **local references**. These remain valid while the active
environment frame is maintained or explicitly removed.  
Local JNI references are valid **only within the thread that created them**. For this reason, all `JLocalObject` and
`ILocalObject` instances in `Rxmxnx.JNetInterface` are bound to a specific `IEnvironment` instance.

### Environment Frames

Environment frames store local references. While JNI does not impose a strict limit on local references, the active
frame may enforce a limit.

#### Types of Frames

1. **Initial Frame**:
    - The default frame when a thread associates with the JVM via JNI or the Invocation API.
    - Remains valid for as long as the thread is attached.

2. **Call Frame**:
    - Created by the JVM when executing a Java native call.
    - `JNativeCallAdapter` must be used to notify `Rxmxnx.JNetInterface` of this temporary frame.

3. **Fixed Frame**:
    - A manually created JNI frame with a predefined reference limit.
    - Exceeding this limit causes older references to be invalidated (FIFO behavior).

The `IEnvironment` interface provides the `LocalCapacity` property, allowing:

- Checking the current frame's local reference capacity.
- Ensuring that the frame supports a required capacity.

##### Notes

- `LocalCapacity` is `null` for initial and call frames, as their capacity is undefined.
- Fixed frames have a predefined `LocalCapacity` set at creation.
- `LocalCapacity` can only be increased beyond its initial value.
- Setting `LocalCapacity` in a fixed frame throws an `InvalidOperationException`.
- Setting `LocalCapacity` in initial or call frames triggers the JNI `EnsureLocalCapacity` call.
    - If unsuccessful, a `JniException` is thrown.
- Only one active frame exists per environment at a time.
- Once a frame is finalized, its parent frame is reactivated, invalidating any references from the finalized frame.

---  

#### Fixed Frame

A frame with a **predefined local reference limit**. When full, older references are **automatically invalidated**.

To use a fixed frame in `Rxmxnx.JNetInterface`:

```csharp
environment.WithFrame(16, () => { /* JNI operations */ });
```  

Alternative overloads allow passing state or returning a result:

```csharp
environment.WithFrame(16, state, (s) => { /* JNI operations */ });
int result = environment.WithFrame(16, () => { return 42; });
```  

##### Notes

- Delegates should be **static**
  for [better performance](https://devblogs.microsoft.com/dotnet/understanding-the-cost-of-csharp-delegates/).
- In **.NET 9.0+**, the **state parameter** supports `ref struct`.

---  

#### Call Frame

Created automatically by the JVM when executing a Java native call. Native calls must follow specific naming and
parameter conventions.

##### JNI Native Call Parameters

Example Java native call signatures and corresponding JNI parameters:

| Java Signature                                        | JNI Parameter Types                                                       |  
|-------------------------------------------------------|---------------------------------------------------------------------------|  
| `call()`                                              | `()` (no parameters)                                                      |  
| `call(String a)`                                      | `JStringLocalRef`                                                         |  
| `call(boolean a)`                                     | `JBoolean`                                                                |  
| `call(Class<?> a)`                                    | `JClassLocalRef`                                                          |  
| `call(Character a)`                                   | `JObjectLocalRef`                                                         |  
| `<T> call(T a)`                                       | `JObjectLocalRef`                                                         |  
| `<T> call(T[] a, boolean[] b)`                        | `JObjectArrayLocalRef`, `JBooleanArrayLocalRef`                           |  
| `<T call Number> method(T a, char b)`                 | `JObjectLocalRef`, `JChar`                                                |  
| `call(int a, String b, long c, int[] d, Integer[] e)` | `JInt`, `JStringLocalRef`, `JLong`, `JIntArrayLocalRef`, `JArrayLocalRef` |  

*Refer to the [JNI Naming Conventions](jni-classes.md#naming-conventions).*

To create a call frame in `Rxmxnx.JNetInterface`, use:

```csharp
JNativeCall.Create(JEnvironmentRef envRef).Build();
JNativeCall.Create(IVirtualMachine vm, JEnvironmentRef envRef).Build();
```  

For different cases:

- **Instance methods:**
  ```csharp
  JNativeCall.Create(JEnvironmentRef envRef, JObjectLocalRef localRef, out JLocalObject jLocal).Build();
  JNativeCall.Create(IVirtualMachine vm, JEnvironmentRef envRef, JObjectLocalRef localRef, out JLocalObject jLocal).Build();
  JNativeCall.Create<TObject>(JEnvironmentRef envRef, JObjectLocalRef localRef, out TObject jObject).Build();
  JNativeCall.Create<TObject>(IVirtualMachine vm, JEnvironmentRef envRef, JObjectLocalRef localRef, out TObject jObject).Build();
  ```  
- **Static methods:**
  ```csharp
  JNativeCall.Create(JEnvironmentRef envRef, JClassLocalRef classRef, out JClassObject jClass).Build();
  JNativeCall.Create(IVirtualMachine vm, JEnvironmentRef envRef, JClassLocalRef classRef, out JClassObject jClass).Build();
  ```  

To use reference type argument calls, use `WithParameter` method before call `Build()` method.

- `call(String a)`:
    ```csharp
    adapter.WithParameter(JStringLocalRef stringRefA, out JStringObject jStringA)
    ```  
- `call(Class<?> a)`:
    ```csharp
    adapter.WithParameter(JClassLocalRef classRefA, out JClassObject jClassA)
    ```  
- `call(Character a)`:
    ```csharp
    adapter.WithParameter<JCharacterObject>(JObjectLocalRef localRefA, out JCharacterObject jCharacterA)
    ```  
- `<T> call(T a)`:
    ```csharp
    adapter.WithParameter(JObjectLocalRef localRefA, out JLocalObject jLocalA)
    ```  
- `<T> call(T[] a, boolean[] b)`:
    ```csharp
    adapter
        .WithParameter<JLocalObject>(JArrayObjectLocalRef arrayRefA, out JArrayObject<JLocalObject> jArrayA)
        .WithParameter(JBooleanArrayLocalRef arrayRefB, out JArrayObject<JBoolean> jArrayB)
    ```  
- `<T extends Number> call(T a, char b)`:
    ```csharp
    adapter.WithParameter<JNumberObject>(JObjectLocalRef localRefA, out JNumberObject jNumberA)
    ```  
- `<T extends Number> call(int a, String b, long c, int[] d, Integer[] e)`:
    ```csharp
    adapter
        .WithParameter(JStringLocalRef stringRefB, out JStringObject jStringB)
        .WithParameter(JIntArrayLocalRef arrayRefD, out JArrayObject<JInt> jArrayD) 
        .WithParameter<JIntegerObject>(JArrayLocalRef arrayRefE, out JArrayObject<JIntegerObject> jArrayE)
    ```

To finalize a call and remove the call frame:

```csharp
adapter.FinalizeCall();
```  

Other overloads exist for different return types:

```csharp
adapter.FinalizeCall(JLocalObject? result);
adapter.FinalizeCall(JClassObject? result);
adapter.FinalizeCall(JStringObject? result);
adapter.FinalizeCall(JThrowableObject? result);
adapter.FinalizeCall(JArrayObject? result);
adapter.FinalizeCall(JLocalObject.InterfaceView? result);
adapter.FinalizeCall<TPrimitive>(TPrimitive result);
adapter.FinalizeCall(JArrayObject<JBoolean>? result);
adapter.FinalizeCall(JArrayObject<JByte>? result);
adapter.FinalizeCall(JArrayObject<JChar>? result);
adapter.FinalizeCall(JArrayObject<JDouble>? result);
adapter.FinalizeCall(JArrayObject<JFloat>? result);
adapter.FinalizeCall(JArrayObject<JInt>? result);
adapter.FinalizeCall(JArrayObject<JLong>? result);
adapter.FinalizeCall(JArrayObject<JShort>? result);
adapter.FinalizeCall<TElement>(JArrayObject<TElement>? result);
```  

##### Notes

- Methods using `IVirtualMachine` are more efficient since `Rxmxnx.JNetInterface` supports multiple JVM instances.
- Both `JNativeCallAdapter` and `JNativeCallAdapter.Builder` are `ref struct` types, making them incompatible
  with the Visual Basic .NET language.
- Once the `Build()` method is called, it is always required to call the `Finalize` method on the created instance.
  Failing to do so may affect the behavior of the `IEnvironment` instance, as it could treat invalid local references as
  immutable.
- Local references associated with the target instance/class and parameters cannot be manually released.
  Calling `.Dispose()` has no effect on these references.

---  

## Global Reference Handling

Global JNI references are managed via `JGlobal` instances in `Rxmxnx.JNetInterface`.

To obtain a `JGlobal` reference from a `JLocalObject`:

```csharp
JGlobal globalRef = localObject.Global;
```  

To release the global reference:

```csharp
globalRef.Dispose();
```  

##### Notes

- **Class references (`JClassObject`) are shared** among all environments within the same JVM.
- `JGlobal` instances linked to `JClassObject` types **are never garbage collected**.
- `JGlobal` instances **may be automatically invalidated** if detected as unusable.
- For main-class types, `JGlobal` references **are not validated**.

---  

## Global Weak Reference Handling

Weak global JNI references are managed via `JWeak` instances.

To obtain a `JWeak` reference from a `JLocalObject`:

```csharp
JWeak weakRef = localObject.Weak;
```  

To release the weak reference:

```csharp
weakRef.Dispose();
```  

##### Notes

- `JWeak` references **are checked more frequently** than `JGlobal` references.
- If a valid `JWeak` reference exists, it will be used **unless a valid `JGlobal` reference is available**.  
