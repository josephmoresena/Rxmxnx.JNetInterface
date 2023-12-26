namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Double</c> instance.
/// </summary>
public sealed class JDoubleObject : JNumberObject<JDouble, JDoubleObject>, IPrimitiveWrapperType<JDoubleObject, JDouble>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JDoubleObject>(IClassType.GetMetadata<JNumberObject>());
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JDoubleObjectArraySignature;

	/// <inheritdoc/>
	internal JDoubleObject(JClassObject jClass, JObjectLocalRef localRef, JDouble value) :
		base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JDoubleObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	private JDoubleObject(JLocalObject jLocal) : base(jLocal) { }

	static JDoubleObject? IReferenceType<JDoubleObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JDoubleObject>(jLocal)) : default;
	static JDoubleObject? IReferenceType<JDoubleObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JDoubleObject>(jGlobal, env)) : default;
	static JDoubleObject? IPrimitiveWrapperType<JDoubleObject, JDouble>.Create(IEnvironment env, JDouble? value)
		=> value is not null ? (JDoubleObject)env.ReferenceProvider.CreateWrapper(value.Value) : default;
}