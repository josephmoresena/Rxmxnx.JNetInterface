namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Class&lt;?&gt;</c> instance.
/// </summary>
public sealed partial class JClassObject : JLocalObject, IClassType<JClassObject>,
	IInterfaceObject<JSerializableObject>, IInterfaceObject<JAnnotatedElementObject>,
	IInterfaceObject<JGenericDeclarationObject>, IInterfaceObject<JTypeObject>
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
		=> this.Environment.AccessFeature.RegisterNatives(this, [call, ..calls,]);
	/// <summary>
	/// Registers <paramref name="calls"/> as native methods.
	/// </summary>
	/// <param name="calls">A <see cref="JNativeCall"/> list.</param>
	public void Register(IReadOnlyList<JNativeCall> calls)
		=> this.Environment.AccessFeature.RegisterNatives(this, calls);
	/// <summary>
	/// Unregisters any native call.
	/// </summary>
	public void UnregisterNativeCalls() => this.Environment.AccessFeature.ClearNatives(this);
	/// <summary>
	/// Determines whether an object of current class can be safely cast to
	/// <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">Java class instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of current class can be safely cast to
	/// <paramref name="jClass"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean IsAssignableTo(JClassObject jClass)
	{
		IEnvironment env = this.Environment;
		return env.ClassFeature.IsAssignableFrom(jClass, this);
	}

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
		=> new ClassObjectMetadata(base.CreateMetadata())
		{
			Name = this.Name, ClassSignature = this.ClassSignature, IsFinal = this.IsFinal,
		};
	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not ClassObjectMetadata classMetadata)
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
		return env.ClassFeature.GetClass(className);
	}
	/// <summary>
	/// Retrieves the java class for given type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>The class instance for given type.</returns>
	public static JClassObject GetClass<TDataType>(IEnvironment env) where TDataType : IDataType<TDataType>
		=> env.ClassFeature.GetClass<TDataType>();
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="className">Name of class to load.</param>
	/// <param name="rawClassBytes">Binary span with class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	public static JClassObject LoadClass(IEnvironment env, CString className, ReadOnlySpan<Byte> rawClassBytes,
		JLocalObject? jClassLoader = default)
		=> env.ClassFeature.LoadClass(className, rawClassBytes, jClassLoader);
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <typeparam name="TDataType">The type with class definition.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="rawClassBytes">Binary span with class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	public static JClassObject LoadClass<TDataType>(IEnvironment env, ReadOnlySpan<Byte> rawClassBytes,
		JLocalObject? jClassLoader = default) where TDataType : JLocalObject, IReferenceType<TDataType>
		=> env.ClassFeature.LoadClass<TDataType>(rawClassBytes, jClassLoader);
}