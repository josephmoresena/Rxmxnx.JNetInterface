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
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="data"/>.
	/// </summary>
	/// <param name="data">UTF-16 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	JStringObject Create(ReadOnlySpan<Char> data);
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="utf8Data"/>.
	/// </summary>
	/// <param name="utf8Data">UTF-8 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	JStringObject Create(ReadOnlySpan<Byte> utf8Data);
}