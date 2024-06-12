namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JExecutableObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Executable</c> instance.
/// </summary>
public partial class JExecutableObject : JAccessibleObject, IClassType<JExecutableObject>,
	IInterfaceObject<JGenericDeclarationObject>, IInterfaceObject<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JAccessibleObject>
	                                                    .Create<JExecutableObject>(
		                                                    "java/lang/reflect/Executable"u8, JTypeModifier.Abstract)
	                                                    .Implements<JGenericDeclarationObject>()
	                                                    .Implements<JMemberObject>().Build();

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
			if (!String.IsNullOrWhiteSpace(this._classHash))
				return env.ClassFeature.GetClass(this._classHash);
			JClassObject result = env.FunctionSet.GetDeclaringClass(this);
			this._classHash = result.Hash;
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
	private protected JExecutableObject(JClassObject jClass, JObjectLocalRef localRef, JCallDefinition definition,
		JClassObject declaringClass) : base(jClass, localRef)
	{
		this._callDefinition = definition;
		this._classHash = declaringClass.Hash;
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