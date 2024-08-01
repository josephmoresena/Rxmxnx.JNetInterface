namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a primitive memory block.
/// </summary>
public abstract class JPrimitiveMemory : JNativeMemory, IFixedContext<Byte>
{
	/// <inheritdoc/>
	private protected JPrimitiveMemory(INativeMemoryAdapter adapter) : base(adapter, false) { }
	/// <inheritdoc/>
	private protected JPrimitiveMemory(JNativeMemory mem) : base(mem) { }

	IFixedContext<Byte> IFixedMemory.AsBinaryContext() => this;
	Span<Byte> IFixedMemory.Bytes => this.GetBinaryContext().Bytes;
	Span<Byte> IFixedMemory<Byte>.Values => this.GetBinaryContext().Values;
	IFixedContext<TDestination> IFixedContext<Byte>.Transformation<TDestination>(out IFixedMemory residual)
		=> this.GetBinaryContext().Transformation<TDestination>(out residual);
	IFixedContext<TDestination> IFixedContext<Byte>.Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this.GetBinaryContext().Transformation<TDestination>(out residual);

	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{Byte}"/> instance</returns>
	internal new IFixedContext<Byte> GetBinaryContext() => (IFixedContext<Byte>)base.GetBinaryContext();
}

public sealed partial class JPrimitiveMemory<TPrimitive>
{
	/// <inheritdoc/>
	internal JPrimitiveMemory(INativeMemoryAdapter adapter) : base(adapter)
		=> this._context = this.GetBinaryContext().Transformation<TPrimitive>(out IFixedMemory _);
}