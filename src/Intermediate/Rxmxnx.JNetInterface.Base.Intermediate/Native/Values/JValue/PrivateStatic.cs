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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static IsDefaultDelegate GetIsDefault()
		=> NativeUtilities.SizeOf<IntPtr>() == NativeUtilities.SizeOf<JValue>() ?
			JValue.DefaultPointer :
			JValue.Default;
	/// <summary>
	/// Indicates whether <paramref name="jValue"/> has the <see langword="default"/> value.
	/// </summary>
	/// <param name="jValue"><see cref="JValue"/> value.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jValue"/>  <see langword="default"/>; otherwise;
	/// <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean Default(in JValue jValue) => jValue is { _lsi: 0, _msi: 0, };
	/// <summary>
	/// Indicates whether <paramref name="jValue"/> has the <see langword="default"/> value.
	/// </summary>
	/// <param name="jValue"><see cref="JValue"/> value.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jValue"/>  <see langword="default"/>; otherwise;
	/// <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static Boolean DefaultPointer(in JValue jValue) => jValue._pointerValue == default;
}