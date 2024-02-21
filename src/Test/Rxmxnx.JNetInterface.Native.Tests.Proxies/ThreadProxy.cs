namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.AbstractProxyJustification)]
public abstract class ThreadProxy : EnvironmentProxy, IThread
{
	public abstract CString Name { get; }
	public abstract Boolean Attached { get; }
	public abstract Boolean Daemon { get; }

	public abstract void Dispose();
}