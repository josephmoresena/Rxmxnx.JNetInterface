namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Function pointer to Set the value of Java fields through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver field.</typeparam>
/// <typeparam name="TField">Type of field.</typeparam>
[StructLayout(LayoutKind.Sequential)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal readonly unsafe struct SetGenericFieldFunction<TReceiver, TField> : ISetFieldFunction
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TField : unmanaged, INativeType
{
	/// <summary>
	/// Pointer to <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
	private readonly ISetFieldFunction.SetFieldFunction _function;

	/// <summary>
	/// <c>Set&lt;type&gt;Field</c>.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Set(JEnvironmentRef envRef, TReceiver receiver, JFieldId fieldId, TField value)
	{
		switch (TField.Type)
		{
			case JNativeType.JBoolean when OperatingSystem.IsWindows():
				this._function.Windows.Boolean(envRef, receiver.Value.Pointer, fieldId,
				                               Unsafe.As<TField, Byte>(ref value));
				break;
			case JNativeType.JBoolean:
				this._function.Unix.Boolean(envRef, receiver.Value.Pointer, fieldId,
				                            Unsafe.As<TField, Byte>(ref value));
				break;
			case JNativeType.JByte when OperatingSystem.IsWindows():

				this._function.Windows.Byte(envRef, receiver.Value.Pointer, fieldId,
				                            Unsafe.As<TField, SByte>(ref value));
				break;
			case JNativeType.JByte:

				this._function.Unix.Byte(envRef, receiver.Value.Pointer, fieldId, Unsafe.As<TField, SByte>(ref value));
				break;
			case JNativeType.JChar when OperatingSystem.IsWindows():

				this._function.Windows.Char(envRef, receiver.Value.Pointer, fieldId,
				                            Unsafe.As<TField, UInt16>(ref value));
				break;
			case JNativeType.JChar:
				this._function.Unix.Char(envRef, receiver.Value.Pointer, fieldId, Unsafe.As<TField, UInt16>(ref value));
				break;
			case JNativeType.JDouble when OperatingSystem.IsWindows():

				this._function.Windows.Double(envRef, receiver.Value.Pointer, fieldId,
				                              Unsafe.As<TField, Double>(ref value));
				break;
			case JNativeType.JDouble:

				this._function.Unix.Double(envRef, receiver.Value.Pointer, fieldId,
				                           Unsafe.As<TField, Double>(ref value));
				break;
			case JNativeType.JFloat when OperatingSystem.IsWindows():

				this._function.Windows.Float(envRef, receiver.Value.Pointer, fieldId,
				                             Unsafe.As<TField, Single>(ref value));
				break;
			case JNativeType.JFloat:

				this._function.Unix.Float(envRef, receiver.Value.Pointer, fieldId,
				                          Unsafe.As<TField, Single>(ref value));
				break;
			case JNativeType.JInt when OperatingSystem.IsWindows():

				this._function.Windows.Int(envRef, receiver.Value.Pointer, fieldId,
				                           Unsafe.As<TField, Int32>(ref value));
				break;
			case JNativeType.JInt:
				this._function.Unix.Int(envRef, receiver.Value.Pointer, fieldId, Unsafe.As<TField, Int32>(ref value));
				break;
			case JNativeType.JLong when OperatingSystem.IsWindows():

				this._function.Windows.Long(envRef, receiver.Value.Pointer, fieldId,
				                            Unsafe.As<TField, Int64>(ref value));
				break;
			case JNativeType.JLong:
				this._function.Unix.Long(envRef, receiver.Value.Pointer, fieldId, Unsafe.As<TField, Int64>(ref value));
				break;
			case JNativeType.JShort when OperatingSystem.IsWindows():

				this._function.Windows.Short(envRef, receiver.Value.Pointer, fieldId,
				                             Unsafe.As<TField, Int16>(ref value));
				break;
			case JNativeType.JShort:

				this._function.Unix.Short(envRef, receiver.Value.Pointer, fieldId, Unsafe.As<TField, Int16>(ref value));
				break;
			default:
				if (OperatingSystem.IsWindows())
					this._function.Windows.Object(envRef, receiver.Value.Pointer, fieldId,
					                              Unsafe.As<TField, JObjectLocalRef>(ref value));
				else
					this._function.Unix.Object(envRef, receiver.Value.Pointer, fieldId,
					                           Unsafe.As<TField, JObjectLocalRef>(ref value));
				break;
		}
	}
}