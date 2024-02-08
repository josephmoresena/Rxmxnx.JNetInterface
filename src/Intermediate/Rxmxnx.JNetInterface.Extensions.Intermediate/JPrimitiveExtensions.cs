namespace Rxmxnx.JNetInterface;

/// <summary>
/// Set of <see cref="IPrimitiveType"/> native extensions.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class JPrimitiveExtensions
{
	/// <summary>
	/// Creates a <see cref="JBooleanObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JBoolean"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JBooleanObject"/> instance.</returns>
	public static JBooleanObject ToJObject(this JBoolean value, IEnvironment env) => JBooleanObject.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JByteObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JByteObject"/> instance.</returns>
	public static JByteObject ToJObject(this JByte value, IEnvironment env)
		=> JNumberObject<JByte, JByteObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JCharacterObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JCharacterObject"/> instance.</returns>
	public static JCharacterObject ToJObject(this JChar value, IEnvironment env) => JCharacterObject.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JDoubleObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JDoubleObject"/> instance.</returns>
	public static JDoubleObject ToJObject(this JDouble value, IEnvironment env)
		=> JNumberObject<JDouble, JDoubleObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JFloatObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JFloatObject"/> instance.</returns>
	public static JFloatObject ToJObject(this JFloat value, IEnvironment env)
		=> JNumberObject<JFloat, JFloatObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JIntegerObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JIntegerObject"/> instance.</returns>
	public static JIntegerObject ToJObject(this JInt value, IEnvironment env)
		=> JNumberObject<JInt, JIntegerObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JLongObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JLongObject"/> instance.</returns>
	public static JLongObject ToJObject(this JLong value, IEnvironment env)
		=> JNumberObject<JLong, JLongObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JShortObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JShortObject"/> instance.</returns>
	public static JShortObject ToJObject(this JShort value, IEnvironment env)
		=> JNumberObject<JShort, JShortObject>.Create(env, value);
}