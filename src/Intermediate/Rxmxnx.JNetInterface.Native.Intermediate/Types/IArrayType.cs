namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java array type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IArrayType : IReferenceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Array;

	/// <summary>
	/// Retrieves the metadata for given array type.
	/// </summary>
	/// <typeparam name="TArray">Type of current java array datatype.</typeparam>
	/// <returns>The <see cref="JClassTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JArrayTypeMetadata GetMetadata<TArray>() where TArray : JArrayObject, IArrayType<TArray>
		=> (JArrayTypeMetadata)IDataType.GetMetadata<TArray>();
}

/// <summary>
/// This interface exposes an object that represents a java array type instance.
/// </summary>
/// <typeparam name="TArray">Type of java class type.</typeparam>
public interface IArrayType<TArray> : IArrayType, IReferenceType<TArray>
	where TArray : JArrayObject, IArrayType<TArray> { }

/// <summary>
/// This interface exposes an object that represents a java array type instance.
/// </summary>
/// <typeparam name="TArray">Type of java class type.</typeparam>
/// <typeparam name="TElement">Type of <see cref="IDataType"/> array element.</typeparam>
public interface IArrayType<TArray, TElement> : IArrayType<JArrayObject<TElement>>, IArrayObject<TElement>
	where TArray : JArrayObject, IArrayType<TArray, TElement> where TElement : IObject, IDataType<TElement> { }