namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java class type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IClassType : IReferenceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Class;

	/// <summary>
	/// Retrieves the metadata for given class type.
	/// </summary>
	/// <typeparam name="TClass">Type of current java class datatype.</typeparam>
	/// <returns>The <see cref="JClassTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JClassTypeMetadata GetMetadata<TClass>() where TClass : JReferenceObject, IClassType<TClass>
		=> (JClassTypeMetadata)IDataType.GetMetadata<TClass>();
}

/// <summary>
/// This interface exposes an object that represents a java class type instance.
/// </summary>
/// <typeparam name="TClass">Type of java class type.</typeparam>
public interface IClassType<out TClass> : IClassType, IReferenceType<TClass>
	where TClass : JReferenceObject, IClassType<TClass> { }