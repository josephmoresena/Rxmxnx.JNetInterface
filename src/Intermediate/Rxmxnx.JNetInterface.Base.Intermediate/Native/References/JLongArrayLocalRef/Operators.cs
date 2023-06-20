namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JLongArrayLocalRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JLongArrayLocalRef left, JLongArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator ==(JLongArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator ==(JArrayLocalRef left, JLongArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JLongArrayLocalRef left, JLongArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JLongArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JArrayLocalRef left, JLongArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static explicit operator JLongArrayLocalRef(JObjectLocalRef value) => new(value);
    /// <inheritdoc/>
    public static explicit operator JLongArrayLocalRef(JArrayLocalRef value) => new(value);
}