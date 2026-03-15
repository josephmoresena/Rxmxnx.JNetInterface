namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a local cache owner object.
/// </summary>
internal interface ILocalCacheOwner
{
	/// <summary>
	/// Local cache.
	/// </summary>
	LocalCache LocalCache { get; set; } //Change to LocalCache
}