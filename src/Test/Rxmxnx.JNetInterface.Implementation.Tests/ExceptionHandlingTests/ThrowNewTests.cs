namespace Rxmxnx.JNetInterface.Tests;

public partial class ExceptionHandlingTests
{
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ThrowNewThrowableTest(Boolean utf8Message, Boolean throwException = false)
		=> ExceptionHandlingTests.ThrowNewTest<JThrowableObject>(utf8Message, throwException);
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ThrowNewErrorTest(Boolean utf8Message, Boolean throwException = false)
		=> ExceptionHandlingTests.ThrowNewTest<JErrorObject>(utf8Message, throwException);
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ThrowNewExceptionTest(Boolean utf8Message, Boolean throwException = false)
		=> ExceptionHandlingTests.ThrowNewTest<JExceptionObject>(utf8Message, throwException);
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ThrowNewRuntimeExceptionTest(Boolean utf8Message, Boolean throwException = false)
		=> ExceptionHandlingTests.ThrowNewTest<JRuntimeExceptionObject>(utf8Message, throwException);

	private static void ThrowNewTest<TThrowable>(Boolean utf8Message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String message = ExceptionHandlingTests.fixture.Create<String>();
			JClassLocalRef classRef = ExceptionHandlingTests.fixture.Create<JClassLocalRef>();

			using IFixedPointer.IDisposable fMem = IDataType.GetMetadata<TThrowable>().Information.GetFixedPointer();
			JThrowableLocalRef throwableRef = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
			JGlobalRef globalRef = ExceptionHandlingTests.fixture.Create<JGlobalRef>();
			IMutableWrapper<JThrowableLocalRef> exceptionOccurred = IMutableWrapper<JThrowableLocalRef>.Create();

			proxyEnv.FindClass((ReadOnlyValPtr<Byte>)fMem.Pointer).Returns(classRef);
			proxyEnv.ThrowNew(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(JResult.Ok);
			proxyEnv.NewGlobalRef(throwableRef.Value).Returns(globalRef);
			proxyEnv.ExceptionOccurred().Returns(_ => exceptionOccurred.Value);
			proxyEnv.ExceptionCheck().Returns(_ => !exceptionOccurred.Value.IsDefault);
			proxyEnv.When(e => e.ThrowNew(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>()))
			        .Do(_ => { exceptionOccurred.Value = throwableRef; });
			proxyEnv.When(e => e.ExceptionClear()).Do(_ => { exceptionOccurred.Value = default; });
			proxyEnv.When(e => e.Throw(Arg.Any<JThrowableLocalRef>()))
			        .Do(c => { exceptionOccurred.Value = (JThrowableLocalRef)c[0]; });

			using JClassObject jClass = env.ClassFeature.GetClass<TThrowable>();
			ThrowableException ex;

			if (throwException)
			{
				ex = utf8Message ?
					Assert.ThrowsAny<ThrowableException>(
						() => JThrowableObject.ThrowNew<TThrowable>(env, (CString)message, true)) :
					Assert.ThrowsAny<ThrowableException>(
						() => JThrowableObject.ThrowNew<TThrowable>(env, message, true));
			}
			else
			{
				if (utf8Message)
					JThrowableObject.ThrowNew<TThrowable>(env, (CString)message);
				else
					JThrowableObject.ThrowNew<TThrowable>(env, message);
				ex = env.PendingException!;
			}

			Assert.Equal(globalRef.Value, ex.ThrowableRef.Value);
			Assert.Equal(message, ex.Message);

			proxyEnv.Received(1).ThrowNew(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>());

			env.PendingException = default;

			ex.GlobalThrowable.Dispose();

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			if (throwException)
			{
				ex = utf8Message ?
					Assert.ThrowsAny<ThrowableException>(
						() => JThrowableObject.ThrowNew(jClass, (CString)message, true)) :
					Assert.ThrowsAny<ThrowableException>(() => JThrowableObject.ThrowNew(jClass, message, true));
			}
			else
			{
				if (utf8Message)
					JThrowableObject.ThrowNew(env.ClassFeature.GetClass<TThrowable>(), (CString)message);
				else
					JThrowableObject.ThrowNew(env.ClassFeature.GetClass<TThrowable>(), message);
				ex = env.PendingException!;
			}

			Assert.Equal(globalRef.Value, ex.ThrowableRef.Value);
			Assert.Equal(message, ex.Message);

			proxyEnv.Received(1).ThrowNew(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>());
			env.PendingException = default;

			ex.GlobalThrowable.Dispose();
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
}