namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
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
				nativeInterface.InstanceMethodFunctions.MethodFunctions.CallObjectMethod.Call(
					this.Reference, classRef.Value, getNameId, default);
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
				nativeInterface.InstanceMethodFunctions.MethodFunctions.CallBooleanMethod.Call(
					this.Reference, classRef.Value, isPrimitiveId, default);
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
		private unsafe JClassLocalRef FindClass(JClassObject jClass)
		{
			if (jClass.ClassSignature.Length == 1)
				return this.FindPrimitiveClass(jClass.ClassSignature[0]);
			JTrace.FindClass(jClass);
			fixed (Byte* ptr = &MemoryMarshal.GetReference(jClass.Name.AsSpan()))
				return this.FindClass(new IntPtr(ptr));
		}
		/// <summary>
		/// Retrieves class from cache or loads it using JNI.
		/// </summary>
		/// <param name="classInformation">A <see cref="ITypeInformation"/> instance.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private unsafe JClassObject GetOrFindClass(ITypeInformation classInformation)
		{
			if (this._classes.TryGetValue(classInformation.Hash, out JClassObject? result)) return result;
			if (MetadataHelper.GetMetadata(classInformation.Hash) is { } metadata)
				//Class is found in metadata cache.
			{
				result = new(this.ClassObject, metadata);
			}
			else
			{
				JClassLocalRef classRef = this._objects.FindClassParameter(classInformation.Hash);
				if (classRef.IsDefault)
					fixed (Byte* ptr = &MemoryMarshal.GetReference(classInformation.ClassName.AsSpan()))
						classRef = this.FindClass(new IntPtr(ptr));
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
		/// <param name="metadata">A <see cref="ITypeInformation"/> instance.</param>
		/// <param name="buffer">Binary span with class information.</param>
		/// <param name="jClassLoader">The object used as class loader.</param>
		/// <returns>Loaded <see cref="JClassObject"/> instance.</returns>
		private JClassObject LoadClass(ITypeInformation metadata, ReadOnlySpan<Byte> buffer,
			JClassLoaderObject? jClassLoader)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClassLoader);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JObjectLocalRef localRef = jniTransaction.Add(jClassLoader);
			JClassLocalRef classRef = this.DefineClass(metadata.ClassName, buffer, localRef);
			if (this._classes.TryGetValue(metadata.Hash, out JClassObject? result))
				//Class found in metadata cache.
				this.DefineExistingClass(result, jniTransaction, classRef);
			else
				result = new(this.ClassObject, metadata, classRef);
			return this.Register(result);
		}
		/// <summary>
		/// Loads a java class from its binary information into the current VM.
		/// </summary>
		/// <param name="className">Class name</param>
		/// <param name="buffer">Buffer containing the .class file data.</param>
		/// <param name="localRef">Class loader reference.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		private unsafe JClassLocalRef DefineClass(CString className, ReadOnlySpan<Byte> buffer,
			JObjectLocalRef localRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.DefineClassInfo);
			JClassLocalRef classRef;
			fixed (Byte* namePtr = &MemoryMarshal.GetReference(className.AsSpan()))
			fixed (Byte* bufferPtr = &MemoryMarshal.GetReference(buffer))
			{
				classRef = nativeInterface.ClassFunctions.DefineClass(this.Reference, namePtr, localRef, new(bufferPtr),
				                                                      buffer.Length);
			}
			if (classRef.IsDefault) this.CheckJniError();
			return classRef;
		}
		/// <summary>
		/// Define class definition of existing class in metadata cache.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <exception cref="InvalidOperationException"></exception>
		private void DefineExistingClass(JClassObject jClass, INativeTransaction jniTransaction,
			JClassLocalRef classRef)
		{
			JClassLocalRef classRefO = jniTransaction.Add(jClass);
			if (!classRefO.IsDefault && !this._env.IsSame(classRef.Value, default))
			{
				if (!this._env.IsSame(classRef.Value, classRefO.Value))
					throw new InvalidOperationException("Redefinition class is unsupported.");
			}
			else
			{
				jClass.SetValue(classRef);
				this._classes.Unload(classRefO);
			}
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
		/// <summary>
		/// Retrieves the <see cref="JModuleObject"/> instance from <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>A <see cref="JModuleObject"/> instance.</returns>
		private unsafe JModuleObject GetModule(JClassLocalRef classRef)
		{
			ref readonly NativeInterface9 nativeInterface =
				ref this.GetNativeInterface<NativeInterface9>(NativeInterface9.GetModuleInfo);
			JObjectLocalRef localRef = nativeInterface.GetModule(this.Reference, classRef);
			if (localRef == default) this.CheckJniError();
			return new(this.GetClass<JModuleObject>(), localRef);
		}
	}
}