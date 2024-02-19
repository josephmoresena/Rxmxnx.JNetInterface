namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class PrimitiveObjectTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void BooleanTest() => PrimitiveObjectTests.Test<Boolean>();
	[Fact]
	internal void ByteTest() => PrimitiveObjectTests.Test<Byte>();
	[Fact]
	internal void CharTest() => PrimitiveObjectTests.Test<Char>();
	[Fact]
	internal void Int16Test() => PrimitiveObjectTests.Test<Int16>();
	[Fact]
	internal void Int32Test() => PrimitiveObjectTests.Test<Int32>();
	[Fact]
	internal void Int64Test() => PrimitiveObjectTests.Test<Int64>();
	[Fact]
	internal void SingleTest() => PrimitiveObjectTests.Test<Single>();
	[Fact]
	internal void DoubleTest() => PrimitiveObjectTests.Test<Double>();

	private static void Test<T>() where T : unmanaged, IEquatable<T>, IComparable, IConvertible
	{
		T value = PrimitiveObjectTests.fixture.Create<T>();
		T value2 = PrimitiveObjectTests.fixture.Create<T>();
		CString className = (CString)PrimitiveObjectTests.fixture.Create<String>();
		CString signature = (CString)PrimitiveObjectTests.fixture.Create<String>();
		PrimitiveObjectProxy<T> obj = new(className, signature, value);
		PrimitiveObjectProxy<T> obj2 = new(className, signature, value);
		PrimitiveObjectProxy<T> obj3 = new(className, signature, value2);
		ObjectProxy objProxy = Substitute.For<ObjectProxy>();
		Span<T> values = stackalloc T[3];
		Span<JValue> jValues = stackalloc JValue[3];
		Span<Byte> bytes = values.AsBytes();
		Int32 idx = 2 * NativeUtilities.SizeOf<T>();

		(obj as IObject).CopyTo(bytes);
		(obj as IObject).CopyTo(bytes, ref idx);
		(obj as IObject).CopyTo(jValues, 1);

		Assert.Equal(value.GetHashCode(), obj.GetHashCode());
		Assert.Equal(value.ToString(), obj.ToString());
		Assert.Equal(className, (obj as IObject).ObjectClassName);
		Assert.Equal(signature, (obj as IObject).ObjectSignature);
		Assert.Equal(value, values[0]);
		Assert.Equal(obj.ToByte(), bytes[0]);
		Assert.Equal(jValues[1], JValue.Create(value));

		Assert.Equal(value, values[2]);
		Assert.Equal(idx, bytes.Length);
		Assert.True(obj.Equals(value));
		Assert.True(obj.Equals(obj2));
		Assert.False(obj.Equals(obj3));

		Assert.True(obj.Equals((Object)value));
		Assert.True(obj.Equals(obj2));
		Assert.False(obj.Equals(obj3));
		Assert.False(obj.Equals(objProxy));

		Assert.True(obj.Equals((Object)obj2));
		Assert.False(obj.Equals((Object)obj3));
		Assert.False(obj.Equals(objProxy));

		Assert.True(obj == obj2);
		Assert.False(obj == obj3);
		Assert.False(obj != obj2);
		Assert.True(obj != obj3);

		Assert.True(obj == (JObject)obj2);
		Assert.False(obj == (JObject)obj3);
		Assert.False(obj != (JObject)obj2);
		Assert.True(obj != (JObject)obj3);

		Assert.Equal(0, (obj as IComparable).CompareTo(value));
		Assert.Equal(0, (obj as IComparable).CompareTo(obj2));
		Assert.Equal(value.CompareTo(value2), (obj as IComparable).CompareTo(value2));
		Assert.Equal(value.CompareTo(value2), (obj as IComparable).CompareTo(obj3));
		Assert.Throws<ArgumentException>(() => (obj as IComparable).CompareTo(String.Empty));

		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.GetTypeCode());
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToBoolean(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToByte(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToChar(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToDateTime(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToDecimal(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToDouble(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToInt16(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToInt32(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToInt64(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToSByte(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToSingle(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToString(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToType(typeof(Int64), default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToUInt16(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToUInt32(default));
		PrimitiveObjectTests.ConvertibleTest(value, obj, c => c.ToUInt64(default));
	}
	private static void ConvertibleTest<TResult>(IConvertible value, IConvertible obj, Func<IConvertible, TResult> func)
	{
		try
		{
			TResult result = func(value);
			Assert.Equal(result, func(obj));
		}
		catch (Exception e)
		{
			Exception e2 = Assert.Throws(e.GetType(), () => func(obj));
			Assert.Equal(e.Message, e2.Message);
		}
	}
}