namespace Rxmxnx.JNetInterface.Internal.Native;

/// <summary>
/// Context of a fixed primitive sequence.
/// </summary>
/// <typeparam name="TValue">Type of objects in the read-only fixed memory block.</typeparam>
internal sealed record JNativeContext<TValue> : JNativeReadOnlyContext<TValue>, IFixedContext<TValue>
	where TValue : unmanaged
{
	/// <inheritdoc/>
	public override Boolean ReadOnly => false;

	/// <inheritdoc/>
	public JNativeContext(JNativeMemoryHandler handler, IWrapper<Boolean> invalid, Int32 offset = 0) : base(
		handler, invalid, offset) { }
	/// <inheritdoc/>
	public new Span<Byte> Bytes
	{
		get
		{
			ValidationUtilities.ThrowIfInvalidSequence(this.Invalid);
			return this.Pointer.GetUnsafeSpan<Byte>(this.BinarySize);
		}
	}
	/// <inheritdoc/>
	public new Span<TValue> Values
	{
		get
		{
			ValidationUtilities.ThrowIfInvalidSequence(this.Invalid);
			return this.Pointer.GetUnsafeSpan<TValue>(this.BinarySize);
		}
	}

	IFixedContext<Byte> IFixedMemory.AsBinaryContext()
		=> new JNativeContext<Byte>(this.Handler, this.Invalid, this.Offset);

	/// <inheritdoc/>
	public IFixedContext<TDestination> Transformation<TDestination>(out IFixedMemory residual)
		where TDestination : unmanaged
	{
		IFixedContext<TDestination> result = this as IFixedContext<TDestination> ??
			new JNativeContext<TDestination>(this.Handler, this.Invalid, this.Offset);
		Int32 offset = this.Offset + result.Values.Length * NativeUtilities.SizeOf<TDestination>();
		residual = new JNativeContext<Byte>(this.Handler, this.Invalid, offset);
		return result;
	}
	/// <inheritdoc/>
	public new IFixedContext<TDestination> Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		where TDestination : unmanaged
	{
		IFixedContext<TDestination> result = this as IFixedContext<TDestination> ??
			new JNativeContext<TDestination>(this.Handler, this.Invalid, this.Offset);
		Int32 offset = this.Offset + result.Values.Length * NativeUtilities.SizeOf<TDestination>();
		residual = new JNativeContext<Byte>(this.Handler, this.Invalid, offset);
		return result;
	}
}