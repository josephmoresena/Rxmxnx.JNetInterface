namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public sealed class JVoidObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new("java/lang/Void"u8);
	private static readonly CString classSignature = CString.Concat("L"u8, JVoidObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JVoidObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JVoidObjectTests.className, JVoidObjectTests.classSignature,
	                                                   JVoidObjectTests.arraySignature);

	[Fact]
	internal void MetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JVoidObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JVoidObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JVoidObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JVoidObjectTests.fixture.Create<JGlobalRef>();

		using JClassObject jClassClass = new(env);
		using JClassObject jVoidObjectClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jVoidObjectClass);
		using JGlobal jGlobal = new(vm, new(jVoidObjectClass, IClassType.GetMetadata<JVoidObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JVoidObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JVoidObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JVoidObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JVoidObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JVoidObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JVoidObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JVoidObjectTests.hash.ToString(), IDataType.GetHash<JVoidObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JVoidObject>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JVoidObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JVoidObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JVoidObject>());
		Assert.Empty(typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JVoidObject>().Returns(jVoidObjectClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jVoidObjectClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jVoidObjectClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		Assert.Throws<InvalidOperationException>(() => typeMetadata.CreateInstance(jVoidObjectClass, localRef, true));
		Assert.Throws<InvalidOperationException>(() => typeMetadata.ParseInstance(jLocal));
		Assert.Throws<InvalidOperationException>(() => typeMetadata.ParseInstance(jLocal, true));
		Assert.Throws<InvalidOperationException>(() => typeMetadata.ParseInstance(env, jGlobal));
		Assert.Throws<InvalidOperationException>(() => new JVoidObject());

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JVoidObject>(Arg.Any<JReferenceObject>());
	}
}