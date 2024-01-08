namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : INioFeature
	{
		public JBufferObject NewDirectByteBuffer(IFixedMemory.IDisposable memory)
		{
			JClassObject jClass = this.GetClass<JDirectByteBufferObject>();
			NewDirectByteBufferDelegate newDirectByteBuffer = this.GetDelegate<NewDirectByteBufferDelegate>();
			JObjectLocalRef localRef = newDirectByteBuffer(this.Reference, memory.Pointer, memory.Bytes.Length);
			this.CheckJniError();
			return this.Register<JDirectByteBufferObject>(new(jClass, memory, localRef));
		}
		public void WithDirectByteBuffer<TBuffer>(Int32 capacity, Action<TBuffer> action) where TBuffer : JBufferObject
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				JEnvironmentCache.AllocToFixedContext(stackalloc Byte[capacity], this) :
				new Byte[capacity].AsMemory().GetFixedContext();
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			action(buffer);
		}
		public void WithDirectByteBuffer<TBuffer, TState>(Int32 capacity, TState state, Action<TBuffer, TState> action)
			where TBuffer : JBufferObject
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				JEnvironmentCache.AllocToFixedContext(stackalloc Byte[capacity], this) :
				new Byte[capacity].AsMemory().GetFixedContext();
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			action(buffer, state);
		}
		public TResult WithDirectByteBuffer<TBuffer, TResult>(Int32 capacity, Func<TBuffer, TResult> func)
			where TBuffer : JBufferObject
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				JEnvironmentCache.AllocToFixedContext(stackalloc Byte[capacity], this) :
				new Byte[capacity].AsMemory().GetFixedContext();
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			return func(buffer);
		}
		public TResult WithDirectByteBuffer<TBuffer, TState, TResult>(Int32 capacity, TState state,
			Func<TBuffer, TState, TResult> func) where TBuffer : JBufferObject
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				JEnvironmentCache.AllocToFixedContext(stackalloc Byte[capacity], this) :
				new Byte[capacity].AsMemory().GetFixedContext();
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			return func(buffer, state);
		}
		public IntPtr GetDirectAddress(JBufferObject buffer)
		{
			ValidationUtilities.ThrowIfDummy(buffer);
			GetDirectBufferAddressDelegate getDirectBufferAddress = this.GetDelegate<GetDirectBufferAddressDelegate>();
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			JObjectLocalRef localRef = jniTransaction.Add(buffer);
			IntPtr result = getDirectBufferAddress(this.Reference, localRef);
			this.CheckJniError();
			return result;
		}
		public Int64 GetDirectCapacity(JBufferObject buffer)
		{
			ValidationUtilities.ThrowIfDummy(buffer);
			GetDirectBufferCapacityDelegate getDirectBufferCapacity =
				this.GetDelegate<GetDirectBufferCapacityDelegate>();
			using JniTransaction jniTransaction = this.VirtualMachine.CreateTransaction();
			JObjectLocalRef localRef = jniTransaction.Add(buffer);
			Int64 result = getDirectBufferCapacity(this.Reference, localRef);
			this.CheckJniError();
			return result;
		}
	}
}