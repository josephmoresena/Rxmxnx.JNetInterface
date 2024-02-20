namespace Rxmxnx.JNetInterface.Primitives;

public partial struct JBoolean : IPrimitiveEquatable, ISpanParsable<JBoolean>
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Equals(other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Equals(other);

	/// <inheritdoc cref="IEquatable{JPrimitiveObject}.Equals(JPrimitiveObject)"/>
	private Boolean Equals(JPrimitiveObject? other)
	{
		if (other is null || other.ObjectSignature[0] != UnicodePrimitiveSignatures.BooleanSignatureChar)
			return false;
		return this._value == other.ToByte();
	}
	/// <inheritdoc cref="IEquatable{IPrimitiveType}.Equals(IPrimitiveType)"/>
	private Boolean Equals(IPrimitiveType? other)
		=> other switch
		{
			JBoolean jBoolean => this == jBoolean,
			IWrapper<JBoolean> jBooleanWrapper => this == jBooleanWrapper.Value,
			IWrapper<Boolean> booleanWrapper => this.Value == booleanWrapper.Value,
			IWrapper<Byte> byteWrapper => this._value == byteWrapper.Value,
			JPrimitiveObject jPrimitive => this.Equals(jPrimitive),
			_ => false,
		};

	static JBoolean IParsable<JBoolean>.Parse(String s, IFormatProvider? provider) => JBoolean.Parse(s);
	static Boolean IParsable<JBoolean>.TryParse(String? s, IFormatProvider? provider, out JBoolean result)
		=> JBoolean.TryParse(s, out result);
	static JBoolean ISpanParsable<JBoolean>.Parse(ReadOnlySpan<Char> s, IFormatProvider? provider) => JBoolean.Parse(s);
	static Boolean ISpanParsable<JBoolean>.TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider,
		out JBoolean result)
		=> JBoolean.TryParse(s, out result);
}