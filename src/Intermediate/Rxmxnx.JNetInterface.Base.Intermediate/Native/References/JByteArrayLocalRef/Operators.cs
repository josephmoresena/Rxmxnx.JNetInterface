namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JByteArrayLocalRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JByteArrayLocalRef left, JByteArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator ==(JByteArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator ==(JArrayLocalRef left, JByteArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JByteArrayLocalRef left, JByteArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JByteArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JArrayLocalRef left, JByteArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static explicit operator JByteArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
    /// <inheritdoc/>
    public static explicit operator JByteArrayLocalRef(JArrayLocalRef arrayRef) => new(arrayRef);
}