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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override String ToString() => this.Hash;
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override Int32 GetHashCode() => this.Hash.GetHashCode();
	/// <inheritdoc/>
	public Boolean Equals(InfoSequenceBase? other) => this.Hash.Equals(other?.Hash);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => this.Equals(obj as InfoSequenceBase);

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