namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class PrimitiveTypeTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void BooleanTest() => PrimitiveTypeTests.Test<JBoolean, Boolean>();
	[Fact]
	internal void ByteTest() => PrimitiveTypeTests.Test<JByte, SByte>();
	[Fact]
	internal void CharTest() => PrimitiveTypeTests.Test<JChar, Char>();
	[Fact]
	internal void DoubleTest() => PrimitiveTypeTests.Test<JDouble, Double>();
	[Fact]
	internal void FloatTest() => PrimitiveTypeTests.Test<JFloat, Single>();
	[Fact]
	internal void IntTest() => PrimitiveTypeTests.Test<JInt, Int32>();
	[Fact]
	internal void LongTest() => PrimitiveTypeTests.Test<JLong, Int64>();
	[Fact]
	internal void ShortTest() => PrimitiveTypeTests.Test<JShort, Int16>();

	private static void Test<TPrimitive, TValue>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		TPrimitive value = PrimitiveTypeTests.fixture.Create<TValue>();

		PrimitiveTypeTests.CopyToTest<TPrimitive, TValue>(value);
		PrimitiveTypeTests.EqualityTest<TPrimitive, TValue>(value, value);
		foreach (TValue newValue in PrimitiveTypeTests.fixture.CreateMany<TValue>(10))
			PrimitiveTypeTests.EqualityTest<TPrimitive, TValue>(value, newValue);
		PrimitiveTypeTests.MetadataTest<TPrimitive, TValue>(value);
	}
	private static void CopyToTest<TPrimitive, TValue>(TPrimitive value)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		Span<TPrimitive> values = stackalloc TPrimitive[10];
		Span<JValue> jValues = stackalloc JValue[3];
		Int32 idx = 5 * NativeUtilities.SizeOf<TPrimitive>();

		value.CopyTo(values.AsBytes());
		value.CopyTo(values.AsBytes(), ref idx);
		value.CopyTo(jValues, 1);

		Assert.True(value.AsBytes().SequenceEqual(values[0].AsBytes()));
		Assert.True(value.AsBytes().SequenceEqual(values[5].AsBytes()));
		Assert.Equal(idx, 6 * NativeUtilities.SizeOf<TPrimitive>());
		Assert.Equal(jValues[1], JValue.Create(value));
	}
	private static void MetadataTest<TPrimitive, TValue>(TPrimitive value)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		Assert.Equal(JTypeKind.Primitive, TPrimitive.Kind);
		Assert.Equal(metadata, TPrimitive.Metadata);
		Assert.Null(TPrimitive.FamilyType);
		Assert.Equal(JTypeModifier.Final, metadata.Modifier);
		Assert.Equal(NativeUtilities.SizeOf<TPrimitive>(), metadata.SizeOf);
		Assert.Equal(typeof(TValue), metadata.UnderlineType);
		Assert.Equal(typeof(TPrimitive), metadata.Type);
		Assert.Equal(1, metadata.Signature.Length);
		Assert.Equal(value, metadata.CreateInstance(value.AsBytes()));
	}
	private static void EqualityTest<TPrimitive, TValue>(TPrimitive value, TPrimitive value2)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		Object valObj = value;
		Boolean equals = value.AsBytes().SequenceEqual(value2.AsBytes());
		Assert.Equal(equals, value.Equals(value2));
		Assert.Equal(equals, value == value2);
		Assert.Equal(!equals, value != value2);
		Assert.Equal(equals, value.Equals(IWrapper.Create(value2)));
		Assert.Equal(equals, value.Equals(IWrapper.Create(value2.Value)));
		Assert.Equal(equals, value.Equals(IWrapper.Create(value2.Value)));
		Assert.Equal(equals, value.Equals((JObject)value2));

		Assert.Equal(equals, valObj.Equals(value2));
		Assert.Equal(equals, valObj.Equals(value2.Value));
		Assert.Equal(equals, valObj.Equals(IWrapper.Create(value2)));
		Assert.Equal(equals, valObj.Equals(IWrapper.Create(value2.Value)));
		Assert.Equal(equals, valObj.Equals(IWrapper.Create(value2.Value)));
		Assert.Equal(equals, valObj.Equals((JObject)value2));

		PrimitiveProxy<TPrimitive> wrapperPrimitive = Substitute.For<PrimitiveProxy<TPrimitive>>();
		PrimitiveProxy<TValue> wrapperValue = Substitute.For<PrimitiveProxy<TValue>>();
		PrimitiveProxy<Byte> wrapperByte = Substitute.For<PrimitiveProxy<Byte>>();

		wrapperPrimitive.Value.Returns(value2);
		wrapperValue.Value.Returns(value2.Value);
		wrapperByte.Value.Returns(value2.AsBytes()[0]);

		Assert.Equal(equals, value.Equals(wrapperPrimitive));
		Assert.Equal(equals, value.Equals(wrapperValue));
		Assert.Equal(equals, valObj.Equals(wrapperPrimitive));
		Assert.Equal(equals, valObj.Equals(wrapperValue));
		Assert.False(value.Equals(new Object()));
		if (typeof(TValue) != typeof(Boolean)) return;
		Assert.Equal(equals, value.Equals(wrapperByte));
		Assert.False(valObj.Equals((JObject)NativeUtilities.Transform<TPrimitive, JByte>(in value)));
	}
}