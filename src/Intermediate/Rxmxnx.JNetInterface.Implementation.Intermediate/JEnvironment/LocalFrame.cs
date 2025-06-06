namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// Local reference frame
	/// </summary>
	private sealed class LocalFrame : LocalCache, IDisposable
	{
		/// <summary>
		/// A <see cref="JEnvironment"/> instance.
		/// </summary>
		private readonly JEnvironment _env;
		/// <summary>
		/// Internal reference queue.
		/// </summary>
		private readonly List<JObjectLocalRef> _references;

		/// <summary>
		/// Local frame result.
		/// </summary>
		private JLocalObject? _result;

		/// <inheritdoc/>
		public override ObjectLifetime this[JObjectLocalRef localRef]
		{
			set
			{
				if (!this.IsRegistered(localRef))
				{
					this.ValidateQueue();
					this._references.Add(localRef);
				}
				base[localRef] = value;
			}
		}
		/// <inheritdoc/>
		public override Int32? Capacity
		{
			get => base.Capacity;
			set => throw new InvalidOperationException(IMessageResource.GetInstance().StackTraceFixed);
		}
		/// <inheritdoc/>
		public override String Name => "local";

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="capacity">Current capacity.</param>
		public LocalFrame(JEnvironment env, Int32 capacity) : base(env._cache.GetLocalCache())
		{
			env.CreateLocalFrame(capacity);
			env._cache.CheckJniError();

			base.Capacity = capacity;
			this._env = env;
			this._references = new(capacity);

			this._env.SetObjectCache(this);
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			if (this._result is not null && !this.Contains(this._result.LocalReference))
				this._result = default; // Result is not contained in the local frame.
			this.FinalizeFrame(this._env);
		}
		/// <summary>
		/// Sets result for local frame.
		/// </summary>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="result">A <typeparamref name="TResult"/> result.</param>
		public void SetResult<TResult>(TResult result)
		{
			JLocalObject? jObject = result as JLocalObject ?? ILocalViewObject.GetObject(result as ILocalViewObject);
			if (jObject is null) return;
			this._result = jObject;
		}

		/// <inheritdoc/>
		public override void Remove(JObjectLocalRef localRef)
		{
			if (this.Contains(localRef)) // localRef is owned by current frame.
			{
				this._references.Remove(localRef);
				if (this._result?.LocalReference == localRef) // Result is removed.
					this._result = default;
			}
			base.Remove(localRef);
		}
		/// <inheritdoc/>
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		public override Boolean IsFromLocalFrame(JObjectLocalRef localRef)
			=> this.Contains(localRef) || base.IsFromLocalFrame(localRef);

		/// <summary>
		/// Validates current queue.
		/// </summary>
		private void ValidateQueue()
		{
			if (this._references.Count < this.Capacity) return;
			JObjectLocalRef localRef = this._references[0];
			this._references.RemoveAt(0);
			base.Remove(localRef);
		}
		/// <summary>
		/// Finalizes current frame.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		private void FinalizeFrame(JEnvironment? env)
		{
			if (env is null) return;
			JLocalObject? result = this._result;
			this.ClearCache(env, false, result?.LocalReference ?? default);
			env.DeleteLocalFrame(this, result);
		}
	}
}