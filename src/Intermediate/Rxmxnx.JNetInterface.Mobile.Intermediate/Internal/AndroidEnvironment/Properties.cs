namespace Rxmxnx.JNetInterface.Internal;

internal partial class AndroidEnvironment
{
	/// <inheritdoc cref="IEnvironment.Version"/>
	public Int32 Version => (Int32)JRuntimeVersion.J6;
	/// <inheritdoc/>
	public JEnvironmentRef Reference => this._m.Core.Reference;
	/// <inheritdoc/>
	public IVirtualMachine VirtualMachine => this._m.Core.Host.Value;
	/// <inheritdoc/>
	public Int32? LocalCapacity
	{
		get => this._m.LocalCapacity;
		set => this._m.LocalCapacity = value;
	}
	/// <inheritdoc/>
	public ThrowableException? PendingException
	{
		get => this._m.PendingException;
		set => this._m.PendingException = value;
	}
	/// <inheritdoc/>
	public Int32 UsedStackBytes => this._m.Core.UsedStackBytes;
	/// <inheritdoc/>
	public Int32 UsableStackBytes
	{
		get => this._m.UsableStackBytes;
		set => this._m.UsableStackBytes = value;
	}
	/// <inheritdoc/>
	public Boolean NoProxy => true;
	/// <inheritdoc/>
	public virtual Boolean IsAttached => true;
}