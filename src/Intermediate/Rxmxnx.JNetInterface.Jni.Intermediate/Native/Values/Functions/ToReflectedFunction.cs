// ReSharper disable ConvertIfStatementToReturnStatement

namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to convert an accessible identifier to Java accessible object through JNI.
/// </summary>
/// <typeparam name="TAccessible">Type of accessible identifier.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct ToReflectedFunction<TAccessible> : IToReflectedFunction
	where TAccessible : unmanaged, IAccessibleIdentifierType, INativePointerType<TAccessible>
{
	/// <summary>
	/// Pointer to <c>ToReflected<typeparamref name="TAccessible"/></c> function.
	/// Converts a <typeparamref name="TAccessible"/> to <c>java.lang.reflect</c> object.
	/// </summary>
	private readonly IToReflectedFunction.ToReflectedFunction _function;

	/// <summary>
	/// Pointer to <c>ToReflected<typeparamref name="TAccessible"/></c> function.
	/// Converts a <typeparamref name="TAccessible"/> to <c>java.lang.reflect</c> object.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JObjectLocalRef ToReflected(JEnvironmentRef envRef, JClassLocalRef classRef, TAccessible accessibleId,
		JBoolean isStatic)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
			return this._function.Windows(envRef, classRef, accessibleId.Pointer, isStatic);
#endif
		return this._function.Unix(envRef, classRef, accessibleId.Pointer, isStatic);
	}
}