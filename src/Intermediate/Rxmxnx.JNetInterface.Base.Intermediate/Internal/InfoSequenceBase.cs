namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Base class for information sequence.
/// </summary>
internal abstract partial class InfoSequenceBase(String hash, Int32 nameLength)
{
	/// <summary>
	/// Information name.
	/// </summary>
	public CString Name { get; } = CString.Create<ItemState>(new(hash, nameLength));
	/// <summary>
	/// Information hash.
	/// </summary>
	public String Hash { get; } = hash;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override String ToString() => this.Hash;
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this.Hash.GetHashCode();

	/// <summary>
	/// Retrieves printable text hash.
	/// </summary>
	/// <param name="hash">Class hash.</param>
	/// <param name="lastChar">Last char hash.</param>
	/// <returns>A read-only UTF-16 char span.</returns>
	[ExcludeFromCodeCoverage]
	public static ReadOnlySpan<Char> GetPrintableHash(String hash, out String lastChar)
	{
		ReadOnlySpan<Char> hashSpan = hash;
		lastChar = hashSpan[^1] == default ? @"\0" : $"{hashSpan[^1]}";
		return hashSpan[..^1];
	}

	/// <summary>
	/// State for retrieve functional <see cref="CString"/>.
	/// </summary>
	/// <param name="buffer">UTF-16 buffer.</param>
	/// <param name="length">UTF-8 text length.</param>
	/// <param name="offset">UTF-8 text offset.</param>
	protected readonly struct ItemState(String buffer, Int32 length, Int32 offset = 0) : IUtf8FunctionState<ItemState>
	{
		/// <summary>
		/// Internal buffer.
		/// </summary>
		private readonly String _buffer = buffer;
		/// <summary>
		/// Buffer length.
		/// </summary>
		private readonly Int32 _length = length;
		private readonly Range _range = new(offset, offset + length);

		/// <inheritdoc/>
		public static ReadOnlySpan<Byte> GetSpan(ItemState state) => state._buffer.AsSpan().AsBytes()[state._range];
		/// <inheritdoc/>
		public static Int32 GetLength(in ItemState state) => state._length;
		/// <inheritdoc/>
		public Boolean IsNullTerminated => true;
	}
}