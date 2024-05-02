namespace Rxmxnx.JNetInterface.Tests.Nio;

[ExcludeFromCodeCoverage]
public sealed class PrimitiveBufferObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly MethodInfo getMetadata =
		typeof(IClassType).GetMethod(nameof(IClassType.GetMetadata), BindingFlags.Public | BindingFlags.Static)!;

	[Fact]
	internal void ByteTest() => PrimitiveBufferObjectTests.PrimitiveBufferTest<JByteBufferObject, JByte>();
	[Fact]
	internal void CharTest() => PrimitiveBufferObjectTests.PrimitiveBufferTest<JCharBufferObject, JChar>();
	[Fact]
	internal void DoubleTest() => PrimitiveBufferObjectTests.PrimitiveBufferTest<JDoubleBufferObject, JDouble>();
	[Fact]
	internal void FloatTest() => PrimitiveBufferObjectTests.PrimitiveBufferTest<JFloatBufferObject, JFloat>();
	[Fact]
	internal void IntTest() => PrimitiveBufferObjectTests.PrimitiveBufferTest<JIntBufferObject, JInt>();
	[Fact]
	internal void LongTest() => PrimitiveBufferObjectTests.PrimitiveBufferTest<JLongBufferObject, JLong>();
	[Fact]
	internal void ShortTest() => PrimitiveBufferObjectTests.PrimitiveBufferTest<JShortBufferObject, JShort>();
	[Fact]
	internal void MappedByteTest() => PrimitiveBufferObjectTests.PrimitiveBufferTest<JMappedByteBufferObject, JByte>();

	private static void PrimitiveBufferTest<TBuffer, TPrimitive>()
		where TBuffer : JBufferObject<TPrimitive>, IClassType<TBuffer>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>
	{
		PrimitiveBufferObjectTests.CreationTest<TBuffer, TPrimitive>(true);
		PrimitiveBufferObjectTests.CreationTest<TBuffer, TPrimitive>(false);
		PrimitiveBufferObjectTests.MetadataTest<TBuffer, TPrimitive>(true);
		PrimitiveBufferObjectTests.MetadataTest<TBuffer, TPrimitive>(false);
	}
	private static void CreationTest<TBuffer, TPrimitive>(Boolean useMetadata)
		where TBuffer : JBufferObject<TPrimitive>, IClassType<TBuffer>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TBuffer>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = PrimitiveBufferObjectTests.fixture.Create<JObjectLocalRef>();
		Int64 capacity = PrimitiveBufferObjectTests.fixture.Create<Int32>();
		using JClassObject jClass = new(env);
		using JClassObject jBufferClass = new(jClass, typeMetadata);
		using TBuffer jBuffer = Assert.IsType<TBuffer>(typeMetadata.CreateInstance(jBufferClass, localRef, true));

		env.FunctionSet.IsDirectBuffer(jBuffer).Returns(false);
		env.FunctionSet.BufferCapacity(jBuffer).Returns(capacity);

		ILocalObject.ProcessMetadata(jBuffer,
		                             useMetadata ?
			                             new BufferObjectMetadata(new(jBufferClass))
			                             {
				                             Capacity = capacity, IsDirect = false,
			                             } :
			                             new ObjectMetadata(jBufferClass));

		BufferObjectMetadata objectMetadata = Assert.IsType<BufferObjectMetadata>(ILocalObject.CreateMetadata(jBuffer));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.False(objectMetadata.IsDirect);
		Assert.Null(objectMetadata.Address);
		Assert.Equal(capacity, objectMetadata.Capacity);
		Assert.Equal(objectMetadata, new(objectMetadata));

		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JBufferObject>()));
		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JComparableObject>().Object));

		env.FunctionSet.Received(useMetadata ? 0 : 1).IsDirectBuffer(jBuffer);
		env.FunctionSet.Received(useMetadata ? 0 : 1).BufferCapacity(jBuffer);
		env.NioFeature.Received(0).GetDirectCapacity(jBuffer);
		env.NioFeature.Received(0).GetDirectAddress(jBuffer);

		JComparableObject jComparableObject = jBuffer.CastTo<JComparableObject>();
		Assert.Equal(jBuffer.Id, jComparableObject.Id);
		Assert.Equal(jBuffer, jComparableObject.Object);

		if (typeof(TPrimitive) != typeof(JChar)) return;
		JAppendableObject jAppendableObject = jBuffer.CastTo<JAppendableObject>();
		JReadableObject jReadableObject = jBuffer.CastTo<JReadableObject>();

		Assert.Equal(jBuffer.Id, jAppendableObject.Id);
		Assert.Equal(jBuffer.Id, jReadableObject.Id);
		Assert.Equal(jBuffer, jAppendableObject.Object);
		Assert.Equal(jBuffer, jReadableObject.Object);
	}
	private static void MetadataTest<TBuffer, TPrimitive>(Boolean disposeParse)
		where TBuffer : JBufferObject<TPrimitive>, IClassType<TBuffer>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TBuffer>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = PrimitiveBufferObjectTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = PrimitiveBufferObjectTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = PrimitiveBufferObjectTests.fixture.Create<JGlobalRef>();
		JTypeModifier modifier = typeof(TBuffer).BaseType == typeof(JBufferObject<TPrimitive>) ||
			typeof(TBuffer).BaseType == typeof(JByteBufferObject) ?
				JTypeModifier.Abstract :
				JTypeModifier.Extensible;
		JClassTypeMetadata baseMetadata = typeof(TBuffer).BaseType == typeof(JBufferObject<TPrimitive>) ?
			IClassType.GetMetadata<JBufferObject>() :
			(JClassTypeMetadata)PrimitiveBufferObjectTests.getMetadata.MakeGenericMethod(typeof(TBuffer).BaseType!)
			                                              .Invoke(default, [])!;
		using JClassObject jClassClass = new(env);
		using JClassObject jBufferClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jBufferClass);
		using JGlobal jGlobal = new(vm, new(jBufferClass, IClassType.GetMetadata<TBuffer>()), globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(modifier, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<TBuffer>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(TBuffer), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(baseMetadata, typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<TBuffer>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<TBuffer>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<TBuffer>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<TBuffer>());
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<TBuffer>().Returns(jBufferClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, localRef, jBufferClass, false);
		env.ClassFeature.GetObjectClass(jLocal).Returns(jBufferClass);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JBufferObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JComparableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using TBuffer jBuffer0 = Assert.IsType<TBuffer>(typeMetadata.CreateInstance(jBufferClass, localRef, true));
		using TBuffer jBuffer1 = Assert.IsType<TBuffer>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using TBuffer jBuffer2 = Assert.IsType<TBuffer>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).GetObjectClass(jLocal);
		env.ClassFeature.Received(0).IsInstanceOf<TBuffer>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
}