namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a native memory block.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.InternalInheritanceJustification)]
#endif
public abstract partial class JNativeMemory : IReadOnlyFixedContext<Byte>, IDisposable
{
	/// <summary>
	/// Internal memory adapter.
	/// </summary>
	private readonly INativeMemoryAdapter _adapter;
	/// <summary>
	/// Internal memory context.
	/// </summary>
	private readonly IReadOnlyFixedContext<Byte>.IDisposable _context;
	/// <inheritdoc cref="JNativeMemory.Disposed"/>
	private readonly IMutableWrapper<Boolean> _disposed = IMutableWrapper<Boolean>.Create();

	/// <summary>
	/// Indicates whether the current sequence is a copy.
	/// </summary>
	public Boolean Copy => this._adapter.Copy;
	/// <summary>
	/// Indicates whether the current sequence is critical.
	/// </summary>
	public Boolean Critical => this._adapter.Critical;
	/// <inheritdoc/>
	public IntPtr Pointer => this._context.Pointer;

	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	~JNativeMemory() { this.ReleaseUnmanagedResources(); }

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JNativeMemory"/> to
	/// <see cref="FixedPointerValue"/>.
	/// </summary>
	/// <param name="memory">A <see cref="JNativeMemory"/> to explicitly convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator FixedPointerValue(JNativeMemory memory)
	{
		// Using Rxmxnx.PInvoke.Extensions implementation, the first condition is always true.
		if (memory.TryCreateFixedValue(out FixedPointerValue result)) return result;
		if (memory._context is IFixedContext<Byte>.IDisposable d)
		{
			_ = d.ValuePointer.GetUnsafeFixedContext(d.Bytes.Length, out FixedContextValue<Byte> fc);
			return fc;
		}
		_ = memory._context.ValuePointer.GetUnsafeFixedContext(memory._context.Bytes.Length,
		                                                       out ReadOnlyFixedContextValue<Byte> rfc);
		return rfc;
	}
}

/// <summary>
/// This class represents a native memory block.
/// </summary>
/// <typeparam name="TValue">Value type in the memory block.</typeparam>
public sealed partial class JNativeMemory<TValue> : JNativeMemory, IReadOnlyFixedContext<TValue>
	where TValue : unmanaged
{
	/// <summary>
	/// Internal memory context.
	/// </summary>
	private readonly IReadOnlyFixedContext<TValue>? _context;

	/// <inheritdoc/>
	public ReadOnlySpan<TValue> Values
		=> this._context is not null ? this._context.Values : ((ReadOnlyFixedContextValue<TValue>)this).Values;

	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<TValue>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context?.Transformation<TDestination>(out residual) ??
			this.GetBinaryContext().Transformation<TDestination>(out residual);

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JNativeMemory{TPrimitive}"/> to
	/// <see cref="ReadOnlyFixedContextValue{TElement}"/>.
	/// </summary>
	/// <param name="jNativeMemory">A <see cref="JNativeMemory{TPrimitive}"/> to explicitly convert.</param>
	public static explicit operator ReadOnlyFixedContextValue<TValue>(JNativeMemory<TValue> jNativeMemory)
	{
		if (jNativeMemory.TryCreateFixedValue(out FixedPointerValue fp))
			return (ReadOnlyFixedContextValue<TValue>)fp;
		return ReadOnlyFixedContextValue<TValue>.CreateValue(jNativeMemory._context ?? jNativeMemory);
	}
}