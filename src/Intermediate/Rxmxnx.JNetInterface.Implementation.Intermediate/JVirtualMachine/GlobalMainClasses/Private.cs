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
		private readonly ConcurrentDictionary<String, Boolean> _mainClasses;
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
			this._mainClasses.TryAdd(hash, true);
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
					jGlobal.SetValue(env.GetMainClassGlobalRef(typeInformation));
				}
				catch (Exception)
				{
					if (GlobalMainClasses.IsBuiltInBasicType(typeInformation))
						throw;
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
			JGlobal pGlobalClass = this.GlobalClassCache[classMetadata.Hash];
			this.GlobalClassCache.TryGetValue(wrapperClassHash, out JGlobal? wGlobalClass);
			pGlobalClass.SetValue(env.GetPrimitiveMainClassGlobalRef(classMetadata, wGlobalClass));
		}

		/// <summary>
		/// Creates dictionary for main classes.
		/// </summary>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <param name="globalClassCache">A <see cref="ClassCache{JGlobal}"/> instance.</param>
		/// <returns>A <see cref="ConcurrentDictionary{String, Global}"/> instance.</returns>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
		                 Justification = CommonConstants.NoMethodOverloadingJustification)]
		private static ConcurrentDictionary<String, Boolean> CreateMainClassesDictionary(IVirtualMachine vm,
			ClassCache<JGlobal> globalClassCache)
		{
			ConcurrentDictionary<String, Boolean> result = new();
			foreach (String hash in JVirtualMachine.userMainClasses.Keys)
			{
				if (!globalClassCache.TryGetValue(hash, out JGlobal? mainClass))
					mainClass = new(vm, JVirtualMachine.userMainClasses[hash], default);
				globalClassCache[hash] = mainClass;
				result.TryAdd(hash, true);
			}
			return result;
		}
		/// <summary>
		/// Indicates whether is built-in basic class.
		/// </summary>
		/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="typeInformation"/> is a basic built-in class; otherwise;
		/// <see langword="false"/>.
		/// </returns>
		private static Boolean IsBuiltInBasicType(ITypeInformation typeInformation)
			=> typeInformation.Hash switch
			{
				ClassNameHelper.VoidObjectHash or ClassNameHelper.BooleanObjectHash or
					ClassNameHelper.CharacterObjectHash or ClassNameHelper.NumberHash or ClassNameHelper.EnumHash or
					ClassNameHelper.BufferHash or ClassNameHelper.MemberHash or ClassNameHelper.ExecutableHash or
					ClassNameHelper.MethodHash or ClassNameHelper.FieldHash => true,
				_ => !GlobalMainClasses.IsBuiltInNumberType(typeInformation.Hash),
				// If class is not built-in, VM initialization may should continue.
			};
	}
}