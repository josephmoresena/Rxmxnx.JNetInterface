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

		/// <inheritdoc/>
		public override JClassObject ClassObject { get; } = default!;
		/// <inheritdoc/>
		public override JClassObject ThrowableObject { get; } = default!;
		/// <inheritdoc/>
		public override JClassObject StackTraceElementObject { get; } = default!;
		/// <summary>
		/// Class for Java <c>void</c> type.
		/// </summary>
		protected JClassObject VoidPrimitive { get; } = default!;
		/// <summary>
		/// Class for <see cref="JBoolean"/>.
		/// </summary>
		protected JClassObject BooleanPrimitive { get; } = default!;
		/// <summary>
		/// Class for <see cref="JByte"/>.
		/// </summary>
		protected JClassObject BytePrimitive { get; } = default!;
		/// <summary>
		/// Class for <see cref="JChar"/>.
		/// </summary>
		protected JClassObject CharPrimitive { get; } = default!;
		/// <summary>
		/// Class for <see cref="JDouble"/>.
		/// </summary>
		protected JClassObject DoublePrimitive { get; } = default!;
		/// <summary>
		/// Class for <see cref="JFloat"/>.
		/// </summary>
		protected JClassObject FloatPrimitive { get; } = default!;
		/// <summary>
		/// Class for <see cref="JInt"/>.
		/// </summary>
		protected JClassObject IntPrimitive { get; } = default!;
		/// <summary>
		/// Class for <see cref="JLong"/>.
		/// </summary>
		protected JClassObject LongPrimitive { get; } = default!;
		/// <summary>
		/// Class for <see cref="JShort"/>.
		/// </summary>
		protected JClassObject ShortPrimitive { get; } = default!;

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

			JClassObject jClass = new(env); // java.lang.Class<?> class.

			this.ClassObject = jClass;
			this.ThrowableObject = new(jClass, MetadataHelper.GetExactMetadata<JThrowableObject>());
			this.StackTraceElementObject = new(jClass, MetadataHelper.GetExactMetadata<JStackTraceElementObject>());

			this.VoidPrimitive = new(jClass, JPrimitiveTypeMetadata.VoidMetadata);
			this.BooleanPrimitive = new(jClass, MetadataHelper.GetExactMetadata<JBoolean>());
			this.BytePrimitive = new(jClass, MetadataHelper.GetExactMetadata<JByte>());
			this.CharPrimitive = new(jClass, MetadataHelper.GetExactMetadata<JChar>());
			this.DoublePrimitive = new(jClass, MetadataHelper.GetExactMetadata<JDouble>());
			this.FloatPrimitive = new(jClass, MetadataHelper.GetExactMetadata<JFloat>());
			this.IntPrimitive = new(jClass, MetadataHelper.GetExactMetadata<JInt>());
			this.LongPrimitive = new(jClass, MetadataHelper.GetExactMetadata<JLong>());
			this.ShortPrimitive = new(jClass, MetadataHelper.GetExactMetadata<JShort>());
		}

		/// <summary>
		/// Indicates whether <paramref name="jGlobal"/> is a main global class.
		/// </summary>
		/// <param name="jGlobal">A <see cref="JGlobal"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="jGlobal"/> is main global class; otherwise;
		/// <see langword="false"/>.
		/// </returns>
		public static Boolean IsMainGlobal(JGlobal? jGlobal)
		{
			if (jGlobal?.ObjectMetadata is not ClassObjectMetadata classMetadata) return false;
			JVirtualMachine vm = (jGlobal.VirtualMachine as JVirtualMachine)!;
			return vm.IsMainGlobal(classMetadata.Hash, jGlobal);
		}
	}
}