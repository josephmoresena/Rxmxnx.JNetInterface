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
	where TReceiver : unmanaged, INativePointerType where TField : unmanaged, INativeDataType<TField>
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
		// ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
		switch (TField.Type)
		{
#if !ANDROID
			case JNativeType.JBoolean when SystemInfo.IsWindows:
				this._function.Windows.Boolean(envRef, receiver.Pointer, fieldId, (Byte)value);
				break;
			case JNativeType.JByte when SystemInfo.IsWindows:
				this._function.Windows.Byte(envRef, receiver.Pointer, fieldId, (SByte)value);
				break;
			case JNativeType.JChar when SystemInfo.IsWindows:
				this._function.Windows.Char(envRef, receiver.Pointer, fieldId, (UInt16)value);
				break;
			case JNativeType.JDouble when SystemInfo.IsWindows:
				this._function.Windows.Double(envRef, receiver.Pointer, fieldId, (Double)value);
				break;
			case JNativeType.JFloat when SystemInfo.IsWindows:
				this._function.Windows.Float(envRef, receiver.Pointer, fieldId, (Single)value);
				break;
			case JNativeType.JInt when SystemInfo.IsWindows:
				this._function.Windows.Int(envRef, receiver.Pointer, fieldId, (Int32)value);
				break;
			case JNativeType.JLong when SystemInfo.IsWindows:
				this._function.Windows.Long(envRef, receiver.Pointer, fieldId, (Int64)value);
				break;
			case JNativeType.JShort when SystemInfo.IsWindows:
				this._function.Windows.Short(envRef, receiver.Pointer, fieldId, (Int16)value);
				break;
#endif
			case JNativeType.JBoolean:
				this._function.Unix.Boolean(envRef, receiver.Pointer, fieldId, (Byte)value);
				break;
			case JNativeType.JByte:
				this._function.Unix.Byte(envRef, receiver.Pointer, fieldId, (SByte)value);
				break;
			case JNativeType.JChar:
				this._function.Unix.Char(envRef, receiver.Pointer, fieldId, (UInt16)value);
				break;
			case JNativeType.JDouble:
				this._function.Unix.Double(envRef, receiver.Pointer, fieldId, (Double)value);
				break;
			case JNativeType.JFloat:
				this._function.Unix.Float(envRef, receiver.Pointer, fieldId, (Single)value);
				break;
			case JNativeType.JInt:
				this._function.Unix.Int(envRef, receiver.Pointer, fieldId, (Int32)value);
				break;
			case JNativeType.JLong:
				this._function.Unix.Long(envRef, receiver.Pointer, fieldId, (Int64)value);
				break;
			case JNativeType.JShort:
				this._function.Unix.Short(envRef, receiver.Pointer, fieldId, (Int16)value);
				break;
			default:
#if !ANDROID
				if (SystemInfo.IsWindows)
				{
					this._function.Windows.Object(envRef, receiver.Pointer, fieldId, (JObjectLocalRef)value);
					return;
				}
#endif
				this._function.Unix.Object(envRef, receiver.Pointer, fieldId, (JObjectLocalRef)value);
				break;
		}
	}
}