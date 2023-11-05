namespace Rxmxnx.JNetInterface.Internal.Native;

/// <summary>
/// Context of a read-only fixed primitive sequence.
/// </summary>
/// <typeparam name="TValue">Type of objects in the read-only fixed memory block.</typeparam>
internal record JNativeReadOnlyContext<TValue> : IReadOnlyFixedContext<TValue> where TValue : unmanaged
{
	/// <summary>
	/// Internal handler.
	/// </summary>
	public JNativeMemoryHandler Handler { get; }
	/// <summary>
	/// Indicates whether current instance is valid.
	/// </summary>
	public IWrapper<Boolean> Invalid { get; }
	/// <summary>
	/// Internal offset.
	/// </summary>
	public Int32 Offset { get; }
	/// <summary>
	/// Binary memory size.
	/// </summary>
	public Int32 BinarySize => this.Handler.BinarySize - this.Offset;
	/// <summary>
	/// Number of elements in fixed memory sequence.
	/// </summary>
	public Int32 Count => this.BinarySize / NativeUtilities.SizeOf<TValue>();
	/// <summary>
	/// Indicates whether current instance is read-only.
	/// </summary>
	public virtual Boolean ReadOnly => true;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="handler">A <see cref="JNativeMemoryHandler"/> instance.</param>
	/// <param name="invalid">Indicates whether current instance is invalid.</param>
	/// <param name="offset">Offset.</param>
	public JNativeReadOnlyContext(JNativeMemoryHandler handler, IWrapper<Boolean> invalid, Int32 offset = 0)
	{
		this.Handler = handler;
		this.Invalid = invalid;
		this.Offset = offset;
	}
	/// <inheritdoc/>
	public IntPtr Pointer => this.Handler.Pointer + this.Offset;
	/// <inheritdoc/>
	public ReadOnlySpan<Byte> Bytes
	{
		get
		{
			ValidationUtilities.ThrowIfInvalidSequence(this.Invalid);
			return this.Pointer.GetUnsafeReadOnlySpan<Byte>(this.BinarySize);
		}
	}
	/// <inheritdoc/>
	public ReadOnlySpan<TValue> Values
	{
		get
		{
			ValidationUtilities.ThrowIfInvalidSequence(this.Invalid);
			return this.Pointer.GetUnsafeReadOnlySpan<TValue>(this.BinarySize);
		}
	}

	IReadOnlyFixedContext<Byte> IReadOnlyFixedMemory.AsBinaryContext()
		=> this as IReadOnlyFixedContext<Byte> ?? (this.ReadOnly ?
			new JNativeReadOnlyContext<Byte>(this.Handler, this.Invalid, this.Offset) :
			new JNativeContext<Byte>(this.Handler, this.Invalid, this.Offset));

	/// <inheritdoc/>
	public IReadOnlyFixedContext<TDestination> Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		where TDestination : unmanaged
	{
		IReadOnlyFixedContext<TDestination> result = this as IReadOnlyFixedContext<TDestination> ?? (this.ReadOnly ?
			new JNativeReadOnlyContext<TDestination>(this.Handler, this.Invalid, this.Offset) :
			new JNativeContext<TDestination>(this.Handler, this.Invalid, this.Offset));
		Int32 offset = this.Offset + result.Values.Length * NativeUtilities.SizeOf<TDestination>();
		residual = this.ReadOnly ?
			new JNativeReadOnlyContext<Byte>(this.Handler, this.Invalid, offset) :
			new JNativeContext<Byte>(this.Handler, this.Invalid, offset);
		return result;
	}
}