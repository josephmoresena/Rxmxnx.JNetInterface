namespace Rxmxnx.JNetInterface.Types.Inheritance;

/// <summary>
/// This interface exposes a java data type which extends or implements another.
/// </summary>
/// <typeparam name="TDerivative">Type of a datatype which is derived of <typeparamref name="TBase"/>.</typeparam>
/// <typeparam name="TBase">Type of datatype which <typeparamref name="TDerivative"/> extends or implements.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IDerivedType<out TDerivative, TBase> : IDataType<TDerivative>
	where TDerivative : JReferenceObject, IDataType<TDerivative> where TBase : JReferenceObject, IDataType<TBase>
{
	/// <summary>
	/// Type of current derivation.
	/// </summary>
	internal static abstract JDerivationKind Type { get; }
}