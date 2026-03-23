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
internal readonly unsafe struct GetAccessibleIdFunction<TAccessible> : IGetAccessibleIdFunction
	where TAccessible : unmanaged, IAccessibleIdentifierType, INativePointerType<TAccessible>
{
	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;ID</c> function.
	/// </summary>
	/// <remarks>The accessible object is determined by its name and signature.</remarks>
	private readonly IGetAccessibleIdFunction.GetAccessibleIdFunction _function;

	/// <summary>
	/// <c>Get&lt;type&gt;ID</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TAccessible GetId(JEnvironmentRef envRef, JClassLocalRef localRef, Byte* name, Byte* descriptor)
	{
		IntPtr result = SystemInfo.IsWindows ?
			this._function.Windows(envRef, localRef, name, descriptor) :
			this._function.Unix(envRef, localRef, name, descriptor);
		return TAccessible.New(result);
	}
}