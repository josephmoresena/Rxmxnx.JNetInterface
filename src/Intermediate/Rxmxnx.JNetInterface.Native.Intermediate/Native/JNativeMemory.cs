namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a native memory block.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.InternalInheritanceJustification)]
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
	/// Indicates whether current sequence is a copy.
	/// </summary>
	public Boolean Copy => this._adapter.Copy;
	/// <summary>
	/// Indicates whether current sequence is critical.
	/// </summary>
	public Boolean Critical => this._adapter.Critical;
	/// <inheritdoc/>
	public IntPtr Pointer => this._context.Pointer;

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
	/// <param name="adapter"><see cref="INativeMemoryAdapter"/> instance.</param>
	private protected JNativeMemory(INativeMemoryAdapter adapter)
	{
		this._adapter = adapter;
		this._context = adapter.GetReadOnlyContext(this);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="memory"><see cref="JNativeMemory"/> instance.</param>
	private protected JNativeMemory(JNativeMemory memory)
	{
		this._adapter = memory._adapter;
		this._context = memory._context;
		this._disposed = memory._disposed;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="adapter"><see cref="INativeMemoryAdapter"/> instance.</param>
	/// <param name="isReadOnly">Indicates current memory block is read-only.</param>
	private protected JNativeMemory(INativeMemoryAdapter adapter, Boolean isReadOnly)
	{
		this._adapter = adapter;
		this._context = isReadOnly ? adapter.GetReadOnlyContext(this) : adapter.GetContext(this);
	}

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	~JNativeMemory() { this.ReleaseUnmanagedResources(); }

	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{Byte}"/> instance</returns>
	internal IReadOnlyFixedContext<Byte> GetBinaryContext() => this._context;
}

/// <summary>
/// This class represents a native memory block.
/// </summary>
/// <typeparam name="TValue">Value type in memory block.</typeparam>
public sealed class JNativeMemory<TValue> : JNativeMemory, IReadOnlyFixedContext<TValue> where TValue : unmanaged
{
	/// <summary>
	/// Internal memory context.
	/// </summary>
	private readonly IReadOnlyFixedContext<TValue> _context;

	/// <inheritdoc/>
	public ReadOnlySpan<TValue> Values => this._context.Values;

	/// <inheritdoc/>
	internal JNativeMemory(INativeMemoryAdapter adapter) : base(adapter)
		=> this._context = this.Memory.AsBinaryContext().Transformation<TValue>(out _);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="mem">A <see cref="JNativeMemory"/> instance.</param>
	/// <param name="context">A <see cref="IReadOnlyFixedContext{TPrimitive}"/> instance.</param>
	internal JNativeMemory(JNativeMemory mem, IReadOnlyFixedContext<TValue> context) : base(mem)
		=> this._context = context;

	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<TValue>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);

	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{TValue}"/> instance</returns>
	internal IReadOnlyFixedContext<TValue> GetContext() => this._context;
}