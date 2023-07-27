namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a JNI string provider instance.
/// </summary>
public interface IStringProvider
{
	/// <summary>
	/// Retrieves the string value from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// A <see cref="String"/> value if <paramref name="jLocal"/> is a <c>java.lang.String</c>;
	/// otherwise, <see langword="null"/>.
	/// </returns>
	String? GetStringValue(JLocalObject jLocal);
}