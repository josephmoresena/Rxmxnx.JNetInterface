namespace Rxmxnx.JNetInterface.Native.Proxies;

/// <summary>
/// This interface exposes a JNI proxy thread.
/// </summary>
[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.AbstractProxyJustification)]
public abstract class ThreadProxy : EnvironmentProxy, IThread
{
	/// <inheritdoc/>
	public abstract CString Name { get; }
	/// <inheritdoc/>
	public abstract Boolean Daemon { get; }
	/// <inheritdoc/>
	public abstract Boolean Attached { get; }

	/// <inheritdoc/>
	public abstract void Dispose();
}