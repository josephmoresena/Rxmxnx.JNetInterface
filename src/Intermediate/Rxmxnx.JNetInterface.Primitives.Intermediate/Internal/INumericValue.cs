namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive number.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive number.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface INumericValue<TPrimitive> : IBinaryNumber<TPrimitive>
	where TPrimitive : unmanaged, INumericValue<TPrimitive>, IPrimitiveNumericType<TPrimitive>
{
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromChecked{TOther}(TOther, out TPrimitive)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean TryConvertFromChecked<TValue, TOther>(TOther value, ConvertFromHelper<TValue> helper)
		where TValue : unmanaged, IBinaryNumber<TValue> where TOther : INumberBase<TOther>
		=> value switch
		{
			JByte jByte => helper.TryConvertFromChecked(jByte.Value),
			JChar jChar => helper.TryConvertFromChecked(jChar.Value),
			JDouble jDouble => helper.TryConvertFromChecked(jDouble.Value),
			JFloat jFloat => helper.TryConvertFromChecked(jFloat.Value),
			JInt jInt => helper.TryConvertFromChecked(jInt.Value),
			JLong jLong => helper.TryConvertFromChecked(jLong.Value),
			JShort jShort => helper.TryConvertFromChecked(jShort.Value),
			_ => helper.TryConvertFromChecked(value),
		};
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromSaturating{TOther}(TOther, out TPrimitive)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean TryConvertFromSaturating<TValue, TOther>(TOther value, ConvertFromHelper<TValue> helper)
		where TValue : unmanaged, IBinaryNumber<TValue> where TOther : INumberBase<TOther>
		=> value switch
		{
			JByte jByte => helper.TryConvertFromSaturating(jByte.Value),
			JChar jChar => helper.TryConvertFromSaturating(jChar.Value),
			JDouble jDouble => helper.TryConvertFromSaturating(jDouble.Value),
			JFloat jFloat => helper.TryConvertFromSaturating(jFloat.Value),
			JInt jInt => helper.TryConvertFromSaturating(jInt.Value),
			JLong jLong => helper.TryConvertFromSaturating(jLong.Value),
			JShort jShort => helper.TryConvertFromSaturating(jShort.Value),
			_ => helper.TryConvertFromSaturating(value),
		};
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromTruncating{TOther}(TOther, out TPrimitive)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean TryConvertFromTruncating<TValue, TOther>(TOther value, ConvertFromHelper<TValue> helper)
		where TValue : unmanaged, IBinaryNumber<TValue> where TOther : INumberBase<TOther>
		=> value switch
		{
			JByte jByte => helper.TryConvertFromTruncating(jByte.Value),
			JChar jChar => helper.TryConvertFromTruncating(jChar.Value),
			JDouble jDouble => helper.TryConvertFromTruncating(jDouble.Value),
			JFloat jFloat => helper.TryConvertFromTruncating(jFloat.Value),
			JInt jInt => helper.TryConvertFromTruncating(jInt.Value),
			JLong jLong => helper.TryConvertFromTruncating(jLong.Value),
			JShort jShort => helper.TryConvertFromTruncating(jShort.Value),
			_ => helper.TryConvertFromTruncating(value),
		};

	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean TryConvertToChecked<TValue, TOther>(ConvertToHelper<TValue, TOther> helper)
		where TValue : unmanaged, IBinaryNumber<TValue> where TOther : INumberBase<TOther>
	{
		Type typeofOther = typeof(TOther);
		if (typeofOther == typeof(JByte))
			return helper.TryConvertToChecked<SByte>(static v =>
			{
				JByte value = v;
				return Unsafe.As<JByte, TOther>(ref value);
			});
		if (typeofOther == typeof(JChar))
			return helper.TryConvertToChecked<Char>(static v =>
			{
				JChar value = v;
				return Unsafe.As<JChar, TOther>(ref value);
			});
		if (typeofOther == typeof(JDouble))
			return helper.TryConvertToChecked<Double>(static v =>
			{
				JDouble value = v;
				return Unsafe.As<JDouble, TOther>(ref value);
			});
		if (typeofOther == typeof(JFloat))
			return helper.TryConvertToChecked<Single>(static v =>
			{
				JFloat value = v;
				return Unsafe.As<JFloat, TOther>(ref value);
			});
		if (typeofOther == typeof(JInt))
			return helper.TryConvertToChecked<Int32>(static v =>
			{
				JInt value = v;
				return Unsafe.As<JInt, TOther>(ref value);
			});
		if (typeofOther == typeof(JLong))
			return helper.TryConvertToChecked<Int64>(static v =>
			{
				JLong value = v;
				return Unsafe.As<JLong, TOther>(ref value);
			});
		if (typeofOther == typeof(JShort))
			return helper.TryConvertToChecked<Int16>(static v =>
			{
				JShort value = v;
				return Unsafe.As<JShort, TOther>(ref value);
			});
		return helper.TryConvertToChecked();
	}
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToSaturating{TOther}(TPrimitive, out TOther)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean TryConvertToSaturating<TValue, TOther>(ConvertToHelper<TValue, TOther> helper)
		where TValue : unmanaged, IBinaryNumber<TValue> where TOther : INumberBase<TOther>
	{
		Type typeofOther = typeof(TOther);
		if (typeofOther == typeof(JByte))
			return helper.TryConvertToSaturating<SByte>(static v =>
			{
				JByte value = v;
				return Unsafe.As<JByte, TOther>(ref value);
			});
		if (typeofOther == typeof(JChar))
			return helper.TryConvertToSaturating<Char>(static v =>
			{
				JChar value = v;
				return Unsafe.As<JChar, TOther>(ref value);
			});
		if (typeofOther == typeof(JDouble))
			return helper.TryConvertToSaturating<Double>(static v =>
			{
				JDouble value = v;
				return Unsafe.As<JDouble, TOther>(ref value);
			});
		if (typeofOther == typeof(JFloat))
			return helper.TryConvertToSaturating<Single>(static v =>
			{
				JFloat value = v;
				return Unsafe.As<JFloat, TOther>(ref value);
			});
		if (typeofOther == typeof(JInt))
			return helper.TryConvertToSaturating<Int32>(static v =>
			{
				JInt value = v;
				return Unsafe.As<JInt, TOther>(ref value);
			});
		if (typeofOther == typeof(JLong))
			return helper.TryConvertToSaturating<Int64>(static v =>
			{
				JLong value = v;
				return Unsafe.As<JLong, TOther>(ref value);
			});
		if (typeofOther == typeof(JShort))
			return helper.TryConvertToSaturating<Int16>(static v =>
			{
				JShort value = v;
				return Unsafe.As<JShort, TOther>(ref value);
			});
		return helper.TryConvertToSaturating();
	}
	/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToTruncating{TOther}(TPrimitive, out TOther)"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean TryConvertToTruncating<TValue, TOther>(ConvertToHelper<TValue, TOther> helper)
		where TValue : unmanaged, IBinaryNumber<TValue> where TOther : INumberBase<TOther>
	{
		Type typeofOther = typeof(TOther);
		if (typeofOther == typeof(JByte))
			return helper.TryConvertToTruncating<SByte>(static v =>
			{
				JByte value = v;
				return Unsafe.As<JByte, TOther>(ref value);
			});
		if (typeofOther == typeof(JChar))
			return helper.TryConvertToTruncating<Char>(static v =>
			{
				JChar value = v;
				return Unsafe.As<JChar, TOther>(ref value);
			});
		if (typeofOther == typeof(JDouble))
			return helper.TryConvertToTruncating<Double>(static v =>
			{
				JDouble value = v;
				return Unsafe.As<JDouble, TOther>(ref value);
			});
		if (typeofOther == typeof(JFloat))
			return helper.TryConvertToTruncating<Single>(static v =>
			{
				JFloat value = v;
				return Unsafe.As<JFloat, TOther>(ref value);
			});
		if (typeofOther == typeof(JInt))
			return helper.TryConvertToTruncating<Int32>(static v =>
			{
				JInt value = v;
				return Unsafe.As<JInt, TOther>(ref value);
			});
		if (typeofOther == typeof(JLong))
			return helper.TryConvertToTruncating<Int64>(static v =>
			{
				JLong value = v;
				return Unsafe.As<JLong, TOther>(ref value);
			});
		if (typeofOther == typeof(JShort))
			return helper.TryConvertToTruncating<Int16>(static v =>
			{
				JShort value = v;
				return Unsafe.As<JShort, TOther>(ref value);
			});
		return helper.TryConvertToTruncating();
	}

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static Boolean INumberBase<TPrimitive>.TryConvertFromChecked<TOther>(TOther value, out TPrimitive result)
	{
		if (value is TPrimitive primitiveResult)
		{
			result = primitiveResult;
			return true;
		}
		Unsafe.SkipInit(out result);
		return TPrimitive.Zero.GetTypeCode() switch
		{
			TypeCode.SByte => INumericValue<TPrimitive>.TryConvertFromChecked<SByte, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Char => INumericValue<TPrimitive>.TryConvertFromChecked<Char, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Double => INumericValue<TPrimitive>.TryConvertFromChecked<Double, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Single => INumericValue<TPrimitive>.TryConvertFromChecked<Single, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int32 => INumericValue<TPrimitive>.TryConvertFromChecked<Int32, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int64 => INumericValue<TPrimitive>.TryConvertFromChecked<Int64, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int16 => INumericValue<TPrimitive>.TryConvertFromChecked<Int16, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			_ => false,
		};
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static Boolean INumberBase<TPrimitive>.TryConvertFromSaturating<TOther>(TOther value, out TPrimitive result)
	{
		if (value is TPrimitive primitiveResult)
		{
			result = primitiveResult;
			return true;
		}
		Unsafe.SkipInit(out result);
		return TPrimitive.Zero.GetTypeCode() switch
		{
			TypeCode.SByte => INumericValue<TPrimitive>.TryConvertFromSaturating<SByte, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Char => INumericValue<TPrimitive>.TryConvertFromSaturating<Char, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Double => INumericValue<TPrimitive>.TryConvertFromSaturating<Double, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Single => INumericValue<TPrimitive>.TryConvertFromSaturating<Single, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int32 => INumericValue<TPrimitive>.TryConvertFromSaturating<Int32, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int64 => INumericValue<TPrimitive>.TryConvertFromSaturating<Int64, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int16 => INumericValue<TPrimitive>.TryConvertFromSaturating<Int16, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			_ => false,
		};
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static Boolean INumberBase<TPrimitive>.TryConvertFromTruncating<TOther>(TOther value, out TPrimitive result)
	{
		if (value is TPrimitive primitiveResult)
		{
			result = primitiveResult;
			return true;
		}
		Unsafe.SkipInit(out result);
		return TPrimitive.Zero.GetTypeCode() switch
		{
			TypeCode.SByte => INumericValue<TPrimitive>.TryConvertFromTruncating<SByte, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Char => INumericValue<TPrimitive>.TryConvertFromTruncating<Char, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Double => INumericValue<TPrimitive>.TryConvertFromTruncating<Double, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Single => INumericValue<TPrimitive>.TryConvertFromTruncating<Single, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int32 => INumericValue<TPrimitive>.TryConvertFromTruncating<Int32, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int64 => INumericValue<TPrimitive>.TryConvertFromTruncating<Int64, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			TypeCode.Int16 => INumericValue<TPrimitive>.TryConvertFromTruncating<Int16, TOther>(
				value, new(ref result, static v => (TPrimitive)v)),
			_ => false,
		};
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static Boolean INumberBase<TPrimitive>.TryConvertToChecked<TOther>(TPrimitive value, out TOther result)
	{
		if (value is TOther otherResult)
		{
			result = otherResult;
			return true;
		}
		Unsafe.SkipInit(out result);
		return value switch
		{
			JByte jByte => INumericValue<TPrimitive>.TryConvertToChecked<SByte, TOther>(new(jByte.Value, ref result)),
			JChar jChar => INumericValue<TPrimitive>.TryConvertToChecked<Char, TOther>(new(jChar.Value, ref result)),
			JDouble jDouble => INumericValue<TPrimitive>.TryConvertToChecked<Double, TOther>(
				new(jDouble.Value, ref result)),
			JFloat jSingle => INumericValue<TPrimitive>.TryConvertToChecked<Single, TOther>(
				new(jSingle.Value, ref result)),
			JInt jInt => INumericValue<TPrimitive>.TryConvertToChecked<Int32, TOther>(new(jInt.Value, ref result)),
			JLong jLong => INumericValue<TPrimitive>.TryConvertToChecked<Int64, TOther>(new(jLong.Value, ref result)),
			JShort jShort => INumericValue<TPrimitive>.TryConvertToChecked<Int16, TOther>(
				new(jShort.Value, ref result)),
			_ => false,
		};
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static Boolean INumberBase<TPrimitive>.TryConvertToSaturating<TOther>(TPrimitive value, out TOther result)
	{
		if (value is TOther otherResult)
		{
			result = otherResult;
			return true;
		}
		Unsafe.SkipInit(out result);
		return value switch
		{
			JByte jByte =>
				INumericValue<TPrimitive>.TryConvertToSaturating<SByte, TOther>(new(jByte.Value, ref result)),
			JChar jChar => INumericValue<TPrimitive>.TryConvertToSaturating<Char, TOther>(new(jChar.Value, ref result)),
			JDouble jDouble => INumericValue<TPrimitive>.TryConvertToSaturating<Double, TOther>(
				new(jDouble.Value, ref result)),
			JFloat jSingle => INumericValue<TPrimitive>.TryConvertToSaturating<Single, TOther>(
				new(jSingle.Value, ref result)),
			JInt jInt => INumericValue<TPrimitive>.TryConvertToSaturating<Int32, TOther>(new(jInt.Value, ref result)),
			JLong jLong => INumericValue<TPrimitive>.TryConvertToSaturating<Int64, TOther>(
				new(jLong.Value, ref result)),
			JShort jShort => INumericValue<TPrimitive>.TryConvertToSaturating<Int16, TOther>(
				new(jShort.Value, ref result)),
			_ => false,
		};
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static Boolean INumberBase<TPrimitive>.TryConvertToTruncating<TOther>(TPrimitive value, out TOther result)
	{
		if (value is TOther otherResult)
		{
			result = otherResult;
			return true;
		}
		Unsafe.SkipInit(out result);
		return value switch
		{
			JByte jByte =>
				INumericValue<TPrimitive>.TryConvertToTruncating<SByte, TOther>(new(jByte.Value, ref result)),
			JChar jChar => INumericValue<TPrimitive>.TryConvertToTruncating<Char, TOther>(new(jChar.Value, ref result)),
			JDouble jDouble => INumericValue<TPrimitive>.TryConvertToTruncating<Double, TOther>(
				new(jDouble.Value, ref result)),
			JFloat jSingle => INumericValue<TPrimitive>.TryConvertToTruncating<Single, TOther>(
				new(jSingle.Value, ref result)),
			JInt jInt => INumericValue<TPrimitive>.TryConvertToTruncating<Int32, TOther>(new(jInt.Value, ref result)),
			JLong jLong => INumericValue<TPrimitive>.TryConvertToTruncating<Int64, TOther>(
				new(jLong.Value, ref result)),
			JShort jShort => INumericValue<TPrimitive>.TryConvertToTruncating<Int16, TOther>(
				new(jShort.Value, ref result)),
			_ => false,
		};
	}

	/// <summary>
	/// Helper struct for <c>ConvertFrom</c> methods.
	/// </summary>
	/// <typeparam name="TValue">Type of <see cref="INumberBase{TSelf}"/>.</typeparam>
	/// <param name="refResult">Managed reference to primitive value.</param>
	/// <param name="conv">Delegate for <typeparamref name="TValue"/> value conversion.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private readonly ref struct ConvertFromHelper<TValue>(ref TPrimitive refResult, Func<TValue, TPrimitive> conv)
		where TValue : unmanaged, INumberBase<TValue>
	{
		/// <summary>
		/// Managed reference to result.
		/// </summary>
		private readonly ref TPrimitive _resultRef = ref refResult;
		/// <summary>
		/// Conversion delegate.
		/// </summary>
		private readonly Func<TValue, TPrimitive> _conv = conv;

		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromChecked{TOther}(TOther, out TPrimitive)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertFromChecked<TOther>(TOther value) where TOther : INumberBase<TOther>
		{
			Boolean tryConvertFrom = value is TValue result || TValue.TryConvertFromChecked(value, out result);
			this._resultRef = this._conv(result);
			return tryConvertFrom;
		}
		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromSaturating{TOther}(TOther, out TPrimitive)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertFromSaturating<TOther>(TOther value) where TOther : INumberBase<TOther>
		{
			Boolean tryConvertFrom = value is TValue result || TValue.TryConvertFromSaturating(value, out result);
			this._resultRef = this._conv(result);
			return tryConvertFrom;
		}
		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertFromSaturating{TOther}(TOther, out TPrimitive)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertFromTruncating<TOther>(TOther value) where TOther : INumberBase<TOther>
		{
			Boolean tryConvertFrom = value is TValue result || TValue.TryConvertFromTruncating(value, out result);
			this._resultRef = this._conv(result);
			return tryConvertFrom;
		}
	}

	/// <summary>
	/// Helper struct for <c>ConvertTo</c> methods.
	/// </summary>
	/// <typeparam name="TValue">Type of <see cref="INumberBase{TSelf}"/>.</typeparam>
	/// <typeparam name="TOther">Type of <see cref="INumberBase{TSelf}"/>.</typeparam>
	/// <param name="value"><typeparamref name="TValue"/> value.</param>
	/// <param name="refResult">Managed reference to <typeparamref name="TOther"/> value.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private readonly ref struct ConvertToHelper<TValue, TOther>(TValue value, ref TOther refResult)
		where TValue : unmanaged, INumberBase<TValue> where TOther : INumberBase<TOther>
	{
		/// <summary>
		/// Managed reference to result.
		/// </summary>
		private readonly ref TOther _refResult = ref refResult;
		/// <summary>
		/// Input value.
		/// </summary>
		private readonly TValue _value = value;

		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Boolean TryConvertToDirect()
		{
			if (this._value is not TOther other) return false;
			this._refResult = other;
			return true;
		}

		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertToChecked()
			=> this.TryConvertToDirect() || TValue.TryConvertToChecked(this._value, out this._refResult!);
		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertToChecked<TOther2>(Func<TOther2, TOther> conv)
			where TOther2 : unmanaged, INumberBase<TOther2>
		{
			Boolean tryConvertTo = this._value is TOther2 result || TValue.TryConvertToChecked(this._value, out result);
			this._refResult = conv(result);
			return tryConvertTo;
		}
		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertToSaturating()
			=> this.TryConvertToDirect() || TValue.TryConvertToSaturating(this._value, out this._refResult!);
		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertToSaturating<TOther2>(Func<TOther2, TOther> conv)
			where TOther2 : unmanaged, INumberBase<TOther2>
		{
			Boolean tryConvertTo = this._value is TOther2 result ||
				TValue.TryConvertToSaturating(this._value, out result);
			this._refResult = conv(result);
			return tryConvertTo;
		}
		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertToTruncating()
			=> this.TryConvertToDirect() || TValue.TryConvertToTruncating(this._value, out this._refResult!);
		/// <inheritdoc cref="INumberBase{TPrimitive}.TryConvertToChecked{TOther}(TPrimitive, out TOther)"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Boolean TryConvertToTruncating<TOther2>(Func<TOther2, TOther> conv)
			where TOther2 : unmanaged, INumberBase<TOther2>
		{
			Boolean tryConvertTo = this._value is TOther2 result ||
				TValue.TryConvertToTruncating(this._value, out result);
			this._refResult = conv(result);
			return tryConvertTo;
		}
	}
}