namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public class JAccessibleObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.AccessibleObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JAccessibleObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JAccessibleObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JAccessibleObjectTests.className,
	                                                   JAccessibleObjectTests.classSignature,
	                                                   JAccessibleObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JAccessibleObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JAccessibleObjectTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jAccessibleClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JAccessibleObject jAccessible =
			Assert.IsType<JAccessibleObject>(typeMetadata.CreateInstance(jAccessibleClass, localRef, true));

		ObjectMetadata objectMetadata = ILocalObject.CreateMetadata(jAccessible);

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);

		JAnnotatedElementObject jSerializable = jAccessible.CastTo<JAnnotatedElementObject>();
		Assert.Equal(jAccessible.Id, jSerializable.Id);
		Assert.Equal(jAccessible, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jAccessible, jAccessible.CastTo<JLocalObject>()));
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JAccessibleObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JAccessibleObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JAccessibleObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JAccessibleObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JAccessibleObject>()), !env.NoProxy,
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JAccessibleObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JAccessibleObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JAccessibleObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JAccessibleObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JAccessibleObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JAccessibleObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JAccessibleObjectTests.hash.ToString(), IDataType.GetHash<JAccessibleObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JAccessibleObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JAccessibleObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JAccessibleObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JAccessibleObject jAccessible0 =
			Assert.IsType<JAccessibleObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JAccessibleObject jAccessible1 =
			Assert.IsType<JAccessibleObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JAccessibleObject jAccessible2 =
			Assert.IsType<JAccessibleObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JAccessibleObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}