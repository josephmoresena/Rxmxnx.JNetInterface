namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JFloatArrayLocalRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JFloatArrayLocalRef left, JFloatArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JFloatArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JArrayLocalRef left, JFloatArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JFloatArrayLocalRef left, JFloatArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JFloatArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JArrayLocalRef left, JFloatArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static explicit operator JFloatArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
	/// <inheritdoc/>
	public static explicit operator JFloatArrayLocalRef(JArrayLocalRef arrayRef) => new(arrayRef);
}
