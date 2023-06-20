namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JIntArrayLocalRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JIntArrayLocalRef left, JIntArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JIntArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JArrayLocalRef left, JIntArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JIntArrayLocalRef left, JIntArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JIntArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JArrayLocalRef left, JIntArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static explicit operator JIntArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
	/// <inheritdoc/>
	public static explicit operator JIntArrayLocalRef(JArrayLocalRef arrayRef) => new(arrayRef);
}