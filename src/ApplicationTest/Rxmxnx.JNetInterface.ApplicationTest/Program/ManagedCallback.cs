using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Program : IManagedCallback
{
	private Program(IVirtualMachine vm) => this.VirtualMachine = vm;
	public IVirtualMachine VirtualMachine { get; }

	public JStringObject? GetString(JLocalObject jLocal)
	{
		IEnvironment env = jLocal.Environment;
		return JStringObject.Create(env, $"Hello from .NET, {Environment.MachineName}");
	}
	public JInt GetInt(JLocalObject jLocal) => Environment.CurrentManagedThreadId;
	public void PassString(JLocalObject jLocal, JStringObject? jString) => Console.WriteLine(jString?.Value);
}