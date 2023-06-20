namespace Rxmxnx.JNetInterface.Native.References;

internal partial struct JWeakRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JWeakRef left, JWeakRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JWeakRef left, JWeakRef right) => !left.Equals(right);
}
