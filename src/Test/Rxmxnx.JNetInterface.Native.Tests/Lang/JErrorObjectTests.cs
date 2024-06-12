namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public sealed class JErrorObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new("java/lang/Error"u8);
	private static readonly CString classSignature = CString.Concat("L"u8, JErrorObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JErrorObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JErrorObjectTests.className, JErrorObjectTests.classSignature,
	                                                   JErrorObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ProcessMetadataTest(Boolean useMessage, Boolean emptyStackTrace = false)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JThrowableLocalRef throwableRef = JErrorObjectTests.fixture.Create<JThrowableLocalRef>();
		String message = JErrorObjectTests.fixture.Create<String>();
		StackTraceInfo[] stackTrace = emptyStackTrace ?
			JErrorObjectTests.fixture.CreateMany<StackTraceInfo>().ToArray() :
			[];
		using JClassObject jClass = new(env);
		using JClassObject jErrorClass = new(jClass, IClassType.GetMetadata<JErrorObject>());
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassObject jStackTraceElementClass = new(jClass, IClassType.GetMetadata<JStackTraceElementObject>());
		using JClassObject jStackTraceElementArrayClass =
			new(jClass, IClassType.GetMetadata<JStackTraceElementObject>().GetArrayMetadata()!);
		using JStringObject jStringMessage = new(jStringClass, default, message);
		using JErrorObject jError = (JErrorObject)IClassType.GetMetadata<JErrorObject>()
		                                                    .CreateInstance(jErrorClass, throwableRef.Value);
		using JArrayObject<JStackTraceElementObject> stackTraceElements =
			new(jStackTraceElementArrayClass, default, stackTrace.Length);
		JStackTraceElementObject[] elements =
			stackTrace.Select(i => i.CreateStackTrace(jStackTraceElementClass)).ToArray();
		ThrowableObjectMetadata throwableMetadata = new(new(jErrorClass)) { Message = useMessage ? message : default, };
		env.FunctionSet.GetMessage(jError).Returns(jStringMessage);
		env.FunctionSet.GetStackTrace(jError).Returns(stackTraceElements);
		env.ArrayFeature.GetElement(stackTraceElements, Arg.Any<Int32>()).Returns(c => elements[(Int32)c[1]]);
		env.WithFrame(Arg.Any<Int32>(), stackTraceElements,
		              Arg.Any<Func<JArrayObject<JStackTraceElementObject>, StackTraceInfo[]>>()).Returns(
			c => (c[2] as Func<JArrayObject<JStackTraceElementObject>, StackTraceInfo[]>)!.Invoke(
				(JArrayObject<JStackTraceElementObject>)c[1]));

		ILocalObject.ProcessMetadata(jError, throwableMetadata);

		Assert.Equal(throwableMetadata.ObjectClassName, jError.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, jError.ObjectSignature);
		Assert.Equal(jStringMessage.Value, jError.Message);
		Assert.Equal(stackTrace, jError.StackTrace);

		ThrowableObjectMetadata objectMetadata =
			Assert.IsType<ThrowableObjectMetadata>(ILocalObject.CreateMetadata(jError));
		Assert.Equal(throwableMetadata.ObjectClassName, objectMetadata.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, objectMetadata.ObjectSignature);
		Assert.Equal(throwableMetadata.Message ?? jStringMessage.Value, objectMetadata.Message);
		Assert.Equal(objectMetadata, new(objectMetadata));

		JSerializableObject jSerializable = jError.CastTo<JSerializableObject>();
		Assert.Equal(jError.Id, jSerializable.Id);
		Assert.Equal(jError, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jError, jError.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jError, jError.CastTo<JThrowableObject>()));

		env.FunctionSet.Received(useMessage ? 0 : 1).GetMessage(jError);

		Assert.Equal(throwableRef, jError.Reference);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JErrorObject>();
		String textValue = typeMetadata.ToString();
		String exceptionMessage = JErrorObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JErrorObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JErrorObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JErrorObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jErrorClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jErrorClass);
		using JGlobal jGlobal = new(vm, new(jErrorClass, IClassType.GetMetadata<JErrorObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JErrorObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JErrorObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JErrorObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JErrorObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JErrorObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JErrorObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JErrorObjectTests.hash.ToString(), IDataType.GetHash<JErrorObject>());
		Assert.Equal(IDataType.GetMetadata<JThrowableObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JErrorObject>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JErrorObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JThrowableObject), EnvironmentProxy.GetFamilyType<JErrorObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JErrorObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JErrorObject>().Returns(jErrorClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jErrorClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jErrorClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JThrowableObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));

		using JErrorObject jError0 =
			Assert.IsType<JErrorObject>(typeMetadata.CreateInstance(jErrorClass, throwableRef.Value, true));
		using JErrorObject jError1 = Assert.IsType<JErrorObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JErrorObject jError2 = Assert.IsType<JErrorObject>(typeMetadata.ParseInstance(env, jGlobal));
		ThrowableException exception =
			Assert.IsType<ThrowableException<JErrorObject>>(typeMetadata.CreateException(jGlobal, exceptionMessage));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JErrorObject>(Arg.Any<JReferenceObject>());
		Assert.Equal(jGlobal, exception.Global);
		Assert.Equal(exceptionMessage, exception.Message);

		Assert.True(typeMetadata.IsInstance(jError0));
		Assert.True(typeMetadata.IsInstance(jError1));
		Assert.True(typeMetadata.IsInstance(jError2));
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
#pragma warning disable CA1859
	internal void ThrowTest(Boolean fail)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JErrorObject>();
		String exceptionMessage = JErrorObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JThrowableLocalRef throwableRef = JErrorObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JErrorObjectTests.fixture.Create<JGlobalRef>();
		JWeakRef weakRef = JErrorObjectTests.fixture.Create<JWeakRef>();
		Exception failException = JErrorObjectTests.fixture.Create<Exception>();
		using JClassObject jClassClass = new(env);
		using JClassObject jErrorClass = new(jClassClass, typeMetadata);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringMessage = new(jStringClass, default, exceptionMessage);
		using JErrorObject jError =
			Assert.IsType<JErrorObject>(typeMetadata.CreateInstance(jErrorClass, throwableRef.Value, true));
		using JGlobal jGlobal = new(vm, new ThrowableObjectMetadata(new(jErrorClass)) { Message = exceptionMessage, },
		                            globalRef);
		using JWeak jWeak = new(jGlobal, weakRef);

		IMutableWrapper<ThrowableException?> mutableException = IMutableWrapper<ThrowableException>.Create();

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ReferenceFeature.Create<JGlobal>(jError).Returns(jGlobal);
		env.FunctionSet.GetMessage(jError).Returns(jStringMessage);
		env.ClassFeature.GetTypeMetadata(jErrorClass).Returns(typeMetadata);
		env.PendingException = Arg.Do<ThrowableException>(c =>
		{
			if (fail) throw failException;
			mutableException.Value = c;
			env.PendingException.Returns(c);
		});
		thread.ReferenceFeature.CreateWeak(jGlobal).Returns(jWeak);
		thread.ClassFeature.GetClass(JErrorObjectTests.className).Returns(jErrorClass);
		thread.GetReferenceType(jWeak).Returns(JReferenceType.WeakGlobalRefType);
		thread.IsSameObject(jWeak, default).Returns(false);

		if (!fail)
		{
			ThrowableException exception = Assert.Throws<ThrowableException<JErrorObject>>(() => jError.Throw());
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
			thread.ClassFeature.Received(2).GetClass(JErrorObjectTests.className);
			thread.Received(3).GetReferenceType(jWeak);
			thread.Received(3).IsSameObject(jWeak, default);

			IThrowableException<JThrowableObject> exceptionT = (IThrowableException<JThrowableObject>)exception;
			IThrowableException<JErrorObject> exceptionO = (IThrowableException<JErrorObject>)exception;

			exceptionT.WithSafeInvoke(t => Assert.Equal(typeof(JErrorObject), t.GetType()));
			Assert.Equal(typeof(JErrorObject), exceptionT.WithSafeInvoke(t => t.GetType()));
			exceptionO.WithSafeInvoke(t => Assert.Equal(typeof(JErrorObject), t.GetType()));
			Assert.Equal(typeof(JErrorObject), exceptionO.WithSafeInvoke(t => t.GetType()));
		}
		else
		{
			Assert.Equal(failException, Assert.ThrowsAny<Exception>(() => jError.Throw()));
		}

		env.ReferenceFeature.Received(1).Create<JGlobal>(jError);
		env.FunctionSet.Received(1).GetMessage(jError);
	}
#pragma warning restore CA1859
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ThrowNewTest(Boolean throwException)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String exceptionMessage = JErrorObjectTests.fixture.Create<String>();
		CString utf8Message = (CString)exceptionMessage;
		JThrowableObject.ThrowNew<JErrorObject>(env, exceptionMessage, throwException);
		JThrowableObject.ThrowNew<JErrorObject>(env, utf8Message, throwException);

		env.ClassFeature.Received(1).ThrowNew<JErrorObject>(exceptionMessage, throwException);
		env.ClassFeature.Received(1).ThrowNew<JErrorObject>(utf8Message, throwException);
	}
	[Fact]
	internal void ThrowableMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JErrorObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String message = JErrorObjectTests.fixture.Create<String>();
		using JClassObject jClass = new(env);
		using JClassObject jErrorClass = new(jClass, IClassType.GetMetadata<JErrorObject>());

		ThrowableObjectMetadata throwableMetadata = new(jErrorClass, typeMetadata, message);
		Assert.Equal(typeMetadata.ClassName, throwableMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, throwableMetadata.ObjectSignature);
		Assert.Equal(message, throwableMetadata.Message);
	}
}