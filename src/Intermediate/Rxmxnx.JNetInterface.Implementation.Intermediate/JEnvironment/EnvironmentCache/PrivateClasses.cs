namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
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
		/// Retrieves a <see cref="JStringObject"/> containing class name.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="isPrimitive">Output. Indicates whether class is primitive.</param>
		/// <returns>A <see cref="JStringObject"/> instance.</returns>
		private JStringObject GetClassName(JClassLocalRef classRef, out Boolean isPrimitive)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, this.ClassObject);
			jniTransaction.Add(classRef);
			isPrimitive = this.IsPrimitiveClass(classRef, access);
			return this.GetClassName(classRef, access);
		}
		/// <summary>
		/// Retrieves a <see cref="JStringObject"/> containing class name.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="access">A <see cref="AccessCache"/> instance.</param>
		/// <returns>A <see cref="JStringObject"/> instance.</returns>
		private unsafe JStringObject GetClassName(JClassLocalRef classRef, AccessCache access)
		{
			JMethodId getNameId = access.GetMethodId(NativeFunctionSetImpl.GetNameDefinition, this._env);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.CallObjectMethodInfo);
			JObjectLocalRef localRef =
				nativeInterface.InstanceMethodFunctions.CallObjectMethod.Call(
					this.Reference, classRef.Value, getNameId, ReadOnlyValPtr<JValue>.Zero);
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return new(jStringClass, localRef.Transform<JObjectLocalRef, JStringLocalRef>());
		}
		/// <summary>
		/// Indicates whether class is primitive.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="access">A <see cref="AccessCache"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if class is primitive; otherwise; <see langword="false"/>.
		/// </returns>
		private unsafe Boolean IsPrimitiveClass(JClassLocalRef classRef, AccessCache access)
		{
			JMethodId isPrimitiveId = access.GetMethodId(NativeFunctionSetImpl.IsPrimitiveDefinition, this._env);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.CallBooleanMethodInfo);
			JBoolean result =
				nativeInterface.InstanceMethodFunctions.CallBooleanMethod.Call(
					this.Reference, classRef.Value, isPrimitiveId, ReadOnlyValPtr<JValue>.Zero);
			return result.Value;
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
		/// Retrieves the current <paramref name="classRef"/> instance as <see cref="JClassObject"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="isLocalRef">Indicates whether <paramref name="classRef"/> is local reference.</param>
		/// <param name="classObjectMetadata">A <see cref="ClassObjectMetadata"/> instance.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetClass(JClassLocalRef classRef, Boolean isLocalRef,
			ClassObjectMetadata? classObjectMetadata)
		{
			JClassObject result;
			if (classObjectMetadata is null)
				result = this.GetClass(classRef, isLocalRef);
			else
				result = this.GetClass(classObjectMetadata.Name, isLocalRef ? classRef : default);
			return this.Register(result);
		}
		/// <summary>
		/// Retrieves class instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetClass(ReadOnlySpan<Byte> className, JClassLocalRef classRef)
		{
			CStringSequence classInformation = MetadataHelper.GetClassInformation(className, true);
			JTrace.GetClass(classRef, classInformation);
			if (!this._classes.TryGetValue(classInformation.ToString(), out JClassObject? jClass))
				jClass = new(this.ClassObject, new TypeInformation(classInformation), classRef);
			if (jClass.LocalReference == default && classRef.Value != default) jClass.SetValue(classRef);
			return jClass;
		}
		/// <summary>
		/// Retrieves <see cref="JClassLocalRef"/> reference for given instance.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		private JClassLocalRef FindClass(JClassObject jClass)
		{
			if (jClass.ClassSignature.Length == 1)
				return this.FindPrimitiveClass(jClass.ClassSignature[0]);
			JTrace.FindClass(jClass);
			return jClass.Name.WithSafeFixed(this, EnvironmentCache.FindClass);
		}
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
		private unsafe Boolean IsInstanceOf(JObjectLocalRef localRef, JClassLocalRef classRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.IsInstanceOfInfo);
			JBoolean result = nativeInterface.ObjectFunctions.IsInstanceOf(this.Reference, localRef, classRef);
			this.CheckJniError();
			return result.Value;
		}
		/// <summary>
		/// Loads a java class from its binary information into the current VM.
		/// </summary>
		/// <param name="rawClassMemory">
		/// A fixed context containing class binary information.
		/// </param>
		/// <param name="args">Cache, Class metadata and class loader.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private static unsafe JClassObject LoadClass(in IReadOnlyFixedContext<Byte> rawClassMemory,
			(EnvironmentCache cache, ITypeInformation metadata, JClassLoaderObject? jClassLoader) args)
		{
			ImplementationValidationUtilities.ThrowIfProxy(args.jClassLoader);
			ref readonly NativeInterface nativeInterface =
				ref args.cache.GetNativeInterface<NativeInterface>(NativeInterface.DefineClassInfo);
			using INativeTransaction jniTransaction = args.cache.VirtualMachine.CreateTransaction(2);
			using IFixedPointer.IDisposable classNamePointer = args.metadata.GetClassNameFixedPointer();
			JObjectLocalRef localRef = jniTransaction.Add(args.jClassLoader);
			JClassLocalRef classRef = nativeInterface.ClassFunctions.DefineClass(
				args.cache.Reference, (ReadOnlyValPtr<Byte>)classNamePointer.Pointer, localRef, rawClassMemory.Pointer,
				rawClassMemory.Bytes.Length);
			if (classRef.IsDefault) args.cache.CheckJniError();
			if (args.cache._classes.TryGetValue(args.metadata.Hash, out JClassObject? result))
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
				result = new(args.cache.ClassObject, args.metadata, classRef);
			}
			return args.cache.Register(result);
		}
		/// <summary>
		/// Retrieves the <see cref="ClassObjectMetadata"/> instance from <paramref name="jObject"/> metadata.
		/// </summary>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		/// <returns>A <see cref="ClassObjectMetadata"/> instance.</returns>
		private ClassObjectMetadata? GetClassObjectMetadata(JReferenceObject jObject)
			=> jObject switch
			{
				ILocalObject local => ILocalObject.CreateMetadata(local) as ClassObjectMetadata,
				JGlobalBase jGlobal => this.VirtualMachine.LoadMetadataGlobal(jGlobal),
				_ => default,
			};
		/// <summary>
		/// Retrieves the current <paramref name="jObject"/> instance as <see cref="JClassObject"/>.
		/// </summary>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject AsClassObjectUnchecked(JReferenceObject jObject)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JClassLocalRef classRef = jniTransaction.Add<JClassLocalRef>(jObject);
			JReferenceType referenceType = this._env.GetReferenceType(classRef.Value);
			Boolean isLocalRef = referenceType == JReferenceType.LocalRefType;
			ClassObjectMetadata? classObjectMetadata = this.GetClassObjectMetadata(jObject);
			JTrace.AsClassObject(classRef, referenceType, classObjectMetadata);
			JClassObject result = this.GetClass(classRef, isLocalRef, classObjectMetadata);
			switch (jObject)
			{
				case ILocalObject local when classRef == result.LocalReference:
					result.Lifetime.Synchronize(local.Lifetime);
					break;
				case JGlobal jGlobal when !result.Lifetime.HasValidGlobal<JGlobal>():
					result.Lifetime.SetGlobal(jGlobal);
					break;
				case JWeak jWeak when !result.Lifetime.HasValidGlobal<JWeak>():
					result.Lifetime.SetGlobal(jWeak);
					break;
			}
			if (classObjectMetadata is not null)
				ILocalObject.ProcessMetadata(result, classObjectMetadata);
			return result;
		}
	}
}