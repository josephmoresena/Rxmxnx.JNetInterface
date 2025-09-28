namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JChar : IPrimitiveEquatable
{
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => IPrimitiveNumericType.Equals(this, other);
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => IPrimitiveNumericType.Equals(this, other);

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JByte(JChar value) => (SByte)value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JShort"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JShort(JChar value) => (Int16)value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JInt"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JInt(JChar value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JLong"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JLong(JChar value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JFloat"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JFloat(JChar value) => value._value;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JChar"/> to <see cref="JDouble"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> to explicitly convert.</param>
	public static explicit operator JDouble(JChar value) => value._value;

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit IPrimitiveNumericType<JChar>.operator JChar(JChar jPrimitive) => jPrimitive;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JChar>.operator SByte(JChar jPrimitive)
		=> NativeUtilities.AsBytes(jPrimitive).ToValue<SByte>();
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JChar>.operator Int64(JChar jPrimitive) => jPrimitive._value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JChar>.operator Char(JChar jPrimitive) => jPrimitive.Value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JChar>.operator UInt16(JChar jPrimitive) => jPrimitive._value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JChar>.operator Int32(JChar jPrimitive) => jPrimitive._value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JChar>.operator Int16(JChar jPrimitive) => (Int16)jPrimitive._value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static explicit INativeDataType<JChar>.operator Single(JChar jPrimitive) => jPrimitive._value;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static implicit INativeDataType<JChar>.operator JChar(UInt16 jPrimitive) => (Char)jPrimitive;
}