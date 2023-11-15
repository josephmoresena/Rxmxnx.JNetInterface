namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a local <c>java.lang.Object</c> instance.
/// </summary>
public interface ILocalObject : IObject
{
	/// <inheritdoc cref="IEnvironment.VirtualMachine"/>
	internal IVirtualMachine VirtualMachine { get; }
	/// <inheritdoc cref="JReferenceObject.IsDummy"/>
	internal Boolean IsDummy { get; }

	/// <summary>
	/// Creates the object metadata for current instance.
	/// </summary>
	/// <returns>The object metadata for current instance.</returns>
	protected JObjectMetadata CreateMetadata();
	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="instanceMetadata">The object metadata for current instance.</param>
	protected void ProcessMetadata(JObjectMetadata instanceMetadata);

	/// <summary>
	/// Retrieves the metadata for given object.
	/// </summary>
	/// <param name="jLocal">A <see cref="ILocalObject"/> instance.</param>
	/// <returns>The object metadata for <paramref name="jLocal"/>.</returns>
	internal static JObjectMetadata CreateMetadata(ILocalObject jLocal) => jLocal.CreateMetadata();
	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="jLocal">A <see cref="ILocalObject"/> instance.</param>
	/// <param name="instanceMetadata">The object metadata for <paramref name="jLocal"/>.</param>
	internal static void ProcessMetadata(ILocalObject jLocal, JObjectMetadata instanceMetadata)
		=> jLocal.ProcessMetadata(instanceMetadata);
}