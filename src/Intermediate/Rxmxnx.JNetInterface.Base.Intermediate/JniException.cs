namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents an error that occurs during a JNI call.
/// </summary>
public class JniException : Exception
{
	/// <summary>
	/// JNI result.
	/// </summary>
	public JResult Result { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="result">A <see cref="JResult"/> value.</param>
	/// <param name="message">The message that describes the error.</param>
	private protected JniException(JResult result = JResult.Error, String? message = default) : base(
		message ?? Enum.GetName(result))
		=> this.Result = result;

	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JResult"/> to
	/// <see cref="JniException"/>.
	/// </summary>
	/// <param name="result">A <see cref="JniException"/> to implicitly convert.</param>
	public static implicit operator JniException?(JResult result) => result is not JResult.Ok ? new(result) : default;
}