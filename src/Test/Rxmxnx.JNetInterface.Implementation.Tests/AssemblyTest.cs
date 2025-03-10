namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class AssemblyTest
{
	[Fact]
	internal void BaseInternalsTest() => AssemblyTest.InternalTest(typeof(IObject).Assembly);
	[Fact]
	internal void PrimitiveInternalsTest() => AssemblyTest.InternalTest(typeof(JBoolean).Assembly);
	[Fact]
	internal void NativeInternalsTest() => AssemblyTest.InternalTest(typeof(IVirtualMachine).Assembly);
	[Fact]
	internal void ExtensionsInternalsTest() => AssemblyTest.InternalTest(typeof(PrimitiveExtensions).Assembly);
	[Fact]
	internal void ImplementationInternalsTest() => AssemblyTest.InternalTest(typeof(JEnvironment).Assembly);

	private static void InternalTest(Assembly asm)
	{
		Type[] types = asm.GetExportedTypes();
		Assert.DoesNotContain(types, t => t.FullName is { } name && name.Contains("Rxmxnx.JNetInterface.Internal"));
	}
}