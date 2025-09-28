namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java native data type value.
/// </summary>
/// <typeparam name="TNativeType">Type of native data type.</typeparam>
internal interface INativeDataType<TNativeType> : INativeType
	where TNativeType : unmanaged, INativeDataType<TNativeType>
{
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="Byte"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator Byte(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<Byte>(JNativeType.JBoolean);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="JObjectLocalRef"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator JObjectLocalRef(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<JObjectLocalRef>(JNativeType.JObject);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="SByte"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator SByte(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<SByte>(JNativeType.JByte);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="UInt16"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator UInt16(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<UInt16>(JNativeType.JChar);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="UInt16"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator Char(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<Char>(JNativeType.JChar);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="Double"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator Double(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<Double>(JNativeType.JDouble);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="Single"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator Single(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<Single>(JNativeType.JFloat);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="Int32"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator Int32(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<Int32>(JNativeType.JInt);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="Int64"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator Int64(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<Int64>(JNativeType.JLong);
	/// <summary>
	/// Defines an explicit conversion of a given <typeparamref name="TNativeType"/> to <see cref="Int16"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TNativeType"/> to explicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual explicit operator Int16(TNativeType value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<Int16>(JNativeType.JShort);

	/// <summary>
	/// Defines an implicit conversion of a given <see cref="Byte"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="Byte"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(Byte value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JObjectLocalRef"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="JObjectLocalRef"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(JObjectLocalRef value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="SByte"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="SByte"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(SByte value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="UInt16"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="UInt16"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(UInt16 value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="Double"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="Double"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(Double value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="Single"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="Single"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(Single value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="Int32"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="Int32"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(Int32 value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="Int64"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="Int64"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(Int64 value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="Int16"/> to <typeparamref name="TNativeType"/>.
	/// </summary>
	/// <param name="value">A <see cref="Int16"/> to implicitly convert.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static virtual implicit operator TNativeType(Int16 value)
		=> CommonValidationUtilities.ThrowInvalidCastToNativeDataType<TNativeType>(TNativeType.Type);
}