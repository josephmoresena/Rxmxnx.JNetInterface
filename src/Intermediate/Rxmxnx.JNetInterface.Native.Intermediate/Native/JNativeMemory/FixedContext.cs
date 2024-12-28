namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JNativeMemory
{
	IReadOnlyFixedContext<Byte> IReadOnlyFixedMemory.AsBinaryContext() => this;
	[ExcludeFromCodeCoverage]
	IReadOnlyFixedContext<Object> IReadOnlyFixedMemory.AsObjectContext() => throw new NotImplementedException();
	[ExcludeFromCodeCoverage]
	Boolean IReadOnlyFixedMemory.IsNullOrEmpty => this._disposed.Value || this._context.Bytes.IsEmpty;
	ReadOnlySpan<Byte> IReadOnlyFixedMemory.Bytes => this._context.Bytes;
	[ExcludeFromCodeCoverage]
	ReadOnlySpan<Object> IReadOnlyFixedMemory.Objects => ReadOnlySpan<Object>.Empty;
	ReadOnlySpan<Byte> IReadOnlyFixedMemory<Byte>.Values => this._context.Bytes;
	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<Byte>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);
}