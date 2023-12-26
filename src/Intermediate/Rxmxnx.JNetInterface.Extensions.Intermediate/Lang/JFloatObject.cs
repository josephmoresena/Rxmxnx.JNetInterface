namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Float</c> instance.
/// </summary>
public sealed class JFloatObject : JNumberObject<JFloat, JFloatObject>, IPrimitiveWrapperType<JFloatObject, JFloat>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JFloatObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JFloatObjectArraySignature;

	/// <inheritdoc/>
	internal JFloatObject(JClassObject jClass, JObjectLocalRef localRef, JFloat value) :
		base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JFloatObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JFloatObject(JLocalObject jLocal) : base(jLocal) { }

	static JFloatObject? IReferenceType<JFloatObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JFloatObject>(jLocal)) : default;
	static JFloatObject? IReferenceType<JFloatObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JFloatObject>(jGlobal, env)) : default;
	static JFloatObject? IPrimitiveWrapperType<JFloatObject, JFloat>.Create(IEnvironment env, JFloat? value)
		=> value is not null ? (JFloatObject)env.ReferenceProvider.CreateWrapper(value.Value) : default;
}