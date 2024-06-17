namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JClassObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public sealed record ClassObjectMetadata : ObjectMetadata, ITypeInformation
{
	/// <summary>
	/// <see cref="ClassObjectMetadata"/> instance for Java <c>void</c> type.
	/// </summary>
	public static readonly ClassObjectMetadata VoidMetadata = new(JPrimitiveTypeMetadata.VoidMetadata);

	/// <summary>
	/// Class modifier.
	/// </summary>
	private readonly JTypeModifier? _modifier;

	/// <summary>
	/// Class name of the current type.
	/// </summary>
	public CString Name { get; internal init; }
	/// <summary>
	/// Class signature of the current type.
	/// </summary>
	public CString ClassSignature { get; internal init; }
	/// <summary>
	/// Indicates whether the class of the current type is final.
	/// </summary>
	public Boolean? IsFinal { get; internal init; }
	/// <summary>
	/// Indicates whether the class of the current type is interface.
	/// </summary>
	public Boolean? IsInterface { get; internal init; }
	/// <summary>
	/// Indicates whether the class of the current type is enum.
	/// </summary>
	public Boolean? IsEnum { get; internal init; }
	/// <summary>
	/// Indicates whether the class of the current type is annotation.
	/// </summary>
	public Boolean? IsAnnotation { get; internal init; }
	/// <summary>
	/// Array type dimension.
	/// </summary>
	public Int32? ArrayDimension { get; internal init; }

	/// <inheritdoc/>
	internal ClassObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		ClassObjectMetadata? classMetadata = metadata as ClassObjectMetadata;
		this.Name = classMetadata?.Name!;
		this.ClassSignature = classMetadata?.ClassSignature!;
		this.Hash = classMetadata?.Hash!;
		this.IsInterface = classMetadata?.IsInterface;
		this.IsEnum = classMetadata?.IsEnum;
		this.IsAnnotation = classMetadata?.IsAnnotation;
		this.IsFinal = classMetadata?.IsFinal;
		this.ArrayDimension = classMetadata?.ArrayDimension;

		this._modifier = classMetadata?._modifier;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	internal ClassObjectMetadata(JClassObject jClass) : base(jClass.Class, IClassType.GetMetadata<JClassObject>())
	{
		this.Name = jClass.Name;
		this.ClassSignature = jClass.ClassSignature;
		this.Hash = jClass.Hash;
		this.IsInterface = jClass.IsInterface;
		this.IsEnum = jClass.IsEnum;
		this.IsAnnotation = jClass.IsAnnotation;
		this.IsFinal = jClass.IsFinal;
		this.ArrayDimension = jClass.ArrayDimension;

		this._modifier = this.TypeMetadata?.Modifier;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	private ClassObjectMetadata(ITypeInformation metadata) : base(IClassType.GetMetadata<JClassObject>())
	{
		this.Name = metadata.ClassName;
		this.ClassSignature = metadata.Signature;
		this.Hash = metadata.Hash;
		this.ArrayDimension = JClassObject.GetArrayDimension(metadata.Signature);
		this._modifier = metadata.Modifier;
		if (metadata.Kind is not JTypeKind.Undefined)
		{
			this.IsInterface = metadata.Kind is JTypeKind.Interface or JTypeKind.Annotation;
			this.IsAnnotation = metadata.Kind is JTypeKind.Annotation;
			this.IsEnum = metadata.Kind is JTypeKind.Enum;
		}
		if (metadata.Modifier.HasValue)
			this.IsFinal = metadata.Modifier == JTypeModifier.Final;
	}
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	private ClassObjectMetadata(ClassObjectMetadata classMetadata) : base(classMetadata)
	{
		this.Name = classMetadata.Name;
		this.ClassSignature = classMetadata.ClassSignature;
		this.Hash = classMetadata.Hash;
		this.IsInterface = classMetadata.IsInterface;
		this.IsEnum = classMetadata.IsEnum;
		this.IsAnnotation = classMetadata.IsAnnotation;
		this.IsFinal = classMetadata.IsFinal;
		this.ArrayDimension = classMetadata.ArrayDimension;
		this._modifier = classMetadata._modifier;
	}
	/// <summary>
	/// Class hash of the current type.
	/// </summary>
	public String Hash { get; internal init; }

	[ExcludeFromCodeCoverage]
	CString ITypeInformation.ClassName => this.Name;
	[ExcludeFromCodeCoverage]
	CString ITypeInformation.Signature => this.ClassSignature;
	[ExcludeFromCodeCoverage]
	JTypeKind ITypeInformation.Kind => ClassObjectMetadata.GetKind(this);
	[ExcludeFromCodeCoverage]
	JTypeModifier? ITypeInformation.Modifier => this.IsFinal.GetValueOrDefault() ? JTypeModifier.Final : this._modifier;

	/// <inheritdoc/>
	public override String ToTraceText()
		=> $"name: {this.Name} signature: {this.ClassSignature} final: {this.IsFinal} interface: {this.IsInterface} enum: {this.IsEnum} annotation: {this.IsAnnotation} hash: {ITypeInformation.GetPrintableHash(this.Hash, out String lastChar)}{lastChar}";

	/// <summary>
	/// Creates a <see cref="ClassObjectMetadata"/> for given <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IReferenceType"/> type.</typeparam>
	/// <returns>A <see cref="ClassObjectMetadata"/> instance.</returns>
	public static ClassObjectMetadata Create<TDataType>() where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TDataType>();
		return new(metadata);
	}

	/// <summary>
	/// Retrieves <see cref="JTypeKind"/> value from <paramref name="classMetadata"/>.
	/// </summary>
	/// <param name="classMetadata">A <see cref="ClassObjectMetadata"/> instance.</param>
	/// <returns>A <see cref="JTypeKind"/> value.</returns>
	private static JTypeKind GetKind(ClassObjectMetadata classMetadata)
	{
		if (classMetadata.ClassSignature.Length == 1)
			return JTypeKind.Primitive;
		if (classMetadata.ArrayDimension > 0 || classMetadata.ClassSignature[0] == CommonNames.ArraySignaturePrefixChar)
			return JTypeKind.Array;
		if (classMetadata.IsAnnotation.GetValueOrDefault())
			return JTypeKind.Annotation;
		if (classMetadata.IsInterface.GetValueOrDefault())
			return JTypeKind.Interface;
		if (classMetadata.IsEnum.GetValueOrDefault())
			return JTypeKind.Enum;
		return classMetadata.IsFinal.HasValue ? JTypeKind.Class : JTypeKind.Undefined;
	}
}