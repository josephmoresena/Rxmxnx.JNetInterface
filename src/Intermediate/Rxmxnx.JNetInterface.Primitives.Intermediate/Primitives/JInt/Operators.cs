namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JInt : IPrimitiveEquatable
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => IPrimitiveNumericType.Equals(this, other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => IPrimitiveNumericType.Equals(this, other);

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JByte"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JByte(JInt value) => Unsafe.As<JInt, JByte>(ref value);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JShort(JInt value) => Unsafe.As<JInt, JShort>(ref value);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JInt"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to implicitly convert.</param>
	public static implicit operator JLong(JInt value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JInt"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> to explicitly convert.</param>
	public static explicit operator JChar(JInt value) => Unsafe.As<JInt, JChar>(ref value);
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

#pragma warning disable CS0473
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static implicit INativeDataType<JInt>.operator JInt(SByte value) => new(value);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static implicit INativeDataType<JInt>.operator JInt(UInt16 value) => new(value);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static implicit INativeDataType<JInt>.operator JInt(Single value) => new(value);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static implicit INativeDataType<JInt>.operator JInt(Int64 value) => new(value);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static implicit INativeDataType<JInt>.operator JInt(Int16 value) => new(value);

	static explicit INativeDataType<JInt>.operator Int32(JInt jPrimitive) => jPrimitive._value;
	static explicit IPrimitiveNumericType<JInt>.operator JLong(JInt jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JInt>.operator JDouble(JInt jPrimitive) => jPrimitive;
	static explicit IPrimitiveNumericType<JInt>.operator JFloat(JInt jPrimitive) => jPrimitive;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JInt>.operator Single(JInt jPrimitive) => jPrimitive._value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit IPrimitiveNumericType<JInt>.operator JInt(JInt jPrimitive) => jPrimitive;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JInt>.operator SByte(JInt jPrimitive) => Unsafe.As<JInt, SByte>(ref jPrimitive);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JInt>.operator Int64(JInt jPrimitive) => jPrimitive._value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JInt>.operator Int16(JInt jPrimitive) => Unsafe.As<JInt, Int16>(ref jPrimitive);
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JInt>.operator Char(JInt jPrimitive) => Unsafe.As<JInt, Char>(ref jPrimitive);
#pragma warning restore CS0473
}