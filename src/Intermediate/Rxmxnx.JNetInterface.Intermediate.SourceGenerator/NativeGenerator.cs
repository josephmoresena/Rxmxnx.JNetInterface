using System;
using System.Text;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

internal static class NativeGenerator
{
	public const String AssemblyName = "Rxmxnx.JNetInterface.Native.Intermediate";

	private const String sourceNamespace = "namespace Rxmxnx.JNetInterface.Native;";
	private const String genericDocFormat =
		"/// <typeparam name=\"TArg{0}\"><see cref=\"IDataType\"/> type of {0}{1} method argument.</typeparam>";
	private const String argTypeFormat = "TArg{0}";
	private const String constraintFormat = "\twhere TArg{0} : IDataType, IObject ";
	private const String argumentFormat = "JArgumentMetadata.Create<TArg{0}>()";

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
			DefinitionSufix = "> : JMethodDefinitionBase ",
			BodyPrefix = @"{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	public JConstructorDefinition() : 
		base(ConstructorName, ",
			BodySufix = @") { }
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
			DefinitionSufix = @"> : JMethodDefinitionBase 
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
			BodySufix = @") { }
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
			DefinitionSufix = "> : JMethodDefinitionBase ",
			BodyPrefix = @"{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name=""methodName"">Method name.</param>
	public JMethodDefinition(CString methodName) : 
		base(methodName, ",
			BodySufix = @") { }
}",
		};
	}

	public static void Execute(GeneratorExecutionContext context)
	{
		NativeGenerator.Generate(context, NativeGenerator.constructor);
		NativeGenerator.Generate(context, NativeGenerator.function);
		NativeGenerator.Generate(context, NativeGenerator.method);
	}

	private static void Generate(GeneratorExecutionContext context, NativeMethodGenerator method)
	{
		StringBuilder strBuildDoc = new();
		StringBuilder strBuildArgType = new();
		StringBuilder strBuildCons = new();
		StringBuilder strBuildArg = new();

		for (UInt32 i = 1; i <= Byte.MaxValue; i++)
		{
			NativeGenerator.PrepareFile(strBuildDoc, strBuildArgType, strBuildArg, i);
			strBuildDoc.Append(String.Format(NativeGenerator.genericDocFormat, i, i.GetOrdinalSuffix()));
			strBuildArgType.Append(String.Format(NativeGenerator.argTypeFormat, i));
			strBuildCons.AppendLine(String.Format(NativeGenerator.constraintFormat, i));
			strBuildArg.Append(String.Format(NativeGenerator.argumentFormat, i));

			StringBuilder strBuildSource = new();

			strBuildSource.AppendLine(NativeGenerator.sourceNamespace);
			strBuildSource.AppendLine(method.Documentation);
			strBuildSource.AppendLine(strBuildDoc.ToString());
			strBuildSource.Append(method.DefinitionPrefix);
			strBuildSource.Append(strBuildArgType);
			strBuildSource.AppendLine(method.DefinitionSufix);
			strBuildSource.Append(strBuildCons);
			strBuildSource.Append(method.BodyPrefix);
			strBuildSource.Append(strBuildArg);
			strBuildSource.AppendLine(method.BodySufix);

			context.AddSource($"{method.TypeName}.g{i:000}.cs", strBuildSource.ToString());
		}
	}
	private static void PrepareFile(StringBuilder strBuildDoc, StringBuilder strBuildArgType, StringBuilder strBuildArg,
		UInt32 index)
	{
		if (index > 1)
		{
			strBuildDoc.Append(Environment.NewLine);
			strBuildArgType.Append(", ");
			strBuildArg.Append(", ");
		}
	}
}