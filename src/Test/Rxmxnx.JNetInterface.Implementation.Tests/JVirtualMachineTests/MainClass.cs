namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	[Flags]
	internal enum MainClass : UInt32
	{
		None = 0x0,
		Class = 0x1,
		Throwable = 0x2,
		StackTraceElement = 0x4,
		VoidObject = 0x8,
		VoidPrimitive = 0x10,
		BooleanObject = 0x20,
		BooleanPrimitive = 0x40,
		ByteObject = 0x80,
		BytePrimitive = 0x100,
		CharacterObject = 0x200,
		CharPrimitive = 0x400,
		DoubleObject = 0x800,
		DoublePrimitive = 0x1000,
		FloatObject = 0x2000,
		FloatPrimitive = 0x4000,
		IntegerObject = 0x8000,
		IntPrimitive = 0x10000,
		LongObject = 0x20000,
		LongPrimitive = 0x40000,
		ShortObject = 0x80000,
		ShortPrimitive = 0x100000,
	}
}