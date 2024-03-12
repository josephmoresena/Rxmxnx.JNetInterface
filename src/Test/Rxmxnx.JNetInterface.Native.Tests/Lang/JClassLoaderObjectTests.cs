namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public class JClassLoaderObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.ClassLoaderObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JClassLoaderObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JClassLoaderObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JClassLoaderObjectTests.className,
	                                                   JClassLoaderObjectTests.classSignature,
	                                                   JClassLoaderObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JClassLoaderObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JClassLoaderObjectTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jNumberClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassLoaderObject jNumber =
			Assert.IsType<JClassLoaderObject>(typeMetadata.CreateInstance(jNumberClass, localRef, true));

		ObjectMetadata objectMetadata = ILocalObject.CreateMetadata(jNumber);

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JClassLoaderObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JClassLoaderObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JClassLoaderObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JClassLoaderObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JClassLoaderObject>()),
		                            !env.NoProxy, globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JClassLoaderObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JClassLoaderObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JClassLoaderObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JClassLoaderObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JClassLoaderObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JClassLoaderObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JClassLoaderObjectTests.hash.ToString(), IDataType.GetHash<JClassLoaderObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JClassLoaderObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JStringObject>());
		Assert.Empty(typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JClassLoaderObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JClassLoaderObject jNumber0 =
			Assert.IsType<JClassLoaderObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JClassLoaderObject jNumber1 =
			Assert.IsType<JClassLoaderObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JClassLoaderObject jNumber2 = Assert.IsType<JClassLoaderObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JClassLoaderObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}