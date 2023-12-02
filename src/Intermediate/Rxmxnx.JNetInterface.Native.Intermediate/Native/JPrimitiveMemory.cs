namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a primitive memory block.
/// </summary>
public abstract record JPrimitiveMemory : JNativeMemory, IFixedContext<Byte>
{
	/// <inheritdoc/>
	internal JPrimitiveMemory(IVirtualMachine vm, JNativeMemoryHandler handler) : base(vm, handler, false) { }
	/// <inheritdoc/>
	internal JPrimitiveMemory(JNativeMemory mem) : base(mem) { }

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

/// <summary>
/// This class represents a primitive memory block.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> in memory block.</typeparam>
public sealed record JPrimitiveMemory<TPrimitive> : JPrimitiveMemory, IFixedContext<TPrimitive>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Internal memory context.
	/// </summary>
	private readonly IFixedContext<TPrimitive> _context;
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
	internal JPrimitiveMemory(IVirtualMachine vm, JNativeMemoryHandler handler) : base(vm, handler)
		=> this._context = this.GetBinaryContext().Transformation<TPrimitive>(out IFixedMemory _);
	
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="mem">A <see cref="JNativeMemory"/> instance.</param>
	/// <param name="context">A <see cref="IFixedContext{TPrimitive}"/> instance.</param>
	private JPrimitiveMemory(JNativeMemory mem, IFixedContext<TPrimitive> context) : base(mem)
		=> this._context = context;

	/// <inheritdoc/>
	public Span<TPrimitive> Values => this._context.Values;

	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<TPrimitive>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);
	IFixedContext<TDestination> IFixedContext<TPrimitive>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);
	IFixedContext<TDestination> IFixedContext<TPrimitive>.Transformation<TDestination>(out IFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);
	ReadOnlySpan<TPrimitive> IReadOnlyFixedMemory<TPrimitive>.Values => this.Values;

	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JPrimitiveMemory{TPrimitive}"/> to
	/// <see cref="JNativeMemory{TPrimitive}"/>.
	/// </summary>
	/// <param name="jPrimitiveMemory">A <see cref="JPrimitiveMemory{TPrimitive}"/> instance.</param>
	/// <returns>A <see cref="JNativeMemory{TPrimitive}"/> instance.</returns>
	public static implicit operator JNativeMemory<TPrimitive>(JPrimitiveMemory<TPrimitive> jPrimitiveMemory)
		=> new(jPrimitiveMemory, jPrimitiveMemory._context);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JNativeMemory{TPrimitive}"/> to
	/// <see cref="JPrimitiveMemory{TPrimitive}"/>.
	/// </summary>
	/// <param name="jNativeMemory">A <see cref="JNativeMemory{TPrimitive}"/> instance.</param>
	/// <returns>A <see cref="JPrimitiveMemory{TPrimitive}"/> instance.</returns>
	public static explicit operator JPrimitiveMemory<TPrimitive>(JNativeMemory<TPrimitive> jNativeMemory)
		=> new(jNativeMemory, (IFixedContext<TPrimitive>)jNativeMemory.GetContext());
}