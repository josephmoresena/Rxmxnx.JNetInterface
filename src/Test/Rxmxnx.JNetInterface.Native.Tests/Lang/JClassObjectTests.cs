namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public sealed class JClassObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ConstructorClassTest(Boolean isProxy)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JClassObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment(isProxy);
		JClassObject jClass = new(env);
		ObjectLifetime lifetime = jClass.Lifetime;
		ClassObjectMetadata objectMetadata = Assert.IsType<ClassObjectMetadata>(ILocalObject.CreateMetadata(jClass));

		Assert.Equal(env, jClass.Environment);
		Assert.Equal(default, jClass.Reference);
		Assert.True(jClass.IsFinal);
		Assert.True(jClass.IsDefault);
		Assert.NotInRange(jClass.Id, Int64.MinValue, default);
		Assert.Equal(isProxy, jClass.IsProxy);
		Assert.Equal(jClass, jClass.Class);

		Assert.Equal(typeMetadata.ClassName, jClass.Name);
		Assert.Equal(typeMetadata.Signature, jClass.ClassSignature);
		Assert.Equal(typeMetadata.ClassName, jClass.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, jClass.ObjectSignature);
		Assert.Equal(typeMetadata.Hash, jClass.Hash);

		jClass.Dispose();

		env.ReferenceFeature.Received(1).IsParameter(jClass);
		env.ReferenceFeature.Received(1).Unload(jClass);

		Assert.False(lifetime.IsDisposed);
		Assert.True(lifetime.IsRealClass);
		Assert.Equal(jClass.Class, lifetime.Class);
		Assert.Equal(jClass.Environment, lifetime.Environment);
		Assert.Equal(jClass.Id, lifetime.Id);
		Assert.Equal(IntPtr.Size, lifetime.Span.Length);
		Assert.Equal(IntPtr.Zero, lifetime.Span.AsValue<IntPtr>());

		Assert.Equal(jClass.ObjectClassName, objectMetadata.ObjectClassName);
		Assert.Equal(jClass.ObjectSignature, objectMetadata.ObjectSignature);
		Assert.Equal(jClass.Name, objectMetadata.Name);
		Assert.Equal(jClass.ClassSignature, objectMetadata.ClassSignature);
		Assert.Equal(jClass.IsFinal, objectMetadata.IsFinal);
		Assert.Equal(jClass.Hash, objectMetadata.Hash);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void GetClassNameTest(Boolean isProxy)
	{
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment(isProxy);
		NativeFunctionSet functionSet = Substitute.For<NativeFunctionSet>();
		JStringLocalRef stringRef = JClassObjectTests.fixture.Create<JStringLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, stringTypeMetadata);
		using JStringObject jString = new(jStringClass, stringRef);

		env.FunctionSet.Returns(functionSet);
		functionSet.GetClassName(jClass).Returns(jString);
		functionSet.IsPrimitiveClass(jClass).Returns(false);

		Assert.Equal(jString, jClass.GetClassName(out Boolean isPrimitive));
		Assert.False(isPrimitive);

		functionSet.Received(1).GetClassName(jClass);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, 1)]
	[InlineData(false, 1)]
	[InlineData(true, 2)]
	[InlineData(false, 2)]
	internal void GetClassInfoTest(Boolean isProxy, Byte c = 0)
	{
		ITypeInformation information = Substitute.For<ITypeInformation>();
		CString className = (CString)JClassObjectTests.fixture.Create<String>();
		CString signature = (CString)JClassObjectTests.fixture.Create<String>();
		String hash = JClassObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment(isProxy);
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jClassObj = new(jClass, classRef);

		env.ClassFeature.GetClassInfo(jClassObj).Returns(information);
		information.ClassName.Returns(className);
		information.Signature.Returns(signature);
		information.Hash.Returns(hash);

		if (c < 1)
			Assert.Equal(className, jClassObj.Name);
		if (c < 2)
			Assert.Equal(signature, jClassObj.ClassSignature);
		if (c < 3)
			Assert.Equal(hash, jClassObj.Hash);
		env.ClassFeature.Received(1).GetClassInfo(jClassObj);
	}
}