using System;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	private const String PrimitiveOperatorsFormat = @"#nullable enable
// <auto-generated/>
namespace {0};

partial struct {1} : IEqualityOperators<{1}, {1}, Boolean>, IEqualityOperators<{1}, {2}, Boolean>
{{
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => HashCode.Combine(this.{3});
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Boolean Equals([NotNullWhen(true)] Object? obj) 
		=> IPrimitiveType<{1}, {2}>.Equals(this, obj){4};
	
	/// <inheritdoc cref=""IEqualityOperators{{TSelf, TOther, TResult}}.op_Equality(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator ==({1} left, {1} right) => left.{3}.Equals(right.{3});
	/// <inheritdoc cref=""IEqualityOperators{{TSelf, TOther, TResult}}.op_Inequality(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=({1} left, {1} right) => !(left == right);
	
	/// <inheritdoc cref=""IEqualityOperators{{TSelf, TOther, TResult}}.op_Equality(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator ==({1} left, {2} right) => left.{3}.Equals(right);
	/// <inheritdoc cref=""IEqualityOperators{{TSelf, TOther, TResult}}.op_Inequality(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=({1} left, {2} right) => !(left == right);

	/// <inheritdoc cref=""IEqualityOperators{{TSelf, TOther, TResult}}.op_Equality(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator ==({2} left, {1} right) => left.Equals(right.{3});
	/// <inheritdoc cref=""IEqualityOperators{{TSelf, TOther, TResult}}.op_Inequality(TSelf, TOther)"" />
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=({2} left, {1} right) => !(left == right);
}}
#nullable restore";
	private const String PrimitiveNumericEquals = @"
		|| obj is IPrimitiveType primitive && IPrimitiveNumericType.Equals(this, primitive) 
		|| obj is JPrimitiveObject primitiveObj && IPrimitiveNumericType.Equals(this, primitiveObj)";

	/// <summary>
	/// Generates operators for self-equatable structures.
	/// </summary>
	/// <param name="primitiveSymbol">A type symbol of self-equatable structure.</param>
	/// <param name="context">Generation context.</param>
	/// <param name="underlineType">Primitive underline type.</param>
	/// <param name="valueName">Internal absolute value field name.</param>
	/// <param name="isNumeric">Indicates whether the native type is a numeric value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GeneratePrimitiveOperators(this ISymbol primitiveSymbol, GeneratorExecutionContext context,
		String underlineType, String valueName, Boolean isNumeric)
	{
		String fileName = $"{primitiveSymbol.Name}.Primitive.g.cs";
		String equalityComplement = isNumeric ? GenerationExtensions.PrimitiveNumericEquals : String.Empty;
		String source = String.Format(GenerationExtensions.PrimitiveOperatorsFormat,
		                              primitiveSymbol.ContainingNamespace, primitiveSymbol.Name, underlineType,
		                              valueName, equalityComplement);
		context.AddSource(fileName, source);
	}
}