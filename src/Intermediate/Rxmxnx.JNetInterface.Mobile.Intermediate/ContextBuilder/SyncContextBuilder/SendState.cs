namespace Rxmxnx.JNetInterface.ContextBuilder;

public ref partial struct SyncContextBuilder
{
#pragma warning disable CS8500
	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Send(SendOrPostCallback, Object)"/>
	/// </summary>
	private abstract unsafe class SendState
	{
		/// <summary>
		/// A <see cref="SyncContextBuilder"/> unmanaged pointer.
		/// </summary>
		private readonly SyncContextBuilder* _builderPtr;

		public Exception? Exception { get; private set; }

		/// <summary>
		/// State pointer.
		/// </summary>
		protected void* InterfacePointer { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="builderPtr">A <see cref="SyncContextBuilder"/> pointer.</param>
		protected SendState(SyncContextBuilder* builderPtr) => this._builderPtr = builderPtr;

		/// <summary>
		/// Executes the current SEND logic using the specified context builder.
		/// </summary>
		protected abstract void Invoke(SyncContextBuilder builder);

		/// <summary>
		/// Processes the given state object if it is of type <see cref="SendState"/>.
		/// Executes the corresponding logic defined in the SEND state.
		/// </summary>
		/// <param name="obj">An object that represents the SEND state.</param>
		public static void Send(Object? obj)
		{
			if (obj is not SendState state) return;
			try
			{
				state.Invoke(state._builderPtr[0]);
			}
			catch (Exception ex)
			{
				state.Exception = ex;
			}
		}
	}

	/// <summary>
	/// Object state for <see cref="SynchronizationContext.Send(SendOrPostCallback, Object)"/>
	/// </summary>
	/// <typeparam name="TResult">The type of the result of the operation.</typeparam>
	private abstract unsafe class SendState<TResult> : SendState
	{
		/// <summary>
		/// Represents the result of the operation executed within
		/// <see cref="SynchronizationContext.Send(SendOrPostCallback, Object)"/>.
		/// </summary>
		public TResult? Result { get; protected set; }

		/// <inheritdoc/>
		protected SendState(SyncContextBuilder* builderPtr) : base(builderPtr) { }
	}
#pragma warning restore CS8500
}