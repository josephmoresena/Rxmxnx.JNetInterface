namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a VM initialization argument.
/// </summary>
public sealed partial class JVirtualMachineInitArg
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
	public IList<JVirtualMachineInitOption> Options { get; init; }
	/// <summary>
	/// Indicates whether initialization ignores any unrecognized option.
	/// </summary>
	public Boolean IgnoreUnrecognized { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="version">JNI version.</param>
	public JVirtualMachineInitArg(Int32 version)
	{
		this._version = version;
		this.Options = Array.Empty<JVirtualMachineInitOption>();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">A <see cref="VirtualMachineInitArgumentValue"/> value.</param>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	internal unsafe JVirtualMachineInitArg(VirtualMachineInitArgumentValue value)
	{
		this._version = value.Version;
		this.IgnoreUnrecognized = value.IgnoreUnrecognized.Value;
		this.Options = new OptionList(JVirtualMachineInitOption.GetOptions(
			                              MemoryMarshal.CreateSpan(
				                              ref Unsafe.AsRef<VirtualMachineInitOptionValue>(value.Options),
				                              value.OptionsLength)));
	}
}