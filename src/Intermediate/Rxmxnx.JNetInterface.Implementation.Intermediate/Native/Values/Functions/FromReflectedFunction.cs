namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to convert Java accessible object to accessible identifier through JNI.
/// </summary>
/// <typeparam name="TAccessible">Type of accessible identifier.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct FromReflectedFunction<TAccessible>
	where TAccessible : unmanaged, IAccessibleIdentifierType<TAccessible>
{
	/// <summary>
	/// Pointer to <c>FromReflected<typeparamref name="TAccessible"/></c> function.
	/// Converts a <c>java.lang.reflect</c> object to a <typeparamref name="TAccessible"/>.
	/// </summary>
	private readonly delegate* unmanaged<IntPtr, IntPtr, IntPtr> _ptr;

	/// <summary>
	/// Pointer to <c>FromReflected<typeparamref name="TAccessible"/></c> function.
	/// Converts a <c>java.lang.reflect</c> object to a <typeparamref name="TAccessible"/>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TAccessible FromReflected(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> TAccessible.New(this._ptr(envRef.Pointer, localRef.Pointer));
}