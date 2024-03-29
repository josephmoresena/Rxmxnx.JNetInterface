namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a local <c>java.lang.Object</c> instance.
/// </summary>
public interface ILocalObject : IObject
{
	/// <inheritdoc cref="IEnvironment.VirtualMachine"/>
	internal IVirtualMachine VirtualMachine { get; }
	/// <inheritdoc cref="JReferenceObject.IsProxy"/>
	internal Boolean IsProxy { get; }
	/// <summary>
	/// Local <see cref="ObjectLifetime"/> instance.
	/// </summary>
	internal ObjectLifetime Lifetime { get; }
	/// <summary>
	/// Local <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	internal JObjectLocalRef InternalReference { get; }

	/// <summary>
	/// Creates the object metadata for current instance.
	/// </summary>
	/// <returns>The object metadata for current instance.</returns>
	protected ObjectMetadata CreateMetadata();
	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="instanceMetadata">The object metadata for current instance.</param>
	protected void ProcessMetadata(ObjectMetadata instanceMetadata);

	/// <summary>
	/// Retrieves a <typeparamref name="TReference"/> instance from current instance.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <returns>A <typeparamref name="TReference"/> instance from current instance.</returns>
	public TReference CastTo<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>;

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