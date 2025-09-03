namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to get a pointer to elements Java primitive array through JNI.
/// </summary>
/// <typeparam name="TPrimitiveType">Type of primitive value.</typeparam>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct GetPrimitiveArrayElementsFunction<TPrimitiveType, TArrayRef>
	where TPrimitiveType : unmanaged, INativeType, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType
{
	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;Elements</c> function.
	/// Returns the body of the primitive array.
	/// </summary>
	private readonly delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, out JBoolean, void*> _ptr;

	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;Elements</c> function.
	/// Returns the body of the primitive array.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ValPtr<TPrimitiveType> Get(JEnvironmentRef envRef, TArrayRef arrayRef, out JBoolean isCopy)
		=> (TPrimitiveType*)this._ptr(envRef, arrayRef.ArrayValue, out isCopy);
}