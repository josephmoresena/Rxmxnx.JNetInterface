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
			Assert.Throws<CriticalException>(() => env.PendingException = default);

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