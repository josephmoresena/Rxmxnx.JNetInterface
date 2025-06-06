namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JNativeMemory
{
	IReadOnlyFixedContext<Byte> IReadOnlyFixedMemory.AsBinaryContext() => this;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	IReadOnlyFixedContext<Object> IReadOnlyFixedMemory.AsObjectContext()
	{
		IMessageResource resource = IMessageResource.GetInstance();
		throw new NotImplementedException(resource.UnmanagedMemoryContext);
	}
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	Boolean IReadOnlyFixedMemory.IsNullOrEmpty => this._disposed.Value || this._context.Bytes.IsEmpty;
	ReadOnlySpan<Byte> IReadOnlyFixedMemory.Bytes => this._context.Bytes;
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	ReadOnlySpan<Object> IReadOnlyFixedMemory.Objects => ReadOnlySpan<Object>.Empty;
	ReadOnlySpan<Byte> IReadOnlyFixedMemory<Byte>.Values => this._context.Bytes;
	IReadOnlyFixedContext<TDestination> IReadOnlyFixedContext<Byte>.
		Transformation<TDestination>(out IReadOnlyFixedMemory residual)
		=> this._context.Transformation<TDestination>(out residual);
}