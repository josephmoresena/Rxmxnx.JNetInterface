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
		/// Class cache cache.
		/// </summary>
		public ClassCache<JClassObject> GetClassCache() => this._classes;
		/// <summary>
		/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
		/// <param name="runtimeInformation">Runtime known type information.</param>
		/// <param name="deleteLocalRef">Indicates whether local class reference should be deleted.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference,
			WellKnownRuntimeTypeInformation runtimeInformation = default, Boolean deleteLocalRef = false)
		{
			Boolean isReferenceType = runtimeInformation.Kind is not null and not JTypeKind.Primitive;
			using JStringObject jString = this.GetClassName(classRef, isReferenceType, out Boolean isPrimitive);
			try
			{
				using JNativeMemory<Byte> utf8Text = jString.GetNativeUtf8Chars();
				JClassLocalRef usableClassRef = keepReference ? classRef : default;
				JClassObject jClass = isPrimitive ?
					this.GetPrimitiveClass(utf8Text.Values) :
					this.GetClass(utf8Text.Values, usableClassRef, runtimeInformation);
				if (JVirtualMachine.IsMainClass(jClass.Hash))
					this.LoadMainClass(jClass, classRef, deleteLocalRef);
				return jClass;
			}
			finally
			{
				this.FreeUnregistered(jString);
			}
		}
		/// <inheritdoc cref="JEnvironment.LoadClass(JClassObject?)"/>
		public void LoadClass(JClassObject? jClass)
		{
			if (jClass is null) return;
			this._classes[jClass.Hash] = jClass;
			this.VirtualMachine.LoadGlobal(jClass);
		}
		/// <inheritdoc cref="JEnvironment.LoadClass(JClassObject?)"/>
		/// <param name="frame">A <see cref="LocalFrame"/> instance.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		public void LoadClass(LocalFrame frame, JClassLocalRef classRef, JClassObject jClass)
		{
			this._classes[jClass.Hash] = jClass;
			this.VirtualMachine.LoadGlobal(jClass);
			if (classRef != jClass.LocalReference) return;
			JTrace.RegisterObject(jClass, frame.Id, frame.Name);
			frame[classRef.Value] = jClass.Lifetime.GetCacheable();
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> using <paramref name="className"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="signature">Class JNI signature.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public unsafe JClassLocalRef FindMainClass(CString className, CString signature)
		{
			JClassLocalRef classRef;
			fixed (Byte* ptr = &MemoryMarshal.GetReference(className.AsSpan()))
			{
				JTrace.FindClass(className);
				classRef = this.FindClass(ptr, true);
			}
			if (!classRef.IsDefault) return classRef;

			this._env.DescribeException();
			this.ClearException();

			IMessageResource resource = IMessageResource.GetInstance();
			String mainClassName = ClassNameHelper.GetClassName(signature);
			String message = resource.MainClassUnavailable(mainClassName);
			throw new NotSupportedException(message);
		}
		/// <summary>
		/// Retrieves class element from interfaces class array.
		/// </summary>
		/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> reference.</param>
		/// <param name="index">Element index.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetInterfaceClass(JArrayLocalRef arrayRef, Int32 index)
		{
			JObjectLocalRef localRef =
				this.GetObjectArrayElement(JObjectArrayLocalRef.FromReference(in arrayRef), index);
			return this.AsClassObject(JClassLocalRef.FromReference(in localRef), JTypeKind.Interface);
		}
		/// <summary>
		/// Reloads current class object.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>Current <see cref="JClassLocalRef"/> reference.</returns>
		public JClassLocalRef ReloadClass(JClassObject? jClass)
		{
			if (jClass is null) return default;
			Boolean isMainClass = JVirtualMachine.IsMainClass(jClass.Hash);
			JGlobal? jGlobal = isMainClass ? this.VirtualMachine.LoadGlobal(jClass) : default;
			JClassLocalRef classRef = jClass.As<JClassLocalRef>();
			Boolean findClass = classRef.IsDefault;

			if (jGlobal is not null)
			{
				if (jGlobal.IsDefault)
				{
					if (findClass) classRef = this.FindClass(jClass);
					ClassObjectMetadata classMetadata = (ClassObjectMetadata)jGlobal.ObjectMetadata;
					jGlobal.SetValue(this._env.GetMainClassGlobalRef(classMetadata, classRef, findClass));
					this.VirtualMachine.ReloadAccess(jClass.Hash);
				}
				// Always use the global reference if it is a main class.
				classRef = jGlobal.As<JClassLocalRef>();
			}
			else if (findClass)
			{
				classRef = this.FindClass(jClass);
				jClass.SetValue(classRef);
				// Registers class in the current local frame.
				this.Register(jClass);
			}

			return classRef;
		}
	}
}