namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JShort : IPrimitiveEquatable
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => IPrimitiveNumericType.Equals(this, other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => IPrimitiveNumericType.Equals(this, other);

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JShort"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to explicitly convert.</param>
	public static explicit operator JByte(JShort value) => NativeUtilities.AsBytes(value).ToValue<JByte>();
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JShort"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to explicitly convert.</param>
	public static implicit operator JInt(JShort value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JShort"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to implicitly convert.</param>
	public static implicit operator JLong(JShort value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JShort"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to explicitly convert.</param>
	public static explicit operator JChar(JShort value) => (Char)value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JShort"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to implicitly convert.</param>
	public static implicit operator JFloat(JShort value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JShort"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> to implicitly convert.</param>
	public static implicit operator JDouble(JShort value) => value._value;

	static explicit IPrimitiveNumericType<JShort>.operator JDouble(JShort jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JShort>.operator JFloat(JShort jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JShort>.operator Single(JShort jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JShort>.operator JInt(JShort jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JShort>.operator Int32(JShort jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JShort>.operator JLong(JShort jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JShort>.operator Int64(JShort jPrimitive) => jPrimitive._value;
	[ExcludeFromCodeCoverage]
	static explicit IPrimitiveNumericType<JShort>.operator JShort(JShort jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JShort>.operator Int16(JShort jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JShort>.operator SByte(JShort jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<SByte>();
	static explicit IPrimitiveNumericType<JShort>.operator Char(JShort jPrimitive) => (Char)jPrimitive._value;
}