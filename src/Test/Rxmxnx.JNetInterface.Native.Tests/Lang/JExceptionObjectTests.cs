namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
public sealed class JExceptionObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.ExceptionObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JExceptionObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JExceptionObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JExceptionObjectTests.className,
	                                                   JExceptionObjectTests.classSignature,
	                                                   JExceptionObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ProcessMetadataTest(Boolean useMessage, Boolean emptyStackTrace = false)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JThrowableLocalRef throwableRef = JExceptionObjectTests.fixture.Create<JThrowableLocalRef>();
		String message = JExceptionObjectTests.fixture.Create<String>();
		StackTraceInfo[] stackTrace = emptyStackTrace ?
			JExceptionObjectTests.fixture.CreateMany<StackTraceInfo>().ToArray() :
			[];
		using JClassObject jClass = new(env);
		using JClassObject jExceptionClass = new(jClass, IClassType.GetMetadata<JExceptionObject>());
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassObject jStackTraceElementClass = new(jClass, IClassType.GetMetadata<JStackTraceElementObject>());
		using JClassObject jStackTraceElementArrayClass =
			new(jClass, IClassType.GetMetadata<JStackTraceElementObject>().GetArrayMetadata()!);
		using JStringObject jStringMessage = new(jStringClass, default, message);
		using JExceptionObject jException = (JExceptionObject)IClassType.GetMetadata<JExceptionObject>()
		                                                                .CreateInstance(
			                                                                jExceptionClass, throwableRef.Value);
		using JArrayObject<JStackTraceElementObject> stackTraceElements =
			new(jStackTraceElementArrayClass, default, stackTrace.Length);
		JStackTraceElementObject[] elements =
			stackTrace.Select(i => i.CreateStackTrace(jStackTraceElementClass)).ToArray();
		ThrowableObjectMetadata throwableMetadata =
			new(new(jExceptionClass)) { Message = useMessage ? message : default, };
		env.FunctionSet.GetMessage(jException).Returns(jStringMessage);
		env.FunctionSet.GetStackTrace(jException).Returns(stackTraceElements);
		env.ArrayFeature.GetElement(stackTraceElements, Arg.Any<Int32>()).Returns(c => elements[(Int32)c[1]]);
		env.WithFrame(Arg.Any<Int32>(), jException, Arg.Any<Func<JExceptionObject, StackTraceInfo[]>>())
		   .Returns(c => (c[2] as Func<JExceptionObject, StackTraceInfo[]>)!.Invoke((JExceptionObject)c[1]));

		ILocalObject.ProcessMetadata(jException, throwableMetadata);

		Assert.Equal(throwableMetadata.ObjectClassName, jException.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, jException.ObjectSignature);
		Assert.Equal(jStringMessage.Value, jException.Message);
		Assert.Equal(stackTrace, jException.StackTrace);

		ThrowableObjectMetadata objectMetadata =
			Assert.IsType<ThrowableObjectMetadata>(ILocalObject.CreateMetadata(jException));
		Assert.Equal(throwableMetadata.ObjectClassName, objectMetadata.ObjectClassName);
		Assert.Equal(throwableMetadata.ObjectSignature, objectMetadata.ObjectSignature);
		Assert.Equal(throwableMetadata.Message ?? jStringMessage.Value, objectMetadata.Message);
		Assert.Equal(objectMetadata, new(objectMetadata));

		JSerializableObject jSerializable = jException.CastTo<JSerializableObject>();
		Assert.Equal(jException.Id, jSerializable.Id);
		Assert.Equal(jException, jSerializable.Object);

		Assert.True(Object.ReferenceEquals(jException, jException.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jException, jException.CastTo<JThrowableObject>()));

		env.FunctionSet.Received(useMessage ? 0 : 1).GetMessage(jException);

		Assert.Equal(throwableRef, jException.Reference);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JExceptionObject>();
		String textValue = typeMetadata.ToString();
		String exceptionMessage = JExceptionObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JExceptionObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JExceptionObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JExceptionObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jExceptionClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jExceptionClass);
		using JGlobal jGlobal = new(vm, new(jExceptionClass, IClassType.GetMetadata<JExceptionObject>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Extensible, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JExceptionObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JExceptionObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JExceptionObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JExceptionObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JExceptionObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JExceptionObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JExceptionObjectTests.hash.ToString(), IDataType.GetHash<JExceptionObject>());
		Assert.Equal(IDataType.GetMetadata<JThrowableObject>(), typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<JExceptionObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JExceptionObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JThrowableObject), EnvironmentProxy.GetFamilyType<JExceptionObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JExceptionObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JExceptionObject>().Returns(jExceptionClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, throwableRef.Value, jExceptionClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jExceptionClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JThrowableObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));

		using JExceptionObject jException0 =
			Assert.IsType<JExceptionObject>(typeMetadata.CreateInstance(jExceptionClass, throwableRef.Value, true));
		using JExceptionObject jException1 =
			Assert.IsType<JExceptionObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JExceptionObject jException2 = Assert.IsType<JExceptionObject>(typeMetadata.ParseInstance(env, jGlobal));
		ThrowableException exception =
			Assert.IsType<ThrowableException<JExceptionObject>>(
				typeMetadata.CreateException(jGlobal, exceptionMessage));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JExceptionObject>(Arg.Any<JReferenceObject>());
		Assert.Equal(jGlobal, exception.Global);
		Assert.Equal(exceptionMessage, exception.Message);

		Assert.True(typeMetadata.IsInstance(jException0));
		Assert.True(typeMetadata.IsInstance(jException1));
		Assert.True(typeMetadata.IsInstance(jException2));

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ThrowTest(Boolean fail)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JExceptionObject>();
		String exceptionMessage = JExceptionObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JThrowableLocalRef throwableRef = JExceptionObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JExceptionObjectTests.fixture.Create<JGlobalRef>();
		JWeakRef weakRef = JExceptionObjectTests.fixture.Create<JWeakRef>();
		Exception failException = JExceptionObjectTests.fixture.Create<Exception>();
		using JClassObject jClassClass = new(env);
		using JClassObject jExceptionClass = new(jClassClass, typeMetadata);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringMessage = new(jStringClass, default, exceptionMessage);
		using JExceptionObject jException =
			Assert.IsType<JExceptionObject>(typeMetadata.CreateInstance(jExceptionClass, throwableRef.Value, true));
		using JGlobal jGlobal =
			new(vm, new ThrowableObjectMetadata(new(jExceptionClass)) { Message = exceptionMessage, }, globalRef);
		using JWeak jWeak = new(jGlobal, weakRef);

		IMutableWrapper<ThrowableException?> mutableException = IMutableWrapper<ThrowableException>.Create();

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ReferenceFeature.Create<JGlobal>(jException).Returns(jGlobal);
		env.FunctionSet.GetMessage(jException).Returns(jStringMessage);
		env.ClassFeature.GetTypeMetadata(jExceptionClass).Returns(typeMetadata);
		env.PendingException = Arg.Do<ThrowableException>(c =>
		{
			if (fail) throw failException;
			mutableException.Value = c;
			env.PendingException.Returns(c);
		});
		thread.ReferenceFeature.CreateWeak(jGlobal).Returns(jWeak);
		thread.ClassFeature.GetClass(JExceptionObjectTests.className).Returns(jExceptionClass);
		thread.GetReferenceType(jWeak).Returns(JReferenceType.WeakGlobalRefType);
		thread.IsSameObject(jWeak, default).Returns(false);

		if (!fail)
		{
			ThrowableException exception =
				Assert.Throws<ThrowableException<JExceptionObject>>(() => jException.Throw());
			Assert.Equal(exceptionMessage, exception.Message);
			Assert.Equal(jGlobal, exception.Global);
			Assert.Equal(mutableException.Value, exception);

			exception.WithSafeInvoke(t =>
			{
				Assert.Equal(default, t.InternalReference);
				Assert.Equal(default, (t as ILocalObject).InternalReference);
				Assert.Equal(exceptionMessage, t.Message);
				Assert.Equal(typeMetadata.ClassName, t.ObjectClassName);
				Assert.Equal(typeMetadata.Signature, t.ObjectSignature);
			});
			Assert.Equal(jWeak, exception.WithSafeInvoke(t => t.Weak));

			thread.ReferenceFeature.Received(2).CreateWeak(jGlobal);
			thread.ClassFeature.Received(2).GetClass(JExceptionObjectTests.className);
			thread.Received(3).GetReferenceType(jWeak);
			thread.Received(3).IsSameObject(jWeak, default);

			IThrowableException<JThrowableObject> exceptionT = (IThrowableException<JThrowableObject>)exception;
			IThrowableException<JExceptionObject> exceptionO = (IThrowableException<JExceptionObject>)exception;

			exceptionT.WithSafeInvoke(t => Assert.Equal(typeof(JExceptionObject), t.GetType()));
			Assert.Equal(typeof(JExceptionObject), exceptionT.WithSafeInvoke(t => t.GetType()));
			exceptionO.WithSafeInvoke(t => Assert.Equal(typeof(JExceptionObject), t.GetType()));
			Assert.Equal(typeof(JExceptionObject), exceptionO.WithSafeInvoke(t => t.GetType()));
		}
		else
		{
			Assert.Equal(failException, Assert.ThrowsAny<Exception>(() => jException.Throw()));
		}

		env.ReferenceFeature.Received(1).Create<JGlobal>(jException);
		env.FunctionSet.Received(1).GetMessage(jException);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ThrowNewTest(Boolean throwException)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String exceptionMessage = JExceptionObjectTests.fixture.Create<String>();
		CString utf8Message = (CString)exceptionMessage;
		JThrowableObject.ThrowNew<JExceptionObject>(env, exceptionMessage, throwException);
		JThrowableObject.ThrowNew<JExceptionObject>(env, utf8Message, throwException);

		env.ClassFeature.Received(1).ThrowNew<JExceptionObject>(exceptionMessage, throwException);
		env.ClassFeature.Received(1).ThrowNew<JExceptionObject>(utf8Message, throwException);
	}
	[Fact]
	internal void ThrowableMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JExceptionObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String message = JExceptionObjectTests.fixture.Create<String>();
		using JClassObject jClass = new(env);
		using JClassObject jExceptionClass = new(jClass, IClassType.GetMetadata<JExceptionObject>());

		ThrowableObjectMetadata throwableMetadata = new(jExceptionClass, typeMetadata, message);
		Assert.Equal(typeMetadata.ClassName, throwableMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, throwableMetadata.ObjectSignature);
		Assert.Equal(message, throwableMetadata.Message);
	}
}