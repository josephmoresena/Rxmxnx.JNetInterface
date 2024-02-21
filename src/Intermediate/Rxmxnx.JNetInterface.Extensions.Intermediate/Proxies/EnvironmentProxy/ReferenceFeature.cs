namespace Rxmxnx.JNetInterface.Native.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase;
	/// <inheritdoc/>
	public abstract Boolean Unload(JLocalObject jLocal);
	/// <inheritdoc/>
	public abstract Boolean Unload(JGlobalBase jGlobal);
	/// <inheritdoc/>
	public abstract Boolean IsParameter(JLocalObject jLocal);
	/// <inheritdoc/>
	public abstract IDisposable GetSynchronizer(JReferenceObject jObject);
}