namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
#pragma warning disable CS8500
	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Send(SendOrPostCallback, Object)"/>
	/// </summary>
	private sealed unsafe class
		SendObjectFunctionState<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult,
			TFunction>(
			SyncContextBuilder* builderPtr) : SendState<TResult>(builderPtr) where TResult : class, IJavaPeerable
		where TFunction : IAndroidNativeFunction<JReferenceObject?>
#if NET9_0_OR_GREATER
		, allows ref struct
#endif
	{
		/// <summary>
		/// Gets or initializes a pointer to the function instance.
		/// </summary>
		public TFunction* FunctionPointer
		{
			get => (TFunction*)this.InterfacePointer;
			init => this.InterfacePointer = value;
		}

		/// <inheritdoc/>
		protected override void Invoke(SyncContextBuilder builder)
		{
			ref TFunction f = ref Unsafe.AsRef<TFunction>(this.FunctionPointer);
			this.Result = builder.ObjectFunctionInvoke<TResult, TFunction>(ref f);
		}
	}
#pragma warning restore CS8500
}