namespace Rxmxnx.JNetInterface.Native;

public partial class JNativeCallEntry
{
	/// <summary>
	/// Java native method entry.
	/// </summary>
	private sealed class GenericEntry<T> : JNativeCallEntry where T : Delegate
	{
		/// <inheritdoc/>
		public override Delegate? Delegate { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="del">Delegate.</param>
		/// <param name="definition">Definition.</param>
		public GenericEntry(T del, JCallDefinition definition) : base(del.GetUnsafeIntPtr(), definition)
			=> this.Delegate = del;
	}
}