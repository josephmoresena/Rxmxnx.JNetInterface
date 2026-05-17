namespace Rxmxnx.JNetInterface.Native;

public partial record ClassObjectMetadata
{
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
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="kind">Class kind.</param>
	/// <param name="isFinal">Indicates whether class type is final.</param>
	internal ClassObjectMetadata(JClassObject jClass, JTypeKind kind = JTypeKind.Undefined, Boolean? isFinal = default)
		: base(jClass.Class)
	{
		this.Name = jClass.Name;
		this.ClassSignature = jClass.ClassSignature;
		this.Hash = jClass.Hash;
		this.ArrayDimension = jClass.ArrayDimension;
		this.IsInterface = kind switch
		{
			JTypeKind.Undefined => jClass.IsInterface,
			JTypeKind.Interface or JTypeKind.Annotation => true,
			_ => false,
		};
		this.IsEnum = kind switch
		{
			JTypeKind.Undefined => jClass.IsEnum,
			JTypeKind.Enum => true,
			_ => false,
		};
		this.IsAnnotation = kind switch
		{
			JTypeKind.Undefined => jClass.IsAnnotation,
			JTypeKind.Annotation => true,
			_ => false,
		};
		this.IsFinal = isFinal switch
		{
			null when this.IsEnum.Value => true,
			null when this.IsInterface.Value => false,
			null => jClass.IsFinal,
			_ => isFinal,
		};
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="classHash">Class hash.</param>
	/// <param name="classNameLength">JNI class name length.</param>
	/// <param name="signatureLength">JNI signature length.</param>
	/// <param name="kind">Class kind.</param>
	internal ClassObjectMetadata(String classHash, Int32 classNameLength, Int32 signatureLength, JTypeKind kind) : base(
		IClassType.GetMetadata<JClassObject>(), false)
	{
		this.Name = InfoSequenceBase.GetClassName(classHash, classNameLength);
		this.ClassSignature = InfoSequenceBase.GetClassSignature(classHash, classNameLength, signatureLength);
		this.ArrayDimension = kind is JTypeKind.Array ? JClassObject.GetArrayDimension(this.ClassSignature) : 0;
		this.Hash = classHash;
		this.IsInterface = kind is JTypeKind.Interface or JTypeKind.Annotation;
		this.IsEnum = kind is not JTypeKind.Class ? kind is JTypeKind.Enum : null;
		this.IsAnnotation = kind is not JTypeKind.Interface ? kind is JTypeKind.Annotation : null;
		this.IsFinal = kind switch
		{
			JTypeKind.Class or JTypeKind.Array => null,
			JTypeKind.Interface or JTypeKind.Annotation => false,
			_ => true,
		};
	}

	/// <summary>
	/// Retrieves <see cref="JTypeKind"/> value from <paramref name="classMetadata"/>.
	/// </summary>
	/// <param name="classMetadata">A <see cref="ClassObjectMetadata"/> instance.</param>
	/// <returns>A <see cref="JTypeKind"/> value.</returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	internal static JTypeKind? GetKind(ClassObjectMetadata? classMetadata)
	{
		if (classMetadata is null) return default;
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
		return classMetadata.IsFinal.HasValue ? JTypeKind.Class : null;
	}
	/// <summary>
	/// Creates a <see cref="ClassObjectMetadata"/> from <paramref name="typeInformation"/> instance.
	/// </summary>
	/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
	/// <param name="fromProxy">Indicates whether the current instance is a dummy object (fake java object).</param>
	/// <returns>A <see cref="ClassObjectMetadata"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static ClassObjectMetadata Create(ITypeInformation typeInformation, Boolean? fromProxy = null)
		=> new(typeInformation, fromProxy);
}