namespace Rxmxnx.JNetInterface.Native;

public partial record ClassObjectMetadata
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <param name="fromProxy">Indicates whether the current instance is a dummy object (fake java object).</param>
	private ClassObjectMetadata(ITypeInformation metadata, Boolean? fromProxy) : base(
		IClassType.GetMetadata<JClassObject>(), fromProxy)
	{
		this.Name = metadata.ClassName;
		this.ClassSignature = metadata.Signature;
		this.Hash = metadata.Hash;
		this.ArrayDimension = JClassObject.GetArrayDimension(metadata.Signature);
		this.IsFinal = metadata.IsFinal;
		if (metadata.Kind is JTypeKind.Undefined) return;
		this.IsInterface = metadata.Kind is JTypeKind.Interface or JTypeKind.Annotation;
		this.IsAnnotation = metadata.Kind is JTypeKind.Annotation;
		this.IsEnum = metadata.Kind is JTypeKind.Enum;
	}
	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
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
	}
}