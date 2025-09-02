namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java object reference through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ReferenceFunctionSet
{
	/// <summary>
	/// Pointer to <c>PushLocalFrame</c> function.
	/// Creates a new local reference frame, in which at least a given number of local references can be created.
	/// </summary>
	/// <remarks>
	/// Note that local references already created in previous local frames are still valid in the current local frame.
	/// </remarks>
	private readonly delegate* unmanaged<IntPtr, Int32, Int32> _pushLocalFrame;
	/// <summary>
	/// Pointer to <c>PopLocalFrame</c> function.
	/// Pops off the current local reference frame, frees all the local references, and returns a
	/// local reference in the previous local reference frame for the given result object.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, IntPtr> _popLocalFrame;

	/// <summary>
	/// Pointer to <c>NewGlobalRef</c> function.
	/// Creates a new global reference to the object referred.
	/// </summary>
	/// <remarks>Global references must be explicitly disposed of by calling <c>DeleteGlobalRef()</c>.</remarks>
	public readonly NewRefFunction<JGlobalRef> NewGlobalRef;
	/// <summary>
	/// Pointer to <c>DeleteGlobalRef</c> function.
	/// Deletes the given global reference.
	/// </summary>
	public readonly DeleteRefFunction<JGlobalRef> DeleteGlobalRef;
	/// <summary>
	/// Pointer to <c>DeleteLocalRef</c> function.
	/// Deletes the given local reference.
	/// </summary>
	public readonly DeleteRefFunction<JObjectLocalRef> DeleteLocalRef;
	/// <summary>
	/// Pointer to <c>IsSameObject</c> function.
	/// Tests whether two references to refer to the same Java object.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, IntPtr, Byte> _isSameObject;
	/// <summary>
	/// Pointer to <c>NewLocalRef</c> function.
	/// Creates a new local reference that refers to the given object.
	/// </summary>
	public readonly NewRefFunction<JObjectLocalRef> NewLocalRef;
	/// <summary>
	/// Pointer to <c>EnsureLocalCapacity</c> function.
	/// Ensures that at least a given number of local references can be created in the current thread.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, Int32, Int32> _ensureLocalCapacity;

	/// <summary>
	/// <c>PushLocalFrame</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult PushLocalFrame(JEnvironmentRef envRef, Int32 capacity)
		=> (JResult)this._pushLocalFrame(envRef.Pointer, capacity);
	/// <summary>
	/// <c>PopLocalFrame</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef PopLocalFrame(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> new(this._popLocalFrame(envRef.Pointer, localRef.Pointer));
	/// <summary>
	/// <c>EnsureLocalCapacity</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JBoolean IsSameObject(JEnvironmentRef envRef, JObjectLocalRef localRef0, JObjectLocalRef localRef1)
		=> this._isSameObject(envRef.Pointer, localRef0.Pointer, localRef1.Pointer) == JBoolean.TrueValue;
	/// <summary>
	/// <c>EnsureLocalCapacity</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult EnsureLocalCapacity(JEnvironmentRef envRef, Int32 capacity)
		=> (JResult)this._ensureLocalCapacity(envRef.Pointer, capacity);
}