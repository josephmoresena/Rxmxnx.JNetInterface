namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Provides utilities for JNI method implementation.
/// </summary>
internal static partial class NativeCallUtilities
{
	/// <summary>
	/// Indicates whether native call adapters should check parameter references type.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean CheckRefTypeNativeCallEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableCheckRefTypeNativeCall", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether native call adapters should check parameter class object class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean CheckClassRefNativeCallEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> !AppContext.TryGetSwitch("JNetInterface.DisableCheckClassRefNativeCall", out Boolean disable) ||
				!disable;
	}

	/// <summary>
	/// Creates an object of type <paramref name="typeMetadata"/> using <paramref name="jClass"/> and
	/// <paramref name="localRef"/>.
	/// </summary>
	/// <param name="cache">A <see cref="AlienLocalCache"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classMetadata"><paramref name="jClass"/> type metadata.</param>
	/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>
	/// Creates an object of type <paramref name="typeMetadata"/> using <paramref name="jClass"/> and
	/// <paramref name="localRef"/>.
	/// </returns>
	public static JReferenceObject CreateAlienObject(AlienLocalCache cache, JClassObject jClass,
		JObjectLocalRef localRef, JReferenceTypeMetadata classMetadata, JReferenceTypeMetadata? typeMetadata = default)
	{
		JLocalObject jLocal = classMetadata.CreateInstance(jClass, localRef, true);
		Boolean isNotViewOutput = classMetadata.Kind is JTypeKind.Class or JTypeKind.Enum;
		if (isNotViewOutput && (typeMetadata is null || classMetadata == typeMetadata))
		{
			cache.RegisterAlien(localRef, jLocal);
			return jLocal;
		}
		typeMetadata ??= classMetadata;
		JReferenceObject result = typeMetadata.ParseInstance(jLocal, typeMetadata.Modifier != JTypeModifier.Final);
		cache.RegisterAlien(localRef, result as ILocalObject ?? jLocal);
		return result;
	}
	/// <summary>
	/// Retrieves initial <see cref="JLocalObject"/> instance for <paramref name="classRef"/>.
	/// </summary>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	/// <param name="cache">A <see cref="AlienLocalCache"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="validateReference">Indicates whether <paramref name="classRef"/> should be validated.</param>
	/// <returns>Initial <see cref="JClassObject"/> instance for <paramref name="classRef"/>.</returns>
	public static JClassObject? CreateAlienClass(INativeThread env, AlienLocalCache cache, JClassLocalRef classRef,
		Boolean validateReference)
	{
		if (classRef == default) return default;
		if (validateReference) NativeCallUtilities.ThrowIfNotClassObject(env, classRef.Value);
		JClassObject result = env.GetReferenceTypeClass(classRef, true);
		if (classRef.Value == result.LocalReference)
			// This call owns class.
			cache.RegisterAlien(classRef, result);
		else
			// This call does not own the class. A ClassView is registered instead.
			cache.RegisterAlien(classRef, new ClassView(classRef, result));
		return result;
	}
	/// <summary>
	/// Throws an exception if <paramref name="localRef"/> is not a local reference.
	/// </summary>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="localRef"/> is not a local reference.
	/// </exception>
	public static void ThrowIfNotLocalReference(INativeThread env, JObjectLocalRef localRef)
	{
		if (env.Version < IVirtualMachine.MinimalVersion || !NativeCallUtilities.CheckRefTypeNativeCallEnabled) return;
		if (env.GetReferenceType(localRef) == JReferenceType.LocalRefType) return;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new ArgumentException(resource.OnlyLocalReferencesAllowed);
	}
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	/// <param name="localRef">Object instance to get class.</param>
	/// <param name="typeMetadata">Output. Instantiation metadata.</param>
	/// <param name="validateReference">Indicates whether <paramref name="localRef"/> should be checked as local.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
	public static JClassObject GetObjectClass(INativeThread env, JObjectLocalRef localRef,
		out JReferenceTypeMetadata typeMetadata, Boolean validateReference)
	{
		if (env.LocalCache.GetLifetime(localRef)?.Class is { } jClass)
		{
			typeMetadata = env.ClassFeature.GetTypeMetadata(jClass);
			return jClass;
		}
		if (validateReference) NativeCallUtilities.ThrowIfNotLocalReference(env, localRef);
		return env.GetObjectClass(localRef, out typeMetadata);
	}
}