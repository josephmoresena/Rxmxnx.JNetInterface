namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class AccessibleInfoSequence
{
	/// <summary>
	/// Writes a call information on <paramref name="span"/> using <paramref name="arg"/>.
	/// </summary>
	/// <param name="span">A UTF-16 char buffer.</param>
	/// <param name="arg">A <see cref="CallSpanState"/> instance.</param>
	private static void WriteCallBuffer(Span<Char> span, CallSpanState arg)
	{
		Span<Byte> buffer = span.AsBytes();
		ReadOnlySpan<Byte> nameSpan = arg.GetNameSpan();
		ReadOnlySpan<Byte> returnTypeSpan = arg.GetReturnTypeSpan();
		ReadOnlySpan<JArgumentMetadata> argsSpan = arg.GetArgumentsSpan();

		nameSpan.CopyTo(buffer);
		buffer = AccessibleInfoSequence.WriteCallArguments(buffer[(nameSpan.Length + 1)..], argsSpan);
		returnTypeSpan.CopyTo(buffer);
	}
	/// <summary>
	/// Writes a field information on <paramref name="span"/> using <paramref name="arg"/>.
	/// </summary>
	/// <param name="span">A UTF-16 char buffer.</param>
	/// <param name="arg">A <see cref="FieldSpanState"/> instance.</param>
	private static void WriteFieldBuffer(Span<Char> span, FieldSpanState arg)
	{
		Span<Byte> buffer = span.AsBytes();
		ReadOnlySpan<Byte> nameSpan = arg.GetNameSpan();
		ReadOnlySpan<Byte> returnTypeSpan = arg.GetReturnTypeSpan();
		nameSpan.CopyTo(buffer);
		buffer = buffer[(nameSpan.Length + 1)..];
		returnTypeSpan.CopyTo(buffer);
	}
	/// <summary>
	/// Retrieves the needed length to write the all call arguments signature.
	/// </summary>
	/// <param name="args">List of call arguments.</param>
	/// <param name="callSize">Total size in bytes of call parameters.</param>
	/// <param name="referenceCount">Reference counts.</param>
	/// <param name="sizes">Arguments sizes.</param>
	/// <returns>Needed length to write the all call arguments signature.</returns>
	private static Int32 GetArgumentLength(ReadOnlySpan<JArgumentMetadata> args, out Int32 callSize,
		out Int32 referenceCount, out Int32[] sizes)
	{
		Int32 result = 0;
		callSize = 0;
		referenceCount = 0;
		sizes = args.Length > 0 ? new Int32[args.Length] : [];
		for (Int32 i = 0; i < args.Length; i++)
		{
			Int32 length = args[i].Signature.Length;
			result += length;
			callSize += args[i].Size;
			sizes[i] = args[i].Size;
			if (length > 1) referenceCount++;
		}
		return result;
	}
	/// <summary>
	/// Writes the call arguments information on <paramref name="buffer"/>.
	/// </summary>
	/// <param name="buffer">A UTF-8 char buffer.</param>
	/// <param name="args">List of call arguments.</param>
	/// <returns><paramref name="buffer"/> with written bytes as its offset.</returns>
	private static Span<Byte> WriteCallArguments(Span<Byte> buffer, ReadOnlySpan<JArgumentMetadata> args)
	{
		buffer[0] = CommonNames.MethodParameterPrefixChar;
		buffer = buffer[1..];
		foreach (JArgumentMetadata arg in args)
		{
			ReadOnlySpan<Byte> signature = arg.Signature.AsSpan();
			signature.CopyTo(buffer);
			buffer = buffer[signature.Length..];
		}
		buffer[0] = CommonNames.MethodParameterSuffixChar;
		return buffer[1..];
	}
}