namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Reference cache implementation.
	/// </summary>
	private sealed record ReferenceCache : ReferenceHelperCache<JVirtualMachine, JVirtualMachineRef>
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
	}
}