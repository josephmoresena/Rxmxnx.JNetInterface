namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java array type instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IArrayType : IReferenceType, IDisposable
{
	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	static abstract JArrayTypeMetadata Metadata { get; }

	static JTypeKind IDataType.Kind => JTypeKind.Array;

	/// <summary>
	/// Retrieves the metadata for given array type.
	/// Current type metadata.
	/// </summary>
	/// <typeparam name="TArray">Type of current java array datatype.</typeparam>
	/// <returns>The <see cref="JArrayTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JArrayTypeMetadata GetMetadata<TArray>() where TArray : JArrayObject, IArrayType
		=> TArray.Metadata;

	/// <summary>
	/// Retrieves metadata for the array of arrays of <typeparamref name="TElement"/> type.
	/// </summary>
	/// <typeparam name="TElement">A <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <returns>A <see cref="JArrayTypeMetadata"/> for the array of arrays of <typeparamref name="TElement"/> type.</returns>
	internal static JArrayTypeMetadata GetArrayArrayMetadata<TElement>() where TElement : IObject, IDataType<TElement>
		=> IArrayType.GetMetadata<JArrayObject<JArrayObject<TElement>>>();
}