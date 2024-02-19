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
		CString className = (CString)PrimitiveObjectTests.fixture.Create<String>();
		CString signature = (CString)PrimitiveObjectTests.fixture.Create<String>();
		T value = PrimitiveObjectTests.fixture.Create<T>();
		PrimitiveObjectProxy<T> obj = new(className, signature, value);

		Assert.Equal(value.GetHashCode(), obj.GetHashCode());
		Assert.Equal(value.ToString(), obj.ToString());
		Assert.Equal(className, (obj as IObject).ObjectClassName);
		Assert.Equal(signature, (obj as IObject).ObjectSignature);

		PrimitiveObjectTests.CopyToTest(obj, value);
		PrimitiveObjectTests.EqualityTest(obj, obj);
		foreach (T newValue in PrimitiveObjectTests.fixture.CreateMany<T>(10))
		{
			PrimitiveObjectProxy<T> newObj = new(obj.ObjectClassName, obj.ObjectSignature, newValue);
			PrimitiveObjectTests.EqualityTest(obj, newObj);
		}
		Assert.Throws<ArgumentException>(() => (obj as IComparable).CompareTo(String.Empty));
		PrimitiveObjectTests.ConvertibleTest(obj);
	}
	private static void CopyToTest<T>(JPrimitiveObject obj, T value)
		where T : unmanaged, IEquatable<T>, IComparable, IConvertible
	{
		Span<T> values = stackalloc T[3];
		Span<JValue> jValues = stackalloc JValue[3];
		Span<Byte> bytes = values.AsBytes();
		Int32 idx = 2 * NativeUtilities.SizeOf<T>();

		(obj as IObject).CopyTo(bytes);
		(obj as IObject).CopyTo(bytes, ref idx);
		(obj as IObject).CopyTo(jValues, 1);

		Assert.Equal(value, values[0]);
		Assert.Equal(obj.ToByte(), bytes[0]);
		Assert.Equal(jValues[1], JValue.Create(value));

		Assert.Equal(value, values[2]);
		Assert.Equal(idx, bytes.Length);
	}
	private static void EqualityTest<T>(JPrimitiveObject.Generic<T> obj, JPrimitiveObject.Generic<T> obj2)
		where T : unmanaged, IEquatable<T>, IComparable, IConvertible
	{
		PrimitiveObjectProxy<T> copy = new(obj.ObjectClassName, obj.ObjectSignature, obj.Value);
		Boolean equals = obj.Value.Equals(obj2.Value);
		Int32 comparable = obj.Value.CompareTo(obj2.Value);
		ObjectProxy objProxy = Substitute.For<ObjectProxy>();
		ReferenceObjectProxy jObject = new(obj.ObjectClassName, obj.ObjectSignature, false);

		Assert.True(obj.Equals(obj.Value));
		Assert.True(obj.Equals(copy));
		Assert.Equal(equals, obj.Equals(obj2));

		Assert.True(obj.Equals((Object)obj.Value));
		Assert.True(obj.Equals(copy));
		Assert.Equal(equals, obj.Equals(obj2));
		Assert.False(obj.Equals(objProxy));
		Assert.False(obj.Equals(jObject));

		Assert.True(obj.Equals((Object)copy));
		Assert.Equal(equals, obj.Equals((Object)obj2));
		Assert.False(obj.Equals((Object)jObject));

		Assert.True(obj == copy);
		Assert.Equal(equals, obj == obj2);
		Assert.False(obj != copy);
		Assert.Equal(!equals, obj != obj2);

		Assert.True(obj == (JObject)copy);
		Assert.Equal(equals, obj == (JObject)obj2);
		Assert.False(obj != (JObject)copy);
		Assert.Equal(!equals, obj != (JObject)obj2);

		Assert.Equal(0, (obj as IComparable).CompareTo(obj.Value));
		Assert.Equal(0, (obj as IComparable).CompareTo(copy));
		Assert.Equal(comparable, (obj as IComparable).CompareTo(obj2.Value));
		Assert.Equal(comparable, (obj as IComparable).CompareTo(obj2));
	}
	private static void ConvertibleTest<T>(IPrimitiveValue<T> obj)
		where T : unmanaged, IEquatable<T>, IComparable, IConvertible
	{
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.GetTypeCode());
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToBoolean(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToByte(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToChar(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToDateTime(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToDecimal(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToDouble(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToInt16(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToInt32(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToInt64(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToSByte(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToSingle(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToString(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToType(typeof(Int64), default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToUInt16(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToUInt32(default));
		PrimitiveObjectTests.ConvertibleTest(obj.Value, obj, c => c.ToUInt64(default));
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