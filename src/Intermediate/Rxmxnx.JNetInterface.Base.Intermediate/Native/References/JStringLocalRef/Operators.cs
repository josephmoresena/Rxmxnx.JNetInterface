namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JStringLocalRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JStringLocalRef left, JStringLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JStringLocalRef left, JStringLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static explicit operator JStringLocalRef(JObjectLocalRef objRef) => new(objRef);
}