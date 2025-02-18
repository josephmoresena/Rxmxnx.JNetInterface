namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a VM initialization argument.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
public sealed class JVirtualMachineInitArg
{
	/// <inheritdoc cref="JVirtualMachineInitArg.Version"/>
	private readonly Int32 _version;

	/// <summary>
	/// JNI version.
	/// </summary>
	public Int32 Version => this._version;
	/// <summary>
	/// Initialize options.
	/// </summary>
	public CStringSequence Options { get; init; }
	/// <summary>
	/// Indicates whether initialization ignores any unrecognized option.
	/// </summary>
	public Boolean IgnoreUnrecognized { get; init; }

	/// <summary>
	/// Options text.
	/// </summary>
	[ExcludeFromCodeCoverage]
	private String OptionsString => $"[{String.Join(", ", this.Options)}]";

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="version">JNI version.</param>
	public JVirtualMachineInitArg(Int32 version)
	{
		this._version = version;
		this.Options = CStringSequence.Create(ReadOnlySpan<Char>.Empty); // No allocation.
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">A <see cref="VirtualMachineInitArgumentValue"/> value.</param>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	internal JVirtualMachineInitArg(VirtualMachineInitArgumentValue value)
	{
		this._version = value.Version;
		this.IgnoreUnrecognized = value.IgnoreUnrecognized.Value;
		this.Options = JVirtualMachineInitArg.GetOptions(value);
	}

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override String ToString()
		=> $"{{ {nameof(JVirtualMachineInitArg.Version)} = 0x{this.Version:x8}, {nameof(JVirtualMachineInitArg.Options)} = {this.OptionsString}, {nameof(JVirtualMachineInitArg.IgnoreUnrecognized)} = {this.IgnoreUnrecognized} }}";

	/// <summary>
	/// Copies current options strings to <paramref name="optionSpan"/>.
	/// </summary>
	/// <param name="optionSpan">A <see cref="VirtualMachineInitOptionValue"/> span.</param>
	internal void CopyOptions(Span<VirtualMachineInitOptionValue> optionSpan)
	{
		for (Int32 i = 0; i < optionSpan.Length; i++)
			optionSpan[i] = new(this.Options[i].AsSpan().GetUnsafeValPtr());
	}

	/// <summary>
	/// Retrieves a <see cref="CStringSequence"/> with options information.
	/// </summary>
	/// <param name="value">A <see cref="VirtualMachineInitArgumentValue"/> value.</param>
	/// <returns>A <see cref="CStringSequence"/> instance.</returns>
	private static unsafe CStringSequence GetOptions(VirtualMachineInitArgumentValue value)
	{
		if (value.OptionsLength == 0) return CStringSequence.Create(ReadOnlySpan<Char>.Empty);
		ReadOnlySpan<VirtualMachineInitOptionValue> optionsValue = new(value.Options, value.OptionsLength);
		CString[] options = new CString[optionsValue.Length];
		for (Int32 i = 0; i < optionsValue.Length; i++)
			options[i] = JVirtualMachineInitArg.GetUnsafeCString(optionsValue[i].OptionString);
		return new(options);
	}
	/// <summary>
	/// Retrieves an unsafe <see cref="CString"/> from given pointer.
	/// </summary>
	/// <param name="ptr">A UTF-8 string pointer.</param>
	/// <returns>A <see cref="CString"/> instance.</returns>
	private static CString GetUnsafeCString(ReadOnlyValPtr<Byte> ptr)
	{
		Int32 length = 0;
		while ((ptr + length).Reference != default)
			length++;
		return CString.CreateUnsafe(ptr, length, true);
	}
}