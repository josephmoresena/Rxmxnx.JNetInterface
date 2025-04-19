namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a local <c>java.lang.Object</c> instance.
/// </summary>
public interface ILocalObject : IObject
{
	/// <inheritdoc cref="JReferenceObject.IsProxy"/>
	internal Boolean IsProxy { get; }
	/// <summary>
	/// Local <see cref="ObjectLifetime"/> instance.
	/// </summary>
	internal ObjectLifetime Lifetime { get; }
	/// <summary>
	/// Internal reference value.
	/// </summary>
	internal JObjectLocalRef LocalReference { get; }

	/// <summary>
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	IEnvironment Environment { get; }

	/// <summary>
	/// Creates the object metadata for the current instance.
	/// </summary>
	/// <returns>The object metadata for the current instance.</returns>
	protected ObjectMetadata CreateMetadata();
	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="instanceMetadata">The object metadata for the current instance.</param>
	protected void ProcessMetadata(ObjectMetadata instanceMetadata);

	/// <summary>
	/// Retrieves a <typeparamref name="TReference"/> instance from current instance.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <returns>A <typeparamref name="TReference"/> instance from current instance.</returns>
	TReference CastTo<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>()
		where TReference : JReferenceObject, IReferenceType<TReference>;

	/// <summary>
	/// Retrieves the metadata for given object.
	/// </summary>
	/// <param name="jLocal">A <see cref="ILocalObject"/> instance.</param>
	/// <returns>The object metadata for <paramref name="jLocal"/>.</returns>
	internal static ObjectMetadata CreateMetadata(ILocalObject jLocal) => jLocal.CreateMetadata();
	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="jLocal">A <see cref="ILocalObject"/> instance.</param>
	/// <param name="instanceMetadata">The object metadata for <paramref name="jLocal"/>.</param>
	internal static void ProcessMetadata(ILocalObject jLocal, ObjectMetadata instanceMetadata)
		=> jLocal.ProcessMetadata(instanceMetadata);
}