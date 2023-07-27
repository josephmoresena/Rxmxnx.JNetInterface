namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a JNI string provider instance.
/// </summary>
public interface IStringProvider
{
	/// <summary>
	/// Retrieves the string value from <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	/// A <see cref="String"/> value if <paramref name="jObject"/> is a <c>java.lang.String</c>;
	/// otherwise, <see langword="null"/>.
	/// </returns>
	String? GetStringValue(JReferenceObject jObject);
}