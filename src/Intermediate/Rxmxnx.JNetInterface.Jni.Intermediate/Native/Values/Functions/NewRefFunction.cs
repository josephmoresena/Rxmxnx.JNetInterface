namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to create new object reference through JNI.
/// </summary>
/// <typeparam name="TReference">Type of object reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct NewRefFunction<TReference> : INewRefFunction
	where TReference : unmanaged, INativeReferenceType, INativePointerType<TReference>
{
	/// <summary>
	/// Pointer to <c>New<typeparamref name="TReference"/>Ref</c> function.
	/// </summary>
	private readonly INewRefFunction.NewRefFunction _function;

	/// <summary>
	/// Creates a new <typeparamref name="TReference"/> reference to the object referred.
	/// </summary>
	/// <remarks>
	/// Created references must be explicitly disposed of by calling
	/// <c>Delete<typeparamref name="TReference"/>Ref()</c>.
	/// </remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TReference NewRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
#if !ANDROID
			if (SystemInfo.IsWindows)
			{
				IntPtr resultW = this._function.Windows(envRef, localRef);
				return TReference.New(resultW);
			}
#endif
			IntPtr result = this._function.Unix(envRef, localRef);
			return TReference.New(result);
		}
		finally
		{
#if !PACKAGE
			JniCallBoundary.ThrowIfPending();
#endif
		}
	}
}