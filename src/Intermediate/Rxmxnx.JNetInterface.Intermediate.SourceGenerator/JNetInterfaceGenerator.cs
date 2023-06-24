using System;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator
{
    [Generator]
    public class JNetInterfaceGenerator : ISourceGenerator
    {
        private const String abstractionsAssembly = "Rxmxnx.JNetInterface.Abstractions";
        private const String implementationAssembly = "Rxmxnx.JNetInterface";

        public void Execute(GeneratorExecutionContext context)
        {
            INamedTypeSymbol[] typeSymbols = context.GetSourceTypeSymbols();
            switch (context.Compilation.AssemblyName)
            {
                case BaseGenerator.AssemblyName:
                    BaseGenerator.Execute(context);
                    break;
                case NativeGenerator.AssemblyName:
                    NativeGenerator.Execute(context);
                    break;
                case abstractionsAssembly:
                    BaseGenerator.Execute(context);
                    NativeGenerator.Execute(context);
                    break;
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
}