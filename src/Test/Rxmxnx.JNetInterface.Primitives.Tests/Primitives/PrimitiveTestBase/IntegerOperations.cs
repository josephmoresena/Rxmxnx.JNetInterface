namespace Rxmxnx.JNetInterface.Tests.Primitives;

public partial class PrimitiveTestBase
{
	private static void IntegerValueTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		Assert.Equal(primitive.Value.GetByteCount(), primitive.GetByteCount());
		Assert.Equal(primitive.Value.GetShortestBitLength(), primitive.GetShortestBitLength());

		foreach (CultureInfo culture in PrimitiveTestBase.GetCultures(10))
		{
			PrimitiveTestBase.TryFormatCharTest<TPrimitive, TValue>(primitive, culture);
			PrimitiveTestBase.TryFormatByteTest<TPrimitive, TValue>(primitive, culture);
			PrimitiveTestBase.IntegerBigEndianTest<TPrimitive, TValue>(primitive);
			PrimitiveTestBase.IntegerLittleEndianTest<TPrimitive, TValue>(primitive);
		}
	}
	private static void TryFormatCharTest<TPrimitive, TValue>(TPrimitive primitive, CultureInfo culture)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		Span<Char> span0 = stackalloc Char[primitive.Value.ToString(culture).Length];
		Span<Char> span1 = stackalloc Char[span0.Length];
		Assert.Equal(primitive.Value.TryFormat(span0, out Int32 charsW0, default, culture),
		             primitive.TryFormat(span1, out Int32 charsW1, default, culture));
		Assert.Equal(charsW0, charsW1);
		Assert.True(span0.SequenceEqual(span1));
	}
	private static void TryFormatByteTest<TPrimitive, TValue>(TPrimitive primitive, CultureInfo culture)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		Span<Byte> span0 = stackalloc Byte[((CString)primitive.Value.ToString(culture)).Length];
		Span<Byte> span1 = stackalloc Byte[span0.Length];
		Assert.Equal(primitive.Value.TryFormat(span0, out Int32 bytesW0, default, culture),
		             primitive.TryFormat(span1, out Int32 bytesW1, default, culture));
		Assert.Equal(bytesW0, bytesW1);
		Assert.True(span0.SequenceEqual(span1));
	}
	private static void IntegerBigEndianTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		Span<Byte> bytes0 = stackalloc Byte[primitive.GetByteCount()];
		Span<Byte> bytes1 = stackalloc Byte[bytes0.Length];
		Assert.Equal(primitive.Value.TryWriteBigEndian(bytes0, out Int32 bytesW0),
		             primitive.TryWriteBigEndian(bytes1, out Int32 bytesW1));
		Assert.Equal(bytesW0, bytesW1);
		Assert.True(bytes0.SequenceEqual(bytes1));
		PrimitiveTestBase.IntegerReadBigEndianTest<TPrimitive, TValue>(bytes0, bytes1);
	}
	private static void IntegerReadBigEndianTest<TPrimitive, TValue>(Span<Byte> bytes0, Span<Byte> bytes1)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		Boolean unsigned = typeof(TValue) == typeof(Char);
		Assert.Equal(TValue.TryReadBigEndian(bytes0, unsigned, out TValue value),
		             TPrimitive.TryReadBigEndian(bytes1, unsigned, out TPrimitive primitive));
		Assert.Equal(value, primitive.Value);
	}
	private static void IntegerLittleEndianTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		Span<Byte> bytes0 = stackalloc Byte[primitive.GetByteCount()];
		Span<Byte> bytes1 = stackalloc Byte[bytes0.Length];
		Assert.Equal(primitive.Value.TryWriteLittleEndian(bytes0, out Int32 bytesW0),
		             primitive.TryWriteLittleEndian(bytes1, out Int32 bytesW1));
		Assert.Equal(bytesW0, bytesW1);
		Assert.True(bytes0.SequenceEqual(bytes1));
		PrimitiveTestBase.IntegerReadLittleEndianTest<TPrimitive, TValue>(bytes0, bytes1);
	}
	private static void IntegerReadLittleEndianTest<TPrimitive, TValue>(Span<Byte> bytes0, Span<Byte> bytes1)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		Boolean unsigned = typeof(TValue) == typeof(Char);
		Assert.Equal(TValue.TryReadLittleEndian(bytes0, unsigned, out TValue value),
		             TPrimitive.TryReadLittleEndian(bytes1, unsigned, out TPrimitive primitive));
		Assert.Equal(value, primitive.Value);
	}
	private static void ShiftTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IBinaryInteger<TPrimitive>, IShiftOperators<TPrimitive, Int32, TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IBinaryInteger<TValue>,
		IShiftOperators<TValue, Int32, TValue>
	{
		for (Int32 i = 0; i < 8 * NativeUtilities.SizeOf<TPrimitive>(); i++)
			Assert.Equal(primitive.Value << i, (primitive << i).Value);
		for (Int32 i = 0; i < 8 * NativeUtilities.SizeOf<TPrimitive>(); i++)
			Assert.Equal(primitive.Value >> i, (primitive >> i).Value);
		for (Int32 i = 0; i < 8 * NativeUtilities.SizeOf<TPrimitive>(); i++)
			Assert.Equal(primitive.Value >>> i, (primitive >>> i).Value);
	}
}