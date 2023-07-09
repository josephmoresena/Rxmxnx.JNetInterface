namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a java class provider instance.
/// </summary>
public interface IClassProvider //TODO: Change IClass to JClassObject
{
	/// <summary>
	/// Retrieves the java class named <paramref name="className"/>.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <returns>The class instance with given class name.</returns>
	IClass GetClass(CString className);
	/// <summary>
	/// Retrieves the java class for given type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>The class instance for given type.</returns>
	IClass GetClass<TDataType>() where TDataType : IDataType<TDataType>;
}