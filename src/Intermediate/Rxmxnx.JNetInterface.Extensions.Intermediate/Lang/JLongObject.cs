namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Long</c> instance.
/// </summary>
public sealed class JLongObject : JNumberObject<JLong, JLongObject>, IPrimitiveWrapperType<JLongObject>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JLongObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JLongObjectArraySignature;

	/// <inheritdoc/>
	public JLongObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JLongObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JLongObject(JLocalObject jLocal, JLong? value) : base(jLocal, value) { }

	/// <summary>
	/// Creates a <see cref="JLongObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JLong"/> value.</param>
	/// <returns>A new <see cref="JLongObject"/> instance.</returns>
	public static JLongObject? Create(IEnvironment env, JLong? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value), value) : default;

	/// <inheritdoc/>
	static JLongObject? IReferenceType<JLongObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JLongObject>(jLocal)) : default;
}