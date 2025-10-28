namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// State structure for multidimensional array fill.
/// </summary>
/// <typeparam name="TElement">The type of array element.</typeparam>
#if NET9_0_OR_GREATER
internal ref struct ArrayFillState<TElement>(ReadOnlySpan<Int32> dimensions, ReadOnlySpan<TElement> values)
#else
#if !NET9_0_OR_GREATER && !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal unsafe struct ArrayFillState<TElement>
#endif
{
#if NET9_0_OR_GREATER
	/// <summary>
	/// Dimensions span.
	/// </summary>
	private readonly ReadOnlySpan<Int32> _dimensions = dimensions;
	/// <summary>
	/// Data memory.
	/// </summary>
	private ReadOnlySpan<TElement> _memory = values;
#else
	/// <summary>
	/// Dimensions pointer.
	/// </summary>
	private readonly ReadOnlyValPtr<Int32> _dimensionPtr;
	/// <summary>
	/// Pointer to data memory.
	/// </summary>
	private ReadOnlyValPtr<TElement> _memoryPtr;
	/// <summary>
	/// Number of elements in memory.
	/// </summary>
	private Int32 _count;
#endif
	/// <summary>
	/// Delegate for <see cref="JLocalObject"/> instantiation from <see cref="TElement"/> value.
	/// </summary>
	public Func<IEnvironment, TElement, JLocalObject?>? CreateInstance { get; init; }

#if !NET9_0_OR_GREATER
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="dimensionPtr">Pointer to dimensions.</param>
	/// <param name="memoryPtr">Pointer to data memory.</param>
	/// <param name="count">Number of elements in memory.</param>
#pragma warning disable CS8500
	public ArrayFillState(Int32* dimensionPtr, TElement* memoryPtr, Int32 count)
#pragma warning restore CS8500
	{
		this._dimensionPtr = dimensionPtr;
		this._count = count;
		this._memoryPtr = memoryPtr;
	}
#endif
	/// <summary>
	/// Retrieves the primitive span for the current iteration.
	/// </summary>
	/// <param name="count">Number of elements to take.</param>
	/// <returns>A read-only span of <typeparamref name="TElement"/> items.</returns>
	public ReadOnlySpan<TElement> Take(Int32 count)
	{
#if NET9_0_OR_GREATER
		ReadOnlySpan<TElement> result = this._memory;
		if (this._memory.Length > count)
		{
			this._memory = result[count..];
			return result[..this._memory.Length];
		}
		this._memory = [];
		return result;
#else
		Int32 spanLength = Math.Min(count, this._count);
		ReadOnlySpan<TElement> result = new(this._memoryPtr, spanLength);
		if (result.Length > count)
		{
			this._memoryPtr += spanLength;
			this._count -= spanLength;
		}
		this._memoryPtr = default;
		this._count = default;
		return result;
#endif
	}
	/// <inheritdoc cref="Array.GetLength(Int32)"/>
	public Int32 GetLength(Int32 dimension)
#if NET9_0_OR_GREATER
		=> this._dimensions[dimension];
#else
		=> (this._dimensionPtr + dimension).Reference;
#endif
}