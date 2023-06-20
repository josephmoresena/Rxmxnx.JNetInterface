namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JArrayLocalRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static explicit operator JArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
}