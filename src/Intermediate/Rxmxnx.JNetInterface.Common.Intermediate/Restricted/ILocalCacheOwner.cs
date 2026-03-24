namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a local cache owner object.
/// </summary>
internal interface ILocalCacheOwner
{
	/// <summary>
	/// Local cache.
	/// </summary>
	LocalCache LocalCache { get; set; }

	/// <summary>
	/// Creates a new local reference frame.
	/// </summary>
	/// <param name="capacity">Frame capacity.</param>
	void CreateLocalFrame(Int32 capacity);
	/// <summary>
	/// Deletes the current local reference frame.
	/// </summary>
	/// <param name="frame">A <see cref="LocalFrame"/> instance.</param>
	/// <param name="result">Current result.</param>
	void DeleteLocalFrame(LocalFrame frame, JLocalObject? result);
}