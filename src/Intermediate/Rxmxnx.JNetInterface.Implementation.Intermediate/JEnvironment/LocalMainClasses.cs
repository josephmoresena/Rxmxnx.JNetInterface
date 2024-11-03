namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private abstract class LocalMainClasses : MainClasses<JClassObject>
	{
		/// <inheritdoc cref="JEnvironment.Reference"/>
		public readonly JEnvironmentRef Reference;
		/// <summary>
		/// Managed thread.
		/// </summary>
		public readonly Thread Thread = Thread.CurrentThread;
		/// <inheritdoc cref="IEnvironment.Version"/>
		public readonly Int32 Version;
		/// <inheritdoc cref="JEnvironment.VirtualMachine"/>
		public readonly JVirtualMachine VirtualMachine;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
		/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
		protected LocalMainClasses(JVirtualMachine vm, JEnvironmentRef envRef, IEnvironment env)
		{
			this.VirtualMachine = vm;
			this.Reference = envRef;
			this.Version = EnvironmentCache.GetVersion(envRef);

			if (this.Version < NativeInterface.RequiredVersion)
				return; // Avoid class instantiation if unsupported version.

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
			if (jGlobal?.ObjectMetadata is not ClassObjectMetadata classMetadata) return false;
			JVirtualMachine vm = (jGlobal.VirtualMachine as JVirtualMachine)!;
			return classMetadata.Hash switch
			{
				ClassNameHelper.ClassHash => LocalMainClasses.IsMainGlobal(jGlobal, vm, this.ClassObject),
				ClassNameHelper.ThrowableHash => LocalMainClasses.IsMainGlobal(jGlobal, vm, this.ThrowableObject),
				ClassNameHelper.StackTraceElementHash => LocalMainClasses.IsMainGlobal(
					jGlobal, vm, this.StackTraceElementObject),
				// Primitive classes.
				ClassNameHelper.VoidPrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.VoidPrimitive),
				ClassNameHelper.BooleanPrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.BooleanPrimitive),
				ClassNameHelper.BytePrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.BytePrimitive),
				ClassNameHelper.CharPrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.CharPrimitive),
				ClassNameHelper.DoublePrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.DoublePrimitive),
				ClassNameHelper.FloatPrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.FloatPrimitive),
				ClassNameHelper.IntPrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.IntPrimitive),
				ClassNameHelper.LongPrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.LongPrimitive),
				ClassNameHelper.ShortPrimitiveHash => JVirtualMachine.PrimitiveMainClassesEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.ShortPrimitive),
				// Only if feature enable wrapper class are main.
				ClassNameHelper.VoidObjectHash => JVirtualMachine.VoidObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.VoidObject),
				ClassNameHelper.BooleanObjectHash => JVirtualMachine.BooleanObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.BooleanObject),
				ClassNameHelper.ByteObjectHash => JVirtualMachine.ByteObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.ByteObject),
				ClassNameHelper.CharacterObjectHash => JVirtualMachine.CharacterObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.CharacterObject),
				ClassNameHelper.DoubleObjectHash => JVirtualMachine.DoubleObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.DoubleObject),
				ClassNameHelper.FloatObjectHash => JVirtualMachine.FloatObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.FloatObject),
				ClassNameHelper.IntegerObjectHash => JVirtualMachine.IntegerObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.IntegerObject),
				ClassNameHelper.LongObjectHash => JVirtualMachine.LongObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.LongObject),
				ClassNameHelper.ShortObjectHash => JVirtualMachine.ShortObjectMainClassEnabled &&
					LocalMainClasses.IsMainGlobal(jGlobal, vm, this.ShortObject),
				_ => false,
			};
		}

		/// <summary>
		/// Indicates whether <paramref name="jGlobal"/> is <paramref name="mainClass"/>.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="mainClass">Local <see cref="JClassObject"/> main instance.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="jGlobal"/> is main global class; otherwise;
		/// <see langword="false"/>.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Boolean IsMainGlobal(JGlobal jGlobal, JVirtualMachine vm, JClassObject mainClass)
			=> Object.ReferenceEquals(jGlobal, vm.LoadGlobal(mainClass));
	}
}