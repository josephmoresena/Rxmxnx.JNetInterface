namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JFieldObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Field</c> instance.
/// </summary>
public sealed partial class JFieldObject : JAccessibleObject, IClassType<JFieldObject>, IInterfaceObject<JMemberObject>
{
	/// <summary>
	/// Type information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.FieldHash, 23);
	/// <summary>
	/// Type interfaces.
	/// </summary>
	private static readonly ImmutableHashSet<JInterfaceTypeMetadata> typeInterfaces =
	[
		IInterfaceType.GetMetadata<JMemberObject>(),
	];
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JFieldObject>(
		JFieldObject.typeInfo, IClassType.GetMetadata<JAccessibleObject>(), JTypeModifier.Final,
		JFieldObject.typeInterfaces);

	static TypeMetadata IClassType<JFieldObject>.Metadata => JFieldObject.typeMetadata;

	/// <summary>
	/// Field JNI definition.
	/// </summary>
	public JFieldDefinition Definition => this._fieldDefinition ??= this.GetFieldDefinition();
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
	/// JNI field id.
	/// </summary>
	public JFieldId FieldId => this._fieldId ??= this.GetFieldId();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">Local object reference.</param>
	/// <param name="definition">Field definition.</param>
	/// <param name="declaringClass">Declaring class.</param>
	internal JFieldObject(JClassObject jClass, JObjectLocalRef localRef, JFieldDefinition? definition = default,
		JClassObject? declaringClass = default) : base(jClass, localRef)
	{
		this._fieldDefinition = definition;
		this._classInformation = declaringClass?.GetInformation();
	}

	/// <inheritdoc/>
	private JFieldObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JFieldObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JFieldObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JFieldObject IClassType<JFieldObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFieldObject IClassType<JFieldObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFieldObject IClassType<JFieldObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}