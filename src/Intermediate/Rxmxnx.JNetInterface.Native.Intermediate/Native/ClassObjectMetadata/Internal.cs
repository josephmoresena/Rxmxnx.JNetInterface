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
		this.IsInterface = kind != JTypeKind.Undefined ?
			kind is JTypeKind.Interface or JTypeKind.Annotation :
			jClass.IsInterface;
		this.IsEnum = kind != JTypeKind.Undefined ? kind is JTypeKind.Enum : jClass.IsEnum;
		this.IsAnnotation = kind != JTypeKind.Undefined ? kind is JTypeKind.Annotation : jClass.IsAnnotation;
		this.IsFinal = isFinal;
		switch (this.IsFinal)
		{
			case null when kind is JTypeKind.Primitive or JTypeKind.Enum:
				this.IsFinal = true;
				break;
			case null when kind is JTypeKind.Interface or JTypeKind.Annotation:
				this.IsFinal = false;
				break;
			case null:
				this.IsFinal = jClass.IsFinal;
				break;
		}
	}

	/// <summary>
	/// Retrieves <see cref="JTypeKind"/> value from <paramref name="classMetadata"/>.
	/// </summary>
	/// <param name="classMetadata">A <see cref="ClassObjectMetadata"/> instance.</param>
	/// <returns>A <see cref="JTypeKind"/> value.</returns>
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
}