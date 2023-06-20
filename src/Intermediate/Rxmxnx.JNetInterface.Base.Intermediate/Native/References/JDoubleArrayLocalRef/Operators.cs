namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JDoubleArrayLocalRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JDoubleArrayLocalRef left, JDoubleArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JDoubleArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JArrayLocalRef left, JDoubleArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JDoubleArrayLocalRef left, JDoubleArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JDoubleArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JArrayLocalRef left, JDoubleArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static explicit operator JDoubleArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
	/// <inheritdoc/>
	public static explicit operator JDoubleArrayLocalRef(JArrayLocalRef arrayRef) => new(arrayRef);
}