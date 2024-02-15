namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed record ViewObjectProxy : IViewObject
{
	/// <summary>
	/// Internal <see cref="ObjectProxy"/> instance.
	/// </summary>
	private readonly ObjectProxy _proxy;

	public ViewObjectProxy(ObjectProxy proxy) => this._proxy = proxy;

	IObject IViewObject.Object => this._proxy;
}