namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Number</c> instance.
/// </summary>
public sealed class JByteObject : JNumberObject, IClassType<JByteObject>, IPrimitiveWrapperType<JByteObject>
{
	static JDataTypeMetadata IDataType.Metadata => JPrimitiveWrapperTypeMetadata<JByteObject>.Instance;
	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => IPrimitiveType.GetMetadata<JByte>();

	/// <inheritdoc/>
	public JByteObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	internal JByteObject(IEnvironment env, JObjectLocalRef jLocalRef, Boolean isDummy, Boolean isNativeParameter,
		JClassObject? jClass = default) : base(env, jLocalRef, isDummy, isNativeParameter, jClass) { }
	/// <inheritdoc/>
	private JByteObject(JLocalObject jLocal) :
		base(jLocal, jLocal.Environment.ClassProvider.GetClass<JByteObject>()) { }

	/// <inheritdoc/>
	public new static JByteObject? Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JClassObject>(jLocal)) : default;
}