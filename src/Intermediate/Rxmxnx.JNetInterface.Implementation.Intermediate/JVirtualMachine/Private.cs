namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public partial class JVirtualMachine
{
	/// <summary>
	/// User main classes' dictionary.
	/// </summary>
	private static readonly ConcurrentDictionary<String, JDataTypeMetadata> userMainClasses =
		MainClasses.CreateMainClassesDictionary();

	/// <summary>
	/// <see cref="JVirtualMachine"/> cache.
	/// </summary>
	private readonly VirtualMachineCore<JEnvironment> _core;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	private JVirtualMachine(JVirtualMachineRef vmRef) => this._core = new(this, vmRef);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="core">A <see cref="VirtualMachineCore{JEnvironment}"/> reference.</param>
	private JVirtualMachine(VirtualMachineCore<JEnvironment> core) => this._core = core;
}