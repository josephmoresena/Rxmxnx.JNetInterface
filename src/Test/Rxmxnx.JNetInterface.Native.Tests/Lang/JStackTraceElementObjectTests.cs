namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public class JStackTraceElementObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.StackTraceElementObject);
	private static readonly CString classSignature =
		CString.Concat("L"u8, JStackTraceElementObjectTests.className, ";"u8);
	private static readonly CString
		arraySignature = CString.Concat("["u8, JStackTraceElementObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JStackTraceElementObjectTests.className,
	                                                   JStackTraceElementObjectTests.classSignature,
	                                                   JStackTraceElementObjectTests.arraySignature);

	[Fact]
	internal void CreateMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStackTraceElementObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = JStackTraceElementObjectTests.fixture.Create<JObjectLocalRef>();
		StackTraceInfo info = JStackTraceElementObjectTests.fixture.Create<StackTraceInfo>();
		using JClassObject jClass = new(env);
		using JClassObject jStackTraceElementClass = new(jClass, typeMetadata);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringClassName = new(jStringClass, default, info.ClassName);
		using JStringObject jStringFileName = new(jStringClass, default, info.FileName);
		using JStringObject jStringMethodName = new(jStringClass, default, info.MethodName);
		using JStackTraceElementObject jStackTraceElement =
			Assert.IsType<JStackTraceElementObject>(
				typeMetadata.CreateInstance(jStackTraceElementClass, localRef, true));

		env.FunctionSet.GetLineNumber(jStackTraceElement).Returns(info.LineNumber);
		env.FunctionSet.IsNativeMethod(jStackTraceElement).Returns(info.NativeMethod);
		env.FunctionSet.GetClassName(jStackTraceElement).Returns(jStringClassName);
		env.FunctionSet.GetFileName(jStackTraceElement).Returns(jStringFileName);
		env.FunctionSet.GetMethodName(jStackTraceElement).Returns(jStringMethodName);

		StackTraceElementObjectMetadata objectMetadata =
			Assert.IsType<StackTraceElementObjectMetadata>(ILocalObject.CreateMetadata(jStackTraceElement));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.Equal(info, objectMetadata.Information);
		Assert.Equal(objectMetadata, new(objectMetadata));

		JSerializableObject jSerializable = jStackTraceElement.CastTo<JSerializableObject>();
		Assert.Equal(jStackTraceElement.Id, jSerializable.Id);
		Assert.Equal(jStackTraceElement, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jStackTraceElement, jStackTraceElement.CastTo<JLocalObject>()));

		env.FunctionSet.Received(1).GetLineNumber(jStackTraceElement);
		env.FunctionSet.Received(1).IsNativeMethod(jStackTraceElement);
		env.FunctionSet.Received(1).GetClassName(jStackTraceElement);
		env.FunctionSet.Received(1).GetFileName(jStackTraceElement);
		env.FunctionSet.Received(1).GetMethodName(jStackTraceElement);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStackTraceElementObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JStackTraceElementObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = JStackTraceElementObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = JStackTraceElementObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jStackTraceElementClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jStackTraceElementClass);
		using JGlobal jGlobal =
			new(vm, new(jStackTraceElementClass, IClassType.GetMetadata<JStackTraceElementObject>()), !env.NoProxy,
			    globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JStackTraceElementObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JStackTraceElementObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JStackTraceElementObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JStackTraceElementObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JStackTraceElementObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JStackTraceElementObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JStackTraceElementObjectTests.hash.ToString(), IDataType.GetHash<JStackTraceElementObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JStackTraceElementObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, Array.Empty<JArgumentMetadata>()));
		Assert.IsType<JFieldDefinition<JStackTraceElementObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JStackTraceElementObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JStackTraceElementObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JStackTraceElementObject>().Returns(jStackTraceElementClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jStackTraceElementClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jStackTraceElementClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JStackTraceElementObject jStackTraceElement0 =
			Assert.IsType<JStackTraceElementObject>(
				typeMetadata.CreateInstance(jStackTraceElementClass, localRef, true));
		using JStackTraceElementObject jStackTraceElement1 =
			Assert.IsType<JStackTraceElementObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JStackTraceElementObject jStackTraceElement2 =
			Assert.IsType<JStackTraceElementObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JStackTraceElementObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}