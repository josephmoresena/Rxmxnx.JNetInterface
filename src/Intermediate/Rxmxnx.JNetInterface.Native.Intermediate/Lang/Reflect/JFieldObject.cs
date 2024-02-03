namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Field</c> instance.
/// </summary>
public sealed partial class JFieldObject : JAccessibleObject, IClassType<JFieldObject>,
	IInterfaceObject<JGenericDeclarationObject>, IInterfaceObject<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JAccessibleObject>
	                                                      .Create<JFieldObject>(
		                                                      UnicodeClassNames.FieldObject(), JTypeModifier.Final)
	                                                      .Implements<JGenericDeclarationObject>()
	                                                      .Implements<JMemberObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JFieldObject.metadata;

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
			JClassObject result = env.Functions.GetDeclaringClass(this);
			this._classHash = result.Hash;
			return result;
		}
	}

	/// <summary>
	/// JNI field id.
	/// </summary>
	internal JFieldId FieldId => this._fieldId ??= this.GetFieldId();

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

	static JFieldObject IReferenceType<JFieldObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFieldObject IReferenceType<JFieldObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFieldObject IReferenceType<JFieldObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}