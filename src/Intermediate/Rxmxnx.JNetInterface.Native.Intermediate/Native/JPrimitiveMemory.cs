namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a primitive memory block.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> in memory block.</typeparam>
public sealed record JPrimitiveMemory<TPrimitive> : JNativeMemory<TPrimitive>, IFixedContext<Byte>,
	IFixedContext<TPrimitive> where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Memory release mode.
	/// </summary>
	public new JReleaseMode? ReleaseMode
	{
		get => !this.Critical ? base.ReleaseMode : default(JReleaseMode?);
		set
		{
			if (this.Critical || !value.HasValue)
				return;
			base.ReleaseMode = value.GetValueOrDefault();
		}
	}

	/// <inheritdoc/>
	internal JPrimitiveMemory(IVirtualMachine vm, JNativeMemoryHandler handler) : base(vm, handler, false) { }

	IFixedContext<Byte> IFixedMemory.AsBinaryContext() => this;
	Span<Byte> IFixedMemory.Bytes => this.GetBinaryContext().Bytes;
	Span<Byte> IFixedMemory<Byte>.Values => this.GetBinaryContext().Values;
	IFixedContext<TDestination> IFixedContext<Byte>.Transformation<TDestination>(out IFixedMemory residual)
		=> this.GetBinaryContext().Transformation<TDestination>(out residual);
	IFixedContext<TDestination> IFixedContext<Byte>.Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this.GetBinaryContext().Transformation<TDestination>(out residual);
	/// <inheritdoc/>
	public new Span<TPrimitive> Values => this.GetValueContext().Values;
	IFixedContext<TDestination> IFixedContext<TPrimitive>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this.GetValueContext().Transformation<TDestination>(out residual);
	IFixedContext<TDestination> IFixedContext<TPrimitive>.Transformation<TDestination>(out IFixedMemory residual)
		=> this.GetValueContext().Transformation<TDestination>(out residual);

	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{Byte}"/> instance</returns>
	private new JNativeContext<Byte> GetBinaryContext() => (JNativeContext<Byte>)base.GetBinaryContext();
	/// <summary>
	/// Retrieves the memory block context.
	/// </summary>
	/// <returns>A <see cref="IReadOnlyFixedContext{TValue}"/> instance</returns>
	private new JNativeContext<TPrimitive> GetValueContext() => (JNativeContext<TPrimitive>)base.GetValueContext();
}