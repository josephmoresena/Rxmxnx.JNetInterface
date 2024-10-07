namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class JCallDefinition
{
	/// <summary>
	/// Total size in bytes of call parameters.
	/// </summary>
	private readonly Int32 _callSize;
	/// <summary>
	/// Count of reference parameters.
	/// </summary>
	private readonly Int32 _referenceCount;
	/// <summary>
	/// Call argument's size.
	/// </summary>
	private readonly Int32[] _sizes;
}