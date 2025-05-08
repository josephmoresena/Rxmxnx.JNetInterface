namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class DefineClassTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal unsafe void InvalidDefinitionTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JClassTypeMetadata throwableMetadata = IClassType.GetMetadata<JClassFormatErrorObject>();
		using IFixedPointer.IDisposable clsCtx =
			throwableMetadata.Information.GetFixedPointer(out IFixedPointer.IDisposable nameCtx);
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			Memory<Byte> binary = DefineClassTests.fixture.CreateMany<Byte>(Random.Shared.Next(10, 100)).ToArray();
			using MemoryHandle handle = binary.Pin();
			const String message = "Throwable message";
			JGlobalRef globalRef = DefineClassTests.fixture.Create<JGlobalRef>();
			JStringLocalRef classNameRef = DefineClassTests.fixture.Create<JStringLocalRef>();
			JStringLocalRef messageRef = DefineClassTests.fixture.Create<JStringLocalRef>();
			JThrowableLocalRef throwableRef = DefineClassTests.fixture.Create<JThrowableLocalRef>();
			JClassLocalRef classRef = DefineClassTests.fixture.Create<JClassLocalRef>();
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
			proxyEnv.GetObjectClass(throwableRef.Value).Returns(classRef);
			proxyEnv.GetMethodId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
			                     Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				JClassLocalRef methodClassRef = (JClassLocalRef)c[0];
				ReadOnlyValPtr<Byte> methodNamePtr = (ReadOnlyValPtr<Byte>)c[1];
				JMethodId? methodId = proxyEnv.GetMainMethodId(methodClassRef, (Byte*)methodNamePtr.Pointer);
				exceptionOccurred.Value = default;
				return methodId.GetValueOrDefault();
			});
			proxyEnv.CallObjectMethod(classRef.Value, proxyEnv.VirtualMachine.ClassGetNameMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(_ =>
			{
				exceptionOccurred.Value = default;
				return classNameRef.Value;
			});
			proxyEnv.GetStringUtfLength(classNameRef).Returns(_ =>
			{
				exceptionOccurred.Value = default;
				return throwableMetadata.ClassName.Length;
			});
			proxyEnv.GetStringUtfChars(classNameRef, Arg.Any<ValPtr<JBoolean>>()).Returns(_ =>
			{
				exceptionOccurred.Value = default;
				return namePtr;
			});
			proxyEnv.CallObjectMethod(throwableRef.Value, proxyEnv.VirtualMachine.ThrowableGetMessageMethodId,
			                          ReadOnlyValPtr<JValueWrapper>.Zero).Returns(_ =>
			{
				exceptionOccurred.Value = default;
				return messageRef.Value;
			});
			proxyEnv.NewGlobalRef(throwableRef.Value).Returns(_ =>
			{
				exceptionOccurred.Value = default;
				return globalRef;
			});
			proxyEnv.GetStringLength(messageRef).Returns(_ =>
			{
				exceptionOccurred.Value = default;
				return message.Length;
			});
			proxyEnv.When(e => e.GetStringRegion(Arg.Any<JStringLocalRef>(), Arg.Any<Int32>(), Arg.Any<Int32>(),
			                                     Arg.Any<ValPtr<Char>>())).Do(c =>
			{
				Int32 start = (Int32)c[1];
				Int32 count = (Int32)c[2];
				ValPtr<Char> buffer = (ValPtr<Char>)c[3];

				Span<Char> span = buffer.Pointer.GetUnsafeSpan<Char>(count);
				exceptionOccurred.Value = default;
				message.AsSpan()[start..count].CopyTo(span);
			});

			proxyEnv.DefineClass(Arg.Any<ReadOnlyValPtr<Byte>>(), default, handle.ToIntPtr(), binary.Length)
			        .Returns(_ =>
			        {
				        // Initial Error.
				        exceptionOccurred.Value = throwableRef;
				        return default;
			        });

			ThrowableException ex =
				Assert.Throws<ThrowableException<JClassFormatErrorObject>>(() => JClassObject.LoadClass(
					                                                           env,
					                                                           throwableMetadata.ClassName.AsSpan(),
					                                                           binary.Span));

			Assert.Equal(message, ex.Message);
			Assert.Equal(globalRef, ex.GlobalThrowable.As<JGlobalRef>());

			proxyEnv.Received(1)
			        .DefineClass(Arg.Any<ReadOnlyValPtr<Byte>>(), default, handle.ToIntPtr(), binary.Length);
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
	internal void RedefinitionTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			JClassLocalRef classRef = DefineClassTests.fixture.Create<JClassLocalRef>();
			Memory<Byte> binary = DefineClassTests.fixture.CreateMany<Byte>(Random.Shared.Next(10, 100)).ToArray();
			using MemoryHandle handle = binary.Pin();
			proxyEnv.DefineClass(Arg.Any<ReadOnlyValPtr<Byte>>(), default, handle.ToIntPtr(), binary.Length)
			        .Returns(classRef);

			Exception ex =
				Assert.Throws<InvalidOperationException>(() => JClassObject.LoadClass<JClassObject>(env, binary.Span));

			Assert.Equal(IMessageResource.GetInstance().ClassRedefinition, ex.Message);

			proxyEnv.Received(1)
			        .DefineClass(Arg.Any<ReadOnlyValPtr<Byte>>(), default, handle.ToIntPtr(), binary.Length);
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
	internal void SimpleTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			JClassLocalRef classRef = DefineClassTests.fixture.Create<JClassLocalRef>();
			Memory<Byte> binary = DefineClassTests.fixture.CreateMany<Byte>(Random.Shared.Next(10, 100)).ToArray();
			using MemoryHandle handle = binary.Pin();
			proxyEnv.DefineClass(Arg.Any<ReadOnlyValPtr<Byte>>(), default, handle.ToIntPtr(), binary.Length)
			        .Returns(classRef);

			using JClassObject jClass = JClassObject.LoadClass<JAnnotationObject>(env, binary.Span);

			Assert.Equal(classRef, jClass.Reference);
			proxyEnv.Received(1)
			        .DefineClass(Arg.Any<ReadOnlyValPtr<Byte>>(), default, handle.ToIntPtr(), binary.Length);
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