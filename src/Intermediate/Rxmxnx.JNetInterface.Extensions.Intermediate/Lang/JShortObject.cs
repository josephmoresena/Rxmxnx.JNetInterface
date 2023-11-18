namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Short</c> instance.
/// </summary>
public sealed class JShortObject : JNumberObject<JShort, JShortObject>, IPrimitiveWrapperType<JShortObject, JShort>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JShortObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JShortObjectArraySignature;

	/// <inheritdoc/>
	private JShortObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JShortObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	private JShortObject(JLocalObject jLocal, JShort value) : base(jLocal, value) => jLocal.Dispose();

	static JShortObject? IReferenceType<JShortObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JShortObject>(jLocal)) : default;
	static JShortObject? IReferenceType<JShortObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JShortObject>(jGlobal, env)) : default;
	static JShortObject? IPrimitiveWrapperType<JShortObject, JShort>.Create(IEnvironment env, JShort? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value.Value), value.Value) : default;
}