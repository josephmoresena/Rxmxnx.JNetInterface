namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// This record stores cache for a <see cref="JEnvironment"/> instance.
	/// </summary>
	private sealed partial record JEnvironmentCache : LocalMainClasses
	{
		/// <summary>
		/// Maximum amount of bytes usable on stack.
		/// </summary>
		private const Int32 MaxStackBytes = 128;

		/// <summary>
		/// JNI Delegate dictionary.
		/// </summary>
		private static readonly Dictionary<Type, Int32> delegateIndex = new()
		{
			{ typeof(DefineClassDelegate), 0 },
			{ typeof(FindClassDelegate), 1 },
			{ typeof(FromReflectedMethodDelegate), 2 },
			{ typeof(FromReflectedFieldDelegate), 3 },
			{ typeof(ToReflectedMethodDelegate), 4 },
			{ typeof(GetSuperclassDelegate), 5 },
			{ typeof(IsAssignableFromDelegate), 6 },
			{ typeof(ToReflectedFieldDelegate), 7 },
			{ typeof(ThrowDelegate), 8 },
			{ typeof(ThrowNewDelegate), 9 },
			{ typeof(ExceptionOccurredDelegate), 10 },
			{ typeof(ExceptionDescribeDelegate), 11 },
			{ typeof(ExceptionClearDelegate), 12 },
			{ typeof(FatalErrorDelegate), 13 },
			{ typeof(PushLocalFrameDelegate), 14 },
			{ typeof(PopLocalFrameDelegate), 15 },
			{ typeof(NewGlobalRefDelegate), 16 },
			{ typeof(DeleteGlobalRefDelegate), 17 },
			{ typeof(DeleteLocalRefDelegate), 18 },
			{ typeof(IsSameObjectDelegate), 19 },
			{ typeof(NewLocalRefDelegate), 20 },
			{ typeof(EnsureLocalCapacityDelegate), 21 },
			{ typeof(NewObjectADelegate), 25 },
			{ typeof(GetObjectClassDelegate), 26 },
			{ typeof(IsInstanceOfDelegate), 27 },
			{ typeof(GetMethodIdDelegate), 28 },
			{ typeof(CallObjectMethodADelegate), 31 },
			{ typeof(CallBooleanMethodADelegate), 34 },
			{ typeof(CallByteMethodADelegate), 37 },
			{ typeof(CallCharMethodADelegate), 40 },
			{ typeof(CallShortMethodADelegate), 43 },
			{ typeof(CallIntMethodADelegate), 46 },
			{ typeof(CallLongMethodADelegate), 49 },
			{ typeof(CallFloatMethodADelegate), 52 },
			{ typeof(CallDoubleMethodADelegate), 55 },
			{ typeof(CallVoidMethodADelegate), 58 },
			{ typeof(CallNonVirtualObjectMethodADelegate), 61 },
			{ typeof(CallNonVirtualBooleanMethodADelegate), 64 },
			{ typeof(CallNonVirtualByteMethodADelegate), 67 },
			{ typeof(CallNonVirtualCharMethodADelegate), 70 },
			{ typeof(CallNonVirtualShortMethodADelegate), 73 },
			{ typeof(CallNonVirtualIntMethodADelegate), 76 },
			{ typeof(CallNonVirtualLongMethodADelegate), 79 },
			{ typeof(CallNonVirtualFloatMethodADelegate), 82 },
			{ typeof(CallNonVirtualDoubleMethodADelegate), 85 },
			{ typeof(CallNonVirtualVoidMethodADelegate), 88 },
			{ typeof(GetFieldIdDelegate), 89 },
			{ typeof(GetObjectFieldDelegate), 90 },
			{ typeof(GetBooleanFieldDelegate), 91 },
			{ typeof(GetByteFieldDelegate), 92 },
			{ typeof(GetCharFieldDelegate), 93 },
			{ typeof(GetShortFieldDelegate), 94 },
			{ typeof(GetIntFieldDelegate), 95 },
			{ typeof(GetLongFieldDelegate), 96 },
			{ typeof(GetFloatFieldDelegate), 97 },
			{ typeof(GetDoubleFieldDelegate), 98 },
			{ typeof(SetObjectFieldDelegate), 99 },
			{ typeof(SetBooleanFieldDelegate), 100 },
			{ typeof(SetByteFieldDelegate), 101 },
			{ typeof(SetCharFieldDelegate), 102 },
			{ typeof(SetShortFieldDelegate), 103 },
			{ typeof(SetIntFieldDelegate), 104 },
			{ typeof(SetLongFieldDelegate), 105 },
			{ typeof(SetFloatFieldDelegate), 106 },
			{ typeof(SetDoubleFieldDelegate), 107 },
			{ typeof(GetStaticMethodIdDelegate), 108 },
			{ typeof(CallStaticObjectMethodADelegate), 111 },
			{ typeof(CallStaticBooleanMethodADelegate), 114 },
			{ typeof(CallStaticByteMethodADelegate), 117 },
			{ typeof(CallStaticCharMethodADelegate), 120 },
			{ typeof(CallStaticShortMethodADelegate), 123 },
			{ typeof(CallStaticIntMethodADelegate), 126 },
			{ typeof(CallStaticLongMethodADelegate), 129 },
			{ typeof(CallStaticFloatMethodADelegate), 132 },
			{ typeof(CallStaticDoubleMethodADelegate), 135 },
			{ typeof(CallStaticVoidMethodADelegate), 138 },
			{ typeof(GetStaticFieldIdDelegate), 139 },
			{ typeof(GetStaticObjectFieldDelegate), 140 },
			{ typeof(GetStaticBooleanFieldDelegate), 141 },
			{ typeof(GetStaticByteFieldDelegate), 142 },
			{ typeof(GetStaticCharFieldDelegate), 143 },
			{ typeof(GetStaticShortFieldDelegate), 144 },
			{ typeof(GetStaticIntFieldDelegate), 145 },
			{ typeof(GetStaticLongFieldDelegate), 146 },
			{ typeof(GetStaticFloatFieldDelegate), 147 },
			{ typeof(GetStaticDoubleFieldDelegate), 148 },
			{ typeof(SetStaticObjectFieldDelegate), 149 },
			{ typeof(SetStaticBooleanFieldDelegate), 150 },
			{ typeof(SetStaticByteFieldDelegate), 151 },
			{ typeof(SetStaticCharFieldDelegate), 152 },
			{ typeof(SetStaticShortFieldDelegate), 153 },
			{ typeof(SetStaticIntFieldDelegate), 154 },
			{ typeof(SetStaticLongFieldDelegate), 155 },
			{ typeof(SetStaticFloatFieldDelegate), 156 },
			{ typeof(SetStaticDoubleFieldDelegate), 157 },
			{ typeof(NewStringDelegate), 158 },
			{ typeof(GetStringLengthDelegate), 159 },
			{ typeof(GetStringCharsDelegate), 160 },
			{ typeof(ReleaseStringCharsDelegate), 161 },
			{ typeof(NewStringUtfDelegate), 162 },
			{ typeof(GetStringUtfLengthDelegate), 163 },
			{ typeof(GetStringUtfCharsDelegate), 164 },
			{ typeof(ReleaseStringUtfCharsDelegate), 165 },
			{ typeof(GetArrayLengthDelegate), 166 },
			{ typeof(NewObjectArrayDelegate), 167 },
			{ typeof(GetObjectArrayElementDelegate), 168 },
			{ typeof(SetObjectArrayElementDelegate), 169 },
			{ typeof(NewBooleanArrayDelegate), 170 },
			{ typeof(NewByteArrayDelegate), 171 },
			{ typeof(NewCharArrayDelegate), 172 },
			{ typeof(NewShortArrayDelegate), 173 },
			{ typeof(NewIntArrayDelegate), 174 },
			{ typeof(NewLongArrayDelegate), 175 },
			{ typeof(NewFloatArrayDelegate), 176 },
			{ typeof(NewDoubleArrayDelegate), 177 },
			{ typeof(GetBooleanArrayElementsDelegate), 178 },
			{ typeof(GetByteArrayElementsDelegate), 179 },
			{ typeof(GetCharArrayElementsDelegate), 180 },
			{ typeof(GetShortArrayElementsDelegate), 181 },
			{ typeof(GetIntArrayElementsDelegate), 182 },
			{ typeof(GetLongArrayElementsDelegate), 183 },
			{ typeof(GetFloatArrayElementsDelegate), 184 },
			{ typeof(GetDoubleArrayElementsDelegate), 185 },
			{ typeof(ReleaseBooleanArrayElementsDelegate), 186 },
			{ typeof(ReleaseByteArrayElementsDelegate), 187 },
			{ typeof(ReleaseCharArrayElementsDelegate), 188 },
			{ typeof(ReleaseShortArrayElementsDelegate), 189 },
			{ typeof(ReleaseIntArrayElementsDelegate), 190 },
			{ typeof(ReleaseLongArrayElementsDelegate), 191 },
			{ typeof(ReleaseFloatArrayElementsDelegate), 192 },
			{ typeof(ReleaseDoubleArrayElementsDelegate), 193 },
			{ typeof(GetBooleanArrayRegionDelegate), 194 },
			{ typeof(GetByteArrayRegionDelegate), 195 },
			{ typeof(GetCharArrayRegionDelegate), 196 },
			{ typeof(GetShortArrayRegionDelegate), 197 },
			{ typeof(GetIntArrayRegionDelegate), 198 },
			{ typeof(GetLongArrayRegionDelegate), 199 },
			{ typeof(GetFloatArrayRegionDelegate), 200 },
			{ typeof(GetDoubleArrayRegionDelegate), 201 },
			{ typeof(SetBooleanArrayRegionDelegate), 202 },
			{ typeof(SetByteArrayRegionDelegate), 203 },
			{ typeof(SetCharArrayRegionDelegate), 204 },
			{ typeof(SetShortArrayRegionDelegate), 205 },
			{ typeof(SetIntArrayRegionDelegate), 206 },
			{ typeof(SetLongArrayRegionDelegate), 207 },
			{ typeof(SetFloatArrayRegionDelegate), 208 },
			{ typeof(SetDoubleArrayRegionDelegate), 209 },
			{ typeof(RegisterNativesDelegate), 210 },
			{ typeof(UnregisterNativesDelegate), 211 },
			{ typeof(MonitorEnterDelegate), 212 },
			{ typeof(MonitorExitDelegate), 213 },
			{ typeof(GetVirtualMachineDelegate), 214 },
			{ typeof(GetStringRegionDelegate), 215 },
			{ typeof(GetStringUtfRegionDelegate), 216 },
			{ typeof(GetPrimitiveArrayCriticalDelegate), 217 },
			{ typeof(ReleasePrimitiveArrayCriticalDelegate), 218 },
			{ typeof(GetStringCriticalDelegate), 219 },
			{ typeof(ReleaseStringCriticalDelegate), 220 },
			{ typeof(NewWeakGlobalRefDelegate), 221 },
			{ typeof(DeleteWeakGlobalRefDelegate), 222 },
			{ typeof(ExceptionCheckDelegate), 223 },
			{ typeof(NewDirectByteBufferDelegate), 224 },
			{ typeof(GetDirectBufferAddressDelegate), 225 },
			{ typeof(GetDirectBufferCapacityDelegate), 226 },
			{ typeof(GetObjectRefTypeDelegate), 227 },
			// JNI 0x00090000
			{ typeof(GetModuleDelegate), 228 },
			// JNI 0x00130000
			{ typeof(IsVirtualThreadDelegate), 229 },
		};

		/// <summary>
		/// Cancellation token.
		/// </summary>
		private readonly CancellationTokenSource _cancellation = new();
		/// <summary>
		/// Class cache.
		/// </summary>
		private readonly ClassCache<JClassObject> _classes = new();
		/// <summary>
		/// Delegate cache.
		/// </summary>
		private readonly DelegateHelperCache _delegateCache = new();
		/// <summary>
		/// Main <see cref="JEnvironment"/> instance.
		/// </summary>
		private readonly JEnvironment _env;
		/// <inheritdoc cref="JEnvironment.Reference"/>
		public readonly JEnvironmentRef Reference;
		/// <summary>
		/// Managed thread.
		/// </summary>
		public readonly Thread Thread;
		/// <inheritdoc cref="IEnvironment.Version"/>
		public readonly Int32 Version;

		/// <inheritdoc cref="JEnvironment.VirtualMachine"/>
		public readonly JVirtualMachine VirtualMachine;

		/// <summary>
		/// Object cache.
		/// </summary>
		private LocalCache _objects = new();
		/// <summary>
		/// Amount of bytes used from stack.
		/// </summary>
		private Int32 _usedStackBytes;

		/// <summary>
		/// Ensured capacity.
		/// </summary>
		public Int32? Capacity => this._objects.Capacity;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="vm">A <see cref="JVirtualMachine"/> instance.</param>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> instance.</param>
		public JEnvironmentCache(JVirtualMachine vm, JEnvironment env, JEnvironmentRef envRef) : base(env)
		{
			this._env = env;

			this.VirtualMachine = vm;
			this.Reference = envRef;
			this.Version = JEnvironmentCache.GetVersion(envRef);
			this.Thread = Thread.CurrentThread;

			Task.Factory.StartNew(JEnvironmentCache.FinalizeCache, this, this._cancellation.Token);
			this.LoadMainClasses();
		}

		/// <summary>
		/// Retrieves a <typeparamref name="TDelegate"/> instance for <typeparamref name="TDelegate"/>.
		/// </summary>
		/// <typeparam name="TDelegate">Type of method delegate.</typeparam>
		/// <returns>A <typeparamref name="TDelegate"/> instance.</returns>
		public TDelegate GetDelegate<TDelegate>() where TDelegate : Delegate
		{
			ValidationUtilities.ThrowIfDifferentThread(this.Thread);
			Type typeOfT = typeof(TDelegate);
			Int32 index = JEnvironmentCache.delegateIndex[typeOfT];
			IntPtr ptr = this.GetPointer(index);
			return this._delegateCache.GetDelegate<TDelegate>(ptr);
		}
		/// <summary>
		/// Ensure local capacity to <paramref name="capacity"/>.
		/// </summary>
		/// <param name="capacity">Top of local references.</param>
		/// <exception cref="JniException"/>
		public void EnsureLocalCapacity(Int32 capacity)
		{
			if (capacity <= 0) return;
			ValidationUtilities.ThrowIfDifferentThread(this.Thread);
			EnsureLocalCapacityDelegate ensureLocalCapacity = this.GetDelegate<EnsureLocalCapacityDelegate>();
			JResult result = ensureLocalCapacity(this.Reference, capacity);
			if (result != JResult.Ok)
				throw new JniException(result);
			this._objects.Capacity = capacity;
		}
		/// <summary>
		/// Checks JNI occurred error.
		/// </summary>
		public void CheckJniError()
		{
			ExceptionOccurredDelegate exceptionOccurred = this.GetDelegate<ExceptionOccurredDelegate>();
			JThrowableLocalRef throwableRef = exceptionOccurred(this.Reference);
			if (throwableRef.Value == default) return;
			try
			{
				ExceptionClearDelegate exceptionClear = this.GetDelegate<ExceptionClearDelegate>();
				exceptionClear(this.Reference);
				using LocalFrame frame = new(this._env, 10);
				JClassLocalRef classRef = this._env.GetObjectClass(throwableRef.Value);
				JClassObject jClass = this.AsClassObject(classRef);
				String message = JEnvironmentCache.GetThrowableMessage(jClass, throwableRef);
				ThrowableObjectMetadata objectMetadata = new(jClass, message);
				JThrowableTypeMetadata throwableMetadata =
					MetadataHelper.GetMetadata(jClass.Hash) as JThrowableTypeMetadata ??
					(JThrowableTypeMetadata)MetadataHelper.GetMetadata<JThrowableObject>();
				JGlobalRef globalRef = this.CreateGlobalRef(throwableRef.Value);
				JGlobal jGlobalThrowable = new(this.VirtualMachine, objectMetadata, false, globalRef);
				throw throwableMetadata.CreateException(jGlobalThrowable, message);
			}
			finally
			{
				ThrowDelegate jThrow = this.GetDelegate<ThrowDelegate>();
				jThrow(this.Reference, throwableRef);
			}
		}
		/// <summary>
		/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference)
		{
			using JStringObject jString = JClassObject.GetClassName(this._env, classRef, out Boolean isPrimitive);
			using JNativeMemory<Byte> utf8Text = jString.GetNativeUtf8Chars();
			JClassObject jClass = isPrimitive ?
				this.GetPrimitiveClass(utf8Text.Values) :
				this.GetClass(utf8Text.Values, keepReference ? classRef : default);
			if (keepReference && jClass.InternalReference == default) jClass.SetValue(classRef);
			return jClass;
		}
		/// <inheritdoc cref="IEnvironment.JniSecure"/>
		public Boolean JniSecure() => this.Thread.ManagedThreadId == Environment.CurrentManagedThreadId;
		/// <inheritdoc cref="JEnvironment.SetObjectCache(LocalCache?)"/>
		public void SetObjectCache(LocalCache localCache) { this._objects = localCache; }
		/// <summary>
		/// Retrieves local cache.
		/// </summary>
		public LocalCache GetLocalCache() => this._objects;
		/// <summary>
		/// Class cache cache.
		/// </summary>
		public ClassCache GetClassCache() => this._classes;
		/// <summary>
		/// Release all references.
		/// </summary>
		public void FreeReferences()
		{
			this._objects.ClearCache(this._env, true);
			this._cancellation.Cancel();
		}
		/// <summary>
		/// Creates a new local reference for <paramref name="result"/>.
		/// </summary>
		/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
		/// <param name="result">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="deleteGlobal">Indicates whether global reference must be deleted.</param>
		public void CreateLocalRef(JGlobalRef globalRef, JLocalObject? result, Boolean deleteGlobal = true)
		{
			if (globalRef == default || result is not null) return;
			try
			{
				JObjectLocalRef localRef = this._env.CreateLocalRef(globalRef.Value);
				JLocalObject jLocal = this.Register(result)!;
				jLocal.SetValue(localRef);
			}
			finally
			{
				if (deleteGlobal) this._env.DeleteGlobalRef(globalRef);
			}
		}
		/// <summary>
		/// Creates a global reference from <paramref name="localRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
		public JGlobalRef CreateGlobalRef(JObjectLocalRef localRef)
		{
			NewGlobalRefDelegate newGlobalRef = this.GetDelegate<NewGlobalRefDelegate>();
			JGlobalRef globalRef = newGlobalRef(this.Reference, localRef);
			if (globalRef == default) this.CheckJniError();
			return globalRef;
		}
		/// <summary>
		/// Removes <paramref name="jLocal"/> from current thread.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		public void Remove(JLocalObject? jLocal)
		{
			if (jLocal is null) return;
			this._objects.Remove(jLocal.InternalReference);
			if (jLocal is JClassObject)
				this._classes.Unload(
					NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(jLocal.InternalReference));
		}
		/// <summary>
		/// Retrieves <see cref="AccessCache"/> for <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>A <see cref="AccessCache"/> instance.</returns>
		/// <exception cref="ArgumentException">Throw if <see cref="AccessCache"/> is not found.</exception>
		public AccessCache GetAccess(INativeTransaction jniTransaction, JClassObject jClass)
		{
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			return this._classes[classRef] ?? this.VirtualMachine.GetAccess(classRef) ??
				throw new ArgumentException("Invalid class object.", nameof(jClass));
		}
		/// <summary>
		/// Attempts to get the value associated with the specified hash from the cache.
		/// </summary>
		/// <param name="hash">The hash class to get.</param>
		/// <param name="jClass"></param>
		/// <returns>
		/// <see langword="true"/> if the hash was found in the cache; otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean TryGetClass(String hash, [NotNullWhen(true)] out JClassObject? jClass)
			=> this._classes.TryGetValue(hash, out jClass);
		/// <summary>
		/// Creates an object from given reference.
		/// </summary>
		/// <typeparam name="TResult">A <see cref="IDataType"/> type.</typeparam>
		/// <param name="localRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="register">Indicates whether object must be registered.</param>
		/// <returns>A <typeparamref name="TResult"/> instance.</returns>
		public TResult? CreateObject<TResult>(JObjectLocalRef localRef, Boolean register)
			where TResult : IDataType<TResult>
		{
			this.CheckJniError();
			if (localRef == default) return default;

			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetMetadata<TResult>();
			JClassObject jClass;
			if (metadata.Modifier == JTypeModifier.Final)
			{
				jClass = this.GetClass<TResult>();
			}
			else
			{
				JClassLocalRef classRef = this._env.GetObjectClass(localRef);
				try
				{
					jClass = this.GetClass(classRef, false);
				}
				finally
				{
					this._env.DeleteLocalRef(classRef.Value);
				}
			}
			TResult result = (TResult)(Object)metadata.CreateInstance(jClass, localRef, true);
			if (localRef != (result as JLocalObject)!.InternalReference && register)
				this._env.DeleteLocalRef(localRef);
			return register ? this.Register(result) : result;
		}
		/// <inheritdoc cref="JEnvironment.LoadClass(JClassObject?)"/>
		public void LoadClass(JClassObject? jClass)
		{
			if (jClass is null) return;
			this._classes[jClass.Hash] = jClass;
			this.VirtualMachine.LoadGlobal(jClass);
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> reference for primitive class.
		/// </summary>
		/// <param name="signature">Primitive signature.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public JClassLocalRef FindPrimitiveClass(Byte signature)
		{
			using JClassObject wrapperClass = signature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => this.GetClass<JBooleanObject>(),
				UnicodePrimitiveSignatures.ByteSignatureChar => this.GetClass<JByteObject>(),
				UnicodePrimitiveSignatures.CharSignatureChar => this.GetClass<JCharacterObject>(),
				UnicodePrimitiveSignatures.DoubleSignatureChar => this.GetClass<JDoubleObject>(),
				UnicodePrimitiveSignatures.FloatSignatureChar => this.GetClass<JFloatObject>(),
				UnicodePrimitiveSignatures.IntSignatureChar => this.GetClass<JIntegerObject>(),
				UnicodePrimitiveSignatures.LongSignatureChar => this.GetClass<JLongObject>(),
				UnicodePrimitiveSignatures.ShortSignatureChar => this.GetClass<JShortObject>(),
				_ => this.GetClass<JVoidObject>(),
			};
			JObjectLocalRef localRef =
				this.GetStaticObjectField(wrapperClass, InternalFunctionCache.PrimitiveTypeDefinition);
			return NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(in localRef);
		}
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
			if (classRef.Value != default) return classRef;
			classRef = this.FindClass(jClass);
			jClass.SetValue(classRef);
			this.Register(jClass);
			return classRef;
		}
		/// <summary>
		/// Retrieves primitive class instance for <paramref name="className"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		/// <exception cref="ArgumentException">Non-primitive class.</exception>
		private JClassObject GetPrimitiveClass(ReadOnlySpan<Byte> className)
			=> className.Length switch
			{
				3 => className[0] == 0x69 /*i*/ ?
					this.IntPrimitive :
					throw new ArgumentException("Invalid primitive type."),
				4 => className[0] switch
				{
					0x62 //b
						=> this.BooleanPrimitive,
					0x63 //c
						=> this.CharPrimitive,
					0x6C //l
						=> this.LongPrimitive,
					_ => throw new ArgumentException("Invalid primitive type."),
				},
				5 => className[0] switch
				{
					0x66 //f
						=> this.FloatPrimitive,
					0x73 //l
						=> this.ShortPrimitive,
					_ => throw new ArgumentException("Invalid primitive type."),
				},
				6 => className[0] == 0x64 /*d*/ ?
					this.DoublePrimitive :
					throw new ArgumentException("Invalid primitive type."),
				7 => className[0] == 0x62 /*b*/ ?
					this.BooleanPrimitive :
					throw new ArgumentException("Invalid primitive type."),
				_ => throw new ArgumentException("Invalid primitive type."),
			};
		/// <summary>
		/// Retrieves class instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetClass(ReadOnlySpan<Byte> className, JClassLocalRef classRef)
		{
			CStringSequence classInformation = MetadataHelper.GetClassInformation(className);
			if (!this.TryGetClass(classInformation.ToString(), out JClassObject? jClass))
				jClass = new(this.ClassObject, new TypeInformation(classInformation), classRef);
			return jClass;
		}

		/// <summary>
		/// Retrieves a <see cref="JVirtualMachine"/> from given <paramref name="jEnv"/>.
		/// </summary>
		/// <param name="jEnv">A <see cref="JEnvironmentRef"/> reference.</param>
		/// <returns>A <see cref="IVirtualMachine"/> instance.</returns>
		public static IVirtualMachine GetVirtualMachine(JEnvironmentRef jEnv)
		{
			Int32 index = JEnvironmentCache.delegateIndex[typeof(GetVirtualMachineDelegate)];
			GetVirtualMachineDelegate getVirtualMachine =
				jEnv.Reference.Reference[index].GetUnsafeDelegate<GetVirtualMachineDelegate>()!;
			JResult result = getVirtualMachine(jEnv, out JVirtualMachineRef vmRef);
			if (result == JResult.Ok)
				return JVirtualMachine.GetVirtualMachine(vmRef);
			throw new JniException(result);
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> using <paramref name="classNameCtx"/> as class name.
		/// </summary>
		/// <param name="classNameCtx">A <see cref="IReadOnlyFixedMemory"/> instance.</param>
		/// <param name="cache">Current <see cref="JEnvironmentCache"/> instance.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public static JClassLocalRef FindClass(in IReadOnlyFixedMemory classNameCtx, JEnvironmentCache cache)
		{
			FindClassDelegate findClass = cache.GetDelegate<FindClassDelegate>();
			JClassLocalRef result = findClass(cache.Reference, (ReadOnlyValPtr<Byte>)classNameCtx.Pointer);
			if (result.Value == default) cache.CheckJniError();
			return result;
		}

		/// <summary>
		/// Retrieves the JNI function pointer for <paramref name="index"/>.
		/// </summary>
		/// <param name="index">JNI function index.</param>
		/// <returns>JNI function pointer.</returns>
		private IntPtr GetPointer(Int32 index)
		{
			Int32 lastNormalIndex = JEnvironmentCache.delegateIndex[typeof(GetObjectRefTypeDelegate)];
			if (index <= lastNormalIndex)
				return this.Reference.Reference.Reference[index];
			index -= lastNormalIndex;
			return this.Reference.Reference.GetAdditionalPointers(this.Version)[index];
		}
		/// <summary>
		/// Indicates whether current JNI call must use <see langword="stackalloc"/> or <see langword="new"/> to
		/// hold JNI call parameter.
		/// </summary>
		/// <param name="requiredBytes">Output. Number of bytes to allocate.</param>
		/// <returns>
		/// <see langword="true"/> if current call must use <see langword="stackalloc"/>; otherwise,
		/// <see langword="false"/>.
		/// </returns>
		private Boolean UseStackAlloc(Int32 requiredBytes)
		{
			this._usedStackBytes += requiredBytes;
			if (this._usedStackBytes <= JEnvironmentCache.MaxStackBytes) return true;
			this._usedStackBytes -= requiredBytes;
			return false;
		}
		/// <summary>
		/// Retrieves <see cref="JClassLocalRef"/> reference for given instance.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		private JClassLocalRef FindClass(JClassObject jClass)
			=> jClass.ClassSignature.Length != 1 ?
				jClass.Name.WithSafeFixed(this, JEnvironmentCache.FindClass) :
				this.FindPrimitiveClass(jClass.ClassSignature[0]);

		/// <summary>
		/// Retrieves JNI version for <paramref name="envRef"/>.
		/// </summary>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> instance.</param>
		/// <returns>JNI version for <paramref name="envRef"/>.</returns>
		private static Int32 GetVersion(JEnvironmentRef envRef)
		{
			GetVersionDelegate getVersion =
				envRef.Reference.Reference.GetVersionPointer.GetUnsafeDelegate<GetVersionDelegate>()!;
			return getVersion(envRef);
		}
		/// <summary>
		/// Cache finalize method.
		/// </summary>
		/// <param name="obj">A <see cref="JEnvironmentCache"/> instance.</param>
		private static void FinalizeCache(Object? obj)
		{
			if (obj is not JEnvironmentCache cache) return;
			cache.Thread.Join();
			JVirtualMachine.RemoveEnvironment(cache.VirtualMachine.Reference, cache.Reference);
		}
		/// <summary>
		/// Retrieves throwable message.
		/// </summary>
		/// <param name="jClass">Throwable class.</param>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <returns>Throwable message.</returns>
		private static String GetThrowableMessage(JClassObject jClass, JThrowableLocalRef throwableRef)
		{
			using JStringObject throwableMessage = JThrowableObject.GetThrowableMessage(jClass, throwableRef);
			return throwableMessage.Value;
		}
	}
}