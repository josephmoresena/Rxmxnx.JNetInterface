namespace Rxmxnx.JNetInterface;

/// <summary>
/// The exception that is thrown when a method call is invalid for JVM or thread state.
/// </summary>
/// <param name="message">The message that describes the error.</param>
public sealed class RunningStateException(String message) : InvalidOperationException(message);

/// <summary>
/// The exception that is thrown when a method call is invalid for an unsafe thread state.
/// </summary>
/// <param name="message">The message that describes the error.</param>
public sealed class UnsafeStateException(String message) : InvalidOperationException(message);

/// <summary>
/// The exception that is thrown when a method call is invalid for current Java version.
/// </summary>
/// <param name="message">The message that describes the error.</param>
public sealed class JavaVersionException(String message) : InvalidOperationException(message);

/// <summary>
/// The exception that is thrown when a method call occurs in an invalid thread.
/// </summary>
/// <param name="message">The message that describes the error.</param>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
public sealed class DifferentThreadException(String message) : InvalidOperationException(message);