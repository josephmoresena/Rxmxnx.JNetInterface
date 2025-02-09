namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	[Flags]
	private enum Publish : Byte
	{
		SelfContained = 0x0,
		ReadyToRun = 0x1,
		NativeAot = 0x2,
		JniLibrary = 0x4,
		NoReflection = 0x80,
	}

	private enum NetVersion : Byte
	{
		Net80 = 0,
		Net90 = 1,
	}
}