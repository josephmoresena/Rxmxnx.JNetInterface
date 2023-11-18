namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
public sealed class JByteObject : JNumberObject<JByte, JByteObject>, IPrimitiveWrapperType<JByteObject, JByte>
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

	static JByteObject? IReferenceType<JByteObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JByteObject>(jLocal)) : default;
	static JByteObject? IReferenceType<JByteObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JByteObject>(jGlobal, env)) : default;
	static JByteObject? IPrimitiveWrapperType<JByteObject, JByte>.Create(IEnvironment env, JByte? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value.Value), value.Value) : default;
}