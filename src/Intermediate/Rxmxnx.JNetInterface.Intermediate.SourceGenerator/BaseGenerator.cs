using System;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator
{
    internal static class BaseGenerator
    {
        public const String AssemblyName = "Rxmxnx.JNetInterface.Base.Intermediate";

        public static void Execute(GeneratorExecutionContext context)
        {
            context.GenerateUnicodeConstructor("Rxmxnx.JNetInterface.Internal.UnicodeClassNames");
            context.GenerateUnicodeConstructor("Rxmxnx.JNetInterface.Internal.UnicodePrimitiveArraySignatures");
            context.GenerateUnicodeConstructor("Rxmxnx.JNetInterface.Internal.UnicodePrimitiveSignatures");
            context.GenerateUnicodeConstructor("Rxmxnx.JNetInterface.UnicodeMethodNames");
            context.GenerateUnicodeConstructor("Rxmxnx.JNetInterface.UnicodeMethodSignatures");
            context.GenerateUnicodeConstructor("Rxmxnx.JNetInterface.UnicodeObjectSignatures");
        }
    }
}
