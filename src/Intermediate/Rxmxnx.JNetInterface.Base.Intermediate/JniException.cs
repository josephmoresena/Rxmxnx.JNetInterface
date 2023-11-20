namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents an error that occurs during a JNI call.
/// </summary>
public sealed class JniException : Exception
{
	/// <summary>
	/// JNI result.
	/// </summary>
	public JResult Result { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="result">A <see cref="JResult"/> value.</param>
	public JniException(JResult result) : base(Enum.GetName(result)) => this.Result = result;
}