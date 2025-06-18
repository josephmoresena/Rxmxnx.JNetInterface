using System.Diagnostics.CodeAnalysis;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

[ExcludeFromCodeCoverage]
[Generator]
public class JNetInterfaceGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
#if DEBUG
		//if (!System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Launch();
#endif
		IncrementalValuesProvider<INamedTypeSymbol> types = context.SyntaxProvider.GetTypeSymbols();
		IncrementalValuesProvider<NativeTypeHelper> nativeHelpers = types.GetNativeTypeHelpers();
		context.RegisterSourceOutput(nativeHelpers, (c, h) => h.AddSourceCode(c));
	}
}