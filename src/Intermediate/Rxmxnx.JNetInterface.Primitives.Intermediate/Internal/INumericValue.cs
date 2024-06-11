namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface INumericValue<TPrimitive, TValue> : IBinaryNumber<TPrimitive>
	where TPrimitive : unmanaged, IPrimitiveNumericType<TPrimitive, TValue>, IPrimitiveValue<TValue>,
	IBinaryNumber<TPrimitive>, IPrimitiveEquatable
	where TValue : unmanaged, IBinaryNumber<TValue>, IMinMaxValue<TValue>, IConvertible
{
	static Int32 INumberBase<TPrimitive>.Radix => 2;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<TPrimitive>.IsCanonical(TPrimitive value) => true;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<TPrimitive>.IsComplexNumber(TPrimitive value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<TPrimitive>.IsImaginaryNumber(TPrimitive value) => false;

	static Boolean INumberBase<TPrimitive>.TryConvertFromChecked<TOther>(TOther value, out TPrimitive result)
	{
		Unsafe.SkipInit(out result);
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertFromChecked(value, out Unsafe.As<TPrimitive, TValue>(ref result));
		result = Unsafe.As<TOther, TPrimitive>(ref value);
		return true;
	}
	static Boolean INumberBase<TPrimitive>.TryConvertFromSaturating<TOther>(TOther value, out TPrimitive result)
	{
		Unsafe.SkipInit(out result);
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertFromSaturating(value, out Unsafe.As<TPrimitive, TValue>(ref result));
		result = Unsafe.As<TOther, TPrimitive>(ref value);
		return true;
	}
	static Boolean INumberBase<TPrimitive>.TryConvertFromTruncating<TOther>(TOther value, out TPrimitive result)
	{
		Unsafe.SkipInit(out result);
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertFromTruncating(value, out Unsafe.As<TPrimitive, TValue>(ref result));
		result = Unsafe.As<TOther, TPrimitive>(ref value);
		return true;
	}
	static Boolean INumberBase<TPrimitive>.TryConvertToChecked<TOther>(TPrimitive value, out TOther result)
	{
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertToChecked(value.Value, out result!);
		Unsafe.SkipInit(out result);
		Unsafe.As<TOther, TValue>(ref result) = value.Value;
		return true;
	}
	static Boolean INumberBase<TPrimitive>.TryConvertToSaturating<TOther>(TPrimitive value, out TOther result)
	{
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertToSaturating(value.Value, out result!);
		Unsafe.SkipInit(out result);
		Unsafe.As<TOther, TValue>(ref result) = value.Value;
		return true;
	}
	static Boolean INumberBase<TPrimitive>.TryConvertToTruncating<TOther>(TPrimitive value, out TOther result)
	{
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertToTruncating(value.Value, out result!);
		Unsafe.SkipInit(out result);
		Unsafe.As<TOther, TValue>(ref result) = value.Value;
		return true;
	}
}