namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNINativeInterface_</c> struct. Contains all pointers to the functions of JNI.
/// </summary>
public readonly partial struct JNativeInterface : INative<JNativeInterface>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JNativeInterface;

    /// <summary>
    /// public reserved entries. 
    /// </summary>
    private readonly ComReserved _reserved;

	/// <summary>
	/// Pointer to <c>GetVersion</c> function. Retrieves the version of the <c>JNIEnv</c> interface.
	/// </summary>
	internal readonly IntPtr GetVersionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>DefineClass</c> function. Loads a class or interface type from a buffer of raw 
	/// binary data.
	/// </summary>
	internal readonly IntPtr DefineClassPointer { get; init; }
	/// <summary>
	/// Pointer to <c>FindClass</c> function. Loads a class or interface type of a given name.
	/// </summary>
	internal readonly IntPtr FindClassPointer { get; init; }
	/// <summary>
	/// Pointer to <c>FromReflectedMethod</c> function. Retrieves a method identifer for given
	/// <c>java.lang.reflect.Method</c> or <c>java.lang.reflect.Constructor</c> reference.
	/// </summary>
	internal readonly IntPtr FromReflectedMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>FromReflectedField</c> function. Retrieves a field identifer for given
	/// <c>java.lang.reflect.Field</c> reference.
	/// </summary>
	internal readonly IntPtr FromReflectedFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ToReflectedMethod</c> function. Retrieves a <c>java.lang.reflect.Method</c> or 
	/// <c>java.lang.reflect.Constructor</c> reference for given method identifer and class reference.
	/// </summary>
	internal readonly IntPtr ToReflectedMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetSuperclass</c> function. Retrieves a local reference to the class extended by 
	/// given class reference.
	/// </summary>
	internal readonly IntPtr GetSuperclassPointer { get; init; }
	/// <summary>
	/// Pointer to <c>IsAssignableFrom</c> function. Determines whether instances of one class can 
	/// be used when instances of another class are expected.
	/// </summary>
	internal readonly IntPtr IsAssignableFromPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ToReflectedField</c> function. Retrieves a <c>java.lang.reflect.Field</c> 
	/// reference for given field identifer and class reference.
	/// </summary>
	internal readonly IntPtr ToReflectedFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>Throw</c> function. Causes a <c>java.lang.Throwable</c> object to be thrown.
	/// </summary>
	internal readonly IntPtr ThrowPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ThrowNew</c> function. Constructs and throws an exception object from the 
	/// specified class and message.
	/// </summary>
	internal readonly IntPtr ThrowNewPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ExceptionOccurred</c> function. Retrieves the reference to the exception object 
	/// that is being thrown on the current thread.
	/// </summary>
	internal readonly IntPtr ExceptionOccurredPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ExceptionDescribe</c> function. Prints an exception and a backtrace of the 
	/// stack to a system error-reporting channel, such as <c>System.out.err</c>.
	/// </summary>
	internal readonly IntPtr ExceptionDescribePointer { get; init; }
	/// <summary>
	/// Pointer to <c>ExceptionClear</c> function. Clears any pending exception that is currently 
	/// being thrown in the current thread.
	/// </summary>
	internal readonly IntPtr ExceptionClearPointer { get; init; }
	/// <summary>
	/// Pointer to <c>FatalError</c> function. Raises a fatal error and does not expect the JVM to 
	/// recover.
	/// </summary>
	internal readonly IntPtr FatalErrorPointer { get; init; }
	/// <summary>
	/// Pointer to <c>PushLocalFrame</c> function. Creates a new local reference frame, in which at 
	/// least a given number of local references can be created.
	/// </summary>
	internal readonly IntPtr PushLocalFramePointer { get; init; }
	/// <summary>
	/// Pointer to <c>PopLocalFrame</c> function. Pops off the current local reference frame, frees all 
	/// the local references, and returns a local reference in the previous local reference frame for the 
	/// given local object reference.
	/// </summary>
	internal readonly IntPtr PopLocalFramePointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewGlobalRef</c> function. Creates a new global reference to the object referred 
	/// to by the given reference.
	/// </summary>
	internal readonly IntPtr NewGlobalRefPointer { get; init; }
	/// <summary>
	/// Pointer to <c>DeleteGlobalRef</c> function. Deletes the given global reference.
	/// </summary>
	internal readonly IntPtr DeleteGlobalRefPointer { get; init; }
	/// <summary>
	/// Pointer to <c>DeleteLocalRef</c> function. Deletes the given local reference.
	/// </summary>
	internal readonly IntPtr DeleteLocalRefPointer { get; init; }
	/// <summary>
	/// Pointer to <c>IsSameObject</c> function. Indicates whether two references refer to the same 
	/// object.
	/// </summary>
	internal readonly IntPtr IsSameObjectPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewLocalRef</c> function. Creates a new local reference to the object referred 
	/// to by the given reference.
	/// </summary>
	internal readonly IntPtr NewLocalRefPointer { get; init; }
	/// <summary>
	/// Pointer to <c>EnsureLocalCapacity</c> function. Ensures that at least a given number of local 
	/// references can be created in the current thread.
	/// </summary>
	internal readonly IntPtr EnsureLocalCapacityPointer { get; init; }
	/// <summary>
	/// Pointer to <c>AllocObject</c> function. Allocates a new object without invoking any of the 
	/// constructors for the object.
	/// </summary>
	internal readonly IntPtr AllocObjectPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewObject</c> function. Constructs a new object.
	/// </summary>
	internal readonly IntPtr NewObjectPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewObjectV</c> function. Constructs a new object.
	/// </summary>
	internal readonly IntPtr NewObjectVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewObjectA</c> function. Constructs a new object.
	/// </summary>
	internal readonly IntPtr NewObjectAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetObjectClass</c> function. Constructs a new object.
	/// </summary>
	internal readonly IntPtr GetObjectClassPointer { get; init; }
	/// <summary>
	/// Pointer to <c>IsInstanceOf</c> function. Indicates whether an object is an instance of a class or 
	/// interface.
	/// </summary>
	internal readonly IntPtr IsInstanceOfPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetMethodId</c> function. Retrieves the method identifier for an instance 
	/// (non-static) method of a class or interface. 
	/// </summary>
	internal readonly IntPtr GetMethodIdPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallObjectMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is an object.
	/// </summary>
	internal readonly IntPtr CallObjectMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallObjectMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is an object.
	/// </summary>
	internal readonly IntPtr CallObjectMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallObjectMethodA</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is an object.
	/// </summary>
	internal readonly IntPtr CallObjectMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallBooleanMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallBooleanMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallBooleanMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallBooleanMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallBooleanMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallBooleanMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallByteMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallByteMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallByteMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallByteMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallByteMethodA</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallByteMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallCharMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Char"/> value.
	/// </summary>
	internal readonly IntPtr CallCharMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallCharMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Char"/> value.
	/// </summary>
	internal readonly IntPtr CallCharMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallCharMethodA</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Char"/> value.
	/// </summary>
	internal readonly IntPtr CallCharMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallShortMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallShortMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallShortMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallShortMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallShortMethodA</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallShortMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallIntMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallIntMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallIntMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallIntMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallIntMethodA</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallIntMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallLongMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallLongMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallLongMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallLongMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallLongMethodA</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallLongMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallFloatMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallFloatMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallFloatMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallFloatMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallFloatMethodA</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallFloatMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallDoubleMethod</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallDoubleMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallDoubleMethodV</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallDoubleMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallDoubleMethodA</c> function. Invokes an instance (non-static) method on an object 
	/// whose return is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallDoubleMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallVoidMethod</c> function. Invokes an returnless instance (non-static) method on 
	/// an object.
	/// </summary>
	internal readonly IntPtr CallVoidMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallVoidMethodV</c> function. Invokes an returnless instance (non-static) method on 
	/// an object.
	/// </summary>
	internal readonly IntPtr CallVoidMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallVoidMethodA</c> function. Invokes an returnless instance (non-static) method on 
	/// an object.
	/// </summary>
	internal readonly IntPtr CallVoidMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualObjectMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is an object.
	/// </summary>
	internal readonly IntPtr CallNonVirtualObjectMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualObjectMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is an object.
	/// </summary>
	internal readonly IntPtr CallNonVirtualObjectMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualObjectMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is an object.
	/// </summary>
	internal readonly IntPtr CallNonVirtualObjectMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualBooleanMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualBooleanMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualBooleanMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualBooleanMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualBooleanMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualBooleanMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualByteMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualByteMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualByteMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualByteMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualByteMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualByteMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualCharMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Char"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualCharMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualCharMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Char"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualCharMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualCharMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Char"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualCharMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualShortMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualShortMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualShortMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualShortMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualShortMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualShortMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualIntMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualIntMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualIntMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualIntMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualIntMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualIntMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualLongMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualLongMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualLongMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualLongMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualLongMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualLongMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualFloatMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualFloatMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualFloatMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualFloatMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualFloatMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualFloatMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualDoubleMethod</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualDoubleMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualDoubleMethodV</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualDoubleMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualDoubleMethodA</c> function. Invokes an instance (non-static) method on 
	/// an object whose return is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallNonVirtualDoubleMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualVoidMethod</c> function. Invokes an returnless instance (non-static) 
	/// method on an object.
	/// </summary>
	internal readonly IntPtr CallNonVirtualVoidMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualVoidMethodV</c> function. Invokes an returnless instance (non-static) 
	/// method on an object.
	/// </summary>
	internal readonly IntPtr CallNonVirtualVoidMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallNonVirtualVoidMethodA</c> function. Invokes an returnless instance (non-static) 
	/// method on an object.
	/// </summary>
	internal readonly IntPtr CallNonVirtualVoidMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetFieldId</c> function. Retrieves the field identifier for an instance (non-static) 
	/// field of a class. 
	/// </summary>
	internal readonly IntPtr GetFieldIdPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetObjectField</c> function. Retrieves the object that is the value of a field of 
	/// an instance.
	/// </summary>
	internal readonly IntPtr GetObjectFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetBooleanField</c> function. Retrieves the <see cref="Boolean"/> that is the 
	/// value of a field of an instance.
	/// </summary>
	internal readonly IntPtr GetBooleanFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetByteField</c> function. Retrieves the <see cref="SByte"/> that is the 
	/// value of a field of an instance.
	/// </summary>
	internal readonly IntPtr GetByteFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetCharField</c> function. Retrieves the <see cref="Char"/> that is the 
	/// value of a field of an instance.
	/// </summary>
	internal readonly IntPtr GetCharFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetShortField</c> function. Retrieves the <see cref="Int16"/> that is the 
	/// value of a field of an instance.
	/// </summary>
	internal readonly IntPtr GetShortFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetIntField</c> function. Retrieves the <see cref="Int32"/>that is the 
	/// value of a field of an instance.
	/// </summary>
	internal readonly IntPtr GetIntFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetLongField</c> function. Retrieves the <see cref="Int64"/> that is the 
	/// value of a field of an instance.
	/// </summary>
	internal readonly IntPtr GetLongFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetFloatField</c> function. Retrieves the <see cref="Single"/> that is the 
	/// value of a field of an instance.
	/// </summary>
	internal readonly IntPtr GetFloatFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetDoubleField</c> function. Retrieves the <see cref="Double"/> that is the 
	/// value of a field of an instance.
	/// </summary>
	internal readonly IntPtr GetDoubleFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetObjectField</c> function. Sets an object to the value of a field of an instance.
	/// </summary>
	internal readonly IntPtr SetObjectFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetBooleanField</c> function. Sets a <see cref="Boolean"/> to the value of a field 
	/// of an instance.
	/// </summary>
	internal readonly IntPtr SetBooleanFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetByteField</c> function. Sets a <see cref="SByte"/> to the value of a field 
	/// of an instance.
	/// </summary>
	internal readonly IntPtr SetByteFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetCharField</c> function. Sets a <see cref="Char"/> to the value of a field 
	/// of an instance.
	/// </summary>
	internal readonly IntPtr SetCharFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetShortField</c> function. Sets a <see cref="Int16"/> to the value of a field 
	/// of an instance.
	/// </summary>
	internal readonly IntPtr SetShortFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetIntField</c> function. Sets a <see cref="Int32"/>to the value of a field 
	/// of an instance.
	/// </summary>
	internal readonly IntPtr SetIntFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetLongField</c> function. Sets a <see cref="Int64"/> to the value of a field 
	/// of an instance.
	/// </summary>
	internal readonly IntPtr SetLongFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetFloatField</c> function. Sets a <see cref="Single"/> to the value of a field 
	/// of an instance.
	/// </summary>
	internal readonly IntPtr SetFloatFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetDoubleField</c> function. Sets a <see cref="Double"/> to the value of a field 
	/// of an instance.
	/// </summary>
	internal readonly IntPtr SetDoubleFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticMethodId</c> function. Retrieves the method identifier for an static method 
	/// of a class. 
	/// </summary>
	internal readonly IntPtr GetStaticMethodIdPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticObjectMethod</c> function. Invokes an static method on a class whose return 
	/// is an object.
	/// </summary>
	internal readonly IntPtr CallStaticObjectMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticObjectMethodV</c> function. Invokes an static method on a class whose return 
	/// is an object.
	/// </summary>
	internal readonly IntPtr CallStaticObjectMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticObjectMethodA</c> function. Invokes an static method on a class whose return 
	/// is an object.
	/// </summary>
	internal readonly IntPtr CallStaticObjectMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticBooleanMethod</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticBooleanMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticBooleanMethodV</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticBooleanMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticBooleanMethodA</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Boolean"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticBooleanMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticByteMethod</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticByteMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticByteMethodV</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticByteMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticByteMethodA</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticByteMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticByteMethod</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="SByte"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticCharMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticCharMethodV</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Char"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticCharMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticCharMethodA</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Char"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticCharMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticShortMethod</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticShortMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticShortMethodV</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticShortMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticShortMethodA</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int16"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticShortMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticIntMethod</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallStaticIntMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticIntMethodV</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallStaticIntMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticIntMethodA</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int32"/>value.
	/// </summary>
	internal readonly IntPtr CallStaticIntMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticLongMethod</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticLongMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticLongMethodV</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticLongMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticLongMethodA</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Int64"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticLongMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticFloatMethod</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticFloatMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticFloatMethodV</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticFloatMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticFloatMethodA</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Single"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticFloatMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticDoubleMethod</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticDoubleMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticDoubleMethodV</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticDoubleMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticDoubleMethodA</c> function. Invokes an static method on a class whose return 
	/// is a <see cref="Double"/> value.
	/// </summary>
	internal readonly IntPtr CallStaticDoubleMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticVoidMethod</c> function. Invokes an returnless static method on a class.
	/// </summary>
	internal readonly IntPtr CallStaticVoidMethodPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticVoidMethodV</c> function. Invokes an returnless static method on a class.
	/// </summary>
	internal readonly IntPtr CallStaticVoidMethodVPointer { get; init; }
	/// <summary>
	/// Pointer to <c>CallStaticVoidMethodA</c> function. Invokes an returnless static method on a class.
	/// </summary>
	internal readonly IntPtr CallStaticVoidMethodAPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticFieldId</c> function. Retrieves the method identifier for an static method 
	/// of a class. 
	/// </summary>
	internal readonly IntPtr GetStaticFieldIdPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticObjectField</c> function. Retrieves the object that is the value of a field 
	/// of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticObjectFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticBooleanField</c> function. Retrieves the <see cref="Boolean"/> that is the 
	/// value of a field of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticBooleanFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticByteField</c> function. Retrieves the <see cref="SByte"/> that is the 
	/// value of a field of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticByteFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticCharField</c> function. Retrieves the <see cref="Char"/> that is the 
	/// value of a field of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticCharFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticShortField</c> function. Retrieves the <see cref="Int16"/> that is the 
	/// value of a field of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticShortFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticIntField</c> function. Retrieves the <see cref="Int32"/>that is the 
	/// value of a field of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticIntFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticLongField</c> function. Retrieves the <see cref="Int64"/> that is the 
	/// value of a field of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticLongFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticFloatField</c> function. Retrieves the <see cref="Single"/> that is the 
	/// value of a field of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticFloatFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStaticDoubleField</c> function. Retrieves the <see cref="Double"/> that is the 
	/// value of a field of a class object.
	/// </summary>
	internal readonly IntPtr GetStaticDoubleFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticObjectField</c> function. Sets an object to the value of a field of a class 
	/// object.
	/// </summary>
	internal readonly IntPtr SetStaticObjectFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticBooleanField</c> function. Sets a <see cref="Boolean"/> to the value of a 
	/// field of a class object.
	/// </summary>
	internal readonly IntPtr SetStaticBooleanFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticByteField</c> function. Sets a <see cref="SByte"/> to the value of a 
	/// field of a class object.
	/// </summary>
	internal readonly IntPtr SetStaticByteFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticCharField</c> function. Sets a <see cref="Char"/> to the value of a 
	/// field of a class object.
	/// </summary>
	internal readonly IntPtr SetStaticCharFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticShortField</c> function. Sets a <see cref="Int16"/> to the value of a 
	/// field of a class object.
	/// </summary>
	internal readonly IntPtr SetStaticShortFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticBooleanField</c> function. Sets a <see cref="Boolean"/> to the value of a 
	/// field of a class object.
	/// </summary>
	internal readonly IntPtr SetStaticIntFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticLongField</c> function. Sets a <see cref="Int64"/> to the value of a 
	/// field of a class object.
	/// </summary>
	internal readonly IntPtr SetStaticLongFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticFloatField</c> function. Sets a <see cref="Single"/> to the value of a 
	/// field of a class object.
	/// </summary>
	internal readonly IntPtr SetStaticFloatFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetStaticDoubleField</c> function. Sets a <see cref="Double"/> to the value of a 
	/// field of a class object.
	/// </summary>
	internal readonly IntPtr SetStaticDoubleFieldPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewString</c> function. Constructs a new <c>java.lang.String</c> object 
	/// from a pointer to Unicode characters sequence.
	/// </summary>
	internal readonly IntPtr NewStringPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStringLength</c> function. Retrieves the number of Unicode characters that 
	/// constitute a string.
	/// </summary>
	internal readonly IntPtr GetStringLengthPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStringChars</c> function. Returns a pointer to the Unicode characters sequence
	/// of the string.
	/// </summary>
	internal readonly IntPtr GetStringCharsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseStringChars</c> function. Informs the JVM that the access to the unicode 
	/// characters sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseStringCharsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewStringUtf</c> function. Constructs a new <c>java.lang.String</c> object 
	/// from a pointer to Utf-8 characters sequence.
	/// </summary>
	internal readonly IntPtr NewStringUtfPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStringUtfLength</c> function. Retrieves the number of bytes needed to represent a 
	/// string in Utf-8 format.
	/// </summary>
	internal readonly IntPtr GetStringUtfLengthPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStringUtfChars</c> function. Returns a pointer to the Utf-8 characters sequence
	/// of the string.
	/// </summary>
	internal readonly IntPtr GetStringUtfCharsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseStringUtfChars</c> function. Informs the JVM that the access to the Utf-8 
	/// characters sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseStringUtfCharsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetArrayLength</c> function. Retrieves the number of elements in a given array.
	/// </summary>
	internal readonly IntPtr GetArrayLengthPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewObjectArray</c> function. Constructs a new array holding objects in <c>class</c> or 
	/// <c>interface</c> elementType.
	/// </summary>
	internal readonly IntPtr NewObjectArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetObjectArrayElement</c> function. Retrieves a local reference of an object into 
	/// the array at given index.
	/// </summary>
	internal readonly IntPtr GetObjectArrayElementPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetObjectArrayElement</c> function. Sets an object as the array element at given index.
	/// </summary>
	internal readonly IntPtr SetObjectArrayElementPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewBooleanArray</c> function. Constructs a new array of <see cref="Boolean"/> elements.
	/// </summary>
	internal readonly IntPtr NewBooleanArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewByteArray</c> function. Constructs a new array of <see cref="SByte"/> elements.
	/// </summary>
	internal readonly IntPtr NewByteArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewCharArray</c> function. Constructs a new array of <see cref="Char"/> elements.
	/// </summary>
	internal readonly IntPtr NewCharArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewShortArray</c> function. Constructs a new array of <see cref="Int16"/> elements.
	/// </summary>
	internal readonly IntPtr NewShortArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewIntArray</c> function. Constructs a new array of <see cref="Int32"/>elements.
	/// </summary>
	internal readonly IntPtr NewIntArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewLongArray</c> function. Constructs a new array of <see cref="Int64"/> elements.
	/// </summary>
	internal readonly IntPtr NewLongArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewFloatArray</c> function. Constructs a new array of <see cref="Single"/> elements.
	/// </summary>
	internal readonly IntPtr NewFloatArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewDoubleArray</c> function. Constructs a new array of <see cref="Double"/> elements.
	/// </summary>
	internal readonly IntPtr NewDoubleArrayPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetBooleanArrayElements</c> function. Returns a pointer to the <see cref="Boolean"/> sequence 
	/// of the array.
	/// </summary>
	internal readonly IntPtr GetBooleanArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetByteArrayElements</c> function. Returns a pointer to the <see cref="SByte"/> sequence 
	/// of the array.
	/// </summary>
	internal readonly IntPtr GetByteArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetCharArrayElements</c> function. Returns a pointer to the <see cref="Char"/> sequence 
	/// of the array.
	/// </summary>
	internal readonly IntPtr GetCharArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetShortArrayElements</c> function. Returns a pointer to the <see cref="Int16"/> sequence 
	/// of the array.
	/// </summary>
	internal readonly IntPtr GetShortArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetIntArrayElements</c> function. Returns a pointer to the <see cref="Int32"/>sequence 
	/// of the array.
	/// </summary>
	internal readonly IntPtr GetIntArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetLongArrayElements</c> function. Returns a pointer to the <see cref="Int64"/> sequence 
	/// of the array.
	/// </summary>
	internal readonly IntPtr GetLongArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetFloatArrayElements</c> function. Returns a pointer to the <see cref="Single"/> sequence 
	/// of the array.
	/// </summary>
	internal readonly IntPtr GetFloatArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetDoubleArrayElements</c> function. Returns a pointer to the <see cref="Double"/> sequence 
	/// of the array.
	/// </summary>
	internal readonly IntPtr GetDoubleArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseBooleanArrayElements</c> function. Informs the JVM that the access to the 
	/// <see cref="Boolean"/> sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseBooleanArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseByteArrayElements</c> function. Informs the JVM that the access to the 
	/// <see cref="SByte"/> sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseByteArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseCharArrayElements</c> function. Informs the JVM that the access to the 
	/// <see cref="Char"/> sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseCharArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseShortArrayElements</c> function. Informs the JVM that the access to the 
	/// <see cref="Int16"/> sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseShortArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseIntArrayElements</c> function. Informs the JVM that the access to the 
	/// <see cref="Int32"/>sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseIntArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseDoubleArrayElements</c> function. Informs the JVM that the access to the 
	/// <see cref="Int64"/> sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseLongArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseFloatArrayElements</c> function. Informs the JVM that the access to the 
	/// <see cref="Single"/> sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseFloatArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseDoubleArrayElements</c> function. Informs the JVM that the access to the 
	/// <see cref="Double"/> sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseDoubleArrayElementsPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetBooleanArrayRegion</c> function. Copies a specified number of <see cref="Boolean"/> 
	/// from the specified offset.
	/// </summary>
	internal readonly IntPtr GetBooleanArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetByteArrayRegion</c> function. Copies a specified number of <see cref="SByte"/> 
	/// from the specified offset.
	/// </summary>
	internal readonly IntPtr GetByteArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetCharArrayRegion</c> function. Copies a specified number of <see cref="Char"/> 
	/// from the specified offset.
	/// </summary>
	internal readonly IntPtr GetCharArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetShortArrayRegion</c> function. Copies a specified number of <see cref="Int16"/> 
	/// from the specified offset.
	/// </summary>
	internal readonly IntPtr GetShortArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetIntArrayRegion</c> function. Copies a specified number of <see cref="Int32"/>
	/// from the specified offset.
	/// </summary>
	internal readonly IntPtr GetIntArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetLongArrayRegion</c> function. Copies a specified number of <see cref="Int64"/> 
	/// from the specified offset.
	/// </summary>
	internal readonly IntPtr GetLongArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetFloatArrayRegion</c> function. Copies a specified number of <see cref="Single"/> 
	/// from the specified offset.
	/// </summary>
	internal readonly IntPtr GetFloatArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetDoubleArrayRegion</c> function. Copies a specified number of <see cref="Double"/> 
	/// from the specified offset.
	/// </summary>
	internal readonly IntPtr GetDoubleArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetBooleanArrayRegion</c> function. Copies back a region of a <see cref="Boolean"/> 
	/// array from a buffer.
	/// </summary>
	internal readonly IntPtr SetBooleanArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetByteArrayRegion</c> function. Copies back a region of a <see cref="SByte"/> 
	/// array from a buffer.
	/// </summary>
	internal readonly IntPtr SetByteArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetCharArrayRegion</c> function. Copies back a region of a <see cref="Char"/> 
	/// array from a buffer.
	/// </summary>
	internal readonly IntPtr SetCharArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetShortArrayRegion</c> function. Copies back a region of a <see cref="Int16"/> 
	/// array from a buffer.
	/// </summary>
	internal readonly IntPtr SetShortArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetIntArrayRegion</c> function. Copies back a region of a <see cref="Int32"/>
	/// array from a buffer.
	/// </summary>
	internal readonly IntPtr SetIntArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetLongArrayRegion</c> function. Copies back a region of a <see cref="Int64"/> 
	/// array from a buffer.
	/// </summary>
	internal readonly IntPtr SetLongArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetFloatArrayRegion</c> function. Copies back a region of a <see cref="Single"/> 
	/// array from a buffer.
	/// </summary>
	internal readonly IntPtr SetFloatArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>SetDoubleArrayRegion</c> function. Copies back a region of a <see cref="Double"/> 
	/// array from a buffer.
	/// </summary>
	internal readonly IntPtr SetDoubleArrayRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>RegisterNatives</c> function. Registers specified native methods implementation for 
	/// given class reference.
	/// </summary>
	internal readonly IntPtr RegisterNativesPointer { get; init; }
	/// <summary>
	/// Pointer to <c>UnregisterNatives</c> function. Unregisters native methods of a class.
	/// </summary>
	internal readonly IntPtr UnregisterNativesPointer { get; init; }
	/// <summary>
	/// Pointer to <c>MonitorEnter</c> function. Enters the monitor associated with the given object.
	/// </summary>
	internal readonly IntPtr MonitorEnterPointer { get; init; }
	/// <summary>
	/// Pointer to <c>MonitorExit</c> function. Exits the monitor associated with the given object.
	/// </summary>
	internal readonly IntPtr MonitorExitPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetJavaVM</c> function. Retrieves a <c>JavaVM</c> pointer to which the current 
	/// thread is attached.
	/// </summary>
	internal readonly IntPtr GetJavaVMPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStringRegion</c> function. Copies a specified number of Unicode characters from the 
	/// specified offset.
	/// </summary>
	internal readonly IntPtr GetStringRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStringUtfRegion</c> function. Translates a specified number of Unicode characters 
	/// to Utf-8 format from the specified offset.
	/// </summary>
	internal readonly IntPtr GetStringUtfRegionPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetPrimitiveArrayCritical</c> function. Returns a pointer to the content of a array 
	/// object.
	/// </summary>
	internal readonly IntPtr GetPrimitiveArrayCriticalPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleasePrimitiveArrayCritical</c> function. Informs the JVM that the access to the 
	/// primitive type sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleasePrimitiveArrayCriticalPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetStringCritical</c> function. Returns a pointer to the contents of a string object.
	/// </summary>
	internal readonly IntPtr GetStringCriticalPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ReleaseStringCritical</c> function. Informs the JVM that the access to the unicode 
	/// characters sequence is not longer needed.
	/// </summary>
	internal readonly IntPtr ReleaseStringCriticalPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewWeakGlobalRef</c> function. Creates a new weak global reference to the object 
	/// referred to by the given reference.
	/// </summary>
	internal readonly IntPtr NewWeakGlobalRefPointer { get; init; }
	/// <summary>
	/// Pointer to <c>DeleteWeakGlobalRef</c> function. Creates a new weak global reference to the object 
	/// referred to by the given reference.
	/// </summary>
	internal readonly IntPtr DeleteWeakGlobalRefPointer { get; init; }
	/// <summary>
	/// Pointer to <c>ExceptionCheck</c> function. Indicates whether an exception has been thrown.
	/// </summary>
	internal readonly IntPtr ExceptionCheckPointer { get; init; }
	/// <summary>
	/// Pointer to <c>NewDirectByteBuffer</c> function. Allocates and returns a direct 
	/// <c>java.nio.ByteBuffer</c> referring to the block of memory given the memory address and 
	/// capacity bytes.
	/// </summary>
	internal readonly IntPtr NewDirectByteBufferPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetDirectBufferAddress</c> function. Fetches and returns the starting address of 
	/// the memory region referenced by the given direct <c>java.nio.Buffer</c>.
	/// </summary>
	internal readonly IntPtr GetDirectBufferAddressPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetDirectBufferCapacity</c> function. Fetches and returns the capacity of the 
	/// memory region referenced by the given direct <c>java.nio.Buffer</c>. 
	/// </summary>
	internal readonly IntPtr GetDirectBufferCapacityPointer { get; init; }
	/// <summary>
	/// Pointer to <c>GetObjectRefType</c> function. Returns the type of the give reference.
	/// </summary>
	internal readonly IntPtr GetObjectRefTypePointer { get; init; }

    #region Overrided Methods
    /// <inheritdoc/>
    public override String ToString() => INative.ToString(this);
    #endregion
}