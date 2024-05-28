namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JClassObject> typeMetadata = TypeMetadataBuilder<JClassObject>
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
	/// Array class dimension.
	/// </summary>
	private Int32? _arrayDimension;

	/// <summary>
	/// Fully qualified class name.
	/// </summary>
	private CString? _className;
	/// <summary>
	/// Internal class hash.
	/// </summary>
	private String? _hash;
	/// <summary>
	/// Indicates whether the current class is an annotation.
	/// </summary>
	private Boolean? _isAnnotation;
	/// <summary>
	/// Indicates whether the current class is an enum.
	/// </summary>
	private Boolean? _isEnum;
	/// <summary>
	/// Indicates whether the current class is final.
	/// </summary>
	private Boolean? _isFinal;
	/// <summary>
	/// Indicates whether the current class is an interface.
	/// </summary>
	private Boolean? _isInterface;
	/// <summary>
	/// JNI signature for an object of the current instance.
	/// </summary>
	private CString? _signature;

	/// <summary>
	/// Loads class information.
	/// </summary>
	private void LoadClassInformation()
	{
		if (CString.IsNullOrEmpty(this._className) || CString.IsNullOrEmpty(this._signature) ||
		    String.IsNullOrWhiteSpace(this._hash))
			this.Environment.ClassFeature.GetClassInfo(this, out this._className, out this._signature, out this._hash);
	}
	private Boolean IsFinalType()
	{
		Boolean result = this.Environment.FunctionSet.IsFinal(this, out JModifierObject.Modifiers modifier);
		this._isInterface = !result && modifier.HasFlag(JModifierObject.Modifiers.Interface);
		this._isEnum = result && modifier.HasFlag(JModifierObject.Modifiers.Enum);
		this._isAnnotation = this._isInterface.Value && modifier.HasFlag(JModifierObject.Modifiers.Annotation);
		return result;
	}
	/// <summary>
	/// Retrieves printable text hash.
	/// </summary>
	/// <returns>A read-only UTF-16 char span.</returns>
	[ExcludeFromCodeCoverage]
	private ReadOnlySpan<Char> GetPrintableHash(out String lastChar)
	{
		ReadOnlySpan<Char> hash = this.Hash;
		lastChar = hash[^1] == default ? @"\0" : $"{hash[^1]}";
		return hash[..^1];
	}

	static JClassObject IClassType<JClassObject>.Create(IReferenceType.ClassInitializer initializer)
	{
		IEnvironment env = initializer.Class.Environment;
		JObjectLocalRef localRef = initializer.LocalReference;
		JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
		return env.ClassFeature.AsClassObject(classRef);
	}
	static JClassObject IClassType<JClassObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> initializer.Instance.Environment.ClassFeature.AsClassObject(initializer.Instance);
	static JClassObject IClassType<JClassObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> initializer.Environment.ClassFeature.AsClassObject(initializer.Global);
}