namespace Rxmxnx.JNetInterface.Tests.Native.Access;

[ExcludeFromCodeCoverage]
public sealed class JMainMethodDefinitionTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void InformationTest()
	{
		Assert.True("main"u8.SequenceEqual(JMainMethodDefinition.Instance.Name));
		Assert.True("([Ljava/lang/String;)V"u8.SequenceEqual(JMainMethodDefinition.Instance.Descriptor));
		Assert.Equal(1, JMainMethodDefinition.Instance.Count);
		Assert.Single(JMainMethodDefinition.Instance.Sizes);
		Assert.Equal(IntPtr.Size, JMainMethodDefinition.Instance.Sizes[0]);
	}
	[Fact]
	internal void InvokeWithNullArgsTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClass = new(env);
		JMainMethodDefinition.Instance.InvokeWithNullArgs(jClass);

		env.AccessFeature.Received(1).CallStaticMethod(jClass, JMainMethodDefinition.Instance,
		                                               Arg.Is<IObject?[]>(i => i.Length == 1 && i[0] == null));
	}
	[Theory]
	[InlineData]
	[InlineData(true)]
	[InlineData(false)]
	internal void InvokeTest(Boolean? nullInput = default)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JArrayLocalRef arrayRef = JMainMethodDefinitionTests.fixture.Create<JArrayLocalRef>();
		String?[]? args = (nullInput.HasValue ?
			!nullInput.Value ?
				JMainMethodDefinitionTests.fixture.CreateMany<String?>(Random.Shared.Next(0, 10)) :
				Enumerable.Repeat(default(String?), Random.Shared.Next(0, 10)) :
			default)?.ToArray();
		using JClassObject jClass = new(env);
		using JClassObject jArrayClass = new(jClass, IDataType.GetMetadata<JArrayObject<JStringObject>>());
		using JClassObject jStringClass = new(jClass, IDataType.GetMetadata<JStringObject>());
		JArrayObject<JStringObject> jArgs = new(jArrayClass, arrayRef, args?.Length);
		IDictionary<String, JStringObject>? jStrings = args?.Where(s => s != null).Select(s => s!).ToHashSet()
		                                                   .ToDictionary(
			                                                   s => s,
			                                                   s => new JStringObject(
				                                                   jStringClass,
				                                                   JMainMethodDefinitionTests.fixture
					                                                   .Create<JStringLocalRef>(), s));

		env.ArrayFeature.CreateArray<JStringObject>(args?.Length ?? default).Returns(jArgs);
		env.StringFeature.Create(Arg.Any<String>()).Returns(c => jStrings?[(c[0] as String)!]);

		JMainMethodDefinition.Instance.Invoke(jClass, args!);
		env.AccessFeature.Received(1).CallStaticMethod(jClass, JMainMethodDefinition.Instance,
		                                               args is not null ?
			                                               Arg.Is<IObject?[]>(
				                                               i => i.Length == 1 &&
					                                               Object.ReferenceEquals(i[0], jArgs)) :
			                                               Arg.Is<IObject?[]>(i => i.Length == 1 && i[0] == null));
		for (Int32 i = 0; i < (args?.Length ?? default); i++)
		{
			if (args![i] is not null)
				env.ArrayFeature.Received().SetElement(jArgs, i, jStrings?[args[i]!]);
		}

		jArgs.Dispose();
	}
}