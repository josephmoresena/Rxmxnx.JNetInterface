namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private sealed partial class EnvironmentCache
	{
		/// <summary>
		/// Load main classes.
		/// </summary>
		private void LoadMainClasses()
		{
			this.Register(this.ClassObject);
			this.Register(this.ThrowableObject);
			this.Register(this.StackTraceElementObject);

			// Register user main classes.
			foreach (ITypeInformation? typeInformation in JVirtualMachine.MainClassesInformation)
			{
				if (!this._classes.TryGetValue(typeInformation.Hash, out JClassObject? mainClass))
					// Only creates JClassObject instance if not found in class cache.
					mainClass = new(this.ClassObject, typeInformation);
				this.Register(mainClass);
			}

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
		/// <param name="isReferenceType">Indicates whether class reference is from a reference type.</param>
		/// <param name="isPrimitive">Output. Indicates whether class is primitive.</param>
		/// <returns>A <see cref="JStringObject"/> instance.</returns>
		private JStringObject GetClassName(JClassLocalRef classRef, Boolean isReferenceType, out Boolean isPrimitive)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			AccessCache access = this.GetAccess(jniTransaction, this.GetClass<JClassObject>());
			jniTransaction.Add(classRef);
			isPrimitive = !isReferenceType && this.IsPrimitiveClass(classRef, access);
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
			if (localRef == default) this.CheckJniError();
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
			this.CheckJniError();
			return result.Value;
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
			JClassLocalRef assignableRef = isLocalRef ? classRef : default;
			JClassObject result = classObjectMetadata is null ?
				this.GetClass(classRef, isLocalRef, (WellKnownRuntimeTypeInformation)classObjectMetadata) :
				this.GetClass(classObjectMetadata, assignableRef);
			if (JVirtualMachine.IsMainClass(result.Hash))
				this.LoadMainClass(result, classRef, false);
			return this.Register(result);
		}
		/// <summary>
		/// Retrieves class instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="runtimeInformation">Runtime known type information.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetClass(ReadOnlySpan<Byte> className, JClassLocalRef classRef,
			WellKnownRuntimeTypeInformation runtimeInformation)
		{
			TypeInfoSequence classInformation = MetadataHelper.GetClassInformation(className, true);
			JTrace.GetClass(classRef, classInformation.Name);
			if (!this._classes.TryGetValue(classInformation.ToString(), out JClassObject? jClass))
			{
				JTypeKind kind = classInformation.Name[0] == CommonNames.ArraySignaturePrefixChar ?
					JTypeKind.Array :
					runtimeInformation.Kind.GetValueOrDefault();
				ITypeInformation typeInformation =
					// If well-known type, we should use exact metadata.
					(ITypeInformation?)MetadataHelper.GetExactMetadata(classInformation.ToString()) ??
					new TypeInformation(classInformation, kind, runtimeInformation.IsFinal);
				jClass = new(this.ClassObject, typeInformation, classRef);
			}
			if (jClass.LocalReference == default && classRef.Value != default) jClass.SetValue(classRef);
			return jClass;
		}
		/// <summary>
		/// Retrieves class instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="typeInformation">Agnostic type information.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetClass(ITypeInformation typeInformation, JClassLocalRef classRef)
		{
			JTrace.GetClass(classRef, typeInformation.ClassName);
			if (!this._classes.TryGetValue(typeInformation.Hash, out JClassObject? jClass))
				jClass = new(this.ClassObject, typeInformation, classRef);
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
			JTrace.GetMetadataOrFindClass(jClass);
			JClassLocalRef classRef;
			fixed (Byte* ptr = &MemoryMarshal.GetReference(jClass.Name.AsSpan()))
				classRef = this.FindClass(ptr);
			return classRef;
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> using <paramref name="namePtr"/> as class name.
		/// </summary>
		/// <param name="namePtr">A pointer to class name.</param>
		/// <param name="withNoCheckError">Indicates whether <see cref="CheckJniError"/> should not be called.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		private unsafe JClassLocalRef FindClass(Byte* namePtr, Boolean withNoCheckError = false)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.FindClassInfo);
			JClassLocalRef result = nativeInterface.ClassFunctions.FindClass(this.Reference, namePtr);
			if (result.IsDefault && !withNoCheckError) this.CheckJniError();
			return result;
		}
		/// <summary>
		/// Retrieves class from cache or loads it using JNI.
		/// </summary>
		/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private unsafe JClassObject GetOrFindClass(ITypeInformation typeInformation)
		{
			if (this._classes.TryGetValue(typeInformation.Hash, out JClassObject? result))
			{
				JTrace.ClassFound(result);
				return this.GetLoadedClass(result);
			}
			if (MetadataHelper.GetExactMetadata(typeInformation.Hash) is { } metadata)
			{
				// Class is found in type metadata cache.
				result = new(this.ClassObject, metadata);
				JTrace.ClassFound(metadata);
			}
			else
			{
				JClassLocalRef classRef = this._objects.FindClassParameter(typeInformation.Hash);
				if (classRef.IsDefault && typeInformation is not ClassObjectMetadata)
					// Only find class by name if class was not in the runtime class cache.
					fixed (Byte* ptr = &MemoryMarshal.GetReference(typeInformation.ClassName.AsSpan()))
					{
						JTrace.FindClass(typeInformation.ClassName);
						classRef = this.FindClass(ptr);
					}
				else
					JTrace.ClassFound(typeInformation, classRef);

				result = new(this.ClassObject, typeInformation, classRef);
			}
			return this.Register(result);
		}
		/// <summary>
		/// Retrieves primitive wrapper class from primitive signature.
		/// </summary>
		/// <param name="signature">JNI primitive signature.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetPrimitiveWrapperClass(Byte signature)
		{
			JClassTypeMetadata wrapperTypeInformation = (JClassTypeMetadata)(signature switch
			{
				CommonNames.BooleanSignatureChar => MetadataHelper.GetExactMetadata<JBooleanObject>(),
				CommonNames.ByteSignatureChar => MetadataHelper.GetExactMetadata<JByteObject>(),
				CommonNames.CharSignatureChar => MetadataHelper.GetExactMetadata<JCharacterObject>(),
				CommonNames.DoubleSignatureChar => MetadataHelper.GetExactMetadata<JDoubleObject>(),
				CommonNames.FloatSignatureChar => MetadataHelper.GetExactMetadata<JFloatObject>(),
				CommonNames.IntSignatureChar => MetadataHelper.GetExactMetadata<JIntegerObject>(),
				CommonNames.LongSignatureChar => MetadataHelper.GetExactMetadata<JLongObject>(),
				CommonNames.ShortSignatureChar => MetadataHelper.GetExactMetadata<JShortObject>(),
				_ => MetadataHelper.GetExactMetadata<JVoidObject>(),
			});

			if (this._classes.TryGetValue(wrapperTypeInformation.Hash, out JClassObject? wrapperClass))
			{
				JTrace.ClassFound(wrapperClass);
			}
			else
			{
				wrapperClass = this.GetLoadedClass(new(this.ClassObject, wrapperTypeInformation));
				JTrace.ClassFound(wrapperTypeInformation);
			}
			return wrapperClass;
		}
		/// <summary>
		/// Loads <paramref name="jClass"/> in the current class cache and retrieves the same instance.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns><paramref name="jClass"/> same instance.</returns>
		[return: NotNullIfNotNull(nameof(jClass))]
		private JClassObject? GetLoadedClass(JClassObject? jClass)
		{
			if (!this._objects.Equals(this) && JObject.IsNullOrDefault(jClass)) this.LoadClass(jClass);
			return jClass;
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
		/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
		/// <param name="buffer">Binary span with class information.</param>
		/// <param name="jClassLoader">The object used as class loader.</param>
		/// <returns>Loaded <see cref="JClassObject"/> instance.</returns>
		private JClassObject LoadClass(ITypeInformation typeInformation, ReadOnlySpan<Byte> buffer,
			JClassLoaderObject? jClassLoader)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClassLoader);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JObjectLocalRef localRef = jniTransaction.Add(jClassLoader);
			JClassLocalRef classRef = this.DefineClass(typeInformation.ClassName, buffer, localRef);
			if (this._classes.TryGetValue(typeInformation.Hash, out JClassObject? result))
				//Class found in metadata cache.
			{
				this.DefineExistingClass(result, jniTransaction, classRef);
			}
			else
			{
				result = new(this.ClassObject, typeInformation, classRef);
				if (JVirtualMachine.IsMainClass(typeInformation.Hash))
					this.LoadMainClass(result, classRef);
			}
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
				JTrace.DefiningClass(this.Reference, className, buffer);
				classRef = nativeInterface.ClassFunctions.DefineClass(this.Reference, namePtr, localRef, bufferPtr,
				                                                      buffer.Length);
			}
			if (classRef.IsDefault) this.CheckJniError();
			JTrace.DefiningClass(this.Reference, className, classRef);
			return classRef;
		}
		/// <summary>
		/// Define a existing class in metadata cache.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <exception cref="InvalidOperationException"></exception>
		private void DefineExistingClass(JClassObject jClass, INativeTransaction jniTransaction,
			JClassLocalRef classRef)
		{
			JClassLocalRef classRefO = jniTransaction.Add(jClass);
			if (classRefO.IsDefault || this._env.IsSame(classRef.Value, default))
			{
				if (JVirtualMachine.IsMainClass(jClass.Hash))
					this.LoadMainClass(jClass, classRef);
				else
					jClass.SetValue(classRef);
				this._classes.Unload(classRefO);
			}
			else if (!this._env.IsSame(classRef.Value, classRefO.Value))
			{
				IMessageResource resource = IMessageResource.GetInstance();
				throw new InvalidOperationException(resource.ClassRedefinition);
			}
		}
		/// <summary>
		/// Loads <paramref name="jClass"/> as main class.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="deleteLocalRef">Indicates whether local class reference should be deleted.</param>
		private void LoadMainClass(JClassObject jClass, JClassLocalRef classRef, Boolean deleteLocalRef = true)
		{
			JGlobal jGlobal = this.VirtualMachine.LoadGlobal(jClass);
			ClassObjectMetadata classMetadata = (ClassObjectMetadata)jGlobal.ObjectMetadata;
			if (jGlobal.IsDefault)
			{
				// A global-reference is created only if the existing one is default.
				jGlobal.SetValue(this._env.GetMainClassGlobalRef(classMetadata, classRef, deleteLocalRef));
				this.VirtualMachine.ReloadAccess(jClass.Hash);
			}
			else if (deleteLocalRef)
			{
				this._env.DeleteLocalRef(classRef.Value);
			}
			if (deleteLocalRef) jClass.ClearValue();
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
			Boolean isLocalRef = this.IsLocalObject(jObject, out JReferenceType referenceType);
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
			JTrace.AsClassObject(result.Name, jObject);
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
		/// <summary>
		/// Determines whether an object of <paramref name="jClass"/> can be safely cast to
		/// <paramref name="otherClass"/>.
		/// </summary>
		/// <param name="jClass">Java class instance.</param>
		/// <param name="otherClass">Other java class instance.</param>
		/// <param name="createFrame">Delegate to create <see cref="LocalFrame"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if an object of <paramref name="jClass"/> can be safely cast to
		/// <paramref name="otherClass"/>; otherwise, <see langword="false"/>.
		/// </returns>
#if !PACKAGE
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2234,
		                 Justification = CommonConstants.BackwardOperationJustification)]
#endif
		private Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass,
			Func<JEnvironment, LocalFrame>? createFrame)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			ImplementationValidationUtilities.ThrowIfProxy(otherClass);
			Boolean? result = MetadataHelper.IsAssignable(jClass, otherClass);
			if (result.HasValue)
				return result.Value; // Cached assignation.

			using LocalFrame? _ = createFrame?.Invoke(this._env);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JClassLocalRef otherClassRef = jniTransaction.Add(this.ReloadClass(otherClass));
			result = this.IsAssignableFrom(classRef, otherClassRef);
			this.CheckJniError();

			if (result.Value) // If true, inverse is false.
				return MetadataHelper.SetAssignable(jClass, otherClass, result.Value);

			// Checks inverse assignation.
			Boolean inverseResult = this.IsAssignableFrom(otherClass, jClass);
			MetadataHelper.SetAssignable(otherClass, jClass, inverseResult);
			this.CheckJniError();
			return MetadataHelper.SetAssignable(jClass, otherClass, result.Value);
		}
		/// <summary>
		/// Indicates whether <paramref name="classRef"/> is assignable to <paramref name="otherClassRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="otherClassRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="classRef"/> is assignable to <paramref name="otherClassRef"/>;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		private unsafe Boolean IsAssignableFrom(JClassLocalRef classRef, JClassLocalRef otherClassRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.IsAssignableFromInfo);
			Boolean classResult = nativeInterface.ClassFunctions
			                                     .IsAssignableFrom(this.Reference, classRef, otherClassRef).Value;
			return classResult;
		}
		/// <summary>
		/// Retrieves the class object and instantiation metadata.
		/// </summary>
		/// <param name="localRef">Object instance to get class.</param>
		/// <param name="currentMetadata">Current type metadata.</param>
		/// <param name="typeMetadata">Output. Instantiation metadata.</param>
		/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
		private JClassObject GetObjectClass(JObjectLocalRef localRef, JReferenceTypeMetadata currentMetadata,
			out JReferenceTypeMetadata typeMetadata)
		{
			JClassObject jClass;
			if (currentMetadata.Modifier != JTypeModifier.Final)
			{
				// If the modifier is not final, we should try to obtain more approximate type metadata.
				jClass = this._env.GetObjectClass(localRef, out typeMetadata);
			}
			else
			{
				// If the modifier is final, we can use exact metadata.
				jClass = currentMetadata.GetClass(this._env);
				typeMetadata = currentMetadata;
			}
			return jClass;
		}
		/// <summary>
		/// Indicates whether <paramref name="jObject"/> is a local object.
		/// </summary>
		/// <param name="jObject">A <paramref name="jObject"/> instance.</param>
		/// <param name="referenceType">Output. Type of reference used by <paramref name="jObject"/>.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="jObject"/> is a local object; otherwise, <see langword="false"/>.
		/// </returns>
		private Boolean IsLocalObject(JReferenceObject jObject, out JReferenceType referenceType)
		{
			JObjectLocalRef localRef = jObject.As<JObjectLocalRef>();
			if (this.Version < IVirtualMachine.MinimalVersion)
			{
				Boolean result = jObject is ILocalObject jLocal && jLocal.LocalReference == localRef;
				referenceType = result ? JReferenceType.LocalRefType : JReferenceType.InvalidRefType;
				return result;
			}
			referenceType = this._env.GetReferenceType(localRef);
			return referenceType == JReferenceType.LocalRefType;
		}
		/// <summary>
		/// Retrieves the current <paramref name="classRef"/> reference as <see cref="JClassObject"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="runtimeInformation">Runtime known type information.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject AsClassObject(JClassLocalRef classRef, WellKnownRuntimeTypeInformation runtimeInformation)
			=> this.Register(this.GetClass(classRef, true, runtimeInformation));
		/// <summary>
		/// Computes the class compatibility with <typeparamref name="TDataType"/> instance.
		/// </summary>
		/// <typeparam name="TDataType">A <see cref="IDataType{TDataType}"/> type.</typeparam>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="sameClass">
		/// Output. Indicates whether <paramref name="jClass"/> is the class for <typeparamref name="TDataType"/> type.
		/// </param>
		private void CheckClassCompatibility<TDataType>(JClassObject jClass, out Boolean sameClass)
			where TDataType : IDataType<TDataType>
		{
			JDataTypeMetadata elementTypeMetadata = MetadataHelper.GetExactMetadata<TDataType>();
			sameClass = elementTypeMetadata.Hash.AsSpan().SequenceEqual(jClass.Hash);

			if (sameClass) return; // Same class type.

			if (elementTypeMetadata.Kind is JTypeKind.Primitive ||
			    jClass.IsPrimitive) // There is no compatibility between primitive types.
				CommonValidationUtilities.ThrowIfInvalidCast(elementTypeMetadata, false);

			JClassObject elementTypeClass = this.GetClass<TDataType>();
			Boolean allowedCast =
				this.IsAssignableFrom(jClass, elementTypeClass, e => new(e, IVirtualMachine.GetObjectClassCapacity));
			CommonValidationUtilities.ThrowIfInvalidCast(elementTypeMetadata, allowedCast);
		}
		/// <summary>
		/// Retrieves the array class instance for given element class.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetArrayClass(JClassObject jClass)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jClass);
			Int32 elementTypeSignature = jClass.ClassSignature.Length;
			Int32 offset = 2 * elementTypeSignature;
			Int32 length = elementTypeSignature + 1;
			ReadOnlySpan<Byte> typeInformationSpan = MemoryMarshal.AsBytes(jClass.Hash.AsSpan());
			ReadOnlySpan<Byte> arrayClassName = typeInformationSpan[offset..(offset + length)];

			using LocalFrame _ = new(this._env, IVirtualMachine.GetObjectClassCapacity);
			return this.GetClass(arrayClassName);
		}
	}
}