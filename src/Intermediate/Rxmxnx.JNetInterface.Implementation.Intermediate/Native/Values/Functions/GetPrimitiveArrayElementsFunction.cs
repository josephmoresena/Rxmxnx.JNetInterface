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
internal readonly unsafe struct
	GetPrimitiveArrayElementsFunction<TPrimitiveType, TArrayRef> : IGetPrimitiveArrayElementsFunction
	where TPrimitiveType : unmanaged, INativeType, IPrimitiveType<TPrimitiveType>
	where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType
{
	/// <summary>
	/// Pointer to <c>Get&lt;PrimitiveType&gt;Elements</c> function.
	/// Returns the body of the primitive array.
	/// </summary>
	private readonly IGetPrimitiveArrayElementsFunction.GetPrimitiveArrayElementsFunction _function;

	/// <summary>
	/// <c>Get&lt;PrimitiveType&gt;Elements</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ValPtr<TPrimitiveType> Get(JEnvironmentRef envRef, TArrayRef arrayRef, out JBoolean isCopy)
	{
		fixed (JBoolean* isCopyPtr = &isCopy)
		{
			return (ValPtr<TPrimitiveType>)(OperatingSystem.IsWindows() ?
				this._function.Windows(envRef, arrayRef.ArrayValue, isCopyPtr) :
				this._function.Unix(envRef, arrayRef.ArrayValue, isCopyPtr));
		}
	}
}