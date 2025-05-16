namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utility class for argument validation in current implementation.
/// </summary>
internal static class ImplementationValidationUtilities
{
	/// <summary>
	/// Throws an exception if <paramref name="jObject"/> is proxy.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="nameofObject">Name of <paramref name="jObject"/>.</param>
	/// <exception cref="ArgumentException">Throws an exception if <paramref name="jObject"/> is proxy.</exception>
	public static void ThrowIfProxy(JReferenceObject? jObject,
		[CallerArgumentExpression(nameof(jObject))] String nameofObject = "")
	{
		if (jObject is null || !jObject.IsProxy) return;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new ProxyObjectException(resource.InvalidReferenceObject, nameofObject);
	}
	/// <summary>
	/// Throws an exception if <paramref name="objectMetadata"/> is proxy.
	/// </summary>
	/// <param name="objectMetadata">A <see cref="ObjectMetadata"/> instance.</param>
	/// <param name="nameofObject">Name of <paramref name="objectMetadata"/>.</param>
	/// <exception cref="ArgumentException">Throws an exception if <paramref name="objectMetadata"/> is proxy.</exception>
	public static void ThrowIfProxy(ObjectMetadata? objectMetadata,
		[CallerArgumentExpression(nameof(objectMetadata))] String nameofObject = "")
	{
		if (objectMetadata is null || !objectMetadata.FromProxy.GetValueOrDefault()) return;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new ProxyObjectException(resource.InvalidReferenceObject, nameofObject);
	}
	/// <summary>
	/// Throws an exception if <paramref name="definition"/> doesn't match with <paramref name="otherDefinition"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	/// <param name="otherDefinition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="definition"/> doesn't match with <paramref name="otherDefinition"/>.
	/// </exception>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static void ThrowIfNotMatchDefinition(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
	{
		if (definition.Hash == otherDefinition.Hash) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String message = resource.DefinitionNotMatch(definition, otherDefinition);
		throw new ArgumentException(message);
	}
	/// <summary>
	/// Throws an exception if <paramref name="result"/> is not <see cref="JResult.Ok"/>.
	/// </summary>
	/// <param name="result">A <see cref="JResult"/> value.</param>
	/// <exception cref="JniException">
	/// Throws an exception if <paramref name="result"/> is not <see cref="JResult.Ok"/>.
	/// </exception>
	public static void ThrowIfInvalidResult(JResult result)
	{
		JniException? exception = result;
		if (exception is not null)
			throw exception;
	}
	/// <summary>
	/// Throws an exception if <paramref name="jObject"/> is default.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="message">Exception message.</param>
	/// <exception cref="InvalidOperationException">Throws an exception if <paramref name="jObject"/> is default.</exception>
	public static void ThrowIfDefault(JReferenceObject jObject, String? message = default)
	{
		if (!jObject.IsDefault || !JObject.IsNullOrDefault(jObject)) return;
		throw new ArgumentException(message ?? IMessageResource.GetInstance().DisposedObject);
	}
	/// <summary>
	/// Throws an exception if <paramref name="thread"/> is different to current thread.
	/// </summary>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="thread">A <see cref="Thread"/> instance.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="thread"/> is different to current
	/// thread.
	/// </exception>
	public static void ThrowIfDifferentThread(JEnvironmentRef envRef, Thread thread)
	{
		if (thread == Thread.CurrentThread) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String message = resource.DifferentThread(envRef, thread.ManagedThreadId);
		throw new DifferentThreadException(message);
	}
	/// <summary>
	/// Throws an exception if <paramref name="length"/> is invalid.
	/// </summary>
	/// <param name="length">Array length.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="length"/> is invalid.
	/// </exception>
	public static void ThrowIfInvalidArrayLength(Int32 length)
	{
		if (length >= 0) return;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new ArgumentException(resource.InvalidArrayLength, nameof(length));
	}
	/// <summary>
	/// Throws an exception if JVM is not alive.
	/// </summary>
	/// <param name="isAlive">Indicates whether JVM remains alive.</param>
	/// <exception cref="InvalidOperationException">Throws an exception if JVM is not alive.</exception>
	public static void ThrowIfInvalidVirtualMachine(Boolean isAlive)
	{
		if (isAlive) return;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new RunningStateException(resource.DeadVirtualMachine);
	}
	/// <summary>
	/// Throws an exception if current thread is not attached to JVM.
	/// </summary>
	/// <param name="isAttached">Indicates whether current thread is attached to a JVM.</param>
	/// <exception cref="InvalidOperationException">Throws an exception if current thread is not attached to JVM</exception>
	public static void ThrowIfNotAttached(Boolean isAttached)
	{
		if (isAttached) return;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new RunningStateException(resource.NotAttachedThread);
	}
	/// <summary>
	/// Throws an exception if JNI execution is not secure.
	/// </summary>
	/// <param name="functionName">Name of JNI function.</param>
	/// <param name="jniSecure">Indicates whether JNI execution is safe.</param>
	/// <exception cref="NotImplementedException">
	/// Throws an exception if JNI execution is not secure.
	/// </exception>
	public static void ThrowIfUnsafe(String functionName, Boolean jniSecure)
	{
		if (jniSecure) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String message = resource.CallOnUnsafe(functionName);
		throw new UnsafeStateException(message);
	}
	/// <summary>
	/// Throws an exception if JNI execution is not avaliable in current version.
	/// </summary>
	/// <param name="functionName">Name of JNI function.</param>
	/// <param name="requiredVersion">Required version to execute JNI function.</param>
	/// <param name="currentVersion">Current version of JNI.</param>
	/// <exception cref="NotImplementedException">
	/// Throws an exception if JNI execution is not avaliable in current version.
	/// </exception>
	public static void ThrowIfInvalidVersion(String functionName, Int32 requiredVersion, Int32 currentVersion)
	{
		if (currentVersion >= requiredVersion) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String message = resource.InvalidCallVersion(currentVersion, functionName, requiredVersion);
		throw new JavaVersionException(message);
	}
	/// <summary>
	/// Throws an exception if <paramref name="version"/> is invalid.
	/// </summary>
	/// <param name="version">JNI version.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="version"/> is invalid.
	/// </exception>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Int32 ThrowIfInvalidVersion(Int32 version)
	{
		if (version > 0) return version;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new JavaVersionException(resource.IncompatibleLibrary);
	}
}