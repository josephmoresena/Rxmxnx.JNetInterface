namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Class&lt;?&gt;</c> instance.
/// </summary>
public sealed partial class JClassObject : JLocalObject, IClass, IDataType<JClassObject>
{
	/// <inheritdoc/>
	public static CString ClassName => UnicodeClassNames.JClassObjectClassName;
	/// <inheritdoc/>
	public static CString Signature => UnicodeObjectSignatures.JClassObjectSignature;
	/// <inheritdoc/>
	public static JTypeModifier Modifier => JTypeModifier.Final;

	/// <inheritdoc cref="IClass.Reference"/>
	internal JClassLocalRef Reference => this.As<JClassLocalRef>();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jClassRef">Local class reference.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	internal JClassObject(IEnvironment env, JClassLocalRef jClassRef, Boolean isDummy, Boolean isNativeParameter) :
		base(env, jClassRef.Value, isDummy, isNativeParameter, env.ClassProvider.ClassObject) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JClassObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JClassObject>(jGlobal, env)) { }

	/// <inheritdoc/>
	public CString Name
	{
		get
		{
			if (this._className is null)
				this.LoadClassInfo();
			return this._className!;
		}
	}
	/// <inheritdoc/>
	public CString ClassSignature
	{
		get
		{
			if (this._className is null)
				this.LoadClassInfo();
			return this._className!;
		}
	}
	/// <inheritdoc/>
	public String Hash
	{
		get
		{
			if (this._hash is null)
				this.LoadClassInfo();
			return this._hash!;
		}
	}
	/// <inheritdoc/>
	public Boolean? IsFinal => this._isFinal;

	JClassLocalRef IClass.Reference => this.Reference;

	/// <summary>
	/// Initialize the current instance with given <see cref="IDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">The <see cref="IDataType"/> with class definition.</typeparam>
	internal void Initialize<TDataType>() where TDataType : JLocalObject, IDataType
	{
		this._className = TDataType.ClassName;
		this._signature = TDataType.Signature;
		this._hash = new CStringSequence(this.Name, this.ClassSignature).ToString();
		this._isFinal = TDataType.Modifier == JTypeModifier.Final;
	}

	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JClassMetadata(base.CreateMetadata())
		{
			Name = this.Name, ClassSignature = this.ClassSignature, IsFinal = this.IsFinal,
		};
	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata metadata)
	{
		base.ProcessMetadata(metadata);
		if (metadata is not JClassMetadata classMetadata)
			return;

		this._className = classMetadata.Name;
		this._signature = classMetadata.ClassSignature;
		this._hash = classMetadata.Hash;
		this._isFinal = classMetadata.IsFinal;
	}

	/// <inheritdoc/>
	public static JClassObject? Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JClassObject>(jLocal)) : default;
}