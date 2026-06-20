namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java non-virtual functions through JNI.
/// </summary>
/// <typeparam name="TResult">Type of return function.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct CallNonVirtualGenericFunction<TResult> : ICallNonvirtualMethodFunction
	where TResult : unmanaged, INativeDataType<TResult>
{
	/// <summary>
	/// Internal reserved entries.
	/// </summary>
	private readonly MethodOffset _offset;
#pragma warning restore CS0169
	/// <summary>
	/// Caller <c>A</c> function.
	/// </summary>
	private readonly ICallNonvirtualMethodFunction.CallMethodFunction _function;

	/// <summary>
	/// <c>CallNonvirtual&lt;Type&gt;MethodA</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Call(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
		=> TResult.Type switch
		{
#if !ANDROID
			JNativeType.JBoolean when SystemInfo.IsWindows => this._function.Windows.Boolean(
				envRef, localRef, classRef, methodId, args),
			JNativeType.JByte when SystemInfo.IsWindows => this._function.Windows.Byte(
				envRef, localRef, classRef, methodId, args),
			JNativeType.JChar when SystemInfo.IsWindows => this._function.Windows.Char(
				envRef, localRef, classRef, methodId, args),
			JNativeType.JDouble when SystemInfo.IsWindows => this._function.Windows.Double(
				envRef, localRef, classRef, methodId, args),
			JNativeType.JFloat when SystemInfo.IsWindows => this._function.Windows.Float(
				envRef, localRef, classRef, methodId, args),
			JNativeType.JInt when SystemInfo.IsWindows => this._function.Windows.Int(
				envRef, localRef, classRef, methodId, args),
			JNativeType.JLong when SystemInfo.IsWindows => this._function.Windows.Long(
				envRef, localRef, classRef, methodId, args),
			JNativeType.JShort when SystemInfo.IsWindows => this._function.Windows.Short(
				envRef, localRef, classRef, methodId, args),
			_ when SystemInfo.IsWindows => this._function.Windows.Object(envRef, localRef, classRef, methodId, args),
#endif
			JNativeType.JBoolean => this._function.Unix.Boolean(envRef, localRef, classRef, methodId, args),
			JNativeType.JByte => this._function.Unix.Byte(envRef, localRef, classRef, methodId, args),
			JNativeType.JChar => this._function.Unix.Char(envRef, localRef, classRef, methodId, args),
			JNativeType.JDouble => this._function.Unix.Double(envRef, localRef, classRef, methodId, args),
			JNativeType.JFloat => this._function.Unix.Float(envRef, localRef, classRef, methodId, args),
			JNativeType.JInt => this._function.Unix.Int(envRef, localRef, classRef, methodId, args),
			JNativeType.JLong => this._function.Unix.Long(envRef, localRef, classRef, methodId, args),
			JNativeType.JShort => this._function.Unix.Short(envRef, localRef, classRef, methodId, args),
			_ => this._function.Unix.Object(envRef, localRef, classRef, methodId, args),
		};
}