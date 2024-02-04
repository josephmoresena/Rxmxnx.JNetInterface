namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IClassFeature
{
	/// <summary>
	/// Retrieves the java class named <paramref name="classHash"/>.
	/// </summary>
	/// <param name="classHash">Class name.</param>
	/// <returns>The class instance with given class name.</returns>
	internal JClassObject GetClass(String classHash);
}