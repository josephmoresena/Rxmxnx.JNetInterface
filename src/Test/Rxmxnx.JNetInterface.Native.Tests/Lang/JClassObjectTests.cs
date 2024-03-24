namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public sealed class JClassObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = UnicodeClassNames.ClassObject;
	private static readonly CString classSignature = CString.Concat("L"u8, JClassObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JClassObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JClassObjectTests.className, JClassObjectTests.classSignature,
	                                                   JClassObjectTests.arraySignature);

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
		Assert.Equal(jClass.IsInterface, objectMetadata.IsInterface);
		Assert.Equal(jClass.IsEnum, objectMetadata.IsEnum);
		Assert.Equal(jClass.IsAnnotation, objectMetadata.IsAnnotation);
		Assert.Equal(jClass.IsArray,
		             objectMetadata.ObjectSignature[0] == UnicodeObjectSignatures.ArraySignaturePrefixChar);
		Assert.Equal(jClass.ArrayDimension, objectMetadata.ArrayDimension);
		Assert.Equal(jClass.Hash, objectMetadata.Hash);
		Assert.Equal($"{jClass.Name} {jClass.Reference}", jClass.ToString());

		JSerializableObject jSerializable = jClass.CastTo<JSerializableObject>();
		JAnnotatedElementObject jAnnotated = jClass.CastTo<JAnnotatedElementObject>();
		JGenericDeclarationObject jGenericDeclaration = jClass.CastTo<JGenericDeclarationObject>();
		JTypeObject jType = jClass.CastTo<JTypeObject>();

		Assert.Equal(jClass.Id, jSerializable.Id);
		Assert.Equal(jClass.Id, jAnnotated.Id);
		Assert.Equal(jClass.Id, jGenericDeclaration.Id);
		Assert.Equal(jClass.Id, jType.Id);

		Assert.Equal(jClass, jSerializable.Object);
		Assert.Equal(jClass, jAnnotated.Object);
		Assert.Equal(jClass, jGenericDeclaration.Object);
		Assert.Equal(jClass, jType.Object);

		Assert.True(Object.ReferenceEquals(jClass, jClass.CastTo<JLocalObject>()));
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void GetClassNameTest(Boolean isProxy)
	{
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment(isProxy);
		JStringLocalRef stringRef = JClassObjectTests.fixture.Create<JStringLocalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, stringTypeMetadata);
		using JStringObject jString = new(jStringClass, stringRef);

		Assert.Equal(isProxy, jClass.IsProxy);
		Assert.Equal(isProxy, jStringClass.IsProxy);
		Assert.Equal(isProxy, jString.IsProxy);

		env.FunctionSet.GetClassName(jClass).Returns(jString);
		env.FunctionSet.IsPrimitiveClass(jClass).Returns(false);

		Assert.Equal(jString, jClass.GetClassName(out Boolean isPrimitive));
		Assert.False(isPrimitive);

		env.FunctionSet.Received(1).GetClassName(jClass);
	}
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	internal void GetClassInfoTest(Byte call)
	{
		ITypeInformation information = Substitute.For<ITypeInformation>();
		CString className0 = (CString)JClassObjectTests.fixture.Create<String>();
		CString signature0 = (CString)JClassObjectTests.fixture.Create<String>();
		String hash0 = JClassObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		Boolean isFinal = JClassObjectTests.fixture.Create<Boolean>();
		JModifierObject.Modifiers modifiers = (JModifierObject.Modifiers)Random.Shared.Next(1, 0x8000 - 1);
		using JClassObject jClass = new(env);
		using JClassObject jClassObj = new(jClass, classRef);

		env.ClassFeature.GetClassInfo(jClassObj).Returns(information);
		information.ClassName.Returns(className0);
		information.Signature.Returns(signature0);
		information.Hash.Returns(hash0);
		env.FunctionSet.IsFinal(jClassObj, out Arg.Any<JModifierObject.Modifiers>()).ReturnsForAnyArgs(c =>
		{
			c[1] = modifiers;
			return isFinal;
		});

		if (call < 1)
			Assert.Equal(className0, jClassObj.Name);
		if (call < 2)
			Assert.Equal(signature0, jClassObj.ClassSignature);
		if (call < 3)
			Assert.Equal(hash0, jClassObj.Hash);

		Assert.Equal(signature0[0] == UnicodeObjectSignatures.ArraySignaturePrefixChar, jClassObj.IsArray);
		Assert.Equal(signature0.Length == 1, jClassObj.IsPrimitive);
		Assert.Equal(JClassObject.GetArrayDimension(signature0), jClassObj.ArrayDimension);

		if (call < 1)
			Assert.Equal(isFinal, jClassObj.IsFinal);
		if (call < 2)
			Assert.Equal(!isFinal && modifiers.HasFlag(JModifierObject.Modifiers.Interface), jClassObj.IsInterface);
		if (call < 3)
			Assert.Equal(isFinal && modifiers.HasFlag(JModifierObject.Modifiers.Enum), jClassObj.IsEnum);
		if (call < 4)
			Assert.Equal(
				!isFinal && modifiers.HasFlag(JModifierObject.Modifiers.Interface) &&
				modifiers.HasFlag(JModifierObject.Modifiers.Annotation), jClassObj.IsAnnotation);

		env.ClassFeature.Received(1).GetClassInfo(jClassObj);
		env.FunctionSet.Received(1).IsFinal(jClassObj, out Arg.Any<JModifierObject.Modifiers>());

		ClassObjectMetadata metadata = new(jClassObj);
		Assert.Equal(jClassObj.Name, metadata.Name);
		Assert.Equal(jClassObj.ClassSignature, metadata.ClassSignature);
		Assert.Equal(jClassObj.ObjectClassName, metadata.ObjectClassName);
		Assert.Equal(jClassObj.ObjectSignature, metadata.ObjectSignature);
		Assert.Equal(jClassObj.ArrayDimension, metadata.ArrayDimension);
		Assert.Equal(jClassObj.IsAnnotation, metadata.IsAnnotation);
		Assert.Equal(jClassObj.IsFinal, metadata.IsFinal);
		Assert.Equal(jClassObj.IsInterface, metadata.IsInterface);
		Assert.Equal(jClassObj.IsEnum, metadata.IsEnum);
		Assert.Equal(jClassObj.Hash, metadata.Hash);
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

		Assert.Equal($"{classObjectMetadata.Name} {jStringClass.Reference}", jStringClass.ToString());
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
	internal void GetInterfacesTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		JArrayLocalRef arrRef = JClassObjectTests.fixture.Create<JArrayLocalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, classRef);
		using JClassObject jArrayClass = new(jClass, IArrayType.GetArrayMetadata<JArrayObject<JClassObject>>());
		using JArrayObject<JClassObject> interfaces = new(jArrayClass, arrRef, 0);

		env.FunctionSet.GetInterfaces(jClass).Returns(interfaces);
		Assert.Equal(interfaces, jClass.GetInterfaces());
		env.FunctionSet.Received(1).GetInterfaces(jClass);
	}
	[Fact]
	internal void GetSuperClassTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef superClassRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, classRef);
		using JClassObject jSuperClass = new(jClassClass, superClassRef);

		env.ClassFeature.GetSuperClass(jClass).ReturnsForAnyArgs(jSuperClass);

		Assert.Equal(jSuperClass, jClass.GetSuperClass());
		env.ClassFeature.Received(1).GetSuperClass(jClass);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JClassObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JClassObjectTests.fixture.Create<JClassLocalRef>();
		JGlobalRef globalRef = JClassObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClass = new(env);
		using JClassObject jClassResult = new(jClass, classRef);
		using JLocalObject jLocal = new(env, classRef.Value, jClass);
		using JGlobal jGlobal = new(vm, new(jClass), !env.NoProxy, globalRef);
		JReferenceObject jObject = jGlobal;

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JClassObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JClassObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JClassObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JClassObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JClassObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JClassObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JClassObjectTests.hash.ToString(), IDataType.GetHash<JClassObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JClassObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, Array.Empty<JArgumentMetadata>()));
		Assert.IsType<JFieldDefinition<JClassObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JClassObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JClassObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JTypeObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JTypeObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, classRef.Value, jClass, false);

		env.ClassFeature.AsClassObject(classRef).Returns(jClassResult);
		env.ClassFeature.AsClassObject(jLocal).Returns(jClassResult);
		env.ClassFeature.AsClassObject(Arg.Is<JGlobal>(g => jObject.Equals(g))).Returns(jClassResult);
		env.ClassFeature.IsInstanceOf<JClassObject>(jLocal).Returns(true);

		Assert.Equal(jClass, typeMetadata.ParseInstance(jClass, disposeParse));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JGenericDeclarationObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JTypeObject>()));
		Assert.True(typeMetadata.IsInstance(jClassResult));
		Assert.True(typeMetadata.IsInstance(jLocal));
		Assert.Equal(jClassResult, typeMetadata.CreateInstance(jClass, classRef.Value, true));
		Assert.Equal(jClassResult, typeMetadata.ParseInstance(jClassResult, disposeParse));
		Assert.Equal(jClassResult, typeMetadata.ParseInstance(jLocal, disposeParse));
		Assert.Equal(jClassResult, typeMetadata.ParseInstance(env, jGlobal));

		env.ReferenceFeature.Received(1).GetLifetime(Arg.Any<JLocalObject>(), Arg.Any<JObjectLocalRef>(),
		                                             Arg.Any<JClassObject>(), Arg.Any<Boolean>());
		env.ClassFeature.Received(1).AsClassObject(classRef);
		env.ClassFeature.Received(1).AsClassObject(jLocal);
		env.ClassFeature.Received(1).AsClassObject(jGlobal);
		env.ClassFeature.Received(1).IsInstanceOf<JClassObject>(jLocal);

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
	[Fact]
	internal void VoidMetadataTest()
	{
		JPrimitiveTypeMetadata primitiveVoidMetadata = JPrimitiveTypeMetadata.VoidMetadata;
		ClassObjectMetadata voidClassObjectMetadata = ClassObjectMetadata.VoidMetadata;

		Assert.Equal(JClassObjectTests.className, voidClassObjectMetadata.ObjectClassName);
		Assert.Equal(JClassObjectTests.classSignature, voidClassObjectMetadata.ObjectSignature);
		Assert.Equal(primitiveVoidMetadata.ClassName, voidClassObjectMetadata.Name);
		Assert.Equal(primitiveVoidMetadata.Signature, voidClassObjectMetadata.ClassSignature);
		Assert.Equal(primitiveVoidMetadata.Hash, voidClassObjectMetadata.Hash);
		Assert.Equal(0, voidClassObjectMetadata.ArrayDimension);
		Assert.True(voidClassObjectMetadata.IsFinal);
		Assert.False(voidClassObjectMetadata.IsInterface);
		Assert.False(voidClassObjectMetadata.IsAnnotation);
		Assert.False(voidClassObjectMetadata.IsEnum);
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
	[Fact]
	internal void GetArrayDimensionTest()
	{
		CString signature = new(() => "I"u8);
		for (Int32 i = 0; i < 10; i++)
		{
			Assert.Equal(i, JClassObject.GetArrayDimension(signature));
			signature = CString.Concat("["u8, signature);
		}
	}
}