namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class ExceptionHandlingTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
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

			ThrowableException? throwableException = Assert.ThrowsAny<ThrowableException>(jThrowable.Throw);
			Assert.Same(throwableException, env.PendingException);
			Assert.Equal(globalRef.Value, throwableException.ThrowableRef.Value);
			Assert.Equal(Environment.CurrentManagedThreadId, throwableException.ThreadId);
			Assert.Equal(message, throwableException.Message);

			throwableException = default;

			proxyEnv.Received(1).CallObjectMethod(throwableRef.Value,
			                                      proxyEnv.VirtualMachine.ThrowableGetMessageMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).NewGlobalRef(throwableRef.Value);
			proxyEnv.Received(1).GetStringLength(messageRef);
			proxyEnv.Received(1).GetStringRegion(messageRef, 0, message.Length, Arg.Any<ValPtr<Char>>());
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
	[Fact]
	internal void ExceptionOccurredTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String message = "Throwable message";
			JGlobalRef globalRef = ExceptionHandlingTests.fixture.Create<JGlobalRef>();
			JStringLocalRef classNameRef = ExceptionHandlingTests.fixture.Create<JStringLocalRef>();
			JStringLocalRef messageRef = ExceptionHandlingTests.fixture.Create<JStringLocalRef>();
			JThrowableLocalRef throwableRef = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
			JClassLocalRef classRef = ExceptionHandlingTests.fixture.Create<JClassLocalRef>();
			JClassTypeMetadata throwableMetadata = IClassType.GetMetadata<JClassNotFoundExceptionObject>();
			IMutableWrapper<JThrowableLocalRef> exceptionOccurred = IMutableWrapper.Create<JThrowableLocalRef>();

			using IFixedPointer.IDisposable clsCtx = throwableMetadata.Information.GetFixedPointer();

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			proxyEnv.ExceptionCheck().Returns(_ => exceptionOccurred.Value != NativeInterfaceProxy.NoThrowable);
			proxyEnv.ExceptionOccurred().Returns(_ =>
			{
				try
				{
					return exceptionOccurred.Value;
				}
				finally
				{
					if (exceptionOccurred.Value == throwableRef)
						exceptionOccurred.Value = default;
				}
			});
			proxyEnv.When(e => e.ExceptionClear()).Do(_ => exceptionOccurred.Value = default);
			proxyEnv.When(e => e.Throw(Arg.Any<JThrowableLocalRef>()))
			        .Do(c => exceptionOccurred.Value = (JThrowableLocalRef)c[0]);

			proxyEnv.FindClass(Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				exceptionOccurred.Value = throwableRef;
				return default;
			});
			proxyEnv.GetObjectClass(throwableRef.Value).Returns(classRef);
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(classNameRef.Value);
			proxyEnv.GetStringUtfLength(classNameRef).Returns(throwableMetadata.ClassName.Length);
			proxyEnv.GetStringUtfChars(classNameRef, Arg.Any<ValPtr<JBoolean>>())
			        .Returns((ReadOnlyValPtr<Byte>)clsCtx.Pointer);

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

			ThrowableException? throwableException =
				Assert.ThrowsAny<ThrowableException>(
					() => JClassObject.GetClass(env, "rxmxnx/jnetinterface/test/NoExistingClass"u8));
			Assert.Same(throwableException, env.PendingException);
			Assert.Equal(globalRef.Value, throwableException.ThrowableRef.Value);
			Assert.Equal(Environment.CurrentManagedThreadId, throwableException.ThreadId);
			Assert.Equal(message, throwableException.Message);
			Assert.IsType<ThrowableException<JClassNotFoundExceptionObject>>(throwableException);

			throwableException = default;

			proxyEnv.Received(1).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).GetObjectClass(throwableRef.Value);
			proxyEnv.Received(1).CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).GetStringUtfLength(classNameRef);
			proxyEnv.Received(1).GetStringUtfChars(classNameRef, Arg.Any<ValPtr<JBoolean>>());

			proxyEnv.Received(1).CallObjectMethod(throwableRef.Value,
			                                      proxyEnv.VirtualMachine.ThrowableGetMessageMethodId,
			                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(1).NewGlobalRef(throwableRef.Value);
			proxyEnv.Received(1).GetStringLength(messageRef);
			proxyEnv.Received(1).GetStringRegion(messageRef, 0, message.Length, Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(1).Throw(JThrowableLocalRef.FromReference(globalRef.Value));
			proxyEnv.Received(1).ExceptionClear();
			proxyEnv.Received().ExceptionCheck();

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

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
}