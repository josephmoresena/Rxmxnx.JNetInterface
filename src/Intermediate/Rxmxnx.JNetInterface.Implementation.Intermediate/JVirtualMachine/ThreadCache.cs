namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// IEnvironment cache implementation.
	/// </summary>
	private sealed class ThreadCache : ReferenceHelperCache<JEnvironment, JEnvironmentRef, ThreadCreationArgs?>
	{
		/// <summary>
		/// A <see cref="JVirtualMachine"/> instance.
		/// </summary>
		private readonly JVirtualMachine _vm;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		public ThreadCache(JVirtualMachine vm) => this._vm = vm;

		/// <inheritdoc/>
		public override JEnvironment Get(JEnvironmentRef reference, out Boolean isNew,
			ThreadCreationArgs? arg = default)
		{
			JEnvironment result = base.Get(reference, out isNew, arg);
			JTrace.EnvironmentLoad(reference, isNew);
			return result;
		}

		/// <inheritdoc/>
		protected override JEnvironment Create(JEnvironmentRef reference, ThreadCreationArgs? args)
			=> !args.HasValue ?
				new JEnvironment(this._vm, reference) :
				new JEnvironment.JThread(this._vm, reference, args.Value);
		/// <inheritdoc/>
		protected override void Destroy(JEnvironment instance)
		{
			if (instance is IDisposable disposable)
				disposable.Dispose();
		}
	}
}