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
		/// Registers current instance in <paramref name="cache"/>.
		/// </summary>
		/// <param name="cache">A <see cref="JEnvironmentCache"/> instance.</param>
		/// <returns>Current registered instance.</returns>
		public LocalMainClasses Register(JEnvironmentCache cache)
		{
			cache.Register(this.ClassObject);
			cache.Register(this.ThrowableObject);
			cache.Register(this.StackTraceElementObject);

			cache.Register(this.BooleanPrimitive);
			cache.Register(this.BytePrimitive);
			cache.Register(this.CharPrimitive);
			cache.Register(this.DoublePrimitive);
			cache.Register(this.FloatPrimitive);
			cache.Register(this.IntPrimitive);
			cache.Register(this.LongPrimitive);
			cache.Register(this.ShortPrimitive);

			return this;
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