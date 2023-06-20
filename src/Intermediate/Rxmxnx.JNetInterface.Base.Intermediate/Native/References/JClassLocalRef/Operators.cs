namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JClassLocalRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JClassLocalRef left, JClassLocalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JClassLocalRef left, JClassLocalRef right) => !left.Equals(right);
	/// <inheritdoc/>
	public static explicit operator JClassLocalRef(JObjectLocalRef value) => new(value);
}