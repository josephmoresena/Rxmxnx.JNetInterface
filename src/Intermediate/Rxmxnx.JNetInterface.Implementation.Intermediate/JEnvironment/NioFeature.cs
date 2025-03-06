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
	private sealed partial class EnvironmentCache : INioFeature
	{
		public unsafe TDirectBuffer NewDirectByteBuffer(IFixedMemory.IDisposable memory)
		{
			JClassObject jClass = this.GetClass<JDirectByteBufferObject>();
			ref readonly NativeInterface4 nativeInterface =
				ref this.GetNativeInterface<NativeInterface4>(NativeInterface4.NewDirectByteBufferInfo);
			JObjectLocalRef localRef =
				nativeInterface.NewDirectByteBuffer(this.Reference, memory.Pointer, memory.Bytes.Length);
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
#if NET9_0_OR_GREATER
	where TState : allows ref struct
#endif
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
			Func<TBuffer, TState, TResult> func)
#if NET9_0_OR_GREATER
	where TState : allows ref struct
#endif
			where TBuffer : TDirectBuffer
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
			ref readonly NativeInterface4 nativeInterface =
				ref this.GetNativeInterface<NativeInterface4>(NativeInterface4.GetDirectBufferAddressInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(buffer);
			IntPtr result = nativeInterface.GetDirectBufferAddress(this.Reference, localRef);
			this.CheckJniError();
			return result;
		}
		public unsafe Int64 GetDirectCapacity(JBufferObject buffer)
		{
			ImplementationValidationUtilities.ThrowIfProxy(buffer);
			ref readonly NativeInterface4 nativeInterface =
				ref this.GetNativeInterface<NativeInterface4>(NativeInterface4.GetDirectBufferAddressInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectLocalRef localRef = jniTransaction.Add(buffer);
			Int64 result = nativeInterface.GetDirectBufferCapacity(this.Reference, localRef);
			this.CheckJniError();
			return result;
		}
	}
}