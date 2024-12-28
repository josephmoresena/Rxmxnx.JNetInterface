using System;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	private const String NumericPrimitiveOperatorsFormat = @"#nullable enable
// <auto-generated/>
namespace {0};

partial struct {1} : ISpanFormattable, IMinMaxValue<{1}>, IBinaryNumber<{1}>, INumericValue<{1}, {2}>
{{
	/// <inheritdoc cref=""IMinMaxValue{{TSelf}}.MinValue""/>
	public static readonly {1} MinValue = {2}.MinValue;
	/// <inheritdoc cref=""IMinMaxValue{{TSelf}}.MaxValue""/>
	public static readonly {1} MaxValue = {2}.MaxValue;
{3}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.IsZero({1} value) => value._value == 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.IsCanonical({1} value) => true;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.IsComplexNumber({1} value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.IsImaginaryNumber({1} value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.TryConvertFromChecked<TOther>(TOther value, out {1} result) 
		=> INumericValue<{1}, {2}>.TryConvertFromChecked(value, out result);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.TryConvertFromSaturating<TOther>(TOther value, out {1} result) 
		=> INumericValue<{1}, {2}>.TryConvertFromSaturating(value, out result);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.TryConvertFromTruncating<TOther>(TOther value, out {1} result) 
		=> INumericValue<{1}, {2}>.TryConvertFromTruncating(value, out result);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.TryConvertToChecked<TOther>({1} value, out TOther result) 
		=> INumericValue<{1}, {2}>.TryConvertToChecked(value, out result);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.TryConvertToSaturating<TOther>({1} value, out TOther result) 
		=> INumericValue<{1}, {2}>.TryConvertToSaturating(value, out result);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{1}>.TryConvertToTruncating<TOther>({1} value, out TOther result) 
		=> INumericValue<{1}, {2}>.TryConvertToTruncating(value, out result);
{4}

    /// <inheritdoc cref=""IComparisonOperators{{TSelf, TOther, Boolean}}.op_GreaterThan(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator >({1} left, {1} right) => left.CompareTo(right) > 0;
	/// <inheritdoc cref=""IComparisonOperators{{TSelf, TOther, Boolean}}.op_GreaterThanOrEqual(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator >=({1} left, {1} right) => left.CompareTo(right) >= 0;
    /// <inheritdoc cref=""IComparisonOperators{{TSelf, TOther, Boolean}}.op_LessThanOrEqual(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator <=({1} left, {1} right) => left.CompareTo(right) <= 0;
    /// <inheritdoc cref=""IComparisonOperators{{TSelf, TOther, Boolean}}.op_LessThan(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator <({1} left, {1} right) => left.CompareTo(right) < 0;

	/// <inheritdoc cref=""IModulusOperators{{TSelf, TOther, TResult}}.op_Modulus(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {1} operator %({1} left, {1} right) 
		=> IPrimitiveNumericType<{1}, {2}>.Modulus(left._value, right._value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {1} operator --({1} value) 
	{{
		IPrimitiveNumericType<{1}, {2}>.Decrement(ref value);
		return value;
	}}
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {1} operator checked --({1} value) 
	{{
		IPrimitiveNumericType<{1}, {2}>.CheckedDecrement(ref value);
		return value;
	}}
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {1} operator ++({1} value) 
	{{
		IPrimitiveNumericType<{1}, {2}>.Increment(ref value);
		return value;
	}}
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {1} operator checked ++({1} value) 
	{{
		IPrimitiveNumericType<{1}, {2}>.CheckedIncrement(ref value);
		return value;
	}}

	static {1} IMinMaxValue<{1}>.MinValue => {1}.MinValue;
	static {1} IMinMaxValue<{1}>.MaxValue => {1}.MaxValue;
	static Int32 INumberBase<{1}>.Radix => 2;
}}
#nullable restore";

	private const String NumericFormattableFormat = @"
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public String ToString(String? format, IFormatProvider? formatProvider) => this.Value.ToString(format, formatProvider);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Boolean TryFormat(Span<Char> destination, out Int32 charsWritten, ReadOnlySpan<Char> format = default, IFormatProvider? provider = default)
		=> this.Value.TryFormat(destination, out charsWritten, format, provider);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} Abs({0} value) => {1}.Abs(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsEvenInteger({0} value) => {1}.IsEvenInteger(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsOddInteger({0} value) => {1}.IsOddInteger(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsNegative({0} value) => {1}.IsNegative(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsPositive({0} value) => {1}.IsPositive(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} MaxMagnitude({0} x, {0} y) => {1}.MaxMagnitude(x._value, y._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} MinMagnitude({0} x, {0} y) => {1}.MinMagnitude(x._value, y._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} Parse(String s, IFormatProvider? provider)
	    => IPrimitiveNumericType<{0}, {1}>.Parse(s, provider);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} Parse(String s, NumberStyles style, IFormatProvider? provider)
	    => IPrimitiveNumericType<{0}, {1}>.Parse(s, style, provider);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} Parse(ReadOnlySpan<Char> s, IFormatProvider? provider)
	    => IPrimitiveNumericType<{0}, {1}>.Parse(s, provider);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} Parse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider)
	    => IPrimitiveNumericType<{0}, {1}>.Parse(s, style, provider);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider,
		out {0} result)
		=> IPrimitiveNumericType<{0}, {1}>.TryParse(s, provider, out result);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse([NotNullWhen(true)] String? s, NumberStyles style, IFormatProvider? provider,
		out {0} result)
		=> IPrimitiveNumericType<{0}, {1}>.TryParse(s, style, provider, out result);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider, out {0} result)
		=> IPrimitiveNumericType<{0}, {1}>.TryParse(s, provider, out result);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean TryParse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider, out {0} result)
		=> IPrimitiveNumericType<{0}, {1}>.TryParse(s, style, provider, out result);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsPow2({0} value) => IPrimitiveNumericType<{0}, {1}>.IsPow2(value.Value);
	/// <inheritdoc cref=""IBinaryNumber{{TSelf}}.Log2(TSelf)""/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} Log2({0} value) => IPrimitiveNumericType<{0}, {1}>.Log2(value.Value);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator +({0} left, {0} right) 
		=> IPrimitiveNumericType<{0}, {1}>.Addition(left._value, right._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator checked +({0} left, {0} right) 
		=> IPrimitiveNumericType<{0}, {1}>.CheckedAddition(left._value, right._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator /({0} left, {0} right)
		=> IPrimitiveNumericType<{0}, {1}>.Division(left._value, right._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator checked /({0} left, {0} right)
		=> IPrimitiveNumericType<{0}, {1}>.CheckedDivision(left._value, right._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator *({0} left, {0} right)
		=> IPrimitiveNumericType<{0}, {1}>.Multiply(left._value, right._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator checked *({0} left, {0} right)
		=> IPrimitiveNumericType<{0}, {1}>.CheckedMultiply(left._value, right._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator -({0} left, {0} right)
		=> IPrimitiveNumericType<{0}, {1}>.Subtraction(left._value, right._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator checked -({0} left, {0} right)
		=> IPrimitiveNumericType<{0}, {1}>.CheckedSubtraction(left._value, right._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator +({0} value)
		=> IPrimitiveNumericType<{0}, {1}>.UnaryPlus(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator -({0} value)
		=> IPrimitiveNumericType<{0}, {1}>.UnaryNegation(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator checked -({0} value)
		=> IPrimitiveNumericType<{0}, {1}>.CheckedUnaryNegation(value._value);";

	private const String CharFormattable = @"
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	String IFormattable.ToString(String? format, IFormatProvider? formatProvider) 
		=> ((IFormattable)this.Value).ToString(format, formatProvider);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Boolean ISpanFormattable.TryFormat(Span<Char> destination, out Int32 charsWritten, ReadOnlySpan<Char> format, IFormatProvider? provider)
		=> ((ISpanFormattable)this.Value).TryFormat(destination, out charsWritten, format, provider);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean IBinaryNumber<JChar>.IsPow2(JChar value) => IPrimitiveNumericType<JChar, Char>.IsPow2(value.Value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IBinaryNumber<JChar>.Log2(JChar value) => IPrimitiveNumericType<JChar, Char>.Log2(value.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar INumberBase<JChar>.Abs(JChar value) => value;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<JChar>.IsEvenInteger(JChar value) => (value._value & 1) == 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<JChar>.IsOddInteger(JChar value) => (value._value & 1) != 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<JChar>.IsNegative(JChar value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<JChar>.IsPositive(JChar value) => true;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar INumberBase<JChar>.MaxMagnitude(JChar x, JChar y) => (Char)Math.Max(x._value, y._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar INumberBase<JChar>.MinMagnitude(JChar x, JChar y) => (Char)Math.Min(x._value, y._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar INumberBase<JChar>.Parse(String s, NumberStyles style, IFormatProvider? provider)
	    => IPrimitiveNumericType<JChar, Char>.Parse(s, style, provider);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar INumberBase<JChar>.Parse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider)
	    => IPrimitiveNumericType<JChar, Char>.Parse(s, style, provider);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<JChar>.TryParse(String? s, NumberStyles style, IFormatProvider? provider,
		out JChar result)
		=> IPrimitiveNumericType<JChar, Char>.TryParse(s, style, provider, out result);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<JChar>.TryParse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider,
		out JChar result)
		=> IPrimitiveNumericType<JChar, Char>.TryParse(s, style, provider, out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IAdditionOperators<JChar, JChar, JChar>.operator +(JChar left, JChar right) 
		=> IPrimitiveNumericType<JChar, Char>.Addition(left._value, right._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IAdditionOperators<JChar, JChar, JChar>.operator checked +(JChar left, JChar right) 
		=> IPrimitiveNumericType<JChar, Char>.CheckedAddition(left._value, right._value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IDivisionOperators<JChar, JChar, JChar>.operator /(JChar left, JChar right)
		=> IPrimitiveNumericType<JChar, Char>.Division(left._value, right._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IDivisionOperators<JChar, JChar, JChar>.operator checked /(JChar left, JChar right)
		=> IPrimitiveNumericType<JChar, Char>.CheckedDivision(left._value, right._value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IMultiplyOperators<JChar, JChar, JChar>.operator *(JChar left, JChar right)
		=> IPrimitiveNumericType<JChar, Char>.Multiply(left._value, right._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IMultiplyOperators<JChar, JChar, JChar>.operator checked *(JChar left, JChar right)
		=> IPrimitiveNumericType<JChar, Char>.CheckedMultiply(left._value, right._value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar ISubtractionOperators<JChar, JChar, JChar>.operator -(JChar left, JChar right)
		=> IPrimitiveNumericType<JChar, Char>.Subtraction(left._value, right._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar ISubtractionOperators<JChar, JChar, JChar>.operator checked -(JChar left, JChar right)
		=> IPrimitiveNumericType<JChar, Char>.CheckedSubtraction(left._value, right._value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IUnaryPlusOperators<JChar, JChar>.operator +(JChar value)
		=> IPrimitiveNumericType<JChar, Char>.UnaryPlus(value._value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IUnaryNegationOperators<JChar, JChar>.operator -(JChar value)
		=> IPrimitiveNumericType<JChar, Char>.UnaryNegation(value._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IUnaryNegationOperators<JChar, JChar>.operator checked -(JChar value)
		=> IPrimitiveNumericType<JChar, Char>.CheckedUnaryNegation(value._value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IParsable<JChar>.Parse(String s, IFormatProvider? provider)
	    => IPrimitiveNumericType<JChar, Char>.Parse(s, provider);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean IParsable<JChar>.TryParse(String? s, IFormatProvider? provider, out JChar result)
		=> IPrimitiveNumericType<JChar, Char>.TryParse(s, provider, out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar ISpanParsable<JChar>.Parse(ReadOnlySpan<Char> s, IFormatProvider? provider)
	    => IPrimitiveNumericType<JChar, Char>.Parse(s, provider);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean ISpanParsable<JChar>.TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider, out JChar result)
		=> IPrimitiveNumericType<JChar, Char>.TryParse(s, provider, out result);";

	private const String IntegerFormattableFormat = @"
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsFinite({0} value) => true;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsInfinity({0} value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsInteger({0} value) => true;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsNaN({0} value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsNegativeInfinity({0} value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsNormal({0} value) => (value._value != 0);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsPositiveInfinity({0} value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsRealNumber({0} value) => true;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean INumberBase<{0}>.IsSubnormal({0} value) => false;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static {0} INumberBase<{0}>.MaxMagnitudeNumber({0} x, {0} y) 
		=> IPrimitiveNumericType<{0}, {1}>.MaxMagnitudeNumber(x._value, y._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static {0} INumberBase<{0}>.MinMagnitudeNumber({0} x, {0} y) 
		=> IPrimitiveNumericType<{0}, {1}>.MinMagnitudeNumber(x._value, y._value);

	/// <inheritdoc cref=""IBitwiseOperators{{TSelf, TOther, TResult}}.op_BitwiseAnd(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator &({0} left, {0} right) 
		=> IPrimitiveNumericType<{0}, {1}>.BitwiseAnd(left._value, right._value);
	/// <inheritdoc cref=""IBitwiseOperators{{TSelf, TOther, TResult}}.op_BitwiseOr(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator |({0} left, {0} right) 
		=> IPrimitiveNumericType<{0}, {1}>.BitwiseOr(left._value, right._value);
	/// <inheritdoc cref=""IBitwiseOperators{{TSelf, TOther, TResult}}.op_ExclusiveOr(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator ^({0} left, {0} right) 
		=> IPrimitiveNumericType<{0}, {1}>.ExclusiveOr(left._value, right._value);
	/// <inheritdoc cref=""IBitwiseOperators{{TSelf, TOther, TResult}}.op_OnesComplement(TSelf)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator ~({0} value) => IPrimitiveNumericType<{0}, {1}>.OnesComplement(value._value);";

	private const String FloatingFormattableFormat = @"
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsFinite({0} value) => {1}.IsFinite(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsInfinity({0} value) => {1}.IsInfinity(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsInteger({0} value) => {1}.IsInteger(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsNaN({0} value) => {1}.IsNaN(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsNegativeInfinity({0} value) => {1}.IsNegativeInfinity(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsNormal({0} value) => {1}.IsNormal(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsPositiveInfinity({0} value) => {1}.IsPositiveInfinity(value.Value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsRealNumber({0} value) => {1}.IsRealNumber(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean IsSubnormal({0} value) => {1}.IsSubnormal(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} MaxMagnitudeNumber({0} x, {0} y) => {1}.MaxMagnitudeNumber(x._value, y._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} MinMagnitudeNumber({0} x, {0} y) => {1}.MinMagnitudeNumber(x._value, y._value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static {0} IBitwiseOperators<{0}, {0}, {0}>.operator &({0} left, {0} right) 
		=> IPrimitiveNumericType<{0}, {1}>.BitwiseAnd(left._value, right._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static {0} IBitwiseOperators<{0}, {0}, {0}>.operator |({0} left, {0} right) 
		=> IPrimitiveNumericType<{0}, {1}>.BitwiseOr(left._value, right._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static {0} IBitwiseOperators<{0}, {0}, {0}>.operator ^({0} left, {0} right) 
		=> IPrimitiveNumericType<{0}, {1}>.ExclusiveOr(left._value, right._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static {0} IBitwiseOperators<{0}, {0}, {0}>.operator ~({0} value) 
		=> IPrimitiveNumericType<{0}, {1}>.OnesComplement(value._value);";

	/// <summary>
	/// Generates operators for numeric structures.
	/// </summary>
	/// <param name="numericSymbol">A type symbol of numeric structure.</param>
	/// <param name="context">Generation context.</param>
	/// <param name="underlineType">Primitive underline type.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GeneratePrimitiveNumericOperators(this ISymbol numericSymbol, GeneratorExecutionContext context,
		String underlineType)
	{
		String fileName = $"{numericSymbol.Name}.Numeric.g.cs";
		String formattable = numericSymbol.Name is "JChar" ?
			GenerationExtensions.CharFormattable :
			String.Format(GenerationExtensions.NumericFormattableFormat, numericSymbol.Name, underlineType);
		String numerics = numericSymbol.Name is "JDouble" or "JFloat" ?
			String.Format(GenerationExtensions.FloatingFormattableFormat, numericSymbol.Name, underlineType) :
			String.Format(GenerationExtensions.IntegerFormattableFormat, numericSymbol.Name, underlineType);
		String source = String.Format(GenerationExtensions.NumericPrimitiveOperatorsFormat,
		                              numericSymbol.ContainingNamespace, numericSymbol.Name, underlineType, formattable,
		                              numerics);
#pragma warning disable RS1035
		context.AddSource(fileName, source);
#pragma warning restore RS1035
	}
}