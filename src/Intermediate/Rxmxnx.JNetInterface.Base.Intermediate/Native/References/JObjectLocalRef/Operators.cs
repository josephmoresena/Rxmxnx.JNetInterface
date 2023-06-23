namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JObjectLocalRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JObjectLocalRef left, JObjectLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JObjectLocalRef left, JObjectLocalRef right) => !left.Equals(right);
}