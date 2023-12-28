namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JInt : IPrimitiveEquatable
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => IPrimitiveNumericType.Equals(this, other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => IPrimitiveNumericType.Equals(this, other);

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JByte(JInt value) => NativeUtilities.AsBytes(value).ToValue<JByte>();
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JShort(JInt value) => NativeUtilities.AsBytes(value).ToValue<JShort>();
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JInt"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to implicitly convert.</param>
	public static implicit operator JLong(JInt value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JChar(JInt value) => NativeUtilities.AsBytes(value).ToValue<JChar>();
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JInt"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to implicitly convert.</param>
	public static implicit operator JFloat(JInt value) => value._value;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JInt"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to implicitly convert.</param>
	public static implicit operator JDouble(JInt value) => value._value;

	static explicit IPrimitiveNumericType<JInt>.operator Int32(JInt jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JInt>.operator JLong(JInt jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JInt>.operator JDouble(JInt jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JInt>.operator JFloat(JInt jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JInt>.operator Single(JInt jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JInt>.operator JInt(JInt jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JInt>.operator SByte(JInt jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<SByte>();
	static explicit IPrimitiveNumericType<JInt>.operator Int64(JInt jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JInt>.operator Int16(JInt jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<Int16>();
	static explicit IPrimitiveNumericType<JInt>.operator Char(JInt jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<Char>();
}