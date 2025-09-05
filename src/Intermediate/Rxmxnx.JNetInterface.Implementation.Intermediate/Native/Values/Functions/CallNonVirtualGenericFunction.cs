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
	where TResult : unmanaged, INativeType
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
	{
		TResult result = default;
		switch (TResult.Type)
		{
			case JNativeType.JBoolean when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Byte>(ref result) =
					this._function.Windows.Boolean(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JBoolean:
				Unsafe.As<TResult, Byte>(ref result) =
					this._function.Unix.Boolean(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JByte when OperatingSystem.IsWindows():
				Unsafe.As<TResult, SByte>(ref result) =
					this._function.Windows.Byte(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JByte:
				Unsafe.As<TResult, SByte>(ref result) =
					this._function.Unix.Byte(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JChar when OperatingSystem.IsWindows():
				Unsafe.As<TResult, UInt16>(ref result) =
					this._function.Windows.Char(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JChar:
				Unsafe.As<TResult, UInt16>(ref result) =
					this._function.Unix.Char(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JDouble when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Double>(ref result) =
					this._function.Windows.Double(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JDouble:
				Unsafe.As<TResult, Double>(ref result) =
					this._function.Unix.Double(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JFloat when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Single>(ref result) =
					this._function.Windows.Float(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JFloat:
				Unsafe.As<TResult, Single>(ref result) =
					this._function.Unix.Float(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JInt when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Int32>(ref result) =
					this._function.Windows.Int(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JInt:
				Unsafe.As<TResult, Int32>(ref result) =
					this._function.Unix.Int(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JLong when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Int64>(ref result) =
					this._function.Windows.Long(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JLong:
				Unsafe.As<TResult, Int64>(ref result) =
					this._function.Unix.Long(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JShort when OperatingSystem.IsWindows():
				Unsafe.As<TResult, Int16>(ref result) =
					this._function.Windows.Short(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JShort:
				Unsafe.As<TResult, Int16>(ref result) =
					this._function.Unix.Short(envRef, localRef, classRef, methodId, args);
				break;
			default:
				Unsafe.As<TResult, JObjectLocalRef>(ref result) = OperatingSystem.IsWindows() ?
					this._function.Windows.Object(envRef, localRef, classRef, methodId, args) :
					this._function.Unix.Object(envRef, localRef, classRef, methodId, args);
				break;
		}
		return result;
	}
}