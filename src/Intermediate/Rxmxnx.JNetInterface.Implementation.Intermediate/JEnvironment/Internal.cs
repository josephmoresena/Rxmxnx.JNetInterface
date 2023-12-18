namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
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
			this._cache.DeleteLocalRef(classRef.Value);
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
		CStringSequence sequence = MetadataHelper.GetHashSequence(utf8Text.Values);
		if (!this._cache.TryGetClass(sequence.ToString(), out JClassObject? jClass))
			jClass = new(this._cache.ClassObject, new TypeInformation(sequence));
		if (keepReference) jClass.SetValue(classRef);
		return this._cache.Register(jClass);
	}
}