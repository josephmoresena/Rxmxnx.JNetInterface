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
			TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreateMany<TPrimitive>().ToArray();

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
			TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreateMany<TPrimitive>().ToArray();

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

	private static void NestedFailTest<TPrimitive>(NativeInterfaceProxy proxyEnv)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = JEnvironment.GetEnvironment(proxyEnv.Reference);
		TPrimitive[] value = PrimitiveArrayMemoryTests.fixture.CreateMany<TPrimitive>().ToArray();
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
}