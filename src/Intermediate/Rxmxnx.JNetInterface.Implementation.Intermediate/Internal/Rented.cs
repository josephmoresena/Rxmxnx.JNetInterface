namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal rented array struct.
/// </summary>
/// <typeparam name="T">Type of items in the array pool.</typeparam>
internal ref struct Rented<T> where T : unmanaged
{
	/// <summary>
	/// Rented array.
	/// </summary>
	private T[]? _array;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="capacity">Capacity.</param>
	/// <param name="span">Output. Internal span.</param>
	public Rented(Int32 capacity, out Span<T> span)
	{
		this._array = ArrayPool<T>.Shared.Rent(capacity);
		span = this._array.AsSpan()[..capacity];
	}

	/// <summary>
	/// Returns rented memory to the pool.
	/// </summary>
	public void Free()
	{
		Rented<T>.Free(this._array);
		this._array = default;
	}

	/// <summary>
	/// Returns <paramref name="arr"/> to the pool.
	/// </summary>
	/// <param name="arr">Rented array.</param>
	private static void Free(T[]? arr)
	{
		if (arr is not null)
			ArrayPool<T>.Shared.Return(arr, true);
	}
}