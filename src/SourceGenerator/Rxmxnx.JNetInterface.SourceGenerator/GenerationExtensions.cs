using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rxmxnx.JNetInterface.SourceGenerator;

[ExcludeFromCodeCoverage]
internal static partial class GenerationExtensions
{
	/// <summary>
	/// Retrieves all <see cref="INamedTypeSymbol"/> in processing assembly.
	/// </summary>
	/// <param name="syntaxProvider">The current <see cref="SyntaxValueProvider"/> instance.</param>
	/// <returns>A <see cref="IncrementalValuesProvider{INamedTypeSymbol}"/> instance.</returns>
	public static IncrementalValuesProvider<INamedTypeSymbol> GetTypeSymbols(this SyntaxValueProvider syntaxProvider)
		=> syntaxProvider
		   .CreateSyntaxProvider(GenerationExtensions.IsTypeSymbol, GenerationExtensions.GetNamedTypeSymbol)
		   .SelectUnique();
	/// <summary>
	/// Retrieves all <see cref="NativeTypeHelper"/> associated to each named symbol in <paramref name="namedSymbols"/>.
	/// </summary>
	/// <param name="namedSymbols">The current <see cref="IncrementalValuesProvider{NativeTypeHelper}"/> instance.</param>
	/// <returns>A <see cref="IncrementalValuesProvider{NativeTypeHelper}"/> instance.</returns>
	public static IncrementalValuesProvider<NativeTypeHelper> GetNativeTypeHelpers(
		this IncrementalValuesProvider<INamedTypeSymbol> namedSymbols)
		=> namedSymbols.Where(s => s.TypeKind is TypeKind.Struct).Select(GenerationExtensions.GetHelper)
		               .Where(h => h is not null)!;

	/// <summary>
	/// Selects the uniques <see cref="INamedTypeSymbol"/> in <paramref name="source"/>.
	/// </summary>
	/// <param name="source">The current <see cref="IncrementalValuesProvider{INamedTypeSymbol}"/> instance.</param>
	/// <returns>Filtered <paramref name="source"/> instance.</returns>
	private static IncrementalValuesProvider<INamedTypeSymbol> SelectUnique(
		this IncrementalValuesProvider<INamedTypeSymbol?> source)
	{
		IncrementalValueProvider<ImmutableArray<INamedTypeSymbol?>> incArray =
			source.Where(s => s is not null).Collect();
		IncrementalValueProvider<IEnumerable<INamedTypeSymbol>> incEnumerable =
			incArray.Select((arr, _) => arr.Distinct(SymbolEqualityComparer.Default).Cast<INamedTypeSymbol>());
		return incEnumerable.SelectMany((e, _) => e);
	}
	/// <summary>
	/// Indicates whether <paramref name="node"/> is a <see cref="TypeDeclarationSyntax"/> instance.
	/// </summary>
	/// <param name="node">A <see cref="SyntaxNode"/> instance.</param>
	/// <param name="_">A <see cref="CancellationToken"/> instance.</param>
	/// <returns>
	/// A <see langword="true"/> if <paramref name="node"/> is a <see cref="TypeDeclarationSyntax"/> instance;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean IsTypeSymbol(SyntaxNode node, CancellationToken _) => node is TypeDeclarationSyntax;
	/// <summary>
	/// Retrieves the <paramref name="ctx"/> node as a <see cref="INamedTypeSymbol"/> instance.
	/// </summary>
	/// <param name="ctx">A <see cref="GeneratorSyntaxContext"/> instance.</param>
	/// <param name="_">A <see cref="CancellationToken"/> instance.</param>
	/// <returns>
	/// The current node as <see cref="INamedTypeSymbol"/> instance or <see langword="null"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static INamedTypeSymbol? GetNamedTypeSymbol(GeneratorSyntaxContext ctx, CancellationToken _)
		=> ctx.SemanticModel.GetDeclaredSymbol(ctx.Node) as INamedTypeSymbol;
	/// <summary>
	/// Creates a <see cref="NativeTypeHelper"/> for <paramref name="typeSymbol"/>.
	/// </summary>
	/// <param name="typeSymbol">A <see cref="INamedTypeSymbol"/> instance.</param>
	/// <param name="_">A <see cref="CancellationToken"/> instance.</param>
	/// <returns>A <see cref="Nullable{NativeTypeHelper}"/> instance.</returns>
	private static NativeTypeHelper? GetHelper(INamedTypeSymbol? typeSymbol, CancellationToken _)
	{
		if (typeSymbol is null) return default;
		ImmutableHashSet<String> interfaces =
			typeSymbol.AllInterfaces.Select(i => i.ToDisplayString()).ToImmutableHashSet();
		return interfaces.Contains("Rxmxnx.JNetInterface.Types.INativeType") ? new(typeSymbol, interfaces) : default;
	}
}