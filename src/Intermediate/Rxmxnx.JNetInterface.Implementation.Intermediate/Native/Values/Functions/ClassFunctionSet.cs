namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java classes through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe partial struct ClassFunctionSet
{
	/// <summary>
	/// Pointer to <c>DefineClass</c> function.
	/// Loads a class from a buffer of raw class data.
	/// The buffer containing the raw class data is not referenced by the <c>VM</c> after the
	/// <c>DefineClass</c> call returns, and it may be discarded if desired.
	/// </summary>
	private readonly DefineClassPtr _defineClass;
	/// <summary>
	/// Pointer to <c>FindClass</c> function.
	/// Loads a locally-defined class with the specified name.
	/// </summary>
	private readonly FindClassPtr _findClass;

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
	private readonly GetSuperclassPtr _getSuperclass;
	/// <summary>
	/// Pointer to <c>IsAssignableFrom</c> function.
	/// Determines whether an object of the first class can be safely cast to the second class.
	/// </summary>
	private readonly IsAssignableFromPtr _isAssignableFrom;

	/// <summary>
	/// Pointer to <c>ToReflectedField</c> function.
	/// Converts a field ID derived from a class to a <c>java.lang.reflect.Field</c> object.
	/// </summary>
	public readonly ToReflectedFunction<JFieldId> ToReflectedField;

	/// <summary>
	/// <c>DefineClass</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JClassLocalRef DefineClass(JEnvironmentRef envRef, Byte* className, JObjectLocalRef localRef, Byte* buffPtr,
		Int32 buffSize)
		=> OperatingSystem.IsWindows() ?
			this._defineClass.Windows(envRef, className, localRef, buffPtr, buffSize) :
			this._defineClass.Unix(envRef, className, localRef, buffPtr, buffSize);
	/// <summary>
	/// <c>FindClass</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JClassLocalRef FindClass(JEnvironmentRef envRef, Byte* className)
		=> OperatingSystem.IsWindows() ?
			this._findClass.Windows(envRef, className) :
			this._findClass.Unix(envRef, className);
	/// <summary>
	/// <c>GetSuperClass</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JClassLocalRef GetSuperclass(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> OperatingSystem.IsWindows() ?
			this._getSuperclass.Windows(envRef, classRef) :
			this._getSuperclass.Unix(envRef, classRef);
	/// <summary>
	/// <c>GetSuperClass</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JBoolean IsAssignableFrom(JEnvironmentRef envRef, JClassLocalRef classRef0, JClassLocalRef classRef1)
		=> OperatingSystem.IsWindows() ?
			this._isAssignableFrom.Windows(envRef, classRef0, classRef1) :
			this._isAssignableFrom.Unix(envRef, classRef0, classRef1);
}