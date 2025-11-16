namespace Rxmxnx.JNetInterface;

/// <summary>
/// Java runtime versions.
/// </summary>
public enum JRuntimeVersion
{
	/// <summary>
	/// Undefined version.
	/// </summary>
	Undefined = 0x0,
	/// <summary>
	/// Java 1.
	/// </summary>
	SEd0 = 0x00010000,
	/// <summary>
	/// Java Version 1.1.
	/// </summary>
	SEd1 = JRuntimeVersion.SEd0 + 1,
	/// <summary>
	/// Java Version 1.2.
	/// </summary>
	SEd2 = JRuntimeVersion.SEd0 + 2,
	/// <summary>
	/// Java Version 1.3.
	/// </summary>
	SEd3 = JRuntimeVersion.SEd0 + 3,
	/// <summary>
	/// Java Version 1.4.
	/// </summary>
	SEd4 = JRuntimeVersion.SEd0 + 4,
	/// <summary>
	/// Java Version 5 (1.5).
	/// </summary>
	J5 = JRuntimeVersion.SEd0 + 5,
	/// <summary>
	/// Java Version 6 (1.6).
	/// </summary>
	J6 = JRuntimeVersion.SEd0 + 6,
	/// <summary>
	/// Java Version 7 (1.7).
	/// </summary>
	J7 = JRuntimeVersion.SEd0 + 7,
	/// <summary>
	/// Java Version 8 (1.8).
	/// </summary>
	J8 = JRuntimeVersion.SEd0 + 8,
	/// <summary>
	/// Java Version 9.
	/// </summary>
	J9 = JRuntimeVersion.SEd0 * 9,
	/// <summary>
	/// Java Version 10.
	/// </summary>
	J10 = JRuntimeVersion.SEd0 * 10,
	/// <summary>
	/// Java Version 11.
	/// </summary>
	J11 = JRuntimeVersion.SEd0 * 11,
	/// <summary>
	/// Java Version 12.
	/// </summary>
	J12 = JRuntimeVersion.SEd0 * 12,
	/// <summary>
	/// Java Version 12.
	/// </summary>
	J13 = JRuntimeVersion.SEd0 * 13,
	/// <summary>
	/// Java Version 14.
	/// </summary>
	J14 = JRuntimeVersion.SEd0 * 14,
	/// <summary>
	/// Java Version 15.
	/// </summary>
	J15 = JRuntimeVersion.SEd0 * 15,
	/// <summary>
	/// Java Version 16.
	/// </summary>
	J16 = JRuntimeVersion.SEd0 * 16,
	/// <summary>
	/// Java Version 17.
	/// </summary>
	J17 = JRuntimeVersion.SEd0 * 17,
	/// <summary>
	/// Java Version 18.
	/// </summary>
	J18 = JRuntimeVersion.SEd0 * 18,
	/// <summary>
	/// Java Version 19.
	/// </summary>
	J19 = JRuntimeVersion.SEd0 * 19,
	/// <summary>
	/// Java Version 20.
	/// </summary>
	J20 = JRuntimeVersion.SEd0 * 20,
	/// <summary>
	/// Java Version 21.
	/// </summary>
	J21 = JRuntimeVersion.SEd0 * 21,
	/// <summary>
	/// Java Version 22.
	/// </summary>
	J22 = JRuntimeVersion.SEd0 * 22,
	/// <summary>
	/// Java Version 23.
	/// </summary>
	J23 = JRuntimeVersion.SEd0 * 23,
	/// <summary>
	/// Java Version 24.
	/// </summary>
	J24 = JRuntimeVersion.SEd0 * 24,
	/// <summary>
	/// Java Version 25.
	/// </summary>
	J25 = JRuntimeVersion.SEd0 * 25,
}

/// <summary>
/// Extensions for <see cref="JRuntimeVersion"/> enum.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class JRuntimeVersionExtensions
{
	/// <summary>
	/// Retrieves the Java runtime version name.
	/// </summary>
	/// <param name="jreVersion">A <see cref="JRuntimeVersion"/> value.</param>
	/// <returns>The name of <paramref name="jreVersion"/> value.</returns>
	public static String GetRuntimeName(this JRuntimeVersion jreVersion)
		=> jreVersion switch
		{
			JRuntimeVersion.SEd0 => "JRE 1.0",
			JRuntimeVersion.SEd1 => "JRE 1.1",
			JRuntimeVersion.SEd2 => "JRE 1.2",
			JRuntimeVersion.SEd3 => "JRE 1.3",
			JRuntimeVersion.SEd4 => "JRE 1.4",
			JRuntimeVersion.J5 => "JRE 1.5",
			JRuntimeVersion.J6 => "JRE 1.6",
			JRuntimeVersion.J7 => "JRE 1.7",
			JRuntimeVersion.J8 => "JRE 1.8",
			JRuntimeVersion.J9 => "JRE 9.0",
			JRuntimeVersion.J10 => "JRE 10.0",
			JRuntimeVersion.J11 => "JRE 11.0",
			JRuntimeVersion.J12 => "JRE 12.0",
			JRuntimeVersion.J13 => "JRE 13.0",
			JRuntimeVersion.J14 => "JRE 14.0",
			JRuntimeVersion.J15 => "JRE 15.0",
			JRuntimeVersion.J16 => "JRE 16.0",
			JRuntimeVersion.J17 => "JRE 17.0",
			JRuntimeVersion.J18 => "JRE 18.0",
			JRuntimeVersion.J19 => "JRE 19.0",
			JRuntimeVersion.J20 => "JRE 20.0",
			JRuntimeVersion.J21 => "JRE 21.0",
			JRuntimeVersion.J22 => "JRE 22.0",
			JRuntimeVersion.J23 => "JRE 23.0",
			JRuntimeVersion.J24 => "JRE 24.0",
			JRuntimeVersion.J25 => "JRE 25.0",
			_ => JRuntimeVersionExtensions.GetRuntimeName((Int32)jreVersion),
		};

	/// <summary>
	/// Retrieves the Java runtime version name.
	/// </summary>
	/// <param name="jreVersion">A <see cref="Int32"/> value.</param>
	/// <returns>The name of <paramref name="jreVersion"/> value.</returns>
	private static String GetRuntimeName(Int32 jreVersion)
	{
		const Int32 j0 = (Int32)JRuntimeVersion.SEd0;
		return jreVersion > j0 && jreVersion % j0 == 0 ?
			$"JRE {jreVersion / j0}.0" :
			$"JRE 0x{jreVersion:x8}"; // Invalid Version
	}
}