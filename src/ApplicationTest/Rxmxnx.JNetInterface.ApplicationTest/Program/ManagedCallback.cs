using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Program : IManagedCallback
{
	private readonly IVirtualMachine _vm;

	private Program(IVirtualMachine vm) => this._vm = vm;

	IVirtualMachine IManagedCallback.VirtualMachine => this._vm;

	JStringObject IManagedCallback.GetString(JLocalObject jLocal)
	{
		IEnvironment env = jLocal.Environment;
		return JStringObject.Create(env, $"Hello from .NET, {Environment.MachineName}");
	}
	JInt IManagedCallback.GetInt(JLocalObject jLocal) => Environment.CurrentManagedThreadId;
	void IManagedCallback.PassString(JLocalObject jLocal, JStringObject? jString) => Console.WriteLine(jString?.Value);

	static JIntegerObject? IManagedCallback.SumArray(JClassObject jClass, JArrayObject<JInt>? jArray)
	{
		if (jArray is null) return default;
		IEnvironment env = jClass.Environment;
		JInt result = 0;
		using JPrimitiveMemory<JInt> intElements = jArray.GetElements();
		foreach (JInt element in intElements.Values)
			result += element;
		return JNumberObject<JInt, JIntegerObject>.Create(env, result);
	}
}