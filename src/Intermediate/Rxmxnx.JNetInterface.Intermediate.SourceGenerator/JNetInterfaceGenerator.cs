using System;
using System.Collections.Immutable;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

[Generator]
public class JNetInterfaceGenerator : ISourceGenerator
{
	public void Execute(GeneratorExecutionContext context)
	{
		foreach (INamedTypeSymbol typeSymbol in context.GetSourceTypeSymbols())
		{
			switch (typeSymbol.ToString())
			{
				// UTF-8 Containers
				case "Rxmxnx.JNetInterface.Internal.UnicodeClassNames":
				case "Rxmxnx.JNetInterface.Internal.UnicodePrimitiveArraySignatures":
				case "Rxmxnx.JNetInterface.Internal.UnicodePrimitiveSignatures":
				case "Rxmxnx.JNetInterface.UnicodeMethodNames":
				case "Rxmxnx.JNetInterface.UnicodeMethodSignatures":
				case "Rxmxnx.JNetInterface.UnicodeObjectSignatures":
					typeSymbol.GenerateUtf8ContainerInitializer(context);
					break;
				default:
					IImmutableSet<String> interfaces = typeSymbol.GetInterfacesNames();
					NativeTypeHelper? nativeHelper = NativeTypeHelper.Create(typeSymbol, interfaces);
					nativeHelper?.AddSourceCode(context);
					break;
			}
		}
	}

	public void Initialize(GeneratorInitializationContext context)
	{
		// No initialization required for this one
#if DEBUG
		//if (!System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Launch();
#endif
	}
}