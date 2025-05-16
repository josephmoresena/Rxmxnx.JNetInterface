namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class PrimitiveArrayMemoryTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[Fact]
	internal void GetSetBooleanTest() => PrimitiveArrayMemoryTests.GetSetTest<JBoolean>();
	[Fact]
	internal void GetSetByteTest() => PrimitiveArrayMemoryTests.GetSetTest<JByte>();
	[Fact]
	internal void GetSetCharTest() => PrimitiveArrayMemoryTests.GetSetTest<JChar>();
	[Fact]
	internal void GetSetDoubleTest() => PrimitiveArrayMemoryTests.GetSetTest<JDouble>();
	[Fact]
	internal void GetSetFloatTest() => PrimitiveArrayMemoryTests.GetSetTest<JFloat>();
	[Fact]
	internal void GetSetIntTest() => PrimitiveArrayMemoryTests.GetSetTest<JInt>();
	[Fact]
	internal void GetSetLongTest() => PrimitiveArrayMemoryTests.GetSetTest<JLong>();
	[Fact]
	internal void GetSetShortTest() => PrimitiveArrayMemoryTests.GetSetTest<JShort>();

	[Fact]
	internal void IndexOfBooleanTest() => PrimitiveArrayMemoryTests.IndexOfTest<JBoolean>();
	[Fact]
	internal void IndexOfByteTest() => PrimitiveArrayMemoryTests.IndexOfTest<JByte>();
	[Fact]
	internal void IndexOfCharTest() => PrimitiveArrayMemoryTests.IndexOfTest<JChar>();
	[Fact]
	internal void IndexOfDoubleTest() => PrimitiveArrayMemoryTests.IndexOfTest<JDouble>();
	[Fact]
	internal void IndexOfFloatTest() => PrimitiveArrayMemoryTests.IndexOfTest<JFloat>();
	[Fact]
	internal void IndexOfIntTest() => PrimitiveArrayMemoryTests.IndexOfTest<JInt>();
	[Fact]
	internal void IndexOfLongTest() => PrimitiveArrayMemoryTests.IndexOfTest<JLong>();
	[Fact]
	internal void IndexOfShortTest() => PrimitiveArrayMemoryTests.IndexOfTest<JShort>();

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

	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Abort)]
	internal void BooleanElementsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false,
		JReleaseMode releaseMode = JReleaseMode.Free)
		=> PrimitiveArrayMemoryTests.ElementTest<JBoolean>(referenceKind, isCopy, releaseMode);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Abort)]
	internal void ByteElementsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false,
		JReleaseMode releaseMode = JReleaseMode.Free)
		=> PrimitiveArrayMemoryTests.ElementTest<JByte>(referenceKind, isCopy, releaseMode);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Abort)]
	internal void CharElementsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false,
		JReleaseMode releaseMode = JReleaseMode.Free)
		=> PrimitiveArrayMemoryTests.ElementTest<JChar>(referenceKind, isCopy, releaseMode);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Abort)]
	internal void DoubleElementsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false,
		JReleaseMode releaseMode = JReleaseMode.Free)
		=> PrimitiveArrayMemoryTests.ElementTest<JDouble>(referenceKind, isCopy, releaseMode);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Abort)]
	internal void FloatElementsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false,
		JReleaseMode releaseMode = JReleaseMode.Free)
		=> PrimitiveArrayMemoryTests.ElementTest<JFloat>(referenceKind, isCopy, releaseMode);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Abort)]
	internal void IntElementsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false,
		JReleaseMode releaseMode = JReleaseMode.Free)
		=> PrimitiveArrayMemoryTests.ElementTest<JInt>(referenceKind, isCopy, releaseMode);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Abort)]
	internal void LongElementsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false,
		JReleaseMode releaseMode = JReleaseMode.Free)
		=> PrimitiveArrayMemoryTests.ElementTest<JLong>(referenceKind, isCopy, releaseMode);
	[Theory]
	[InlineData(JMemoryReferenceKind.Local)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted)]
	[InlineData(JMemoryReferenceKind.Local, true)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Commit)]
	[InlineData(JMemoryReferenceKind.Local, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, false, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.Local, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadIndependent, true, JReleaseMode.Abort)]
	[InlineData(JMemoryReferenceKind.ThreadUnrestricted, true, JReleaseMode.Abort)]
	internal void ShortElementsTest(JMemoryReferenceKind referenceKind, Boolean isCopy = false,
		JReleaseMode releaseMode = JReleaseMode.Free)
		=> PrimitiveArrayMemoryTests.ElementTest<JShort>(referenceKind, isCopy, releaseMode);

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
			TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreatePrimitiveArray<TPrimitive>(3);

			using JClassObject arrayClass = JClassObject.GetClass<JArrayObject<TPrimitive>>(env);
			using JArrayObject<TPrimitive> jArray = new(arrayClass, arrayRef);
			using IFixedContext<TPrimitive>.IDisposable fMem = value.AsMemory().GetFixedContext();
			ValPtr<Byte> valPtr = fMem.AsBinaryContext().ValuePointer;
			Boolean isCopy = PrimitiveArrayMemoryTests.fixture.Create<Boolean>();

			proxyEnv.NewGlobalRef(arrayRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(arrayRef.Value).Returns(weakRef);
			proxyEnv.GetArrayLength(arrayRef).Returns(value.Length);
			proxyEnv.GetPrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetPrimitiveArrayCritical(wArrayRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetPrimitiveArrayCritical(gArrayRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

			using JPrimitiveMemory<TPrimitive> seq = jArray.GetCriticalElements(referenceKind);
			Assert.Equal(value.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);
			Assert.Null(seq.ReleaseMode);
			Assert.False(seq.Copy);
			seq.ReleaseMode = JReleaseMode.Abort;
			Assert.Null(seq.ReleaseMode);

			Assert.Throws<UnsafeStateException>(() => JClassObject.GetClass(env, "package/critical/Test"u8));

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
			PrimitiveArrayMemoryTests.NestedFailTest<TPrimitive>(proxyEnv);
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
	private static void IndexOfTest<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JArrayLocalRef arrayRef = PrimitiveArrayMemoryTests.fixture.Create<JArrayLocalRef>();

		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreatePrimitiveArray<TPrimitive>(10);

			using JClassObject arrayClass = JClassObject.GetClass<JArrayObject<TPrimitive>>(env);
			using JArrayObject<TPrimitive> jArray = new(arrayClass, arrayRef);
			using IFixedContext<TPrimitive>.IDisposable fMem = value.AsMemory().GetFixedContext();
			ValPtr<Byte> valPtr = fMem.AsBinaryContext().ValuePointer;
			Boolean isCopy = PrimitiveArrayMemoryTests.fixture.Create<Boolean>();

			proxyEnv.GetArrayLength(arrayRef).Returns(value.Length);
			proxyEnv.GetPrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
			{
				ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
				isCopyPtr.Reference = isCopy;
				return valPtr;
			});

			IList<TPrimitive> list = jArray;
			for (Int32 i = 0; i < value.Length; i++)
				Assert.InRange(list.IndexOf(value[i]), 0, i);

			TPrimitive[] destination = new TPrimitive[jArray.Length * 2];
			list.CopyTo(destination, 4);

			Assert.Equal(value, destination[4..(4 + value.Length)]);

			proxyEnv.Received(1).GetArrayLength(arrayRef);
			proxyEnv.Received().GetPrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<JBoolean>>());
		}
		finally
		{
			proxyEnv.Received()
			        .ReleasePrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<Byte>>(), Arg.Any<JReleaseMode>());

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	private static void ElementTest<TPrimitive>(JMemoryReferenceKind referenceKind, Boolean isCopy,
		JReleaseMode releaseMode) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JWeakRef weakRef = PrimitiveArrayMemoryTests.fixture.Create<JWeakRef>();
		JGlobalRef globalRef = PrimitiveArrayMemoryTests.fixture.Create<JGlobalRef>();
		JArrayLocalRef arrayRef = PrimitiveArrayMemoryTests.fixture.Create<JArrayLocalRef>();

		JArrayLocalRef wArrayRef = JArrayLocalRef.FromReference(weakRef.Value);
		JArrayLocalRef gArrayRef = JArrayLocalRef.FromReference(globalRef.Value);
		Byte signature = IPrimitiveType.GetMetadata<TPrimitive>().Signature[0];
		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreatePrimitiveArray<TPrimitive>(3);

			using JClassObject arrayClass = JClassObject.GetClass<JArrayObject<TPrimitive>>(env);
			using JArrayObject<TPrimitive> jArray = new(arrayClass, arrayRef);
			using IFixedContext<TPrimitive>.IDisposable fMem = value.AsMemory().GetFixedContext();
			ValPtr<Byte> valPtr = fMem.AsBinaryContext().ValuePointer;

			proxyEnv.NewGlobalRef(arrayRef.Value).Returns(globalRef);
			proxyEnv.NewWeakGlobalRef(arrayRef.Value).Returns(weakRef);
			proxyEnv.GetArrayLength(arrayRef).Returns(value.Length);
			PrimitiveArrayMemoryTests.ConfigureGetElements(proxyEnv, ref arrayRef, isCopy, valPtr.Pointer);
			PrimitiveArrayMemoryTests.ConfigureGetElements(proxyEnv, ref wArrayRef, isCopy, valPtr.Pointer);
			PrimitiveArrayMemoryTests.ConfigureGetElements(proxyEnv, ref gArrayRef, isCopy, valPtr.Pointer);
			proxyEnv.GetObjectRefType(weakRef.Value).Returns(JReferenceType.WeakGlobalRefType);
			proxyEnv.GetObjectRefType(globalRef.Value).Returns(JReferenceType.GlobalRefType);

			using JPrimitiveMemory<TPrimitive> seq = jArray.GetElements(referenceKind);
			Assert.Equal(value.Length, seq.Values.Length);
			Assert.Equal(fMem.Pointer, seq.Pointer);
			Assert.Equal(JReleaseMode.Free, seq.ReleaseMode);
			Assert.Equal(isCopy, seq.Copy);
			seq.ReleaseMode = releaseMode;
			Assert.Equal(releaseMode, seq.ReleaseMode);

			proxyEnv.Received(1).GetArrayLength(arrayRef);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .NewWeakGlobalRef(arrayRef.Value);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .NewGlobalRef(arrayRef.Value);
			PrimitiveArrayMemoryTests.AssertReceivedGetElements(proxyEnv, ref arrayRef, signature,
			                                                    referenceKind is JMemoryReferenceKind.Local ? 1 : 0);
			PrimitiveArrayMemoryTests.AssertReceivedGetElements(proxyEnv, ref wArrayRef, signature,
			                                                    referenceKind is JMemoryReferenceKind
				                                                    .ThreadIndependent ?
				                                                    1 :
				                                                    0);
			PrimitiveArrayMemoryTests.AssertReceivedGetElements(proxyEnv, ref gArrayRef, signature,
			                                                    referenceKind is JMemoryReferenceKind
				                                                    .ThreadUnrestricted ?
				                                                    1 :
				                                                    0);
		}
		finally
		{
			PrimitiveArrayMemoryTests.AssertReceivedReleaseElements(proxyEnv, ref arrayRef, signature,
			                                                        referenceKind is JMemoryReferenceKind.Local ? 1 : 0,
			                                                        releaseMode);
			PrimitiveArrayMemoryTests.AssertReceivedReleaseElements(proxyEnv, ref wArrayRef, signature,
			                                                        referenceKind is JMemoryReferenceKind
				                                                        .ThreadIndependent ?
				                                                        1 :
				                                                        0, releaseMode);
			PrimitiveArrayMemoryTests.AssertReceivedReleaseElements(proxyEnv, ref gArrayRef, signature,
			                                                        referenceKind is JMemoryReferenceKind
				                                                        .ThreadUnrestricted ?
				                                                        1 :
				                                                        0, releaseMode);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadIndependent ? 1 : 0)
			        .DeleteWeakGlobalRef(weakRef);
			proxyEnv.Received(referenceKind is JMemoryReferenceKind.ThreadUnrestricted ? 1 : 0)
			        .DeleteGlobalRef(globalRef);

			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
	private static void GetSetTest<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		JArrayLocalRef arrayRef = PrimitiveArrayMemoryTests.fixture.Create<JArrayLocalRef>();
		Byte signature = IPrimitiveType.GetMetadata<TPrimitive>().Signature[0];

		try
		{
			IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
			TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreatePrimitiveArray<TPrimitive>(10);
			TPrimitive[] newValue = PrimitiveArrayMemoryTests.fixture.CreatePrimitiveArray<TPrimitive>(10);

			using JClassObject arrayClass = JClassObject.GetClass<JArrayObject<TPrimitive>>(env);
			using JArrayObject<TPrimitive> jArray = new(arrayClass, arrayRef);
			using IFixedContext<TPrimitive>.IDisposable fMem = value.AsMemory().GetFixedContext();

			proxyEnv.GetArrayLength(arrayRef).Returns(value.Length);
			PrimitiveArrayMemoryTests.ConfigureGetAndSet(proxyEnv, arrayRef, fMem.Pointer);

			for (Int32 i = 0; i < value.Length; i++)
			{
				proxyEnv.Received(0).GetArrayLength(arrayRef);
				proxyEnv.ClearReceivedCalls();
				Assert.Equal(value[i], jArray[i]);
				PrimitiveArrayMemoryTests.AssertReceivedGetArrayRegion(proxyEnv, ref arrayRef, signature, 1);
				jArray[i] = newValue[i];
				PrimitiveArrayMemoryTests.AssertReceivedSetArrayRegion(proxyEnv, ref arrayRef, signature, 1);
				proxyEnv.Received(0).GetPrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<JBoolean>>());
				proxyEnv.Received(0)
				        .ReleasePrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<Byte>>(), Arg.Any<JReleaseMode>());
			}
			Assert.True(newValue.SequenceEqual(value));
			proxyEnv.ClearReceivedCalls();

			TPrimitive[] source = PrimitiveArrayMemoryTests.fixture.CreatePrimitiveArray<TPrimitive>(value.Length / 2);
			TPrimitive[] destination = new TPrimitive[value.Length / 2];

			jArray.Get(destination, 2);
			PrimitiveArrayMemoryTests.AssertReceivedGetArrayRegion(proxyEnv, ref arrayRef, signature, 1,
			                                                       destination.Length);
			Assert.Equal(destination, value[2..(2 + destination.Length)]);

			jArray.Set(source, 1);
			PrimitiveArrayMemoryTests.AssertReceivedSetArrayRegion(proxyEnv, ref arrayRef, signature, 1, source.Length);
			Assert.Equal(source, value[1..(1 + source.Length)]);
		}
		finally
		{
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.True(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}

	private static void ConfigureGetAndSet(NativeInterfaceProxy proxyEnv, JArrayLocalRef arrayRef, IntPtr valPtr)
	{
		proxyEnv.When(e => e.GetBooleanArrayRegion(JBooleanArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                           Arg.Any<Int32>(), Arg.Any<ValPtr<JBoolean>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ValPtr<JBoolean> ptr = (ValPtr<JBoolean>)c[3];
			ReadOnlyValPtr<JBoolean> source = (ReadOnlyValPtr<JBoolean>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.GetByteArrayRegion(JByteArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                        Arg.Any<Int32>(), Arg.Any<ValPtr<JByte>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ValPtr<JByte> ptr = (ValPtr<JByte>)c[3];
			ReadOnlyValPtr<JByte> source = (ReadOnlyValPtr<JByte>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.GetCharArrayRegion(JCharArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                        Arg.Any<Int32>(), Arg.Any<ValPtr<JChar>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ValPtr<JChar> ptr = (ValPtr<JChar>)c[3];
			ReadOnlyValPtr<JChar> source = (ReadOnlyValPtr<JChar>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.GetDoubleArrayRegion(JDoubleArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                          Arg.Any<Int32>(), Arg.Any<ValPtr<JDouble>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ValPtr<JDouble> ptr = (ValPtr<JDouble>)c[3];
			ReadOnlyValPtr<JDouble> source = (ReadOnlyValPtr<JDouble>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.GetFloatArrayRegion(JFloatArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                         Arg.Any<Int32>(), Arg.Any<ValPtr<JFloat>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ValPtr<JFloat> ptr = (ValPtr<JFloat>)c[3];
			ReadOnlyValPtr<JFloat> source = (ReadOnlyValPtr<JFloat>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.GetIntArrayRegion(JIntArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                       Arg.Any<Int32>(), Arg.Any<ValPtr<JInt>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ValPtr<JInt> ptr = (ValPtr<JInt>)c[3];
			ReadOnlyValPtr<JInt> source = (ReadOnlyValPtr<JInt>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.GetLongArrayRegion(JLongArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                        Arg.Any<Int32>(), Arg.Any<ValPtr<JLong>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ValPtr<JLong> ptr = (ValPtr<JLong>)c[3];
			ReadOnlyValPtr<JLong> source = (ReadOnlyValPtr<JLong>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.GetShortArrayRegion(JShortArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                         Arg.Any<Int32>(), Arg.Any<ValPtr<JShort>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ValPtr<JShort> ptr = (ValPtr<JShort>)c[3];
			ReadOnlyValPtr<JShort> source = (ReadOnlyValPtr<JShort>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.SetBooleanArrayRegion(JBooleanArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                           Arg.Any<Int32>(), Arg.Any<ReadOnlyValPtr<JBoolean>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ReadOnlyValPtr<JBoolean> source = (ReadOnlyValPtr<JBoolean>)c[3];
			ValPtr<JBoolean> ptr = (ValPtr<JBoolean>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.SetByteArrayRegion(JByteArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                        Arg.Any<Int32>(), Arg.Any<ReadOnlyValPtr<JByte>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ReadOnlyValPtr<JByte> source = (ReadOnlyValPtr<JByte>)c[3];
			ValPtr<JByte> ptr = (ValPtr<JByte>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.SetCharArrayRegion(JCharArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                        Arg.Any<Int32>(), Arg.Any<ReadOnlyValPtr<JChar>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ReadOnlyValPtr<JChar> source = (ReadOnlyValPtr<JChar>)c[3];
			ValPtr<JChar> ptr = (ValPtr<JChar>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.SetDoubleArrayRegion(JDoubleArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                          Arg.Any<Int32>(), Arg.Any<ReadOnlyValPtr<JDouble>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ReadOnlyValPtr<JDouble> source = (ReadOnlyValPtr<JDouble>)c[3];
			ValPtr<JDouble> ptr = (ValPtr<JDouble>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.SetFloatArrayRegion(JFloatArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                         Arg.Any<Int32>(), Arg.Any<ReadOnlyValPtr<JFloat>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ReadOnlyValPtr<JFloat> source = (ReadOnlyValPtr<JFloat>)c[3];
			ValPtr<JFloat> ptr = (ValPtr<JFloat>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.SetIntArrayRegion(JIntArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                       Arg.Any<Int32>(), Arg.Any<ReadOnlyValPtr<JInt>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ReadOnlyValPtr<JInt> source = (ReadOnlyValPtr<JInt>)c[3];
			ValPtr<JInt> ptr = (ValPtr<JInt>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.SetLongArrayRegion(JLongArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                        Arg.Any<Int32>(), Arg.Any<ReadOnlyValPtr<JLong>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ReadOnlyValPtr<JLong> source = (ReadOnlyValPtr<JLong>)c[3];
			ValPtr<JLong> ptr = (ValPtr<JLong>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
		proxyEnv.When(e => e.SetShortArrayRegion(JShortArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(),
		                                         Arg.Any<Int32>(), Arg.Any<ReadOnlyValPtr<JShort>>())).Do(c =>
		{
			Int32 offset = (Int32)c[1];
			ReadOnlyValPtr<JShort> source = (ReadOnlyValPtr<JShort>)c[3];
			ValPtr<JShort> ptr = (ValPtr<JShort>)valPtr + offset;
			Int32 items = (Int32)c[2];
			MemoryMarshal.CreateReadOnlySpan(in source.Reference, items)
			             .CopyTo(MemoryMarshal.CreateSpan(ref ptr.Reference, items));
		});
	}
	private static void ConfigureGetElements(NativeInterfaceProxy proxyEnv, ref JArrayLocalRef arrayRef, Boolean isCopy,
		IntPtr valPtr)
	{
		proxyEnv.GetBooleanArrayElements(arrayRef.Transform<JArrayLocalRef, JBooleanArrayLocalRef>(),
		                                 Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
		{
			ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
			isCopyPtr.Reference = isCopy;
			return (ValPtr<JBoolean>)valPtr;
		});
		proxyEnv.GetByteArrayElements(arrayRef.Transform<JArrayLocalRef, JByteArrayLocalRef>(),
		                              Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
		{
			ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
			isCopyPtr.Reference = isCopy;
			return (ValPtr<JByte>)valPtr;
		});
		proxyEnv.GetCharArrayElements(arrayRef.Transform<JArrayLocalRef, JCharArrayLocalRef>(),
		                              Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
		{
			ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
			isCopyPtr.Reference = isCopy;
			return (ValPtr<JChar>)valPtr;
		});
		proxyEnv.GetDoubleArrayElements(arrayRef.Transform<JArrayLocalRef, JDoubleArrayLocalRef>(),
		                                Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
		{
			ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
			isCopyPtr.Reference = isCopy;
			return (ValPtr<JDouble>)valPtr;
		});
		proxyEnv.GetFloatArrayElements(arrayRef.Transform<JArrayLocalRef, JFloatArrayLocalRef>(),
		                               Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
		{
			ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
			isCopyPtr.Reference = isCopy;
			return (ValPtr<JFloat>)valPtr;
		});
		proxyEnv.GetIntArrayElements(arrayRef.Transform<JArrayLocalRef, JIntArrayLocalRef>(),
		                             Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
		{
			ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
			isCopyPtr.Reference = isCopy;
			return (ValPtr<JInt>)valPtr;
		});
		proxyEnv.GetLongArrayElements(arrayRef.Transform<JArrayLocalRef, JLongArrayLocalRef>(),
		                              Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
		{
			ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
			isCopyPtr.Reference = isCopy;
			return (ValPtr<JLong>)valPtr;
		});
		proxyEnv.GetShortArrayElements(arrayRef.Transform<JArrayLocalRef, JShortArrayLocalRef>(),
		                               Arg.Any<ValPtr<JBoolean>>()).Returns(c =>
		{
			ValPtr<JBoolean> isCopyPtr = (ValPtr<JBoolean>)c[1];
			isCopyPtr.Reference = isCopy;
			return (ValPtr<JShort>)valPtr;
		});
	}
	private static void AssertReceivedGetElements(NativeInterfaceProxy proxyEnv, ref JArrayLocalRef arrayRef,
		Byte signature, Int32 count)
	{
		proxyEnv.Received(signature == CommonNames.BooleanSignatureChar ? count : 0)
		        .GetBooleanArrayElements(arrayRef.Transform<JArrayLocalRef, JBooleanArrayLocalRef>(),
		                                 Arg.Any<ValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.ByteSignatureChar ? count : 0)
		        .GetByteArrayElements(arrayRef.Transform<JArrayLocalRef, JByteArrayLocalRef>(),
		                              Arg.Any<ValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.CharSignatureChar ? count : 0)
		        .GetCharArrayElements(arrayRef.Transform<JArrayLocalRef, JCharArrayLocalRef>(),
		                              Arg.Any<ValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.DoubleSignatureChar ? count : 0)
		        .GetDoubleArrayElements(arrayRef.Transform<JArrayLocalRef, JDoubleArrayLocalRef>(),
		                                Arg.Any<ValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.FloatSignatureChar ? count : 0)
		        .GetFloatArrayElements(arrayRef.Transform<JArrayLocalRef, JFloatArrayLocalRef>(),
		                               Arg.Any<ValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.IntSignatureChar ? count : 0)
		        .GetIntArrayElements(arrayRef.Transform<JArrayLocalRef, JIntArrayLocalRef>(),
		                             Arg.Any<ValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.LongSignatureChar ? count : 0)
		        .GetLongArrayElements(arrayRef.Transform<JArrayLocalRef, JLongArrayLocalRef>(),
		                              Arg.Any<ValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.ShortSignatureChar ? count : 0)
		        .GetShortArrayElements(arrayRef.Transform<JArrayLocalRef, JShortArrayLocalRef>(),
		                               Arg.Any<ValPtr<JBoolean>>());
	}
	private static void AssertReceivedReleaseElements(NativeInterfaceProxy proxyEnv, ref JArrayLocalRef arrayRef,
		Byte signature, Int32 count, JReleaseMode releaseMode)
	{
		proxyEnv.Received(signature == CommonNames.BooleanSignatureChar ? count : 0).ReleaseBooleanArrayElements(
			arrayRef.Transform<JArrayLocalRef, JBooleanArrayLocalRef>(), Arg.Any<ReadOnlyValPtr<JBoolean>>(),
			releaseMode);
		proxyEnv.Received(signature == CommonNames.ByteSignatureChar ? count : 0).ReleaseByteArrayElements(
			arrayRef.Transform<JArrayLocalRef, JByteArrayLocalRef>(), Arg.Any<ReadOnlyValPtr<JByte>>(), releaseMode);
		proxyEnv.Received(signature == CommonNames.CharSignatureChar ? count : 0).ReleaseCharArrayElements(
			arrayRef.Transform<JArrayLocalRef, JCharArrayLocalRef>(), Arg.Any<ReadOnlyValPtr<JChar>>(), releaseMode);
		proxyEnv.Received(signature == CommonNames.DoubleSignatureChar ? count : 0).ReleaseDoubleArrayElements(
			arrayRef.Transform<JArrayLocalRef, JDoubleArrayLocalRef>(), Arg.Any<ReadOnlyValPtr<JDouble>>(),
			releaseMode);
		proxyEnv.Received(signature == CommonNames.FloatSignatureChar ? count : 0).ReleaseFloatArrayElements(
			arrayRef.Transform<JArrayLocalRef, JFloatArrayLocalRef>(), Arg.Any<ReadOnlyValPtr<JFloat>>(), releaseMode);
		proxyEnv.Received(signature == CommonNames.IntSignatureChar ? count : 0).ReleaseIntArrayElements(
			arrayRef.Transform<JArrayLocalRef, JIntArrayLocalRef>(), Arg.Any<ReadOnlyValPtr<JInt>>(), releaseMode);
		proxyEnv.Received(signature == CommonNames.LongSignatureChar ? count : 0).ReleaseLongArrayElements(
			arrayRef.Transform<JArrayLocalRef, JLongArrayLocalRef>(), Arg.Any<ReadOnlyValPtr<JLong>>(), releaseMode);
		proxyEnv.Received(signature == CommonNames.ShortSignatureChar ? count : 0).ReleaseShortArrayElements(
			arrayRef.Transform<JArrayLocalRef, JShortArrayLocalRef>(), Arg.Any<ReadOnlyValPtr<JShort>>(), releaseMode);
	}
	private static void AssertReceivedGetArrayRegion(NativeInterfaceProxy proxyEnv, ref JArrayLocalRef arrayRef,
		Byte signature, Int32 count, Int32 items = 1)
	{
		proxyEnv.Received(signature == CommonNames.BooleanSignatureChar ? count : 0).GetBooleanArrayRegion(
			JBooleanArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.ByteSignatureChar ? count : 0).GetByteArrayRegion(
			JByteArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ValPtr<JByte>>());
		proxyEnv.Received(signature == CommonNames.CharSignatureChar ? count : 0).GetCharArrayRegion(
			JCharArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ValPtr<JChar>>());
		proxyEnv.Received(signature == CommonNames.DoubleSignatureChar ? count : 0).GetDoubleArrayRegion(
			JDoubleArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ValPtr<JDouble>>());
		proxyEnv.Received(signature == CommonNames.FloatSignatureChar ? count : 0).GetFloatArrayRegion(
			JFloatArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ValPtr<JFloat>>());
		proxyEnv.Received(signature == CommonNames.IntSignatureChar ? count : 0).GetIntArrayRegion(
			JIntArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ValPtr<JInt>>());
		proxyEnv.Received(signature == CommonNames.LongSignatureChar ? count : 0).GetLongArrayRegion(
			JLongArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ValPtr<JLong>>());
		proxyEnv.Received(signature == CommonNames.ShortSignatureChar ? count : 0).GetShortArrayRegion(
			JShortArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ValPtr<JShort>>());
	}
	private static void AssertReceivedSetArrayRegion(NativeInterfaceProxy proxyEnv, ref JArrayLocalRef arrayRef,
		Byte signature, Int32 count, Int32 items = 1)
	{
		proxyEnv.Received(signature == CommonNames.BooleanSignatureChar ? count : 0).SetBooleanArrayRegion(
			JBooleanArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items,
			Arg.Any<ReadOnlyValPtr<JBoolean>>());
		proxyEnv.Received(signature == CommonNames.ByteSignatureChar ? count : 0).SetByteArrayRegion(
			JByteArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ReadOnlyValPtr<JByte>>());
		proxyEnv.Received(signature == CommonNames.CharSignatureChar ? count : 0).SetCharArrayRegion(
			JCharArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ReadOnlyValPtr<JChar>>());
		proxyEnv.Received(signature == CommonNames.DoubleSignatureChar ? count : 0).SetDoubleArrayRegion(
			JDoubleArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items,
			Arg.Any<ReadOnlyValPtr<JDouble>>());
		proxyEnv.Received(signature == CommonNames.FloatSignatureChar ? count : 0).SetFloatArrayRegion(
			JFloatArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ReadOnlyValPtr<JFloat>>());
		proxyEnv.Received(signature == CommonNames.IntSignatureChar ? count : 0).SetIntArrayRegion(
			JIntArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ReadOnlyValPtr<JInt>>());
		proxyEnv.Received(signature == CommonNames.LongSignatureChar ? count : 0).SetLongArrayRegion(
			JLongArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ReadOnlyValPtr<JLong>>());
		proxyEnv.Received(signature == CommonNames.ShortSignatureChar ? count : 0).SetShortArrayRegion(
			JShortArrayLocalRef.FromReference(in arrayRef), Arg.Any<Int32>(), items, Arg.Any<ReadOnlyValPtr<JShort>>());
	}

#pragma warning disable CA1859
	private static void NestedFailTest<TPrimitive>(NativeInterfaceProxy proxyEnv)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreatePrimitiveArray<TPrimitive>(3);
		JArrayLocalRef arrayRef = PrimitiveArrayMemoryTests.fixture.Create<JArrayLocalRef>();

		using JClassObject arrayClass = JClassObject.GetClass<JArrayObject<TPrimitive>>(env);
		using JArrayObject<TPrimitive> jArray = new(arrayClass, arrayRef);
		using IFixedContext<TPrimitive>.IDisposable fMem = value.AsMemory().GetFixedContext();
		ValPtr<Byte> valPtr = fMem.AsBinaryContext().ValuePointer;

		proxyEnv.GetArrayLength(arrayRef).Returns(value.Length);
		proxyEnv.GetPrimitiveArrayCritical(arrayRef, Arg.Any<ValPtr<JBoolean>>()).Returns(valPtr);
		PrimitiveArrayMemoryTests.ConfigureGetElements(proxyEnv, ref arrayRef, false, valPtr.Pointer);

		// Clears non-existing exception.
		env.PendingException = default;

		JNativeMemory<TPrimitive> seq = jArray.GetElements();
		Assert.Equal(value.Length, seq.Values.Length);
		Assert.Equal(fMem.Pointer, seq.Pointer);

		JNativeMemory<TPrimitive> seqCritical = jArray.GetCriticalElements(JMemoryReferenceKind.Local);
		Assert.Equal(value.Length, seq.Values.Length);
		Assert.Equal(fMem.Pointer, seq.Pointer);

		proxyEnv.ExceptionCheck().Returns(true);
		Assert.Throws<CriticalException>(() => seq.Dispose());

		proxyEnv.ExceptionCheck().Returns(false);
		env.PendingException = default;

		proxyEnv.ExceptionCheck().Returns(true);
		Assert.Throws<CriticalException>(() => seqCritical.Dispose());

		proxyEnv.ExceptionCheck().Returns(false);
		env.PendingException = default;

		seq.Dispose();
		seqCritical.Dispose();
	}
#pragma warning restore CA1859
}