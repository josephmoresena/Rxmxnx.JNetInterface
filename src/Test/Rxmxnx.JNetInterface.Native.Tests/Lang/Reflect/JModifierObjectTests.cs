namespace Rxmxnx.JNetInterface.Tests.Lang.Reflect;

[ExcludeFromCodeCoverage]
public class JModifierObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.ModifierObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JModifierObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JModifierObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JModifierObjectTests.className,
	                                                   JModifierObjectTests.classSignature,
	                                                   JModifierObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JModifierObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JModifierObjectTests.fixture.Create<JObjectLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jModifierClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JModifierObject jModifier =
			Assert.IsType<JModifierObject>(typeMetadata.CreateInstance(jModifierClass, localRef, true));

		ObjectMetadata objectMetadata = ILocalObject.CreateMetadata(jModifier);

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);

		Assert.True(Object.ReferenceEquals(jModifier, jModifier.CastTo<JLocalObject>()));
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JModifierObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JModifierObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JModifierObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JModifierObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JModifierObject>()), !env.NoProxy,
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JModifierObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JModifierObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JModifierObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JModifierObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JModifierObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JModifierObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JModifierObjectTests.hash.ToString(), IDataType.GetHash<JModifierObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JModifierObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JModifierObject>());
		Assert.Empty(typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JModifierObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JModifierObject jModifier0 =
			Assert.IsType<JModifierObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JModifierObject jModifier1 =
			Assert.IsType<JModifierObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JModifierObject jModifier2 = Assert.IsType<JModifierObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JModifierObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
	[Fact]
	internal void ModifiersTest()
	{
		Assert.True(JModifierObject.PrimitiveModifiers.HasFlag(JModifierObject.Modifiers.Abstract));
		Assert.True(JModifierObject.PrimitiveModifiers.HasFlag(JModifierObject.Modifiers.Final));
		Assert.True(JModifierObject.PrimitiveModifiers.HasFlag(JModifierObject.Modifiers.Public));
	}
}