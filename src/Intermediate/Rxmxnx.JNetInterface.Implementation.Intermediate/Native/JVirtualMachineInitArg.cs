namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores a VM initialization argument.
/// </summary>
public sealed record JVirtualMachineInitArg
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
	public JVirtualMachineInitOption[] Options { get; init; }
	/// <summary>
	/// Indicates whether initialization ignores any unrecognized option.
	/// </summary>
	public Boolean IgnoreUnrecognized { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="version">JNI version.</param>
	internal JVirtualMachineInitArg(Int32 version)
	{
		this._version = version;
		this.Options = Array.Empty<JVirtualMachineInitOption>();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">A <see cref="JVirtualMachineInitArgumentValue"/> value.</param>
	internal JVirtualMachineInitArg(JVirtualMachineInitArgumentValue value)
	{
		this._version = value.Version;
		this.IgnoreUnrecognized = new JBoolean(value.IgnoreUnrecognized).Value;
		this.Options = JVirtualMachineInitOption.GetOptions(MemoryMarshal.CreateSpan(ref value.Options.Reference, value.OptionsLenght));
	}
}