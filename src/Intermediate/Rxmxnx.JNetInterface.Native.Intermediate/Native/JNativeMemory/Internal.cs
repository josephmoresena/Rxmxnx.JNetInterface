namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JNativeMemory
{
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

	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{Byte}"/> instance</returns>
	internal IReadOnlyFixedContext<Byte> GetBinaryContext() => this._context;
}

public sealed partial class JNativeMemory<TValue>
{
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

	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{TValue}"/> instance</returns>
	internal IReadOnlyFixedContext<TValue> GetContext() => this._context;
}