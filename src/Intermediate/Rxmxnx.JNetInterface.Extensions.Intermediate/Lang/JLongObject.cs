namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Long</c> instance.
/// </summary>
public sealed class JLongObject : JNumberObject<JLong, JLongObject>, IPrimitiveWrapperType<JLongObject, JLong>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JLongObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JLongObjectArraySignature;

	/// <inheritdoc/>
	private JLongObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JLongObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JLongObject(JLocalObject jLocal, JLong value) : base(jLocal, value) => jLocal.Dispose();

	static JLongObject? IReferenceType<JLongObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JLongObject>(jLocal)) : default;
	static JLongObject? IReferenceType<JLongObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JLongObject>(jGlobal, env)) : default;
	static JLongObject? IPrimitiveWrapperType<JLongObject, JLong>.Create(IEnvironment env, JLong? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value.Value), value.Value) : default;
}