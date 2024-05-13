namespace Rxmxnx.JNetInterface.Tests.Lang.Reflect;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public class JMethodObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.MethodObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JMethodObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JMethodObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JMethodObjectTests.className, JMethodObjectTests.classSignature,
	                                                   JMethodObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ConstructorClassTest(Boolean initDefinition)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JMethodObject>();
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JMethodObjectTests.fixture.Create<JObjectLocalRef>();
		JArrayLocalRef arrayRef = JMethodObjectTests.fixture.Create<JArrayLocalRef>();
		JMethodId methodId = JMethodObjectTests.fixture.Create<JMethodId>();
		JMethodDefinition methodDefinition = new("methodName"u8);
		using JClassObject jClass = new(env);
		using JClassObject jMethodClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, stringTypeMetadata);
		using JClassObject jClassArrayClass = new(jClass, IArrayType.GetMetadata<JArrayObject<JClassObject>>());
		using JStringObject jStringMethodName = new(jStringClass, default, methodDefinition.Name.ToString());
		using JArrayObject<JClassObject> jArrayParameters = new(jClassArrayClass, arrayRef, 0);
		using JMethodObject jMethod = initDefinition ?
			new(jMethodClass, localRef, methodDefinition, jClass) :
			Assert.IsType<JMethodObject>(typeMetadata.CreateInstance(jMethodClass, localRef));

		env.FunctionSet.GetDeclaringClass(jMethod).Returns(jClass);
		env.FunctionSet.GetName(jMethod).Returns(jStringMethodName);
		env.FunctionSet.GetReturnType(jMethod).Returns(default(JClassObject));
		env.FunctionSet.GetParameterTypes(jMethod).Returns(jArrayParameters);
		env.ClassFeature.GetClass(jClass.Hash).Returns(jClass);
		env.AccessFeature.GetDefinition(jStringMethodName, jArrayParameters, default).Returns(methodDefinition);
		env.AccessFeature.GetMethodId(jMethod).Returns(methodId);

		Assert.Equal(methodDefinition, jMethod.Definition);
		Assert.Equal(jClass, jMethod.DeclaringClass);
		Assert.Equal(methodId, jMethod.MethodId);
		Assert.Equal(methodId.Pointer, jMethod.MethodId.Pointer);

		env.FunctionSet.Received(initDefinition ? 0 : 1).GetDeclaringClass(jMethod);
		env.FunctionSet.Received(initDefinition ? 0 : 1).GetName(jMethod);
		env.FunctionSet.Received(initDefinition ? 0 : 1).GetReturnType(jMethod);
		env.ClassFeature.Received(initDefinition ? 1 : 0).GetClass(jClass.Hash);
		env.AccessFeature.Received(initDefinition ? 0 : 1).GetDefinition(jStringMethodName, jArrayParameters, default);
		env.AccessFeature.Received(1).GetMethodId(jMethod);

		jMethod.ClearValue();
		Assert.Null(Assert.IsType<ExecutableObjectMetadata>(ILocalObject.CreateMetadata(jMethod)).MethodId);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void CreateMetadataTest(Boolean useMetadata)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JMethodObject>();
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JMethodObjectTests.fixture.Create<JObjectLocalRef>();
		JArrayLocalRef arrayRef = JMethodObjectTests.fixture.Create<JArrayLocalRef>();
		JMethodDefinition methodDefinition = new("methodName"u8);
		JMethodId methodId = JMethodObjectTests.fixture.Create<JMethodId>();
		using JClassObject jClass = new(env);
		using JClassObject jMethodClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, stringTypeMetadata);
		using JClassObject jClassArrayClass = new(jClass, IArrayType.GetMetadata<JArrayObject<JClassObject>>());
		using JStringObject jStringMethodName = new(jStringClass, default, methodDefinition.Name.ToString());
		using JArrayObject<JClassObject> jArrayParameters = new(jClassArrayClass, arrayRef, 0);
		using JMethodObject jMethod =
			Assert.IsType<JMethodObject>(typeMetadata.CreateInstance(jMethodClass, localRef, true));

		env.FunctionSet.GetDeclaringClass(jMethod).Returns(jClass);
		env.FunctionSet.GetName(jMethod).Returns(jStringMethodName);
		env.FunctionSet.GetReturnType(jMethod).Returns(default(JClassObject));
		env.FunctionSet.GetParameterTypes(jMethod).Returns(jArrayParameters);
		env.ClassFeature.GetClass(jClass.Hash).Returns(jClass);
		env.AccessFeature.GetDefinition(jStringMethodName, jArrayParameters, default).Returns(methodDefinition);
		env.AccessFeature.GetMethodId(jMethod).Returns(methodId);

		ILocalObject.ProcessMetadata(
			jMethod,
			useMetadata ?
				new ExecutableObjectMetadata(new(jMethodClass))
				{
					Definition = methodDefinition, MethodId = methodId, ClassHash = jClass.Hash,
				} :
				new ObjectMetadata(jMethodClass));

		ExecutableObjectMetadata objectMetadata =
			Assert.IsType<ExecutableObjectMetadata>(ILocalObject.CreateMetadata(jMethod));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(methodDefinition, objectMetadata.Definition);
		Assert.Equal(jClass.Hash, objectMetadata.ClassHash);
		Assert.Equal(useMetadata ? methodId : null, objectMetadata.MethodId);
		Assert.Equal(objectMetadata, new(objectMetadata));

		Assert.Equal(jClass, jMethod.DeclaringClass);

		JAnnotatedElementObject jAnnotatedElement = jMethod.CastTo<JAnnotatedElementObject>();
		JGenericDeclarationObject jGenericDeclaration = jMethod.CastTo<JGenericDeclarationObject>();
		JMemberObject jMember = jMethod.CastTo<JMemberObject>();

		Assert.Equal(jMethod.Id, jAnnotatedElement.Id);
		Assert.Equal(jMethod.Id, jGenericDeclaration.Id);
		Assert.Equal(jMethod.Id, jMember.Id);

		Assert.Equal(jMethod, jAnnotatedElement.Object);
		Assert.Equal(jMethod, jGenericDeclaration.Object);
		Assert.Equal(jMethod, jMember.Object);

		Assert.True(Object.ReferenceEquals(jMethod, jMethod.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jMethod, jMethod.CastTo<JAccessibleObject>()));
		Assert.True(Object.ReferenceEquals(jMethod, jMethod.CastTo<JExecutableObject>()));

		env.FunctionSet.Received(useMetadata ? 0 : 1).GetDeclaringClass(jMethod);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetName(jMethod);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetReturnType(jMethod);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetParameterTypes(jMethod);
		env.ClassFeature.Received(1).GetClass(jClass.Hash);
		env.AccessFeature.Received(useMetadata ? 0 : 1).GetDefinition(jStringMethodName, jArrayParameters, default);
		env.AccessFeature.Received(0).GetMethodId(jMethod);

		Assert.Equal(methodId, jMethod.MethodId);
		Assert.Equal(methodId.Pointer, jMethod.MethodId.Pointer);

		env.AccessFeature.Received(useMetadata ? 0 : 1).GetMethodId(jMethod);
		jMethod.ClearValue();
		Assert.Null(Assert.IsType<ExecutableObjectMetadata>(ILocalObject.CreateMetadata(jMethod)).MethodId);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JMethodObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JMethodObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JMethodObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JMethodObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JMethodObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JMethodObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JMethodObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JMethodObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JMethodObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JMethodObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JMethodObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JMethodObjectTests.hash.ToString(), IDataType.GetHash<JMethodObject>());
		Assert.Equal(IDataType.GetMetadata<JExecutableObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JMethodObject>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JMethodObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JMethodObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JMethodObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JMemberObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JMemberObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JMethodObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JGenericDeclarationObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JMethodObject jMethod0 =
			Assert.IsType<JMethodObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JMethodObject jMethod1 = Assert.IsType<JMethodObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JMethodObject jMethod2 = Assert.IsType<JMethodObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JMethodObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}