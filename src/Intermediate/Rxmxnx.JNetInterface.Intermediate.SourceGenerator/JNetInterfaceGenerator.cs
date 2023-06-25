using System;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

[Generator]
public class JNetInterfaceGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        foreach (INamedTypeSymbol typeSymbol in context.GetSourceTypeSymbols())
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
                // Native types.
                case "Rxmxnx.JNetInterface.Native.Values.JInvokeInterface":
                case "Rxmxnx.JNetInterface.Native.Values.JNativeInterface":
                case "Rxmxnx.JNetInterface.Native.Values.JValue":
                    typeSymbol.GenerateNativeStructToString(context);
                    break;
                // Native Equatable types.
                case "Rxmxnx.JNetInterface.Native.Identifiers.JFieldId":
                case "Rxmxnx.JNetInterface.Native.Identifiers.JMethodId":
                case "Rxmxnx.JNetInterface.Native.References.JEnvironmentRef":
                case "Rxmxnx.JNetInterface.Native.References.JGlobalRef":
                case "Rxmxnx.JNetInterface.Native.References.JObjectLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JVirtualMachineRef":
                case "Rxmxnx.JNetInterface.Native.References.JWeakRef":
                    typeSymbol.GenerateSelfEquatableStructOperators(context, "_value");
                    typeSymbol.GenerateNativeStructToString(context);
                    break;
                case "Rxmxnx.JNetInterface.Native.Values.JEnvironmentValue":
                case "Rxmxnx.JNetInterface.Native.Values.JVirtualMachineValue":
                    typeSymbol.GenerateSelfEquatableStructOperators(context, "_functions");
                    typeSymbol.GenerateNativeStructToString(context);
                    break;
                // Object References
                case "Rxmxnx.JNetInterface.Native.References.JArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JClassLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JStringLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JThrowableLocalRef":
                    typeSymbol.GenerateObjectRefOperators(context);
                    typeSymbol.GenerateSelfEquatableStructOperators(context, "_value");
                    typeSymbol.GenerateNativeStructToString(context);
                    break;
                // Array References
                case "Rxmxnx.JNetInterface.Native.References.JBooleanArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JByteArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JCharArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JDoubleArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JFloatArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JIntArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JLongArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JObjectArrayLocalRef":
                case "Rxmxnx.JNetInterface.Native.References.JShortArrayLocalRef":
                    typeSymbol.GenerateArrayRefOperators(context);
                    typeSymbol.GenerateObjectRefOperators(context);
                    typeSymbol.GenerateSelfEquatableStructOperators(context, "_value");
                    typeSymbol.GenerateNativeStructToString(context);
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