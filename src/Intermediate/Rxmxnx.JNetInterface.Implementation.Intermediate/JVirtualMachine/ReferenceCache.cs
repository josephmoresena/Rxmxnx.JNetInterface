namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Reference cache implementation.
	/// </summary>
	private sealed record ReferenceCache : ReferenceHelperCache<JVirtualMachine, JVirtualMachineRef, Boolean>
	{
		/// <summary>
		/// Current <see cref="ReferenceCache"/> instance.
		/// </summary>
		public static readonly ReferenceCache Instance = new();

		/// <inheritdoc/>
		protected override JVirtualMachine Create(JVirtualMachineRef reference, Boolean isDestroyable)
			=> !isDestroyable ? new(reference) : new Invoked(reference);
		/// <inheritdoc/>
		protected override void Destroy(JVirtualMachine instance)
		{
			if (instance is IDisposable disposable)
				disposable.Dispose();
		}

		public override JVirtualMachine Get(JVirtualMachineRef reference, out Boolean isNew, Boolean arg = false)
		{
			JVirtualMachine result = base.Get(reference, out isNew, arg);
			if (isNew) result.InitializeClasses();
			return result;
		}
	}
}