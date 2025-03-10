namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Java native call entry.
/// </summary>
public partial class JNativeCallEntry : IFixedPointer
{
	/// <summary>
	/// Method name.
	/// </summary>
	public CString Name => this._definition.Name;
	/// <summary>
	/// Method descriptor.
	/// </summary>
	public CString Descriptor => this._definition.Descriptor;
	/// <summary>
	/// Definition hash.
	/// </summary>
	public String Hash => this._definition.Hash;
	/// <summary>
	/// Managed function.
	/// </summary>
	public virtual Delegate? Delegate => default;
	/// <inheritdoc/>
	public IntPtr Pointer { get; }
}