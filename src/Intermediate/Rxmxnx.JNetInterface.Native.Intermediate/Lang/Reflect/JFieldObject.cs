namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JClassTypeMetadata<JFieldObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Field</c> instance.
/// </summary>
public sealed partial class JFieldObject : JAccessibleObject, IClassType<JFieldObject>, IInterfaceObject<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JAccessibleObject>
	                                                    .Create<JFieldObject>(
		                                                    "java/lang/reflect/Field"u8, JTypeModifier.Final)
	                                                    .Implements<JMemberObject>().Build();

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
			if (!String.IsNullOrWhiteSpace(this._classHash))
				return env.ClassFeature.GetClass(this._classHash);
			JClassObject result = env.FunctionSet.GetDeclaringClass(this);
			this._classHash = result.Hash;
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
	internal JFieldObject(JClassObject jClass, JObjectLocalRef localRef, JFieldDefinition definition,
		JClassObject declaringClass) : base(jClass, localRef)
	{
		this._fieldDefinition = definition;
		this._classHash = declaringClass.Hash;
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