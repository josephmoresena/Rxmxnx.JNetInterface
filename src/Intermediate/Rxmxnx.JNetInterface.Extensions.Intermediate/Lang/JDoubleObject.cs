namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Double</c> instance.
/// </summary>
public sealed class JDoubleObject : JNumberObject<JDouble, JDoubleObject>, IPrimitiveWrapperType<JDoubleObject>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JDoubleObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JDoubleObjectArraySignature;

	/// <inheritdoc/>
	public JDoubleObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JDoubleObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JDoubleObject(JLocalObject jLocal, JDouble? value) : base(jLocal, value) { }

	/// <summary>
	/// Creates a <see cref="JDoubleObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JDouble"/> value.</param>
	/// <returns>A new <see cref="JDoubleObject"/> instance.</returns>
	public static JDoubleObject? Create(IEnvironment env, JDouble? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value), value) : default;

	/// <inheritdoc/>
	static JDoubleObject? IReferenceType<JDoubleObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JDoubleObject>(jLocal)) : default;
}