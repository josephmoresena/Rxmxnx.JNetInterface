namespace Rxmxnx.JNetInterface.Native;

internal partial class JPrimitiveObject
{
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateBoolean(Byte value)
		=> new(JValue.Create(value), sizeof(Byte), UnicodePrimitiveSignatures.JBooleanSignature,
		       UnicodeClassNames.JBooleanObjectClassName);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateByte(SByte value)
		=> new(JValue.Create(value), sizeof(SByte), UnicodePrimitiveSignatures.JByteSignature,
		       UnicodeClassNames.JByteObjectClassName);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateChar(Char value)
		=> new(JValue.Create(value), sizeof(Char), UnicodePrimitiveSignatures.JCharSignature,
		       UnicodeClassNames.JCharacterObjectClassName);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateDouble(Double value)
		=> new(JValue.Create(value), sizeof(Double), UnicodePrimitiveSignatures.JDoubleSignature,
		       UnicodeClassNames.JDoubleObjectClassName);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateFloat(Single value)
		=> new(JValue.Create(value), sizeof(Single), UnicodePrimitiveSignatures.JFloatSignature,
		       UnicodeClassNames.JFloatObjectClassName);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateInt(Int32 value)
		=> new(JValue.Create(value), sizeof(Int32), UnicodePrimitiveSignatures.JIntSignature,
		       UnicodeClassNames.JIntegerObjectClassName);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateLong(Int64 value)
		=> new(JValue.Create(value), sizeof(Int64), UnicodePrimitiveSignatures.JLongSignature,
		       UnicodeClassNames.JLongObjectClassName);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateShort(Int16 value)
		=> new(JValue.Create(value), sizeof(Int16), UnicodePrimitiveSignatures.JShortSignature,
		       UnicodeClassNames.JShortObjectClassName);
}