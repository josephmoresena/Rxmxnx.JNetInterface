namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Class&lt;?&gt;</c> instance.
/// </summary>
public sealed partial class JClassObject : JLocalObject, IClassType<JClassObject>,
	IInterfaceImplementation<JClassObject, JSerializableObject>,
	IInterfaceImplementation<JClassObject, JAnnotatedElementObject>,
	IInterfaceImplementation<JClassObject, JGenericDeclarationObject>,
	IInterfaceImplementation<JClassObject, JTypeObject>
{
	/// <summary>
	/// Fully-qualified class name.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public CString ClassSignature
	{
		get
		{
			if (this._className is null)
				this.LoadClassInformation();
			return this._signature!;
		}
	}
	/// <summary>
	/// Internal class hash.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
	/// Registers <paramref name="calls"/> as native methods.
	/// </summary>
	/// <param name="call">A <see cref="JNativeCall"/> instance.</param>
	/// <param name="calls">A <see cref="JNativeCall"/> array.</param>
	public void Register(JNativeCall call, params JNativeCall[] calls)
		=> this.Environment.AccessProvider.RegisterNatives(this, [call, ..calls,]);
	/// <summary>
	/// Unregisters any native call.
	/// </summary>
	public void UnregisterNativeCalls() => this.Environment.AccessProvider.ClearNatives(this);

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
	public static JClassObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? jLocal.Environment.ClassProvider.AsClassObject(jLocal) : default;
	/// <inheritdoc/>
	public static JClassObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? env.ClassProvider.AsClassObject(jGlobal) : default;
}