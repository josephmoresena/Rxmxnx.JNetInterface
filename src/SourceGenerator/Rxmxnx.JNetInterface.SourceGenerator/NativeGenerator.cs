using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

[ExcludeFromCodeCoverage]
[SuppressMessage("csharpsquid", "S3963:\"static\" fields should be initialized inline",
                 Justification = "Static constructor is needed.")]
internal static class NativeGenerator
{
	public const String AssemblyName = "Rxmxnx.JNetInterface.Native.Intermediate";

	private const String SourceNamespace = "namespace Rxmxnx.JNetInterface.Native;";
	private const String GenericDocFormat =
		"/// <typeparam name=\"TArg{0}\"><see cref=\"IDataType\"/> type of {0}{1} method argument.</typeparam>";
	private const String ArgTypeFormat = "TArg{0}";
	private const String ConstraintFormat = "\twhere TArg{0} : IDataType, IObject ";
	private const String ArgumentFormat = "JArgumentMetadata.Create<TArg{0}>()";

	private static readonly NativeMethodGenerator constructor;
	private static readonly NativeMethodGenerator function;
	private static readonly NativeMethodGenerator method;

	static NativeGenerator()
	{
		NativeGenerator.constructor = new()
		{
			TypeName = "JConstructorDefinition",
			Documentation = @"
/// <summary>
/// This class stores a <see cref=""JClass""/> constructor definition.
/// </summary>",
			DefinitionPrefix = "public sealed partial record JConstructorDefinition<",
			DefinitionSuffix = "> : JMethodDefinitionBase ",
			BodyPrefix = @"{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	public JConstructorDefinition() : 
		base(ConstructorName, ",
			BodySuffix = @") { }
}",
		};
		NativeGenerator.function = new()
		{
			TypeName = "JFunctionDefinition",
			Documentation = @"
/// <summary>
/// This class stores a <see cref=""JClass""/> function definition.
/// </summary>
/// <typeparam name=""TResult""><see cref=""IDataType""/> type of function result.</typeparam>",
			DefinitionPrefix = "public sealed partial record JFunctionDefinition<TResult, ",
			DefinitionSuffix = @"> : JMethodDefinitionBase 
	where TResult : IDataType<TResult>",
			BodyPrefix = @"{
	/// <summary>
	/// Return type.
	/// </summary>
	private static readonly Type returnType = GetReturnType<TResult>();
	
	/// <inheritdoc/>
	internal override Type? Return => returnType;
	
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name=""functionName"">Function name.</param>
	public JFunctionDefinition(CString functionName) : 
		base(functionName, TResult.Signature, ",
			BodySuffix = @") { }
}",
		};
		NativeGenerator.method = new()
		{
			TypeName = "JMethodDefinition",
			Documentation = @"
/// <summary>
/// This class stores a <see cref=""JClass""/> method definition.
/// </summary>",
			DefinitionPrefix = "public sealed partial record JMethodDefinition<",
			DefinitionSuffix = "> : JMethodDefinitionBase ",
			BodyPrefix = @"{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name=""methodName"">Method name.</param>
	public JMethodDefinition(CString methodName) : 
		base(methodName, ",
			BodySuffix = @") { }
}",
		};
	}

	public static void Execute(GeneratorExecutionContext context)
	{
		NativeGenerator.Generate(context, NativeGenerator.constructor);
		NativeGenerator.Generate(context, NativeGenerator.function);
		NativeGenerator.Generate(context, NativeGenerator.method);
	}

	private static void Generate(GeneratorExecutionContext context, NativeMethodGenerator generator)
	{
		StringBuilder strBuildDoc = new();
		StringBuilder strBuildArgType = new();
		StringBuilder strBuildCons = new();
		StringBuilder strBuildArg = new();

		for (UInt32 i = 1; i <= Byte.MaxValue; i++)
		{
			NativeGenerator.PrepareFile(strBuildDoc, strBuildArgType, strBuildArg, i);
			strBuildDoc.Append(String.Format(NativeGenerator.GenericDocFormat, i, i.GetOrdinalSuffix()));
			strBuildArgType.Append(String.Format(NativeGenerator.ArgTypeFormat, i));
			strBuildCons.AppendLine(String.Format(NativeGenerator.ConstraintFormat, i));
			strBuildArg.Append(String.Format(NativeGenerator.ArgumentFormat, i));

			StringBuilder strBuildSource = new();

			strBuildSource.AppendLine(NativeGenerator.SourceNamespace);
			strBuildSource.AppendLine(generator.Documentation);
			strBuildSource.AppendLine(strBuildDoc.ToString());
			strBuildSource.Append(generator.DefinitionPrefix);
			strBuildSource.Append(strBuildArgType);
			strBuildSource.AppendLine(generator.DefinitionSuffix);
			strBuildSource.Append(strBuildCons);
			strBuildSource.Append(generator.BodyPrefix);
			strBuildSource.Append(strBuildArg);
			strBuildSource.AppendLine(generator.BodySuffix);

#pragma warning disable RS1035
			context.AddSource($"{generator.TypeName}.g{i:000}.cs", strBuildSource.ToString());
#pragma warning restore RS1035
		}
	}
	private static void PrepareFile(StringBuilder strBuildDoc, StringBuilder strBuildArgType, StringBuilder strBuildArg,
		UInt32 index)
	{
		if (index <= 1) return;
		strBuildDoc.AppendLine();
		strBuildArgType.Append(", ");
		strBuildArg.Append(", ");
	}
}