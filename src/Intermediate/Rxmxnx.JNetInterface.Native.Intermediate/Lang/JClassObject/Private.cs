namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Reference metadata.
	/// </summary>
	private static readonly JReferenceMetadata metadata = JMetadataBuilder
	                                                      .Create<JLocalObject>(
		                                                      UnicodeClassNames.JClassObjectClassName,
		                                                      JTypeModifier.Final)
	                                                      .WithSignature(UnicodeObjectSignatures.JClassObjectSignature)
	                                                      .Build();

	static JDataTypeMetadata IDataType.Metadata => JClassObject.metadata;

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
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JClassObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.ClassObject)
	{
		if (jLocal is not IClass jClass)
			return;

		this._className = jClass.Name;
		this._signature = jClass.ClassSignature;
		this._hash = jClass.Hash;
		this._isFinal = jClass.IsFinal;
	}

	/// <summary>
	/// Loads class information.
	/// </summary>
	private void LoadClassInformation()
	{
		if (this._className is null || this._signature is null)
			this.Environment.ClassProvider.GetClassInfo(this, out this._className, out this._signature, out this._hash);
	}
}