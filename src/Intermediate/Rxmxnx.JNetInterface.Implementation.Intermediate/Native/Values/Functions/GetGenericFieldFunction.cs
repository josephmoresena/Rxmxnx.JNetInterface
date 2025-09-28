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
	where TReceiver : unmanaged, INativePointerType where TField : unmanaged, INativeDataType<TField>
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
		=> TField.Type switch
		{
			JNativeType.JBoolean when OperatingSystem.IsWindows() => this._function.Windows.Boolean(
				envRef, receiver.Pointer, fieldId),
			JNativeType.JBoolean => this._function.Unix.Boolean(envRef, receiver.Pointer, fieldId),
			JNativeType.JByte when OperatingSystem.IsWindows() => this._function.Windows.Byte(
				envRef, receiver.Pointer, fieldId),
			JNativeType.JByte => this._function.Unix.Byte(envRef, receiver.Pointer, fieldId),
			JNativeType.JChar when OperatingSystem.IsWindows() => this._function.Windows.Char(
				envRef, receiver.Pointer, fieldId),
			JNativeType.JChar => this._function.Unix.Char(envRef, receiver.Pointer, fieldId),
			JNativeType.JDouble when OperatingSystem.IsWindows() => this._function.Windows.Double(
				envRef, receiver.Pointer, fieldId),
			JNativeType.JDouble => this._function.Unix.Double(envRef, receiver.Pointer, fieldId),
			JNativeType.JFloat when OperatingSystem.IsWindows() => this._function.Windows.Float(
				envRef, receiver.Pointer, fieldId),
			JNativeType.JFloat => this._function.Unix.Float(envRef, receiver.Pointer, fieldId),
			JNativeType.JInt when OperatingSystem.IsWindows() => this._function.Windows.Int(
				envRef, receiver.Pointer, fieldId),
			JNativeType.JInt => this._function.Unix.Int(envRef, receiver.Pointer, fieldId),
			JNativeType.JLong when OperatingSystem.IsWindows() => this._function.Windows.Long(
				envRef, receiver.Pointer, fieldId),
			JNativeType.JLong => this._function.Unix.Long(envRef, receiver.Pointer, fieldId),
			JNativeType.JShort when OperatingSystem.IsWindows() => this._function.Windows.Short(
				envRef, receiver.Pointer, fieldId),
			JNativeType.JShort => this._function.Unix.Short(envRef, receiver.Pointer, fieldId),
			_ => OperatingSystem.IsWindows() ?
				this._function.Windows.Object(envRef, receiver.Pointer, fieldId) :
				this._function.Unix.Object(envRef, receiver.Pointer, fieldId),
		};
}