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
		/// Metadata for <see cref="JThrowableObject"/>.
		/// </summary>
		private readonly ClassObjectMetadata _throwableMetadata;

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
		/// Loads main class.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="mainClass">A <see cref="JGlobal"/> main class instance.</param>
		/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
		private static void LoadMainClass(JEnvironment env, JGlobal mainClass, ITypeInformation typeInformation)
		{
			JGlobalRef globalRef = env.GetMainClassGlobalRef(typeInformation);
			mainClass.SetValue(globalRef);
			JTrace.MainClassLoaded(typeInformation.Signature, globalRef);
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