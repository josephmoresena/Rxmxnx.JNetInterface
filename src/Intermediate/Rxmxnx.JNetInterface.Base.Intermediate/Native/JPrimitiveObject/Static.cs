namespace Rxmxnx.JNetInterface.Native;

internal partial class JPrimitiveObject
{
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateBoolean(Byte value) => new Generic<Byte>(value);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateByte(SByte value) => new Generic<SByte>(value);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateChar(Char value) => new Generic<Char>(value);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateDouble(Double value) => new Generic<Double>(value);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateFloat(Single value) => new Generic<Single>(value);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateInt(Int32 value) => new Generic<Int32>(value);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateLong(Int64 value) => new Generic<Int64>(value);
	/// <summary>
	/// Create a <see cref="JPrimitiveObject"/> instance from <paramref name="value"/>.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	/// <returns><see cref="JPrimitiveObject"/> instance.</returns>
	public static JPrimitiveObject CreateShort(Int16 value) => new Generic<Int16>(value);
}