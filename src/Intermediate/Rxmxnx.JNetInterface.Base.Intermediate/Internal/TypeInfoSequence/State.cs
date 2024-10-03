namespace Rxmxnx.JNetInterface.Internal;

internal partial class TypeInfoSequence
{
	/// <summary>
	/// State for retrieve functional <see cref="CString"/>.
	/// </summary>
	/// <param name="buffer">UTF-16 buffer.</param>
	/// <param name="length">UTF-8 text length.</param>
	/// <param name="offset">UTF-8 text offset.</param>
	private readonly struct State(String buffer, Int32 length, Int32 offset = 0) : IUtf8FunctionState<State>
	{
		private readonly String _buffer = buffer;
		private readonly Int32 _length = length;
		private readonly Range _range = new(offset, offset + length);

		public static ReadOnlySpan<Byte> GetSpan(State state) => state._buffer.AsSpan().AsBytes()[state._range];
		public static Int32 GetLength(State state) => state._length;
		public Boolean IsNullTerminated => true;
	}
}