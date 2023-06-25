using System;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	private const String objectRefFormat = @"#nullable enable
// <auto-generated/>
namespace {0};

partial struct {1} 
{{
    /// <summary>
    /// Internal constructor.
    /// </summary>
    /// <param name=""objRef""><see cref=""JObjectLocalRef""/> value.</param>
    internal {1}(JObjectLocalRef objRef) => this._value = new(objRef); {2}

    /// <summary>
    /// Defines an explicit conversion of a given <see cref=""JObjectLocalRef""/> to <see cref=""{1}""/>.
    /// </summary>
    /// <param name=""objRef"">A <see cref=""JObjectLocalRef""/> to implicitly convert.</param>
    public static explicit operator {1}(JObjectLocalRef objRef) => new(objRef);

    /// <summary>
    /// Determines whether a specified <see cref=""{1}""/> and a <see cref=""JObjectLocalRef""/> instance
    /// have the same value.
    /// </summary>
    /// <param name=""left"">The <see cref=""{1}""/> to compare.</param>
    /// <param name=""right"">The <see cref=""JObjectLocalRef""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is the same as the value 
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator ==({1} left, JObjectLocalRef right) => left.Equals(right);
    /// <summary>
    /// Determines whether a specified <see cref=""JObjectLocalRef""/> and a <see cref=""{1}""/> instance
    /// have the same value.
    /// </summary>
    /// <param name=""left"">The <see cref=""JObjectLocalRef""/> to compare.</param>
    /// <param name=""right"">The <see cref=""{1}""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is the same as the value 
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator ==(JObjectLocalRef left, {1} right) => left.Equals(right);

    /// <summary>
    /// Determines whether a specified <see cref=""{1}""/> and a <see cref=""JObjectLocalRef""/> instance
    /// have different values.
    /// </summary>
    /// <param name=""left"">The <see cref=""{1}""/> to compare.</param>
    /// <param name=""right"">The <see cref=""JObjectLocalRef""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is different from the value  
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator !=({1} left, JObjectLocalRef right) => !left.Equals(right);
    /// <summary>
    /// Determines whether a specified <see cref=""JObjectLocalRef""/> and a <see cref=""{1}""/> instance
    /// have different values.
    /// </summary>
    /// <param name=""left"">The <see cref=""JObjectLocalRef""/> to compare.</param>
    /// <param name=""right"">The <see cref=""{1}""/> to compare.</param>
    /// <returns>
    /// <see langword=""true""/> if the value of <paramref name=""left""/> is different from the value  
    /// of <paramref name=""right""/>; otherwise, <see langword=""false""/>.
    /// </returns>
    public static Boolean operator !=(JObjectLocalRef left, {1} right) => !left.Equals(right);
}}
#nullable restore";

	private const String objectRefOverrideFormat = @"
	
    /// <inheritdoc/>
    public override Int32 GetHashCode() => HashCode.Combine(this._value);
    /// <inheritdoc/>
    public override Boolean Equals([NotNullWhen(true)] Object? obj) => {0}(this, obj);";

	/// <summary>
	/// Generates operators for object reference structures.
	/// </summary>
	/// <param name="objRefSymbol">A type symbol of object reference structure.</param>
	/// <param name="context">Generation context.</param>
	public static void GenerateObjectRefOperators(this ISymbol objRefSymbol, GeneratorExecutionContext context)
	{
		String equalFunction = objRefSymbol.Name.Contains("ArrayLocalRef")
			? "JArrayLocalRef.ArrayEquals"
			: "JObjectLocalRef.ObjectEquals";
		String overrides = objRefSymbol.Name == "JArrayLocalRef"
			? String.Empty
			: String.Format(GenerationExtensions.objectRefOverrideFormat, equalFunction);

		context.AddSource($"{objRefSymbol.Name}.ObjRef.g.cs",
		                  String.Format(GenerationExtensions.objectRefFormat,
		                                objRefSymbol.ContainingNamespace, objRefSymbol.Name, overrides));
	}
}