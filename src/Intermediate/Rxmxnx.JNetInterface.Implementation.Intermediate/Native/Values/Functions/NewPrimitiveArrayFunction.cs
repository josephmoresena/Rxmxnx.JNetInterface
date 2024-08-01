namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to create Java primitive array through JNI.
/// </summary>
/// <typeparam name="TArrayRef">Type of array reference.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct NewPrimitiveArrayFunction<TArrayRef>
	where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType
{
	/// <summary>
	/// Pointer to <c>New&lt;PrimitiveType&gt;Array</c> function.
	/// Constructs a new primitive array object.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, Int32, TArrayRef> NewArray;
}