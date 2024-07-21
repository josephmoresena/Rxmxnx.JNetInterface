namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Well known type runtime information.
/// </summary>
internal readonly struct WellKnownRuntimeTypeInformation
{
	/// <summary>
	/// Type kind.
	/// </summary>
	public JTypeKind? Kind { get; init; }
	/// <summary>
	/// Indicates whether current type is final.
	/// </summary>
	public Boolean? IsFinal { get; init; }

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JTypeKind"/> to
	/// <see cref="JTypeKind"/>.
	/// </summary>
	/// <param name="typeKind">A <see cref="JTypeKind"/> to implicitly convert.</param>
	public static implicit operator WellKnownRuntimeTypeInformation(JTypeKind? typeKind)
	{
		Boolean? isFinal = typeKind switch
		{
			JTypeKind.Enum or JTypeKind.Primitive => true,
			JTypeKind.Interface or JTypeKind.Annotation => false,
			_ => null,
		};
		return new() { Kind = typeKind, IsFinal = isFinal, };
	}
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JReferenceTypeMetadata"/> to
	/// <see cref="JTypeKind"/>.
	/// </summary>
	/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> to implicitly convert.</param>
	public static implicit operator WellKnownRuntimeTypeInformation(JReferenceTypeMetadata? typeMetadata)
		=> new()
		{
			Kind = typeMetadata?.Kind,
			IsFinal = typeMetadata is not null ? typeMetadata.Modifier == JTypeModifier.Final : null,
		};
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="ClassObjectMetadata"/> to
	/// <see cref="JTypeKind"/>.
	/// </summary>
	/// <param name="classObjectMetadata">A <see cref="ClassObjectMetadata"/> to explicitly convert.</param>
	public static explicit operator WellKnownRuntimeTypeInformation(ClassObjectMetadata? classObjectMetadata)
		=> new() { Kind = ClassObjectMetadata.GetKind(classObjectMetadata), IsFinal = classObjectMetadata?.IsFinal, };
}