namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java classes through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct ClassFunctionSet
{
	/// <summary>
	/// Pointer to <c>DefineClass</c> function.
	/// Loads a class from a buffer of raw class data.
	/// The buffer containing the raw class data is not referenced by the <c>VM</c> after the
	/// <c>DefineClass</c> call returns, and it may be discarded if desired.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, Byte*, JObjectLocalRef, IntPtr, Int32, JClassLocalRef>
		DefineClass;
	/// <summary>
	/// Pointer to <c>FindClass</c> function.
	/// Loads a locally-defined class with the specified name.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, ReadOnlyValPtr<Byte>, JClassLocalRef> FindClass;

	/// <summary>
	/// Pointer to <c>FromReflectedMethod</c> function.
	/// Converts a <c>java.lang.reflect.Method</c> or <c>java.lang.reflect.Constructor</c> object to a method ID.
	/// </summary>
	public readonly FromReflectedFunction<JMethodId> FromReflectedMethod;
	/// <summary>
	/// Pointer to <c>FromReflectedField</c> function.
	/// Converts a <c>java.lang.reflect.Field</c> to a field ID.
	/// </summary>
	public readonly FromReflectedFunction<JFieldId> FromReflectedField;
	/// <summary>
	/// Pointer to <c>ToReflectedMethod</c> function.
	/// Converts a method ID derived from a class to a <c>java.lang.reflect.Method</c> or
	/// <c>java.lang.reflect.Constructor</c> object.
	/// </summary>
	public readonly ToReflectedFunction<JMethodId> ToReflectedMethod;

	/// <summary>
	/// Pointer to <c>GetSuperclass</c> function.
	/// This function returns the object that represents the superclass of the specified class.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JClassLocalRef> GetSuperclass;
	/// <summary>
	/// Pointer to <c>IsAssignableFrom</c> function.
	/// Determines whether an object of the first class can be safely cast to the second class.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JClassLocalRef, JBoolean> IsAssignableFrom;

	/// <summary>
	/// Pointer to <c>ToReflectedField</c> function.
	/// Converts a field ID derived from a class to a <c>java.lang.reflect.Field</c> object.
	/// </summary>
	public readonly ToReflectedFunction<JFieldId> ToReflectedField;
}