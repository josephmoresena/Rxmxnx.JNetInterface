namespace Rxmxnx.JNetInterface.Internal;

internal partial class TypeInfoSequence
{
	/// <summary>
	/// State for buffer creation.
	/// </summary>
	/// <param name="chr0">Pointer to UTF-8 class name.</param>
	/// <param name="length">Class name length.</param>
	/// <param name="isArray">Indicates whether current class is an array.</param>
	private readonly unsafe struct SpanState(Byte* chr0, Int32 length, Boolean isArray)
	{
		/// <summary>
		/// Indicates whether current class is an array.
		/// </summary>
		public Boolean IsArray => isArray;
		/// <summary>
		/// Class name span.
		/// </summary>
		/// <returns>A read-only byte span.</returns>
		public ReadOnlySpan<Byte> GetSpan() => new(chr0, length);
	}
}