namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Class&lt;?&gt;</c> instance.
/// </summary>
public sealed partial class JClassObject : JLocalObject, IClassType<JClassObject>,
	IInterfaceObject<JSerializableObject>, IInterfaceObject<JAnnotatedElementObject>,
	IInterfaceObject<JGenericDeclarationObject>, IInterfaceObject<JTypeObject>
{
	/// <summary>
	/// JNI class reference.
	/// </summary>
	public new JClassLocalRef Reference => this.To<JClassLocalRef>();
	/// <summary>
	/// Fully qualified class name.
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
			if (this._signature is null)
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
	/// Indicates whether the current class is final.
	/// </summary>
	public Boolean IsFinal => this._isFinal ??= this.IsFinalType();
	/// <summary>
	/// Indicates whether the current class is an array.
	/// </summary>
	public Boolean IsArray => this.ClassSignature[0] == CommonNames.ArraySignaturePrefixChar;
	/// <summary>
	/// Indicates whether the current class is an array.
	/// </summary>
	public Boolean IsPrimitive => this.ClassSignature.Length == 1;
	/// <summary>
	/// Indicates whether the current class is an interface.
	/// </summary>
	public Boolean IsInterface
	{
		get
		{
			if (!this._isInterface.HasValue)
				this._isFinal = this.IsFinalType();
			return this._isInterface!.Value;
		}
	}
	/// <summary>
	/// Indicates whether the current class is an enum.
	/// </summary>
	public Boolean IsEnum
	{
		get
		{
			if (!this._isEnum.HasValue)
				this._isFinal = this.IsFinalType();
			return this._isEnum!.Value;
		}
	}
	/// <summary>
	/// Indicates whether the current class is an annotation.
	/// </summary>
	public Boolean IsAnnotation
	{
		get
		{
			if (!this._isAnnotation.HasValue)
				this._isFinal = this.IsFinalType();
			return this._isAnnotation!.Value;
		}
	}
	/// <summary>
	/// Array class dimension.
	/// </summary>
	public Int32 ArrayDimension => this._arrayDimension ??= JClassObject.GetArrayDimension(this.ClassSignature);

	/// <summary>
	/// Registers <paramref name="calls"/> as native methods.
	/// </summary>
	/// <param name="entry">A <see cref="JNativeCallEntry"/> instance.</param>
	/// <param name="calls">A <see cref="JNativeCallEntry"/> array.</param>
	public void Register(JNativeCallEntry entry, params JNativeCallEntry[] calls)
		=> this.Environment.AccessFeature.RegisterNatives(this, [entry, .. calls,]);
	/// <summary>
	/// Registers <paramref name="calls"/> as native methods.
	/// </summary>
	/// <param name="calls">A <see cref="JNativeCallEntry"/> list.</param>
	public void Register(IReadOnlyList<JNativeCallEntry> calls)
		=> this.Environment.AccessFeature.RegisterNatives(this, calls);
	/// <summary>
	/// Unregisters any native call.
	/// </summary>
	public void UnregisterNativeCalls() => this.Environment.AccessFeature.ClearNatives(this);
	/// <summary>
	/// Determines whether an object of the current class can be safely cast to
	/// <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">Java class instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of the current class can be safely cast to
	/// <paramref name="jClass"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean IsAssignableTo(JClassObject jClass)
	{
		IEnvironment env = this.Environment;
		return env.ClassFeature.IsAssignableFrom(jClass, this);
	}
	/// <summary>
	/// Retrieves a <see cref="JStringObject"/> containing class name.
	/// </summary>
	/// <param name="isPrimitive">Indicates whether current class is primitive.</param>
	/// <returns>A <see cref="JStringObject"/> instance.</returns>
	public JStringObject GetClassName(out Boolean isPrimitive)
	{
		IEnvironment env = this.Environment;
		isPrimitive = env.FunctionSet.IsPrimitiveClass(this);
		return env.FunctionSet.GetClassName(this);
	}
	/// <summary>
	/// Retrieves superclass of the current type.
	/// </summary>
	/// <returns>Current super class <see cref="JClassObject"/> instance.</returns>
	public JClassObject? GetSuperClass()
	{
		IEnvironment env = this.Environment;
		return env.ClassFeature.GetSuperClass(this);
	}
	/// <summary>
	/// Retrieves an array with interfaces implemented by current type.
	/// </summary>
	/// <returns>A <see cref="JArrayObject{JClassObject}"/> instance.</returns>
	public JArrayObject<JClassObject> GetInterfaces()
	{
		IEnvironment env = this.Environment;
		return env.FunctionSet.GetInterfaces(this);
	}
	/// <summary>
	/// Retrieves the <see cref="JModuleObject"/> instance from current class.
	/// </summary>
	/// <returns>A <see cref="JModuleObject"/> instance.</returns>
	public JModuleObject? GetModule()
	{
		IEnvironment env = this.Environment;
		return env.ClassFeature.GetModule(this);
	}
	/// <summary>
	/// Retrieves a <see cref="ITypeInformation"/> from current instance.
	/// </summary>
	/// <returns>A <see cref="ITypeInformation"/> instance.</returns>
	public ITypeInformation GetInformation() => (ITypeInformation)this.CreateMetadata();

	/// <inheritdoc/>
	public override String ToString()
		=> !this.Reference.IsDefault ?
			JObject.GetObjectIdentifier(this.ClassSignature, this.Reference) :
			$"{this.Name}";
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override String ToTraceText()
		=> $"{this} hash: {ITypeInformation.GetPrintableHash(this.Hash, out String lastChar)}{lastChar}";

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
		=> new ClassObjectMetadata(base.CreateMetadata())
		{
			Name = this.Name,
			ClassSignature = this.ClassSignature,
			IsInterface = this.IsInterface,
			IsEnum = this.IsEnum,
			IsAnnotation = this.IsAnnotation,
			IsFinal = this.IsFinal,
			ArrayDimension = this.ArrayDimension,
			Hash = this.Hash,
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
		this._isInterface = classMetadata.IsInterface;
		this._isEnum = classMetadata.IsEnum;
		this._isAnnotation = classMetadata.IsAnnotation;
		this._isFinal = classMetadata.IsFinal;
		this._arrayDimension = classMetadata.ArrayDimension;
	}

	/// <summary>
	/// Retrieves the java class for <c>void</c>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>The class instance with given class name.</returns>
	public static JClassObject GetVoidClass(IEnvironment env) => env.ClassFeature.VoidPrimitive;
	/// <summary>
	/// Retrieves the java class named <paramref name="className"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="className">Class name.</param>
	/// <returns>The class instance with given class name.</returns>
	public static JClassObject GetClass(IEnvironment env, ReadOnlySpan<Byte> className)
		=> env.ClassFeature.GetClass(className);
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
	public static JClassObject LoadClass(IEnvironment env, ReadOnlySpan<Byte> className,
		ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader = default)
		=> env.ClassFeature.LoadClass(className, rawClassBytes, jClassLoader);
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <typeparam name="TDataType">The type with class definition.</typeparam>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="rawClassBytes">Binary span with class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	public static JClassObject
		LoadClass<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TDataType>(IEnvironment env,
			ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader = default)
		where TDataType : JLocalObject, IReferenceType<TDataType>
		=> env.ClassFeature.LoadClass<TDataType>(rawClassBytes, jClassLoader);
}