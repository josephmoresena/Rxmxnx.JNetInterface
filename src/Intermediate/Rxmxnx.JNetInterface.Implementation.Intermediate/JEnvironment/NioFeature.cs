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
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial record EnvironmentCache : INioFeature
	{
		public unsafe TDirectBuffer NewDirectByteBuffer(IFixedMemory.IDisposable memory)
		{
			JClassObject jClass = this.GetClass<JDirectByteBufferObject>();
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewDirectByteBufferInfo);
			JObjectLocalRef localRef =
				nativeInterface.NioFunctions.NewDirectByteBuffer(this.Reference, memory.Pointer, memory.Bytes.Length);
			this.CheckJniError();
			return this.Register<JDirectByteBufferObject>(new(jClass, memory, localRef));
		}
		public void WithDirectByteBuffer<TBuffer>(Int32 capacity, Action<TBuffer> action) where TBuffer : TDirectBuffer
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				this.GetStackContext(stackalloc Byte[capacity]) :
				EnvironmentCache.AllocHeapContext(capacity);
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			action(buffer);
		}
		public void WithDirectByteBuffer<TBuffer, TState>(Int32 capacity, TState state, Action<TBuffer, TState> action)
			where TBuffer : TDirectBuffer
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				this.GetStackContext(stackalloc Byte[capacity]) :
				EnvironmentCache.AllocHeapContext(capacity);
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			action(buffer, state);
		}
		public TResult WithDirectByteBuffer<TBuffer, TResult>(Int32 capacity, Func<TBuffer, TResult> func)
			where TBuffer : TDirectBuffer
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				this.GetStackContext(stackalloc Byte[capacity]) :
				EnvironmentCache.AllocHeapContext(capacity);
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			return func(buffer);
		}
		public TResult WithDirectByteBuffer<TBuffer, TState, TResult>(Int32 capacity, TState state,
			Func<TBuffer, TState, TResult> func) where TBuffer : TDirectBuffer
		{
			Boolean useStackAlloc = this.UseStackAlloc(capacity);
			using IFixedContext<Byte>.IDisposable memory = useStackAlloc ?
				this.GetStackContext(stackalloc Byte[capacity]) :
				EnvironmentCache.AllocHeapContext(capacity);
			using TBuffer buffer = (TBuffer)this.NewDirectByteBuffer(memory);
			return func(buffer, state);
		}
		public unsafe IntPtr GetDirectAddress(JBufferObject buffer)
		{
			ImplementationValidationUtilities.ThrowIfProxy(buffer);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetDirectBufferAddressInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(buffer);
			IntPtr result = nativeInterface.NioFunctions.GetDirectBufferAddress(this.Reference, localRef);
			this.CheckJniError();
			return result;
		}
		public unsafe Int64 GetDirectCapacity(JBufferObject buffer)
		{
			ImplementationValidationUtilities.ThrowIfProxy(buffer);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetDirectBufferAddressInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(buffer);
			Int64 result = nativeInterface.NioFunctions.GetDirectBufferCapacity(this.Reference, localRef);
			this.CheckJniError();
			return result;
		}
	}
}