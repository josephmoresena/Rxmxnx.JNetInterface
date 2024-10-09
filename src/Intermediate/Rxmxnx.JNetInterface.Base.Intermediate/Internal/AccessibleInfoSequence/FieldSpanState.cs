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
	private readonly struct FieldSpanState(
		Byte* nameChr0,
		Int32 nameLength,
		Byte* returnTypeChr0,
		Int32 returnTypeLength)
	{
		/// <summary>
		/// Field name span.
		/// </summary>
		/// <returns>A read-only byte span.</returns>
		public ReadOnlySpan<Byte> GetNameSpan() => new(nameChr0, nameLength);
		/// <summary>
		/// Field return type span.
		/// </summary>
		/// <returns>A read-only byte span.</returns>
		public ReadOnlySpan<Byte> GetReturnTypeSpan() => new(returnTypeChr0, returnTypeLength);
	}
}