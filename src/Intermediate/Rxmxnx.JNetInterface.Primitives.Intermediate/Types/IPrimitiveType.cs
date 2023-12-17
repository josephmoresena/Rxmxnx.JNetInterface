namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
internal partial interface IPrimitiveType<TPrimitive, TValue> : IPrimitiveType<TPrimitive>, IPrimitiveValue<TValue>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
	void IPrimitiveType.CopyTo(Span<Byte> span)
	{
		Int32 offset = 0;
		this.CopyTo(span, ref offset);
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
	{
		TValue value = this.Value;
		NativeUtilities.AsBytes(in value).CopyTo(span[offset..]);
		offset += IDataType.GetMetadata<TPrimitive>().SizeOf;
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<JValue> span, Int32 index) => span[index] = JValue.Create(this.Value);

	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TValue"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TValue"/> to implicitly convert.</param>
	static abstract implicit operator TPrimitive(TValue value);
}