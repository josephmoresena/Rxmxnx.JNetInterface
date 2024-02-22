namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public static class ReferenceExtensions
{
	public static JClassLocalRef GetClassRef(this IntPtr ptr) => ptr.Transform<IntPtr, JClassLocalRef>();
}