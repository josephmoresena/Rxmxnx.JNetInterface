namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public sealed class JLocalObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(CommonNames.Object);
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
		String? textValue = typeMetadata.ToString();
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

		Assert.StartsWith("{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.ToPrintableHash()} }}", textValue);

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
		Assert.IsType<JFunctionDefinition<JLocalObject>.Parameterless>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFunctionDefinition<JLocalObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, [JArgumentMetadata.Get<JStringObject>(),]));
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

		using JLocalObject jLocal0 = typeMetadata.CreateInstance(jObjectClass, localRef, true);
		using JLocalObject jLocal1 = Assert.IsType<JLocalObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JLocalObject jLocal2 = typeMetadata.ParseInstance(env, jGlobal);

		Assert.Equal(jLocal, jLocal0);
		Assert.Equal(jLocal, jLocal1);
		Assert.False(Object.ReferenceEquals(jLocal, jLocal0));
		Assert.True(Object.ReferenceEquals(jLocal, jLocal1));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JLocalObject>(Arg.Any<JReferenceObject>());
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
		Assert.Equal(jLocal.LocalReference, jLocal.LocalAs<JClassLocalRef>().Value);
		Assert.Equal($"{jObjectClass.Name.ToString().Replace('/', '.')} {jLocal.As<JObjectLocalRef>()}",
		             jLocal.ToString());
		Assert.Equal(nCase switch
		{
			2 => jWeak,
			3 => jGlobal,
			_ => default,
		}, jLocal.Lifetime.GetGlobalObject());

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

		env.GetReferenceType(Arg.Any<JWeak>()).Returns(JReferenceType.WeakGlobalRefType);
		env.GetReferenceType(Arg.Any<JGlobal>()).Returns(JReferenceType.GlobalRefType);

		Assert.Equal(nCase > 1, jLocal.Lifetime.HasValidGlobal<JWeak>());
		Assert.Equal(nCase > 2, jLocal.Lifetime.HasValidGlobal<JGlobal>());

		env.Received(nCase > 1 ? 1 : 0).GetReferenceType(jWeak);
		env.Received(nCase > 2 ? 1 : 0).GetReferenceType(jGlobal);
	}

	[Fact]
	internal void SetValueTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
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

	[Theory]
	[InlineData]
	[InlineData(true)]
	internal void SetGlobal(Boolean useWeak = false)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JLocalObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JLocalObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JLocalObjectTests.fixture.Create<JGlobalRef>();
		JWeakRef weakRef = JLocalObjectTests.fixture.Create<JWeakRef>();

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

		Assert.Null(jLocal.Lifetime.GetGlobalObject());

		using JGlobalBase jGlobal = !useWeak ? new JGlobal(jLocal, globalRef) : new JWeak(jLocal, weakRef);

		jLocal.Lifetime.SetGlobal(jGlobal);

		Assert.Equal(jGlobal, jLocal.Lifetime.GetGlobalObject());
		Assert.Equal(localRef, jLocal.LocalReference);
		Assert.Equal(localRef, jLocal.LocalAs<JObjectLocalRef>());
		Assert.Equal(!useWeak ? globalRef.Value : weakRef.Value, jLocal.Reference);
		Assert.Equal(!useWeak, globalRef == jLocal.To<JGlobalRef>());
		Assert.Equal(!useWeak, globalRef == jLocal.As<JGlobalRef>());
		Assert.Equal(useWeak, weakRef == jLocal.To<JWeakRef>());
		Assert.Equal(useWeak, weakRef == jLocal.As<JWeakRef>());
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void IsInstanceOfTest(Byte nCase)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JLocalObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = nCase > 0 ? JLocalObjectTests.fixture.Create<JObjectLocalRef>() : default;
		JWeakRef weakRef = nCase > 1 ? JLocalObjectTests.fixture.Create<JWeakRef>() : default;
		JGlobalRef globalRef = nCase > 2 ? JLocalObjectTests.fixture.Create<JGlobalRef>() : default;
		Boolean result = JLocalObjectTests.fixture.Create<Boolean>();

		vm.InitializeThread(Arg.Any<CString?>()).Returns(thread);
		env.ReferenceFeature.Unload(Arg.Any<JGlobal>()).Returns(true);
		env.IsValidationAvoidable(Arg.Any<JGlobalBase>()).Returns(true);

		using IDisposable synchronizer = Substitute.For<IDisposable>();
		using JClassObject jClassClass = new(env);
		using JClassObject jObjectClass = new(jClassClass, IClassType.GetMetadata<JLocalObject>(), classRef);
		using JLocalObject jLocal = new(jObjectClass, localRef);

		env.ClassFeature.GetObjectClass(jLocal).Returns(jObjectClass);
		env.ReferenceFeature.GetSynchronizer(Arg.Any<JReferenceObject>()).Returns(synchronizer);
		env.GetReferenceType(Arg.Any<JWeak>()).Returns(JReferenceType.WeakGlobalRefType);
		env.GetReferenceType(Arg.Any<JGlobal>()).Returns(JReferenceType.GlobalRefType);
		env.ClassFeature.IsInstanceOf<JSerializableObject>(Arg.Any<JReferenceObject>()).Returns(result);

		using JWeak jWeak = new(jLocal, weakRef);
		using JGlobal jGlobal = new(jLocal, globalRef);

		env.ReferenceFeature.Create<JGlobal>(jLocal).Returns(jGlobal);
		env.ReferenceFeature.Create<JWeak>(jLocal).Returns(jWeak);

		Assert.Equal(jWeak, jLocal.Weak);
		Assert.Equal(jGlobal, jLocal.Global);

		Assert.Equal(result, jLocal.InstanceOf<JSerializableObject>());

		env.ClassFeature.Received(nCase < 2 ? 1 : 0).IsInstanceOf<JSerializableObject>(jLocal);
		env.ClassFeature.Received(nCase == 2 ? 1 : 0).IsInstanceOf<JSerializableObject>(jWeak);
		env.ClassFeature.Received(nCase > 2 ? 1 : 0).IsInstanceOf<JSerializableObject>(jGlobal);

		env.ReferenceFeature.ClearReceivedCalls();
		env.ClassFeature.ClearReceivedCalls();

		jLocal.SetAssignableTo<JSerializableObject>(result);

		Assert.Equal(result, jLocal.InstanceOf<JSerializableObject>());
		env.ClassFeature.Received(0).IsInstanceOf<JSerializableObject>(Arg.Any<JReferenceObject>());

		if (nCase > 1)
		{
			Assert.Equal(jWeak, jLocal.Weak);
			env.ReferenceFeature.Received(0).Create<JWeak>(jLocal);
		}
		if (nCase > 2)
		{
			Assert.Equal(jGlobal, jLocal.Global);
			env.ReferenceFeature.Received(0).Create<JGlobal>(jLocal);
		}
	}
}