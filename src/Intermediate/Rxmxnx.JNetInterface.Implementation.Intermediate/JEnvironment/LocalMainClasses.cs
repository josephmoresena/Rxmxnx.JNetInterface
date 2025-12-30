// ReSharper disable once ConvertToAutoPropertyWhenPossible

namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private abstract class LocalMainClasses : MainClasses<JClassObject>
	{
		/// <summary>
		/// JNI version.
		/// </summary>
		private readonly Int32 _jniVersion;

		/// <inheritdoc cref="JEnvironment.Reference"/>
		public readonly JEnvironmentRef Reference;
		/// <summary>
		/// Managed thread.
		/// </summary>
		public readonly Thread Thread = Thread.CurrentThread;
		/// <inheritdoc cref="JEnvironment.VirtualMachine"/>
		public readonly JVirtualMachine VirtualMachine;

		/// <inheritdoc cref="IEnvironment.Version"/>
		/// <remarks>
		/// This field must be a property in order to be substituted through <c>ILLink.Substitutions</c>.
		/// </remarks>
		// ReSharper disable once ConvertToAutoPropertyWhenPossible
		public Int32 Version => this._jniVersion;

		/// <inheritdoc/>
		public override JClassObject ClassObject { get; } = default!;
		/// <inheritdoc/>
		public override JClassObject ThrowableObject { get; } = default!;
		/// <inheritdoc/>
		public override JClassObject StackTraceElementObject { get; } = default!;
		/// <inheritdoc/>
		public override JClassObject SystemObject { get; } = default!;
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
			this._jniVersion = LocalMainClasses.GetVersion(envRef);

			if (!this.InstantiationCheck()) return; // Avoid class instantiation.

			JClassObject jClass = new(env); // java.lang.Class<?> class.

			this.ClassObject = jClass;
			this.ThrowableObject = new(jClass, MetadataHelper.GetExactMetadata<JThrowableObject>());
			this.SystemObject = new(jClass, MetadataHelper.GetExactMetadata<JSystemObject>());
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
		/// JNI runtime version.
		/// </summary>
		/// <returns>The current JNI runtime version.</returns>
#if !PACKAGE
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
		                 Justification = CommonConstants.NoMethodOverloadingJustification)]
#endif
		public Int32 GetInterfaceVersion() => this._jniVersion;

		/// <summary>
		/// Performs the instantiation checks.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the instantiation checks are passed; otherwise, <see langword="false"/>.
		/// </returns>
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		private Boolean InstantiationCheck()
		{
			IMessageResource resource = IMessageResource.GetInstance();
			if (JVirtualMachine.IsFixedAndroid)
			{
				// If fixed on Android, JNI should be 0x00010006
				if (this._jniVersion != (Int32)JRuntimeVersion.J6)
					throw new InvalidOperationException(resource.AndroidRuntimeRequired);
			}
			else if (JVirtualMachine.IsFixedRuntimeVersion && this.Version > this._jniVersion)
			{
				// If fixed runtime version, JNI version should be compatible with the fixed JNI version.
				throw new InvalidOperationException(resource.InvalidInterfaceVersion(this._jniVersion, this.Version));
			}
			return this._jniVersion >= NativeInterface.RequiredVersion; // Avoid instantiation if unsupported version.
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

		/// <summary>
		/// Retrieves JNI version for <paramref name="envRef"/>.
		/// </summary>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> instance.</param>
		/// <returns>JNI version for <paramref name="envRef"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe Int32 GetVersion(JEnvironmentRef envRef)
		{
			ref readonly NativeInterface nativeInterface = ref *(NativeInterface*)envRef.InterfacePointer;
			return nativeInterface.GetVersion(envRef);
		}
	}
}