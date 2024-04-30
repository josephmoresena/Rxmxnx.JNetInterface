namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public sealed class JLocalObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.Object);
	private static readonly CString classSignature = CString.Concat("L"u8, JLocalObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JLocalObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JLocalObjectTests.className, JLocalObjectTests.classSignature,
	                                                   JLocalObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JLocalObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JLocalObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JLocalObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JLocalObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jObjectClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jObjectClass);
		using JGlobal jGlobal = new(vm, new(jObjectClass, IClassType.GetMetadata<JLocalObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JLocalObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JLocalObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JLocalObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JLocalObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JLocalObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JLocalObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JLocalObjectTests.hash.ToString(), IDataType.GetHash<JLocalObject>());
		Assert.Null(typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JLocalObject>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JLocalObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JLocalObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JLocalObject>());
		Assert.Empty(typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JLocalObject>().Returns(jObjectClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jObjectClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jObjectClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JLocalObject jLocal0 =
			Assert.IsType<JLocalObject>(typeMetadata.CreateInstance(jObjectClass, localRef, true));
		using JLocalObject jLocal1 = Assert.IsType<JLocalObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JLocalObject jLocal2 = Assert.IsType<JLocalObject>(typeMetadata.ParseInstance(env, jGlobal));

		Assert.Equal(jLocal, jLocal0);

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JLocalObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void GetSynchronizeTest(Byte nCase)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JLocalObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = nCase > 0 ? JLocalObjectTests.fixture.Create<JObjectLocalRef>() : default;
		JWeakRef weakRef = nCase > 1 ? JLocalObjectTests.fixture.Create<JWeakRef>() : default;
		JGlobalRef globalRef = nCase > 2 ? JLocalObjectTests.fixture.Create<JGlobalRef>() : default;

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		env.ReferenceFeature.Unload(Arg.Any<JGlobal>()).Returns(true);
		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

		using IDisposable synchronizer = Substitute.For<IDisposable>();
		using JClassObject jClassClass = new(env);
		using JClassObject jObjectClass = new(jClassClass, IClassType.GetMetadata<JLocalObject>(), classRef);
		using JLocalObject jLocal = new(jObjectClass, localRef);

		env.ClassFeature.GetObjectClass(jLocal).Returns(jObjectClass);
		env.ReferenceFeature.GetSynchronizer(Arg.Any<JReferenceObject>()).Returns(synchronizer);
		env.ClassFeature.IsInstanceOf(Arg.Any<JReferenceObject>(), jObjectClass).Returns(true);

		using JWeak jWeak = new(jLocal, weakRef);
		using JGlobal jGlobal = new(jLocal, globalRef);

		env.ReferenceFeature.Create<JGlobal>(jLocal).Returns(jGlobal);
		env.ReferenceFeature.Create<JWeak>(jLocal).Returns(jWeak);

		Assert.Equal(jWeak, jLocal.Weak);
		Assert.Equal(jGlobal, jLocal.Global);
		Assert.Equal(nCase == 0, jLocal.IsDefaultInstance());
		Assert.Equal(jLocal.InternalReference, jLocal.InternalAs<JClassLocalRef>().Value);
		Assert.Equal($"{jObjectClass.Name} {jLocal.As<JObjectLocalRef>()}", jLocal.ToString());

		Assert.True(jLocal.InstanceOf(jObjectClass));

		env.ClassFeature.Received(nCase <= 1 ? 1 : 0).IsInstanceOf(jLocal, jObjectClass);
		env.ClassFeature.Received(nCase == 2 ? 1 : 0).IsInstanceOf(jWeak, jObjectClass);
		env.ClassFeature.Received(nCase == 3 ? 1 : 0).IsInstanceOf(jGlobal, jObjectClass);

		switch (nCase)
		{
			case <= 1:
				Assert.Equal(localRef, jLocal.To<JObjectLocalRef>());
				break;
			case 2:
				Assert.Equal(weakRef.Value, jLocal.To<JObjectLocalRef>());
				break;
			case 3:
				Assert.Equal(globalRef.Value, jLocal.To<JObjectLocalRef>());
				break;
		}

		if (nCase == 0)
		{
			Assert.Null(jLocal.Synchronize());
			env.ReferenceFeature.Received(0).GetSynchronizer(Arg.Any<JReferenceObject>());
		}
		else
		{
			Assert.Equal(synchronizer, jLocal.Synchronize());
			env.ReferenceFeature.Received(nCase == 1 ? 1 : 0).GetSynchronizer(jLocal);
			env.ReferenceFeature.Received(nCase == 2 ? 1 : 0).GetSynchronizer(jWeak);
			env.ReferenceFeature.Received(nCase == 3 ? 1 : 0).GetSynchronizer(jGlobal);
		}
	}

	[Fact]
	internal void SetValueTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JLocalObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JLocalObjectTests.fixture.Create<JObjectLocalRef>();
		JArrayLocalRef arrayRef = JLocalObjectTests.fixture.Create<JArrayLocalRef>();
		JThrowableLocalRef throwableRef = JLocalObjectTests.fixture.Create<JThrowableLocalRef>();

		using JClassObject jClassClass = new(env);
		using JClassObject jObjectClass = new(jClassClass, IClassType.GetMetadata<JLocalObject>(), classRef);
		using JLocalObject jLocal = new(jObjectClass, default);

		Assert.Equal(default, jLocal.Reference);

		jLocal.SetValue(localRef);
		Assert.Equal(localRef, jLocal.Reference);

		jLocal.SetValue(arrayRef);
		Assert.Equal(arrayRef.Value, jLocal.Reference);

		jLocal.SetValue(throwableRef);
		Assert.Equal(throwableRef.Value, jLocal.Reference);
	}
}