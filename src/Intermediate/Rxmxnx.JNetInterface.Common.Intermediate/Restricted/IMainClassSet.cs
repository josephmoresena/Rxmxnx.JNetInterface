namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a main class set object.
/// </summary>
internal interface IMainClassSet
{
	/// <summary>
	/// Main classes' information.
	/// </summary>
	IEnumerable<ITypeInformation> ClassesInformation { get; }

	/// <summary>
	/// Indicates whether the class hashed by <paramref name="classHash"/> is main.
	/// </summary>
	/// <param name="classHash">A class hash.</param>
	/// <returns>
	/// <see langword="true"/> if the class hashed by <paramref name="classHash"/> is main; otherwise <see langword="false"/>.
	/// </returns>
	Boolean Contains(String classHash);
}