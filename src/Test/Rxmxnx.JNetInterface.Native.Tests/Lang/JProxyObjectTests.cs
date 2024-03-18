namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public class JProxyObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.ProxyObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JProxyObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JProxyObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JProxyObjectTests.className, JProxyObjectTests.classSignature,
	                                                   JProxyObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JProxyObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JProxyObjectTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jProxyClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JProxyObject jProxy =
			Assert.IsType<JProxyObject>(typeMetadata.CreateInstance(jProxyClass, localRef, true));

		ObjectMetadata objectMetadata = ILocalObject.CreateMetadata(jProxy);

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);

		Assert.True(Object.ReferenceEquals(jProxy, jProxy.CastTo<JLocalObject>()));
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JProxyObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JProxyObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JProxyObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JProxyObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JProxyObject>()), !env.NoProxy,
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JProxyObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JProxyObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JProxyObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JProxyObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JProxyObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JProxyObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JProxyObjectTests.hash.ToString(), IDataType.GetHash<JProxyObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JProxyObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, Array.Empty<JArgumentMetadata>()));
		Assert.IsType<JFieldDefinition<JProxyObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JProxyObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JProxyObject>());
		Assert.Empty(typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JProxyObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));
		Assert.Equal(typeMetadata, JProxyObject.ProxyTypeMetadata);

		using JProxyObject jProxy0 =
			Assert.IsType<JProxyObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JProxyObject jProxy1 = Assert.IsType<JProxyObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JProxyObject jProxy2 = Assert.IsType<JProxyObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JProxyObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}