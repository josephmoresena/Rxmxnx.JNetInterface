using JObject = Rxmxnx.JNetInterface.Native.JReferenceObject;

namespace Rxmxnx.JNetInterface.Internal;

internal static partial class NativeCallUtilities
{
	/// <summary>
	/// Populates a span of <see cref="JLocalObject"/> instances using the provided
	/// tokens and the given environment and cache.
	/// </summary>
	/// <param name="env">The thread environment used for native operations.</param>
	/// <param name="cache">The local cache for managing object references.</param>
	/// <param name="tokens">A readonly span containing reference type tokens.</param>
	/// <param name="buffer">A span to store the created <see cref="JLocalObject"/> instances, updated in-place.</param>
	private static Span<JObject?> GetObjects(INativeThread env, AlienLocalCache cache,
		ReadOnlySpan<JReferenceTypeToken> tokens, Span<JObject?> buffer)
	{
		Debug.Assert(tokens.Length <= buffer.Length);
		for (Int32 i = 0; i < tokens.Length; i++)
			buffer[i] = NativeCallUtilities.CreateAlienObject(env, cache, tokens[i]);
		return buffer[..tokens.Length];
	}
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> instance for <paramref name="token"/>.
	/// </summary>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	/// <param name="cache">A <see cref="AlienLocalCache"/> instance.</param>
	/// <param name="token">A <see cref="JReferenceTypeToken"/> reference.</param>
	/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="token"/>.</returns>
	private static JObject? CreateAlienObject(INativeThread env, AlienLocalCache cache, JReferenceTypeToken token)
	{
		if (token.Reference == default) return default;
		if (token.NativeType == JNativeType.JClass)
			return NativeCallUtilities.CreateAlienClass(env, cache, new(token.Reference), true);
		JObjectLocalRef localRef = token.Reference;
		JReferenceTypeMetadata? typeMetadata = MetadataHelper.GetExactMetadata(token);
		if (typeMetadata is { Modifier: JTypeModifier.Final, })
			return NativeCallUtilities.CreateFinalAlienObject(env, cache, typeMetadata, localRef);
		JClassObject jClass =
			NativeCallUtilities.GetObjectClass(env, localRef, out JReferenceTypeMetadata classMetadata, true);
		return NativeCallUtilities.CreateAlienObject(cache, jClass, localRef, classMetadata, typeMetadata);
	}
	/// <summary>
	/// Retrieves initial final <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
	/// </summary>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	/// <param name="cache">A <see cref="AlienLocalCache"/> instance.</param>
	/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JObject CreateFinalAlienObject(INativeThread env, AlienLocalCache cache,
		JReferenceTypeMetadata typeMetadata, JObjectLocalRef localRef)
	{
		JClassObject jClass = typeMetadata.GetClass(env);
		NativeCallUtilities.ThrowIfNotLocalReference(env, localRef);
		return NativeCallUtilities.CreateAlienObject(cache, jClass, localRef, typeMetadata);
	}
	/// <summary>
	/// Throws an exception if <paramref name="localRef"/> is not a class reference.
	/// </summary>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="localRef"/> is not a class reference.
	/// </exception>
	private static void ThrowIfNotClassObject(INativeThread env, JObjectLocalRef localRef)
	{
		NativeCallUtilities.ThrowIfNotLocalReference(env, localRef);
		JClassObject jClass = NativeCallUtilities.CheckClassRefNativeCallEnabled ?
			env.GetObjectClass(localRef) :
			env.ClassFeature.ClassObject;

		if (jClass.Name.AsSpan().SequenceEqual(env.ClassFeature.ClassObject.Name)) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String objectClassName = ClassNameHelper.GetClassName(jClass.ClassSignature);
		String className = ClassNameHelper.GetClassName(env.ClassFeature.ClassObject.ClassSignature);
		String message = resource.NotTypeObject(objectClassName, className);
		throw new ArgumentException(message);
	}
}