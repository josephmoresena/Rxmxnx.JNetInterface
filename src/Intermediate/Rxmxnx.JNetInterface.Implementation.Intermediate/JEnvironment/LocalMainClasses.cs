namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private abstract record LocalMainClasses : MainClasses<JClassObject>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
		protected LocalMainClasses(IEnvironment env)
		{
			this.ClassObject = new(env);
			this.ThrowableObject = new(this.ClassObject, MetadataHelper.GetMetadata<JThrowableObject>());
			this.StackTraceElementObject =
				new(this.ClassObject, MetadataHelper.GetMetadata<JStackTraceElementObject>());

			this.VoidPrimitive = new(this.ClassObject, JPrimitiveTypeMetadata.VoidMetadata);
			this.BooleanPrimitive = new(this.ClassObject, MetadataHelper.GetMetadata<JBoolean>());
			this.BytePrimitive = new(this.ClassObject, MetadataHelper.GetMetadata<JByte>());
			this.CharPrimitive = new(this.ClassObject, MetadataHelper.GetMetadata<JChar>());
			this.DoublePrimitive = new(this.ClassObject, MetadataHelper.GetMetadata<JDouble>());
			this.FloatPrimitive = new(this.ClassObject, MetadataHelper.GetMetadata<JFloat>());
			this.IntPrimitive = new(this.ClassObject, MetadataHelper.GetMetadata<JInt>());
			this.LongPrimitive = new(this.ClassObject, MetadataHelper.GetMetadata<JLong>());
			this.ShortPrimitive = new(this.ClassObject, MetadataHelper.GetMetadata<JShort>());
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