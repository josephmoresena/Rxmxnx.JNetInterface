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

			proxyEnv.NewGlobalRef(stringRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(stringRef.Value).Returns(weakRef);
			proxyEnv.GetStringLength(stringRef).Returns(value.Length);
			proxyEnv.GetStringCritical(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
			proxyEnv.GetStringCritical(wStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
			proxyEnv.GetStringCritical(gStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
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
	[Fact]
	internal void CriticalNestedFailTest()
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JStringLocalRef stringRef = StringMemoryTests.fixture.Create<JStringLocalRef>();
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			String value = StringMemoryTests.fixture.Create<String>();

			using JStringObject jString = new(env.ClassFeature.StringObject, stringRef);
			using IReadOnlyFixedContext<Char>.IDisposable fMem = value.AsMemory().GetFixedContext();

			proxyEnv.GetStringLength(stringRef).Returns(value.Length);
			proxyEnv.GetStringCritical(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);

			using JNativeMemory<Char> seq = jString.GetCriticalChars(JMemoryReferenceKind.Local);
			Assert.Equal(value.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);

			proxyEnv.ClearReceivedCalls();

			proxyEnv.ExceptionCheck().Returns(true);
			Assert.Throws<CriticalException>(() => JStringObject.Create(env, value));

			proxyEnv.Received(1).NewString(Arg.Any<ReadOnlyValPtr<Char>>(), Arg.Any<Int32>());
			proxyEnv.Received(1).ExceptionCheck();

			Assert.Throws<CriticalException>(() => env.PendingException);
			Assert.Throws<CriticalException>(() => env.PendingException = default);

			proxyEnv.ExceptionCheck().Returns(false);
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
	internal void CharsTest(JMemoryReferenceKind referenceKind)
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

			proxyEnv.NewGlobalRef(stringRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(stringRef.Value).Returns(weakRef);
			proxyEnv.GetStringLength(stringRef).Returns(value.Length);
			proxyEnv.GetStringChars(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
			proxyEnv.GetStringChars(wStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
			proxyEnv.GetStringChars(gStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

			using JNativeMemory<Char> seq = jString.GetNativeChars(referenceKind);
			Assert.Equal(value.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);

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
	internal void UtfCharsTest(JMemoryReferenceKind referenceKind)
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

			proxyEnv.NewGlobalRef(stringRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(stringRef.Value).Returns(weakRef);
			proxyEnv.GetStringLength(stringRef).Returns(value.Length);
			proxyEnv.GetStringUtfLength(stringRef).Returns(valueUtf.Length);
			proxyEnv.GetStringUtfChars(stringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
			proxyEnv.GetStringUtfChars(wStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
			proxyEnv.GetStringUtfChars(gStringRef, Arg.Any<ValPtr<JBoolean>>()).Returns(fMem.ValuePointer);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

			using JNativeMemory<Byte> seq = jString.GetNativeUtf8Chars(referenceKind);
			Assert.Equal(valueUtf.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);

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
}