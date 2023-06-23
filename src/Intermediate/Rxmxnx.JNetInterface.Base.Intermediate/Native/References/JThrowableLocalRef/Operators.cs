namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JThrowableLocalRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JThrowableLocalRef left, JThrowableLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JThrowableLocalRef left, JThrowableLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static explicit operator JThrowableLocalRef(JObjectLocalRef objRef) => new(objRef);
}