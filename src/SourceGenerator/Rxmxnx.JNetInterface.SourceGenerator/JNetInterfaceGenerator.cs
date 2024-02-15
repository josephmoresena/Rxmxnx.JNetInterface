using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

[ExcludeFromCodeCoverage]
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
				case "Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode.UnicodeClassNames":
				case "Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode.UnicodePrimitiveArraySignatures":
				case "Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode.UnicodePrimitiveSignatures":
				case "Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode.UnicodeMethodNames":
				case "Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode.UnicodeMethodSignatures":
				case "Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode.UnicodeObjectSignatures":
				case "Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode.UnicodeWrapperObjectArraySignatures":
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