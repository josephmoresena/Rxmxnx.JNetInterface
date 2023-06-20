namespace Rxmxnx.JNetInterface.Native.Values;

internal partial struct JValue
{
    /// <summary>
    /// Internal delegate for check default values of <see cref="JValue"/> instances.
    /// </summary>
    private static readonly IsDefaultDelegate isDefault = GetIsDefault();

    /// <summary>
    /// Retrieves the <see cref="IsDefaultDelegate"/> delegate to use for current process.
    /// </summary>
    /// <returns><see cref="IsDefaultDelegate"/> delegate for current process.</returns>
    private static IsDefaultDelegate GetIsDefault() => Environment.Is64BitProcess ? DefaultLong : Default;
    /// <summary>
    /// Indicates whether <paramref name="jValue"/> has the <see langword="default"/> value.
    /// </summary>
    /// <param name="jValue"><see cref="JValue"/> value.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="jValue"/>  <see langword="default"/>; otherwise; 
    /// <see langword="false"/>.
    /// </returns>
    private static Boolean Default(in JValue jValue) => jValue._lsi == default && jValue._msi == default;
    /// <summary>
    /// Indicates whether <paramref name="jValue"/> has the <see langword="default"/> value.
    /// </summary>
    /// <param name="jValue"><see cref="JValue"/> value.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="jValue"/>  <see langword="default"/>; otherwise; 
    /// <see langword="false"/>.
    /// </returns>
    private static Boolean DefaultLong(in JValue jValue) => Unsafe.AsRef(jValue).Transform<JValue, Int64>() == default;
}
