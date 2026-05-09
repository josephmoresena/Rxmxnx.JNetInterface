// ReSharper disable ConvertIfStatementToReturnStatement

namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to manipulate Java object reference through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe partial struct ReferenceFunctionSet
{
	/// <summary>
	/// Pointers to <c>PushLocalFrame</c> and <c>PopLocalFrame</c>.
	/// </summary>
	private readonly FrameFunctionSet _frameFunctions;
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
	private readonly IsSameObjectPtr _isSameObject;
	/// <summary>
	/// Pointer to <c>NewLocalRef</c> function.
	/// Creates a new local reference that refers to the given object.
	/// </summary>
	public readonly NewRefFunction<JObjectLocalRef> NewLocalRef;
	/// <summary>
	/// Pointer to <c>EnsureLocalCapacity</c> function.
	/// Ensures that at least a given number of local references can be created in the current thread.
	/// </summary>
	private readonly EnsureLocalCapacityPtr _ensureLocalCapacity;

	/// <summary>
	/// <c>PushLocalFrame</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult PushLocalFrame(JEnvironmentRef envRef, Int32 capacity)
		=> this._frameFunctions.PushLocalFrame(envRef, capacity);
	/// <summary>
	/// <c>PopLocalFrame</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef PopLocalFrame(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> this._frameFunctions.PopLocalFrame(envRef, localRef);
	/// <summary>
	/// <c>IsSameObject</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JBoolean IsSameObject(JEnvironmentRef envRef, JObjectLocalRef localRef0, JObjectLocalRef localRef1)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._isSameObject.Windows(envRef, localRef0, localRef1);
#endif
		return this._isSameObject.Unix(envRef, localRef0, localRef1);
	}
	/// <summary>
	/// <c>EnsureLocalCapacity</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JResult EnsureLocalCapacity(JEnvironmentRef envRef, Int32 capacity)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._ensureLocalCapacity.Windows(envRef, capacity);
#endif
		return this._ensureLocalCapacity.Unix(envRef, capacity);
	}
}