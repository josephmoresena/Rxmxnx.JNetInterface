// ReSharper disable RedundantCast

namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal unsafe partial interface
	IPrimitiveType<TPrimitive, TValue> : IPrimitiveType<TPrimitive>, IPrimitiveValue<TValue>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, INativeDataType<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
	{
		Unsafe.As<Byte, TValue>(ref span[offset]) = this.Value;
		offset += sizeof(TPrimitive);
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<JValue> span, Int32 index)
	{
		span[index] = default; // Clears the current value
		Unsafe.As<JValue, TValue>(ref span[index]) = this.Value;
	}

	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TValue"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TValue"/> to implicitly convert.</param>
	static abstract implicit operator TPrimitive(TValue value);

	static TPrimitive IPrimitiveType<TPrimitive>.CreateFrom<TSource>(TSource value)
		=> value switch
		{
			JBoolean jBoolean => (TPrimitive)(SByte)jBoolean.ByteValue,
			JByte jByte => (TPrimitive)jByte.Value,
			JChar jChar => (TPrimitive)jChar.Value,
			JDouble jDouble => (TPrimitive)jDouble.Value,
			JFloat jFloat => (TPrimitive)jFloat.Value,
			JInt jInt => (TPrimitive)jInt.Value,
			JLong jLong => (TPrimitive)jLong.Value,
			JShort jShort => (TPrimitive)jShort.Value,
			TPrimitive primitive => primitive,
			_ => CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TPrimitive>(TSource.JniType),
		};
}