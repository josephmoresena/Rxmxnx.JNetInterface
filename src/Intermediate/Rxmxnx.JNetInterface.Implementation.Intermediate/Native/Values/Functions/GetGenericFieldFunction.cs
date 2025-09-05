namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to Get the value of Java fields through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver field.</typeparam>
/// <typeparam name="TField">Type of field.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct GetGenericFieldFunction<TReceiver, TField> : IGetFieldFunction
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TField : unmanaged, INativeType
{
	/// <summary>
	/// Pointer to <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	private readonly IGetFieldFunction.GetFieldFunction _function;

	/// <summary>
	/// <c>Get&lt;type&gt;Field</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TField Get(JEnvironmentRef envRef, TReceiver receiver, JFieldId fieldId)
	{
		TField result = default;
		switch (TField.Type)
		{
			case JNativeType.JBoolean when OperatingSystem.IsWindows():
				Unsafe.As<TField, Byte>(ref result) =
					this._function.Windows.Boolean(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JBoolean:
				Unsafe.As<TField, Byte>(ref result) =
					this._function.Unix.Boolean(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JByte when OperatingSystem.IsWindows():
				Unsafe.As<TField, SByte>(ref result) =
					this._function.Windows.Byte(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JByte:
				Unsafe.As<TField, SByte>(ref result) =
					this._function.Unix.Byte(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JChar when OperatingSystem.IsWindows():
				Unsafe.As<TField, UInt16>(ref result) =
					this._function.Windows.Char(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JChar:
				Unsafe.As<TField, UInt16>(ref result) =
					this._function.Unix.Char(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JDouble when OperatingSystem.IsWindows():
				Unsafe.As<TField, Double>(ref result) =
					this._function.Windows.Double(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JDouble:
				Unsafe.As<TField, Double>(ref result) =
					this._function.Unix.Double(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JFloat when OperatingSystem.IsWindows():
				Unsafe.As<TField, Single>(ref result) =
					this._function.Windows.Float(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JFloat:
				Unsafe.As<TField, Single>(ref result) =
					this._function.Unix.Float(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JInt when OperatingSystem.IsWindows():
				Unsafe.As<TField, Int32>(ref result) =
					this._function.Windows.Int(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JInt:
				Unsafe.As<TField, Int32>(ref result) = this._function.Unix.Int(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JLong when OperatingSystem.IsWindows():
				Unsafe.As<TField, Int64>(ref result) =
					this._function.Windows.Long(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JLong:
				Unsafe.As<TField, Int64>(ref result) =
					this._function.Unix.Long(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JShort when OperatingSystem.IsWindows():
				Unsafe.As<TField, Int16>(ref result) =
					this._function.Windows.Short(envRef, receiver.Value.Pointer, fieldId);
				break;
			case JNativeType.JShort:
				Unsafe.As<TField, Int16>(ref result) =
					this._function.Unix.Short(envRef, receiver.Value.Pointer, fieldId);
				break;
			default:
				Unsafe.As<TField, JObjectLocalRef>(ref result) = OperatingSystem.IsWindows() ?
					this._function.Windows.Object(envRef, receiver.Value.Pointer, fieldId) :
					this._function.Unix.Object(envRef, receiver.Value.Pointer, fieldId);
				break;
		}
		return result;
	}
}