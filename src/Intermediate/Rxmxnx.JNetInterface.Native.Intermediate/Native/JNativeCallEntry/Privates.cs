namespace Rxmxnx.JNetInterface.Native;

public partial class JNativeCallEntry
{
	/// <summary>
	/// Internal call definition.
	/// </summary>
	private readonly JCallDefinition _definition;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="ptr">A <see cref="IntPtr"/> function pointer.</param>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	private JNativeCallEntry(IntPtr ptr, JCallDefinition definition)
	{
		this.Pointer = ptr;
		this._definition = definition;
	}
}