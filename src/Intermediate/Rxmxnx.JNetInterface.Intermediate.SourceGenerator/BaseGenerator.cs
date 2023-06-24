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

            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.Identifiers.JFieldId", "_value");
            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.Identifiers.JMethodId", "_value");
            context.GenerateObjectReferenceOperator("Rxmxnx.JNetInterface.Native.References.JArrayLocalRef");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JBooleanArrayLocalRef");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JByteArrayLocalRef");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JCharArrayLocalRef");
            context.GenerateObjectReferenceOperator("Rxmxnx.JNetInterface.Native.References.JClassLocalRef");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JDoubleArrayLocalRef");
            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.References.JEnvironmentRef", "_value");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JFloatArrayLocalRef");
            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.References.JGlobalRef", "_value");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JIntArrayLocalRef");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JLongArrayLocalRef");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JObjectArrayLocalRef");
            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.References.JObjectLocalRef", "_value");
            context.GenerateArrayReferenceOperators("Rxmxnx.JNetInterface.Native.References.JShortArrayLocalRef");
            context.GenerateObjectReferenceOperator("Rxmxnx.JNetInterface.Native.References.JStringLocalRef");
            context.GenerateObjectReferenceOperator("Rxmxnx.JNetInterface.Native.References.JThrowableLocalRef");
            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.References.JVirtualMachineRef", "_value");
            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.References.JWeakRef", "_value");

            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.Values.JEnvironmentValue", "_functions");
            context.GenerateNativeToString("Rxmxnx.JNetInterface.Native.Values.JInvokeInterface");
            context.GenerateNativeToString("Rxmxnx.JNetInterface.Native.Values.JNativeInterface");
            context.GenerateNativeToString("Rxmxnx.JNetInterface.Native.Values.JValue");
            context.GenerateNativeEquatableOperator("Rxmxnx.JNetInterface.Native.Values.JVirtualMachineValue", "_functions");
        }
    }
}
