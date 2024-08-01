namespace Rxmxnx.JNetInterface.Tests.Nio;

[ExcludeFromCodeCoverage]
public sealed class PrimitiveBufferTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly MethodInfo getMetadata =
		typeof(IClassType).GetMethod(nameof(IClassType.GetMetadata), BindingFlags.Public | BindingFlags.Static)!;

	[Fact]
	internal void ByteTest() => PrimitiveBufferTests.PrimitiveBufferTest<JByteBufferObject, JByte>();
	[Fact]
	internal void CharTest() => PrimitiveBufferTests.PrimitiveBufferTest<JCharBufferObject, JChar>();
	[Fact]
	internal void DoubleTest() => PrimitiveBufferTests.PrimitiveBufferTest<JDoubleBufferObject, JDouble>();
	[Fact]
	internal void FloatTest() => PrimitiveBufferTests.PrimitiveBufferTest<JFloatBufferObject, JFloat>();
	[Fact]
	internal void IntTest() => PrimitiveBufferTests.PrimitiveBufferTest<JIntBufferObject, JInt>();
	[Fact]
	internal void LongTest() => PrimitiveBufferTests.PrimitiveBufferTest<JLongBufferObject, JLong>();
	[Fact]
	internal void ShortTest() => PrimitiveBufferTests.PrimitiveBufferTest<JShortBufferObject, JShort>();
	[Fact]
	internal void MappedByteTest() => PrimitiveBufferTests.PrimitiveBufferTest<JMappedByteBufferObject, JByte>();
	[Fact]
	internal void DirectByteTest()
	{
		PrimitiveBufferTests.DirectCreationTest();
		PrimitiveBufferTests.DirectCreationTest(false);
		PrimitiveBufferTests.DirectCreationTest(true);
		PrimitiveBufferTests.MetadataTest<JDirectByteBufferObject, JByte>(true);
		PrimitiveBufferTests.MetadataTest<JDirectByteBufferObject, JByte>(false);
	}
	[Fact]
	internal void CreateDirectBufferTest()
	{
		PrimitiveBufferTests.CreateDirectBufferPrimitive<SByte>();
		PrimitiveBufferTests.CreateDirectBufferPrimitive<Int16>();
		PrimitiveBufferTests.CreateDirectBufferPrimitive<Int32>();
		PrimitiveBufferTests.CreateDirectBufferPrimitive<Int64>();
		PrimitiveBufferTests.CreateDirectBufferPrimitive<Guid>();
	}
	[Fact]
	internal void WithDirectBufferTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		Int32 capacity = Random.Shared.Next(0, 10);
		Object obj = new();
		Object result = new();
		Action<JByteBufferObject> action0 = Substitute.For<Action<JByteBufferObject>>();
		Action<JByteBufferObject, Object> action1 = Substitute.For<Action<JByteBufferObject, Object>>();
		Func<JByteBufferObject, Object> func0 = Substitute.For<Func<JByteBufferObject, Object>>();
		Func<JByteBufferObject, Object, Object> func1 = Substitute.For<Func<JByteBufferObject, Object, Object>>();

		env.NioFeature.WithDirectByteBuffer(capacity, func0).Returns(result);
		env.NioFeature.WithDirectByteBuffer(capacity, obj, func1).Returns(result);

		JByteBufferObject.WithDirectBuffer(env, capacity, action0);
		JByteBufferObject.WithDirectBuffer(env, capacity, obj, action1);
		Assert.Equal(result, JByteBufferObject.WithDirectBuffer(env, capacity, func0));
		Assert.Equal(result, JByteBufferObject.WithDirectBuffer(env, capacity, obj, func1));

		env.NioFeature.Received(1).WithDirectByteBuffer(capacity, action0);
		env.NioFeature.Received(1).WithDirectByteBuffer(capacity, obj, action1);
		env.NioFeature.Received(1).WithDirectByteBuffer(capacity, func0);
		env.NioFeature.Received(1).WithDirectByteBuffer(capacity, obj, func1);
	}

	private static void CreateDirectBufferPrimitive<TMemory>() where TMemory : unmanaged
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JDirectByteBufferObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = PrimitiveBufferTests.fixture.Create<JObjectLocalRef>();
		Int32 count = Random.Shared.Next(0, 10);
		TMemory[] values = PrimitiveBufferTests.fixture.CreateMany<TMemory>(count).ToArray();
		using IFixedMemory.IDisposable mem = values.AsMemory().GetFixedContext();
		using JClassObject jClass = new(env);
		using JClassObject jBufferClass = new(jClass, typeMetadata);
		using JDirectByteBufferObject jBuffer = new(jBufferClass, mem, localRef);
		IntPtr address = mem.Pointer;

		env.NioFeature.NewDirectByteBuffer(Arg.Is<IFixedMemory.IDisposable>(m => m.Pointer == address))
		   .Returns(jBuffer);
		Assert.Equal(jBuffer, JByteBufferObject.CreateDirectBuffer(env, values.AsMemory()));
	}
	private static void PrimitiveBufferTest<TBuffer, TPrimitive>()
		where TBuffer : JBufferObject<TPrimitive>, IClassType<TBuffer>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>
	{
		PrimitiveBufferTests.CreationTest<TBuffer, TPrimitive>(true);
		PrimitiveBufferTests.CreationTest<TBuffer, TPrimitive>(false);
		PrimitiveBufferTests.MetadataTest<TBuffer, TPrimitive>(true);
		PrimitiveBufferTests.MetadataTest<TBuffer, TPrimitive>(false);
		PrimitiveBufferTests.WrapperCreationTest<TBuffer, TPrimitive>(true);
		PrimitiveBufferTests.WrapperCreationTest<TBuffer, TPrimitive>(false);
	}
	private static void CreationTest<TBuffer, TPrimitive>(Boolean useMetadata)
		where TBuffer : JBufferObject<TPrimitive>, IClassType<TBuffer>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TBuffer>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = PrimitiveBufferTests.fixture.Create<JObjectLocalRef>();
		Int64 capacity = PrimitiveBufferTests.fixture.Create<Int32>();
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
		Assert.Null(jBuffer.AsDirect());
	}
	private static void WrapperCreationTest<TBuffer, TPrimitive>(Boolean useMetadata)
		where TBuffer : JBufferObject<TPrimitive>, IClassType<TBuffer>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TBuffer>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = PrimitiveBufferTests.fixture.Create<JObjectLocalRef>();
		TPrimitive[] values = PrimitiveBufferTests.CreatePrimitiveArray<TPrimitive>(Random.Shared.Next(0, 10));
		using IFixedMemory.IDisposable mem = values.AsMemory().GetFixedContext();
		Int64 capacity = mem.Bytes.Length;
		IntPtr address = mem.Pointer;
		using JClassObject jClass = new(env);
		using JClassObject jBufferClass = new(jClass, typeMetadata);
		using TBuffer jBuffer = Assert.IsType<TBuffer>(typeMetadata.CreateInstance(jBufferClass, localRef, true));

		env.FunctionSet.IsDirectBuffer(jBuffer).Returns(true);
		env.NioFeature.GetDirectAddress(jBuffer).Returns(address);
		env.NioFeature.GetDirectCapacity(jBuffer).Returns(capacity);

		ILocalObject.ProcessMetadata(jBuffer,
		                             useMetadata ?
			                             new BufferObjectMetadata(new(jBufferClass))
			                             {
				                             Capacity = capacity, IsDirect = true, Address = address,
			                             } :
			                             new ObjectMetadata(jBufferClass));

		BufferObjectMetadata objectMetadata = Assert.IsType<BufferObjectMetadata>(ILocalObject.CreateMetadata(jBuffer));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.True(objectMetadata.IsDirect);
		Assert.Equal(address, objectMetadata.Address);
		Assert.Equal(capacity, objectMetadata.Capacity);
		Assert.Equal(objectMetadata, new(objectMetadata));

		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JBufferObject>()));
		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JComparableObject>().Object));

		env.FunctionSet.Received(useMetadata ? 0 : 1).IsDirectBuffer(jBuffer);
		env.FunctionSet.Received(0).BufferCapacity(jBuffer);
		env.NioFeature.Received(useMetadata ? 0 : 1).GetDirectCapacity(jBuffer);
		env.NioFeature.Received(useMetadata ? 0 : 1).GetDirectAddress(jBuffer);

		IDirectBufferObject<TPrimitive>? directBuffer = jBuffer.AsDirect();

		Assert.NotNull(directBuffer);
		Assert.Equal(address, directBuffer.Address);
		Assert.Equal(capacity, directBuffer.Capacity);
		Assert.Equal(jBuffer, directBuffer.InternalBuffer);

		IFixedContext<TPrimitive> ctx = directBuffer.GetFixedContext();
		Assert.True(Unsafe.AreSame(ref MemoryMarshal.GetReference(ctx.Values),
		                           ref MemoryMarshal.GetReference(values.AsSpan())));
	}
#pragma warning disable CA1859
	private static void DirectCreationTest(Boolean? useMetadata = default)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JDirectByteBufferObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JObjectLocalRef localRef = PrimitiveBufferTests.fixture.Create<JObjectLocalRef>();
		Byte[] bytes = PrimitiveBufferTests.fixture.CreateMany<Byte>(Random.Shared.Next(0, 10)).ToArray();
		using IFixedMemory.IDisposable mem = bytes.AsMemory().GetFixedContext();
		Int64 capacity = mem.Bytes.Length;
		IntPtr address = mem.Pointer;
		using JClassObject jClass = new(env);
		using JClassObject jBufferClass = new(jClass, typeMetadata);
		using JDirectByteBufferObject jBuffer = useMetadata.HasValue ?
			Assert.IsType<JDirectByteBufferObject>(typeMetadata.CreateInstance(jBufferClass, localRef, true)) :
			new(jBufferClass, mem, localRef);

		env.ClassFeature.GetObjectClass(jBuffer).Returns(jBufferClass);
		env.FunctionSet.IsDirectBuffer(jBuffer).Returns(true);
		env.NioFeature.GetDirectAddress(jBuffer).Returns(address);
		env.NioFeature.GetDirectCapacity(jBuffer).Returns(capacity);

		if (useMetadata.HasValue)
			ILocalObject.ProcessMetadata(jBuffer,
			                             useMetadata.Value ?
				                             new BufferObjectMetadata(new(jBufferClass))
				                             {
					                             Capacity = capacity, IsDirect = true, Address = address,
				                             } :
				                             new ObjectMetadata(jBufferClass));

		BufferObjectMetadata objectMetadata = Assert.IsType<BufferObjectMetadata>(ILocalObject.CreateMetadata(jBuffer));

		Assert.Equal(typeMetadata.ClassName, objectMetadata.ObjectClassName);
		Assert.Equal(typeMetadata.Signature, objectMetadata.ObjectSignature);
		Assert.True(objectMetadata.IsDirect);
		Assert.Equal(address, objectMetadata.Address);
		Assert.Equal(capacity, objectMetadata.Capacity);
		Assert.Equal(objectMetadata, new(objectMetadata));

		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JLocalObject>()));
		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JBufferObject>()));
		Assert.True(Object.ReferenceEquals(jBuffer, jBuffer.CastTo<JComparableObject>().Object));

		env.ClassFeature.Received(!useMetadata.HasValue ? 1 : 0).GetObjectClass(jBuffer);
		env.FunctionSet.Received(!useMetadata.HasValue || useMetadata.Value ? 0 : 1).IsDirectBuffer(jBuffer);
		env.FunctionSet.Received(0).BufferCapacity(jBuffer);
		env.NioFeature.Received(!useMetadata.HasValue || useMetadata.Value ? 0 : 1).GetDirectCapacity(jBuffer);
		env.NioFeature.Received(!useMetadata.HasValue || useMetadata.Value ? 0 : 1).GetDirectAddress(jBuffer);

		JComparableObject jComparableObject = jBuffer.CastTo<JComparableObject>();
		JDirectBufferObject jDirectBufferObject = jBuffer.CastTo<JDirectBufferObject>();
		Assert.Equal(jBuffer.Id, jComparableObject.Id);
		Assert.Equal(jBuffer.Id, jDirectBufferObject.Id);
		Assert.Equal(jBuffer, jComparableObject.Object);
		Assert.Equal(jBuffer, jDirectBufferObject.Object);

		IDirectBufferObject<JByte> directBuffer = jBuffer;

		Assert.Equal(address, directBuffer.Address);
		Assert.Equal(capacity, directBuffer.Capacity);
		Assert.Equal(jBuffer, directBuffer.InternalBuffer);
		Assert.True(JBufferObject.IsDirectBuffer(jBuffer));

		Assert.Equal(jBuffer, jBuffer.AsDirect());

		IFixedMemory fixedMemory = directBuffer.AsFixedMemory();
		Assert.True(Unsafe.AreSame(ref MemoryMarshal.GetReference(fixedMemory.Bytes),
		                           ref MemoryMarshal.GetReference(bytes.AsSpan())));

		(typeMetadata.ParseInstance(jBuffer) as IDisposable)!.Dispose();
	}
#pragma warning restore CA1859
	private static void MetadataTest<TBuffer, TPrimitive>(Boolean disposeParse)
		where TBuffer : JBufferObject<TPrimitive>, IClassType<TBuffer>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TBuffer>();
		String? textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = PrimitiveBufferTests.fixture.Create<JClassLocalRef>();
		JObjectLocalRef localRef = PrimitiveBufferTests.fixture.Create<JObjectLocalRef>();
		JGlobalRef globalRef = PrimitiveBufferTests.fixture.Create<JGlobalRef>();
		JTypeModifier modifier = typeof(TBuffer).BaseType == typeof(JBufferObject<TPrimitive>) ||
			typeof(TBuffer).BaseType == typeof(JByteBufferObject) ?
				JTypeModifier.Abstract :
				JTypeModifier.Extensible;
		JClassTypeMetadata baseMetadata = typeof(TBuffer).BaseType == typeof(JBufferObject<TPrimitive>) ?
			IClassType.GetMetadata<JBufferObject>() :
			(JClassTypeMetadata)PrimitiveBufferTests.getMetadata.MakeGenericMethod(typeof(TBuffer).BaseType!)
			                                        .Invoke(default, [])!;
		using JClassObject jClassClass = new(env);
		using JClassObject jBufferClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, localRef, jBufferClass);
		using JGlobal jGlobal = new(vm, new(jBufferClass), globalRef);

		Assert.StartsWith("{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.ToPrintableHash()} }}", textValue);

		Assert.Equal(modifier, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<TBuffer>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(TBuffer), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(baseMetadata, typeMetadata.BaseMetadata);
		Assert.IsType<JFunctionDefinition<TBuffer>.Parameterless>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFunctionDefinition<TBuffer>>(
			typeMetadata.CreateFunctionDefinition("functionName"u8, [JArgumentMetadata.Get<JStringObject>(),]));
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

		Assert.True(typeMetadata.IsInstance(jBuffer0));
		Assert.True(typeMetadata.IsInstance(jBuffer1));
		Assert.True(typeMetadata.IsInstance(jBuffer2));
	}
	private static TPrimitive[] CreatePrimitiveArray<TPrimitive>(Int32 length)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		TPrimitive[] result = new TPrimitive[length];
		JPrimitiveTypeMetadata primitiveTypeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		switch (primitiveTypeMetadata.Signature[0])
		{
			case CommonNames.BooleanSignatureChar:
				PrimitiveBufferTests.fixture.CreateMany<Boolean>(length).ToArray().AsSpan().AsBytes()
				                    .CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.ByteSignatureChar:
				PrimitiveBufferTests.fixture.CreateMany<SByte>(length).ToArray().AsSpan().AsBytes()
				                    .CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.CharSignatureChar:
				PrimitiveBufferTests.fixture.CreateMany<Char>(length).ToArray().AsSpan().AsBytes()
				                    .CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.DoubleSignatureChar:
				PrimitiveBufferTests.fixture.CreateMany<Double>(length).ToArray().AsSpan().AsBytes()
				                    .CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.FloatSignatureChar:
				PrimitiveBufferTests.fixture.CreateMany<Single>(length).ToArray().AsSpan().AsBytes()
				                    .CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.IntSignatureChar:
				PrimitiveBufferTests.fixture.CreateMany<Int32>(length).ToArray().AsSpan().AsBytes()
				                    .CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.LongSignatureChar:
				PrimitiveBufferTests.fixture.CreateMany<Int64>(length).ToArray().AsSpan().AsBytes()
				                    .CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.ShortSignatureChar:
				PrimitiveBufferTests.fixture.CreateMany<Int16>(length).ToArray().AsSpan().AsBytes()
				                    .CopyTo(result.AsSpan().AsBytes());
				break;
		}
		return result;
	}
}