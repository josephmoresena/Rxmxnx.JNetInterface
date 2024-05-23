namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java methods through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver.</typeparam>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct FieldFunctionSet<TReceiver> where TReceiver : unmanaged, IWrapper<JObjectLocalRef>
{
	/// <summary>
	/// Pointer to <c>GetFieldID</c> function.
	/// Returns the field ID for a field of a class.
	/// </summary>
	/// <remarks>The field is specified by its name and signature.</remarks>
	public readonly GetAccessibleIdFunction<JFieldId> GetFieldId;
	/// <summary>
	/// Pointer to <c>GetObjectField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JObjectLocalRef> GetObjectField;
	/// <summary>
	/// Pointer to <c>GetObjectField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JBoolean> GetBooleanField;
	/// <summary>
	/// Pointer to <c>GetByteField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JByte> GetByteField;
	/// <summary>
	/// Pointer to <c>GetCharField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JChar> GetCharField;
	/// <summary>
	/// Pointer to <c>GetShortField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JShort> GetShortField;
	/// <summary>
	/// Pointer to <c>GetIntField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JInt> GetIntField;
	/// <summary>
	/// Pointer to <c>GetLongField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JLong> GetLongField;
	/// <summary>
	/// Pointer to <c>GetFloatField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JFloat> GetFloatField;
	/// <summary>
	/// Pointer to <c>GetDoubleField</c> function.
	/// Returns the value of field.
	/// </summary>
	public readonly GetGenericFieldFunction<TReceiver, JDouble> GetDoubleField;
	/// <summary>
	/// Pointer to <c>SetObjectField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JObjectLocalRef> SetObjectField;
	/// <summary>
	/// Pointer to <c>SetObjectField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JBoolean> SetBooleanField;
	/// <summary>
	/// Pointer to <c>SetByteField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JByte> SetByteField;
	/// <summary>
	/// Pointer to <c>SetCharField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JChar> SetCharField;
	/// <summary>
	/// Pointer to <c>SetShortField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JShort> SetShortField;
	/// <summary>
	/// Pointer to <c>SetIntField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JInt> SetIntField;
	/// <summary>
	/// Pointer to <c>SetLongField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JLong> SetLongField;
	/// <summary>
	/// Pointer to <c>SetFloatField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JFloat> SetFloatField;
	/// <summary>
	/// Pointer to <c>SetDoubleField</c> function.
	/// Sets the value of field.
	/// </summary>
	public readonly SetGenericFieldFunction<TReceiver, JDouble> SetDoubleField;
}