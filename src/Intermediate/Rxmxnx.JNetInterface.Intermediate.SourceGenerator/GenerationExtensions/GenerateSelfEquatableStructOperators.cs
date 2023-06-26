using System;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	private const String structEquatableFormat = @"#nullable enable
// <auto-generated/>
namespace {0};

partial struct {1} : IEquatable<{1}> 
{{
    /// <inheritdoc/>
    public Boolean Equals({1} other) => this.{2}.Equals(other.{2});

	/// <summary>
    /// Determines whether two specified <see cref=""{1}""/> instances have the same value.
    /// </summary>
    /// <param name=""left"">The first <see cref=""{1}""/> to compare.</param>
    /// <param name=""right"">The second <see cref=""{1}""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is the same as the value 
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator ==({1} left, {1} right) => left.Equals(right);
    /// <summary>
    /// Determines whether two specified <see cref=""{1}""/> instances have different values.
    /// </summary>
    /// <param name=""left"">The first <see cref=""{1}""/> to compare.</param>
    /// <param name=""right"">The second <see cref=""{1}""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is different from the value  
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator !=({1} left, {1} right) => !(left == right);
}}
#nullable restore";

	/// <summary>
	/// Generates operators for self-equatable structures.
	/// </summary>
	/// <param name="structSymbol">A type symbol of self-equatable structure.</param>
	/// <param name="context">Generation context.</param>
	/// <param name="valueName">Internal absolute value field name.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GenerateSelfEquatableStructOperators(this ISymbol structSymbol,
		GeneratorExecutionContext context, String valueName)
	{
		String fileName = $"{structSymbol.Name}.Equals.g.cs";
		String source = String.Format(GenerationExtensions.structEquatableFormat, structSymbol.ContainingNamespace,
		                              structSymbol.Name, valueName);
		context.AddSource(fileName, source);
	}
}