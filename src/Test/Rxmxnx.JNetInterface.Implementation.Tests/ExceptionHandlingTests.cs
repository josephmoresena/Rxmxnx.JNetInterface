namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed partial class ExceptionHandlingTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
#pragma warning disable CA1859
	internal void PendingExceptionTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String message = "Throwable message";
			JGlobalRef globalRef = ExceptionHandlingTests.fixture.Create<JGlobalRef>();
			JStringLocalRef messageRef = ExceptionHandlingTests.fixture.Create<JStringLocalRef>();

			using JThrowableObject jThrowable = TestUtilities.CreateThrowable(proxyEnv);
			JThrowableLocalRef throwableRef = jThrowable.Reference;

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();
			proxyEnv.CallObjectMethod(throwableRef.Value, proxyEnv.VirtualMachine.ThrowableGetMessageMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(messageRef.Value);
			proxyEnv.NewGlobalRef(throwableRef.Value).Returns(globalRef);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);
			proxyEnv.GetStringLength(messageRef).Returns(message.Length);
			proxyEnv.When(e => e.GetStringRegion(Arg.Any<JStringLocalRef>(), Arg.Any<Int32>(), Arg.Any<Int32>(),
			                                     Arg.Any<ValPtr<Char>>())).Do(c =>
			{
				JStringLocalRef stringRef = (JStringLocalRef)c[0];
				Int32 start = (Int32)c[1];
				Int32 count = (Int32)c[2];
				ValPtr<Char> buffer = (ValPtr<Char>)c[3];

				Span<Char> span = buffer.Pointer.GetUnsafeSpan<Char>(count);
				if (messageRef == stringRef)
					message.AsSpan()[start..count].CopyTo(span);
			});

			ThrowableException? throwableException = Assert.ThrowsAny<ThrowableException>(() => jThrowable.Throw());
			Assert.Same(throwableException, env.PendingException);
			Assert.Equal(globalRef.Value, throwableException.ThrowableRef.Value);
			Assert.Equal(Environment.CurrentManagedThreadId, throwableException.ThreadId);
			Assert.Equal(message, throwableException.Message);

			proxyEnv.Received(1).CallObjectMethod(throwableRef.Value,
			                                      proxyEnv.VirtualMachine.ThrowableGetMessageMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).NewGlobalRef(throwableRef.Value);
			proxyEnv.Received(1).GetStringLength(messageRef);
			proxyEnv.Received(1).GetStringRegion(messageRef, 0, message.Length, Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(1).Throw(JThrowableLocalRef.FromReference(globalRef.Value));
			proxyEnv.Received(0).ExceptionClear();
			proxyEnv.Received(0).ExceptionCheck();

			env.PendingException = default;
			proxyEnv.Received(1).ExceptionClear();
			Assert.Null(env.PendingException);

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			jThrowable.Throw(false);
			Assert.Equal(throwableException.Message, env.PendingException!.Message);
			Assert.Equal(throwableException.ThreadId, env.PendingException!.ThreadId);
			Assert.Equal(throwableException.ThrowableRef, env.PendingException!.ThrowableRef);
			Assert.Equal(throwableException.EnvironmentRef, env.PendingException!.EnvironmentRef);
			Assert.Equal(globalRef.Value, throwableException.ThrowableRef.Value);
			Assert.Equal(Environment.CurrentManagedThreadId, throwableException.ThreadId);
			Assert.Equal(message, throwableException.Message);

			throwableException = default;

			proxyEnv.Received(0).CallObjectMethod(throwableRef.Value,
			                                      proxyEnv.VirtualMachine.ThrowableGetMessageMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).NewGlobalRef(throwableRef.Value);
			proxyEnv.Received(0).GetStringLength(messageRef);
			proxyEnv.Received(0).GetStringRegion(messageRef, 0, message.Length, Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(1).Throw(JThrowableLocalRef.FromReference(globalRef.Value));
			proxyEnv.Received(0).ExceptionClear();
			proxyEnv.Received(0).ExceptionCheck();

			env.PendingException = throwableException;
			proxyEnv.Received(1).ExceptionClear();
			Assert.Null(env.PendingException);
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
#pragma warning restore CA1859
}