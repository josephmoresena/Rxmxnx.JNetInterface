using System;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator
{
    [Generator]
    public class JNetInterfaceGenerator : ISourceGenerator
    {
        public const String AbstractionsAssembly = "Rxmxnx.JNetInterface.Abstractions";
        public const String ImplementationAssembly = "Rxmxnx.JNetInterface";

        public void Execute(GeneratorExecutionContext context)
        {
            switch (context.Compilation.AssemblyName)
            {
                case BaseGenerator.AssemblyName:
                    BaseGenerator.Execute(context);
                    break;
                case NativeGenerator.AssemblyName:
                    NativeGenerator.Execute(context);
                    break;
                case AbstractionsAssembly:
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