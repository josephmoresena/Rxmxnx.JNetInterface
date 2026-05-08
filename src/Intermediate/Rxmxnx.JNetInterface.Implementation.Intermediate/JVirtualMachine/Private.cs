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

	/// <summary>
	/// Attaches current thread to VM.
	/// </summary>
	/// <param name="args">A <see cref="ThreadCreationArgs"/> instance.</param>
	/// <returns>A <see cref="IThread"/> instance.</returns>
	private IThread AttachThread(ThreadCreationArgs args) => this._core.ThreadCache.AttachThread(args);
	/// <summary>
	/// Initialize main classes.
	/// </summary>
	private void InitializeClasses()
	{
		using IThread thread = this.AttachThread(ThreadCreationArgs.Create(ThreadPurpose.CreateGlobalReference));
		JEnvironment env = this.GetEnvironment(thread.Reference);
		this._core.LoadMainClasses(env);
	}
	/// <summary>
	/// Creates global instance for <paramref name="classMetadata"/>
	/// </summary>
	/// <param name="classMetadata">A <see cref="ClassObjectMetadata"/> instance.</param>
	private void CreateGlobalClass(ClassObjectMetadata classMetadata)
	{
		JGlobal globalClass = new(this, classMetadata, default);
		this._core.GlobalClassCache[classMetadata.Hash] = globalClass;
	}
}