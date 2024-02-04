using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	/// <summary>
	/// Retrieves a enumeration of declared types in the generator context.
	/// </summary>
	/// <param name="context">Generation context.</param>
	/// <returns>The enumeration of declared types in the generator context..</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<INamedTypeSymbol> GetSourceTypeSymbols(this GeneratorExecutionContext context)
		=> GenerationExtensions.GetTypeSymbols(context.Compilation.SourceModule.GlobalNamespace);
	/// <summary>
	/// Retrieves a set of implemented interfaces' names in type symbol.
	/// </summary>
	/// <param name="typeSymbol">Type symbol.</param>
	/// <returns>The set of implemented interfaces' names in type symbol.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IImmutableSet<String> GetInterfacesNames(this INamedTypeSymbol typeSymbol)
	{
		if (typeSymbol is { TypeKind: TypeKind.Struct, IsRefLikeType: false, } ||
		    typeSymbol is { TypeKind: TypeKind.Class, IsStatic: false, })
			return typeSymbol.AllInterfaces.Select(i => i.ToString()).ToImmutableHashSet();
		return ImmutableHashSet<String>.Empty;
	}
	/// <summary>
	/// Retrieves the abbreviation of given number ordinal.
	/// </summary>
	/// <param name="number">A number.</param>
	/// <returns>The abbreviation of <paramref name="number"/> ordinal.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static IEnumerable<INamedTypeSymbol> GetTypeSymbols(INamespaceSymbol namespaceSymbol)
	{
		foreach (INamespaceOrTypeSymbol symbol in namespaceSymbol.GetMembers())
		{
			if (symbol.IsType)
				yield return (INamedTypeSymbol)symbol;
			else
				foreach (INamedTypeSymbol typeSymbol in GenerationExtensions.GetTypeSymbols((INamespaceSymbol)symbol))
					yield return typeSymbol;
		}
	}
}