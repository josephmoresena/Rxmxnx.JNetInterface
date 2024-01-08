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
	Boolean ILocalObject.IsDummy => this.IsDummy;
	ObjectLifetime ILocalObject.Lifetime => this.Object.Lifetime;
	ObjectMetadata ILocalObject.CreateMetadata() => ILocalObject.CreateMetadata(this.Object);
	void ILocalObject.ProcessMetadata(ObjectMetadata instanceMetadata)
		=> ILocalObject.ProcessMetadata(this.Object, instanceMetadata);

	IObject IViewObject.Object => this.Object;
}