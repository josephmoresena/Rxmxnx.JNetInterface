namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Stores initial global classes.
	/// </summary>
	private sealed class GlobalMainClasses : MainClasses<JGlobal>
	{
		/// <summary>
		/// Metadata for <see cref="JClassObject"/>.
		/// </summary>
		public JClassObjectMetadata ClassMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JThrowableObject"/>.
		/// </summary>
		public JClassObjectMetadata ThrowableMetadata { get; }
		/// <summary>
		/// Metadata for <see cref="JStackTraceElementObject"/>.
		/// </summary>
		public JClassObjectMetadata StackTraceElementMetadata { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm"></param>
		public GlobalMainClasses(IVirtualMachine vm)
		{
			this.ClassMetadata = JClassObjectMetadata.Create<JClassObject>();
			this.ClassObject = new(vm, this.ClassMetadata, false, default);
			this.ThrowableMetadata = JClassObjectMetadata.Create<JThrowableObject>();
			this.ThrowableObject = new(vm, this.ThrowableMetadata, false, default);
			this.StackTraceElementMetadata = JClassObjectMetadata.Create<JStackTraceElementObject>();
			this.StackTraceElementObject = new(vm, this.StackTraceElementMetadata, false, default);
		}
	}
}