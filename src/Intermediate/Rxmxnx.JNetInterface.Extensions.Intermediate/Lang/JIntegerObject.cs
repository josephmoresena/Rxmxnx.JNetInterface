namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Integer</c> instance.
/// </summary>
public sealed class JIntegerObject : JNumberObject<JInt, JIntegerObject>, IPrimitiveWrapperType<JIntegerObject, JInt>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JIntegerObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JIntegerObjectArraySignature;

	/// <inheritdoc/>
	private JIntegerObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JIntegerObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JIntegerObject(JLocalObject jLocal, JInt value) : base(jLocal, value) => jLocal.Dispose();

	static JIntegerObject? IReferenceType<JIntegerObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JIntegerObject>(jLocal)) : default;
	static JIntegerObject? IReferenceType<JIntegerObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JIntegerObject>(jGlobal, env)) : default;
	static JIntegerObject? IPrimitiveWrapperType<JIntegerObject, JInt>.Create(IEnvironment env, JInt? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value.Value), value.Value) : default;
}