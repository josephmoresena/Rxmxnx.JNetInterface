namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java functions through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver function.</typeparam>
/// <typeparam name="TResult">Type of return function.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct CallGenericFunction<TReceiver, TResult> : ICallMethodFunction
	where TReceiver : unmanaged, INativePointerType where TResult : unmanaged, INativeDataType<TResult>
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
	private readonly MethodOffset _offset;
	/// <summary>
	/// Caller <c>A</c> function.
	/// </summary>
	private readonly ICallMethodFunction.CallMethodFunction _function;

	/// <summary>
	/// <c>Call&lt;Type&gt;MethodA</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Call(JEnvironmentRef envRef, TReceiver receiver, JMethodId methodId, JValue* args)
		=> TResult.Type switch
		{
#if !ANDROID
			JNativeType.JBoolean when SystemInfo.IsWindows => this._function.Windows.Boolean(
				envRef, receiver.Pointer, methodId, args),
			JNativeType.JByte when SystemInfo.IsWindows => this._function.Windows.Byte(
				envRef, receiver.Pointer, methodId, args),
			JNativeType.JChar when SystemInfo.IsWindows => this._function.Windows.Char(
				envRef, receiver.Pointer, methodId, args),
			JNativeType.JDouble when SystemInfo.IsWindows => this._function.Windows.Double(
				envRef, receiver.Pointer, methodId, args),
			JNativeType.JFloat when SystemInfo.IsWindows => this._function.Windows.Float(
				envRef, receiver.Pointer, methodId, args),
			JNativeType.JInt when SystemInfo.IsWindows => this._function.Windows.Int(
				envRef, receiver.Pointer, methodId, args),
			JNativeType.JLong when SystemInfo.IsWindows => this._function.Windows.Long(
				envRef, receiver.Pointer, methodId, args),
			JNativeType.JShort when SystemInfo.IsWindows => this._function.Windows.Short(
				envRef, receiver.Pointer, methodId, args),
			_ when SystemInfo.IsWindows => this._function.Windows.Object(envRef, receiver.Pointer, methodId, args),
#endif
			JNativeType.JBoolean => this._function.Unix.Boolean(envRef, receiver.Pointer, methodId, args),
			JNativeType.JByte => this._function.Unix.Byte(envRef, receiver.Pointer, methodId, args),
			JNativeType.JChar => this._function.Unix.Char(envRef, receiver.Pointer, methodId, args),
			JNativeType.JDouble => this._function.Unix.Double(envRef, receiver.Pointer, methodId, args),
			JNativeType.JFloat => this._function.Unix.Float(envRef, receiver.Pointer, methodId, args),
			JNativeType.JInt => this._function.Unix.Int(envRef, receiver.Pointer, methodId, args),
			JNativeType.JLong => this._function.Unix.Long(envRef, receiver.Pointer, methodId, args),
			JNativeType.JShort => this._function.Unix.Short(envRef, receiver.Pointer, methodId, args),
			_ => this._function.Unix.Object(envRef, receiver.Pointer, methodId, args),
		};
}