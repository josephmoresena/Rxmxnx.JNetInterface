namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a primitive values sequence base.
/// </summary>
public abstract record JPrimitiveSequence : JPrimitiveReadOnlySequence, IFixedContext<Byte>
{
	/// <inheritdoc/>
	internal override Boolean ReadOnly => false;

	/// <inheritdoc/>
	internal JPrimitiveSequence(JLocalObject source) : base(source) { }
	/// <inheritdoc/>
	internal JPrimitiveSequence(JGlobalBase source, Boolean disposeSource) : base(source, disposeSource) { }
	/// <inheritdoc/>
	public new Span<Byte> Bytes => this.Pointer.GetUnsafeSpan<Byte>(this.BinarySize);
	Span<Byte> IFixedMemory<Byte>.Values => this.Bytes;

	IFixedContext<Byte> IFixedMemory.AsBinaryContext() => this;

	/// <inheritdoc/>
	public IFixedContext<TDestination> Transformation<TDestination>(out IFixedMemory residual)
		where TDestination : unmanaged
	{
		IFixedContext<TDestination> result = this as IFixedContext<TDestination> ??
			new FixedContext<TDestination>(this);
		residual = new FixedContext<TDestination>(this, result.Bytes.Length);
		return result;
	}
	/// <inheritdoc/>
	public new IFixedContext<TDestination> Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		where TDestination : unmanaged
	{
		IFixedContext<TDestination> result = this.Transformation<TDestination>(out IFixedMemory residualTmp);
		residual = residualTmp;
		return result;
	}
}