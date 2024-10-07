namespace Rxmxnx.JNetInterface.Internal;

[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal sealed unsafe partial class AccessibleInfoSequence
{
	/// <summary>
	/// State for buffer creation.
	/// </summary>
	/// <param name="nameChr0">Pointer to UTF-8 name.</param>
	/// <param name="nameLength">Name length.</param>
	/// <param name="returnTypeChr0">Pointer to UTF-8 return type.</param>
	/// <param name="returnTypeLength">Return type length.</param>
	/// <param name="argsPtr">Pointer to call argument list.</param>
	/// <param name="argsCount">Call argument list length.</param>
	private readonly struct CallSpanState(
		Byte* nameChr0,
		Int32 nameLength,
		Byte* returnTypeChr0,
		Int32 returnTypeLength,
		void* argsPtr,
		Int32 argsCount)
	{
		/// <summary>
		/// Call name span.
		/// </summary>
		/// <returns>A read-only byte span.</returns>
		public ReadOnlySpan<Byte> GetNameSpan() => new(nameChr0, nameLength);
		/// <summary>
		/// Call return type signature span.
		/// </summary>
		/// <returns>A read-only byte span.</returns>
		public ReadOnlySpan<Byte> GetReturnTypeSpan() => new(returnTypeChr0, returnTypeLength);
		/// <summary>
		/// Call arguments span.
		/// </summary>
		/// <returns>A read-only <see cref="JArgumentMetadata"/> span.</returns>
		public ReadOnlySpan<JArgumentMetadata> GetArgumentsSpan()
		{
			if (argsCount == 0) return ReadOnlySpan<JArgumentMetadata>.Empty;
			ref JArgumentMetadata arg0 = ref Unsafe.AsRef<JArgumentMetadata>(argsPtr);
			return MemoryMarshal.CreateReadOnlySpan(ref arg0, argsCount);
		}
	}
}