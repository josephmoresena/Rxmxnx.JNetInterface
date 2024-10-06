namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class AccessibleInfoSequence
{
	/// <summary>
	/// State for buffer creation.
	/// </summary>
	/// <param name="nameChr0">Pointer to UTF-8 name.</param>
	/// <param name="nameLength">Name length.</param>
	/// <param name="returnTypeChr0">Pointer to UTF-8 return type.</param>
	/// <param name="returnTypeLength">Return type length.</param>
	private readonly unsafe struct CallSpanState(
		Byte* nameChr0,
		Int32 nameLength,
		Byte* returnTypeChr0,
		Int32 returnTypeLength,
		void* argsPtr,
		Int32 argsCount)
	{
		/// <summary>
		/// Method name span.
		/// </summary>
		/// <returns>A read-only byte span.</returns>
		public ReadOnlySpan<Byte> GetNameSpan() => new(nameChr0, nameLength);
		/// <summary>
		/// Method return type span.
		/// </summary>
		/// <returns>A read-only byte span.</returns>
		public ReadOnlySpan<Byte> GetReturnTypeSpan() => new(returnTypeChr0, returnTypeLength);
		/// <summary>
		/// Method arguments span.
		/// </summary>
		/// <returns>A read-only <see cref="JArgumentMetadata"/> span.</returns>
		public ReadOnlySpan<JArgumentMetadata> GetArgumentsSpan() => new(argsPtr, argsCount);
	}
}