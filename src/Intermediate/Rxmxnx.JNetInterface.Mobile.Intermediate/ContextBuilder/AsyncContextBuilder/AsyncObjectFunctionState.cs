namespace Rxmxnx.JNetInterface.ContextBuilder;

public partial struct AsyncContextBuilder
{
	/// <summary>
	/// Represents the state of an asynchronous object function, providing mechanisms for tracking its progress,
	/// completion, and potential error handling.
	/// </summary>
#pragma warning disable CS8618
	private sealed class
		AsyncObjectFunctionState<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult,
			TFunction>(
			IVirtualMachineHost host,
			AsyncContextBuilder builder,
			Boolean useWeakGlobal,
			Boolean isDaemon = false) : AsyncState<TResult>(host, builder, useWeakGlobal, isDaemon)
		where TResult : class, IJavaPeerable where TFunction : IAndroidNativeFunction<JReferenceObject?>
	{
		/// <summary>
		/// Internal function to be executed.
		/// </summary>
		public TFunction Function { get; init; }

		/// <inheritdoc/>
		public override void Invoke(INativeThread env)
		{
			JLocalObject?[] objects = [];
			JavaInteropCache? interopCache = default;
			try
			{
				AndroidJniContext context = this.CreateContext(env, out interopCache, out objects);
				using InlineCache _ = new(env);
				this.Result = this.Function.Apply(context).ToJniObject<TResult>();
				if (this.Result?.PeerReference.Type is not JniObjectReferenceType.Local) return;

				// We need to handle the case where the result is a local reference.
				JniObjectReference currentRef = this.Result.PeerReference;
				JniObjectReference newRef =
					this.UseWeakGlobal ? currentRef.NewWeakGlobalRef() : currentRef.NewGlobalRef();
				this.Result.SetPeerReference(newRef);
				JniObjectReference.Dispose(ref currentRef);
			}
			catch (Exception e)
			{
				JTrace.UnhandledException(e, true);
				throw;
			}
			finally
			{
				AsyncState.Finalize(interopCache, objects);
			}
		}
	}
#pragma warning restore CS8618
}