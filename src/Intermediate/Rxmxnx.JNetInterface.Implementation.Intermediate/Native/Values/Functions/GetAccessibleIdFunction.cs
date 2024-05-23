namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to get identifier Java accessible objects through JNI.
/// </summary>
/// <typeparam name="TAccessible">Type of accessible identifier.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct GetAccessibleIdFunction<TAccessible>
	where TAccessible : unmanaged, IAccessibleIdentifierType<TAccessible>
{
	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;ID</c> function.
	/// </summary>
	/// <remarks>The accessible object is determined by its name and signature.</remarks>
	public readonly delegate* unmanaged<JEnvironmentRef, JClassLocalRef, ReadOnlyValPtr<Byte>, ReadOnlyValPtr<Byte>,
		TAccessible> GetId;
}