namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
partial class JEnvironment
{
	/// <summary>
	/// <see cref="EnvironmentValue"/> instance.
	/// </summary>
	private readonly EnvironmentValue _m;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	private JEnvironment(JVirtualMachine vm, JEnvironmentRef envRef) => this._m = new EnvironmentCore(vm, this, envRef);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	private JEnvironment(EnvironmentCore core) => this._m = core;
}