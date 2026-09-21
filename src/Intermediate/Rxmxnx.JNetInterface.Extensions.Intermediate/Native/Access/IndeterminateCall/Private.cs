namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="definition">Internal definition.</param>
	/// <param name="returnType">Return type signature.</param>
	private IndeterminateCall(JCallDefinition definition, CString returnType)
	{
		this.Definition = definition;
		this.ReturnType = returnType;
	}

	/// <summary>
	/// Creates a primitive <see cref="JFunctionDefinition"/> instance.
	/// </summary>
	/// <param name="functionName">UTF-8 function name.</param>
	/// <param name="returnTypeSignature">Return type signature.</param>
	/// <param name="args">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JFunctionDefinition"/> instance.</returns>
	private static JFunctionDefinition CreatePrimitiveFunction(ReadOnlySpan<Byte> functionName,
		ReadOnlySpan<Byte> returnTypeSignature, ReadOnlySpan<JArgumentMetadata> args)
		=> returnTypeSignature[0] switch
		{
			CommonNames.BooleanSignatureChar => JFunctionDefinition<JBoolean>.Create(functionName, args),
			CommonNames.ByteSignatureChar => JFunctionDefinition<JByte>.Create(functionName, args),
			CommonNames.CharSignatureChar => JFunctionDefinition<JChar>.Create(functionName, args),
			CommonNames.DoubleSignatureChar => JFunctionDefinition<JDouble>.Create(functionName, args),
			CommonNames.FloatSignatureChar => JFunctionDefinition<JFloat>.Create(functionName, args),
			CommonNames.IntSignatureChar => JFunctionDefinition<JInt>.Create(functionName, args),
			CommonNames.LongSignatureChar => JFunctionDefinition<JLong>.Create(functionName, args),
			_ => JFunctionDefinition<JShort>.Create(functionName, args),
		};
}