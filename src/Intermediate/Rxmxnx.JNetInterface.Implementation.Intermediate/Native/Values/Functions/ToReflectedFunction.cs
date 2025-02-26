namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to convert an accessible identifier to Java accessible object through JNI.
/// </summary>
/// <typeparam name="TAccessible">Type of accessible identifier.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct ToReflectedFunction<TAccessible>
	where TAccessible : unmanaged, IAccessibleIdentifierType
{
	/// <summary>
	/// Pointer to <c>ToReflected<typeparamref name="TAccessible"/></c> function.
	/// Converts a <typeparamref name="TAccessible"/> to <c>java.lang.reflect</c> object.
	/// </summary>
	private readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, IntPtr, JBoolean, JObjectLocalRef> _ptr;

	/// <summary>
	/// Pointer to <c>ToReflected<typeparamref name="TAccessible"/></c> function.
	/// Converts a <typeparamref name="TAccessible"/> to <c>java.lang.reflect</c> object.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef ToReflected(JEnvironmentRef envRef, JClassLocalRef classRef, TAccessible accessibleId,
		JBoolean isStatic)
		=> this._ptr(envRef, classRef, accessibleId.Pointer, isStatic);
}