namespace Rxmxnx.JNetInterface.Native;

public partial record JPrimitiveSequenceBase
{
	internal sealed record FixedContext<TValue> : ReadOnlyFixedContext<TValue>, IFixedContext<TValue>
		where TValue : unmanaged
	{
		private new IFixedContext<Byte> Memory => (IFixedContext<Byte>)base.Memory;

		internal FixedContext(JPrimitiveSequenceBase sequence, Int32 binaryOffset = 0) :
			base(sequence, binaryOffset) { }

		IntPtr IFixedPointer.Pointer => this.Sequence.Pointer + this.BinaryOffset;
		Span<Byte> IFixedMemory.Bytes => this.Memory.Bytes[this.BinaryOffset..];
		Span<TValue> IFixedMemory<TValue>.Values
			=> this.Sequence is IFixedContext<TValue> ctx ?
				ctx.Values[this.GetValueOffset()..] :
				this.Memory.Bytes[this.BinaryOffset..].AsValues<Byte, TValue>();

		IFixedContext<Byte> IFixedMemory.AsBinaryContext() => this.Memory;

		IFixedContext<TDestination> IFixedContext<TValue>.Transformation<TDestination>(out IFixedMemory residual)
		{
			if (this.BinaryOffset == 0)
				return this.Memory.Transformation<TDestination>(out residual);

			IFixedContext<TDestination> result = new FixedContext<TDestination>(this.Sequence, this.BinaryOffset);
			Int32 residualOffset = this.BinaryOffset + result.Values.Length * NativeUtilities.SizeOf<TValue>();
			residual = new FixedContext<Byte>(this.Sequence, residualOffset);
			return result;
		}
		IFixedContext<TDestination> IFixedContext<TValue>.Transformation<TDestination>(
			out IReadOnlyFixedMemory residual)
		{
			if (this.BinaryOffset == 0)
				return this.Memory.Transformation<TDestination>(out residual);

			IFixedContext<TDestination> result = new FixedContext<TDestination>(this.Sequence, this.BinaryOffset);
			Int32 residualOffset = this.BinaryOffset + result.Values.Length * NativeUtilities.SizeOf<TValue>();
			residual = new FixedContext<Byte>(this.Sequence, residualOffset);
			return result;
		}
	}
}