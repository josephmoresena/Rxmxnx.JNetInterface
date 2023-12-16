namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
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
		/// Current <see cref="IEnvironment"/> instance.
		/// </summary>
		public IEnvironment Environment => this._env;
		/// <inheritdoc/>
		public override ObjectLifetime this[JObjectLocalRef localRef]
		{
			set
			{
				this.ValidateQueue();
				this._references.Add(localRef);
				base[localRef] = value;
			}
		}
		/// <inheritdoc/>
		public override Int32? Capacity
		{
			get => base.Capacity;
			set => throw new InvalidOperationException();
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="capacity">Current capacity.</param>
		public LocalFrame(JEnvironment env, Int32 capacity)
		{
			env.CreateLocalFrame(capacity);

			base.Capacity = capacity;
			this._env = env;
			this._references = new(capacity);
			env.SetObjectCache(this);
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			this._references.Clear();
			this.ClearCache(this._env, false);
		}

		/// <inheritdoc/>
		public override void Remove(JObjectLocalRef localRef)
		{
			this._references.Remove(localRef);
			base.Remove(localRef);
		}

		/// <summary>
		/// Validates current queue.
		/// </summary>
		private void ValidateQueue()
		{
			if (this._references.Count < this.Capacity) return;
			JObjectLocalRef localRef = this._references[0];
			this._references.Remove(localRef);
			base.Remove(localRef);
		}
	}
}