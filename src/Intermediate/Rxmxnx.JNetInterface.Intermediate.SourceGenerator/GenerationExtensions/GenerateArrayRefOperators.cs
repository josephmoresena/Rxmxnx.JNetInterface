using System;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	private const String arrayRefFormat = @"#nullable enable
// <auto-generated/>
namespace {0};

partial struct {1} : IWrapper<JArrayLocalRef> 
{{
    JArrayLocalRef IWrapper<JArrayLocalRef>.Value => this.ArrayValue;

    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name=""arrayRef""><see cref=""JArrayLocalRef""/> value.</param>
    private {1}(JArrayLocalRef arrayRef) => this._value = arrayRef;

    /// <summary>
    /// Defines an explicit conversion of a given <see cref=""JArrayLocalRef""/> to <see cref=""{1}""/>.
    /// </summary>
    /// <param name=""arrayRef"">A <see cref=""JArrayLocalRef""/> to implicitly convert.</param>
    public static explicit operator {1}(JArrayLocalRef arrayRef) => new(arrayRef);

    /// <summary>
    /// Determines whether a specified <see cref=""{1}""/> and a <see cref=""JArrayLocalRef""/> instance
    /// have the same value.
    /// </summary>
    /// <param name=""left"">The <see cref=""{1}""/> to compare.</param>
    /// <param name=""right"">The <see cref=""JArrayLocalRef""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is the same as the value 
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator ==({1} left, JArrayLocalRef right) => left.Equals(right);
    /// <summary>
    /// Determines whether a specified <see cref=""JArrayLocalRef""/> and a <see cref=""{1}""/> instance
    /// have the same value.
    /// </summary>
    /// <param name=""left"">The <see cref=""JArrayLocalRef""/> to compare.</param>
    /// <param name=""right"">The <see cref=""{1}""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is the same as the value 
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator ==(JArrayLocalRef left, {1} right) => left.Equals(right);

    /// <summary>
    /// Determines whether a specified <see cref=""{1}""/> and a <see cref=""JArrayLocalRef""/> instance
    /// have different values.
    /// </summary>
    /// <param name=""left"">The <see cref=""{1}""/> to compare.</param>
    /// <param name=""right"">The <see cref=""JArrayLocalRef""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is different from the value  
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator !=({1} left, JArrayLocalRef right) => !left.Equals(right);
    /// <summary>
    /// Determines whether a specified <see cref=""JArrayLocalRef""/> and a <see cref=""{1}""/> instance
    /// have different values.
    /// </summary>
    /// <param name=""left"">The <see cref=""JArrayLocalRef""/> to compare.</param>
    /// <param name=""right"">The <see cref=""{1}""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is different from the value  
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator !=(JArrayLocalRef left, {1} right) => !left.Equals(right);
}}
#nullable restore";

	/// <summary>
	/// Generates operators for array reference structures.
	/// </summary>
	/// <param name="arrayRefSymbol">A type symbol of array reference structure.</param>
	/// <param name="context">Generation context.</param>
	public static void GenerateArrayRefOperators(this ISymbol arrayRefSymbol, GeneratorExecutionContext context)
	{
		context.AddSource($"{arrayRefSymbol.Name}.ArrayRef.g.cs",
		                  String.Format(GenerationExtensions.arrayRefFormat,
		                                arrayRefSymbol.ContainingNamespace, arrayRefSymbol.Name));
	}
}