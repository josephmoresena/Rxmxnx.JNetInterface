using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal interface IManagedCallback
{
	IVirtualMachine VirtualMachine { get; }

	JStringObject? GetString(JLocalObject jLocal);
	JInt GetInt(JLocalObject jLocal);
	void PassString(JLocalObject jLocal, JStringObject? jString);
}