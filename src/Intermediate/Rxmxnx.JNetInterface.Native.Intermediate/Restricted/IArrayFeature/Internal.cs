namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IArrayFeature
{
	/// <summary>
	/// Sets the object element with <paramref name="index"/> on <paramref name="jArray"/>.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="index">Element index.</param>
	/// <param name="value">Object instance.</param>
	internal void SetObjectElement(JArrayObject jArray, Int32 index, JLocalObject? value)
	{
		JArrayTypeMetadata arrayMetadata = jArray.TypeMetadata;
		arrayMetadata.SetObjectElement(jArray, index, value);
	}
}