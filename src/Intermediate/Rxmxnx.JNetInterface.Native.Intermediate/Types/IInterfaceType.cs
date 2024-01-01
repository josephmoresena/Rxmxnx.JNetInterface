namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java interface type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IInterfaceType : IReferenceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Interface;

	/// <summary>
	/// Retrieves the metadata for given interface type.
	/// </summary>
	/// <typeparam name="TInterface">Type of current java interface datatype.</typeparam>
	/// <returns>The <see cref="JInterfaceTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JInterfaceTypeMetadata GetMetadata<TInterface>()
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		=> (JInterfaceTypeMetadata)IDataType.GetMetadata<TInterface>();
}

/// <summary>
/// This interface exposes an object that represents a java interface type instance.
/// </summary>
/// <typeparam name="TInterface">Type of java interface type.</typeparam>
public interface IInterfaceType<TInterface> : IInterfaceType, IReferenceType<TInterface>
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <summary>
	/// Retrieves a <see cref="IArrayObject{TElement}"/> instance from <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TElement">Type of <see cref="IDataType"/> array element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TObject}"/> instance.</param>
	/// <returns>A <see cref="IArrayObject{TElement}"/> instance.</returns>
	public static IArrayObject<TInterface> CastArray<TElement>(JArrayObject<TElement> jArray)
		where TElement : JLocalObject, IReferenceType<TElement>, IInterfaceImplementation<TElement, TInterface>
		=> new JArrayObject.JCastedArray<TInterface>(jArray);
}