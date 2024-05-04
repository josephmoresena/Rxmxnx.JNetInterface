namespace Rxmxnx.JNetInterface;

using TDirectBuffer =
#if !PACKAGE
	JBufferObject
#else
	JByteBufferObject
#endif
	;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache : INioFeature
	{
		public TDirectBuffer NewDirectByteBuffer(IFixedMemory.IDisposable memory)
		{
			JClassObject jClass = this.GetClass<JDirectByteBufferObject>();
			NewDirectByteBufferDelegate newDirectByteBuffer = this.GetDelegate<NewDirectByteBufferDelegate>();
			JObjectLocalRef localRef = newDirectByteBuffer(this.Reference, memory.Pointer, memory.Bytes.Length);
			this.CheckJniError();
			return this.Register<JDirectByteBufferObject>(new(jClass, memory, localRef));
		}
		public void WithDirectByteBuffer<TBuffer>(Int32 capacity, Action<TBuffer> action) where TBuffer : TDirectBuffer
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[capacity], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(capacity);
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			action(buffer);
		}
		public void WithDirectByteBuffer<TBuffer, TState>(Int32 capacity, TState state, Action<TBuffer, TState> action)
			where TBuffer : TDirectBuffer
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[capacity], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(capacity);
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			action(buffer, state);
		}
		public TResult WithDirectByteBuffer<TBuffer, TResult>(Int32 capacity, Func<TBuffer, TResult> func)
			where TBuffer : TDirectBuffer
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[capacity], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(capacity);
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			return func(buffer);
		}
		public TResult WithDirectByteBuffer<TBuffer, TState, TResult>(Int32 capacity, TState state,
			Func<TBuffer, TState, TResult> func) where TBuffer : TDirectBuffer
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[capacity], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(capacity);
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			return func(buffer, state);
		}
		public IntPtr GetDirectAddress(JBufferObject buffer)
		{
			ValidationUtilities.ThrowIfProxy(buffer);
			GetDirectBufferAddressDelegate getDirectBufferAddress = this.GetDelegate<GetDirectBufferAddressDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(buffer);
			IntPtr result = getDirectBufferAddress(this.Reference, localRef);
			this.CheckJniError();
			return result;
		}
		public Int64 GetDirectCapacity(JBufferObject buffer)
		{
			ValidationUtilities.ThrowIfProxy(buffer);
			GetDirectBufferCapacityDelegate getDirectBufferCapacity =
				this.GetDelegate<GetDirectBufferCapacityDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(buffer);
			Int64 result = getDirectBufferCapacity(this.Reference, localRef);
			this.CheckJniError();
			return result;
		}
	}
}