namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Base class for information sequence.
/// </summary>
internal abstract class InfoSequenceBase(String hash, Int32 nameLength) : IEquatable<InfoSequenceBase>,
	IEqualityOperators<InfoSequenceBase, InfoSequenceBase, Boolean>
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
	public Boolean Equals(InfoSequenceBase? other) => this.Hash.Equals(other?.Hash);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override String ToString() => this.Hash;
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this.Hash.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => this.Equals(obj as InfoSequenceBase);

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

	/// <inheritdoc/>
	public static Boolean operator ==(InfoSequenceBase? left, InfoSequenceBase? right)
		=> left?.Equals(right) ?? right is null;
	/// <inheritdoc/>
	public static Boolean operator !=(InfoSequenceBase? left, InfoSequenceBase? right) => !(left == right);

	/// <summary>
	/// State for retrieve functional <see cref="CString"/>.
	/// </summary>
	/// <param name="buffer">UTF-16 buffer.</param>
	/// <param name="length">UTF-8 text length.</param>
	/// <param name="offset">UTF-8 text offset.</param>
	protected readonly struct ItemState(String buffer, Int32 length, Int32 offset = 0) : IUtf8FunctionState<ItemState>
	{
		private readonly String _buffer = buffer;
		private readonly Int32 _length = length;
		private readonly Range _range = new(offset, offset + length);

		public static ReadOnlySpan<Byte> GetSpan(ItemState state) => state._buffer.AsSpan().AsBytes()[state._range];
		public static Int32 GetLength(ItemState state) => state._length;
		public Boolean IsNullTerminated => true;
	}
}