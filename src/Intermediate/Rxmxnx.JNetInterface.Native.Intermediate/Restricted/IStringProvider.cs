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
	/// The <see cref="String"/> representation of <paramref name="jObject"/>.
	/// </returns>
	String ToString(JReferenceObject jObject);
}