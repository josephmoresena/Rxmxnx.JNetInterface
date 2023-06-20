namespace Rxmxnx.JNetInterface.Native.Values;

internal partial struct JValue
{
	/// <summary>
	/// Delegate. Indicates whether <paramref name="value"/> has the <see langword="default"/> value.
	/// </summary>
	/// <param name="value"><see cref="JValue"/> value.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="value"/> is <see langword="default"/>; otherwise; 
	/// <see langword="false"/>.
	/// </returns>
	private delegate Boolean IsDefaultDelegate(in JValue value);
}