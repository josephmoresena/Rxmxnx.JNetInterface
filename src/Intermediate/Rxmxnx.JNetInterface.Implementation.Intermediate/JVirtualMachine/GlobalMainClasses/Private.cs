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
		/// Main classes dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<String, JGlobal> _mainClasses;
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
			if (!this._mainClasses.ContainsKey(classMetadata.Hash))
				this._mainClasses.TryAdd(classMetadata.Hash, new(vm, this._classMetadata, default));
		}
		/// <summary>
		/// Loads user global classes.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		private void LoadUserMainClasses(JEnvironment env)
		{
			foreach (String hash in JVirtualMachine.userMainClasses.Keys)
			{
				if (!this._mainClasses.TryGetValue(hash, out JGlobal? jGlobal) || !jGlobal.IsDefault) continue;
				try
				{
					jGlobal.SetValue(env.GetMainClassGlobalRef(JVirtualMachine.userMainClasses[hash]));
				}
				catch (Exception)
				{
					switch (hash)
					{
						case ClassNameHelper.VoidObjectHash:
						case ClassNameHelper.BooleanObjectHash:
						case ClassNameHelper.ByteObjectHash:
						case ClassNameHelper.CharacterObjectHash:
						case ClassNameHelper.DoubleObjectHash:
						case ClassNameHelper.FloatObjectHash:
						case ClassNameHelper.IntegerObjectHash:
						case ClassNameHelper.LongObjectHash:
						case ClassNameHelper.ShortObjectHash:
						case ClassNameHelper.EnumHash:
						case ClassNameHelper.BufferHash:
						case ClassNameHelper.MemberHash:
						case ClassNameHelper.ExecutableHash:
						case ClassNameHelper.MethodHash:
						case ClassNameHelper.FieldHash:
							throw;
						default:
							// If class is not built-in, VM initialization may should continue.
							continue;
					}
				}
			}
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
			JGlobal pGlobalClass = this._mainClasses[classMetadata.Hash];
			this._mainClasses.TryGetValue(wrapperClassHash, out JGlobal? wGlobalClass);
			pGlobalClass.SetValue(env.GetPrimitiveMainClassGlobalRef(classMetadata, wGlobalClass));
		}

		/// <summary>
		/// Creates dictionary for main classes.
		/// </summary>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <returns>A <see cref="ConcurrentDictionary{String, Global}"/> instance.</returns>
		private static ConcurrentDictionary<String, JGlobal> CreateMainClassesDictionary(IVirtualMachine vm)
		{
			ConcurrentDictionary<String, JGlobal> result = new();
			foreach (String hash in JVirtualMachine.userMainClasses.Keys)
				result.TryAdd(hash, new(vm, JVirtualMachine.userMainClasses[hash], default));
			return result;
		}
	}
}