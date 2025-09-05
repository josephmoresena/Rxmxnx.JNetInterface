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
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TResult : unmanaged, INativeType
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
	{
		TResult result = default;
		switch (TResult.Type)
		{
			case JNativeType.JBoolean when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Byte>(ref result) =
					this._function.Windows.Boolean(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JBoolean:
				Unsafe.As<TResult, Byte>(ref result) =
					this._function.Unix.Boolean(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JByte when OperatingSystem.IsWindows():
				Unsafe.As<TResult, SByte>(ref result) =
					this._function.Windows.Byte(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JByte:
				Unsafe.As<TResult, SByte>(ref result) =
					this._function.Unix.Byte(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JChar when OperatingSystem.IsWindows():
				Unsafe.As<TResult, UInt16>(ref result) =
					this._function.Windows.Char(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JChar:
				Unsafe.As<TResult, UInt16>(ref result) =
					this._function.Unix.Char(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JDouble when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Double>(ref result) =
					this._function.Windows.Double(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JDouble:
				Unsafe.As<TResult, Double>(ref result) =
					this._function.Unix.Double(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JFloat when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Single>(ref result) =
					this._function.Windows.Float(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JFloat:
				Unsafe.As<TResult, Single>(ref result) =
					this._function.Unix.Float(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JInt when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Int32>(ref result) =
					this._function.Windows.Int(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JInt:
				Unsafe.As<TResult, Int32>(ref result) =
					this._function.Unix.Int(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JLong when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Int64>(ref result) =
					this._function.Windows.Long(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JLong:
				Unsafe.As<TResult, Int64>(ref result) =
					this._function.Unix.Long(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JShort when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Int16>(ref result) =
					this._function.Windows.Short(envRef, receiver.Value.Pointer, methodId, args);
				break;
			case JNativeType.JShort:
				Unsafe.As<TResult, Int16>(ref result) =
					this._function.Unix.Short(envRef, receiver.Value.Pointer, methodId, args);
				break;
			default:
				Unsafe.As<TResult, JObjectLocalRef>(ref result) = OperatingSystem.IsWindows() ?
					this._function.Windows.Object(envRef, receiver.Value.Pointer, methodId, args) :
					this._function.Unix.Object(envRef, receiver.Value.Pointer, methodId, args);
				break;
		}
		return result;
	}
}