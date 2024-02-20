namespace Rxmxnx.JNetInterface.Tests.Primitives;

public partial class PrimitiveTestBase
{
	private static void FloatingValueTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		Assert.Equal(primitive.Value.GetExponentByteCount(), primitive.GetExponentByteCount());
		Assert.Equal(primitive.Value.GetSignificandByteCount(), primitive.GetSignificandByteCount());
		Assert.Equal(primitive.Value.GetSignificandBitLength(), primitive.GetSignificandBitLength());
		Assert.Equal(primitive.Value.GetExponentShortestBitLength(), primitive.GetExponentShortestBitLength());

		PrimitiveTestBase.ExponentBigEndianTest<TPrimitive, TValue>(primitive);
		PrimitiveTestBase.SignificandBigEndianTest<TPrimitive, TValue>(primitive);
		PrimitiveTestBase.ExponentLittleEndianTest<TPrimitive, TValue>(primitive);
		PrimitiveTestBase.SignificandLittleEndianTest<TPrimitive, TValue>(primitive);

		PrimitiveTestBase.FloatingOperationsTest<TPrimitive, TValue>(primitive);
		PrimitiveTestBase.TrigonometricTest<TPrimitive, TValue>(primitive);
		PrimitiveTestBase.HyperbolicTest<TPrimitive, TValue>(primitive);
	}
	private static void FloatingOperationsTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		TValue vAbs = TValue.Abs(primitive.Value);
		TPrimitive pAbs = TPrimitive.Abs(primitive);

		Assert.Equal(TValue.Sqrt(vAbs), TPrimitive.Sqrt(pAbs).Value);
		Assert.Equal(TValue.BitDecrement(vAbs), TPrimitive.BitDecrement(pAbs).Value);
		Assert.Equal(TValue.BitIncrement(vAbs), TPrimitive.BitIncrement(pAbs).Value);
		if (primitive.Value != TValue.Zero && vAbs != TValue.Epsilon)
			Assert.Equal(TValue.ReciprocalSqrtEstimate(vAbs), TPrimitive.ReciprocalSqrtEstimate(pAbs).Value);
		else
			Assert.Equal(TValue.ReciprocalSqrtEstimate(TValue.One),
			             TPrimitive.ReciprocalSqrtEstimate(TPrimitive.One).Value);
		Assert.Equal(TValue.Log(vAbs), TPrimitive.Log(pAbs).Value);
		Assert.Equal(TValue.LogP1(vAbs), TPrimitive.LogP1(pAbs).Value);
		Assert.Equal(TValue.Log(TValue.One, vAbs), TPrimitive.Log(TPrimitive.One, pAbs).Value);
		Assert.Equal(TValue.Log2P1(vAbs), TPrimitive.Log2P1(pAbs).Value);
		Assert.Equal(TValue.Log10(vAbs), TPrimitive.Log10(pAbs).Value);
		Assert.Equal(TValue.Log10P1(vAbs), TPrimitive.Log10P1(pAbs).Value);
		Assert.Equal(TValue.Cbrt(vAbs), TPrimitive.Cbrt(pAbs).Value);
		Assert.Equal(TValue.ILogB(vAbs), TPrimitive.ILogB(pAbs));
		if (primitive.Value != TValue.Zero && vAbs != TValue.Epsilon)
			Assert.Equal(TValue.ReciprocalEstimate(primitive.Value), TPrimitive.ReciprocalEstimate(primitive).Value);
		else
			Assert.Equal(TValue.ReciprocalEstimate(TValue.NegativeOne),
			             TPrimitive.ReciprocalEstimate(TPrimitive.NegativeOne).Value);
		Assert.Equal(TValue.Truncate(primitive.Value), TPrimitive.Truncate(primitive).Value);
		Assert.Equal(TValue.Ceiling(primitive.Value), TPrimitive.Ceiling(primitive).Value);
		Assert.Equal(TValue.Floor(primitive.Value), TPrimitive.Floor(primitive).Value);
		Assert.Equal(TValue.ScaleB(primitive.Value, 2), TPrimitive.ScaleB(primitive, 2).Value);
		Assert.Equal(TValue.Round(primitive.Value), TPrimitive.Round(primitive).Value);
		Assert.Equal(TValue.Round(primitive.Value, 2), TPrimitive.Round(primitive, 2).Value);
		Assert.Equal(TValue.Round(primitive.Value, MidpointRounding.ToZero),
		             TPrimitive.Round(primitive, MidpointRounding.ToZero).Value);
		Assert.Equal(TValue.Round(primitive.Value, 2, MidpointRounding.ToEven),
		             TPrimitive.Round(primitive, 2, MidpointRounding.ToEven).Value);
		Assert.Equal(TValue.Exp(primitive.Value), TPrimitive.Exp(primitive).Value);
		Assert.Equal(TValue.Exp2(primitive.Value), TPrimitive.Exp2(primitive).Value);
		Assert.Equal(TValue.Exp10(primitive.Value), TPrimitive.Exp10(primitive).Value);
		Assert.Equal(TValue.ExpM1(primitive.Value), TPrimitive.ExpM1(primitive).Value);
		Assert.Equal(TValue.Exp2M1(primitive.Value), TPrimitive.Exp2M1(primitive).Value);
		Assert.Equal(TValue.Exp10M1(primitive.Value), TPrimitive.Exp10M1(primitive).Value);
		if (primitive.Value != TValue.Zero && vAbs != TValue.Epsilon)
			Assert.Equal(TValue.Pow(primitive.Value, TValue.NegativeOne),
			             TPrimitive.Pow(primitive, TPrimitive.NegativeOne).Value);
		else
			Assert.Equal(TValue.Pow(TValue.NegativeOne, TValue.Zero),
			             TPrimitive.Pow(TValue.NegativeOne, TPrimitive.Zero).Value);
		Assert.Equal(TValue.RootN(primitive.Value, 1), TPrimitive.RootN(primitive, 1).Value);
	}
	private static void TrigonometricTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		TValue vCos = TValue.Cos(primitive.Value);
		TValue vSin = TValue.Sin(primitive.Value);
		TValue vTan = TValue.Tan(primitive.Value);
		TPrimitive pCos = TPrimitive.Cos(primitive);
		TPrimitive pSin = TPrimitive.Sin(primitive);
		TPrimitive pTan = TPrimitive.Tan(primitive);

		Assert.Equal(vCos, pCos.Value);
		Assert.Equal(vSin, pSin.Value);
		Assert.Equal(vTan, pTan.Value);
		Assert.Equal(TValue.CosPi(primitive.Value), TPrimitive.CosPi(primitive).Value);
		Assert.Equal(TValue.SinPi(primitive.Value), TPrimitive.SinPi(primitive).Value);
		Assert.Equal(TValue.TanPi(primitive.Value), TPrimitive.TanPi(primitive).Value);

		Assert.Equal(TValue.Acos(vCos), TPrimitive.Acos(pCos).Value);
		Assert.Equal(TValue.AcosPi(vCos), TPrimitive.AcosPi(pCos).Value);
		Assert.Equal(TValue.Asin(vSin), TPrimitive.Asin(pSin).Value);
		Assert.Equal(TValue.AsinPi(vSin), TPrimitive.AsinPi(pSin).Value);
		Assert.Equal(TValue.Atan(vTan), TPrimitive.Atan(pTan).Value);
		Assert.Equal(TValue.AtanPi(vTan), TPrimitive.AtanPi(pTan).Value);
		Assert.Equal(TValue.Atan2(vSin, vCos), TPrimitive.Atan2(pSin, pCos).Value);
		Assert.Equal(TValue.Atan2Pi(vSin, vCos), TPrimitive.Atan2Pi(pSin, pCos).Value);

		Assert.Equal(TValue.Hypot(TValue.Abs(vCos), TValue.Abs(vSin)),
		             TPrimitive.Hypot(TPrimitive.Abs(vCos), TPrimitive.Abs(vSin)).Value);
		Assert.Equal(TValue.SinCos(primitive.Value), TPrimitive.SinCos(primitive));
		Assert.Equal(TValue.SinCosPi(primitive.Value), TPrimitive.SinCosPi(primitive));
		Assert.Equal(TValue.FusedMultiplyAdd(vSin, vCos, vTan), TPrimitive.FusedMultiplyAdd(pSin, pCos, pTan).Value);
	}
	private static void HyperbolicTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		TValue vCosh = TValue.Cosh(primitive.Value);
		TValue vSinh = TValue.Sinh(primitive.Value);
		TValue vTanh = TValue.Tanh(primitive.Value);
		TPrimitive pCosh = TPrimitive.Cosh(primitive);
		TPrimitive pSinh = TPrimitive.Sinh(primitive);
		TPrimitive pTanh = TPrimitive.Tanh(primitive);

		Assert.Equal(vCosh, pCosh.Value);
		Assert.Equal(vSinh, pSinh.Value);
		Assert.Equal(vTanh, pTanh.Value);

		Assert.Equal(TValue.Acosh(vCosh), TPrimitive.Acosh(pCosh).Value);
		Assert.Equal(TValue.Asinh(vSinh), TPrimitive.Asinh(pSinh).Value);
		Assert.Equal(TValue.Atanh(vTanh), TPrimitive.Atanh(pTanh).Value);
		Assert.Equal(TValue.FusedMultiplyAdd(vSinh, vCosh, vTanh),
		             TPrimitive.FusedMultiplyAdd(pSinh, pCosh, pTanh).Value);
	}
	private static void ExponentBigEndianTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		Span<Byte> span0 = stackalloc Byte[primitive.GetExponentByteCount()];
		Span<Byte> span1 = stackalloc Byte[span0.Length];
		Assert.Equal(primitive.Value.TryWriteExponentBigEndian(span0, out Int32 bytesW0),
		             primitive.TryWriteExponentBigEndian(span1, out Int32 bytesW1));
		Assert.Equal(bytesW0, bytesW1);
		Assert.True(span0.SequenceEqual(span1));
	}
	private static void ExponentLittleEndianTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		Span<Byte> span0 = stackalloc Byte[primitive.GetExponentByteCount()];
		Span<Byte> span1 = stackalloc Byte[span0.Length];
		Assert.Equal(primitive.Value.TryWriteExponentLittleEndian(span0, out Int32 bytesW0),
		             primitive.TryWriteExponentLittleEndian(span1, out Int32 bytesW1));
		Assert.Equal(bytesW0, bytesW1);
		Assert.True(span0.SequenceEqual(span1));
	}
	private static void SignificandBigEndianTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		Span<Byte> span0 = stackalloc Byte[primitive.GetSignificandByteCount()];
		Span<Byte> span1 = stackalloc Byte[span0.Length];
		Assert.Equal(primitive.Value.TryWriteSignificandBigEndian(span0, out Int32 bytesW0),
		             primitive.TryWriteSignificandBigEndian(span1, out Int32 bytesW1));
		Assert.Equal(bytesW0, bytesW1);
		Assert.True(span0.SequenceEqual(span1));
	}
	private static void SignificandLittleEndianTest<TPrimitive, TValue>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveEquatable,
		IFloatingPoint<TPrimitive>, IBinaryNumber<TPrimitive>, IFloatingPointIeee754<TPrimitive>
		where TValue : unmanaged, IConvertible, IMinMaxValue<TValue>, IFloatingPoint<TValue>, IBinaryNumber<TValue>,
		IFloatingPointIeee754<TValue>
	{
		Span<Byte> span0 = stackalloc Byte[primitive.GetSignificandByteCount()];
		Span<Byte> span1 = stackalloc Byte[span0.Length];
		Assert.Equal(primitive.Value.TryWriteSignificandLittleEndian(span0, out Int32 bytesW0),
		             primitive.TryWriteSignificandLittleEndian(span1, out Int32 bytesW1));
		Assert.Equal(bytesW0, bytesW1);
		Assert.True(span0.SequenceEqual(span1));
	}
}