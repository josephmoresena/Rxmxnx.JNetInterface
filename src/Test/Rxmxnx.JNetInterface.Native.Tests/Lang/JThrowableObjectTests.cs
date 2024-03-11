namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public sealed class JThrowableObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.ThrowableObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JThrowableObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JThrowableObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JThrowableObjectTests.className,
	                                                   JThrowableObjectTests.classSignature,
	                                                   JThrowableObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ProcessMetadataTest(Boolean useMessage)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JThrowableLocalRef throwableRef = JThrowableObjectTests.fixture.Create<JThrowableLocalRef>();
		String message = JThrowableObjectTests.fixture.Create<String>();
		StackTraceInfo[] stackTrace = JThrowableObjectTests.fixture.CreateMany<StackTraceInfo>().ToArray();
		using JClassObject jClass = new(env);
		using JClassObject jThrowableClass = new(jClass, IClassType.GetMetadata<JThrowableObject>());
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassObject jStackTraceElementClass = new(jClass, IClassType.GetMetadata<JStackTraceElementObject>());
		using JStringObject jStringMessage = new(jStringClass, default, message);
		using JThrowableObject jThrowable = (JThrowableObject)IClassType.GetMetadata<JThrowableObject>()
		                                                                .CreateInstance(
			                                                                jThrowableClass, throwableRef.Value);
		using JArrayObject<JStackTraceElementObject> stackTraceElements =
			new(jStackTraceElementClass, default, stackTrace.Length);
		JStackTraceElementObject[] elements =
			stackTrace.Select(i => i.CreateStackTrace(jStackTraceElementClass)).ToArray();
		ThrowableObjectMetadata throwableMetadata = new(new(jThrowableClass))
		{
			Message = useMessage ? message : default,
		};
		env.FunctionSet.GetMessage(jThrowable).Returns(jStringMessage);
		env.FunctionSet.GetStackTrace(jThrowable).Returns(stackTraceElements);
		env.ArrayFeature.GetElement(stackTraceElements, Arg.Any<Int32>()).Returns(c => elements[(Int32)c[1]]);
		env.WithFrame(Arg.Any<Int32>(), jThrowable, Arg.Any<Func<JThrowableObject, StackTraceInfo[]>>())
		   .Returns(c => (c[2] as Func<JThrowableObject, StackTraceInfo[]>)!.Invoke((JThrowableObject)c[1]));

		ILocalObject.ProcessMetadata(jThrowable, throwableMetadata);

		Assert.Equal(throwableMetadata.ObjectClassName, jThrowable.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, jThrowable.ObjectSignature);
		Assert.Equal(jStringMessage.Value, jThrowable.Message);
		Assert.Equal(stackTrace, jThrowable.StackTrace);

		env.FunctionSet.Received(useMessage ? 0 : 1).GetMessage(jThrowable);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JThrowableObject>();
		String textValue = typeMetadata.ToString();
		String exceptionMessage = JThrowableObjectTests.fixture.Create<String>();
		VirtualMachineProxy vm = Substitute.For<VirtualMachineProxy>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JThrowableObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JThrowableObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JThrowableObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JThrowableObject>()), !env.NoProxy,
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JThrowableObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JThrowableObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JThrowableObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JThrowableObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JThrowableObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JThrowableObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JThrowableObjectTests.hash.ToString(), IDataType.GetHash<JThrowableObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JThrowableObject>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));

		using JThrowableObject jThrowable0 =
			Assert.IsType<JThrowableObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JThrowableObject jThrowable1 =
			Assert.IsType<JThrowableObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JThrowableObject jThrowable2 = Assert.IsType<JThrowableObject>(typeMetadata.ParseInstance(env, jGlobal));
		ThrowableException exception =
			Assert.IsType<ThrowableException<JThrowableObject>>(
				typeMetadata.CreateException(jGlobal, exceptionMessage));

		env.ClassFeature.Received(1).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JThrowableObject>(Arg.Any<JReferenceObject>());
		Assert.Equal(jGlobal, exception.Global);
		Assert.Equal(exceptionMessage, exception.Message);

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}