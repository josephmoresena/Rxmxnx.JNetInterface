namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Float</c> instance.
/// </summary>
public sealed class JFloatObject : JNumberObject<JFloat, JFloatObject>, IPrimitiveWrapperType<JFloatObject>
{
	static JDataTypeMetadata IDataType.Metadata => JPrimitiveWrapperTypeMetadata<JFloatObject>.Instance;
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JFloatObjectArraySignature;

	/// <inheritdoc/>
	public JFloatObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JFloatObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JFloatObject(JLocalObject jLocal, JFloat? value) : base(jLocal, value) { }

	/// <summary>
	/// Creates a <see cref="JFloatObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JFloat"/> value.</param>
	/// <returns>A new <see cref="JFloatObject"/> instance.</returns>
	public static JFloatObject? Create(IEnvironment env, JFloat? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value), value) : default;

	/// <inheritdoc/>
	static JFloatObject? IDataType<JFloatObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JFloatObject>(jLocal)) : default;
}