namespace Rxmxnx.JNetInterface;

/// <summary>
/// The exception that is thrown when one of the arguments provided to a method not matched with environment type.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
public sealed class ProxyObjectException : ArgumentException
{
	/// <summary>
	/// The exception that is thrown when one of the arguments provided to a method not matched with environment type.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	public ProxyObjectException(String message) : base(message) { }
	/// <summary>
	/// The exception that is thrown when one of the arguments provided to a method not matched with environment type.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="paramName">The name of the parameter that caused the current exception.</param>
	public ProxyObjectException(String message, String paramName) : base(message, paramName) { }
}