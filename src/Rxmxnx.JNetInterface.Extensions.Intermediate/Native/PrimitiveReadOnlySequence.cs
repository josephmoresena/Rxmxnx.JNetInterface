namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a value sequence.
/// </summary>
/// <typeparam name="TValue">Type of <see cref="ValueType"/> elements in sequence.</typeparam>
public abstract record PrimitiveReadOnlySequence<TValue> : IDisposable where TValue : unmanaged
{
	/// <summary>
	/// Indicates current instance is disposed.
	/// </summary>
	private readonly IMutableWrapper<Boolean> _disposed = IMutableWrapper<Boolean>.Create();
	/// <summary>
	/// Internal sequence handler.
	/// </summary>
	private readonly PrimitiveSequenceHandler _handler;
	/// <inheritdoc cref="PrimitiveReadOnlySequence{TValue}.VirtualMachine"/>
	private readonly IVirtualMachine _vm;

	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	public IVirtualMachine VirtualMachine => this._vm;
	/// <summary>
	/// Indicates whether current sequence is a copy.
	/// </summary>
	public Boolean Copy => this._handler.Critical;
	/// <summary>
	/// Indicates whether current sequence is critical.
	/// </summary>
	public Boolean Critical => this._handler.Critical;

	/// <summary>
	/// Release mode.
	/// </summary>
	internal JReleaseMode ReleaseMode { get; set; }
	/// <summary>
	/// Sequence handler.
	/// </summary>
	internal PrimitiveSequenceHandler Handler => this._handler;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="handler"><see cref="PrimitiveSequenceHandler"/> handler.</param>
	internal PrimitiveReadOnlySequence(IVirtualMachine vm, PrimitiveSequenceHandler handler)
	{
		this._vm = vm;
		this._handler = handler;
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		this.ReleaseUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	~PrimitiveReadOnlySequence() { this.ReleaseUnmanagedResources(); }

	/// <inheritdoc cref="IDisposable.Dispose"/>
	private void ReleaseUnmanagedResources()
	{
		if (this._disposed.Value) return;
		try
		{
			this._handler.Release(this._vm, this.ReleaseMode);
		}
		finally
		{
			this._disposed.Value = true;
		}
	}
}