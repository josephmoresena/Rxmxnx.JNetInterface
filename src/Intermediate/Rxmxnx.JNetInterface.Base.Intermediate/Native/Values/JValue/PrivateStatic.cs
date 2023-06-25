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

	/// <summary>
	/// Internal delegate for check default values of <see cref="JValue"/> instances.
	/// </summary>
	private static readonly IsDefaultDelegate isDefault = JValue.GetIsDefault();

	/// <summary>
	/// Retrieves the <see cref="IsDefaultDelegate"/> delegate to use for current process.
	/// </summary>
	/// <returns><see cref="IsDefaultDelegate"/> delegate for current process.</returns>
	private static IsDefaultDelegate GetIsDefault()
	{
		return Environment.Is64BitProcess ? JValue.DefaultLong : JValue.Default;
	}
	/// <summary>
	/// Indicates whether <paramref name="jValue"/> has the <see langword="default"/> value.
	/// </summary>
	/// <param name="jValue"><see cref="JValue"/> value.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jValue"/>  <see langword="default"/>; otherwise;
	/// <see langword="false"/>.
	/// </returns>
	private static Boolean Default(in JValue jValue)
	{
		return jValue._lsi == default && jValue._msi == default;
	}
	/// <summary>
	/// Indicates whether <paramref name="jValue"/> has the <see langword="default"/> value.
	/// </summary>
	/// <param name="jValue"><see cref="JValue"/> value.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jValue"/>  <see langword="default"/>; otherwise;
	/// <see langword="false"/>.
	/// </returns>
	private static Boolean DefaultLong(in JValue jValue)
	{
		return Unsafe.AsRef(jValue).Transform<JValue, Int64>() == default;
	}
}