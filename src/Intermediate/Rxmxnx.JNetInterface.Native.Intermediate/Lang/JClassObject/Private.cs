namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JClassObject> typeMetadata = JTypeMetadataBuilder<JClassObject>
	                                                                        .Create(UnicodeClassNames.ClassObject,
		                                                                        JTypeModifier.Final)
	                                                                        .WithSignature(
		                                                                        UnicodeObjectSignatures
			                                                                        .ClassObjectSignature)
	                                                                        .Implements<JSerializableObject>()
	                                                                        .Implements<JAnnotatedElementObject>()
	                                                                        .Implements<JGenericDeclarationObject>()
	                                                                        .Implements<JTypeObject>().Build();

	static JClassTypeMetadata<JClassObject> IClassType<JClassObject>.Metadata => JClassObject.typeMetadata;

	/// <summary>
	/// Fully qualified class name.
	/// </summary>
	private CString? _className;
	/// <summary>
	/// Internal class hash.
	/// </summary>
	private String? _hash;
	/// <summary>
	/// Indicates whether current class is final.
	/// </summary>
	private Boolean? _isFinal;
	/// <summary>
	/// JNI signature for an object of current instance.
	/// </summary>
	private CString? _signature;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClassClassObject"><see cref="JClassObject"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	private JClassObject(JClassObject jClassClassObject, JClassLocalRef classRef) : base(
		jClassClassObject, classRef.Value) { }

	/// <summary>
	/// Loads class information.
	/// </summary>
	private void LoadClassInformation()
	{
		if (this._className is null || this._signature is null)
			this.Environment.ClassFeature.GetClassInfo(this, out this._className, out this._signature, out this._hash);
	}

	static JClassObject IReferenceType<JClassObject>.Create(IReferenceType.ClassInitializer initializer)
	{
		IEnvironment env = initializer.Class.Environment;
		JObjectLocalRef localRef = initializer.LocalReference;
		JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
		return env.ClassFeature.AsClassObject(classRef);
	}
	static JClassObject IReferenceType<JClassObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> initializer.Instance.Environment.ClassFeature.AsClassObject(initializer.Instance);
	static JClassObject IReferenceType<JClassObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> initializer.Environment.ClassFeature.AsClassObject(initializer.Global);
}