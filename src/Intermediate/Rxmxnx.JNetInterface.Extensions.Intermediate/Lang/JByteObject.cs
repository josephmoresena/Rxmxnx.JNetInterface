namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
public sealed class JByteObject : JNumberObject<JByte, JByteObject>, IPrimitiveWrapperType<JByteObject>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JByteObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JByteObjectArraySignature;

	/// <inheritdoc/>
	public JByteObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JByteObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JByteObject(JLocalObject jLocal, JByte? value) : base(jLocal, value) { }

	/// <summary>
	/// Creates a <see cref="JByteObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JByte"/> value.</param>
	/// <returns>A new <see cref="JByteObject"/> instance.</returns>
	public static JByteObject? Create(IEnvironment env, JByte? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value), value) : default;

	/// <inheritdoc/>
	static JByteObject? IReferenceType<JByteObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JByteObject>(jLocal)) : default;
}