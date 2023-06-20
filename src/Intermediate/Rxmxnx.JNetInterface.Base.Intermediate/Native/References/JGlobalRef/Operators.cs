namespace Rxmxnx.JNetInterface.Native.References;

internal partial struct JGlobalRef
{
	/// <inheritdoc/>
	public static Boolean operator ==(JGlobalRef left, JGlobalRef right) => left.Equals(right);
	/// <inheritdoc/>
	public static Boolean operator !=(JGlobalRef left, JGlobalRef right) => !left.Equals(right);
}
