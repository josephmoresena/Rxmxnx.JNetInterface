namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JExecutableObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Executable</c> instance.
/// </summary>
public partial class JExecutableObject : JAccessibleObject, IClassType<JExecutableObject>,
	IInterfaceObject<JGenericDeclarationObject>, IInterfaceObject<JMemberObject>
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.AccessibleObjectHash, 34);
	/// <summary>
	/// Type interfaces.
	/// </summary>
	private static readonly ImmutableHashSet<JInterfaceTypeMetadata> typeInterfaces =
	[
		IInterfaceType.GetMetadata<JGenericDeclarationObject>(),
		IInterfaceType.GetMetadata<JMemberObject>(),
	];
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JExecutableObject>(
		JExecutableObject.typeInfo, IClassType.GetMetadata<JAccessibleObject>(), JTypeModifier.Abstract,
		JExecutableObject.typeInterfaces);

	static TypeMetadata IClassType<JExecutableObject>.Metadata => JExecutableObject.typeMetadata;

	/// <summary>
	/// Executable JNI definition.
	/// </summary>
	public JCallDefinition Definition => this._callDefinition ??= this.GetCallDefinition();
	/// <summary>
	/// Member declaring class.
	/// </summary>
	public JClassObject DeclaringClass
	{
		get
		{
			IEnvironment env = this.Environment;
			if (this._classInformation is not null)
				return env.ClassFeature.GetClass(this._classInformation);
			JClassObject result = env.FunctionSet.GetDeclaringClass(this);
			this._classInformation = result.GetInformation();
			return result;
		}
	}
	/// <summary>
	/// JNI method id.
	/// </summary>
	public JMethodId MethodId => this._methodId ??= this.GetMethodId();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	/// <param name="definition">Call definition.</param>
	/// <param name="declaringClass">Declaring class.</param>
	private protected JExecutableObject(JClassObject jClass, JObjectLocalRef localRef, JCallDefinition? definition,
		JClassObject? declaringClass) : base(jClass, localRef)
	{
		this._callDefinition = definition;
		this._classInformation = declaringClass?.GetInformation();
	}

	/// <inheritdoc/>
	protected JExecutableObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExecutableObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExecutableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JExecutableObject IClassType<JExecutableObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JExecutableObject IClassType<JExecutableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JExecutableObject IClassType<JExecutableObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}