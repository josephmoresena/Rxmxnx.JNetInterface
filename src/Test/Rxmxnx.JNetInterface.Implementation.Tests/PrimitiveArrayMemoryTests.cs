namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class PrimitiveArrayMemoryTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalBooleanTest(JMemoryReferenceKind referenceKind)
		=> PrimitiveArrayMemoryTests.CriticalElementsTest<JBoolean>(referenceKind);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalByteTest(JMemoryReferenceKind referenceKind)
		=> PrimitiveArrayMemoryTests.CriticalElementsTest<JByte>(referenceKind);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalCharTest(JMemoryReferenceKind referenceKind)
		=> PrimitiveArrayMemoryTests.CriticalElementsTest<JChar>(referenceKind);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalDoubleTest(JMemoryReferenceKind referenceKind)
		=> PrimitiveArrayMemoryTests.CriticalElementsTest<JDouble>(referenceKind);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalFloatTest(JMemoryReferenceKind referenceKind)
		=> PrimitiveArrayMemoryTests.CriticalElementsTest<JFloat>(referenceKind);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalIntTest(JMemoryReferenceKind referenceKind)
		=> PrimitiveArrayMemoryTests.CriticalElementsTest<JInt>(referenceKind);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalLongTest(JMemoryReferenceKind referenceKind)
		=> PrimitiveArrayMemoryTests.CriticalElementsTest<JLong>(referenceKind);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	internal void CriticalShortTest(JMemoryReferenceKind referenceKind)
		=> PrimitiveArrayMemoryTests.CriticalElementsTest<JShort>(referenceKind);

	private static void CriticalElementsTest<TPrimitive>(JMemoryReferenceKind referenceKind)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JWeakRef weakRef = PrimitiveArrayMemoryTests.fixture.Create<JWeakRef>();
		JGlobalRef globalRef = PrimitiveArrayMemoryTests.fixture.Create<JGlobalRef>();
		JArrayLocalRef arrayRef = PrimitiveArrayMemoryTests.fixture.Create<JArrayLocalRef>();

		JArrayLocalRef wArrayRef = JArrayLocalRef.FromReference(weakRef.Value);
		JArrayLocalRef gArrayRef = JArrayLocalRef.FromReference(globalRef.Value);
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreateMany<TPrimitive>().ToArray();

			using JClassObject arrayClass = JClassObject.GetClass<JArrayObject<TPrimitive>>(env);
			using JArrayObject<TPrimitive> jArray = new(arrayClass, arrayRef, default);
			using IFixedContext<TPrimitive>.IDisposable fMem = value.AsMemory().GetFixedContext();
			ValPtr<Byte> valPtr = fMem.AsBinaryContext().ValuePointer;

			proxyEnv.NewGlobalRef(arrayRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(arrayRef.Value).Returns(weakRef);
			proxyEnv.GetArrayLength(arrayRef).Returns(value.Length);
			proxyEnv.GetPrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
			proxyEnv.GetPrimitiveArrayCritical(wArrayRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
			proxyEnv.GetPrimitiveArrayCritical(gArrayRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

			using JPrimitiveMemory<TPrimitive> seq = jArray.GetCriticalElements(referenceKind);
			Assert.Equal(value.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);
			Assert.Null(seq.ReleaseMode);
			seq.ReleaseMode = JReleaseMode.Abort;
			Assert.Null(seq.ReleaseMode);

			Assert.Throws<InvalidOperationException>(() => JClassObject.GetClass(env, "package/critical/Test"u8));

			proxyEnv.Received(1).GetArrayLength(arrayRef);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .NewWeakGlobalRef(arrayRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .NewGlobalRef(arrayRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.Local ? 1 : 0)
			        .GetPrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .GetPrimitiveArrayCritical(wArrayRef, Arg.Any<ValPtr<JBoolean>>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .GetPrimitiveArrayCritical(gArrayRef, Arg.Any<ValPtr<JBoolean>>());
		}
		finally
		{
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.Local ? 1 : 0)
			        .ReleasePrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<Byte>>(), Arg.Any<JReleaseMode>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .ReleasePrimitiveArrayCritical(wArrayRef, Arg.Any<ValPtr<Byte>>(), Arg.Any<JReleaseMode>());
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .ReleasePrimitiveArrayCritical(gArrayRef, Arg.Any<ValPtr<Byte>>(), Arg.Any<JReleaseMode>());
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