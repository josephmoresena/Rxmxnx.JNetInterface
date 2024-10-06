namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Base class for information sequence.
/// </summary>
internal abstract class InfoSequenceBase(String hash, Int32 nameLength)
{
	/// <summary>
	/// Information name.
	/// </summary>
	public CString Name { get; } = CString.Create<ItemState>(new(hash, nameLength));
	/// <summary>
	/// Information hash.
	/// </summary>
	public String Hash { get; } = hash;

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