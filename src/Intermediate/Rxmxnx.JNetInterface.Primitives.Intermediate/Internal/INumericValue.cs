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
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromChecked{TOther}(TOther, out TPrimitive)"/>
	protected new static Boolean TryConvertFromChecked<TOther>(TOther value, out TPrimitive result)
		where TOther : INumberBase<TOther>
	{
		Unsafe.SkipInit(out result);
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertFromChecked(value, out Unsafe.As<TPrimitive, TValue>(ref result));
		result = Unsafe.As<TOther, TPrimitive>(ref value);
		return true;
	}
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromSaturating{TOther}(TOther, out TPrimitive)"/>
	protected new static Boolean TryConvertFromSaturating<TOther>(TOther value, out TPrimitive result)
		where TOther : INumberBase<TOther>
	{
		Unsafe.SkipInit(out result);
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertFromSaturating(value, out Unsafe.As<TPrimitive, TValue>(ref result));
		result = Unsafe.As<TOther, TPrimitive>(ref value);
		return true;
	}
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromTruncating{TOther}(TOther, out TPrimitive)"/>
	protected new static Boolean TryConvertFromTruncating<TOther>(TOther value, out TPrimitive result)
		where TOther : INumberBase<TOther>
	{
		Unsafe.SkipInit(out result);
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertFromTruncating(value, out Unsafe.As<TPrimitive, TValue>(ref result));
		result = Unsafe.As<TOther, TPrimitive>(ref value);
		return true;
	}
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
	protected new static Boolean TryConvertToChecked<TOther>(TPrimitive value, out TOther result)
		where TOther : INumberBase<TOther>
	{
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertToChecked(value.Value, out result!);
		Unsafe.SkipInit(out result);
		Unsafe.As<TOther, TValue>(ref result) = value.Value;
		return true;
	}
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToSaturating{TOther}(TPrimitive, out TOther)"/>
	protected new static Boolean TryConvertToSaturating<TOther>(TPrimitive value, out TOther result)
		where TOther : INumberBase<TOther>
	{
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertToSaturating(value.Value, out result!);
		Unsafe.SkipInit(out result);
		Unsafe.As<TOther, TValue>(ref result) = value.Value;
		return true;
	}
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToTruncating{TOther}(TPrimitive, out TOther)"/>
	protected new static Boolean TryConvertToTruncating<TOther>(TPrimitive value, out TOther result)
		where TOther : INumberBase<TOther>
	{
		if (typeof(TOther) != typeof(TPrimitive) && typeof(TOther) != typeof(TValue))
			return TValue.TryConvertToTruncating(value.Value, out result!);
		Unsafe.SkipInit(out result);
		Unsafe.As<TOther, TValue>(ref result) = value.Value;
		return true;
	}
}