namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial interface IReferenceType : IObject, IDataType, IDisposable
{
	/// <summary>
	/// Retrieves the metadata for given reference type.
	/// </summary>
	/// <typeparam name="TReference">Type of current java reference datatype.</typeparam>
	/// <returns>The <see cref="JReferenceTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JReferenceTypeMetadata GetMetadata<TReference>()
		where TReference : JReferenceObject, IReferenceType<TReference>
		=> (JReferenceTypeMetadata)IDataType.GetMetadata<TReference>();
}

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
/// <typeparam name="TReference">Type of java reference type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReferenceType<out TReference> : IReferenceType, IDataType<TReference>
	where TReference : JReferenceObject, IReferenceType<TReference>
{
	/// <summary>
	/// Creates a <typeparamref name="TReference"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ClassInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> instance from <paramref name="initializer"/>.</returns>
	protected static abstract TReference Create(ClassInitializer initializer);
	/// <summary>
	/// Creates a <typeparamref name="TReference"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ObjectInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> instance from <paramref name="initializer"/>.</returns>
	protected static abstract TReference Create(ObjectInitializer initializer);
	/// <summary>
	/// Creates a <typeparamref name="TReference"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.GlobalInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> instance from <paramref name="initializer"/>.</returns>
	protected static abstract TReference Create(GlobalInitializer initializer);

	/// <summary>
	/// Retrieves the base types from current type.
	/// </summary>
	/// <returns>Enumerable of types.</returns>
	internal static IEnumerable<Type> GetBaseTypes()
	{
		Type? currentType = typeof(TReference).BaseType;
		while (currentType is not null && currentType != typeof(JReferenceObject))
		{
			yield return currentType;
			currentType = currentType.BaseType;
		}
	}
	/// <summary>
	/// Retrieves the interfaces types from current type.
	/// </summary>
	/// <returns>Enumerable of types.</returns>
	[UnconditionalSuppressMessage("Trim analysis", "IL2090")]
	internal static IEnumerable<Type> GetInterfaceTypes()
	{
		Type[] interfaceTypes = typeof(TReference).GetInterfaces();
		foreach (Type interfaceType in interfaceTypes)
			yield return interfaceType;
	}
}