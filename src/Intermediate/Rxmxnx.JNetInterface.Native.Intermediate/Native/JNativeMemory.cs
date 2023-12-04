namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a native memory block.
/// </summary>
public abstract record JNativeMemory : IReadOnlyFixedContext<Byte>, IDisposable
{
	/// <summary>
	/// Internal memory context.
	/// </summary>
	private readonly IReadOnlyFixedContext<Byte>.IDisposable _context;
	/// <inheritdoc cref="JNativeMemory.Disposed"/>
	private readonly IMutableWrapper<Boolean> _disposed = IMutableWrapper<Boolean>.Create();
	/// <summary>
	/// Internal memory handler.
	/// </summary>
	private readonly JNativeMemoryHandler _handler;
	/// <inheritdoc cref="JNativeMemory.VirtualMachine"/>
	private readonly IVirtualMachine _vm;

	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	public IVirtualMachine VirtualMachine => this._vm;
	/// <summary>
	/// Indicates whether current sequence is a copy.
	/// </summary>
	public Boolean Copy => this._handler.Copy;
	/// <summary>
	/// Indicates whether current sequence is critical.
	/// </summary>
	public Boolean Critical => this._handler.Critical;

	/// <summary>
	/// Internal fixed memory.
	/// </summary>
	internal IReadOnlyFixedMemory Memory => this._context;
	/// <summary>
	/// Release mode.
	/// </summary>
	internal JReleaseMode ReleaseMode { get; set; }
	/// <summary>
	/// Indicates current instance is disposed.
	/// </summary>
	internal IWrapper<Boolean> Disposed => this._disposed;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler"><see cref="JNativeMemoryHandler"/> handler.</param>
	internal JNativeMemory(IVirtualMachine vm, JNativeMemoryHandler handler)
	{
		this._vm = vm;
		this._handler = handler;
		this._context = handler.GetReadOnlyContext(this);
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler"><see cref="JNativeMemoryHandler"/> handler.</param>
	/// <param name="isReadOnly">Indicates current memory block is read-only.</param>
	internal JNativeMemory(IVirtualMachine vm, JNativeMemoryHandler handler, Boolean isReadOnly)
	{
		this._vm = vm;
		this._handler = handler;
		this._context = isReadOnly ? handler.GetReadOnlyContext(this) : handler.GetContext(this);
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		this.ReleaseUnmanagedResources();
		GC.SuppressFinalize(this);
	}
	/// <inheritdoc/>
	public IntPtr Pointer => this._context.Pointer;

	IReadOnlyFixedContext<Byte> IReadOnlyFixedMemory.AsBinaryContext() => this;
	ReadOnlySpan<Byte> IReadOnlyFixedMemory.Bytes => this._context.Bytes;
	ReadOnlySpan<Byte> IReadOnlyFixedMemory<Byte>.Values => this._context.Bytes;
	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<Byte>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);

	/// <inheritdoc/>
	~JNativeMemory() { this.ReleaseUnmanagedResources(); }

	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{Byte}"/> instance</returns>
	internal IReadOnlyFixedContext<Byte> GetBinaryContext() => this._context;

	/// <inheritdoc cref="IDisposable.Dispose"/>
	private void ReleaseUnmanagedResources()
	{
		if (this._disposed.Value) return;
		this._disposed.Value = true;
		this._handler.Release(this._vm, this.ReleaseMode);
		this._context.Dispose();
	}
}

/// <summary>
/// This class represents a native memory block.
/// </summary>
/// <typeparam name="TValue">Value type in memory block.</typeparam>
public sealed record JNativeMemory<TValue> : JNativeMemory, IReadOnlyFixedContext<TValue> where TValue : unmanaged
{
	/// <summary>
	/// Internal memory context.
	/// </summary>
	private readonly IReadOnlyFixedContext<TValue> _context;

	/// <inheritdoc/>
	internal JNativeMemory(IVirtualMachine vm, JNativeMemoryHandler handler) : base(vm, handler)
		=> this._context = this.Memory.AsBinaryContext().Transformation<TValue>(out _);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="mem">A <see cref="JNativeMemory"/> instance.</param>
	/// <param name="context">A <see cref="IReadOnlyFixedContext{TPrimitive}"/> instance.</param>
	internal JNativeMemory(JNativeMemory mem, IReadOnlyFixedContext<TValue> context) : base(mem)
		=> this._context = context;

	/// <inheritdoc/>
	public ReadOnlySpan<TValue> Values => this._context.Values;

	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<TValue>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);

	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{TValue}"/> instance</returns>
	internal IReadOnlyFixedContext<TValue> GetContext() => this._context;
}