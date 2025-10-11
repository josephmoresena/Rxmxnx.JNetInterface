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