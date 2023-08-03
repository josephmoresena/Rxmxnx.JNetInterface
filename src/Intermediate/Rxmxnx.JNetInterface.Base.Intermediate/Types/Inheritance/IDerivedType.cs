namespace Rxmxnx.JNetInterface.Types.Inheritance;

/// <summary>
/// This interface exposes a java data type which extends or implements another.
/// </summary>
/// <typeparam name="TDerivative">Type of a datatype which is derived of <typeparamref name="TBase"/>.</typeparam>
/// <typeparam name="TBase">Type of datatype which <typeparamref name="TDerivative"/> extends or implements.</typeparam>
[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IDerivedType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] out TDerivative,
	TBase> : IReferenceType<TDerivative> where TDerivative : JReferenceObject, IReferenceType<TDerivative>
	where TBase : JReferenceObject, IReferenceType<TBase>
{
	/// <summary>
	/// Type of current derivation.
	/// </summary>
	internal static abstract DerivationKind Type { get; }
}