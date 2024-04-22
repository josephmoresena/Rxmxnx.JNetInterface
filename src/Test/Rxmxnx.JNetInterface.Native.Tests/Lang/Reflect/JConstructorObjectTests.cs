namespace Rxmxnx.JNetInterface.Tests.Lang.Reflect;

[ExcludeFromCodeCoverage]
public class JConstructorObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.ConstructorObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JConstructorObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JConstructorObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JConstructorObjectTests.className,
	                                                   JConstructorObjectTests.classSignature,
	                                                   JConstructorObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ConstructorClassTest(Boolean initDefinition)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JConstructorObject>();
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JConstructorObjectTests.fixture.Create<JObjectLocalRef>();
		JArrayLocalRef arrayRef = JConstructorObjectTests.fixture.Create<JArrayLocalRef>();
		JMethodId methodId = JConstructorObjectTests.fixture.Create<JMethodId>();
		JConstructorDefinition methodDefinition = new();
		using JClassObject jClass = new(env);
		using JClassObject jConstructorClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, stringTypeMetadata);
		using JClassObject jClassArrayClass = new(jClass, IArrayType.GetMetadata<JArrayObject<JClassObject>>());
		using JStringObject jStringConstructorName =
			new(jStringClass, default, methodDefinition.Information[0].ToString());
		using JArrayObject<JClassObject> jArrayParameters = new(jClassArrayClass, arrayRef, 0);
		using JConstructorObject jConstructor = initDefinition ?
			new(jConstructorClass, localRef, methodDefinition, jClass) :
			Assert.IsType<JConstructorObject>(typeMetadata.CreateInstance(jConstructorClass, localRef));

		env.FunctionSet.GetDeclaringClass(jConstructor).Returns(jClass);
		env.FunctionSet.GetName(jConstructor).Returns(jStringConstructorName);
		env.FunctionSet.GetReturnType(jConstructor).Returns(default(JClassObject));
		env.FunctionSet.GetParameterTypes(jConstructor).Returns(jArrayParameters);
		env.ClassFeature.GetClass(jClass.Hash).Returns(jClass);
		env.AccessFeature.GetDefinition(jStringConstructorName, jArrayParameters, default).Returns(methodDefinition);
		env.AccessFeature.GetMethodId(jConstructor).Returns(methodId.Pointer);

		Assert.Equal(methodDefinition, jConstructor.Definition);
		Assert.Equal(jClass, jConstructor.DeclaringClass);
		Assert.Equal(methodId, jConstructor.MethodId);
		Assert.Equal(methodId.Pointer, jConstructor.MethodId.Pointer);

		env.FunctionSet.Received(initDefinition ? 0 : 1).GetDeclaringClass(jConstructor);
		env.FunctionSet.Received(initDefinition ? 0 : 1).GetName(jConstructor);
		env.FunctionSet.Received(initDefinition ? 0 : 1).GetReturnType(jConstructor);
		env.ClassFeature.Received(initDefinition ? 1 : 0).GetClass(jClass.Hash);
		env.AccessFeature.Received(initDefinition ? 0 : 1)
		   .GetDefinition(jStringConstructorName, jArrayParameters, default);
		env.AccessFeature.Received(1).GetMethodId(jConstructor);

		jConstructor.ClearValue();
		Assert.Null(Assert.IsType<ExecutableObjectMetadata>(ILocalObject.CreateMetadata(jConstructor)).MethodId);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void CreateMetadataTest(Boolean useMetadata)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JConstructorObject>();
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JConstructorObjectTests.fixture.Create<JObjectLocalRef>();
		JArrayLocalRef arrayRef = JConstructorObjectTests.fixture.Create<JArrayLocalRef>();
		JConstructorDefinition methodDefinition = new();
		JMethodId methodId = JConstructorObjectTests.fixture.Create<JMethodId>();
		using JClassObject jClass = new(env);
		using JClassObject jConstructorClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, stringTypeMetadata);
		using JClassObject jClassArrayClass = new(jClass, IArrayType.GetMetadata<JArrayObject<JClassObject>>());
		using JStringObject jStringConstructorName =
			new(jStringClass, default, methodDefinition.Information[0].ToString());
		using JArrayObject<JClassObject> jArrayParameters = new(jClassArrayClass, arrayRef, 0);
		using JConstructorObject jConstructor =
			Assert.IsType<JConstructorObject>(typeMetadata.CreateInstance(jConstructorClass, localRef, true));

		env.FunctionSet.GetDeclaringClass(jConstructor).Returns(jClass);
		env.FunctionSet.GetName(jConstructor).Returns(jStringConstructorName);
		env.FunctionSet.GetReturnType(jConstructor).Returns(default(JClassObject));
		env.FunctionSet.GetParameterTypes(jConstructor).Returns(jArrayParameters);
		env.ClassFeature.GetClass(jClass.Hash).Returns(jClass);
		env.AccessFeature.GetDefinition(jStringConstructorName, jArrayParameters, default).Returns(methodDefinition);
		env.AccessFeature.GetMethodId(jConstructor).Returns(methodId.Pointer);

		ILocalObject.ProcessMetadata(jConstructor,
		                             useMetadata ?
			                             new ExecutableObjectMetadata(new(jConstructorClass))
			                             {
				                             Definition = methodDefinition,
				                             MethodId = methodId,
				                             ClassHash = jClass.Hash,
			                             } :
			                             new ObjectMetadata(jConstructorClass));

		ExecutableObjectMetadata objectMetadata =
			Assert.IsType<ExecutableObjectMetadata>(ILocalObject.CreateMetadata(jConstructor));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(methodDefinition, objectMetadata.Definition);
		Assert.Equal(jClass.Hash, objectMetadata.ClassHash);
		Assert.Equal(useMetadata ? methodId : null, objectMetadata.MethodId);
		Assert.Equal(objectMetadata, new(objectMetadata));

		Assert.Equal(jClass, jConstructor.DeclaringClass);

		JAnnotatedElementObject jAnnotatedElement = jConstructor.CastTo<JAnnotatedElementObject>();
		JGenericDeclarationObject jGenericDeclaration = jConstructor.CastTo<JGenericDeclarationObject>();
		JMemberObject jMember = jConstructor.CastTo<JMemberObject>();

		Assert.Equal(jConstructor.Id, jAnnotatedElement.Id);
		Assert.Equal(jConstructor.Id, jGenericDeclaration.Id);
		Assert.Equal(jConstructor.Id, jMember.Id);

		Assert.Equal(jConstructor, jAnnotatedElement.Object);
		Assert.Equal(jConstructor, jGenericDeclaration.Object);
		Assert.Equal(jConstructor, jMember.Object);

		Assert.True(Object.ReferenceEquals(jConstructor, jConstructor.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jConstructor, jConstructor.CastTo<JAccessibleObject>()));
		Assert.True(Object.ReferenceEquals(jConstructor, jConstructor.CastTo<JExecutableObject>()));

		env.FunctionSet.Received(useMetadata ? 0 : 1).GetDeclaringClass(jConstructor);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetName(jConstructor);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetReturnType(jConstructor);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetParameterTypes(jConstructor);
		env.ClassFeature.Received(1).GetClass(jClass.Hash);
		env.AccessFeature.Received(useMetadata ? 0 : 1)
		   .GetDefinition(jStringConstructorName, jArrayParameters, default);
		env.AccessFeature.Received(0).GetMethodId(jConstructor);

		Assert.Equal(methodId, jConstructor.MethodId);
		Assert.Equal(methodId.Pointer, jConstructor.MethodId.Pointer);

		env.AccessFeature.Received(useMetadata ? 0 : 1).GetMethodId(jConstructor);
		jConstructor.ClearValue();
		Assert.Null(Assert.IsType<ExecutableObjectMetadata>(ILocalObject.CreateMetadata(jConstructor)).MethodId);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JConstructorObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JConstructorObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JConstructorObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JConstructorObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JConstructorObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JConstructorObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JConstructorObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JConstructorObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JConstructorObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JConstructorObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JConstructorObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JConstructorObjectTests.hash.ToString(), IDataType.GetHash<JConstructorObject>());
		Assert.Equal(IDataType.GetMetadata<JExecutableObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JConstructorObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JConstructorObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JConstructorObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JConstructorObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JMemberObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JMemberObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JConstructorObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JGenericDeclarationObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JConstructorObject jConstructor0 =
			Assert.IsType<JConstructorObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JConstructorObject jConstructor1 =
			Assert.IsType<JConstructorObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JConstructorObject jConstructor2 =
			Assert.IsType<JConstructorObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JConstructorObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}