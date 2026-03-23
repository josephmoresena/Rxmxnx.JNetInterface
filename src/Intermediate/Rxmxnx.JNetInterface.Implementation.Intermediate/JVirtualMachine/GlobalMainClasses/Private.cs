namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	private partial class GlobalMainClasses
	{
		/// <summary>
		/// Metadata for <see cref="JBoolean"/>.
		/// </summary>
		private readonly ClassObjectMetadata _booleanMetadata;
		/// <summary>
		/// Metadata for <see cref="JByte"/>.
		/// </summary>
		private readonly ClassObjectMetadata _byteMetadata;
		/// <summary>
		/// Metadata for <see cref="JChar"/>.
		/// </summary>
		private readonly ClassObjectMetadata _charMetadata;

		/// <summary>
		/// Metadata for <see cref="JClassObject"/>.
		/// </summary>
		private readonly ClassObjectMetadata _classMetadata;
		/// <summary>
		/// Metadata for <see cref="JDouble"/>.
		/// </summary>
		private readonly ClassObjectMetadata _doubleMetadata;
		/// <summary>
		/// Metadata for <see cref="JFloat"/>.
		/// </summary>
		private readonly ClassObjectMetadata _floatMetadata;
		/// <summary>
		/// Metadata for <see cref="JInt"/>.
		/// </summary>
		private readonly ClassObjectMetadata _intMetadata;
		/// <summary>
		/// Metadata for <see cref="JLong"/>.
		/// </summary>
		private readonly ClassObjectMetadata _longMetadata;
		/// <summary>
		/// Metadata for <see cref="JShort"/>.
		/// </summary>
		private readonly ClassObjectMetadata _shortMetadata;
		/// <summary>
		/// Metadata for <see cref="JStackTraceElementObject"/>.
		/// </summary>
		private readonly ClassObjectMetadata _stackTraceElementMetadata;
		/// <summary>
		/// Metadata for <see cref="JSystemObject"/>.
		/// </summary>
		private readonly ClassObjectMetadata _systemMetadata;
		/// <summary>
		/// Metadata for <see cref="JThrowableObject"/>.
		/// </summary>
		private readonly ClassObjectMetadata _throwableMetadata;

		/// <summary>
		/// JVM version.
		/// </summary>
		private JRuntimeVersion? _version;

		/// <summary>
		/// Appends main global class to dictionary.
		/// </summary>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <param name="classMetadata">A <see cref="_classMetadata"/> instance.</param>
		private void AppendGlobal(IVirtualMachine vm, ClassObjectMetadata classMetadata)
		{
			String hash = classMetadata.Hash;
			if (!this.GlobalClassCache.TryGetValue(hash, out JGlobal? mainClass))
			{
				mainClass = new(vm, classMetadata, default);
				this.GlobalClassCache[hash] = mainClass;
			}
			this.GlobalClassCache[classMetadata.Hash] = mainClass;
		}
		/// <summary>
		/// Loads user global classes.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		private void LoadUserMainClasses(JEnvironment env)
		{
			foreach (ITypeInformation typeInformation in JVirtualMachine.MainClassesInformation)
			{
				if (!this.GlobalClassCache.TryGetValue(typeInformation.Hash, out JGlobal? jGlobal) ||
				    !jGlobal.IsDefault) continue;
				if (!this.IsMainLoadable(env, typeInformation.Since, typeInformation.AndroidApiLevel)) continue;

				try
				{
					GlobalMainClasses.LoadMainClass(env, jGlobal, typeInformation);
				}
				catch (Exception)
				{
					if (!GlobalMainClasses.CanProceedWithout(typeInformation))
						throw;
				}
			}
		}
		/// <summary>
		/// Indicates whether the current main class is loadable.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="sinceVersion">Class main's since version.</param>
		/// <param name="apiLevel">Class main's Android API level.</param>
		/// <returns>
		/// <see langword="true"/> if the since value is lower to the current JRE version; otherwise
		/// <see langword="false"/>.
		/// </returns>
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		private Boolean IsMainLoadable(JEnvironment env, JRuntimeVersion sinceVersion, Int32 apiLevel)
		{
			// The JNI version is checked to avoid check the JRE version.
			if ((Int32)sinceVersion < env.Version) return true;
			// If running on Android, checks the API level.
			if (JVirtualMachine.AndroidApiLevel.HasValue)
				return apiLevel >= 0 && JVirtualMachine.AndroidApiLevel >= apiLevel;
			// If no running on Android, avoid to load classes with undefined version.
			if (sinceVersion is JRuntimeVersion.Undefined) return false;
			// If fixed JRE version, avoid to check the JRE version.
			if (JavaStandardFeature.GetRuntimeVersion() is { } jreVersion && jreVersion >= sinceVersion)
				return true;
			// Check java.specification.version property.
			this._version ??= env.GetVersion(this.SystemObject.As<JClassLocalRef>(), true);
			return sinceVersion < this._version;
		}
		/// <summary>
		/// Loads primitive global classes.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		private void LoadPrimitiveMainClasses(JEnvironment env)
		{
			this.LoadPrimitiveMainClass(env, ClassObjectMetadata.VoidMetadata, ClassNameHelper.VoidObjectHash);
			this.LoadPrimitiveMainClass(env, this._booleanMetadata, ClassNameHelper.BooleanObjectHash);
			this.LoadPrimitiveMainClass(env, this._byteMetadata, ClassNameHelper.ByteObjectHash);
			this.LoadPrimitiveMainClass(env, this._charMetadata, ClassNameHelper.CharacterObjectHash);
			this.LoadPrimitiveMainClass(env, this._doubleMetadata, ClassNameHelper.DoubleObjectHash);
			this.LoadPrimitiveMainClass(env, this._floatMetadata, ClassNameHelper.FloatObjectHash);
			this.LoadPrimitiveMainClass(env, this._intMetadata, ClassNameHelper.IntegerObjectHash);
			this.LoadPrimitiveMainClass(env, this._longMetadata, ClassNameHelper.LongObjectHash);
			this.LoadPrimitiveMainClass(env, this._shortMetadata, ClassNameHelper.ShortObjectHash);
		}
		/// <summary>
		/// Loads a primitive global class.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="classMetadata">A <see cref="ClassObjectMetadata"/> instance.</param>
		/// <param name="wrapperClassHash">Wrapper class hash.</param>
		private void LoadPrimitiveMainClass(JEnvironment env, ClassObjectMetadata classMetadata,
			String wrapperClassHash)
		{
			JGlobal pGlobalClass = this.GlobalClassCache[classMetadata.Hash];
			this.GlobalClassCache.TryGetValue(wrapperClassHash, out JGlobal? wGlobalClass);
			JGlobalRef globalRef = env.GetPrimitiveMainClassGlobalRef(classMetadata, wGlobalClass);
			pGlobalClass.SetValue(globalRef);
			JTrace.MainClassLoaded(classMetadata.ClassSignature, globalRef);
		}
	}
}