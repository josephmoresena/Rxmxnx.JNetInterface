namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
#pragma warning disable CS8500
	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Send(SendOrPostCallback, Object)"/>
	/// </summary>
	private sealed unsafe class SendActionState<TAction>(SyncContextBuilder* builderPtr)
		: SendState(builderPtr) where TAction : IAndroidNativeAction
#if NET9_0_OR_GREATER
		, allows ref struct
#endif
	{
		/// <summary>
		/// Gets or initializes a pointer to the action instance.
		/// </summary>
		public TAction* ActionPointer
		{
			get => (TAction*)this.InterfacePointer;
			init => this.InterfacePointer = value;
		}

		/// <inheritdoc/>
		protected override void Invoke(SyncContextBuilder builder)
			=> builder.ActionInvoke(ref Unsafe.AsRef<TAction>(this.ActionPointer));
	}
#pragma warning restore CS8500
}