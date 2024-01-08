namespace Rxmxnx.JNetInterface.Native.Dummies;

/// <summary>
/// This interface exposes a JNI dummy instance.
/// </summary>
public partial interface IDummyEnvironment : IEnvironment
{
	/// <summary>
	/// Creates a <see cref="JBooleanObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JBoolean"/> value.</param>
	/// <returns>A <see cref="JBooleanObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JBooleanObject CreateWrapper(JBoolean value);
	/// <summary>
	/// Creates a <see cref="JByteObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> value.</param>
	/// <returns>A <see cref="JByteObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JByteObject CreateWrapper(JByte value);
	/// <summary>
	/// Creates a <see cref="JCharacterObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> value.</param>
	/// <returns>A <see cref="JCharacterObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JCharacterObject CreateWrapper(JChar value);
	/// <summary>
	/// Creates a <see cref="JDoubleObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> value.</param>
	/// <returns>A <see cref="JDoubleObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JDoubleObject CreateWrapper(JDouble value);
	/// <summary>
	/// Creates a <see cref="JFloatObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> value.</param>
	/// <returns>A <see cref="JFloatObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JFloatObject CreateWrapper(JFloat value);
	/// <summary>
	/// Creates a <see cref="JIntegerObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> value.</param>
	/// <returns>A <see cref="JIntegerObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JIntegerObject CreateWrapper(JInt value);
	/// <summary>
	/// Creates a <see cref="JLongObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> value.</param>
	/// <returns>A <see cref="JLongObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JLongObject CreateWrapper(JLong value);
	/// <summary>
	/// Creates a <see cref="JShortObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> value.</param>
	/// <returns>A <see cref="JShortObject"/> wrapper instance for <paramref name="value"/>.</returns>
	JShortObject CreateWrapper(JShort value);

	/// <summary>
	/// Creates a <see cref="JDirectByteBufferObject"/> instance.
	/// </summary>
	/// <param name="memory">A <see cref="IFixedMemory"/> instance.</param>
	/// <returns>A direct <see cref="JDirectByteBufferObject"/> instance.</returns>
	new JDirectByteBufferObject NewDirectByteBuffer(IFixedMemory.IDisposable memory);
	/// <summary>
	/// Creates an ephemeral <see cref="JDirectByteBufferObject"/> instance.
	/// </summary>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <returns>A <see cref="JDirectByteBufferObject"/> instance.</returns>
	JDirectByteBufferObject CreateEphemeralByteBuffer(Int32 capacity);
}