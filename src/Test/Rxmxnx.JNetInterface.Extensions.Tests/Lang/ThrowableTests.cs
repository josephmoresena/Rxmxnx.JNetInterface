namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public sealed class ThrowableTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly MethodInfo getMetadata =
		typeof(IClassType).GetMethod(nameof(IClassType.GetMetadata), BindingFlags.Public | BindingFlags.Static)!;
	private static readonly MethodInfo interfaceExceptionTest =
		typeof(ThrowableTests).GetMethod(nameof(ThrowableTests.InterfaceExceptionTest),
		                                 BindingFlags.Static | BindingFlags.NonPublic)!;

	[Fact]
	internal void ArrayIndexOutOfBoundsExceptionTest()
		=> ThrowableTests.ThrowableTest<JArrayIndexOutOfBoundsExceptionObject>();
	[Fact]
	internal void ArrayStoreExceptionObjectTest() => ThrowableTests.ThrowableTest<JArrayStoreExceptionObject>();
	[Fact]
	internal void ClassCircularityErrorTest() => ThrowableTests.ThrowableTest<JClassCircularityErrorObject>();
	[Fact]
	internal void ClassFormatErrorTest() => ThrowableTests.ThrowableTest<JClassFormatErrorObject>();
	[Fact]
	internal void ExceptionInInitializerErrorTest()
		=> ThrowableTests.ThrowableTest<JExceptionInInitializerErrorObject>();
	[Fact]
	internal void IncompatibleClassChangeErrorTest()
		=> ThrowableTests.ThrowableTest<JIncompatibleClassChangeErrorObject>();
	[Fact]
	internal void IndexOutOfBoundsExceptionTest() => ThrowableTests.ThrowableTest<JIndexOutOfBoundsExceptionObject>();
	[Fact]
	internal void InstantiationExceptionTest() => ThrowableTests.ThrowableTest<JInstantiationExceptionObject>();
	[Fact]
	internal void LinkageErrorTest() => ThrowableTests.ThrowableTest<JLinkageErrorObject>();
	[Fact]
	internal void NoClassDefFoundErrorObjectTest() => ThrowableTests.ThrowableTest<JNoClassDefFoundErrorObject>();
	[Fact]
	internal void ClassNotFoundExceptionObjectTest() => ThrowableTests.ThrowableTest<JClassNotFoundExceptionObject>();
	[Fact]
	internal void NullPointerExceptionTest() => ThrowableTests.ThrowableTest<JNullPointerExceptionObject>();
	[Fact]
	internal void NoSuchFieldErrorTest() => ThrowableTests.ThrowableTest<JNoSuchFieldErrorObject>();
	[Fact]
	internal void NoSuchMethodErrorTest() => ThrowableTests.ThrowableTest<JNoSuchMethodErrorObject>();
	[Fact]
	internal void OutOfMemoryErrorTest() => ThrowableTests.ThrowableTest<JOutOfMemoryErrorObject>();
	[Fact]
	internal void ReflectiveOperationExceptionTest()
		=> ThrowableTests.ThrowableTest<JReflectiveOperationExceptionObject>();
	[Fact]
	internal void SecurityExceptionTest() => ThrowableTests.ThrowableTest<JSecurityExceptionObject>();
	[Fact]
	internal void StringIndexOutOfBoundsExceptionTest()
		=> ThrowableTests.ThrowableTest<JStringIndexOutOfBoundsExceptionObject>();
	[Fact]
	internal void VirtualMachineErrorTest() => ThrowableTests.ThrowableTest<JVirtualMachineErrorObject>();
	[Fact]
	internal void FileNotFoundExceptionTest() => ThrowableTests.ThrowableTest<JFileNotFoundExceptionObject>();
	[Fact]
	internal void IoExceptionTest() => ThrowableTests.ThrowableTest<JIoExceptionObject>();
	[Fact]
	internal void MalformedUrlExceptionTest() => ThrowableTests.ThrowableTest<JMalformedUrlExceptionObject>();
	[Fact]
	internal void InvocationTargetExceptionTest() => ThrowableTests.ThrowableTest<JInvocationTargetExceptionObject>();
	[Fact]
	internal void ParseExceptionTest() => ThrowableTests.ThrowableTest<JParseExceptionObject>();
	[Fact]
	internal void ArithmeticExceptionTest() => ThrowableTests.ThrowableTest<JArithmeticExceptionObject>();
	[Fact]
	internal void ClassCastExceptionTest() => ThrowableTests.ThrowableTest<JClassCastExceptionObject>();
	[Fact]
	internal void IllegalArgumentExceptionTest() => ThrowableTests.ThrowableTest<JIllegalArgumentExceptionObject>();
	[Fact]
	internal void IllegalStateExceptionTest() => ThrowableTests.ThrowableTest<JIllegalStateExceptionObject>();
	[Fact]
	internal void InternalErrorTest() => ThrowableTests.ThrowableTest<JInternalErrorObject>();
	[Fact]
	internal void InterruptedExceptionTest() => ThrowableTests.ThrowableTest<JInterruptedExceptionObject>();
	[Fact]
	internal void NumberFormatExceptionTest() => ThrowableTests.ThrowableTest<JNumberFormatExceptionObject>();
	[Fact]
	internal void UnsatisfiedLinkErrorTest() => ThrowableTests.ThrowableTest<JUnsatisfiedLinkErrorObject>();
	[Fact]
	internal void IllegalAccessExceptionTest() => ThrowableTests.ThrowableTest<JIllegalAccessExceptionObject>();

	private static void ThrowableTest<TThrowable>() where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		ThrowableTests.ProcessMetadataTest<TThrowable>(false);
		ThrowableTests.ProcessMetadataTest<TThrowable>(true);
		ThrowableTests.ProcessMetadataTest<TThrowable>(false, true);
		ThrowableTests.ProcessMetadataTest<TThrowable>(true, true);
		ThrowableTests.MetadataTest<TThrowable>(false);
		ThrowableTests.MetadataTest<TThrowable>(true);
		ThrowableTests.ThrowTest<TThrowable>(false);
		ThrowableTests.ThrowTest<TThrowable>(true);
	}
	private static void ProcessMetadataTest<TThrowable>(Boolean useMessage, Boolean emptyStackTrace = false)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JThrowableLocalRef throwableRef = ThrowableTests.fixture.Create<JThrowableLocalRef>();
		String message = ThrowableTests.fixture.Create<String>();
		StackTraceInfo[] stackTrace = emptyStackTrace ?
			ThrowableTests.fixture.CreateMany<StackTraceInfo>().ToArray() :
			[];
		using JClassObject jClass = new(env);
		using JClassObject jThrowableClass = new(jClass, IClassType.GetMetadata<TThrowable>());
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassObject jStackTraceElementClass = new(jClass, IClassType.GetMetadata<JStackTraceElementObject>());
		using JStringObject jStringMessage = new(jStringClass, default, message);
		using TThrowable jThrowable = Assert.IsType<TThrowable>(IClassType.GetMetadata<TThrowable>()
		                                                                  .CreateInstance(
			                                                                  jThrowableClass, throwableRef.Value));
		using JArrayObject<JStackTraceElementObject> stackTraceElements =
			new(jStackTraceElementClass, default, stackTrace.Length);
		JStackTraceElementObject[] elements =
			stackTrace.Select(i => i.CreateStackTrace(jStackTraceElementClass)).ToArray();
		ThrowableObjectMetadata throwableMetadata =
			new(new(jThrowableClass)) { Message = useMessage ? message : default, };
		env.FunctionSet.GetMessage(jThrowable).Returns(jStringMessage);
		env.FunctionSet.GetStackTrace(jThrowable).Returns(stackTraceElements);
		env.ArrayFeature.GetElement(stackTraceElements, Arg.Any<Int32>()).Returns(c => elements[(Int32)c[1]]);
		env.WithFrame(Arg.Any<Int32>(), stackTraceElements,
		              Arg.Any<Func<JArrayObject<JStackTraceElementObject>, StackTraceInfo[]>>()).Returns(
			c => (c[2] as Func<JArrayObject<JStackTraceElementObject>, StackTraceInfo[]>)!.Invoke(
				(JArrayObject<JStackTraceElementObject>)c[1]));

		ILocalObject.ProcessMetadata(jThrowable, throwableMetadata);

		Assert.Equal(throwableMetadata.ObjectClassName, jThrowable.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, jThrowable.ObjectSignature);
		Assert.Equal(jStringMessage.Value, jThrowable.Message);
		Assert.Equal(stackTrace, jThrowable.StackTrace);

		ThrowableObjectMetadata objectMetadata =
			Assert.IsType<ThrowableObjectMetadata>(ILocalObject.CreateMetadata(jThrowable));
		Assert.Equal(throwableMetadata.ObjectClassName, objectMetadata.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, objectMetadata.ObjectSignature);
		Assert.Equal(throwableMetadata.Message ?? jStringMessage.Value, objectMetadata.Message);
		Assert.Equal(objectMetadata, new(objectMetadata));

		JSerializableObject jSerializable = jThrowable.CastTo<JSerializableObject>();
		Assert.Equal(jThrowable.Id, jSerializable.Id);
		Assert.Equal(jThrowable, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jThrowable, jThrowable.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jThrowable, jThrowable.CastTo<JThrowableObject>()));

		env.FunctionSet.Received(useMessage ? 0 : 1).GetMessage(jThrowable);

		Assert.Equal(throwableRef, jThrowable.Reference);
	}
	private static void MetadataTest<TThrowable>(Boolean disposeParse)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TThrowable>();
		String? textValue = typeMetadata.ToString();
		String exceptionMessage = ThrowableTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = ThrowableTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = ThrowableTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = ThrowableTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<TThrowable>()), globalRef);

		Assert.StartsWith("{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.ToPrintableHash()} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<TThrowable>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(TThrowable), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(ThrowableTests.getMetadata.MakeGenericMethod(typeof(TThrowable).BaseType!).Invoke(default, []),
		             typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<TThrowable>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<TThrowable>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JThrowableObject), EnvironmentProxy.GetFamilyType<JExceptionObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JExceptionObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<TThrowable>().Returns(jThrowableClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jThrowableClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jThrowableClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JThrowableObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));

		using TThrowable jThrowable0 =
			Assert.IsType<TThrowable>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using TThrowable jThrowable1 = Assert.IsType<TThrowable>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using TThrowable jThrowable2 = Assert.IsType<TThrowable>(typeMetadata.ParseInstance(env, jGlobal));
		ThrowableException exception =
			Assert.IsType<ThrowableException<TThrowable>>(typeMetadata.CreateException(jGlobal, exceptionMessage));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<TThrowable>(Arg.Any<JReferenceObject>());
		Assert.Equal(jGlobal, exception.Global);
		Assert.Equal(exceptionMessage, exception.Message);

		Assert.True(typeMetadata.IsInstance(jThrowable0));
		Assert.True(typeMetadata.IsInstance(jThrowable1));
		Assert.True(typeMetadata.IsInstance(jThrowable2));
	}
#pragma warning disable CA1859
	private static void ThrowTest<TThrowable>(Boolean fail)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TThrowable>();
		String exceptionMessage = ThrowableTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JThrowableLocalRef throwableRef = ThrowableTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = ThrowableTests.fixture.Create<JGlobalRef>();
		JWeakRef weakRef = ThrowableTests.fixture.Create<JWeakRef>();
		Exception failException = ThrowableTests.fixture.Create<Exception>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringMessage = new(jStringClass, default, exceptionMessage);
		using TThrowable jThrowable =
			Assert.IsType<TThrowable>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
		using JGlobal jGlobal =
			new(vm, new ThrowableObjectMetadata(new(jThrowableClass)) { Message = exceptionMessage, }, globalRef);
		using JWeak jWeak = new(jGlobal, weakRef);

		IMutableWrapper<ThrowableException?> mutableException = IMutableWrapper<ThrowableException>.Create();

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ReferenceFeature.Create<JGlobal>(jThrowable).Returns(jGlobal);
		env.FunctionSet.GetMessage(jThrowable).Returns(jStringMessage);
		env.ClassFeature.GetTypeMetadata(jThrowableClass).Returns(typeMetadata);
		env.PendingException = Arg.Do<ThrowableException>(c =>
		{
			if (fail) throw failException;
			mutableException.Value = c;
			env.PendingException.Returns(c);
		});
		thread.ReferenceFeature.CreateWeak(jGlobal).Returns(jWeak);
		thread.ClassFeature.GetClass(typeMetadata.ClassName).Returns(jThrowableClass);
		thread.ClassFeature.GetObjectClass(Arg.Any<ObjectMetadata>())
		      .Returns(c => thread.ClassFeature.GetClass((c[0] as ObjectMetadata)!.ObjectClassName));
		thread.GetReferenceType(jWeak).Returns(JReferenceType.WeakGlobalRefType);
		thread.IsSameObject(jWeak, default).Returns(false);

		if (!fail)
		{
			ThrowableException exception = Assert.Throws<ThrowableException<TThrowable>>(() => jThrowable.Throw());
			Assert.Equal(exceptionMessage, exception.Message);
			Assert.Equal(jGlobal, exception.Global);
			Assert.Equal(mutableException.Value, exception);

			exception.WithSafeInvoke(t =>
			{
				Assert.Equal(default, t.LocalReference);
				Assert.Equal(default, (t as ILocalObject).LocalReference);
				Assert.Equal(exceptionMessage, t.Message);
				Assert.Equal(typeMetadata.ClassName, t.ObjectClassName);
				Assert.Equal(typeMetadata.Signature, t.ObjectSignature);
			});
			Assert.Equal(jWeak, exception.WithSafeInvoke(t => t.Weak));

			thread.ReferenceFeature.Received(2).CreateWeak(jGlobal);
			thread.ClassFeature.Received(2).GetClass(typeMetadata.ClassName);
			env.ClassFeature.GetClass(Arg.Any<ITypeInformation>())
			   .Returns(c => env.ClassFeature.GetClass((c[1] as ITypeInformation)!.ClassName));
			thread.Received(3).GetReferenceType(jWeak);
			thread.Received(3).IsSameObject(jWeak, default);

			JClassTypeMetadata? baseMetadata = typeMetadata.BaseMetadata;

			ThrowableTests.InterfaceExceptionTest<TThrowable>(typeMetadata, exception);
			while (baseMetadata is not null && baseMetadata.Type != typeof(JThrowableObject))
			{
				ThrowableTests.interfaceExceptionTest.MakeGenericMethod(baseMetadata.Type)
				              .Invoke(default, [typeMetadata, exception,]);
				baseMetadata = baseMetadata.BaseMetadata;
			}
			ThrowableTests.InterfaceExceptionTest<JThrowableObject>(typeMetadata, exception);
		}
		else
		{
			Assert.Equal(failException, Assert.ThrowsAny<Exception>(() => jThrowable.Throw()));
		}

		env.ReferenceFeature.Received(1).Create<JGlobal>(jThrowable);
		env.FunctionSet.Received(1).GetMessage(jThrowable);
	}
	private static void InterfaceExceptionTest<TThrowable>(JClassTypeMetadata typeMetadata,
		ThrowableException exception) where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		IThrowableException<TThrowable> exceptionT = (IThrowableException<TThrowable>)exception;
		exceptionT.WithSafeInvoke(t => Assert.Equal(typeMetadata.Type, t.GetType()));
	}
#pragma warning restore CA1859
}