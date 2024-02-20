namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface INumericValue<TValue> : IPrimitiveValue<TValue>
	where TValue : unmanaged, IComparable, IConvertible, ISpanFormattable, IComparable<TValue>, IEquatable<TValue>,
	IBinaryNumber<TValue>, IMinMaxValue<TValue>;

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
	/// <inheritdoc cref="INumberBase{TSelf}.One"/>
	private static readonly TPrimitive one = INumericValue<TPrimitive, TValue>.GetOne();
	/// <inheritdoc cref="INumberBase{TSelf}.Zero"/>
	private static readonly TPrimitive zero = INumericValue<TPrimitive, TValue>.GetZero();
	/// <inheritdoc cref="IBinaryNumber{TSelf}.AllBitsSet"/>
	private static readonly TPrimitive allBitsSet = INumericValue<TPrimitive, TValue>.GetAllBitsSet();
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	private static readonly TPrimitive additiveIdentity = INumericValue<TPrimitive, TValue>.GetAdditiveIdentity();
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	private static readonly TPrimitive multiplicativeIdentity =
		INumericValue<TPrimitive, TValue>.GetMultiplicativeIdentity();

	static TPrimitive INumberBase<TPrimitive>.One => INumericValue<TPrimitive, TValue>.one;
	static Int32 INumberBase<TPrimitive>.Radix => 2;
	static TPrimitive INumberBase<TPrimitive>.Zero => INumericValue<TPrimitive, TValue>.zero;
	static TPrimitive IBinaryNumber<TPrimitive>.AllBitsSet => INumericValue<TPrimitive, TValue>.allBitsSet;
	static TPrimitive IAdditiveIdentity<TPrimitive, TPrimitive>.AdditiveIdentity
		=> INumericValue<TPrimitive, TValue>.additiveIdentity;
	static TPrimitive IMultiplicativeIdentity<TPrimitive, TPrimitive>.MultiplicativeIdentity
		=> INumericValue<TPrimitive, TValue>.multiplicativeIdentity;

	/// <inheritdoc cref="INumberBase{TSelf}.One"/>
	private static TPrimitive GetOne()
	{
		TValue result = TValue.One;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="INumberBase{TSelf}.Zero"/>
	private static TPrimitive GetZero()
	{
		TValue result = TValue.Zero;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
	private static TPrimitive GetAdditiveIdentity()
	{
		TValue result = TValue.AdditiveIdentity;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
	private static TPrimitive GetMultiplicativeIdentity()
	{
		TValue result = TValue.MultiplicativeIdentity;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}
	/// <inheritdoc cref="IBinaryNumber{TSelf}.AllBitsSet"/>
	private static TPrimitive GetAllBitsSet()
	{
		TValue result = TValue.AllBitsSet;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}

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