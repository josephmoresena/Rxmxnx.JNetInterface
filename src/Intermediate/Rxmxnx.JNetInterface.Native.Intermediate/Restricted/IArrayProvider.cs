namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a java array provider instance.
/// </summary>
public interface IArrayProvider
{
	/// <summary>
	/// Retrieves the array length from <paramref name="jObject"/>
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>The length of <paramref name="jObject"/> array.</returns>
	Int32 GetArrayLength(JReferenceObject jObject);
	/// <summary>
	/// Retrieves the element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <returns>The element with <paramref name="index"/> on <paramref name="jArray"/>.</returns>
	TElement GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index) where TElement : IDataType<TElement>;
	/// <summary>
	/// Sets the element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <param name="value">Element value.</param>
	void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement value)
		where TElement : IDataType<TElement>;
}