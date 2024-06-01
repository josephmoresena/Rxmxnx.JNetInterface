namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
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
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ProcessMetadataTest(Boolean useMessage, Boolean emptyStackTrace = false)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JThrowableLocalRef throwableRef = JThrowableObjectTests.fixture.Create<JThrowableLocalRef>();
		String message = JThrowableObjectTests.fixture.Create<String>();
		StackTraceInfo[] stackTrace = emptyStackTrace ?
			JThrowableObjectTests.fixture.CreateMany<StackTraceInfo>().ToArray() :
			[];
		using JClassObject jClass = new(env);
		using JClassObject jThrowableClass = new(jClass, IClassType.GetMetadata<JThrowableObject>());
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JClassObject jStackTraceElementClass = new(jClass, IClassType.GetMetadata<JStackTraceElementObject>());
		using JClassObject jStackTraceElementArrayClass =
			new(jClass, IClassType.GetMetadata<JStackTraceElementObject>().GetArrayMetadata()!);
		using JStringObject jStringMessage = new(jStringClass, default, message);
		using JThrowableObject jThrowable = (JThrowableObject)IClassType.GetMetadata<JThrowableObject>()
		                                                                .CreateInstance(
			                                                                jThrowableClass, throwableRef.Value);
		using JArrayObject<JStackTraceElementObject> stackTraceElements =
			new(jStackTraceElementArrayClass, default, stackTrace.Length);
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

		env.FunctionSet.Received(useMessage ? 0 : 1).GetMessage(jThrowable);

		Assert.Equal(throwableRef, jThrowable.Reference);

		String toString = $"{jThrowable.Class.Name} {jThrowable.Reference} {jThrowable.Message}";
		if (jThrowable.StackTrace.Length > 0)
		{
			StringBuilder strBuild = new(toString);
			foreach (StackTraceInfo t in jThrowable.StackTrace)
				strBuild.AppendLine(t.ToTraceText());
			toString = strBuild.ToString();
		}
		Assert.Equal(toString, jThrowable.ToString());
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JThrowableObject>();
		String textValue = typeMetadata.ToString();
		String exceptionMessage = JThrowableObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JThrowableObjectTests.fixture.Create<JClassLocalRef>();
		JThrowableLocalRef throwableRef = JThrowableObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JThrowableObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, throwableRef.Value, jThrowableClass);
		using JGlobal jGlobal = new(vm, new(jThrowableClass, IClassType.GetMetadata<JThrowableObject>()), globalRef);

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
		Assert.IsType<JFunctionDefinition<JThrowableObject>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JThrowableObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JThrowableObject), EnvironmentProxy.GetFamilyType<JThrowableObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JThrowableObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

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

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<JThrowableObject>(Arg.Any<JReferenceObject>());
		Assert.Equal(jGlobal, exception.Global);
		Assert.Equal(exceptionMessage, exception.Message);

		Assert.True(typeMetadata.IsInstance(jThrowable0));
		Assert.True(typeMetadata.IsInstance(jThrowable1));
		Assert.True(typeMetadata.IsInstance(jThrowable2));
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ThrowTest(Boolean fail)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JThrowableObject>();
		String exceptionMessage = JThrowableObjectTests.fixture.Create<String>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JThrowableLocalRef throwableRef = JThrowableObjectTests.fixture.Create<JThrowableLocalRef>();
		JGlobalRef globalRef = JThrowableObjectTests.fixture.Create<JGlobalRef>();
		JWeakRef weakRef = JThrowableObjectTests.fixture.Create<JWeakRef>();
		Exception failException = JThrowableObjectTests.fixture.Create<Exception>();
		using JClassObject jClassClass = new(env);
		using JClassObject jThrowableClass = new(jClassClass, typeMetadata);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jStringMessage = new(jStringClass, default, exceptionMessage);
		using JThrowableObject jThrowable =
			Assert.IsType<JThrowableObject>(typeMetadata.CreateInstance(jThrowableClass, throwableRef.Value, true));
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
		thread.ClassFeature.GetClass(JThrowableObjectTests.className).Returns(jThrowableClass);
		thread.GetReferenceType(jWeak).Returns(JReferenceType.WeakGlobalRefType);
		thread.IsSameObject(jWeak, default).Returns(false);

		if (!fail)
		{
			ThrowableException exception =
				Assert.Throws<ThrowableException<JThrowableObject>>(() => jThrowable.Throw());
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
			thread.ClassFeature.Received(2).GetClass(JThrowableObjectTests.className);
			thread.Received(3).GetReferenceType(jWeak);
			thread.Received(3).IsSameObject(jWeak, default);
		}
		else
		{
			Assert.Equal(failException, Assert.ThrowsAny<Exception>(() => jThrowable.Throw()));
		}

		env.ReferenceFeature.Received(1).Create<JGlobal>(jThrowable);
		env.FunctionSet.Received(1).GetMessage(jThrowable);
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void ThrowNewTest(Boolean throwException)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String exceptionMessage = JThrowableObjectTests.fixture.Create<String>();
		CString utf8Message = (CString)exceptionMessage;
		JThrowableObject.ThrowNew<JThrowableObject>(env, exceptionMessage, throwException);
		JThrowableObject.ThrowNew<JThrowableObject>(env, utf8Message, throwException);

		env.ClassFeature.Received(1).ThrowNew<JThrowableObject>(exceptionMessage, throwException);
		env.ClassFeature.Received(1).ThrowNew<JThrowableObject>(utf8Message, throwException);
	}
	[Fact]
	internal void ThrowableMetadataTest()
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JThrowableObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String message = JThrowableObjectTests.fixture.Create<String>();
		using JClassObject jClass = new(env);
		using JClassObject jThrowableClass = new(jClass, IClassType.GetMetadata<JThrowableObject>());

		ThrowableObjectMetadata throwableMetadata = new(jThrowableClass, typeMetadata, message);
		Assert.Equal(typeMetadata.ClassName, throwableMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, throwableMetadata.ObjectSignature);
		Assert.Equal(message, throwableMetadata.Message);
	}
}