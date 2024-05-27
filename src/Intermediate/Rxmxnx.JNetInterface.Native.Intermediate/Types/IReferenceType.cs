namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public partial interface IReferenceType : IObject, IDataType, IDisposable
{
	[ExcludeFromCodeCoverage]
	static Type IDataType.FamilyType => typeof(JLocalObject);

	/// <summary>
	/// Retrieves the metadata for given reference type.
	/// </summary>
	/// <typeparam name="TReference">Type of the current java reference datatype.</typeparam>
	/// <returns>The <see cref="JReferenceTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JReferenceTypeMetadata
		GetMetadata<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>()
		where TReference : JReferenceObject, IReferenceType<TReference>
		=> (JReferenceTypeMetadata)IDataType.GetMetadata<TReference>();
}

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
/// <typeparam name="TReference">Type of java reference type.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface
	IReferenceType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] out TReference> :
	IReferenceType,
	IDataType<TReference> where TReference : JReferenceObject, IReferenceType<TReference>
{
	/// <summary>
	/// Cached type interface set.
	/// </summary>
	private static readonly WeakReference<TypeInterfaceHelper<TReference>?> typeInterfaces = new(default);

	/// <summary>
	/// Retrieves current type interfaces set.
	/// </summary>
	internal static IReadOnlySet<Type> TypeInterfaces
	{
		get
		{
			if (IReferenceType<TReference>.typeInterfaces.TryGetTarget(out TypeInterfaceHelper<TReference>? result))
				return result.Set;
			result = new();
			IReferenceType<TReference>.typeInterfaces.SetTarget(result);
			return result.Set;
		}
	}

	/// <summary>
	/// Creates a <typeparamref name="TReference"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ObjectInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> instance from <paramref name="initializer"/>.</returns>
	protected static abstract TReference Create(ObjectInitializer initializer);
}