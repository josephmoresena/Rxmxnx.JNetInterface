namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase;
	/// <inheritdoc/>
	public abstract JWeak CreateWeak(JGlobalBase jGlobal);
	/// <inheritdoc/>
	public abstract void LocalLoad(JGlobalBase jGlobal, JLocalObject jLocal);
	/// <inheritdoc/>
	public virtual Boolean Unload(JLocalObject jLocal) => true;
	/// <inheritdoc/>
	public virtual Boolean Unload(JGlobalBase jGlobal) => true;
	/// <inheritdoc/>
	public abstract Boolean IsParameter(JLocalObject jLocal);
	/// <inheritdoc/>
	public abstract IDisposable GetSynchronizer(JReferenceObject jObject);
}