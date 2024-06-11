namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to convert Java accessible object to accessible identifier through JNI.
/// </summary>
/// <typeparam name="TAccessible">Type of accessible identifier.</typeparam>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct FromReflectedFunction<TAccessible>
	where TAccessible : unmanaged, IAccessibleIdentifierType
{
	/// <summary>
	/// Pointer to <c>FromReflected<typeparamref name="TAccessible"/></c> function.
	/// Converts a <c>java.lang.reflect</c> object to a <typeparamref name="TAccessible"/>.
	/// </summary>
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, TAccessible> FromReflected;
}