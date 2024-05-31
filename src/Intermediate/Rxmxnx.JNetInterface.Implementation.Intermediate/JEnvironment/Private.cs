namespace Rxmxnx.JNetInterface;

[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
partial class JEnvironment
{
	/// <summary>
	/// <see cref="JEnvironment"/> cache.
	/// </summary>
	private readonly EnvironmentCache _cache;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cache">A <see cref="JEnvironment"/> reference.</param>
	private JEnvironment(EnvironmentCache cache) => this._cache = cache;

	/// <summary>
	/// Tests whether two references refer to the same object.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="otherRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if both references refer to the same object; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	private unsafe Boolean IsSame(JObjectLocalRef localRef, JObjectLocalRef otherRef)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.IsSameObjectInfo);
		JBoolean result = nativeInterface.ReferenceFunctions.IsSameObject(this._cache.Reference, localRef, otherRef);
		this._cache.CheckJniError();
		return result.Value;
	}
	/// <summary>
	/// Creates a new local reference frame.
	/// </summary>
	/// <param name="capacity">Frame capacity.</param>
	/// <exception cref="InvalidOperationException"/>
	/// <exception cref="JniException"/>
	private unsafe void CreateLocalFrame(Int32 capacity)
	{
		ref readonly NativeInterface nativeInterface =
			ref this._cache.GetNativeInterface<NativeInterface>(NativeInterface.PushLocalFrameInfo);
		JResult result = nativeInterface.ReferenceFunctions.PushLocalFrame(this.Reference, capacity);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
	}
	/// <summary>
	/// Creates a new global reference to <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JGlobalRef CreateGlobalRef(JReferenceObject jLocal)
		=> this._cache.CreateGlobalRef(jLocal.As<JObjectLocalRef>());
	/// <summary>
	/// Retrieves the <see cref="JArrayTypeMetadata"/> instance for given <paramref name="arraySignature"/>.
	/// </summary>
	/// <param name="arraySignature">JNI array signature.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	private JArrayTypeMetadata GetArrayTypeMetadata(ReadOnlySpan<Byte> arraySignature)
	{
		// Element signature is Array signature without [ prefix.
		ReadOnlySpan<Byte> elementSignature = arraySignature[1..];
		if (elementSignature[0] == UnicodeObjectSignatures.ArraySignaturePrefixChar)
		{
			// Is well-known array class? Primitive arrays are always well-known.
			if (MetadataHelper.GetArrayMetadata(elementSignature) is { } arrayTypeMetadata)
				return arrayTypeMetadata;

			// Iterates over array element.
			return MetadataHelper.GetArrayMetadata(this.GetArrayTypeMetadata(elementSignature)) ??
				MetadataHelper.ObjectArrayArrayMetadata;
		}

		// Object class name is signature without L prefix and ; suffix.
		ReadOnlySpan<Byte> elementClassName = elementSignature[1..^1];
		JReferenceTypeMetadata? elementMetadata = MetadataHelper.GetMetadata(elementClassName);
		if (elementMetadata is not null) // Element is a well-known class.
			return elementMetadata.GetArrayMetadata() ?? MetadataHelper.ObjectArrayMetadata;

		// Element class is not well-known. 
		JClassObject elementClass = this._cache.GetClass(elementClassName);
		elementMetadata = !elementClass.IsInterface ?
			JEnvironment.GetSuperClassMetadata(elementClass) :
			this.GetSuperInterfaceMetadata(elementClass);
		return elementMetadata?.GetArrayMetadata() ?? MetadataHelper.ObjectArrayMetadata;
	}
	/// <summary>
	/// Retrieves the <see cref="JInterfaceTypeMetadata"/> instance from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="hashes">Set of interface validated hashes.</param>
	/// <returns>A <see cref="JInterfaceTypeMetadata"/> instance.</returns>
	private JInterfaceTypeMetadata? GetSuperInterfaceMetadata(JClassObject jClass, HashSet<String>? hashes = default)
	{
		if (hashes is null && jClass.IsAnnotation) // Annotations should use java.lang.annotation.Annotation metadata.
			return (JInterfaceTypeMetadata)MetadataHelper.GetMetadata<JAnnotationObject>();

		hashes ??= [];
		using JArrayObject<JClassObject> interfaces = jClass.GetInterfaces();
		using LocalFrame _ = new(this, 2);
		foreach (JClassObject? interfaceClass in interfaces)
		{
			// Is checked interface? 
			if (hashes.Contains(interfaceClass!.Hash)) continue;

			// Is well-known interface ?
			if (MetadataHelper.GetMetadata(interfaceClass.Hash) is JInterfaceTypeMetadata superInterfaceMetadata)
				return superInterfaceMetadata;

			hashes.Add(interfaceClass.Hash);
			if (this.GetSuperInterfaceMetadata(interfaceClass, hashes) is { } metadata)
				return metadata;
		}
		return default;
	}
	/// <summary>
	/// Retrieves the <see cref="ThrowableException"/> pending exception.
	/// </summary>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	private ThrowableException? GetThrown()
	{
		ThrowableException? jniException = this._cache.Thrown as ThrowableException;
		if (jniException is not null || this._cache.Thrown is null) return jniException;
		if (!this._cache.JniSecure(JniSafetyLevels.ErrorSafe))
			throw this._cache.Thrown;
		return this.ParseException(this._cache.GetPendingException());
	}
	/// <summary>
	/// Parses <paramref name="throwableRef"/> to a <see cref="ThrowableException"/> instance.
	/// </summary>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	private ThrowableException? ParseException(JThrowableLocalRef throwableRef)
	{
		if (throwableRef.IsDefault) return default;
		ThrowableException jniException = this._cache.CreateThrowableException(throwableRef);
		this._cache.ThrowJniException(jniException, false);
		return jniException;
	}
	/// <summary>
	/// Sets <paramref name="throwableException"/> as pending exception.
	/// </summary>
	/// <param name="throwableException">A <see cref="ThrowableException"/> instance.</param>
	private void SetThrown(ThrowableException? throwableException)
	{
		if (this._cache.Thrown is CriticalException)
			throw this._cache.Thrown;
		this._cache.ThrowJniException(throwableException, false);
	}

	/// <summary>
	/// Retrieves the <see cref="JClassTypeMetadata"/> instance from <paramref name="jClass"/>
	/// superclass.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JClassTypeMetadata"/> instance.</returns>
	private static JClassTypeMetadata GetSuperClassMetadata(JClassObject jClass)
	{
		if (jClass.IsEnum) // Enums should use java.lang.Enum metadata.
			return (JClassTypeMetadata)MetadataHelper.GetMetadata<JEnumObject>();

		Boolean checkProxy = true;
		while (jClass.GetSuperClass() is { } superClass)
		{
			// Super class is object.
			if (UnicodeClassNames.Object.AsSpan().SequenceEqual(superClass.Name))
				break;

			// Super class is proxy.
			if (checkProxy && UnicodeClassNames.ProxyObject().SequenceEqual(superClass.Name))
			{
				using JArrayObject<JClassObject> interfaces = superClass.GetInterfaces();
				if (jClass.Environment.ClassFeature.GetTypeMetadata(interfaces.FirstOrDefault()) is
				    JInterfaceTypeMetadata interfaceMetadata)
					return interfaceMetadata.ProxyMetadata;
				break;
			}
			checkProxy = false;

			// Base class is well-known.
			if (MetadataHelper.GetMetadata(superClass.Name) is JClassTypeMetadata classMetadata)
				return classMetadata;

			jClass = superClass;
		}

		return (JClassTypeMetadata)MetadataHelper.GetMetadata<JLocalObject>();
	}
	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
#pragma warning disable CA1859
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
#pragma warning restore CA1859
}