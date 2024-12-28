using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static partial class GenerationExtensions
{
	private const String StaticConstructorFormat = @"#nullable enable
// <auto-generated/>
namespace {0};

partial class {1} 
{{
{3}	/// <summary>
	/// Static constructor.
	/// </summary>
	static {1}()
	{{
{2}	}}
}}
#nullable restore";

	/// <summary>
	/// Generates static constructor for UTF-8 constants container.
	/// </summary>
	/// <param name="utf8ClassContainerSymbol">A class symbol of UTF-8 constants container.</param>
	/// <param name="context">Generation context.</param>
	public static void GenerateUtf8ContainerInitializer(this INamedTypeSymbol utf8ClassContainerSymbol,
		GeneratorExecutionContext context)
	{
		String fileName = $"{utf8ClassContainerSymbol.Name}.Initializer.g.cs";
		StringBuilder strBuild = new();
		StringBuilder strBuildProp = new();
		foreach (ISymbol symbol in utf8ClassContainerSymbol.GetMembers()
		                                                   .Where(m => m.IsStatic &&
			                                                          m.Kind is SymbolKind.Field or SymbolKind.Method))
		{
			String? value = symbol.GetLiteralValue();
			if (value is null) continue;
			if (symbol.Kind == SymbolKind.Field)
			{
				strBuild.AppendLine($"\t\t{symbol.Name} = new(() => \"{value}\"u8);");
			}
			else
			{
				String accessibility = symbol.DeclaredAccessibility switch
				{
					Accessibility.Public => "public",
					Accessibility.Internal => "internal",
					_ => "private",
				};
				strBuildProp.AppendLine(
					$"\t{accessibility} static partial ReadOnlySpan<Byte> {symbol.Name}() => \"{value}\"u8;");
			}
		}
		if (strBuildProp.Length > 0) strBuildProp.AppendLine();
		String source = String.Format(GenerationExtensions.StaticConstructorFormat,
		                              utf8ClassContainerSymbol.ContainingNamespace, utf8ClassContainerSymbol.Name,
		                              strBuild, strBuildProp);
#pragma warning disable RS1035
		context.AddSource(fileName, source);
#pragma warning restore RS1035
	}

	/// <summary>
	/// Retrieves the literal value for UTF-8 constant.
	/// </summary>
	/// <param name="symbol">A symbol for UTF-8 constant.</param>
	/// <returns>The string value to assign to UTF-8 constant.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static String? GetLiteralValue(this ISymbol symbol)
	{
		if (!symbol.CanBeReferencedByName)
			return default;

		AttributeData? attribute = symbol.GetAttributes()
		                                 .FirstOrDefault(a => a.AttributeClass?.Name == nameof(DefaultValueAttribute));
		TypedConstant? paramsAttr = (attribute?.ConstructorArguments)?.FirstOrDefault();
		return paramsAttr?.Value as String;
	}
}