using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	/// <summary>
	/// Retrieves a collection of declared types in the generator context.
	/// </summary>
	/// <param name="context">Generation context.</param>
	public static IEnumerable<INamedTypeSymbol> GetSourceTypeSymbols(this GeneratorExecutionContext context)
	{
		return GenerationExtensions.GetTypeSymbols(context.Compilation.SourceModule.GlobalNamespace);
	}
	/// <summary>
	/// Retrieves the abbreviation of given number ordinal.
	/// </summary>
	/// <param name="number">A number.</param>
	/// <returns>The abbreviation of <paramref name="number"/> ordinal.</returns>
	public static String GetOrdinalSuffix(this UInt32 number)
	{
		String textNumber = number.ToString();
		if (textNumber.EndsWith("11")) return "th";
		if (textNumber.EndsWith("12")) return "th";
		if (textNumber.EndsWith("13")) return "th";
		if (textNumber.EndsWith("1")) return "st";
		if (textNumber.EndsWith("2")) return "nd";
		if (textNumber.EndsWith("3")) return "rd";
		return "th";
	}

	/// <summary>
	/// Enumerates the symbols all types declared in given namespace symbol.
	/// </summary>
	/// <param name="namespaceSymbol">A symbol for a namespace.</param>
	/// <returns>
	/// A enumeration of the symbols of the declared and nested types in given namespace.
	/// </returns>
	private static IEnumerable<INamedTypeSymbol> GetTypeSymbols(INamespaceSymbol namespaceSymbol)
	{
		foreach (INamespaceOrTypeSymbol symbol in namespaceSymbol.GetMembers())
			if (symbol.IsType)
				yield return (INamedTypeSymbol)symbol;
			else
				foreach (INamedTypeSymbol typeSymbol in GenerationExtensions.GetTypeSymbols((INamespaceSymbol)symbol))
					yield return typeSymbol;
	}
}