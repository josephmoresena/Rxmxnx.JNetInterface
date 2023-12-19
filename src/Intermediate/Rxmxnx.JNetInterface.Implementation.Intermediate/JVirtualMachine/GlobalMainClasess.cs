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
		/// A <see cref="JVirtualMachine"/> instance.
		/// </summary>
		public JVirtualMachine VirtualMachine { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm"></param>
		public GlobalMainClasses(JVirtualMachine vm)
		{
			this.VirtualMachine = vm;
			this.ClassMetadata = JClassObjectMetadata.Create<JClassObject>();
			this.ClassObject = new(this.VirtualMachine, this.ClassMetadata, false, default);
			this.ThrowableMetadata = JClassObjectMetadata.Create<JThrowableObject>();
			this.ThrowableObject = new(this.VirtualMachine, this.ThrowableMetadata, false, default);
			this.StackTraceElementMetadata = JClassObjectMetadata.Create<JStackTraceElementObject>();
			this.StackTraceElementObject = new(this.VirtualMachine, this.StackTraceElementMetadata, false, default);
		}
	}
}