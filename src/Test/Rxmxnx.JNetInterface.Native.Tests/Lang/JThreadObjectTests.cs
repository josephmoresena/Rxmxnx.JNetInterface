namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public class JThreadObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new("java/lang/Thread"u8);
	private static readonly CString classSignature = CString.Concat("L"u8, JThreadObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JThreadObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JThreadObjectTests.className, JThreadObjectTests.classSignature,
	                                                   JThreadObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JThreadObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JThreadObjectTests.fixture.Create<JObjectLocalRef>();
		Boolean? isVirtual = JThreadObjectTests.fixture.Create<Boolean?>();
		using JClassObject jClass = new(env);
		using JClassObject jThreadClass = new(jClass, typeMetadata);
		using JThreadObject jThread =
			Assert.IsType<JThreadObject>(typeMetadata.CreateInstance(jThreadClass, localRef, true));

		ObjectMetadata objectMetadata = ILocalObject.CreateMetadata(jThread);

		Assert.True(Object.ReferenceEquals(jThread, jThread.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jThread, (jThread as ILocalObject).CastTo<JLocalObject>()));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);

		JRunnableObject jRunnable = jThread.CastTo<JRunnableObject>();
		Assert.Equal(jThread.Id, jRunnable.Id);
		Assert.Equal(jThread, jRunnable.Object);

		env.IsVirtual(jThread).Returns(isVirtual);
		Assert.Equal(isVirtual, jThread.IsVirtual);
		env.Received(1).IsVirtual(jThread);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JThreadObject>();
		String? textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JThreadObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JThreadObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JThreadObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThreadClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jThreadClass);
		using JGlobal jGlobal = new(vm, new(jThreadClass, IClassType.GetMetadata<JThreadObject>()), globalRef);

		Assert.StartsWith("{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.ToPrintableHash()} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JThreadObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JThreadObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JThreadObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JThreadObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JThreadObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JThreadObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JThreadObjectTests.hash.ToString(), IDataType.GetHash<JThreadObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JThreadObject>.Parameterless>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFunctionDefinition<JThreadObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, [JArgumentMetadata.Get<JStringObject>(),]));
		Assert.IsType<JFieldDefinition<JThreadObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JThreadObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JThreadObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JRunnableObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JRunnableObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JThreadObject>().Returns(jThreadClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jThreadClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThreadClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JRunnableObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JThreadObject jThread0 =
			Assert.IsType<JThreadObject>(typeMetadata.CreateInstance(jThreadClass, localRef, true));
		using JThreadObject jThread1 = Assert.IsType<JThreadObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JThreadObject jThread2 = Assert.IsType<JThreadObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JThreadObject>(Arg.Any<JReferenceObject>());

		Assert.True(typeMetadata.IsInstance(jThread0));
		Assert.True(typeMetadata.IsInstance(jThread1));
		Assert.True(typeMetadata.IsInstance(jThread2));
	}
}