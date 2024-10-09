namespace Rxmxnx.JNetInterface.Tests;

public partial class ExceptionHandlingTests
{
	[Theory]
	[InlineData]
	[InlineData(ExceptionOccurredError.GetClassObject)]
	[InlineData(ExceptionOccurredError.GetClassNameMethod)]
	[InlineData(ExceptionOccurredError.GetClassNameString)]
	[InlineData(ExceptionOccurredError.ClassNameUtfLength)]
	[InlineData(ExceptionOccurredError.ClassNameUtfChars)]
	[InlineData(ExceptionOccurredError.GetThrowableMessageMethod)]
	[InlineData(ExceptionOccurredError.GetThrowableMessageString)]
	[InlineData(ExceptionOccurredError.ThrowableMessageLength)]
	[InlineData(ExceptionOccurredError.ThrowableMessageGetRegion)]
	[InlineData(ExceptionOccurredError.NewGlobalRef)]
	[InlineData(ExceptionOccurredError.GetClassObject | ExceptionOccurredError.GetThrowableMessageString |
		ExceptionOccurredError.NewGlobalRef)]
	internal unsafe void ExceptionOccurredTest(ExceptionOccurredError error = ExceptionOccurredError.None)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JClassTypeMetadata throwableMetadata = IClassType.GetMetadata<JClassNotFoundExceptionObject>();
		using IFixedPointer.IDisposable clsCtx =
			throwableMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String message = "Throwable message";
			JGlobalRef globalRef = ExceptionHandlingTests.fixture.Create<JGlobalRef>();
			JStringLocalRef classNameRef = ExceptionHandlingTests.fixture.Create<JStringLocalRef>();
			JStringLocalRef messageRef = ExceptionHandlingTests.fixture.Create<JStringLocalRef>();
			JThrowableLocalRef throwableRef = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
			JClassLocalRef classRef = ExceptionHandlingTests.fixture.Create<JClassLocalRef>();
			IMutableWrapper<JThrowableLocalRef> exceptionOccurred = IMutableWrapper.Create<JThrowableLocalRef>();

			ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)nameCtx.Pointer;

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();
			proxyEnv.UseDefaultClassRef = false;

			proxyEnv.ExceptionCheck().Returns(_ => exceptionOccurred.Value != NativeInterfaceProxy.NoThrowable);
			proxyEnv.ExceptionOccurred().Returns(_ => exceptionOccurred.Value);
			proxyEnv.When(e => e.ExceptionClear()).Do(_ => exceptionOccurred.Value = default);
			proxyEnv.When(e => e.Throw(Arg.Any<JThrowableLocalRef>()))
			        .Do(c => exceptionOccurred.Value = (JThrowableLocalRef)c[0]);

			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);
			proxyEnv.FindClass(Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(_ =>
			{
				// Initial Error.
				exceptionOccurred.Value = throwableRef;
				return default;
			});
			proxyEnv.GetObjectClass(throwableRef.Value).Returns(_ =>
			{
				if (!error.HasFlag(ExceptionOccurredError.GetClassObject))
				{
					exceptionOccurred.Value = default;
					return classRef;
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
				return default;
			});
			proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
			                     Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				JClassLocalRef methodClassRef = (JClassLocalRef)c[0];
				ReadOnlyValPtr<Byte> methodNamePtr = (ReadOnlyValPtr<Byte>)c[1];
				JMethodId? methodId = proxyEnv.GetMainMethodId(methodClassRef, (Byte*)methodNamePtr.Pointer);
				if (!methodId.HasValue) return default;
				if ((methodId != proxyEnv.VirtualMachine.ClassGetNameMethodId ||
					    !error.HasFlag(ExceptionOccurredError.GetClassNameMethod)) &&
				    (methodId != proxyEnv.VirtualMachine.ThrowableGetMessageMethodId ||
					    !error.HasFlag(ExceptionOccurredError.GetThrowableMessageMethod)))
				{
					exceptionOccurred.Value = default;
					return methodId.GetValueOrDefault();
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
				return default;
			});
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(_ =>
			{
				if (!error.HasFlag(ExceptionOccurredError.GetClassNameString))
				{
					exceptionOccurred.Value = default;
					return classNameRef.Value;
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
				return default;
			});
			proxyEnv.GetStringUtfLength(classNameRef).Returns(_ =>
			{
				if (!error.HasFlag(ExceptionOccurredError.ClassNameUtfLength))
				{
					exceptionOccurred.Value = default;
					return throwableMetadata.ClassName.Length;
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
				return -1;
			});
			proxyEnv.GetStringUtfChars(classNameRef, Arg.Any<ValPtr<JBoolean>>()).Returns(_ =>
			{
				if (!error.HasFlag(ExceptionOccurredError.ClassNameUtfChars))
				{
					exceptionOccurred.Value = default;
					return namePtr;
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
				return default;
			});
			proxyEnv.CallObjectMethod(throwableRef.Value, proxyEnv.VirtualMachine.ThrowableGetMessageMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(_ =>
			{
				if (!error.HasFlag(ExceptionOccurredError.GetThrowableMessageString))
				{
					exceptionOccurred.Value = default;
					return messageRef.Value;
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
				return default;
			});
			proxyEnv.NewGlobalRef(throwableRef.Value).Returns(_ =>
			{
				if (!error.HasFlag(ExceptionOccurredError.NewGlobalRef))
				{
					exceptionOccurred.Value = default;
					return globalRef;
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
				return default;
			});
			proxyEnv.GetStringLength(messageRef).Returns(_ =>
			{
				if (!error.HasFlag(ExceptionOccurredError.ThrowableMessageLength))
				{
					exceptionOccurred.Value = default;
					return message.Length;
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
				return -1;
			});
			proxyEnv.When(e => e.GetStringRegion(Arg.Any<JStringLocalRef>(), Arg.Any<Int32>(), Arg.Any<Int32>(),
			                                     Arg.Any<ValPtr<Char>>())).Do(c =>
			{
				JStringLocalRef stringRef = (JStringLocalRef)c[0];
				Int32 start = (Int32)c[1];
				Int32 count = (Int32)c[2];
				ValPtr<Char> buffer = (ValPtr<Char>)c[3];

				Span<Char> span = buffer.Pointer.GetUnsafeSpan<Char>(count);
				if (messageRef == stringRef && !error.HasFlag(ExceptionOccurredError.ThrowableMessageGetRegion))
				{
					exceptionOccurred.Value = default;
					message.AsSpan()[start..count].CopyTo(span);
					return;
				}
				exceptionOccurred.Value = ExceptionHandlingTests.fixture.Create<JThrowableLocalRef>();
			});

			ExceptionHandlingTests.ExceptionAssert(error, env, globalRef, message);

			Int32 getClassNameCount = !error.HasFlag(ExceptionOccurredError.GetClassObject) &&
				!error.HasFlag(ExceptionOccurredError.GetClassNameMethod) ?
					1 :
					0;
			Int32 classNameUtfLengthCount =
				!error.HasFlag(ExceptionOccurredError.GetClassNameString) ? getClassNameCount : 0;
			Int32 classNameUtfChars =
				!error.HasFlag(ExceptionOccurredError.ClassNameUtfLength) ? classNameUtfLengthCount : 0;
			Int32 getMessageCount = !error.HasFlag(ExceptionOccurredError.GetThrowableMessageMethod) ? 1 : 0;
			Int32 getMessageLengthCount =
				!error.HasFlag(ExceptionOccurredError.GetThrowableMessageString) ? getMessageCount : 0;
			Int32 getMessageRegionCount = !error.HasFlag(ExceptionOccurredError.ThrowableMessageLength) ?
				getMessageLengthCount :
				0;
			Int32 throwGlobalCount = !error.HasFlag(ExceptionOccurredError.NewGlobalRef) ? 1 : 0;
			Int32 throwLocalCount = error.HasFlag(ExceptionOccurredError.NewGlobalRef) ? 1 : 0;

			proxyEnv.Received(1).FindClass(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).GetObjectClass(throwableRef.Value);
			proxyEnv.Received(getClassNameCount).CallObjectMethod(classRef.Value,
			                                                      proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                                                      ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(classNameUtfLengthCount).GetStringUtfLength(classNameRef);
			proxyEnv.Received(classNameUtfChars).GetStringUtfChars(classNameRef, Arg.Any<ValPtr<JBoolean>>());

			proxyEnv.Received(getMessageCount).CallObjectMethod(throwableRef.Value,
			                                                    proxyEnv.VirtualMachine.ThrowableGetMessageMethodId,
			                                                    ReadOnlyValPtr<JValueWrapper>.Zero);
			proxyEnv.Received(getMessageLengthCount).GetStringLength(messageRef);
			proxyEnv.Received(getMessageRegionCount)
			        .GetStringRegion(messageRef, 0, message.Length, Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(1).NewGlobalRef(throwableRef.Value);
			proxyEnv.Received(throwGlobalCount).Throw(JThrowableLocalRef.FromReference(globalRef.Value));
			proxyEnv.Received(throwLocalCount).Throw(throwableRef);

			proxyEnv.Received().ExceptionClear();
			proxyEnv.Received().ExceptionCheck();

			proxyEnv.ClearReceivedCalls();
			proxyEnv.VirtualMachine.ClearReceivedCalls();

			env.PendingException = default;
			proxyEnv.Received(1).ExceptionClear();
			Assert.Null(env.PendingException);
		}
		finally
		{
			nameCtx.Dispose();
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}

	private static void ExceptionAssert(ExceptionOccurredError error, IEnvironment env, JGlobalRef globalRef,
		String message)
	{
		JniException jniException =
			Assert.ThrowsAny<JniException>(
				() => JClassObject.GetClass(env, "rxmxnx/jnetinterface/test/NoExistingClass"u8));

		if (error.HasFlag(ExceptionOccurredError.NewGlobalRef))
		{
			Assert.IsAssignableFrom<CriticalException>(jniException);
		}
		else
		{
			ThrowableException throwableException = Assert.IsAssignableFrom<ThrowableException>(jniException);
			Assert.Same(throwableException, env.PendingException);
			Assert.Equal(globalRef.Value, throwableException.ThrowableRef.Value);
			Assert.Equal(Environment.CurrentManagedThreadId, throwableException.ThreadId);
			if (!error.HasFlag(ExceptionOccurredError.GetThrowableMessageMethod) &&
			    !error.HasFlag(ExceptionOccurredError.GetThrowableMessageString) &&
			    !error.HasFlag(ExceptionOccurredError.ThrowableMessageLength) &&
			    !error.HasFlag(ExceptionOccurredError.ThrowableMessageGetRegion))
				Assert.Equal(message, throwableException.Message);
			else
				Assert.Equal(Enum.GetName(JResult.Error), throwableException.Message);
			if (!error.HasFlag(ExceptionOccurredError.GetClassObject) &&
			    !error.HasFlag(ExceptionOccurredError.GetClassNameMethod) &&
			    !error.HasFlag(ExceptionOccurredError.GetClassNameString) &&
			    !error.HasFlag(ExceptionOccurredError.ClassNameUtfLength) &&
			    !error.HasFlag(ExceptionOccurredError.ClassNameUtfChars))
				Assert.IsType<ThrowableException<JClassNotFoundExceptionObject>>(throwableException);
			else
				Assert.IsType<ThrowableException<JThrowableObject>>(throwableException);
		}
	}
}