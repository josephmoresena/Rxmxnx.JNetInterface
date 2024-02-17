namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record EnvironmentCache
	{
		/// <summary>
		/// Load main classes.
		/// </summary>
		private void LoadMainClasses()
		{
			this.Register(this.ClassObject);
			this.Register(this.ThrowableObject);
			this.Register(this.StackTraceElementObject);

			this.Register(this.BooleanPrimitive);
			this.Register(this.BytePrimitive);
			this.Register(this.CharPrimitive);
			this.Register(this.DoublePrimitive);
			this.Register(this.FloatPrimitive);
			this.Register(this.IntPrimitive);
			this.Register(this.LongPrimitive);
			this.Register(this.ShortPrimitive);
		}
		/// <summary>
		/// Reloads current class object.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>Current <see cref="JClassLocalRef"/> reference.</returns>
		private JClassLocalRef ReloadClass(JClassObject? jClass)
		{
			if (jClass is null) return default;
			JClassLocalRef classRef = jClass.As<JClassLocalRef>();
			if (!classRef.IsDefault) return classRef;
			classRef = this.FindClass(jClass);
			jClass.SetValue(classRef);
			this.Register(jClass);
			return classRef;
		}
		/// <summary>
		/// Retrieves class instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetClass(ReadOnlySpan<Byte> className, JClassLocalRef classRef)
		{
			CStringSequence classInformation = MetadataHelper.GetClassInformation(className);
			if (!this._classes.TryGetValue(classInformation.ToString(), out JClassObject? jClass))
				jClass = new(this.ClassObject, new TypeInformation(classInformation), classRef);
			if (jClass.InternalReference == default && classRef.Value != default) jClass.SetValue(classRef);
			return jClass;
		}
		/// <summary>
		/// Retrieves <see cref="JClassLocalRef"/> reference for given instance.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		private JClassLocalRef FindClass(JClassObject jClass)
			=> jClass.ClassSignature.Length != 1 ?
				jClass.Name.WithSafeFixed(this, EnvironmentCache.FindClass) :
				this.FindPrimitiveClass(jClass.ClassSignature[0]);
		/// <summary>
		/// Retrieves class from cache or loads it using JNI.
		/// </summary>
		/// <param name="classInformation">A <see cref="ITypeInformation"/> instance.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetOrFindClass(ITypeInformation classInformation)
		{
			if (this._classes.TryGetValue(classInformation.Hash, out JClassObject? result)) return result;
			if (MetadataHelper.GetMetadata(classInformation.Hash) is { } metadata)
			{
				result = new(this.ClassObject, metadata);
			}
			else
			{
				JClassLocalRef classRef = this._objects.FindClassParameter(classInformation.Hash);
				if (classRef.IsDefault)
					classRef = classInformation.ClassName.WithSafeFixed(this, EnvironmentCache.FindClass);
				result = new(this.ClassObject, classInformation, classRef);
			}
			return this.Register(result);
		}
		/// <summary>
		/// Indicates whether the object referenced by <paramref name="localRef"/> is an instance
		/// of the class referenced by <paramref name="classRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>
		/// <see langword="true"/> if the object referenced by <paramref name="localRef"/> is an instance
		/// of the class referenced by <paramref name="classRef"/>; otherwise, <see langword="false"/>.
		/// </returns>
		private Boolean IsInstanceOf(JObjectLocalRef localRef, JClassLocalRef classRef)
		{
			IsInstanceOfDelegate isInstanceOf = this.GetDelegate<IsInstanceOfDelegate>();
			Byte result = isInstanceOf(this.Reference, localRef, classRef);
			this.CheckJniError();
			return result == JBoolean.TrueValue;
		}

		/// <summary>
		/// Loads a java class from its binary information into the current VM.
		/// </summary>
		/// <param name="memoryList">
		/// A fixed memory list containing both JNI class name and class binary information.
		/// </param>
		/// <param name="args">Cache and class loader.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private static JClassObject LoadClass(ReadOnlyFixedMemoryList memoryList,
			(EnvironmentCache cache, JClassLoaderObject? jClassLoader) args)
		{
			ValidationUtilities.ThrowIfDummy(args.jClassLoader);
			CStringSequence classInformation = MetadataHelper.GetClassInformation(memoryList[0].Bytes);
			DefineClassDelegate defineClass = args.cache.GetDelegate<DefineClassDelegate>();
			using INativeTransaction jniTransaction = args.cache.VirtualMachine.CreateTransaction(2);
			JObjectLocalRef localRef = jniTransaction.Add(args.jClassLoader);
			JClassLocalRef classRef = defineClass(args.cache.Reference, (ReadOnlyValPtr<Byte>)memoryList[0].Pointer,
			                                      localRef, memoryList[1].Pointer, memoryList[1].Bytes.Length);
			if (classRef.IsDefault) args.cache.CheckJniError();
			if (args.cache._classes.TryGetValue(classInformation.ToString(), out JClassObject? result))
			{
				JEnvironment env = args.cache._env;
				JClassLocalRef classRefO = jniTransaction.Add(result);
				if (classRefO.IsDefault || env.IsSame(classRef.Value, default))
				{
					result.SetValue(classRef);
					args.cache._classes.Unload(classRefO);
				}
				else if (!env.IsSame(classRef.Value, classRefO.Value))
				{
					throw new InvalidOperationException("Redefinition class is unsupported.");
				}
			}
			else
			{
				result = new(args.cache.ClassObject, new TypeInformation(classInformation), classRef);
			}
			return args.cache.Register(result);
		}
	}
}