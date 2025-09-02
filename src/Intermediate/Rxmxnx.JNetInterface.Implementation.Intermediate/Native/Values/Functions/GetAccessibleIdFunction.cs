namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to get identifier Java accessible objects through JNI.
/// </summary>
/// <typeparam name="TAccessible">Type of accessible identifier.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct GetAccessibleIdFunction<TAccessible>
	where TAccessible : unmanaged, IAccessibleIdentifierType<TAccessible>
{
	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;ID</c> function.
	/// </summary>
	/// <remarks>The accessible object is determined by its name and signature.</remarks>
	private readonly delegate* unmanaged<IntPtr, IntPtr, Byte*, Byte*, IntPtr> _ptr;

	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;ID</c> function.
	/// </summary>
	/// <remarks>The accessible object is determined by its name and signature.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TAccessible GetId(JEnvironmentRef envRef, JClassLocalRef localRef, Byte* name, Byte* descriptor)
		=> TAccessible.New(this._ptr(envRef.Pointer, localRef.Pointer, name, descriptor));
}