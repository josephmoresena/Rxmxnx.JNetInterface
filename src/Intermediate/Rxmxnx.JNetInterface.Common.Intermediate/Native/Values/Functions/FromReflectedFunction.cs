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
internal readonly unsafe struct FromReflectedFunction<TAccessible> : IFromReflectedFunction
	where TAccessible : unmanaged, IAccessibleIdentifierType, INativePointerType<TAccessible>
{
	/// <summary>
	/// Pointer to <c>FromReflected<typeparamref name="TAccessible"/></c> function.
	/// Converts a <c>java.lang.reflect</c> object to a <typeparamref name="TAccessible"/>.
	/// </summary>
	private readonly IFromReflectedFunction.FromReflectedFunction _function;

	/// <summary>
	/// <c>FromReflected<typeparamref name="TAccessible"/></c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TAccessible FromReflected(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		IntPtr result = SystemInfo.IsWindows ?
			this._function.Windows(envRef, localRef) :
			this._function.Unix(envRef, localRef);
		return TAccessible.New(result);
	}
}