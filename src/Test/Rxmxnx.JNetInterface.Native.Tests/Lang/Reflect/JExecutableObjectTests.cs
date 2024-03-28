namespace Rxmxnx.JNetInterface.Tests.Lang.Reflect;

[ExcludeFromCodeCoverage]
public class JExecutableObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.ExecutableObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JExecutableObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JExecutableObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JExecutableObjectTests.className,
	                                                   JExecutableObjectTests.classSignature,
	                                                   JExecutableObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void CreateMetadataTest(Boolean useMetadata)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JExecutableObject>();
		JClassTypeMetadata stringTypeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JExecutableObjectTests.fixture.Create<JObjectLocalRef>();
		JArrayLocalRef arrayRef = JExecutableObjectTests.fixture.Create<JArrayLocalRef>();
		JNonTypedFunctionDefinition executableDefinition = new("functionName"u8, stringTypeMetadata.Signature);
		JMethodId methodId = JExecutableObjectTests.fixture.Create<JMethodId>();
		using JClassObject jClass = new(env);
		using JClassObject jExecutableClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassObject jClassArrayClass = new(jClass, IArrayType.GetMetadata<JArrayObject<JClassObject>>());
		using JStringObject jStringExecutableName =
			new(jStringClass, default, executableDefinition.Information[0].ToString());
		using JArrayObject<JClassObject> jArrayParameters = new(jClassArrayClass, arrayRef, 0);
		using JExecutableObject jExecutable =
			Assert.IsType<JExecutableObject>(typeMetadata.CreateInstance(jExecutableClass, localRef, true));

		env.FunctionSet.GetDeclaringClass(jExecutable).Returns(jClass);
		env.FunctionSet.GetName(jExecutable).Returns(jStringExecutableName);
		env.FunctionSet.GetReturnType(jExecutable).Returns(jStringClass);
		env.FunctionSet.GetParameterTypes(jExecutable).Returns(jArrayParameters);
		env.ClassFeature.GetClass(jClass.Hash).Returns(jClass);
		env.AccessFeature.GetDefinition(jStringExecutableName, jArrayParameters, jStringClass)
		   .Returns(executableDefinition);
		env.AccessFeature.GetMethodId(jExecutable).Returns(methodId.Pointer);

		ILocalObject.ProcessMetadata(jExecutable,
		                             useMetadata ?
			                             new ExecutableObjectMetadata(new(jExecutableClass))
			                             {
				                             Definition = executableDefinition,
				                             MethodId = methodId,
				                             ClassHash = jClass.Hash,
			                             } :
			                             new ObjectMetadata(jExecutableClass));

		ExecutableObjectMetadata objectMetadata =
			Assert.IsType<ExecutableObjectMetadata>(ILocalObject.CreateMetadata(jExecutable));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(executableDefinition, objectMetadata.Definition);
		Assert.Equal(jClass.Hash, objectMetadata.ClassHash);
		Assert.Equal(useMetadata ? methodId : null, objectMetadata.MethodId);
		Assert.Equal(objectMetadata, new(objectMetadata));

		Assert.Equal(jClass, jExecutable.DeclaringClass);

		JAnnotatedElementObject jAnnotatedElement = jExecutable.CastTo<JAnnotatedElementObject>();
		JGenericDeclarationObject jGenericDeclaration = jExecutable.CastTo<JGenericDeclarationObject>();
		JMemberObject jMember = jExecutable.CastTo<JMemberObject>();

		Assert.Equal(jExecutable.Id, jAnnotatedElement.Id);
		Assert.Equal(jExecutable.Id, jGenericDeclaration.Id);
		Assert.Equal(jExecutable.Id, jMember.Id);

		Assert.Equal(jExecutable, jAnnotatedElement.Object);
		Assert.Equal(jExecutable, jGenericDeclaration.Object);
		Assert.Equal(jExecutable, jMember.Object);

		Assert.True(Object.ReferenceEquals(jExecutable, jExecutable.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jExecutable, jExecutable.CastTo<JAccessibleObject>()));

		env.FunctionSet.Received(useMetadata ? 0 : 1).GetDeclaringClass(jExecutable);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetName(jExecutable);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetReturnType(jExecutable);
		env.FunctionSet.Received(useMetadata ? 0 : 1).GetParameterTypes(jExecutable);
		env.ClassFeature.Received(1).GetClass(jClass.Hash);
		env.AccessFeature.Received(useMetadata ? 0 : 1).GetDefinition(jStringExecutableName, jArrayParameters,
		                                                              jStringClass);
		env.AccessFeature.Received(0).GetMethodId(jExecutable);

		Assert.Equal(methodId, jExecutable.MethodId);
		Assert.Equal(methodId.Pointer, jExecutable.MethodId.Pointer);

		env.AccessFeature.Received(useMetadata ? 0 : 1).GetMethodId(jExecutable);
		jExecutable.ClearValue();
		Assert.Null(Assert.IsType<ExecutableObjectMetadata>(ILocalObject.CreateMetadata(jExecutable)).MethodId);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JExecutableObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JExecutableObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JExecutableObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JExecutableObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JExecutableObject>()), !env.NoProxy,
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Abstract, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JExecutableObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JExecutableObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JExecutableObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JExecutableObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JExecutableObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JExecutableObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JExecutableObjectTests.hash.ToString(), IDataType.GetHash<JExecutableObject>());
		Assert.Equal(IDataType.GetMetadata<JAccessibleObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JExecutableObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, Array.Empty<JArgumentMetadata>()));
		Assert.IsType<JFieldDefinition<JExecutableObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JExecutableObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JExecutableObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JMemberObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JGenericDeclarationObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JMemberObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JExecutableObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JAnnotatedElementObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JGenericDeclarationObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JExecutableObject jExecutable0 =
			Assert.IsType<JExecutableObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JExecutableObject jExecutable1 =
			Assert.IsType<JExecutableObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JExecutableObject jExecutable2 =
			Assert.IsType<JExecutableObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JExecutableObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}