namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an interface instance.
/// </summary>
public interface IInterfaceObject<TInterface> : IObject
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <summary>
	/// Retrieves a <see cref="IArrayObject{TInterface}"/> instance from <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TElement">Type of <see cref="IDataType"/> array element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject{TElement}"/> instance.</param>
	/// <returns>A <see cref="IArrayObject{TInterface}"/> instance.</returns>
	public static IArrayObject<TInterface> CastArray<TElement>(JArrayObject<TElement> jArray)
		where TElement : JLocalObject, IReferenceType<TElement>, IInterfaceObject<TInterface>
		=> new JArrayObject.JCastedArray<TInterface>(jArray);
	/// <summary>
	/// Sets <paramref name="element"/> instance as the <paramref name="jArray"/> element of <paramref name="index"/>.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject{TInterface}"/></param>
	/// <typeparam name="TElement">Type of <see cref="IDataType"/> element.</typeparam>
	/// <param name="index">Index of element to set to.</param>
	/// <param name="element">A <see cref="JLocalObject"/> instance.</param>
	public static void SetElement<TElement>(JArrayObject<TInterface> jArray, Int32 index, TElement? element)
		where TElement : JLocalObject, IReferenceType<TElement>, IInterfaceObject<TInterface>
	{
		IEnvironment env = jArray.Environment;
		env.ArrayFeature.SetObjectElement(jArray, index, element);
	}
}