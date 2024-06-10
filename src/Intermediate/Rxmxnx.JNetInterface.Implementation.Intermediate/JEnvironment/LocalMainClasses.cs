namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private abstract class LocalMainClasses : MainClasses<JClassObject>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
		protected LocalMainClasses(IEnvironment env)
		{
			this.ClassObject = new(env);
			this.ThrowableObject = new(this.ClassObject, MetadataHelper.GetExactMetadata<JThrowableObject>());
			this.StackTraceElementObject =
				new(this.ClassObject, MetadataHelper.GetExactMetadata<JStackTraceElementObject>());

			this.VoidPrimitive = new(this.ClassObject, JPrimitiveTypeMetadata.VoidMetadata);
			this.BooleanPrimitive = new(this.ClassObject, MetadataHelper.GetExactMetadata<JBoolean>());
			this.BytePrimitive = new(this.ClassObject, MetadataHelper.GetExactMetadata<JByte>());
			this.CharPrimitive = new(this.ClassObject, MetadataHelper.GetExactMetadata<JChar>());
			this.DoublePrimitive = new(this.ClassObject, MetadataHelper.GetExactMetadata<JDouble>());
			this.FloatPrimitive = new(this.ClassObject, MetadataHelper.GetExactMetadata<JFloat>());
			this.IntPrimitive = new(this.ClassObject, MetadataHelper.GetExactMetadata<JInt>());
			this.LongPrimitive = new(this.ClassObject, MetadataHelper.GetExactMetadata<JLong>());
			this.ShortPrimitive = new(this.ClassObject, MetadataHelper.GetExactMetadata<JShort>());
		}

		/// <summary>
		/// Indicates whether <paramref name="jGlobal"/> is a main global class.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="jGlobal"/> is main global class; otherwise;
		/// <see langword="false"/>.
		/// </returns>
		protected Boolean IsMainGlobal(JGlobal? jGlobal)
		{
			if (jGlobal is null) return false;
			JVirtualMachine vm = (jGlobal.VirtualMachine as JVirtualMachine)!;
			return Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.ClassObject)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.ThrowableObject)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.StackTraceElementObject)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.BooleanPrimitive)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.BytePrimitive)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.CharPrimitive)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.DoublePrimitive)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.FloatPrimitive)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.IntPrimitive)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.LongPrimitive)) ||
				Object.ReferenceEquals(jGlobal, vm.LoadGlobal(this.ShortPrimitive));
		}
	}
}