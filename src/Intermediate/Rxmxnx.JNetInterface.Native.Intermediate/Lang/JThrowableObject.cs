namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Throwable</c> instance.
/// </summary>
public partial class JThrowableObject : JLocalObject, IBaseClassType<JThrowableObject>,
	IThrowableType<JThrowableObject>, ILocalObject, IInterfaceImplementation<JThrowableObject, JSerializableObject>
{
	/// <summary>
	/// Throwable message.
	/// </summary>
	public String Message => this._message ??= this.GetMessage();

	/// <inheritdoc/>
	public JThrowableObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	/// <inheritdoc/>
	internal JThrowableObject(IEnvironment env, JObjectLocalRef jLocalRef, Boolean isDummy, Boolean isNativeParameter,
		JClassObject? jClass = default) : base(env, jLocalRef, isDummy, isNativeParameter,
		                                       jClass ?? env.ClassProvider.ThrowableClassObject) { }

	/// <inheritdoc/>
	protected JThrowableObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass) { }

	JObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected new virtual JThrowableObjectMetadata CreateMetadata()
		=> new(base.CreateMetadata()) { Message = this.Message, };

	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not JThrowableObjectMetadata throwableMetadata)
			return;
		this._message ??= throwableMetadata.Message;
	}

	static JThrowableObject? IDataType<JThrowableObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JThrowableObject>(jLocal)) : default;
}