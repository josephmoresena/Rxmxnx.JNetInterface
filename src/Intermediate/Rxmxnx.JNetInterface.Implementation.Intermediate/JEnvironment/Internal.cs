namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	/// <summary>
	/// Class cache.
	/// </summary>
	internal ClassCache ClassCache => this._cache.GetClassCache();
	/// <summary>
	/// Local cache.
	/// </summary>
	internal LocalCache LocalCache => this._cache.GetLocalCache();
	/// <inheritdoc cref="IClassProvider.ClassObject"/>
	internal JClassObject ClassObject => this._cache.ClassObject;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	internal JEnvironment(IVirtualMachine vm, JEnvironmentRef envRef)
		=> this._cache = new((JVirtualMachine)vm, envRef, new(this));

	/// <summary>
	/// Deletes local reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	internal void Delete(JObjectLocalRef localRef)
	{
		if (!this.JniSecure()) return;
		DeleteLocalRefDelegate deleteLocalRef = this._cache.GetDelegate<DeleteLocalRefDelegate>();
		deleteLocalRef(this.Reference, localRef);
	}
	/// <summary>
	/// Sets current object cache.
	/// </summary>
	/// <param name="localCache">A <see cref="LocalCache"/> instance.</param>
	internal void SetObjectCache(LocalCache localCache) => this._cache.SetObjectCache(localCache);
	/// <summary>
	/// Retrieves a global reference for given class name.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	internal JGlobalRef GetClassGlobalRef(CString className)
	{
		JClassLocalRef classRef = className.WithSafeFixed(this._cache, JEnvironmentCache.FindClass);
		try
		{
			JGlobalRef globalRef = this._cache.CreateGlobalRef(classRef.Value);
			return globalRef;
		}
		finally
		{
			this.DeleteLocalRef(classRef.Value);
		}
	}
	/// <summary>
	/// Deletes <paramref name="globalRef"/>.
	/// </summary>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	internal void DeleteGlobalRef(JGlobalRef globalRef)
	{
		DeleteGlobalRefDelegate deleteGlobalRef = this._cache.GetDelegate<DeleteGlobalRefDelegate>();
		deleteGlobalRef(this.Reference, globalRef);
	}
	/// <summary>
	/// Deletes <paramref name="weakRef"/>.
	/// </summary>
	/// <param name="weakRef">A <see cref="JWeakRef"/> reference.</param>
	internal void DeleteWeakGlobalRef(JWeakRef weakRef)
	{
		DeleteWeakGlobalRefDelegate deleteWeakGlobalRef = this._cache.GetDelegate<DeleteWeakGlobalRefDelegate>();
		deleteWeakGlobalRef(this.Reference, weakRef);
	}
	/// <summary>
	/// Retrieves field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal JFieldId GetFieldId(JFieldDefinition definition, JClassLocalRef classRef)
	{
		JFieldId fieldId = definition.Information.WithSafeFixed((this, classRef), JEnvironment.GetFieldId);
		if (fieldId == default) this._cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves static field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal JFieldId GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef)
	{
		JFieldId fieldId = definition.Information.WithSafeFixed((this, classRef), JEnvironment.GetStaticFieldId);
		if (fieldId == default) this._cache.CheckJniError();
		return fieldId;
	}
	/// <summary>
	/// Retrieves method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	internal JMethodId GetMethodId(JCallDefinition definition, JClassLocalRef classRef)
	{
		JMethodId methodId = definition.Information.WithSafeFixed((this, classRef), JEnvironment.GetMethodId);
		if (methodId == default) this._cache.CheckJniError();
		return methodId;
	}
	/// <summary>
	/// Retrieves static method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	internal JMethodId GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef)
	{
		JMethodId methodId = definition.Information.WithSafeFixed((this, classRef), JEnvironment.GetStaticMethodId);
		if (methodId == default) this._cache.CheckJniError();
		return methodId;
	}
	/// <summary>
	/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	internal JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference = false)
	{
		using JStringObject jString = JClassObject.GetClassName(this, classRef);
		using JNativeMemory<Byte> utf8Text = jString.GetUtf8Chars(JMemoryReferenceKind.Local);
		CStringSequence classInformation = MetadataHelper.GetClassInformation(utf8Text.Values);
		if (!this._cache.TryGetClass(classInformation.ToString(), out JClassObject? jClass))
			jClass = new(this._cache.ClassObject, new TypeInformation(classInformation),
			             keepReference ? classRef : default);
		return this._cache.Register(jClass);
	}
	/// <inheritdoc cref="IClassProvider.GetClass{TObject}()"/>
	internal JClassObject GetClass<TObject>() where TObject : JLocalObject, IReferenceType<TObject>
		=> this._cache.GetClass<TObject>();
	/// <summary>
	/// Deletes <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference to remove.</param>
	internal void DeleteLocalRef(JObjectLocalRef localRef)
	{
		if (localRef == default || !this.JniSecure()) return;
		DeleteLocalRefDelegate deleteLocalRef = this._cache.GetDelegate<DeleteLocalRefDelegate>();
		deleteLocalRef(this.Reference, localRef);
	}
	/// <summary>
	/// Retrieves object class reference.
	/// </summary>
	/// <param name="localRef">Object instance to get class.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	internal JClassLocalRef GetObjectClass(JObjectLocalRef localRef)
	{
		GetObjectClassDelegate getObjectClass = this._cache.GetDelegate<GetObjectClassDelegate>();
		JClassLocalRef classRef = getObjectClass(this.Reference, localRef);
		if (classRef.Value == default) this._cache.CheckJniError();
		return classRef;
	}
	/// <summary>
	/// Loads in current cache given class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	internal void LoadClass(JClassObject jClass) => this._cache.LoadClass(jClass);
}