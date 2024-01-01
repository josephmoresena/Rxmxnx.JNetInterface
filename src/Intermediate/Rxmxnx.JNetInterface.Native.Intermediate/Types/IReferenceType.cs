namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReferenceType : IObject, IDataType, IDisposable
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
	/// Creates a <typeparamref name="TReference"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> instance from <paramref name="jLocal"/>.</returns>
	static abstract TReference? Create(JLocalObject? jLocal);
	/// <summary>
	/// Creates a <typeparamref name="TReference"/> instance from <paramref name="jGlobal"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> instance from <paramref name="jGlobal"/>.</returns>
	static abstract TReference? Create(IEnvironment env, JGlobalBase? jGlobal);

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