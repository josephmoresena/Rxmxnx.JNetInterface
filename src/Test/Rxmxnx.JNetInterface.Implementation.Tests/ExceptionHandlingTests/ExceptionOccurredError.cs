namespace Rxmxnx.JNetInterface.Tests;

public partial class ExceptionHandlingTests
{
	[Flags]
	internal enum ExceptionOccurredError : UInt16
	{
		None = 0x0,
		GetClassObject = 0x1,
		GetClassNameMethod = 0x2,
		GetClassNameString = 0x4,
		ClassNameUtfLength = 0x8,
		ClassNameUtfChars = 0x10,
		GetThrowableMessageMethod = 0x20,
		GetThrowableMessageString = 0x40,
		ThrowableMessageLength = 0x80,
		ThrowableMessageGetRegion = 0x100,
		NewGlobalRef = 0x200,
	}
}