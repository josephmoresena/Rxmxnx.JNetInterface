using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal interface IManagedCallback
{
	static virtual IVirtualMachine TypeVirtualMachine { get; set; } = default!;

	IVirtualMachine VirtualMachine { get; }

	JStringObject? GetString(JLocalObject jLocal);
	JInt GetInt(JLocalObject jLocal);
	void PassString(JLocalObject jLocal, JStringObject? jString);

	void AccessStringField(JLocalObject jLocal);
	void Throw(JLocalObject jLocal);

	static abstract JIntegerObject? SumArray(JClassObject jClass, JArrayObject<JInt>? jArray);
	static abstract JArrayObject<JArrayObject<JInt>>? GetIntArrayArray(JClassObject jClass, Int32 length);
	static abstract void PrintClass(JClassObject jClass);
}