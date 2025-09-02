namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java objects through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ObjectFunctionSet
{
	/// <summary>
	/// Pointer to <c>AllocObject</c> function.
	/// Allocates a new Java object without invoking any of the constructors for the object.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, IntPtr> _allocObject;
	/// <summary>
	/// Pointers to <c>NewObject</c> functions.
	/// Constructs a new Java object.
	/// </summary>
	public readonly CallGenericFunction<JClassLocalRef, JObjectLocalRef> NewObject;

	/// <summary>
	/// Pointer to <c>GetObjectClass</c> function.
	/// Returns the class of an object.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, IntPtr> _getObjectClass;
	/// <summary>
	/// Pointer to <c>IsInstanceOf</c> function.
	/// Tests whether an object is an instance of a class.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, IntPtr, Byte> _isInstanceOf;

	/// <summary>
	/// <c>AllocObject</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef AllocObject(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> new(this._allocObject(envRef.Pointer, classRef.Pointer));
	/// <summary>
	/// <c>GetObjectClass</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JClassLocalRef GetObjectClass(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> new(this._getObjectClass(envRef.Pointer, localRef.Pointer));
	/// <summary>
	/// <c>IsInstanceOf</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JBoolean IsInstanceOf(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef)
		=> this._isInstanceOf(envRef.Pointer, localRef.Pointer, classRef.Pointer) == JBoolean.TrueValue;
}