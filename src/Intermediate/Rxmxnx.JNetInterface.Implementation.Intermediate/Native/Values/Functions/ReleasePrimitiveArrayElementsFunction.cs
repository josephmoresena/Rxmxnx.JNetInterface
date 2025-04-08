namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to release the pointer to elements Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ReleasePrimitiveArrayElementsFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType
{
	/// <summary>
	/// Pointer to <c>Release&lt;PrimitiveType&gt;Elements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
	private readonly delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, void*, JReleaseMode, void> _ptr;

	/// <summary>
	/// Pointer to <c>Release&lt;PrimitiveType&gt;Elements</c> function.
	/// Informs the <c>VM</c> that the native code no longer needs access to array elements.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Release(JEnvironmentRef envRef, TArrayRef arrayRef, ReadOnlyValPtr<TPrimitiveType> elements,
		JReleaseMode mode)
		=> this._ptr(envRef, arrayRef.ArrayValue, elements, mode);
}