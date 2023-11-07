namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Short</c> instance.
/// </summary>
public sealed class JShortObject : JNumberObject<JShort, JShortObject>, IPrimitiveWrapperType<JShortObject>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JShortObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JShortObjectArraySignature;

	/// <inheritdoc/>
	public JShortObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JShortObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JShortObject(JLocalObject jLocal, JShort? value) : base(jLocal, value) { }

	/// <summary>
	/// Creates a <see cref="JShortObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JShort"/> value.</param>
	/// <returns>A new <see cref="JShortObject"/> instance.</returns>
	public static JShortObject? Create(IEnvironment env, JShort? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value), value) : default;

	/// <inheritdoc/>
	static JShortObject? IDataType<JShortObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JShortObject>(jLocal)) : default;
}