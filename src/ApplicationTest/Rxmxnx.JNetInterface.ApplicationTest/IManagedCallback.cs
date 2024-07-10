using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal interface IManagedCallback
{
	static virtual IVirtualMachine TypeVirtualMachine { get; set; } = default!;

	IVirtualMachine VirtualMachine { get; }

	JStringObject? GetHelloString(JLocalObject jLocal);
	JInt GetThreadId(JLocalObject jLocal);
	void PrintRuntimeInformation(JLocalObject jLocal, JStringObject? jString);

	void ProcessField(JLocalObject jLocal);
	void Throw(JLocalObject jLocal);

	static abstract JIntegerObject? SumArray(JClassObject jClass, JArrayObject<JInt>? jArray);
	static abstract JArrayObject<JArrayObject<JInt>>? GetIntArrayArray(JClassObject jClass, Int32 length);
	static abstract void PrintClass(JClassObject jClass);
	static abstract JClassObject GetVoidClass(JClassObject jClass);
	static abstract JArrayObject<JClassObject> GetPrimitiveClasses(JClassObject jClass);
}