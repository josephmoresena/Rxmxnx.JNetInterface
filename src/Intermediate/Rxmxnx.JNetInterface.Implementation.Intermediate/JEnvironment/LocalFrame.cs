using JLocalObject = Rxmxnx.JNetInterface.Native.JLocalObject;

namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// Local reference frame
	/// </summary>
	private sealed record LocalFrame : LocalCache, IDisposable
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
			set => throw new InvalidOperationException("Current stack frame is fixed.");
		}

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
				this._result = default; //Result is not contained in the local frame.
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
			if (this.Contains(localRef))
				this._references.Remove(localRef); // localRef is owned by current frame.
			base.Remove(localRef);
		}

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
			this._references.Clear();
			this.ClearCache(env, false, this._result?.LocalReference ?? default);
			env.DeleteLocalFrame(this.Id, this._result);
		}
	}
}