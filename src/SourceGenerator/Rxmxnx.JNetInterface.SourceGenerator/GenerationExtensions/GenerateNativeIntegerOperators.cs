using System;
using System.Runtime.CompilerServices;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	private const String NumericIntegerOperatorsFormat = @"#nullable enable
// <auto-generated/>
namespace {0};

partial struct {1} : IBinaryInteger<{1}>, {4}<{1}>
{{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Int32 IBinaryInteger<{1}>.GetByteCount() => sizeof({2});
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Boolean IBinaryInteger<{1}>.TryWriteBigEndian(Span<Byte> destination, out Int32 bytesWritten)
		=> IPrimitiveIntegerType<{1}, {2}>.TryWriteBigEndian(this._value, destination, out bytesWritten);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Boolean IBinaryInteger<{1}>.TryWriteLittleEndian(Span<Byte> destination, out Int32 bytesWritten)
		=> IPrimitiveIntegerType<{1}, {2}>.TryWriteLittleEndian(this._value, destination, out bytesWritten);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	Int32 IBinaryInteger<{1}>.GetShortestBitLength()
		=> IPrimitiveIntegerType<{1}, {2}>.GetShortestBitLength(this._value);
{3}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean IBinaryInteger<{1}>.TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out {1} value)
		=> IPrimitiveIntegerType<{1}, {2}>.TryReadBigEndian(source, isUnsigned, out value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static Boolean IBinaryInteger<{1}>.TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out {1} value)
		=> IPrimitiveIntegerType<{1}, {2}>.TryReadLittleEndian(source, isUnsigned, out value);
}}
#nullable restore";

	private const String BinaryIntegerFormattableFormat = @"
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} PopCount({0} value) 
		=> IPrimitiveIntegerType<{0}, {1}>.PopCount(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} TrailingZeroCount({0} value) 
		=> IPrimitiveIntegerType<{0}, {1}>.TrailingZeroCount(value._value);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator <<({0} value, Int32 shiftAmount) 
		=> IPrimitiveIntegerType<{0}, {1}>.LeftShift(value._value, shiftAmount);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator >>({0} value, Int32 shiftAmount) 
		=> IPrimitiveIntegerType<{0}, {1}>.RightShift(value._value, shiftAmount);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static {0} operator >>>({0} value, Int32 shiftAmount) 
		=> IPrimitiveIntegerType<{0}, {1}>.UnsignedRightShift(value._value, shiftAmount); ";
	private const String BinaryCharFormattable = @"
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IBinaryInteger<JChar>.PopCount(JChar value) => IPrimitiveIntegerType<JChar, Char>.PopCount(value._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IBinaryInteger<JChar>.TrailingZeroCount(JChar value) 
		=> IPrimitiveIntegerType<JChar, Char>.TrailingZeroCount(value._value);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IShiftOperators<JChar, Int32, JChar>.operator <<(JChar value, Int32 shiftAmount) 
		=> (Char)(value._value << shiftAmount);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IShiftOperators<JChar, Int32, JChar>.operator >>(JChar value, Int32 shiftAmount) 
		=> (Char)(value._value >> shiftAmount);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static JChar IShiftOperators<JChar, Int32, JChar>.operator >>>(JChar value, Int32 shiftAmount) 
		=> (Char)(value._value >>> shiftAmount);";

	/// <summary>
	/// Generates operators for integer structures.
	/// </summary>
	/// <param name="numericSymbol">A type symbol of integer structure.</param>
	/// <param name="context">Generation context.</param>
	/// <param name="underlineType">Primitive underline type.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GenerateNumericPrimitiveIntegerOperators(this ISymbol numericSymbol,
		SourceProductionContext context, String underlineType)
	{
		String fileName = $"{numericSymbol.Name}.Integer.g.cs";
		String signedUnsafe = numericSymbol.Name is "JChar" ? "IUnsignedNumber" : "ISignedNumber";
		String formattable = numericSymbol.Name is "JChar" ?
			GenerationExtensions.BinaryCharFormattable :
			String.Format(GenerationExtensions.BinaryIntegerFormattableFormat, numericSymbol.Name, underlineType);
		String source = String.Format(GenerationExtensions.NumericIntegerOperatorsFormat,
		                              numericSymbol.ContainingNamespace, numericSymbol.Name, underlineType, formattable,
		                              signedUnsafe);
#pragma warning disable RS1035
		context.AddSource(fileName, SourceText.From(source, Encoding.UTF8));
#pragma warning restore RS1035
	}
}