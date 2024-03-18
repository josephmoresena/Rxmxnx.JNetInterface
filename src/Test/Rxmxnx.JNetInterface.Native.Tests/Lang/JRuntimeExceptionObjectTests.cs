namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public sealed class JRuntimeExceptionObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.RuntimeExceptionObject);
	private static readonly CString classSignature =
		CString.Concat("L"u8, JRuntimeExceptionObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JRuntimeExceptionObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JRuntimeExceptionObjectTests.className,
	                                                   JRuntimeExceptionObjectTests.classSignature,
	                                                   JRuntimeExceptionObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ProcessMetadataTest(Boolean useMessage, Boolean emptyStackTrace = false)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JThrowableLocalRef throwableRef = JRuntimeExceptionObjectTests.fixture.Create<JThrowableLocalRef>();
		String message = JRuntimeExceptionObjectTests.fixture.Create<String>();
		StackTraceInfo[] stackTrace = emptyStackTrace ?
			JRuntimeExceptionObjectTests.fixture.CreateMany<StackTraceInfo>().ToArray() :
			Array.Empty<StackTraceInfo>();
		using JClassObject jClass = new(env);
		using JClassObject jRuntimeExceptionClass = new(jClass, IClassType.GetMetadata<JRuntimeExceptionObject>());
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassObject jStackTraceElementClass = new(jClass, IClassType.GetMetadata<JStackTraceElementObject>());
		using JStringObject jStringMessage = new(jStringClass, default, message);
		using JRuntimeExceptionObject jRuntimeException = (JRuntimeExceptionObject)IClassType
			.GetMetadata<JRuntimeExceptionObject>().CreateInstance(jRuntimeExceptionClass, throwableRef.Value);
		using JArrayObject<JStackTraceElementObject> stackTraceElements =
			new(jStackTraceElementClass, default, stackTrace.Length);
		JStackTraceElementObject[] elements =
			stackTrace.Select(i => i.CreateStackTrace(jStackTraceElementClass)).ToArray();
		ThrowableObjectMetadata throwableMetadata =
			new(new(jRuntimeExceptionClass)) { Message = useMessage ? message : default, };
		env.FunctionSet.GetMessage(jRuntimeException).Returns(jStringMessage);
		env.FunctionSet.GetStackTrace(jRuntimeException).Returns(stackTraceElements);
		env.ArrayFeature.GetElement(stackTraceElements, Arg.Any<Int32>()).Returns(c => elements[(Int32)c[1]]);
		env.WithFrame(Arg.Any<Int32>(), jRuntimeException, Arg.Any<Func<JRuntimeExceptionObject, StackTraceInfo[]>>())
		   .Returns(c => (c[2] as Func<JRuntimeExceptionObject, StackTraceInfo[]>)!.Invoke(
			            (JRuntimeExceptionObject)c[1]));

		ILocalObject.ProcessMetadata(jRuntimeException, throwableMetadata);

		Assert.Equal(throwableMetadata.ObjectClassName, jRuntimeException.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, jRuntimeException.ObjectSignature);
		Assert.Equal(jStringMessage.Value, jRuntimeException.Message);
		Assert.Equal(stackTrace, jRuntimeException.StackTrace);

		ThrowableObjectMetadata objectMetadata =
			Assert.IsType<ThrowableObjectMetadata>(ILocalObject.CreateMetadata(jRuntimeException));
		Assert.Equal(throwableMetadata.ObjectClassName, objectMetadata.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, objectMetadata.ObjectSignature);
		Assert.Equal(throwableMetadata.Message ?? jStringMessage.Value, objectMetadata.Message);
		Assert.Equal(objectMetadata, new(objectMetadata));

		JSerializableObject jSerializable = jRuntimeException.CastTo<JSerializableObject>();
		Assert.Equal(jRuntimeException.Id, jSerializable.Id);
		Assert.Equal(jRuntimeException, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jRuntimeException, jRuntimeException.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jRuntimeException, jRuntimeException.CastTo<JExceptionObject>()));
		Assert.True(Object.ReferenceEquals(jRuntimeException, jRuntimeException.CastTo<JThrowableObject>()));

		env.FunctionSet.Received(useMessage ? 0 : 1).GetMessage(jRuntimeException);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JRuntimeExceptionObject>();
		String textValue = typeMetadata.ToString();
		String exceptionMessage = JRuntimeExceptionObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JRuntimeExceptionObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JRuntimeExceptionObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JRuntimeExceptionObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jRuntimeExceptionClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jRuntimeExceptionClass);
		using JGlobal jGlobal = new(vm, new(jRuntimeExceptionClass, IClassType.GetMetadata<JRuntimeExceptionObject>()),
		                            !env.NoProxy, globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JRuntimeExceptionObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JRuntimeExceptionObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JRuntimeExceptionObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JRuntimeExceptionObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JRuntimeExceptionObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JRuntimeExceptionObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JRuntimeExceptionObjectTests.hash.ToString(), IDataType.GetHash<JRuntimeExceptionObject>());
		Assert.Equal(IDataType.GetMetadata<JExceptionObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JRuntimeExceptionObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, Array.Empty<JArgumentMetadata>()));
		Assert.IsType<JFieldDefinition<JRuntimeExceptionObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JThrowableObject), EnvironmentProxy.GetFamilyType<JRuntimeExceptionObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JRuntimeExceptionObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JRuntimeExceptionObject>().Returns(jRuntimeExceptionClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jRuntimeExceptionClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jRuntimeExceptionClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JThrowableObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JExceptionObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));

		using JRuntimeExceptionObject jRuntimeException0 =
			Assert.IsType<JRuntimeExceptionObject>(
				typeMetadata.CreateInstance(jRuntimeExceptionClass, throwableRef.Value, true));
		using JRuntimeExceptionObject jRuntimeException1 =
			Assert.IsType<JRuntimeExceptionObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JRuntimeExceptionObject jRuntimeException2 =
			Assert.IsType<JRuntimeExceptionObject>(typeMetadata.ParseInstance(env, jGlobal));
		ThrowableException exception =
			Assert.IsType<ThrowableException<JRuntimeExceptionObject>>(
				typeMetadata.CreateException(jGlobal, exceptionMessage));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JRuntimeExceptionObject>(Arg.Any<JReferenceObject>());
		Assert.Equal(jGlobal, exception.Global);
		Assert.Equal(exceptionMessage, exception.Message);

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ThrowTest(Boolean fail)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JRuntimeExceptionObject>();
		String exceptionMessage = JRuntimeExceptionObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JThrowableLocalRef throwableRef = JRuntimeExceptionObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JRuntimeExceptionObjectTests.fixture.Create<JGlobalRef>();
		JWeakRef weakRef = JRuntimeExceptionObjectTests.fixture.Create<JWeakRef>();
		Exception failException = JRuntimeExceptionObjectTests.fixture.Create<Exception>();
		using JClassObject jClassClass = new(env);
		using JClassObject jRuntimeExceptionClass = new(jClassClass, typeMetadata);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringMessage = new(jStringClass, default, exceptionMessage);
		using JRuntimeExceptionObject jRuntimeException =
			Assert.IsType<JRuntimeExceptionObject>(
				typeMetadata.CreateInstance(jRuntimeExceptionClass, throwableRef.Value, true));
		using JGlobal jGlobal =
			new(vm, new ThrowableObjectMetadata(new(jRuntimeExceptionClass)) { Message = exceptionMessage, },
			    !env.NoProxy, globalRef);
		using JWeak jWeak = new(jGlobal, weakRef);

		IMutableWrapper<ThrowableException?> mutableException = IMutableWrapper<ThrowableException>.Create();

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ReferenceFeature.Create<JGlobal>(jRuntimeException).Returns(jGlobal);
		env.FunctionSet.GetMessage(jRuntimeException).Returns(jStringMessage);
		env.ClassFeature.GetTypeMetadata(jRuntimeExceptionClass).Returns(typeMetadata);
		env.PendingException = Arg.Do<ThrowableException>(c =>
		{
			if (fail) throw failException;
			mutableException.Value = c;
			env.PendingException.Returns(c);
		});
		thread.ReferenceFeature.CreateWeak(jGlobal).Returns(jWeak);
		thread.ClassFeature.GetClass(JRuntimeExceptionObjectTests.className).Returns(jRuntimeExceptionClass);
		thread.GetReferenceType(jWeak).Returns(JReferenceType.WeakGlobalRefType);
		thread.IsSameObject(jWeak, default).Returns(false);

		if (!fail)
		{
			ThrowableException exception =
				Assert.Throws<ThrowableException<JRuntimeExceptionObject>>(() => jRuntimeException.Throw());
			Assert.Equal(exceptionMessage, exception.Message);
			Assert.Equal(jGlobal, exception.Global);
			Assert.Equal(mutableException.Value, exception);

			exception.WithSafeInvoke(t =>
			{
				Assert.Equal(default, t.InternalReference);
				Assert.Equal(exceptionMessage, t.Message);
				Assert.Equal(typeMetadata.ClassName, t.ObjectClassName);
				Assert.Equal(typeMetadata.Signature, t.ObjectSignature);
			});
			Assert.Equal(jWeak, exception.WithSafeInvoke(t => t.Weak));

			thread.ReferenceFeature.Received(2).CreateWeak(jGlobal);
			thread.ClassFeature.Received(2).GetClass(JRuntimeExceptionObjectTests.className);
			thread.Received(3).GetReferenceType(jWeak);
			thread.Received(3).IsSameObject(jWeak, default);

			IThrowableException<JThrowableObject> exceptionT = (IThrowableException<JThrowableObject>)exception;
			IThrowableException<JExceptionObject> exceptionE = (IThrowableException<JExceptionObject>)exception;
			IThrowableException<JRuntimeExceptionObject> exceptionO =
				(IThrowableException<JRuntimeExceptionObject>)exception;

			exceptionT.WithSafeInvoke(t => Assert.Equal(typeof(JRuntimeExceptionObject), t.GetType()));
			Assert.Equal(typeof(JRuntimeExceptionObject), exceptionT.WithSafeInvoke(t => t.GetType()));
			exceptionE.WithSafeInvoke(t => Assert.Equal(typeof(JRuntimeExceptionObject), t.GetType()));
			Assert.Equal(typeof(JRuntimeExceptionObject), exceptionE.WithSafeInvoke(t => t.GetType()));
			exceptionO.WithSafeInvoke(t => Assert.Equal(typeof(JRuntimeExceptionObject), t.GetType()));
			Assert.Equal(typeof(JRuntimeExceptionObject), exceptionO.WithSafeInvoke(t => t.GetType()));
		}
		else
		{
			Assert.Equal(failException, Assert.ThrowsAny<Exception>(() => jRuntimeException.Throw()));
		}

		env.ReferenceFeature.Received(1).Create<JGlobal>(jRuntimeException);
		env.FunctionSet.Received(1).GetMessage(jRuntimeException);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ThrowNewTest(Boolean throwException)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String exceptionMessage = JRuntimeExceptionObjectTests.fixture.Create<String>();
		CString utf8Message = (CString)exceptionMessage;
		JThrowableObject.ThrowNew<JRuntimeExceptionObject>(env, exceptionMessage, throwException);
		JThrowableObject.ThrowNew<JRuntimeExceptionObject>(env, utf8Message, throwException);

		env.ClassFeature.Received(1).ThrowNew<JRuntimeExceptionObject>(exceptionMessage, throwException);
		env.ClassFeature.Received(1).ThrowNew<JRuntimeExceptionObject>(utf8Message, throwException);
	}
	[Fact]
	internal void ThrowableMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JRuntimeExceptionObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String message = JRuntimeExceptionObjectTests.fixture.Create<String>();
		using JClassObject jClass = new(env);
		using JClassObject jRuntimeExceptionClass = new(jClass, IClassType.GetMetadata<JRuntimeExceptionObject>());

		ThrowableObjectMetadata throwableMetadata = new(jRuntimeExceptionClass, typeMetadata, message);
		Assert.Equal(typeMetadata.ClassName, throwableMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, throwableMetadata.ObjectSignature);
		Assert.Equal(message, throwableMetadata.Message);
	}
}