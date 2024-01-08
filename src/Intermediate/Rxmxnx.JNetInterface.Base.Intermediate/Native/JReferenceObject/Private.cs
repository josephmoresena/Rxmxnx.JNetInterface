namespace Rxmxnx.JNetInterface.Native;

public partial class JReferenceObject
{
	/// <summary>
	/// Sequence lock instance.
	/// </summary>
	private static readonly Object sequenceLock = new();

	/// <summary>
	/// Current sequence value.
	/// </summary>
	private static Int64 sequenceValue = 1;
	/// <summary>
	/// Instance identifier.
	/// </summary>
	private readonly Int64 _id;

	/// <summary>
	/// Indicates whether the current instance is a dummy object (fake java object).
	/// </summary>
	private readonly Boolean _isDummy;

	/// <summary>
	/// Creates the identifier for current instance.
	/// </summary>
	/// <returns>The Identifier for current instance.</returns>
	private static Int64 CreateInstanceId()
	{
		lock (JReferenceObject.sequenceLock)
			return JReferenceObject.sequenceValue++;
	}
}