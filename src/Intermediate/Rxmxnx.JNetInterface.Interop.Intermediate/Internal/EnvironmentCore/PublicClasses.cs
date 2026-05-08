namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed partial class EnvironmentCore
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
			if (this.Host.TypeManager.Contains(jClass.Hash))
				this.LoadMainClass(jClass, classRef, deleteLocalRef);
			return jClass;
		}
		finally
		{
			this.FreeUnregistered(jString);
		}
	}
	/// <summary>
	/// Loads in current cache given class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public void LoadClass(JClassObject? jClass)
	{
		if (jClass is null) return;
		this._classes[jClass.Hash] = jClass;
		this.Host.TypeManager.LoadGlobal(jClass);
	}
	/// <summary>
	/// Loads in current cache given class.
	/// </summary>
	/// <param name="frame">A <see cref="LocalFrame"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public void LoadClass(LocalFrame frame, JClassLocalRef classRef, JClassObject jClass)
	{
		this._classes[jClass.Hash] = jClass;
		this.Host.TypeManager.LoadGlobal(jClass);
		if (classRef.Value != jClass.LocalReference) return;
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
		if (classRef != default) return classRef;

		EnvironmentCore.DescribeException(this);
		this.ClearException();

		IMessageResource resource = IMessageResource.GetInstance();
		String mainClassName = ClassNameHelper.GetClassName(signature);
		String message = resource.MainClassUnavailable(mainClassName);
		throw new NotSupportedException(message);
	}
	/// <summary>
	/// Reloads current class object.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>Current <see cref="JClassLocalRef"/> reference.</returns>
	public JClassLocalRef ReloadClass(JClassObject? jClass)
	{
		if (jClass is null) return default;
		Boolean isMainClass = this.Host.TypeManager.Contains(jClass.Hash);
		JGlobal? jGlobal = isMainClass ? this.Host.TypeManager.LoadGlobal(jClass) : default;
		JClassLocalRef classRef = jClass.As<JClassLocalRef>();
		Boolean findClass = classRef == default;

		if (jGlobal is not null)
		{
			if (jGlobal.IsDefault)
			{
				if (findClass) classRef = this.FindClass(jClass);
				ClassObjectMetadata classMetadata = (ClassObjectMetadata)jGlobal.ObjectMetadata;
				jGlobal.SetValue(EnvironmentCore.GetMainClassGlobalRef(this, classMetadata, classRef, findClass));
				this.Host.TypeManager.ReloadAccess(jClass.Hash);
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