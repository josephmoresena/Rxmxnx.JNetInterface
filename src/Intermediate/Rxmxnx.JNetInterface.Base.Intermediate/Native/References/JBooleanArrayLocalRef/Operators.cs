namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JBooleanArrayLocalRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JBooleanArrayLocalRef left, JBooleanArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JBooleanArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator ==(JArrayLocalRef left, JBooleanArrayLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JBooleanArrayLocalRef left, JBooleanArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JBooleanArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JArrayLocalRef left, JBooleanArrayLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static explicit operator JBooleanArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
	/// <inheritdoc/>
	public static explicit operator JBooleanArrayLocalRef(JArrayLocalRef arrayRef) => new(arrayRef);
}