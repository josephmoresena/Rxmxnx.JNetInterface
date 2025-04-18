namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class StringMemoryTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalCharsTest(JMemoryReferenceKind referenceKind)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JWeakRef weakRef = StringMemoryTests.fixture.Create<JWeakRef>();
		JGlobalRef globalRef = StringMemoryTests.fixture.Create<JGlobalRef>();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();

		JStringLocalRef wStringRef = JStringLocalRef.FromReference(weakRef.Value);
		JStringLocalRef gStringRef = JStringLocalRef.FromReference(globalRef.Value);
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String value = StringMemoryTests.fixture.Create<String>();

			using JStringObject jString = new(env.ClassFeature.StringObject, stringRef);
			using IReadOnlyFixedContext<Char>.IDisposable fMem = value.AsMemory().GetFixedContext();
			ReadOnlyValPtr<Char> valPtr = fMem.ValuePointer;

			proxyEnv.NewGlobalRef(stringRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(stringRef.Value).Returns(weakRef);
			proxyEnv.GetStringLength(stringRef).Returns(value.Length);
			proxyEnv.GetStringCritical(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
			proxyEnv.GetStringCritical(wStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
			proxyEnv.GetStringCritical(gStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

			using JNativeMemory<Char> seq = jString.GetCriticalChars(referenceKind);
			Assert.Equal(value.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);

			Assert.Throws<InvalidOperationException>(() => JClassObject.GetClass(env, "package/critical/Test"u8));

			proxyEnv.Received(1).GetStringLength(stringRef);
			proxyEnv.Received(0).GetStringChars(Arg.Any<JStringLocalRef>(), Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(0).GetStringRegion(Arg.Any<JStringLocalRef>(), Arg.Any<Int32>(), Arg.Any<Int32>(),
			                                     Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .NewWeakGlobalRef(stringRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .NewGlobalRef(stringRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.Local ? 1 : 0)
			        .GetStringCritical(stringRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .GetStringCritical(wStringRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .GetStringCritical(gStringRef, Arg.Any<ValPtr<JBoolean>>());
		}
		finally
		{
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.Local ? 1 : 0)
			        .ReleaseStringCritical(stringRef, Arg.Any<ReadOnlyValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .ReleaseStringCritical(wStringRef, Arg.Any<ReadOnlyValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .ReleaseStringCritical(gStringRef, Arg.Any<ReadOnlyValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .DeleteWeakGlobalRef(weakRef);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .DeleteGlobalRef(globalRef);

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	internal void CriticalNestedFailTest(Boolean isCopy)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String value = StringMemoryTests.fixture.Create<String>();

			using JStringObject jString = new(env.ClassFeature.StringObject, stringRef);
			using IReadOnlyFixedContext<Char>.IDisposable fMem = value.AsMemory().GetFixedContext();
			using JGlobal jGlobalThrowable = new(env.VirtualMachine,
			                                     new ThrowableObjectMetadata(new(env.ClassFeature.ThrowableObject))
			                                     {
				                                     Message = StringMemoryTests.fixture.Create<String>(),
			                                     }, StringMemoryTests.fixture.Create<JGlobalRef>());
			ReadOnlyValPtr<Char> valPtr = fMem.ValuePointer;

			proxyEnv.GetStringLength(stringRef).Returns(value.Length);
			proxyEnv.GetStringCritical(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});

			using JNativeMemory<Char> seq = jString.GetCriticalChars(JMemoryReferenceKind.Local);
			Assert.Equal(value.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);
			Assert.False(seq.Copy);

			proxyEnv.ClearReceivedCalls();

			proxyEnv.ExceptionCheck().Returns(true);
			Assert.Throws<CriticalException>(() => JStringObject.Create(env, value));

			proxyEnv.Received(1).NewString(Arg.Any<ReadOnlyValPtr<Char>>(), Arg.Any<Int32>());
			proxyEnv.Received(1).ExceptionCheck();

			Assert.Throws<CriticalException>(() => env.PendingException);
			Assert.Throws<CriticalException>(() => env.PendingException =
				                                 new ThrowableException<JThrowableObject>(jGlobalThrowable, default));
			// Support clear critical exception
			env.PendingException = default;
			proxyEnv.ExceptionCheck().Returns(false);

			proxyEnv.ClearReceivedCalls();
			StringMemoryTests.NestedFailTest(proxyEnv);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	internal void CharsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JWeakRef weakRef = StringMemoryTests.fixture.Create<JWeakRef>();
		JGlobalRef globalRef = StringMemoryTests.fixture.Create<JGlobalRef>();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();

		JStringLocalRef wStringRef = JStringLocalRef.FromReference(weakRef.Value);
		JStringLocalRef gStringRef = JStringLocalRef.FromReference(globalRef.Value);
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String value = StringMemoryTests.fixture.Create<String>();

			using JStringObject jString = new(env.ClassFeature.StringObject, stringRef);
			using IReadOnlyFixedContext<Char>.IDisposable fMem = value.AsMemory().GetFixedContext();
			ReadOnlyValPtr<Char> valPtr = fMem.ValuePointer;

			proxyEnv.NewGlobalRef(stringRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(stringRef.Value).Returns(weakRef);
			proxyEnv.GetStringLength(stringRef).Returns(value.Length);
			proxyEnv.GetStringChars(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetStringChars(wStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetStringChars(gStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

			using JNativeMemory<Char> seq = jString.GetNativeChars(referenceKind);
			Assert.Equal(value.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);
			Assert.Equal(isCopy, seq.Copy);

			proxyEnv.Received(1).GetStringLength(stringRef);
			proxyEnv.Received(0).GetStringCritical(Arg.Any<JStringLocalRef>(), Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(0).GetStringRegion(Arg.Any<JStringLocalRef>(), Arg.Any<Int32>(), Arg.Any<Int32>(),
			                                     Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .NewWeakGlobalRef(stringRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .NewGlobalRef(stringRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.Local ? 1 : 0)
			        .GetStringChars(stringRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .GetStringChars(wStringRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .GetStringChars(gStringRef, Arg.Any<ValPtr<JBoolean>>());
		}
		finally
		{
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.Local ? 1 : 0)
			        .ReleaseStringChars(stringRef, Arg.Any<ReadOnlyValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .ReleaseStringChars(wStringRef, Arg.Any<ReadOnlyValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .ReleaseStringChars(gStringRef, Arg.Any<ReadOnlyValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .DeleteWeakGlobalRef(weakRef);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .DeleteGlobalRef(globalRef);

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	internal void UtfCharsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JWeakRef weakRef = StringMemoryTests.fixture.Create<JWeakRef>();
		JGlobalRef globalRef = StringMemoryTests.fixture.Create<JGlobalRef>();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();

		JStringLocalRef wStringRef = JStringLocalRef.FromReference(weakRef.Value);
		JStringLocalRef gStringRef = JStringLocalRef.FromReference(globalRef.Value);
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String value = StringMemoryTests.fixture.Create<String>();
			Byte[] valueUtf = Encoding.UTF8.GetBytes(value);

			using JStringObject jString = new(env.ClassFeature.StringObject, stringRef);
			using IReadOnlyFixedContext<Byte>.IDisposable fMem = valueUtf.AsMemory().GetFixedContext();
			ReadOnlyValPtr<Byte> valPtr = fMem.ValuePointer;

			proxyEnv.NewGlobalRef(stringRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(stringRef.Value).Returns(weakRef);
			proxyEnv.GetStringLength(stringRef).Returns(value.Length);
			proxyEnv.GetStringUtfLength(stringRef).Returns(valueUtf.Length);
			proxyEnv.GetStringUtfChars(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetStringUtfChars(wStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetStringUtfChars(gStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

			using JNativeMemory<Byte> seq = jString.GetNativeUtf8Chars(referenceKind);
			Assert.Equal(valueUtf.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);
			Assert.Equal(isCopy, seq.Copy);

			proxyEnv.Received(referenceKind != JMemoryReferenceKind.Local ? 1 : 0).GetStringLength(stringRef);
			proxyEnv.Received(1).GetStringUtfLength(stringRef);
			proxyEnv.Received(0).GetStringChars(Arg.Any<JStringLocalRef>(), Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(0).GetStringCritical(Arg.Any<JStringLocalRef>(), Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(0).GetStringRegion(Arg.Any<JStringLocalRef>(), Arg.Any<Int32>(), Arg.Any<Int32>(),
			                                     Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .NewWeakGlobalRef(stringRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .NewGlobalRef(stringRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.Local ? 1 : 0)
			        .GetStringUtfChars(stringRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .GetStringUtfChars(wStringRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .GetStringUtfChars(gStringRef, Arg.Any<ValPtr<JBoolean>>());
		}
		finally
		{
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.Local ? 1 : 0)
			        .ReleaseStringUtfChars(stringRef, Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .ReleaseStringUtfChars(wStringRef, Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .ReleaseStringUtfChars(gStringRef, Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .DeleteWeakGlobalRef(weakRef);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .DeleteGlobalRef(globalRef);

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void UtfCharsCreation()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();
		String text = StringMemoryTests.fixture.Create<String>();
		Memory<Byte> utfText = Encoding.UTF8.GetBytes(text);

		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using MemoryHandle handle = text.AsMemory().Pin();
			using MemoryHandle utfHandle = utfText.Pin();

			proxyEnv.NewString((ReadOnlyValPtr<Char>)handle.ToIntPtr(), text.Length).Returns(stringRef);
			proxyEnv.NewStringUtf((ReadOnlyValPtr<Byte>)utfHandle.ToIntPtr()).Returns(stringRef);
			proxyEnv.GetStringLength(stringRef).Returns(text.Length);
			proxyEnv.GetStringUtfLength(stringRef).Returns(utfText.Length);
			proxyEnv.When(e => e.GetStringRegion(stringRef, 0, text.Length, Arg.Any<ValPtr<Char>>())).Do(c =>
			{
				ValPtr<Char> ptr = (ValPtr<Char>)c[3];
				text.CopyTo(ptr.Pointer.GetUnsafeSpan<Char>(text.Length));
			});

			using JStringObject jString = JStringObject.Create(env, utfText.Span);

			Assert.Equal(text, jString.Value);
			Assert.Equal(utfText.Length, jString.Utf8Length);
			Assert.Equal(text.Length, jString.Length);
			Assert.False(Object.ReferenceEquals(text, jString.Value));

			proxyEnv.Received(0).NewString(Arg.Any<ReadOnlyValPtr<Char>>(), Arg.Any<Int32>());
			proxyEnv.Received(1).NewStringUtf(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).GetStringRegion(stringRef, 0, text.Length, Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(1).GetStringLength(stringRef);
			proxyEnv.Received(0).GetStringUtfLength(stringRef);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void StringCreation()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();
		String text = StringMemoryTests.fixture.Create<String>();
		Memory<Byte> utfText = Encoding.UTF8.GetBytes(text);

		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using MemoryHandle handle = text.AsMemory().Pin();
			using MemoryHandle utfHandle = utfText.Pin();

			proxyEnv.NewString((ReadOnlyValPtr<Char>)handle.ToIntPtr(), text.Length).Returns(stringRef);
			proxyEnv.NewStringUtf((ReadOnlyValPtr<Byte>)utfHandle.ToIntPtr()).Returns(stringRef);
			proxyEnv.GetStringLength(stringRef).Returns(text.Length);
			proxyEnv.GetStringUtfLength(stringRef).Returns(utfText.Length);
			proxyEnv.When(e => e.GetStringRegion(stringRef, 0, text.Length, Arg.Any<ValPtr<Char>>())).Do(c =>
			{
				ValPtr<Char> ptr = (ValPtr<Char>)c[3];
				text.CopyTo(ptr.Pointer.GetUnsafeSpan<Char>(text.Length));
			});

			using JStringObject jString = JStringObject.Create(env, text);

			Assert.Equal(text, jString.Value);
			Assert.Equal(utfText.Length, jString.Utf8Length);
			Assert.Equal(text.Length, jString.Length);
			Assert.True(Object.ReferenceEquals(text, jString.Value));

			proxyEnv.Received(1).NewString(Arg.Any<ReadOnlyValPtr<Char>>(), Arg.Any<Int32>());
			proxyEnv.Received(0).NewStringUtf(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(0).GetStringRegion(stringRef, 0, text.Length, Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(0).GetStringLength(stringRef);
			proxyEnv.Received(1).GetStringUtfLength(stringRef);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Fact]
	internal void CharsCreation()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();
		String text = StringMemoryTests.fixture.Create<String>();
		Memory<Byte> utfText = Encoding.UTF8.GetBytes(text);

		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			using MemoryHandle handle = text.AsMemory().Pin();
			using MemoryHandle utfHandle = utfText.Pin();

			proxyEnv.NewString((ReadOnlyValPtr<Char>)handle.ToIntPtr(), text.Length).Returns(stringRef);
			proxyEnv.NewStringUtf((ReadOnlyValPtr<Byte>)utfHandle.ToIntPtr()).Returns(stringRef);
			proxyEnv.GetStringLength(stringRef).Returns(text.Length);
			proxyEnv.GetStringUtfLength(stringRef).Returns(utfText.Length);
			proxyEnv.When(e => e.GetStringRegion(stringRef, 0, text.Length, Arg.Any<ValPtr<Char>>())).Do(c =>
			{
				ValPtr<Char> ptr = (ValPtr<Char>)c[3];
				text.CopyTo(ptr.Pointer.GetUnsafeSpan<Char>(text.Length));
			});

			using JStringObject jString = JStringObject.Create(env, text.AsSpan());

			Assert.Equal(text, jString.Value);
			Assert.Equal(utfText.Length, jString.Utf8Length);
			Assert.Equal(text.Length, jString.Length);
			Assert.False(Object.ReferenceEquals(text, jString.Value));

			proxyEnv.Received(1).NewString(Arg.Any<ReadOnlyValPtr<Char>>(), Arg.Any<Int32>());
			proxyEnv.Received(0).NewStringUtf(Arg.Any<ReadOnlyValPtr<Byte>>());
			proxyEnv.Received(1).GetStringRegion(stringRef, 0, text.Length, Arg.Any<ValPtr<Char>>());
			proxyEnv.Received(0).GetStringLength(stringRef);
			proxyEnv.Received(1).GetStringUtfLength(stringRef);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	[Theory]
	[InlineData(0x00010008)]
	[InlineData(0x00090000)]
	[InlineData(0x00150000)]
	[InlineData(0x00180000)]
	[InlineData(0x00010008, true)]
	[InlineData(0x00090000, true)]
	[InlineData(0x00150000, true)]
	[InlineData(0x00180000, true)]
	[InlineData(0x00180000, true, true)]
	internal void UtfStringLongLengthTest(Int32 jniVersion, Boolean noInt32 = false, Boolean longLength = false)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();
		Boolean longLengthCapable = jniVersion >= 0x00180000;
		Int64 length = longLength && longLengthCapable ?
			Random.Shared.NextInt64(UInt32.MaxValue, 2L * UInt32.MaxValue) :
			Random.Shared.Next(0, Int32.MaxValue);

		proxyEnv.GetVersion().Returns(jniVersion);

		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStringObject>();
			using JClassObject jClass = JClassObject.GetClass<JStringObject>(env);
			using JStringObject jString =
				Assert.IsType<JStringObject>(typeMetadata.CreateInstance(jClass, stringRef.Value, true));

			unchecked
			{
				proxyEnv.GetStringUtfLength(stringRef).Returns((Int32)length);
				proxyEnv.GetStringUtfLongLength(stringRef).Returns(length);
			}

			if (!noInt32)
				Assert.Equal(length > Int32.MaxValue ? -1 : length, jString.Utf8Length);
			Assert.Equal(length, jString.Utf8LongLength);

			proxyEnv.Received(longLengthCapable ? 0 : 1).GetStringUtfLength(stringRef);
			proxyEnv.Received(longLengthCapable ? 1 : 0).GetStringUtfLongLength(stringRef);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}

	private static void NestedFailTest(NativeInterfaceProxy proxyEnv)
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		String value = StringMemoryTests.fixture.Create<String>();
		CStringSequence utf = new(value);
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();

		using JStringObject jString = new(env.ClassFeature.StringObject, stringRef);
		using IReadOnlyFixedContext<Char>.IDisposable fMem = value.AsMemory().GetFixedContext();
		using IFixedPointer.IDisposable fUtfMem = utf.GetFixedPointer();
		ReadOnlyValPtr<Char> valPtr = fMem.ValuePointer;
		ReadOnlyValPtr<Byte> valUtfPtr = (ReadOnlyValPtr<Byte>)fUtfMem.Pointer;

		proxyEnv.GetStringLength(stringRef).Returns(value.Length);
		proxyEnv.GetStringUtfLength(stringRef).Returns(utf[0].Length);
		proxyEnv.GetStringChars(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
		proxyEnv.GetStringCritical(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
		proxyEnv.GetStringUtfChars(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valUtfPtr);

		// Clears non-existing exception.
		env.PendingException = default;

		JNativeMemory<Char> seq = jString.GetNativeChars();
		Assert.Equal(value.Length, seq.Values.Length);
		Assert.Equal(fMem.Pointer, seq.Pointer);

		JNativeMemory<Char> seqCritical = jString.GetCriticalChars(JMemoryReferenceKind.Local);
		Assert.Equal(value.Length, seqCritical.Values.Length);
		Assert.Equal(fMem.Pointer, seqCritical.Pointer);

		JNativeMemory<Byte> seqUtf = jString.GetNativeUtf8Chars();
		Assert.Equal(utf[0].Length, seqUtf.Values.Length);
		Assert.Equal(valUtfPtr.Pointer, seqUtf.Pointer);

		proxyEnv.ExceptionCheck().Returns(true);
		Assert.Throws<CriticalException>(() => seq.Dispose());

		proxyEnv.ExceptionCheck().Returns(false);
		env.PendingException = default;

		proxyEnv.ExceptionCheck().Returns(true);
		Assert.Throws<CriticalException>(() => seqCritical.Dispose());

		proxyEnv.ExceptionCheck().Returns(false);
		env.PendingException = default;

		proxyEnv.ExceptionCheck().Returns(true);
		Assert.Throws<CriticalException>(() => seqUtf.Dispose());

		proxyEnv.ExceptionCheck().Returns(false);
		env.PendingException = default;

		seq.Dispose();
		seqCritical.Dispose();
		seqUtf.Dispose();
	}
}