namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JObjectArrayLocalRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JObjectArrayLocalRef left, JObjectArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JObjectArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JArrayLocalRef left, JObjectArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JObjectArrayLocalRef left, JObjectArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JObjectArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JArrayLocalRef left, JObjectArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static explicit operator JObjectArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
	/// <inheritdoc/>
	public static explicit operator JObjectArrayLocalRef(JArrayLocalRef arrayRef) => new(arrayRef);
}