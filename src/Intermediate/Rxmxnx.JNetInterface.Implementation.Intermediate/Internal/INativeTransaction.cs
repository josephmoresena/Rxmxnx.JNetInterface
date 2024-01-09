namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a native transaction.
/// </summary>
internal interface INativeTransaction : IReferenceable<JniTransactionHandle>, IDisposable
{
	/// <summary>
	/// Indicates whether handle is used by current transaction.
	/// </summary>
	/// <param name="reference">A <see cref="IntPtr"/> handle.</param>
	/// <returns>
	/// <see langword="true"/> if handle is used by current transaction;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	Boolean Contains(IntPtr reference);
	/// <summary>
	/// Adds to current transaction <paramref name="localRef"/> reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns><paramref name="localRef"/>.</returns>
	JObjectLocalRef Add(JObjectLocalRef localRef);
	/// <summary>
	/// Adds to current transaction <paramref name="jReferenceObject"/> instance reference.
	/// </summary>
	/// <param name="jReferenceObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
	JObjectLocalRef Add(JReferenceObject? jReferenceObject)
	{
		if (jReferenceObject is null) return default;
		JObjectLocalRef localRef = jReferenceObject.As<JObjectLocalRef>();
		return this.Add(localRef);
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jLocal"/> instance reference.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
	JObjectLocalRef Add(JLocalObject? jLocal)
	{
		if (jLocal is null) return default;
		JObjectLocalRef localRef = jLocal.As<JObjectLocalRef>();
		return this.Add(localRef);
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jClass"/> instance reference.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	JClassLocalRef Add(JClassObject? jClass)
	{
		if (jClass is null) return default;
		JClassLocalRef classRef = jClass.Reference;
		this.Add(classRef.Value);
		return classRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jString"/> instance reference.
	/// </summary>
	/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
	/// <returns>A <see cref="JStringLocalRef"/> reference.</returns>
	JStringLocalRef Add(JStringObject? jString)
	{
		if (jString is null) return default;
		JStringLocalRef stringRef = jString.Reference;
		return this.Add(stringRef);
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jThrowable"/> instance reference.
	/// </summary>
	/// <param name="jThrowable">A <see cref="JThrowableObject"/> instance.</param>
	/// <returns>A <see cref="JArrayLocalRef"/> reference.</returns>
	JThrowableLocalRef Add(JThrowableObject? jThrowable)
	{
		if (jThrowable is null) return default;
		JThrowableLocalRef throwableRef = jThrowable.As<JThrowableLocalRef>();
		this.Add(throwableRef.Value);
		return throwableRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jArray"/> instance reference.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <returns>A <see cref="JArrayLocalRef"/> reference.</returns>
	JArrayLocalRef Add(JArrayObject? jArray)
	{
		if (jArray is null) return default;
		JArrayLocalRef arrayRef = jArray.Reference;
		this.Add(arrayRef.Value);
		return arrayRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="nativeRef"/> reference.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="INativeReferenceType"/> type.</typeparam>
	/// <param name="nativeRef">A <typeparamref name="TReference"/> reference.</param>
	/// <returns><paramref name="nativeRef"/>.</returns>
	TReference Add<TReference>(TReference nativeRef) where TReference : unmanaged, IObjectReferenceType<TReference>
	{
		this.Add(nativeRef.Value);
		return nativeRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jReferenceObject"/> instance reference.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IObjectReferenceType"/> type.</typeparam>
	/// <param name="jReferenceObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> reference.</returns>
	TReference Add<TReference>(JReferenceObject? jReferenceObject)
		where TReference : unmanaged, IObjectReferenceType<TReference>
	{
		if (jReferenceObject is null) return default;
		TReference nativeRef = jReferenceObject.As<TReference>();
		return this.Add(nativeRef);
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jGlobal"/> instance reference.
	/// </summary>
	/// <typeparam name="TGlobalReference">A <see cref="IObjectGlobalReferenceType"/> type.</typeparam>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <typeparamref name="TGlobalReference"/> reference.</returns>
	TGlobalReference Add<TGlobalReference>(JGlobalBase? jGlobal)
		where TGlobalReference : unmanaged, IObjectGlobalReferenceType<TGlobalReference>
	{
		if (jGlobal is null) return default;
		TGlobalReference nativeRef = jGlobal.As<TGlobalReference>();
		this.Add(nativeRef.Value);
		return nativeRef;
	}
	/// <summary>
	/// Adds to current transaction <paramref name="jArray"/> instance reference.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IArrayReferenceType"/> type.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <returns>A <typeparamref name="TReference"/> reference.</returns>
	TReference Add<TReference>(JArrayObject? jArray) where TReference : unmanaged, IArrayReferenceType<TReference>
	{
		if (jArray is null) return default;
		TReference arrayRef = jArray.As<TReference>();
		this.Add(arrayRef.Value);
		return arrayRef;
	}
}