using System;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	private const String PrimitiveToStringFormat = @"#nullable enable
// <auto-generated/>
namespace {0};

partial struct {1} 
{{
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override String ToString() => this.{2}.ToString();
}}
#nullable restore";

	/// <summary>
	/// Generates overrides for <see cref="Object.ToString()"/> for natives structures.
	/// </summary>
	/// <param name="primitiveSymbol">A type symbol of primitive structure.</param>
	/// <param name="context">Generation context.</param>
	/// <param name="valueName">Internal absolute value field name.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GeneratePrimitiveToString(this ISymbol primitiveSymbol, GeneratorExecutionContext context,
		String valueName)
	{
		String fileName = $"{primitiveSymbol.Name}.ToString.g.cs";
		String source = String.Format(GenerationExtensions.PrimitiveToStringFormat, primitiveSymbol.ContainingNamespace,
		                              primitiveSymbol.Name, valueName);
		context.AddSource(fileName, source);
	}
}