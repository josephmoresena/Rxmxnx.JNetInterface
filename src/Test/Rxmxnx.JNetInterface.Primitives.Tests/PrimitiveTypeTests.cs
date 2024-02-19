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
		{
			PrimitiveTypeTests.EqualityTest<TPrimitive, TValue>(value, newValue);
			PrimitiveTypeTests.ComparableTest<TPrimitive, TValue>(value, newValue);
		}
		PrimitiveTypeTests.MetadataTest<TPrimitive, TValue>(value);
		PrimitiveTypeTests.ObjectTest<TPrimitive, TValue>(value);
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
		Assert.Equal(metadata.ArgumentMetadata, JArgumentMetadata.Get<TPrimitive>());

		Assert.Throws<NotImplementedException>(() => PrimitiveTypeImpl.GetNativeType<PrimitiveTypeImpl>());
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
		Assert.Throws<ArgumentException>(() => value.CompareTo(PrimitiveTypeTests.fixture.Create<String>()));

		PrimitiveProxy<TPrimitive> wrapperPrimitive = Substitute.For<PrimitiveProxy<TPrimitive>>();
		PrimitiveProxy<TValue> wrapperValue = Substitute.For<PrimitiveProxy<TValue>>();

		wrapperPrimitive.Value.Returns(value2);
		wrapperValue.Value.Returns(value2.Value);

		Assert.Equal(equals, value.Equals(wrapperPrimitive));
		Assert.Equal(equals, value.Equals(wrapperValue));
		Assert.Equal(equals, valObj.Equals(wrapperPrimitive));
		Assert.Equal(equals, valObj.Equals(wrapperValue));
		Assert.False(value.Equals(new Object()));

		PrimitiveTypeTests.EqualityEquatable<TPrimitive, TValue>(value);
		PrimitiveTypeTests.EqualityEquatable<TPrimitive, TValue>(value2);
		Assert.False(value.Equals(new PrimitiveTypeImpl()));
		if (typeof(TValue) == typeof(Boolean))
			PrimitiveTypeTests.BooleanEqualityTest<TPrimitive, TValue>(value, value2);
		else
			PrimitiveTypeTests.NumericEqualityTest<TPrimitive, TValue>(value, value2);
	}
	private static void EqualityEquatable<TPrimitive, TValue>(TPrimitive value)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		IEquatable<TPrimitive> pEquatable = Substitute.For<IEquatable<TPrimitive>>();
		IEquatable<TValue> vEquatable = Substitute.For<IEquatable<TValue>>();
		IComparable comparable = Substitute.For<IComparable>();

		Boolean pResult = PrimitiveTypeTests.fixture.Create<Boolean>();
		Boolean vResult = PrimitiveTypeTests.fixture.Create<Boolean>();
		Int32 cResult = PrimitiveTypeTests.fixture.Create<Int32>();
		pEquatable.Equals(Arg.Is<TPrimitive>(p => p.Equals(value))).Returns(pResult);
		vEquatable.Equals(Arg.Is<TValue>(v => v.Equals(value.Value))).Returns(vResult);
		comparable.CompareTo(Arg.Is<TValue>(v => v.Equals(value.Value))).Returns(cResult);

		Assert.Equal(pResult, value.Equals(pEquatable));
		Assert.Equal(vResult, value.Equals(vEquatable));
		Assert.Equal(cResult == 0, value.Equals(comparable));
	}
	private static void ComparableTest<TPrimitive, TValue>(TPrimitive value, TPrimitive value2)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		Int32 result = value.Value.CompareTo(value2.Value);
		IComparable<TPrimitive> pComparable = Substitute.For<IComparable<TPrimitive>>();
		IComparable<TValue> vComparable = Substitute.For<IComparable<TValue>>();
		IComparable comparable = Substitute.For<IComparable>();

		Int32 pResult = PrimitiveTypeTests.fixture.Create<Int32>();
		Int32 vResult = PrimitiveTypeTests.fixture.Create<Int32>();
		Int32 cResult = PrimitiveTypeTests.fixture.Create<Int32>();

		pComparable.CompareTo(Arg.Is<TPrimitive>(p => p.Equals(value))).Returns(pResult);
		vComparable.CompareTo(Arg.Is<TValue>(v => v.Equals(value.Value))).Returns(vResult);
		comparable.CompareTo(Arg.Is<TValue>(v => v.Equals(value.Value))).Returns(cResult);

		Assert.Equal(result, value.CompareTo(value2));
		Assert.Equal(result, value.CompareTo(value2.Value));
		Assert.Equal(result, value.CompareTo(IWrapper.Create(value2)));
		Assert.Equal(result, value.CompareTo(IWrapper.Create(value2.Value)));
		Assert.Equal(result, (value as IComparable).CompareTo(value2));
		Assert.Equal(result, (value as IComparable).CompareTo(value2.Value));

		Assert.Equal(-pResult, value.CompareTo(pComparable));
		Assert.Equal(-vResult, value.CompareTo(vComparable));
		Assert.Equal(-cResult, value.CompareTo(comparable));

		Assert.Throws<ArgumentException>(() => value.CompareTo(new Object()));
	}
	private static void BooleanEqualityTest<TPrimitive, TValue>(TPrimitive value, TPrimitive value2)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		Object valObj = value;
		Boolean equals = value.AsBytes().SequenceEqual(value2.AsBytes());
		PrimitiveProxy<Byte> wrapperByte = Substitute.For<PrimitiveProxy<Byte>>();
		PrimitiveProxy proxy = new((JPrimitiveObject)(JObject)value2);

		wrapperByte.Value.Returns(value2.AsBytes()[0]);
		Assert.Equal(equals, value.Equals(proxy));
		Assert.Equal(equals, value.Equals(wrapperByte));
		Assert.False(valObj.Equals((JObject)NativeUtilities.Transform<TPrimitive, JByte>(in value)));
		Assert.False(value.Equals(new PrimitiveTypeImpl()));
	}
	private static void NumericEqualityTest<TPrimitive, TValue>(TPrimitive value, TPrimitive value2)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		JObject bObject = (JBoolean)PrimitiveTypeTests.fixture.Create<Boolean>();
		PrimitiveProxy proxy = new((JPrimitiveObject)(JObject)value2, new(() => "V"u8));
		Assert.False(value.Equals(bObject));
		Assert.False(value.Equals(proxy));
	}
	private static void ObjectTest<TPrimitive, TValue>(TPrimitive value)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IEqualityOperators<TPrimitive, TPrimitive, Boolean>, IPrimitiveEquatable
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
	{
		JPrimitiveObject<TPrimitive> pObj = (JPrimitiveObject<TPrimitive>)(JObject)value;
		TPrimitive value2 = PrimitiveTypeTests.fixture.Create<TValue>();
		JPrimitiveObject<TPrimitive> pObj2 = (JPrimitiveObject<TPrimitive>)(JObject)value2;
		Boolean equals = pObj.Value == pObj2.Value;
		Int32 compare = pObj.Value.CompareTo(pObj2.Value);

		Assert.Equal(PrimitiveTypeImpl.GetNativeType<TPrimitive>(),
		             PrimitiveTypeImpl.GetNativeType<JPrimitiveObject<TPrimitive>>());
		Assert.Equal(typeof(TPrimitive), PrimitiveTypeImpl.GetFamilyType<JPrimitiveObject<TPrimitive>>());

		Assert.Equal(value.ObjectClassName, pObj.ObjectClassName);
		Assert.Equal(value.ObjectSignature, pObj.ObjectSignature);
		Assert.Equal(NativeUtilities.SizeOf<TPrimitive>(), pObj.SizeOf);
		Assert.Equal(value.GetHashCode(), pObj.GetHashCode());
		Assert.Equal(value2.GetHashCode(), pObj2.GetHashCode());
		Assert.Equal(compare, pObj.CompareTo(pObj2));
		Assert.Equal(compare, pObj.CompareTo(pObj2.Value));
		Assert.Equal(compare, pObj.CompareTo(pObj2.Value.Value));

		Assert.True(pObj.Equals((JObject)pObj.Value));
		Assert.True(pObj.Equals(pObj.Value));
		Assert.Equal(equals, pObj.Equals(pObj2));
		Assert.Equal(equals, (pObj as Object).Equals(pObj2));
		Assert.Equal(equals, (pObj as Object).Equals(pObj2.Value));
		Assert.Equal(equals, (pObj as Object).Equals(pObj2.Value.Value));

		Assert.Equal(equals, pObj == pObj2);
		Assert.Equal(!equals, pObj != pObj2);
	}

	private sealed class PrimitiveTypeImpl : IPrimitiveType
	{
		public CString ObjectClassName => throw new NotImplementedException();
		public CString ObjectSignature => throw new NotImplementedException();
		public void CopyTo(Span<Byte> span, ref Int32 offset) { throw new NotImplementedException(); }
		public void CopyTo(Span<JValue> span, Int32 index) { throw new NotImplementedException(); }
		public Int32 CompareTo(Object? obj) => throw new NotImplementedException();
		public TypeCode GetTypeCode() => throw new NotImplementedException();
		public Boolean ToBoolean(IFormatProvider? provider) => throw new NotImplementedException();
		public Byte ToByte(IFormatProvider? provider) => throw new NotImplementedException();
		public Char ToChar(IFormatProvider? provider) => throw new NotImplementedException();
		public DateTime ToDateTime(IFormatProvider? provider) => throw new NotImplementedException();
		public Decimal ToDecimal(IFormatProvider? provider) => throw new NotImplementedException();
		public Double ToDouble(IFormatProvider? provider) => throw new NotImplementedException();
		public Int16 ToInt16(IFormatProvider? provider) => throw new NotImplementedException();
		public Int32 ToInt32(IFormatProvider? provider) => throw new NotImplementedException();
		public Int64 ToInt64(IFormatProvider? provider) => throw new NotImplementedException();
		public SByte ToSByte(IFormatProvider? provider) => throw new NotImplementedException();
		public Single ToSingle(IFormatProvider? provider) => throw new NotImplementedException();
		public String ToString(IFormatProvider? provider) => throw new NotImplementedException();
		public Object ToType(Type conversionType, IFormatProvider? provider) => throw new NotImplementedException();
		public UInt16 ToUInt16(IFormatProvider? provider) => throw new NotImplementedException();
		public UInt32 ToUInt32(IFormatProvider? provider) => throw new NotImplementedException();
		public UInt64 ToUInt64(IFormatProvider? provider) => throw new NotImplementedException();

		public static JNativeType GetNativeType<TPrimitive>() where TPrimitive : IPrimitiveType => TPrimitive.JniType;
		public static Type? GetFamilyType<TPrimitive>() where TPrimitive : IPrimitiveType => TPrimitive.FamilyType;
	}
}