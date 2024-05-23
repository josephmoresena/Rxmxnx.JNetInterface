namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Utility class for argument validation in current implementation.
/// </summary>
public static class ImplementationValidationUtilities
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
		if (jObject is not null && jObject.IsProxy)
			throw new ArgumentException($"Invalid JReferenceObject ({nameofObject}).");
	}
	/// <summary>
	/// Throws an exception if <paramref name="definition"/> doesn't match with <paramref name="otherDefinition"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	/// <param name="otherDefinition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	/// <exception cref="ArgumentException">
	/// Throws an exception if <paramref name="definition"/> doesn't match with <paramref name="otherDefinition"/>.
	/// </exception>
	public static void ThrowIfNotMatchDefinition(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
	{
		if (definition.Hash != otherDefinition.Hash)
			throw new ArgumentException($"[{definition}] Expected: [{otherDefinition}].");
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
		if (jObject.IsDefault && JObject.IsNullOrDefault(jObject))
			throw new ArgumentException(message ?? "Disposed JReferenceObject.");
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
		if (thread != Thread.CurrentThread)
			throw new InvalidOperationException(
				$"JNI Environment ({envRef}) is assigned to another thread. Expected: {thread.ManagedThreadId} Current: {Environment.CurrentManagedThreadId}.");
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
		if (length < 0)
			throw new ArgumentException("Array length must be zero or positive.", nameof(length));
	}
	/// <summary>
	/// Throws an exception if JVM is not alive.
	/// </summary>
	/// <param name="isAlive">Indicates whether JVM remains alive.</param>
	/// <exception cref="InvalidOperationException">Throws an exception if JVM is not alive.</exception>
	public static void ThrowIfInvalidVirtualMachine(Boolean isAlive)
	{
		if (!isAlive)
			throw new InvalidOperationException("Current JVM is not alive.");
	}
	/// <summary>
	/// Throws an exception if current thread is not attached to JVM.
	/// </summary>
	/// <param name="isAttached">Indicates whether current thread is attached to a JVM.</param>
	/// <exception cref="InvalidOperationException">Throws an exception if current thread is not attached to JVM</exception>
	public static void ThrowIfNotAttached(Boolean isAttached)
	{
		if (!isAttached)
			throw new InvalidOperationException("Current thread is not attached.");
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
		if (!jniSecure)
			throw new InvalidOperationException($"Current JNI status is invalid to call {functionName}.");
	}
	/// <summary>
	/// Throws an exception if JNI execution is not avaliable in current version.
	/// </summary>
	/// <param name="functionName">Name of JNI function.</param>
	/// <param name="requiredVersion">Requried version to execute JNI function.</param>
	/// <param name="currentVersion">Current version of JNI.</param>
	/// <exception cref="NotImplementedException">
	/// Throws an exception if JNI execution is not avaliable in current version.
	/// </exception>
	public static void ThrowIfInvalidVersion(String functionName, Int32 requiredVersion, Int32 currentVersion)
	{
		if (currentVersion < requiredVersion)
			throw new InvalidOperationException(
				$"Current JNI version (0x{currentVersion:x8}) is invalid to call {functionName}. JNI required: 0x{requiredVersion:x8}");
	}
	/// <summary>
	/// Throws an exception if <paramref name="version"/> is invalid.
	/// </summary>
	/// <param name="version">JNI version.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="version"/> is invalid.
	/// </exception>
	public static Int32 ThrowIfInvalidVersion(Int32 version)
		=> version > 0 ? version : throw new InvalidOperationException();
}