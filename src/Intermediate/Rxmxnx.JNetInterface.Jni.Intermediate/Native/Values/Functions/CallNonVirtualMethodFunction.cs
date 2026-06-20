namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java non-virtual methods through JNI.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct CallNonVirtualMethodFunction : ICallNonvirtualMethodFunction
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
#pragma warning disable CS0169
	private readonly MethodOffset _offset;
#pragma warning restore CS0169
	/// <summary>
	/// Caller <c>A</c> function.
	/// </summary>
	private readonly ICallNonvirtualMethodFunction.CallMethodFunction _function;

	/// <summary>
	/// <c>CallNonvirtualVoidMethodA</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Call(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
#if !ANDROID
		if (SystemInfo.IsWindows)
		{
			this._function.Windows.Void(envRef, localRef, classRef, methodId, args);
			return;
		}
#endif
		this._function.Unix.Void(envRef, localRef, classRef, methodId, args);
	}
}