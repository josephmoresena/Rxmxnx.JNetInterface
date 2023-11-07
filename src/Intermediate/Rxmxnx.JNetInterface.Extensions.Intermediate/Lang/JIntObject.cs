namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Integer</c> instance.
/// </summary>
public sealed class JIntegerObject : JNumberObject<JInt, JIntegerObject>, IPrimitiveWrapperType<JIntegerObject>
{
	static JDataTypeMetadata IDataType.Metadata => JPrimitiveWrapperTypeMetadata<JIntegerObject>.Instance;
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JIntegerObjectArraySignature;

	/// <inheritdoc/>
	public JIntegerObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JIntegerObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JIntegerObject(JLocalObject jLocal, JInt? value) : base(jLocal, value) { }

	/// <summary>
	/// Creates a <see cref="JIntegerObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JInt"/> value.</param>
	/// <returns>A new <see cref="JIntegerObject"/> instance.</returns>
	public static JIntegerObject? Create(IEnvironment env, JInt? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value), value) : default;

	/// <inheritdoc/>
	static JIntegerObject? IDataType<JIntegerObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JIntegerObject>(jLocal)) : default;
}