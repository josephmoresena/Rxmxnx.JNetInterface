namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
#pragma warning disable CA1859
public sealed class PrimitiveArrayTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	[Fact]
	internal void BooleanTest() => PrimitiveArrayTests.PrimitiveArrayTest<JBoolean>();
	[Fact]
	internal void ByteTest() => PrimitiveArrayTests.PrimitiveArrayTest<JByte>();
	[Fact]
	internal void CharTest() => PrimitiveArrayTests.PrimitiveArrayTest<JChar>();
	[Fact]
	internal void DoubleTest() => PrimitiveArrayTests.PrimitiveArrayTest<JDouble>();
	[Fact]
	internal void FloatTest() => PrimitiveArrayTests.PrimitiveArrayTest<JFloat>();
	[Fact]
	internal void IntTest() => PrimitiveArrayTests.PrimitiveArrayTest<JInt>();
	[Fact]
	internal void LongTest() => PrimitiveArrayTests.PrimitiveArrayTest<JLong>();
	[Fact]
	internal void ShortTest() => PrimitiveArrayTests.PrimitiveArrayTest<JShort>();
	private static void PrimitiveArrayTest<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		JPrimitiveTypeMetadata primitiveTypeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		JArrayTypeMetadata arrayTypeMetadata = IArrayType.GetMetadata<JArrayObject<TPrimitive>>();
		PrimitiveArrayTests.ToStringTest(arrayTypeMetadata, primitiveTypeMetadata);
		PrimitiveArrayTests.ToArrayTest<TPrimitive>();
		PrimitiveArrayTests.SetTest<TPrimitive>();
		PrimitiveArrayTests.MemoryTest<TPrimitive>();
	}
	private static void ToStringTest(JArrayTypeMetadata arrayTypeMetadata, ITypeInformation primitiveTypeMetadata)
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		for (Int32 i = 0; i < 10; i++)
		{
			Int32 arrayLength = Random.Shared.Next(0, Int32.MaxValue);
			JArrayLocalRef arrayRef = PrimitiveArrayTests.fixture.Create<JArrayLocalRef>();
			JClassLocalRef classRef = PrimitiveArrayTests.fixture.Create<JClassLocalRef>();
			Object traceArrayRef = arrayTypeMetadata.Signature[1] switch
			{
				CommonNames.BooleanSignatureChar => JBooleanArrayLocalRef.FromReference(arrayRef),
				CommonNames.ByteSignatureChar => JByteArrayLocalRef.FromReference(arrayRef),
				CommonNames.CharSignatureChar => JCharArrayLocalRef.FromReference(arrayRef),
				CommonNames.DoubleSignatureChar => JDoubleArrayLocalRef.FromReference(arrayRef),
				CommonNames.FloatSignatureChar => JFloatArrayLocalRef.FromReference(arrayRef),
				CommonNames.IntSignatureChar => JIntArrayLocalRef.FromReference(arrayRef),
				CommonNames.LongSignatureChar => JLongArrayLocalRef.FromReference(arrayRef),
				CommonNames.ShortSignatureChar => JShortArrayLocalRef.FromReference(arrayRef),
				_ => JObjectArrayLocalRef.FromReference(arrayRef),
			};

			env.ArrayFeature.GetArrayLength(Arg.Any<JReferenceObject>()).Returns(arrayLength);

			using JClassObject jArrayClass = new(jClassClass, arrayTypeMetadata, classRef);
			using JArrayObject jArray =
				Assert.IsType<JArrayObject>(arrayTypeMetadata.CreateInstance(jArrayClass, arrayRef.Value, true), false);

			Assert.Equal(arrayLength, jArray.Length);
			Assert.Equal(jArrayClass, jArray.Class);
			Assert.Equal(arrayRef, jArray.Reference);
			Assert.Equal(arrayTypeMetadata, jArray.TypeMetadata);
			Assert.Equal(
				$"{primitiveTypeMetadata.ClassName}[{arrayLength}]{String.Concat(Enumerable.Repeat("[]", i))} {arrayRef}",
				jArray.ToString());
			Assert.Equal($"{arrayTypeMetadata.ClassName} {traceArrayRef} length: {jArray.Length}",
			             jArray.ToTraceText());
			Assert.Equal($"[{String.Concat(Enumerable.Repeat("[", i))}{primitiveTypeMetadata.Signature}",
			             arrayTypeMetadata.Signature.ToString());
			arrayTypeMetadata = arrayTypeMetadata.GetArrayMetadata()!;
		}
	}
	private static void ToArrayTest<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JArrayLocalRef arrayRef = PrimitiveArrayTests.fixture.Create<JArrayLocalRef>();
		Int32 length = Random.Shared.Next(0, 11);
		Int32 startIndex = Random.Shared.Next(0, length);
		Int32 count = Random.Shared.Next(0, length - startIndex);

		using JClassObject jClassClass = new(env);
		using JClassObject jArrayClass = new(jClassClass, IArrayType.GetMetadata<JArrayObject<TPrimitive>>());
		using JArrayObject<TPrimitive> jArray = new(jArrayClass, arrayRef, length);

		Assert.Equal(length, jArray.ToArray().Length);
		env.ArrayFeature.Received(1).GetCopy(jArray, Arg.Any<IFixedMemory<TPrimitive>>());
		env.ArrayFeature.ClearReceivedCalls();

		Assert.Equal(length - startIndex, jArray.ToArray(startIndex).Length);
		env.ArrayFeature.Received(1).GetCopy(jArray, Arg.Any<IFixedMemory<TPrimitive>>(), startIndex);
		env.ArrayFeature.ClearReceivedCalls();

		Assert.Equal(count, jArray.ToArray(startIndex, count).Length);
		env.ArrayFeature.Received(1).GetCopy(jArray, Arg.Any<IFixedMemory<TPrimitive>>(), startIndex);
		env.ArrayFeature.ClearReceivedCalls();
	}
	private static void SetTest<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JArrayLocalRef arrayRef = PrimitiveArrayTests.fixture.Create<JArrayLocalRef>();
		Int32 length = Random.Shared.Next(0, 11);
		Int32 startIndex = Random.Shared.Next(0, length);
		TPrimitive[] valuesToSet =
			PrimitiveArrayTests.fixture.CreatePrimitiveArray<TPrimitive>(Random.Shared.Next(0, length - startIndex));

		using JClassObject jClassClass = new(env);
		using JClassObject jArrayClass = new(jClassClass, IArrayType.GetMetadata<JArrayObject<TPrimitive>>());
		using JArrayObject<TPrimitive> jArray = new(jArrayClass, arrayRef, length);

		jArray.Set(valuesToSet, startIndex);
		env.ArrayFeature.Received(1).SetCopy(jArray, Arg.Any<IReadOnlyFixedMemory<TPrimitive>>(), startIndex);
	}
	private static void MemoryTest<TPrimitive>() where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JArrayLocalRef arrayRef = PrimitiveArrayTests.fixture.Create<JArrayLocalRef>();
		Int32 length = Random.Shared.Next(0, 11);
		TPrimitive[] value0 = PrimitiveArrayTests.fixture.CreatePrimitiveArray<TPrimitive>(length);
		TPrimitive[] value1 = PrimitiveArrayTests.fixture.CreatePrimitiveArray<TPrimitive>(length);
		TPrimitive[] value2 = PrimitiveArrayTests.fixture.CreatePrimitiveArray<TPrimitive>(length);

		using JClassObject jClassClass = new(env);
		using JClassObject jArrayClass = new(jClassClass, IArrayType.GetMetadata<JArrayObject<TPrimitive>>());
		using JArrayObject<TPrimitive> jArray = new(jArrayClass, arrayRef, length);
		PrimitiveArrayTests.GetElementsTest(jArray, value0, value1);
		PrimitiveArrayTests.GetCriticalElementsTest(jArray, value0, value2);
	}
	private static void GetElementsTest<TPrimitive>(JArrayObject<TPrimitive> jArray, TPrimitive[] value0,
		TPrimitive[] value1) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		using IFixedContext<TPrimitive>.IDisposable ctx = value0.AsMemory().GetFixedContext();
		IFixedContext<Byte> binarySequenceMemory = ctx.AsBinaryContext();
		INativeMemoryAdapter sequenceMemory = Substitute.For<INativeMemoryAdapter>();
		JReleaseMode releaseMode = PrimitiveArrayTests.fixture.Create<JReleaseMode>();

		sequenceMemory.GetContext(Arg.Any<JPrimitiveMemory<TPrimitive>>()).Returns(binarySequenceMemory);
		jArray.Environment.ArrayFeature.GetSequence(jArray, Arg.Any<JMemoryReferenceKind>()).Returns(sequenceMemory);

		JPrimitiveMemory<TPrimitive> sequence = jArray.GetElements();
		try
		{
			IFixedContext<TPrimitive> elementsContext = sequence;

			Assert.Equal(ctx.Pointer, sequence.Pointer);
			Assert.Equal(sequence.Copy, sequence.Copy);
			Assert.Equal(sequence.Critical, sequenceMemory.Critical);
			Assert.False(sequence.Disposed.Value);
			Assert.True(ctx.Values.SequenceEqual(sequence.Values));
			Assert.Equal(binarySequenceMemory, sequence.GetBinaryContext());

			Assert.Equal(ctx.Pointer, elementsContext.Pointer);
			Assert.True(ctx.Values.SequenceEqual(elementsContext.Values));
			Assert.Equal(ctx.ValuePointer, elementsContext.ValuePointer);
			Assert.Equal((sequence as IFixedContext<TPrimitive>).Transformation<Byte>(out IFixedMemory _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IFixedContext<Byte>).Transformation<Byte>(out IFixedMemory _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal(
				(sequence as IFixedContext<TPrimitive>).Transformation<Byte>(out IReadOnlyFixedMemory _).Pointer,
				binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IFixedContext<Byte>).Transformation<Byte>(out IReadOnlyFixedMemory _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IReadOnlyFixedContext<TPrimitive>).Transformation<Byte>(out _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IReadOnlyFixedContext<Byte>).Transformation<Byte>(out _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IFixedContext<TPrimitive>).AsBinaryContext(), sequence);
			Assert.Equal((sequence as IFixedContext<Byte>).AsBinaryContext(), sequence);
			Assert.Equal((sequence as IReadOnlyFixedContext<TPrimitive>).AsBinaryContext(), sequence);
			Assert.Equal((sequence as IReadOnlyFixedContext<Byte>).AsBinaryContext(), sequence);

			sequence.ReleaseMode = releaseMode;
			Assert.Equal(releaseMode, sequence.ReleaseMode);

			Assert.True((sequence as IReadOnlyFixedContext<TPrimitive>).Values.SequenceEqual(value0));
			Assert.True((sequence as IReadOnlyFixedContext<Byte>).Values.SequenceEqual(value0.AsSpan().AsBytes()));
			Assert.True((sequence as IFixedMemory<Byte>).Values.SequenceEqual(sequence.GetBinaryContext().Values));
			Assert.True((sequence as IFixedMemory).Bytes.SequenceEqual(sequence.GetBinaryContext().Values));

			Int32 i = 0;
			foreach (ref TPrimitive value in sequence.Values)
			{
				value = value1[i];
				i++;
			}

			JNativeMemory<TPrimitive> memory = sequence;
			Assert.Equal(sequence, (JPrimitiveMemory<TPrimitive>)memory);
		}
		finally
		{
			sequence.Dispose();
		}
		sequence.Dispose();
		sequenceMemory.Received(1).GetContext(sequence);
		sequenceMemory.Received(1).Release(releaseMode);

		Assert.Equal(value0, value1);
	}
	private static void GetCriticalElementsTest<TPrimitive>(JArrayObject<TPrimitive> jArray, TPrimitive[] value0,
		TPrimitive[] value1) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		using IFixedContext<TPrimitive>.IDisposable ctx = value0.AsMemory().GetFixedContext();
		IFixedContext<Byte> binarySequenceMemory = ctx.AsBinaryContext();
		INativeMemoryAdapter sequenceMemory = Substitute.For<INativeMemoryAdapter>();
		JReleaseMode releaseMode = PrimitiveArrayTests.fixture.Create<JReleaseMode>();

		sequenceMemory.Critical.Returns(true);
		sequenceMemory.GetContext(Arg.Any<JPrimitiveMemory<TPrimitive>>()).Returns(binarySequenceMemory);
		jArray.Environment.ArrayFeature.GetCriticalSequence(jArray, Arg.Any<JMemoryReferenceKind>())
		      .Returns(sequenceMemory);

		JPrimitiveMemory<TPrimitive> sequence = jArray.GetCriticalElements();
		try
		{
			IFixedContext<TPrimitive> elementsContext = sequence;

			Assert.Equal(ctx.Pointer, sequence.Pointer);
			Assert.Equal(sequence.Copy, sequence.Copy);
			Assert.Equal(sequence.Critical, sequenceMemory.Critical);
			Assert.False(sequence.Disposed.Value);
			Assert.True(ctx.Values.SequenceEqual(sequence.Values));
			Assert.Equal(binarySequenceMemory, sequence.GetBinaryContext());

			Assert.Equal(ctx.Pointer, elementsContext.Pointer);
			Assert.True(ctx.Values.SequenceEqual(elementsContext.Values));
			Assert.Equal(ctx.ValuePointer, elementsContext.ValuePointer);
			Assert.Equal((sequence as IFixedContext<TPrimitive>).Transformation<Byte>(out IFixedMemory _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IFixedContext<Byte>).Transformation<Byte>(out IFixedMemory _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal(
				(sequence as IFixedContext<TPrimitive>).Transformation<Byte>(out IReadOnlyFixedMemory _).Pointer,
				binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IFixedContext<Byte>).Transformation<Byte>(out IReadOnlyFixedMemory _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IReadOnlyFixedContext<TPrimitive>).Transformation<Byte>(out _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IReadOnlyFixedContext<Byte>).Transformation<Byte>(out _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out IFixedMemory _).Pointer);
			Assert.Equal((sequence as IFixedContext<TPrimitive>).AsBinaryContext(), sequence);
			Assert.Equal((sequence as IFixedContext<Byte>).AsBinaryContext(), sequence);
			Assert.Equal((sequence as IReadOnlyFixedContext<TPrimitive>).AsBinaryContext(), sequence);
			Assert.Equal((sequence as IReadOnlyFixedContext<Byte>).AsBinaryContext(), sequence);

			sequence.ReleaseMode = releaseMode;
			Assert.Null(sequence.ReleaseMode);

			Assert.True((sequence as IReadOnlyFixedContext<TPrimitive>).Values.SequenceEqual(value0));
			Assert.True((sequence as IReadOnlyFixedContext<Byte>).Values.SequenceEqual(value0.AsSpan().AsBytes()));
			Assert.True((sequence as IFixedMemory<Byte>).Values.SequenceEqual(sequence.GetBinaryContext().Values));
			Assert.True((sequence as IFixedMemory).Bytes.SequenceEqual(sequence.GetBinaryContext().Values));

			Int32 i = 0;
			foreach (ref TPrimitive value in sequence.Values)
			{
				value = value1[i];
				i++;
			}

			JNativeMemory<TPrimitive> memory = sequence;
			Assert.Equal(sequence, (JPrimitiveMemory<TPrimitive>)memory);
			Assert.Equal(sequence.GetHashCode(), memory.GetHashCode());
		}
		finally
		{
			sequence.Dispose();
		}
		sequence.Dispose();
		sequenceMemory.Received(1).GetContext(sequence);
		sequenceMemory.Received(1).Release();

		Assert.Equal(value0, value1);
	}
}
#pragma warning restore CA1859