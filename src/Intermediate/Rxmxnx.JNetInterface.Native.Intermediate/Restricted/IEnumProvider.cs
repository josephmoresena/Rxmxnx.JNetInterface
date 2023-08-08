namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a JNI enum provider instance.
/// </summary>
public interface IEnumProvider
{
	/// <summary>
	/// Retrieves the current instance name.
	/// </summary>
	/// <param name="jEnum">A <see cref="JEnumObject"/> instance.</param>
	/// <param name="ordinal">Output. Ordinal value.</param>
	/// <returns>The current instance name.</returns>
	String GetName(JEnumObject jEnum, out Int32 ordinal);
	/// <summary>
	/// Retrieves the ordinal value for current enum instance.
	/// </summary>
	/// <param name="jEnum">A <see cref="JEnumObject"/> instance.</param>
	/// <returns>The ordinal value for current enum instance.</returns>
	Int32 GetOrdinal(JEnumObject jEnum);
}