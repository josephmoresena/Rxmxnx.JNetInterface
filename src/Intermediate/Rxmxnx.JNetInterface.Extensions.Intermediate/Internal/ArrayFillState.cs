namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// State structure for multidimensional array fill.
/// </summary>
/// <typeparam name="TElement">The type of array element.</typeparam>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
#if NET9_0_OR_GREATER
internal unsafe ref struct ArrayFillState<TElement>(ReadOnlySpan<Int32> dimensions, ReadOnlySpan<TElement> values)
#else
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
	/// Number of dimensions.
	/// </summary>
	private readonly Int32 _dimensionCount;
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
	/// Constructor.
	/// </summary>
	/// <param name="dimensionPtr">Pointer to dimensions.</param>
	/// <param name="dimensions">Number of dimensions.</param>
	/// <param name="memoryPtr">Pointer to data memory.</param>
	/// <param name="count">Number of elements in memory.</param>
#if NET9_0_OR_GREATER && !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
#pragma warning disable CS8500
	public ArrayFillState(Int32* dimensionPtr, Int32 dimensions, TElement* memoryPtr, Int32 count)
#pragma warning restore CS8500
#if !NET9_0_OR_GREATER
	{
		this._dimensionPtr = dimensionPtr;
		this._dimensionCount = dimensions;
		this._count = count;
		this._memoryPtr = memoryPtr;
	}
#else
		: this(new(dimensionPtr, dimensions), new(memoryPtr, count)) { }
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
		Int32 spanLength = Math.Min(count, this._memory.Length);
		if (this._memory.Length > count)
		{
			this._memory = result[count..];
			return result[..spanLength];
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
		=> this._dimensions.Length >= dimension ? this._dimensions[dimension] : -1;
#else
		=> this._dimensionCount > dimension ? (this._dimensionPtr + dimension).Reference : -1;
#endif
}