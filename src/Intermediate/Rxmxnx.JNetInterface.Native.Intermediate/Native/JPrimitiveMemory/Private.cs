namespace Rxmxnx.JNetInterface.Native;

public sealed partial class JPrimitiveMemory<TPrimitive>
{
	/// <summary>
	/// Internal memory context.
	/// </summary>
	private readonly IFixedContext<TPrimitive> _context;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="mem">A <see cref="JNativeMemory"/> instance.</param>
	/// <param name="context">A <see cref="IFixedContext{TPrimitive}"/> instance.</param>
	private JPrimitiveMemory(JNativeMemory mem, IFixedContext<TPrimitive> context) : base(mem)
		=> this._context = context;

	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<TPrimitive>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);
	IFixedContext<TDestination> IFixedContext<TPrimitive>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);
	IFixedContext<TDestination> IFixedContext<TPrimitive>.Transformation<TDestination>(out IFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);
	ReadOnlySpan<TPrimitive> IReadOnlyFixedMemory<TPrimitive>.Values => this.Values;
}