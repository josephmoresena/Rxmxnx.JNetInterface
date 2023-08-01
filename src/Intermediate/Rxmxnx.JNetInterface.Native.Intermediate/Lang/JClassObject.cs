namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Class&lt;?&gt;</c> instance.
/// </summary>
public sealed partial class JClassObject : JLocalObject, IClassType<JClassObject>
{
	/// <summary>
	/// Fully-qualified class name.
	/// </summary>
	public CString Name
	{
		get
		{
			if (this._className is null)
				this.LoadClassInformation();
			return this._className!;
		}
	}
	/// <summary>
	/// JNI signature for the instances of this class.
	/// </summary>
	public CString ClassSignature
	{
		get
		{
			if (this._className is null)
				this.LoadClassInformation();
			return this._className!;
		}
	}
	/// <summary>
	/// Internal class hash.
	/// </summary>
	public String Hash
	{
		get
		{
			if (this._hash is null)
				this.LoadClassInformation();
			return this._hash!;
		}
	}
	/// <summary>
	/// Indicates whether current class is final.
	/// </summary>
	public Boolean? IsFinal => this._isFinal;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JClassObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JClassObject>(jGlobal, env)) { }

	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JClassObjectMetadata(base.CreateMetadata())
		{
			Name = this.Name, ClassSignature = this.ClassSignature, IsFinal = this.IsFinal,
		};
	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not JClassObjectMetadata classMetadata)
			return;
		this._className = classMetadata.Name;
		this._signature = classMetadata.ClassSignature;
		this._hash = classMetadata.Hash;
		this._isFinal = classMetadata.IsFinal;
	}
	/// <summary>
	/// Retrieves the java class named <paramref name="className"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="className">Class name.</param>
	/// <returns>The class instance with given class name.</returns>
	public static JClassObject GetClass(IEnvironment env, CString className)
	{
		if (!className.IsNullTerminated)
			className = (CString)className.Clone();
		return env.ClassProvider.GetClass(className);
	}
	/// <summary>
	/// Retrieves the java class for given type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>The class instance for given type.</returns>
	public static JClassObject GetClass<TDataType>(IEnvironment env) where TDataType : IDataType<TDataType>
		=> env.ClassProvider.GetClass<TDataType>();

	/// <inheritdoc/>
	public static JClassObject? Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JClassObject>(jLocal)) : default;
}