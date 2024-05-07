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
	private readonly Boolean _isProxy;

	/// <summary>
	/// Indicates whether current instance is blank,
	/// </summary>
	private Boolean IsBlankSpan()
	{
		foreach (Byte t in this.AsSpan())
		{
			if (t != default)
				return false;
		}
		return true;
	}

	/// <summary>
	/// Retrieves the identifier for given instance.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="jOther">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>The Identifier for the current instance.</returns>
	private static Int64 GetInstanceId(JReferenceObject jObject, JReferenceObject? jOther = default)
	{
		if (jOther is not null && jObject is View) return jOther._id;
		lock (JReferenceObject.sequenceLock)
			return JReferenceObject.sequenceValue++;
	}
}