namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public sealed class JClassObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString classSignature = CString.Concat("L"u8, UnicodeClassNames.ClassObject, ";"u8);
	private static readonly CString arraySignature = CString.Concat("[L"u8, UnicodeClassNames.ClassObject, ";"u8);

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

		Assert.Equal(isProxy, jClass.IsProxy);
		Assert.Equal(isProxy, jStringClass.IsProxy);
		Assert.Equal(isProxy, jString.IsProxy);

		env.FunctionSet.Returns(functionSet);
		functionSet.GetClassName(jClass).Returns(jString);
		functionSet.IsPrimitiveClass(jClass).Returns(false);

		Assert.Equal(jString, jClass.GetClassName(out Boolean isPrimitive));
		Assert.False(isPrimitive);

		functionSet.Received(1).GetClassName(jClass);
	}
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	internal void GetClassInfoTest(Byte c)
	{
		ITypeInformation information = Substitute.For<ITypeInformation>();
		CString className = (CString)JClassObjectTests.fixture.Create<String>();
		CString signature = (CString)JClassObjectTests.fixture.Create<String>();
		String hash = JClassObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
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
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void RegisterTest(Boolean useList)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jClassObj = new(jClass, classRef);

		JMethodDefinition methodDefinition = JMethodDefinition.Create("methodName"u8);
		JFunctionDefinition<JClassObject> functionDefinition =
			JFunctionDefinition<JClassObject>.Create("functionName"u8);

		JNativeCallEntry entry1 = JNativeCallEntry.Create(methodDefinition, IntPtr.Zero);
		JNativeCallEntry entry2 = JNativeCallEntry.Create(functionDefinition, IntPtr.Zero);
		if (!useList)
			jClassObj.Register(entry1, entry2);
		else
			jClassObj.Register([entry1, entry2,]);

		env.AccessFeature.Received(1)
		   .RegisterNatives(
			   jClassObj, Arg.Is<IReadOnlyList<JNativeCallEntry>>(l => entry1.Equals(l[0]) && entry2.Equals(l[1])));

		jClassObj.UnregisterNativeCalls();
		env.AccessFeature.Received(1).ClearNatives(jClassObj);
	}
	[Fact]
	internal void ProcessMetadataTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, classRef);
		ObjectMetadata objectMetadata = new(jClass);
		ClassObjectMetadata classObjectMetadata = ClassObjectMetadata.Create<JStringObject>();

		ILocalObject.ProcessMetadata(jStringClass, objectMetadata);

		Assert.Equal(objectMetadata.ObjectClassName, jStringClass.ObjectClassName);
		Assert.Equal(objectMetadata.ObjectSignature, jStringClass.ObjectSignature);
		Assert.Null(jStringClass.Name);
		Assert.Null(jStringClass.ClassSignature);

		ILocalObject.ProcessMetadata(jStringClass, classObjectMetadata);

		env.ClassFeature.Received(0).GetClass(Arg.Any<CString>());

		Assert.Equal(classObjectMetadata.ObjectClassName, jStringClass.ObjectClassName);
		Assert.Equal(classObjectMetadata.ObjectSignature, jStringClass.ObjectSignature);
		Assert.Equal(classObjectMetadata.Name, jStringClass.Name);
		Assert.Equal(classObjectMetadata.ClassSignature, jStringClass.ClassSignature);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void AssignationTest(Boolean isAssignable)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef0 = JClassObjectTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = JClassObjectTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jClass0 = new(jClass, classRef0);
		using JClassObject jClass1 = new(jClass, classRef1);

		env.ClassFeature.IsAssignableFrom(jClass1, jClass0).Returns(isAssignable);
		Assert.Equal(isAssignable, jClass0.IsAssignableTo(jClass1));
	}
	[Fact]
	internal void MetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JClassObject>();
		VirtualMachineProxy vm = Substitute.For<VirtualMachineProxy>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		JGlobalRef globalRef = JClassObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jClassResult = new(jClass, classRef);
		using JLocalObject jLocal = new(env, classRef.Value, jClass);
		using JGlobal jGlobal = new(vm, new(jClass), !env.NoProxy, globalRef);

		Assert.Equal(UnicodeClassNames.ClassObject, typeMetadata.ClassName);
		Assert.Equal(JClassObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JClassObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JTypeObject>(), typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, classRef.Value, jClass, false);

		env.ClassFeature.AsClassObject(classRef).Returns(jClassResult);
		env.ClassFeature.AsClassObject(jLocal).Returns(jClassResult);
		env.ClassFeature.AsClassObject(jGlobal).Returns(jClassResult);

		Assert.Equal(jClass, typeMetadata.ParseInstance(jClass));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Equal(jClassResult, typeMetadata.CreateInstance(jClass, classRef.Value, true));
		Assert.Equal(jClassResult, typeMetadata.ParseInstance(jLocal));
		Assert.Equal(jClassResult, typeMetadata.ParseInstance(env, jGlobal));

		env.ReferenceFeature.Received(1).GetLifetime(Arg.Any<JLocalObject>(), Arg.Any<JObjectLocalRef>(),
		                                             Arg.Any<JClassObject>(), Arg.Any<Boolean>());
		env.ClassFeature.Received(1).AsClassObject(classRef);
		env.ClassFeature.Received(1).AsClassObject(jLocal);
		env.ClassFeature.Received(1).AsClassObject(jGlobal);
		env.ClassFeature.Received(0).IsAssignableTo<JClassObject>(Arg.Any<JReferenceObject>());
	}
	[Fact]
	internal void GetClassTest()
	{
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jClassResult = new(jClass, classRef);

		env.ClassFeature.GetClass<JStringObject>().Returns(jClassResult);
		env.ClassFeature.GetClass(stringTypeMetadata.ClassName).Returns(jClassResult);

		Assert.Equal(jClassResult, JClassObject.GetClass(env, stringTypeMetadata.ClassName));
		Assert.Equal(jClassResult, JClassObject.GetClass<JStringObject>(env));
	}
	[Fact]
	internal void LoadClassTest()
	{
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		Byte[] rawByte = JClassObjectTests.fixture.CreateMany<Byte>().ToArray();
		using JClassObject jClass = new(env);
		using JClassObject jClassResult = new(jClass, classRef);

		env.ClassFeature.LoadClass(stringTypeMetadata.ClassName, Arg.Is<Byte[]>(a => a.SequenceEqual(rawByte)))
		   .Returns(jClassResult);
		env.ClassFeature.LoadClass<JStringObject>(Arg.Is<Byte[]>(a => a.SequenceEqual(rawByte))).Returns(jClassResult);

		Assert.Equal(jClassResult, JClassObject.LoadClass(env, stringTypeMetadata.ClassName, rawByte));
		Assert.Equal(jClassResult, JClassObject.LoadClass<JStringObject>(env, rawByte));
	}
}