using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial interface IManagedCallback
{
	internal static virtual IVirtualMachine TypeVirtualMachine { get; set; } = default!;
	internal static virtual TextWriter TypeWriter { get; set; } = default!;

	IVirtualMachine VirtualMachine { get; }
	TextWriter Writer { get; }

	JStringObject? GetHelloString(JLocalObject jLocal);
	JInt GetThreadId(JLocalObject jLocal);
	void PrintRuntimeInformation(JLocalObject jLocal, JStringObject? jString);

	void ProcessField(JLocalObject jLocal);
	void Throw(JLocalObject jLocal);

	static abstract JIntegerObject? SumArray(JClassObject jClass, JArrayObject<JInt>? jArray);
	static abstract JArrayObject<JArrayObject<JInt>>? GetIntArrayArray(JClassObject jClass, Int32 length);
	static abstract void PrintClass(JClassObject jClass, TextWriter writer);
	static abstract JClassObject GetVoidClass(JClassObject jClass);
	static abstract JArrayObject<JClassObject> GetPrimitiveClasses(JClassObject jClass);

	public static void PrintSwitches()
	{
		Console.WriteLine("==== Feature Switches ====");
		Console.WriteLine($"{nameof(IManagedCallback)}: {typeof(IManagedCallback)}");
		Console.WriteLine($"{nameof(JVirtualMachine.TraceEnabled)}: {JVirtualMachine.TraceEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.MetadataValidationEnabled)}: {IVirtualMachine.MetadataValidationEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.JaggedArrayAutoGenerationEnabled)}: {IVirtualMachine.JaggedArrayAutoGenerationEnabled}");
		Console.WriteLine(
			$"{nameof(IVirtualMachine.TypeMetadataToStringEnabled)}: {IVirtualMachine.TypeMetadataToStringEnabled}");
		Console.WriteLine(
			$"{nameof(JVirtualMachine.FinalUserTypeRuntimeEnabled)}: {JVirtualMachine.FinalUserTypeRuntimeEnabled}");
	}
}