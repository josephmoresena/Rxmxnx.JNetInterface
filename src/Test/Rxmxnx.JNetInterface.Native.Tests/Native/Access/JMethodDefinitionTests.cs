namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class JMethodDefinitionTests
{
	private const String VoidParameterlessDescriptor = "()V";

	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	private void SimpleTest()
	{
		String methodName = JMethodDefinitionTests.fixture.Create<String>();
		CStringSequence seq = new(methodName, JMethodDefinitionTests.VoidParameterlessDescriptor);
		JMethodDefinition methodDefinition = new((CString)methodName);
		Assert.Null(methodDefinition.Return);
		Assert.Equal(seq.ToString(), methodDefinition.Information.ToString());
		Assert.Equal(methodDefinition.Information.GetHashCode(), methodDefinition.GetHashCode());

		Assert.False(methodDefinition.Equals(default));
		Assert.True(methodDefinition.Equals((Object)methodDefinition));
		Assert.True(methodDefinition.Equals((Object)new JMethodDefinition((CString)methodName)));

		Assert.Equal($"{{ Method: {methodName} Descriptor: {JMethodDefinitionTests.VoidParameterlessDescriptor} }}",
		             methodDefinition.ToString());
	}
}