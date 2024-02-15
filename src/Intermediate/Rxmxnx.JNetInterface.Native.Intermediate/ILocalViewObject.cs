namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes viewed <see cref="ILocalObject"/>.
/// </summary>
public interface ILocalViewObject : IViewObject, ILocalObject
{
	/// <summary>
	/// Real <see cref="ILocalObject"/> instance.
	/// </summary>
	new ILocalObject Object { get; }

	IVirtualMachine ILocalObject.VirtualMachine => this.Object.VirtualMachine;
	Boolean ILocalObject.IsProxy => this.IsProxy;
	ObjectLifetime ILocalObject.Lifetime => this.Object.Lifetime;
	ObjectMetadata ILocalObject.CreateMetadata() => ILocalObject.CreateMetadata(this.Object);
	void ILocalObject.ProcessMetadata(ObjectMetadata instanceMetadata)
		=> ILocalObject.ProcessMetadata(this.Object, instanceMetadata);

	IObject IViewObject.Object => this.Object;

	/// <summary>
	/// Retrieves the <see cref="JLocalObject"/> instance from <paramref name="jView"/>.
	/// </summary>
	/// <param name="jView">A <see cref="ILocalViewObject"/> instance.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	internal static JLocalObject? GetObject(ILocalViewObject? jView)
	{
		while (jView is not null)
		{
			if (jView.Object is JLocalObject jLocal) return jLocal;
			jView = jView.Object as ILocalViewObject;
		}
		return default;
	}
}