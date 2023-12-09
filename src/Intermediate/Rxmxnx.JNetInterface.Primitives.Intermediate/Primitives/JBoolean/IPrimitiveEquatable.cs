namespace Rxmxnx.JNetInterface.Primitives;

public partial struct JBoolean : IPrimitiveEquatable
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Equals(other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Equals(other);

	/// <inheritdoc cref="IEquatable{JPrimitiveObject}.Equals(JPrimitiveObject)"/>
	private Boolean Equals(JPrimitiveObject? other)
	{
		if (other is null || other.ObjectSignature[0] != UnicodePrimitiveSignatures.JBooleanSignature[0])
			return false;
		return this._value == JValue.As<Byte>(ref other.ValueReference);
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
}