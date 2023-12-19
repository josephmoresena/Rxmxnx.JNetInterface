namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private class LocalMainClasses : MainClasses<JClassObject>
	{
		/// <summary>
		/// <see cref="JEnvironment"/> instance.
		/// </summary>
		public JEnvironment Environment { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
		public LocalMainClasses(JEnvironment env)
		{
			this.Environment = env;
			this.ClassObject = new(this.Environment, false);
			this.ThrowableObject = new(this.ClassObject, MetadataHelper.GetMetadata<JThrowableObject>());
			this.StackTraceElementObject =
				new(this.ClassObject, MetadataHelper.GetMetadata<JStackTraceElementObject>());
		}
		/// <summary>
		/// Indicates whether <paramref name="jGlobal"/> is a main global class.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="jGlobal"/> is main global class; otherwise;
		/// <see langword="false"/>.
		/// </returns>
		public Boolean IsMainGlobal(JGlobal? jGlobal)
		{
			if (jGlobal is null) return false;
			JVirtualMachine vm = (jGlobal.VirtualMachine as JVirtualMachine)!;
			return Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.ClassObject)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.ThrowableObject)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.StackTraceElementObject));
		}
	}
}